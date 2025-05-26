import React, { useState } from 'react';
import apiClient from '../../../services/ApiClient';
import { useNavigate } from 'react-router-dom';
import { useAuth } from '../../../auth/AuthProvider';

const ServiceCarCreatePage = () => {

  const { user } = useAuth();
  const [formData, setFormData] = useState({
    make: '',
    model: '',
    manufactureYear: '',
    licensePlate: '',
  });

  const navigate = useNavigate();

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData(prev => ({
      ...prev,
      [name]: value
    }));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
console.log(user)
    try {
      await apiClient.post(`/service-cars/my/create/${user?.id}`, {
        make: formData.make,
        model: formData.model,
        manufactureYear: formData.manufactureYear,
        licensePlate: formData.licensePlate,
        customerId: Number(formData.customerId)
      });

      navigate('/service-cars/my');
    } catch (err) {
      console.error('Error creating service car:', err);
      alert('Something went wrong. Please check your inputs.');
    }
  };

  return (
    <div className="container mt-4">
      <h2>Create Service Car</h2>
      <form onSubmit={handleSubmit}>
        <div className="mb-3">
          <label>Make</label>
          <input
            name="make"
            value={formData.make}
            onChange={handleChange}
            className="form-control"
            required
          />
        </div>

        <div className="mb-3">
          <label>Model</label>
          <input
            name="model"
            value={formData.model}
            onChange={handleChange}
            className="form-control"
            required
          />
        </div>

        <div className="mb-3">
          <label>Manufacture Year</label>
          <input
            name="manufactureYear"
            type="date"
            value={formData.manufactureYear}
            onChange={handleChange}
            className="form-control"
            required
          />
        </div>

        <div className="mb-3">
          <label>License Plate</label>
          <input
            name="licensePlate"
            value={formData.licensePlate}
            onChange={handleChange}
            className="form-control"
            required
          />
        </div>

        <button type="submit" className="btn btn-success">Create Service Car</button>
      </form>
    </div>
  );
};

export default ServiceCarCreatePage;
