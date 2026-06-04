import { configureStore } from "@reduxjs/toolkit";
import { projectsSlice } from "./projects-slice";
export const store = configureStore({
  reducer: {
    projects: projectsSlice.reducer,
  },
});
