import NewTask from "./NewTask";

import { useNavigate, useParams } from "react-router-dom";
import {
  deleteTask,
  fetchTasksByProjectId,
  updateTaskStatus,
} from "../../http requests/tasks";
import { Fragment, useEffect, useState } from "react";
import { useAuth } from "../../store/AuthContext";

const STATUS_CONFIG = {
  todo: {
    label: "To Do",
    color: "gray",
  },
  in_progress: {
    label: "In Progress",
    color: "blue",
  },
  waiting_for_approval: {
    label: "Waiting for Approval",
    color: "yellow",
  },
  done: {
    label: "Done",
    color: "green",
  },
  canceled: {
    label: "Canceled",
    color: "red",
  },
};

const nextStatus = {
  todo: "in_progress",
  in_progress: "waiting_for_approval",
  waiting_for_approval: "done",
};

export default function Tasks({ project, participants }) {
  const params = useParams();
  const navigate = useNavigate();
  const { user } = useAuth();
  const [tasks, setTasks] = useState([]);
  const [page, setPage] = useState(1);

  useEffect(() => {
    async function fetchTasks() {
      const fetchedTasks = await fetchTasksByProjectId(project.id, page);
      setTasks(fetchedTasks);
    }

    fetchTasks();
  }, [project.id, page]);

  async function updateTasks() {
    const fetchedTasks = await fetchTasksByProjectId(project.id, page);
    setTasks(fetchedTasks);
  }

  async function onTaskDelete(taskId) {
    const response = await deleteTask(taskId);
    updateTasks();
  }

  async function onTaskStatusChange(taskId, status) {
    const response = await updateTaskStatus(taskId, status);
    updateTasks();
  }

  return (
    <section>
      <h2 className="text-2xl font-bold text-stone-700 mb-4">Tasks</h2>
      {project?.createdById == user?.id && (
        <NewTask
          project={project}
          onTaskCreated={updateTasks}
          participants={participants}
        ></NewTask>
      )}
      {tasks.results && tasks.results.length === 0 && (
        <p className="text-stone-800 my-4">
          This project does not have any tasks!
        </p>
      )}
      {tasks.results && tasks.results.length > 0 && (
        <ul className="p4 mt-8 rounded-md">
          {tasks.results.map((task) => {
            return (
              <li
                key={task.id}
                className="grid grid-cols-5 my-2 p-4 border bg-stone-100"
              >
                <span>{task.title}</span>
                <span
                  style={{
                    backgroundColor: STATUS_CONFIG[task.status].color,
                  }}
                  className="justify-self-center p-5 px-10"
                >
                  {STATUS_CONFIG[task.status].label}
                </span>
                <span className="justify-self-center p-5 px-10 font-bold">
                  {task.assignedToUsername}
                </span>

                <Fragment>
                  {(task.status == "todo" || task.status == "in_progress") &&
                    task.assignedTo == user.id && (
                      <button
                        onClick={() =>
                          onTaskStatusChange(task.id, nextStatus[task.status])
                        }
                        className="text-stone-700 hover:text-red-500"
                      >
                        Next Status
                      </button>
                    )}
                  {task.status == "waiting_for_approval" &&
                    project.createdById == user.id && (
                      <button
                        onClick={() =>
                          onTaskStatusChange(task.id, nextStatus[task.status])
                        }
                        className="text-stone-700 hover:text-red-500"
                      >
                        Approve
                      </button>
                    )}
                  {(task.status == "in_progress" ||
                    task.status == "todo" ||
                    task.status == "waiting_for_approval") &&
                    project.createdById == user.id && (
                      <button
                        className="text-stone-700 hover:text-red-500 col-start-5"
                        onClick={() => onTaskStatusChange(task.id, "canceled")}
                      >
                        Cancel
                      </button>
                    )}
                </Fragment>

                {project.createdById == user.id && (
                  <button
                    onClick={() => onTaskDelete(task.id)}
                    className="text-stone-700 hover:text-red-500 col-start-6"
                  >
                    Delete
                  </button>
                )}
              </li>
            );
          })}
        </ul>
      )}
      {(tasks.next || tasks.previous) && (
        <div className="flex items-center justify-center gap-2 mt-6">
          <button
            disabled={!tasks.previous}
            onClick={() => setPage((p) => p - 1)}
            className={`px-4 py-2 rounded ${
              tasks.previous
                ? "bg-stone-200 hover:bg-stone-300"
                : "bg-stone-100 text-stone-400 cursor-not-allowed"
            }`}
          >
            Previous
          </button>

          <span className="px-4 py-2 font-medium">Page {page}</span>

          <button
            disabled={!tasks.next}
            onClick={() => setPage((p) => p + 1)}
            className={`px-4 py-2 rounded ${
              tasks.next
                ? "bg-stone-200 hover:bg-stone-300"
                : "bg-stone-100 text-stone-400 cursor-not-allowed"
            }`}
          >
            Next
          </button>
        </div>
      )}
    </section>
  );
}
