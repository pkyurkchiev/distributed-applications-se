import { createBrowserRouter, RouterProvider } from "react-router-dom";
import Root from "./pages/Root";
import NoProjectSelected from "./components/projects/NoProjectSelected";
import NewProject from "./components/projects/NewProject";
import SelectedProject from "./components/projects/SelectedProject";

import { loader as selectedProjectLoader } from "./components/projects/SelectedProject";
import { newTaskToProjectAction } from "./components/tasks/NewTask";
import { projectsLoader } from "./components/Sidebar/Sidebar";

import RegisterForm, {
  action as registerAction,
} from "./components/auth/RegisterForm";
import LoginForm, { action as logInAction } from "./components/auth/LoginForm";
import AuthRoot from "./pages/RegisterPage";
import { Provider } from "react-redux";
import { store } from "./store";
import { AuthProvider } from "./store/AuthContext";

const router = createBrowserRouter([
  {
    path: "/",
    element: <Root></Root>,
    loader: projectsLoader,
    shouldRevalidate: () => {
      return true;
    },
    children: [
      {
        index: true,
        element: <NoProjectSelected></NoProjectSelected>,
      },
      {
        path: "project",
        children: [
          {
            path: "create",
            element: <NewProject />,
          },
          {
            path: ":projectId",
            element: <SelectedProject></SelectedProject>,
            loader: selectedProjectLoader,
            action: newTaskToProjectAction,
            shouldRevalidate: () => {
              return true;
            },
          },
        ],
      },
    ],
  },
  {
    path: "auth",
    element: <AuthRoot></AuthRoot>,
    children: [
      {
        path: "login",
        element: <LoginForm></LoginForm>,
        action: logInAction,
      },
      {
        path: "register",
        element: <RegisterForm></RegisterForm>,
        action: registerAction,
      },
    ],
  },
]);

export default function App() {
  return (
    <Provider store={store}>
      <AuthProvider>
        <RouterProvider router={router}></RouterProvider>
      </AuthProvider>
    </Provider>
  );
}
