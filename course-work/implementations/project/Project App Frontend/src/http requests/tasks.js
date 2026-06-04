import axios from "axios";

export async function addTask(inputValue, projectId) {
  const response = await axios.post(
    `http://localhost:8000/api/tasks/`,
    {
      title: inputValue.taskName,
      assignedTo: inputValue.assignedTo,
      project: projectId,
    },
    {
      headers: {
        Authorization: `Token ${localStorage.getItem("token")}`,
      },
    },
  );

  return response.data;
}

export async function deleteTask(taskId) {
  const response = await axios.delete(
    `http://localhost:8000/api/tasks/${taskId}/`,
    {
      headers: {
        Authorization: `Token ${localStorage.getItem("token")}`,
      },
    },
  );

  return response;
}

export async function fetchTasksByProjectId(projectId, page) {
  try {
    const response = await axios(
      `http://localhost:8000/api/projects/${projectId}/tasks/?page=${page}`,
    );

    return response.data;
  } catch (err) {
    return err;
  }
}

export async function updateTaskStatus(taskId, status) {
  const response = await axios.patch(
    `http://localhost:8000/api/tasks/${taskId}/`,
    {
      status: status,
    },
    {
      headers: {
        Authorization: `Token ${localStorage.getItem("token")}`,
      },
    },
  );

  return response;
}
