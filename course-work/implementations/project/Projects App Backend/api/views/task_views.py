from django.utils import timezone
from rest_framework import viewsets
from rest_framework.generics import ListAPIView
from rest_framework.permissions import IsAuthenticatedOrReadOnly

from api.models.task import Task
from api.paginators import TasksPagination
from api.serializers.task_serializers import TaskSerializer


class TasksViewSet(viewsets.ModelViewSet):
    permission_classes = (IsAuthenticatedOrReadOnly, )
    serializer_class = TaskSerializer

    def get_queryset(self):
        return Task.objects.all()
    def perform_create(self, serializer):
        serializer.save(createdBy=self.request.user, updatedBy=self.request.user)

    def perform_update(self, serializer):
        serializer.save(updatedBy=self.request.user, updatedOn=timezone.now())

class TasksByProjectListView(ListAPIView):
    permission_classes = (IsAuthenticatedOrReadOnly, )
    serializer_class = TaskSerializer
    pagination_class = TasksPagination

    def get_queryset(self, project_id=None):
        queryset = Task.objects.filter(project__id=self.kwargs['project_id']).order_by("-status")
        return queryset
