import Input from "../UI/Input";
import { useRef } from "react";
import Modal from "../UI/Modal";
import { useEffect } from "react";
import { Form, Link, redirect, useNavigate } from "react-router-dom";
import { addProject } from "../../http requests/projects";
import { useDispatch } from "react-redux";
import {
  createProjectThunk,
  setProjectsThunk,
} from "../../store/projects-slice";

export default function NewProject() {
  const dispatch = useDispatch();
  const navigate = useNavigate();

  const today = new Date().toISOString().split("T")[0];

  async function triggerFormSubmission(e) {
    e.preventDefault();

    const formData = new FormData(e.currentTarget);
    const project = {
      title: formData.get("title"),
      description: formData.get("description"),
      dueDate: formData.get("date"),
    };

    const result = await dispatch(createProjectThunk(project));

    navigate(`/project/${result.id}`);
  }

  return (
    <>
      <Modal buttonCaption="Close">
        <h2 className="text-xl font-bold text-stone-700 my-4">Invalid Input</h2>
        <p className="text-stone-600 mb-4">There are fields without a value</p>
        <p className="text-stone-600 mb-4">
          Please make sure you provide a value!
        </p>
      </Modal>
      <div className="mt-16 pr-20">
        <Form onSubmit={triggerFormSubmission}>
          <Input label="Title" type="text" name="title"></Input>
          <Input label="Description" textarea name="description"></Input>
          <Input label="Due date" type="date" name="date" min={today}></Input>

          <div className="flex items-center justify-end gap-4 my-4">
            <Link className="text-stone-800 hover:text-stone-950" to="/">
              Cancel
            </Link>
            <button
              className="px-6 py-2 rounded-md bg-stone-800 text-stone-50 hover:bg-stone-950"
              type="submit"
            >
              Save
            </button>
          </div>
        </Form>
      </div>
    </>
  );
}
