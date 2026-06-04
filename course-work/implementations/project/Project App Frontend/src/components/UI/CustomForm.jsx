import { Form, Link, useActionData, useNavigation } from "react-router-dom";
import Button from "./Button";

export default function CustomForm({ type }) {
  const navigation = useNavigation();
  const data = useActionData();

  const isSubmitting = navigation.state === "submitting";

  return (
    <div className="flex flex-col gap-4 w-full max-w-md mx-auto">
      {data?.message && (
        <div className="p-3 bg-red-50 border border-red-200 text-sm text-red-600 font-medium rounded-lg text-center shadow-sm">
          {data.message.map((message) => message).join(`\n`)}
        </div>
      )}
      <div className="w-full max-w-md mx-auto mt-12 p-8 bg-white border border-stone-200 rounded-xl shadow-md">
        <Form method="post" className="flex flex-col gap-5">
          <div className="mb-2">
            <h2 className="text-3xl font-bold text-stone-800 tracking-tight">
              {type}
            </h2>
          </div>

          <div className="flex flex-col gap-4">
            <div className="flex flex-col gap-1.5">
              <label
                htmlFor="username"
                className="text-sm font-semibold text-stone-700"
              >
                Username
              </label>
              <input
                id="username"
                name="username"
                type="text"
                required
                disabled={isSubmitting}
                className="w-full p-2.5 bg-stone-50 border border-stone-300 rounded shadow-sm text-stone-800 text-sm focus:outline-none focus:ring-2 focus:ring-stone-400 transition-all disabled:opacity-60"
              />
            </div>

            <div className="flex flex-col gap-1.5">
              <label
                htmlFor="password"
                className="text-sm font-semibold text-stone-700"
              >
                Password
              </label>
              <input
                id="password"
                name="password"
                type="password"
                required
                disabled={isSubmitting}
                className="w-full p-2.5 bg-stone-50 border border-stone-300 rounded shadow-sm text-stone-800 text-sm focus:outline-none focus:ring-2 focus:ring-stone-400 transition-all disabled:opacity-60"
              />
            </div>
          </div>

          <div className="mt-2">
            <Button
              type="submit"
              disabled={isSubmitting}
              className="w-full justify-center text-center py-2.5"
            >
              {isSubmitting
                ? type === "Login"
                  ? "Logging in..."
                  : "Registering..."
                : type}
            </Button>
          </div>
        </Form>

        <div className="mt-6 pt-4 border-t border-stone-100 text-center text-sm text-stone-500">
          {type === "Login"
            ? "Don't have a account?"
            : "Already have an account?"}{" "}
          <Link
            to={type === "Login" ? "../register" : "../login"}
            className="font-semibold text-stone-700 hover:text-stone-900 underline underline-offset-4 transition-colors"
          >
            {type === "Login" ? "Register here" : "Sign in"}
          </Link>
        </div>
      </div>
    </div>
  );
}
