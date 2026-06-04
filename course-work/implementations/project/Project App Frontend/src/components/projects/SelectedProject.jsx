import Tasks from "../tasks/Tasks";
import { useLoaderData, useNavigate, useParams } from "react-router-dom";
import {
  fetchProjectById,
  deleteProjectById,
  editProject,
  addUserToProject,
  fetchParticipantsByProjectId,
  removeUserFromProject,
} from "../../http requests/projects";
import { useEffect, useState } from "react";
import Input from "../UI/Input";
import {
  setProjectsThunk,
  updateProjectsThunk,
} from "../../store/projects-slice";
import { useDispatch } from "react-redux";
import { useAuth } from "../../store/AuthContext";
import UserSearchCombobox from "./UserSearchCombobox";
import RemoveParticipantCombobox from "./RemoveParticipantCombobox";

export default function SelectedProject() {
  const [isEditing, setIsEditing] = useState(false);
  const params = useParams();
  const project = useLoaderData();
  const [inputData, setInputData] = useState({});
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const { user } = useAuth();

  const [participants, setParticipants] = useState([]);

  useEffect(() => {
    setIsEditing(false);
    setInputData({
      title: project.title,
      dueDate: project.dueDate,
      description: project.description,
      createdById: project.createdById,
      createdByUsername: project.createdByUsername,
    });
  }, [params.projectId]);

  useEffect(() => {
    async function loadParticipants() {
      let data = await fetchParticipantsByProjectId(project.id);
      setParticipants(data);
    }

    loadParticipants();
  }, [params.projectId]);

  const formattedDate = new Date(inputData.dueDate).toLocaleDateString(
    "en-US",
    {
      year: "numeric",
      month: "short",
      day: "numeric",
    },
  );

  async function handleDeleteProject() {
    const response = await deleteProjectById(project.id);
    if (response.status === 204) {
      dispatch(setProjectsThunk());
      navigate("/");
    }
  }

  function handleEditing() {
    if (isEditing) {
      editProject({ id: project.id, ...inputData });
      dispatch(updateProjectsThunk({ id: project.id, ...inputData }));
      setIsEditing(false);
    } else {
      setIsEditing(true);
    }
  }

  function handleChange(event, field) {
    if (field === "title") {
      setInputData((prevState) => {
        return {
          ...prevState,
          title: event.target.value,
        };
      });
    } else if (field === "description") {
      setInputData((prevState) => {
        return {
          ...prevState,
          description: event.target.value,
        };
      });
    } else {
      setInputData((prevState) => {
        return {
          ...prevState,
          date: event.target.value,
        };
      });
    }
  }

  async function handleAddUserToProject(userToAdd) {
    const response = await addUserToProject(params.projectId, userToAdd.id);

    if (response.status == 200) {
      setParticipants((prevState) => [...prevState, response.data.user]);
    }

    return response;
  }

  async function handleRemoveUserFromProject(userToRemove) {
    const response = await removeUserFromProject(
      params.projectId,
      userToRemove.id,
    );

    if (response.status == 200) {
      setParticipants((prevState) =>
        prevState.filter((participant) => participant.id !== userToRemove.id),
      );
    }

    return response;
  }

  return (
    <div className="col-start-2 col-end-3 mt-16 pr-20">
      <header className="pb-4 mb-4 border-b-2 border-stone-300">
        <div className="flex items-center justify-between">
          {!isEditing && (
            <h1 className="text-3xl font-vold font-bold mb-2">
              {inputData.title}
            </h1>
          )}
          {isEditing && (
            <input
              onChange={(event) => handleChange(event, "title")}
              className="text-3xl font-vold text-stone-600 mb-2"
              value={inputData.title}
            ></input>
          )}
          {user?.id == project?.createdById && (
            <div className="flex gap-6">
              <button
                className="text-stone-600 hover:text-stone-950"
                onClick={handleEditing}
              >
                {isEditing ? "Submit Changes" : "Edit"}
              </button>
              <button
                onClick={handleDeleteProject}
                className="text-stone-600 hover:text-stone-950"
              >
                Delete
              </button>
            </div>
          )}
        </div>
        {!isEditing && (
          <>
            <p className="mb-2 text-stone-600">
              By: {inputData.createdByUsername}
            </p>
            {participants.length > 0 && (
              <p className="mb-2 text-stone-600">
                Current Participants:{" "}
                {participants
                  .map((participant) => participant.username)
                  .join(", ")}
              </p>
            )}
            <p className="mb-4 text-stone-400">Due date: {formattedDate}</p>
            <p className="mt-2 border-t-2 border-stone-300 text-stone-600 whitespace-pre-wrap">
              {inputData.description}
            </p>
          </>
        )}
        {isEditing && (
          <>
            <Input
              type="date"
              value={inputData.dueDate}
              onChange={(event) => handleChange(event, "date")}
            ></Input>
            <Input
              onChange={(event) => handleChange(event, "description")}
              value={inputData.description}
              textarea
            ></Input>
          </>
        )}
      </header>
      {user?.id == project.createdById && (
        <>
          <UserSearchCombobox
            handleAddUserToProject={handleAddUserToProject}
          ></UserSearchCombobox>
          <RemoveParticipantCombobox
            participants={participants}
            handleRemoveUserFromProject={handleRemoveUserFromProject}
          ></RemoveParticipantCombobox>
        </>
      )}
      <Tasks project={project} participants={participants}></Tasks>
    </div>
  );
}

export async function loader({ params, request }) {
  const project = await fetchProjectById(params.projectId);
  return project;
}
