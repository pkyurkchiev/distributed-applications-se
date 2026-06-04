from django.urls import include, path


from rest_framework.routers import DefaultRouter
from rest_framework.authtoken.views import obtain_auth_token

from api.views.participant_views import ProjectParticipantsListView, ProjectsByParticipantListView, AddParticipantView, \
    RemoveParticipantView
from api.views.project_views import ProjectViewSet
from api.views.task_views import TasksViewSet, TasksByProjectListView
from api.views.user_views import CreateUserView, GetUserData, UserByUsernameListView

router = DefaultRouter()
router.register(r'projects', ProjectViewSet, basename='project')
router.register(r'tasks', TasksViewSet, basename='task')

urlpatterns = [
    path('', include(router.urls)),
    path('token-auth/', obtain_auth_token),
    path('register/', CreateUserView.as_view()),
    path('me/', GetUserData.as_view()),
    path(
        "projects/<int:project_id>/participants/",
        ProjectParticipantsListView.as_view()
    ),
    path(
        "projects/<int:project_id>/tasks/",
        TasksByProjectListView.as_view()
    ),
    path(
        "tagged_projects/",
        ProjectsByParticipantListView.as_view()
    ),
    path(
        "users/search/",
        UserByUsernameListView.as_view()
    ),
    path(
        "projects/<int:project_id>/add-participant/",
        AddParticipantView.as_view()
    ),
    path('projects/<int:project_id>/remove-participant/',
         RemoveParticipantView.as_view()
    ),
]