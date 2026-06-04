from rest_framework import status
from rest_framework.generics import ListAPIView
from rest_framework.permissions import IsAuthenticatedOrReadOnly
from rest_framework.response import Response
from rest_framework.views import APIView

from api.models.project import Project
from api.paginators import ProjectPagination
from api.serializers.participant_serializers import AddParticipantSerializer
from api.serializers.project_serialzers import ProjectSerializer
from api.serializers.user_serializers import UserSerializer


class ProjectParticipantsListView(ListAPIView):
    serializer_class = UserSerializer

    def get_queryset(self):
        project = Project.objects.get(id=self.kwargs["project_id"])
        return project.participants.all()


class ProjectsByParticipantListView(ListAPIView):
    serializer_class = ProjectSerializer
    permission_classes = [IsAuthenticatedOrReadOnly]
    pagination_class = ProjectPagination

    def get_queryset(self):
        return (
            Project.objects
            .filter(
                participants=self.request.user
                )
            .distinct()
        )


class AddParticipantView(APIView):
    def post(self, request, project_id, *args, **kwargs):
        try:
            project = Project.objects.get(id=project_id)
        except Project.DoesNotExist:
            return Response(
                {"error": "Project not found."},
                status=status.HTTP_404_NOT_FOUND
            )

        serializer = AddParticipantSerializer(data=request.data)
        if serializer.is_valid():

            user_to_add = serializer.validated_data['user_id']

            if project.createdBy == user_to_add:
                return Response(
                    {"message": f"{user_to_add.username} is the project owner."},
                    status=status.HTTP_400_BAD_REQUEST
                )
            elif project.participants.filter(id=user_to_add.id).exists():
                return Response(
                    {"message": f"{user_to_add.username} is already a participant."},
                    status=status.HTTP_400_BAD_REQUEST
                )
            elif project.participants.count() >= 10:
                return Response(
                    {"message": f"You have reached the limit of 10 participants"},
                    status=status.HTTP_400_BAD_REQUEST
                )

            project.participants.add(user_to_add)

            user_data = UserSerializer(user_to_add).data

            return Response(
                {"message": f"Successfully added {user_to_add.username} to the project.", "user": user_data},
                status=status.HTTP_200_OK
            )

        return Response(serializer.errors, status=status.HTTP_400_BAD_REQUEST)


class RemoveParticipantView(APIView):

    def post(self, request, project_id, *args, **kwargs):
        try:
            project = Project.objects.get(id=project_id)
        except Project.DoesNotExist:
            return Response(
                {"error": "Project not found."},
                status=status.HTTP_404_NOT_FOUND
            )

        serializer = AddParticipantSerializer(data=request.data)
        if serializer.is_valid():
            user_to_remove = serializer.validated_data['user_id']

            if project.createdBy == user_to_remove:
                return Response(
                    {"message": f"Cannot remove {user_to_remove.username} because they are the project owner."},
                    status=status.HTTP_400_BAD_REQUEST
                )

            elif not project.participants.filter(id=user_to_remove.id).exists():
                return Response(
                    {"message": f"{user_to_remove.username} is not a participant in this project."},
                    status=status.HTTP_400_BAD_REQUEST
                )

            # 5. Execute relationship removal
            project.participants.remove(user_to_remove)

            # Serialize the removed user's data to return back to the UI
            user_data = UserSerializer(user_to_remove).data

            return Response(
                {
                    "message": f"Successfully removed {user_to_remove.username} from the project.",
                    "user": user_data
                },
                status=status.HTTP_200_OK
            )

        return Response(serializer.errors, status=status.HTTP_400_BAD_REQUEST)
