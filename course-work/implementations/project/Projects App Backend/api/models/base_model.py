import datetime

from django.contrib.auth.models import User
from django.db import models
from django.utils import timezone


class BaseModel(models.Model):
    createdBy = models.ForeignKey(
        User,
        on_delete=models.CASCADE,
        blank=True,
        null=True,
        related_name='%(class)s_created'
    )

    createdOn = models.DateTimeField(default=timezone.now)

    updatedBy = models.ForeignKey(
        User,
        on_delete=models.SET_NULL,
        blank=True,
        null=True,
        related_name='%(class)s_updated'
    )

    updatedOn = models.DateTimeField(default=timezone.now)

    class Meta:
        abstract = True
