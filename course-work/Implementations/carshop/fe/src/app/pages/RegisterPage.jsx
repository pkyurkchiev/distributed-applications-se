import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import apiClient from '../../services/ApiClient';

const Register = () => {
  const navigate = useNavigate();

  const [form, setForm] = useState({
    username: "",
    password: "",
    confirmPassword: "",
    firstName: "",
    lastName: "",
    emailAddress: "",
    phoneNumber: ""
  });

  const [errors, setErrors] = useState({});
  const [apiError, setApiError] = useState(null);
  const [loading, setLoading] = useState(false);

  const validate = () => {
    const newErrors = {};

    if (!form.username.trim()) {
      newErrors.username = "Username is required";
    } else if (form.username.length < 4 || form.username.length > 100) {
      newErrors.username = "Username must be between 4 and 100 characters";
    }

    if (!form.password) {
      newErrors.password = "Password is required";
    } else if (!/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$/.test(form.password)) {
      newErrors.password = "Password must be at least 8 characters, with upper/lowercase, digit, and special character";
    }

    if (!form.confirmPassword) {
      newErrors.confirmPassword = "Please confirm your password";
    } else if (form.password !== form.confirmPassword) {
      newErrors.confirmPassword = "Passwords do not match";
    }

    if (!form.firstName.trim()) {
      newErrors.firstName = "First name is required";
    }

    if (!form.lastName.trim()) {
      newErrors.lastName = "Last name is required";
    }

    if (!form.emailAddress.trim()) {
      newErrors.emailAddress = "Email is required";
    } else if (!/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(form.emailAddress)) {
      newErrors.emailAddress = "Invalid email format";
    }

    if (!form.phoneNumber.trim()) {
      newErrors.phoneNumber = "Phone number is required";
    } else if (!/^\+?[0-9]{7,15}$/.test(form.phoneNumber)) {
      newErrors.phoneNumber = "Invalid phone number";
    }

    setErrors(newErrors);

    return Object.keys(newErrors).length === 0;
  };

  const handleChange = e => {
    setForm({ ...form, [e.target.name]: e.target.value });
  };

  const handleSubmit = async e => {
    e.preventDefault();
    setApiError(null);

    if (!validate()) return;

    setLoading(true);

    try {
      await apiClient.post("auth/register", form);
      navigate('/login');
    } catch (error) {
      setApiError(error.response?.data?.message || "Registration failed");
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="container mt-5" style={{ maxWidth: "480px" }}>
      <h2 className="mb-4">Register</h2>

      {apiError && <div className="alert alert-danger">{apiError}</div>}

      <form onSubmit={handleSubmit} noValidate>
        {[
          { label: "Username", name: "username", type: "text" },
          { label: "Password", name: "password", type: "password" },
          { label: "Confirm Password", name: "confirmPassword", type: "password" },
          { label: "First Name", name: "firstName", type: "text" },
          { label: "Last Name", name: "lastName", type: "text" },
          { label: "Email Address", name: "emailAddress", type: "email" },
          { label: "Phone Number", name: "phoneNumber", type: "text" },
        ].map(({ label, name, type }) => (
          <div className="mb-3" key={name}>
            <label htmlFor={name} className="form-label">{label}</label>
            <input
              id={name}
              name={name}
              type={type}
              className={`form-control ${errors[name] ? "is-invalid" : ""}`}
              value={form[name]}
              onChange={handleChange}
              required
              placeholder={`Enter ${label.toLowerCase()}`}
            />
            {errors[name] && <div className="invalid-feedback">{errors[name]}</div>}
          </div>
        ))}

        <button type="submit" className="btn btn-primary w-100" disabled={loading}>
          {loading ? "Registering..." : "Register"}
        </button>
      </form>
    </div>
  );
};

export default Register;
