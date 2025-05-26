import React, { useEffect, useState } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import apiClient from '../../../services/ApiClient';

const CarUpdatePage = () => {
  const { id } = useParams();
  const navigate = useNavigate();

  const [formData, setFormData] = useState({
    make: '',
    model: '',
    manufactureYear: '',
    price: '',
    isInStock: false,
    imagesBase64: [],  // existing images as base64 strings
    newImagesBase64: [] // newly added images (base64)
  });
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);


  useEffect(() => {
    const fetchCar = async () => {
      try {
        const response = await apiClient.get(`/cars/${id}`);
        const car = response.data;
        setFormData({
          make: car.make || '',
          model: car.model || '',
          manufactureYear: car.manufactureYear || '',
          price: car.price || '',
          isInStock: car.inStock || false,
          imagesBase64: car.imagesBase64 || [],
          newImagesBase64: []
        });
      } catch (err) {
        setError('Failed to fetch car details');
      } finally {
        setLoading(false);
      }
    };

    fetchCar();
  }, [id]);

  // Handle input change
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
    ).then(newBase64Images => {
      setFormData(prev => ({
        ...prev,
        newImagesBase64: [...prev.newImagesBase64, ...newBase64Images]
      }));
    });
  };

  const removeExistingImage = (index) => {
    setFormData(prev => {
      const updatedImages = [...prev.imagesBase64];
      updatedImages.splice(index, 1);
      return { ...prev, imagesBase64: updatedImages };
    });
  };

  const removeNewImage = (index) => {
    setFormData(prev => {
      const updatedNewImages = [...prev.newImagesBase64];
      updatedNewImages.splice(index, 1);
      return { ...prev, newImagesBase64: updatedNewImages };
    });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    try {
      const combinedImagesBase64 = [...formData.imagesBase64, ...formData.newImagesBase64];

      console.log(formData)

      await apiClient.post(`/cars/update/${id}`, {
        id: id,
        make: formData.make,
        model: formData.model,
        manufactureYear: formData.manufactureYear,
        price: formData.price,
        inStock: formData.isInStock,
        imagesBase64: combinedImagesBase64,
      });

      navigate(`/cars/${id}`);
    } catch (err) {
      console.error('Error updating car:', err);
      alert('Something went wrong. Check your inputs.');
    }
  };

  if (loading) return <div>Loading...</div>;
  if (error) return <div className="alert alert-danger">{error}</div>;

  return (
    <div className="container mt-4">
      <h2>Update Car</h2>
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
          <label>Price</label>
          <input
            name="price"
            type="number"
            value={formData.price}
            onChange={handleChange}
            className="form-control"
            required
          />
        </div>

        <div className="form-check mb-3">
          <input
            name="isInStock"
            type="checkbox"
            checked={formData.isInStock}
            onChange={handleChange}
            className="form-check-input"
          />
          <label className="form-check-label">In Stock</label>
        </div>

        <div className="mb-3">
          <label>Existing Images</label>
          <div className="d-flex flex-wrap mb-2">
            {formData.imagesBase64.length === 0 && <p>No images</p>}
            {formData.imagesBase64.map((img, index) => (
              <div key={index} style={{ position: 'relative', marginRight: 10, marginBottom: 10 }}>
                <img
                  src={`data:image/jpeg;base64,${img}`}
                  alt={`Existing ${index}`}
                  style={{ width: 150, height: 100, objectFit: 'cover' }}
                />
                <button
                  type="button"
                  onClick={() => removeExistingImage(index)}
                  style={{
                    position: 'absolute',
                    top: 0,
                    right: 0,
                    backgroundColor: 'rgba(255, 0, 0, 0.7)',
                    border: 'none',
                    color: 'white',
                    cursor: 'pointer',
                    padding: '2px 6px',
                    borderRadius: '50%',
                    fontWeight: 'bold',
                  }}
                  aria-label="Remove image"
                >
                  ×
                </button>
              </div>
            ))}
          </div>

          <label>Add New Images</label>
          <input
            type="file"
            multiple
            accept="image/*"
            onChange={handleImageUpload}
            className="form-control"
          />

          <div className="d-flex flex-wrap mt-2">
            {formData.newImagesBase64.map((img, index) => (
              <div key={index} style={{ position: 'relative', marginRight: 10, marginBottom: 10 }}>
                <img
                  src={`data:image/jpeg;base64,${img}`}
                  alt={`New ${index}`}
                  style={{ width: 150, height: 100, objectFit: 'cover' }}
                />
                <button
                  type="button"
                  onClick={() => removeNewImage(index)}
                  style={{
                    position: 'absolute',
                    top: 0,
                    right: 0,
                    backgroundColor: 'rgba(255, 0, 0, 0.7)',
                    border: 'none',
                    color: 'white',
                    cursor: 'pointer',
                    padding: '2px 6px',
                    borderRadius: '50%',
                    fontWeight: 'bold',
                  }}
                  aria-label="Remove image"
                >
                  ×
                </button>
              </div>
            ))}
          </div>
        </div>

        <button type="submit" className="btn btn-success">
          Update Car
        </button>
      </form>
      <button
          className="btn btn-primary mt-3"
          onClick={() => navigate(`/cars/${id}`)}
        >
         Cancel
        </button>
    </div>
  );
};

export default CarUpdatePage;
