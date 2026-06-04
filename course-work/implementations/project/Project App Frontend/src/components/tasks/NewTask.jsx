import { useEffect, useState } from "react";

import { Form, redirect, useFetcher, useParams } from "react-router-dom";
import { addTask } from "../../http requests/tasks";
import Input from "../UI/Input";
import { fetchParticipantsByProjectId } from "../../http requests/projects";
import { useAuth } from "../../store/AuthContext";

export default function NewTask({ project, onTaskCreated, participants }) {
  const [inputValue, setInputValue] = useState("");
  const [selectedUser, setSelectedUser] = useState("");
  const params = useParams();
  const { user } = useAuth();
  const fetcher = useFetcher();

  const participants_with_owner = [...participants, user];

  useEffect(() => {
    if (fetcher.state === "idle" && fetcher.data?.success) {
      onTaskCreated();
    }
  }, [fetcher.state, fetcher.data]);

  function handleChange(event) {
    setInputValue(event.target.value);
  }

  return (
    <fetcher.Form className="flex items-center gap-4" method="post">
      <Input
        value={inputValue}
        onChange={handleChange}
        name="name"
        type="text"
        className="w-64 px-2 py-1 bg-stone-200"
        textarea
      ></Input>
      <select
        name="assignedTo"
        value={selectedUser}
        onChange={(e) => setSelectedUser(e.target.value)}
        className="px-2 py-1 bg-stone-200"
      >
        <option value="">Assign user</option>
        {participants_with_owner.map((user) => (
          <option key={user.id} value={user.id}>
            {user.username}
          </option>
        ))}
      </select>
      <button className="text-stone-700 hover:text-stone-950">Add Task</button>
    </fetcher.Form>
  );
}

export async function newTaskToProjectAction({ params, request }) {
  const formData = await request.formData();
  const taskName = formData.get("name");
  const assignedTo = formData.get("assignedTo");

  await addTask({ taskName, assignedTo }, params.projectId);
  return { success: true };
}
