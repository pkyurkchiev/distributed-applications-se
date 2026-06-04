import Button from "../UI/Button";
import { Form, redirect, useActionData } from "react-router-dom";
import { Link } from "react-router-dom";
import { createUser, getToken } from "../../http requests/auth";
import CustomForm from "../UI/CustomForm";
export default function RegisterForm() {
  const actionData = useActionData();

  return (
    <>
      <CustomForm type="Register"></CustomForm>
    </>
  );
}

export async function action({ request }) {
  const formData = await request.formData();
  const credentials = {
    username: formData.get("username"),
    password: formData.get("password"),
  };

  const response = await createUser(credentials);

  if (response.status === 400) {
    return { message: Object.values(response.data) };
  }

  localStorage.setItem("token", response.data.token);
  return redirect("/");
}
