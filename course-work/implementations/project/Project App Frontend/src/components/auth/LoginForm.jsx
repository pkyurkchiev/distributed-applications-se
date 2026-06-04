import Button from "../UI/Button";
import {
  Form,
  redirect,
  useActionData,
  useNavigate,
  useNavigation,
} from "react-router-dom";
import { Link } from "react-router-dom";
import { getCurrentUser, getToken } from "../../http requests/auth";
import CustomForm from "../UI/CustomForm";
import { useAuth } from "../../store/AuthContext";
import { useDispatch } from "react-redux";

export default function LoginForm() {
  const data = useActionData();
  const { setUser } = useAuth();
  const navigate = useNavigate();
  const dispatch = useDispatch();

  async function handleLogin(data) {
    localStorage.setItem("token", data.token);
    const user = await getCurrentUser();
    setUser(user);
    navigate("/");
  }

  if (data?.token) {
    handleLogin(data);
  }
  return <CustomForm type="Login" />;
}

export async function action({ request }) {
  const formData = await request.formData();
  const credentials = {
    username: formData.get("username"),
    password: formData.get("password"),
  };

  const response = await getToken(credentials);

  if (response.status === 400) {
    return { message: ["Invalid username or password"] };
  }

  return response.data;
}
