from django.contrib.auth.models import User
from django.db import models

from api.models.base_model import BaseModel
from api.validators import validate_not_past_today


class Project(BaseModel):
    title = models.CharField(max_length=100)
    description = models.TextField()
    dueDate = models.DateField(validators=[validate_not_past_today])
    participants = models.ManyToManyField(
        User,
        related_name='projects'
    )
    