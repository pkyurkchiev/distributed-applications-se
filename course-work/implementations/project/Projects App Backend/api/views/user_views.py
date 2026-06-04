from django.contrib.auth.models import User
from rest_framework.generics import CreateAPIView, ListAPIView
from rest_framework.permissions import AllowAny, IsAuthenticatedOrReadOnly
from rest_framework.response import Response
from rest_framework.views import APIView

from api.serializers.user_serializers import CreateUserSerializer, UserSerializer


class CreateUserView(CreateAPIView):
    permission_classes = [AllowAny]
    serializer_class = CreateUserSerializer
    queryset = User.objects.all()


class GetUserData(APIView):
    permission_classes = [IsAuthenticatedOrReadOnly]

    def get(self, request):
        return Response(
            {
                "id": request.user.id,
                "username": request.user.username
            }
        )


class UserByUsernameListView(ListAPIView):
    serializer_class = UserSerializer
    permission_classes = [IsAuthenticatedOrReadOnly]

    def get_queryset(self):
        query = self.request.data.get('query', '').strip()

        if len(query) >= 3:
            return User.objects.filter(username__icontains=query)

        return User.objects.none()

    def post(self, request, *args, **kwargs):
        return self.list(request, *args, **kwargs)
