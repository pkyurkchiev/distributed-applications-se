import React, { useEffect, useState } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import apiClient from '../../../services/ApiClient';
import { useAuth } from '../../../auth/AuthProvider';

const CarViewPage = () => {
  const { id } = useParams();
  const navigate = useNavigate();

  const { user, loading: authLoading } = useAuth();

  const [car, setCar] = useState(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    const fetchCar = async () => {
      try {
        const response = await apiClient.get(`/cars/${id}`);
        setCar(response.data);
        console.log(response.data)
      } catch (err) {
        setError('Failed to fetch car details');
      } finally {
        setLoading(false);
      }
    };

    fetchCar();
  }, [id]);

  const deleteCar = async () => {
      if (window.confirm('Are you sure you want to delete this car?')) {
        try {
          await apiClient.post(`/cars/delete/${car.id}`);
          navigate('/cars');
        } catch (err) {
          console.error('Failed to delete sale', err);
          alert('Error deleting sale.');
        }
      }
    }

  if (loading) return <div>Loading car details...</div>;
  if (authLoading) return <div>Loading user info...</div>;
  if (error) return <div className="alert alert-danger">{error}</div>;
  if (!car) return <div>No car found</div>;


  return (
    <div className="container mt-4">
      <h2>Car Details</h2>
      <div><strong>Make:</strong> {car.make}</div>
      <div><strong>Model:</strong> {car.model}</div>
      <div><strong>Manufacture Year:</strong> {car.manufactureYear}</div>
      <div><strong>Price:</strong> ${car.price}</div>
      <div><strong>In Stock:</strong> {car.isInStock ? 'Yes' : 'No'}</div>

      <div className="mt-3">
        <h4>Images</h4>
        <div className="d-flex flex-wrap">
          {car.imagesBase64 && car.imagesBase64.length > 0 ? (
            car.imagesBase64.map((img, key) => (
              <img
                key={key}
                src={`data:image/jpeg;base64,${img}`}
                alt={`${car.make} ${car.model}`}
                style={{
                  width: '150px',
                  height: '100px',
                  objectFit: 'cover',
                  marginRight: '10px',
                  marginBottom: '10px'
                }}
              />
            ))
          ) : (
            <p>No images available</p>
          )}
        </div>
      </div>

      {user?.role?.includes('EMPLOYEE') && (
        <button
          className="btn btn-primary mt-3"
          onClick={() => navigate(`/cars/update/${car.id}`)}
        >
          Edit Car
        </button>
      )}
      {user?.role?.includes('EMPLOYEE') && car?.inStock ? (
        <button
          className="btn btn-primary mt-3"
          onClick={deleteCar}
        >
          Delete Car
        </button>
      ) : null}
      <button
          className="btn btn-primary mt-3"
          onClick={() => navigate(`/cars`)}
        >
         Listing Page
        </button>
    </div>
  );
};

export default CarViewPage;
