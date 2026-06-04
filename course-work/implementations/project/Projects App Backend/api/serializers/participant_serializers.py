from django.contrib.auth.models import User
from rest_framework import serializers


class AddParticipantSerializer(serializers.Serializer):
    user_id = serializers.PrimaryKeyRelatedField(queryset=User.objects.all())