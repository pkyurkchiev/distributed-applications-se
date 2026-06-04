from django.utils import timezone
from rest_framework import viewsets
from rest_framework.permissions import IsAuthenticatedOrReadOnly
from rest_framework.response import Response

from api.models.project import Project
from api.paginators import ProjectPagination
from api.serializers.project_serialzers import ProjectSerializer


class ProjectViewSet(viewsets.ModelViewSet):
    permission_classes = (IsAuthenticatedOrReadOnly, )
    serializer_class = ProjectSerializer
    pagination_class = ProjectPagination
    queryset = Project.objects.all()

    def perform_create(self, serializer):
        serializer.save(createdBy=self.request.user, updatedBy=self.request.user)

    def perform_update(self, serializer):
        serializer.save(updatedBy=self.request.user, updatedOn=timezone.now())

    def list(self, request, *args, **kwargs):
        queryset = self.filter_queryset(self.get_queryset().filter(createdBy=self.request.user))

        page = self.paginate_queryset(queryset)
        if page is not None:
            serializer = self.get_serializer(page, many=True)
            return self.get_paginated_response(serializer.data)

        serializer = self.get_serializer(queryset, many=True)
        return Response(serializer.data)
