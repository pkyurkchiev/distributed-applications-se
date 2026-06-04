from rest_framework import serializers
from rest_framework.authtoken.admin import User

from api.models.project import Project
from api.models.task import Task


class ProjectSerializer(serializers.ModelSerializer):
    createdByUsername = serializers.CharField(
        source='createdBy.username',
        read_only=True
    )
    createdById = serializers.IntegerField(
        source='createdBy.id',
        read_only=True
    )
    class Meta:
        model = Project
        fields = ["id", "title", "description", "dueDate", "createdByUsername", "createdById"]

    def create(self, validated_data):
        project = Project.objects.create(**validated_data)
        return project

    def create(self, validated_data):
        user = User.objects.create_user(**validated_data)
        return user
