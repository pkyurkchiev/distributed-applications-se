from rest_framework.pagination import PageNumberPagination
class TasksPagination(PageNumberPagination):
    page_size = 6


class ProjectPagination(PageNumberPagination):
    page_size = 5
