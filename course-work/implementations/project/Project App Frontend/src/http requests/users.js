import axios from "axios";

export async function fetchUsersByUsername(query) {
  const response = await axios.post(
    `http://localhost:8000/api/users/search/`,
    {
      query,
    },
    {
      headers: {
        Authorization: `Token ${localStorage.getItem("token")}`,
      },
    },
  );

  return response.data;
}
