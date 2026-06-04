import { Outlet } from "react-router-dom";
import Sidebar from "../components/Sidebar/Sidebar";

export default function Root() {
  return (
    <main className="my-8 grid grid-cols-[auto_1fr] gap-8 min-h-[40rem]">
      <Sidebar />
      <Outlet></Outlet>
    </main>
  );
}
