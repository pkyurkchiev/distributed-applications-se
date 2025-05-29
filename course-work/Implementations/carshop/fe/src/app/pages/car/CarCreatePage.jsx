import React, { useState } from 'react';
import apiClient from '../../../services/ApiClient';
import { useNavigate } from 'react-router-dom';
import { useAuth } from '../../../auth/AuthProvider';

const CarCreatePage = () => {
  const [formData, setFormData] = useState({
    make: '',
    model: '',
    manufactureYear: '',
    price: '',
    isInStock: false,
  });
  const { user, loading: authLoading } = useAuth();
  const navigate = useNavigate();

  const handleChange = (e) => {
    const { name, value, type, checked } = e.target;
    setFormData(prev => ({
      ...prev,
      [name]: type === 'checkbox' ? checked : value
    }));
  };

  const handleImageUpload = (e) => {
    const files = Array.from(e.target.files);

    Promise.all(
      files.map(file =>
        new Promise((resolve, reject) => {
          const reader = new FileReader();
          reader.onload = () => resolve(reader.result.split(',')[1]);
          reader.onerror = reject;
          reader.readAsDataURL(file);
        })
      )
    ).then(base64Images => {
      setFormData(prev => ({
        ...prev,
        images: base64Images
      }));
    });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    try {
      await apiClient.post('/cars/create', {
        make: formData.make,
        model: formData.model,
        manufactureYear: formData.manufactureYear,
        price: formData.price,
        inStock: formData.isInStock,
        imagesBase64: formData.images
      });

      navigate('/cars');
    } catch (err) {
      console.error('Error creating car:', err);
      alert('Something went wrong. Check your inputs.');
    }
  };

  return (
    <div className="container mt-4">
      <h2>Create New Car</h2>
      <form onSubmit={handleSubmit}>
        <div className="mb-3">
          <label>Make</label>
          <input name="make" value={formData.make} onChange={handleChange} className="form-control" required />
        </div>
        <div className="mb-3">
          <label>Model</label>
          <input name="model" value={formData.model} onChange={handleChange} className="form-control" required />
        </div>
        <div className="mb-3">
          <label>Manufacture Year</label>
          <input name="manufactureYear" type="date" value={formData.manufactureYear} onChange={handleChange} className="form-control" required />
        </div>
        <div className="mb-3">
          <label>Price</label>
          <input name="price" type="number" value={formData.price} onChange={handleChange} className="form-control" required />
        </div>
        <div className="form-check mb-3">
          <input name="isInStock" type="checkbox" checked={formData.isInStock} onChange={handleChange} className="form-check-input" />
          <label className="form-check-label">In Stock</label>
        </div>
        <div className="mb-3">
          <label>Upload Images</label>
          <input type="file" multiple accept="image/*" onChange={handleImageUpload} className="form-control" />
        </div>
        <button type="submit" className="btn btn-success">Create Car</button>
      </form>
    </div>
  );
};

export default CarCreatePage;
