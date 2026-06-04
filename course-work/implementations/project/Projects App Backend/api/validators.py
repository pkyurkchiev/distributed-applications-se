from datetime import date

from django.core.exceptions import ValidationError


def validate_not_past_today(value):
    if value < date.today():
        raise ValidationError("The due date cannot be set in the past!")