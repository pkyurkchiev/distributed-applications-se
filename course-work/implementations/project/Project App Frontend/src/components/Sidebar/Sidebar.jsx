import Button from "../UI/Button";
import {
  Link,
  redirect,
  useLoaderData,
  useNavigate,
  useParams,
} from "react-router-dom";

import { fetchProjects } from "../../http requests/projects";
import { useDispatch, useSelector } from "react-redux";
import { useEffect, useState } from "react";
import {
  resetProjectsThunk,
  setProjectsThunk,
  setTaggedProjectsThunk,
} from "../../store/projects-slice";
import { useAuth } from "../../store/AuthContext";
import Projects from "./Projects";

export default function Sidebar() {
  const { user, setUser } = useAuth();
  const dispatch = useDispatch();
  const params = useParams();
  const navigate = useNavigate();

  const [projectsPage, setProjectsPage] = useState(1);
  const [taggedProjectsPage, setTaggedProjectsPage] = useState(1);

  const projects = useSelector((state) => state.projects.projects);

  const tProjects = useSelector((state) => state.projects.taggedProjects);

  useEffect(() => {
    dispatch(setProjectsThunk());
    dispatch(setTaggedProjectsThunk());
  }, [dispatch]);

  function handleNextOwnedProjects() {}

  function logOut() {
    localStorage.removeItem("token");
    navigate("/auth/login");
  }

  return (
    <aside className="flex flex-col w-1/3 px-8 py-16 bg-stone-900 text-stone-50 md:w-72 rounded-r-xl">
      <div className="mb-8">
        <Button onClick={() => logOut()}>Log Out</Button>
      </div>
      <div className="mb-8">
        <Link
          className="px-4 py-2 text-xs md:text-base rounded-md bg-stone-700 text-stone-400 hover:bg-stone-600 hover:text-stone-100"
          to="/project/create"
        >
          + Add Project
        </Link>
      </div>
      {projects?.count > 0 && (
        <Projects
          title={"Your Projects"}
          projects={projects}
          onNext={() => {
            setProjectsPage((p) => p + 1);
            dispatch(setProjectsThunk(projectsPage + 1));
          }}
          onPrevious={() => {
            setProjectsPage((p) => p - 1);
            dispatch(setProjectsThunk(projectsPage - 1));
          }}
        ></Projects>
      )}
      {tProjects?.count > 0 && (
        <Projects
          title={"Tagged Projects"}
          projects={tProjects}
          onNext={() => {
            setTaggedProjectsPage((p) => p + 1);
            dispatch(setTaggedProjectsThunk(taggedProjectsPage + 1));
          }}
          onPrevious={() => {
            setTaggedProjectsPage((p) => p - 1);
            dispatch(setTaggedProjectsThunk(taggedProjectsPage - 1));
          }}
        ></Projects>
      )}
    </aside>
  );
}

export async function projectsLoader() {
  return null;
}
