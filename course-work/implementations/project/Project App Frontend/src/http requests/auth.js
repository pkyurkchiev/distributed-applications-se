import axios from "axios";

export async function getToken(fd) {
  try {
    const response = await axios.post("http://localhost:8000/api/token-auth/", {
      username: fd.username, //fd.get("username")
      password: fd.password, //fd.get("password")
    });

    return response;
  } catch (err) {
    return err;
  }
}

export async function createUser(fd) {
  try {
    await axios.post("http://localhost:8000/api/register/", {
      username: fd.username, //fd.get("username")
      password: fd.password, //fd.get("password")
    });

    const response = await getToken(fd);

    return response;
  } catch (err) {
    return err.response;
  }
}

export async function getCurrentUser() {
  const token = localStorage.getItem("token");

  if (!token) {
    return null;
  }

  const response = await axios.get(`http://localhost:8000/api/me/`, {
    headers: {
      Authorization: `Token ${token}`,
    },
  });

  return response.data;
}
