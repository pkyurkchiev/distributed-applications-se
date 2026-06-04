from django.contrib.auth.models import User
from django.db import models

from api.models.base_model import BaseModel
from api.models.project import Project


class Task(BaseModel):
    title = models.CharField(max_length=100)
    project = models.ForeignKey(Project, on_delete=models.CASCADE, blank=True, null=True, related_name='tasks')
    assignedTo = models.ForeignKey(User, on_delete=models.CASCADE, blank=True, null=True, related_name='assigned_tasks')

    class Status(models.TextChoices):
        TODO = "todo", "To Do"
        IN_PROGRESS = "in_progress", "In Progress"
        WAITING_FOR_APPROVAL = "waiting_for_approval", "Waiting for approval"
        DONE = "done", "Done"
        CANCELED = "canceled", "Canceled"

    status = models.CharField(
        max_length=20,
        choices=Status.choices,
        default=Status.TODO
    )
