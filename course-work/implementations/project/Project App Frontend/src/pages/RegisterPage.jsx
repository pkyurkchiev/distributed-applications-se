import { Outlet, useNavigate, useNavigation } from "react-router-dom";

export default function AuthRoot() {
  return (
    <main className="flex flex-col items-center mt-4">
      <Outlet></Outlet>
    </main>
  );
}
