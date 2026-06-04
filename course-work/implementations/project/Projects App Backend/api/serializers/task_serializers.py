from rest_framework import serializers

from api.models.task import Task


class TaskSerializer(serializers.ModelSerializer):
    createdBy = serializers.CharField(
        source='createdBy.username',
        read_only=True
    )
    assignedToUsername = serializers.CharField(
        source = 'assignedTo.username',
        read_only=True
    )

    class Meta:
        model = Task
        fields = '__all__'

