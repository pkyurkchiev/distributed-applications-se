import axios from "axios";

export async function fetchProjects(page) {
  try {
    const p = page || 1;

    const response = await axios.get(
      `http://localhost:8000/api/projects/?page=${p}`,
      {
        headers: {
          Authorization: `Token ${localStorage.getItem("token")}`,
        },
      },
    );

    return response.data;
  } catch (err) {
    return err;
  }
}

export async function fetchTaggedProjects(page) {
  try {
    const p = page || 1;

    const response = await axios.get(
      `http://localhost:8000/api/tagged_projects/?page=${p}`,
      {
        headers: {
          Authorization: `Token ${localStorage.getItem("token")}`,
        },
      },
    );

    return response.data;
  } catch (err) {
    return err;
  }
}

export async function fetchProjectById(projectId) {
  const response = await axios.get(
    `http://localhost:8000/api/projects/${projectId}`,
    {
      headers: {
        Authorization: `Token ${localStorage.getItem("token")}`,
      },
    },
  );

  return response.data;
}

export async function deleteProjectById(projectId) {
  const response = await axios.delete(
    `http://localhost:8000/api/projects/${projectId}/`,
    {
      headers: {
        Authorization: `Token ${localStorage.getItem("token")}`,
      },
    },
  );

  return response;
}

export async function addProject(project) {
  try {
    const response = await axios.post(
      `http://localhost:8000/api/projects/`,
      project,
      {
        headers: {
          Authorization: `Token  ${localStorage.getItem("token")}`,
        },
      },
    );

    return response.data;
  } catch (err) {
    return err;
  }
}

export async function editProject(project) {
  try {
    const response = await axios.patch(
      `http://localhost:8000/api/projects/${project.id}/`,
      project,
      {
        headers: {
          Authorization: `Token  ${localStorage.getItem("token")}`,
        },
      },
    );

    return response.data;
  } catch (err) {
    return err;
  }
}

export async function fetchParticipantsByProjectId(projectId) {
  try {
    const response = await axios.get(
      `http://localhost:8000/api/projects/${projectId}/participants`,
      {
        headers: {
          Authorization: `Token  ${localStorage.getItem("token")}`,
        },
      },
    );

    return response.data;
  } catch (err) {
    return err;
  }
}

export async function addUserToProject(projectId, userId) {
  try {
    const response = await axios.post(
      `http://localhost:8000/api/projects/${projectId}/add-participant/`,
      {
        user_id: userId,
      },
      {
        headers: {
          Authorization: `Token  ${localStorage.getItem("token")}`,
        },
      },
    );

    return response;
  } catch (err) {
    return err;
  }
}

export async function removeUserFromProject(projectId, userId) {
  try {
    const response = await axios.post(
      `http://localhost:8000/api/projects/${projectId}/remove-participant/`,
      {
        user_id: userId,
      },
      {
        headers: {
          Authorization: `Token  ${localStorage.getItem("token")}`,
        },
      },
    );

    return response;
  } catch (err) {
    return err;
  }
}
