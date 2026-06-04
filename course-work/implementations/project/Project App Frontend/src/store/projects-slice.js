import { createSlice } from "@reduxjs/toolkit";
import {
  addProject,
  fetchProjects,
  fetchTaggedProjects,
} from "../http requests/projects";

export const projectsSlice = createSlice({
  name: "projects",
  initialState: {
    projects: {
      count: 0,
      next: null,
      previous: null,
      results: [],
    },
    taggedProjects: {
      count: 0,
      next: null,
      previous: null,
      results: [],
    },
  },
  reducers: {
    setProjects(state, action) {
      state.projects = action.payload.projects;
    },
    setTaggedProjects(state, action) {
      state.taggedProjects = action.payload.projects;
    },
    updateProjects(state, action) {
      const index = state.projects.results.findIndex(
        (p) => p.id === action.payload.id,
      );
      state.projects[index] = action.payload;
    },
    resetProjects(state, action) {
      state.projects = [];
    },
    addProjectSuccess(state, action) {
      if (state.projects.results.length < 5) {
        state.projects.results.push(action.payload);
        state.projects.count++;
      }
    },
  },
});

export function setProjectsThunk(page) {
  return async function (dispatch) {
    const projects = await fetchProjects(page);
    dispatch(projectsSlice.actions.setProjects({ projects }));
  };
}

export function setTaggedProjectsThunk(page) {
  return async function (dispatch) {
    const projects = await fetchTaggedProjects(page);
    dispatch(projectsSlice.actions.setTaggedProjects({ projects }));
  };
}

export function updateProjectsThunk(project) {
  return async function (dispatch) {
    dispatch(projectsSlice.actions.updateProjects(project));
  };
}

export function resetProjectsThunk() {
  return async function (dispatch) {
    dispatch(projectsSlice.actions.resetProjects());
  };
}

export function createProjectThunk(project) {
  return async function (dispatch) {
    const response = await addProject(project);
    dispatch(projectsSlice.actions.addProjectSuccess(response));
    return response;
  };
}
