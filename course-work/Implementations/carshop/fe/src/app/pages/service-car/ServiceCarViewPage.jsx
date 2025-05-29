import React, { useEffect, useState } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import apiClient from '../../../services/ApiClient';
import { useAuth } from '../../../auth/AuthProvider';

const ServiceCarViewPage = () => {
  const { id } = useParams();
  const navigate = useNavigate();
  const { user, loading: authLoading } = useAuth();

  const [serviceCar, setServiceCar] = useState(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    const fetchServiceCar = async () => {
      try {
        const response = await apiClient.get(`/service-cars/my/${id}`);
        setServiceCar(response.data);
      } catch (err) {
        setError('Failed to fetch service car details');
      } finally {
        setLoading(false);
      }
    };

    fetchServiceCar();
  }, [id]);

  const deleteCar = async () => {
      if (window.confirm('Are you sure you want to delete this car?')) {
        try {
          await apiClient.post(`/service-cars/my/delete/${serviceCar.id}`);
          navigate('/service-cars/my');
        } catch (err) {
          console.error('Failed to delete sale', err);
          alert('Error deleting sale.');
        }
      }
    }

  if (loading) return <div>Loading service car details...</div>;
  if (authLoading) return <div>Loading user info...</div>;
  if (error) return <div className="alert alert-danger">{error}</div>;
  if (!serviceCar) return <div>No service car found</div>;

  return (
    <div className="container mt-4">
      <h2>Service Car Details</h2>
      <div><strong>Make:</strong> {serviceCar.make}</div>
      <div><strong>Model:</strong> {serviceCar.model}</div>
      <div><strong>Manufacture Year:</strong> {serviceCar.manufactureYear}</div>
      <div><strong>License Plate:</strong> {serviceCar.licensePlate}</div>

      {user?.role?.includes('CUSTOMER') && (
        <button
          className="btn btn-primary mt-3"
          onClick={() => navigate(`/service-cars/my/update/${serviceCar.id}`)}
        >
          Edit Service Car
        </button>
      )}
      {user?.role?.includes('CUSTOMER') ? (
        <button
          className="btn btn-primary mt-3"
          onClick={deleteCar}
        >
          Delete Car
        </button>
      ) : null}

      <button
        className="btn btn-secondary mt-3 ms-2"
        onClick={() => navigate(`/service-cars/my`)}
      >
        Back to Listing
      </button>
    </div>
  );
};

export default ServiceCarViewPage;
