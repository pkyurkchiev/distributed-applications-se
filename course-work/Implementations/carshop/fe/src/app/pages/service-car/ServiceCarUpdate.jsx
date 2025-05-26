import React, { useEffect, useState } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import apiClient from '../../../services/ApiClient';

const ServiceCarUpdatePage = () => {
  const { id } = useParams();
  const navigate = useNavigate();

  const [formData, setFormData] = useState({
    make: '',
    model: '',
    manufactureYear: '',
    licensePlate: '',
    customerId: ''
  });

  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    const fetchServiceCar = async () => {
      try {
        const response = await apiClient.get(`/service-cars/my/${id}`);
        const car = response.data;
        setFormData({
          make: car.make || '',
          model: car.model || '',
          manufactureYear: car.manufactureYear || '',
          licensePlate: car.licensePlate || '',
          customerId: car.customerId || ''
        });
      } catch (err) {
        setError('Failed to fetch service car details');
      } finally {
        setLoading(false);
      }
    };

    fetchServiceCar();
  }, [id]);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData(prev => ({
      ...prev,
      [name]: value
    }));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      await apiClient.post(`/service-cars/my/update/${id}`, {
        id,
        ...formData
      });

      navigate(`/service-cars/my/${id}`);
    } catch (err) {
      console.error('Error updating service car:', err);
      alert('Something went wrong. Check your inputs.');
    }
  };

  if (loading) return <div>Loading...</div>;
  if (error) return <div className="alert alert-danger">{error}</div>;

  return (
    <div className="container mt-4">
      <h2>Update Service Car</h2>
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

        <button type="submit" className="btn btn-success">
          Update Service Car
        </button>
      </form>

      <button
        className="btn btn-primary mt-3"
        onClick={() => navigate(`/service-cars/my/${id}`)}
      >
        Cancel
      </button>
    </div>
  );
};

export default ServiceCarUpdatePage;
