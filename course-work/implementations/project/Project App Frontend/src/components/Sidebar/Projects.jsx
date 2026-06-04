import { useState } from "react";
import { useDispatch } from "react-redux";
import { Link, useParams } from "react-router-dom";

export default function Projects({ projects, title, onNext, onPrevious }) {
  const params = useParams();

  return (
    <div className="mb-8">
      <h2 className="font-bold uppercase md:text-xl text-stone-200">{title}</h2>
      <ul className="mt-2">
        {projects &&
          projects?.results?.map((project) => {
            let cssClasses =
              "w-full flex text-center px-2 py-1 rounded-sm my-1  hover:text-stone-200 hover:bg-stone-800";
            if (project.id === params.projectId) {
              cssClasses += " bg-stone-800 text-stone-200";
            } else {
              cssClasses += " text-stone-400";
            }

            return (
              <li key={project.id}>
                <Link to={`/project/${project.id}`} className={cssClasses}>
                  {project.title}
                </Link>
              </li>
            );
          })}
      </ul>
      <div className="flex items-center justify-center gap-2 mt-4">
        <button
          className={`px-3 py-1 rounded transition-colors ${
            projects.previous
              ? "bg-stone-800 text-stone-300 hover:bg-stone-700"
              : "bg-stone-900 text-stone-600 cursor-not-allowed"
          }`}
          disabled={!projects.previous}
          onClick={onPrevious}
        >
          {"<"}
        </button>
        <button
          className={`px-3 py-1 rounded transition-colors ${
            projects.next
              ? "bg-stone-800 text-stone-300 hover:bg-stone-700"
              : "bg-stone-900 text-stone-600 cursor-not-allowed"
          }`}
          disabled={!projects.next}
          onClick={onNext}
        >
          {">"}
        </button>
      </div>
    </div>
  );
}
