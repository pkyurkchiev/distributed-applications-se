import React, { useState, useEffect } from 'react';
import apiClient from '../../../services/ApiClient';
import { useNavigate } from 'react-router-dom';
import SelectionModal from '../../components/SelectionModal';

const SaleCreatePage = () => {
  const [formData, setFormData] = useState({
    saleDate: '',
    finalPrice: '',
    customerId: null,
    carId: null,
  });

  const [customers, setCustomers] = useState([]);
  const [cars, setCars] = useState([]);
  const [showCustomerModal, setShowCustomerModal] = useState(false);
  const [showCarModal, setShowCarModal] = useState(false);

  const navigate = useNavigate();

  useEffect(() => {
    if (showCustomerModal && customers.length === 0) {
      apiClient.get('/customers')
        .then(res => setCustomers(res.data))
        .catch(() => alert('Failed to fetch customers.'));
    }

    if (showCarModal && cars.length === 0) {
      apiClient.get('/cars/in-stock')
        .then(res => setCars(res.data))
        .catch(() => alert('Failed to fetch cars.'));
    }
  }, [showCustomerModal, showCarModal]);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData(prev => ({ ...prev, [name]: value }));
  };

  const handleSubmit = async (e) => {
  e.preventDefault();

  if (!formData.customerId || !formData.carId) {
    alert('Please select both a customer and a car.');
    return;
  }

  if (!formData.finalPrice || parseFloat(formData.finalPrice) <= 0) {
    alert('Please enter a valid sale price.');
    return;
  }

  try {
    await apiClient.post('/sales/create', {
      ...formData,
    });
    navigate('/sales');
  } catch (err) {
    console.error('Error creating sale:', err);
    alert('Failed to create sale. Check inputs.');
  }
};

  const handleCustomerSelect = (customer) => {
    setFormData(prev => ({ ...prev, customerId: customer.id }));
    setShowCustomerModal(false);
  };

  const handleCarSelect = (car) => {
    setFormData(prev => ({ ...prev, carId: car.id }));
    setShowCarModal(false);
  };

  return (
    <div className="container mt-4">
      <h2>Create Sale</h2>
      <form onSubmit={handleSubmit}>
        <div className="mb-3">
          <label>Sale Date</label>
          <input
            name="saleDate"
            type="date"
            value={formData.saleDate}
            onChange={handleChange}
            className="form-control"
            required
          />
        </div>
        <div className="mb-3">
          <label>Sale Price</label>
          <input
            name="finalPrice"
            type="number"
            value={formData.finalPrice}
            onChange={handleChange}
            className="form-control"
            required
          />
        </div>

        {/* Customer Selection */}
        <div className="mb-3">
          <label>Customer</label>
          <div className="d-flex align-items-center gap-3">
            <button
              type="button"
              className="btn btn-outline-primary"
              onClick={() => setShowCustomerModal(true)}
            >
              {formData.customerId ? 'Change Customer' : 'Select Customer'}
            </button>
            {formData.customerId && (
              <span>Selected ID: {formData.customerId}</span>
            )}
          </div>
        </div>

        {/* Car Selection */}
        <div className="mb-3">
          <label>Car</label>
          <div className="d-flex align-items-center gap-3">
            <button
              type="button"
              className="btn btn-outline-primary"
              onClick={() => setShowCarModal(true)}
            >
              {formData.carId ? 'Change Car' : 'Select Car'}
            </button>
            {formData.carId && (
              <span>Selected ID: {formData.carId}</span>
            )}
          </div>
        </div>

        <button type="submit" className="btn btn-success">Create Sale</button>
      </form>

      {showCustomerModal && (
        <SelectionModal
          title="Select Customer"
          records={customers}
          onSelect={handleCustomerSelect}
          onClose={() => setShowCustomerModal(false)}
          renderItem={(c) => `${c.id} | ${c.firstName} ${c.lastName} | ${c.email}`}
        />
      )}

      {showCarModal && (
        <SelectionModal
          title="Select Car"
          records={cars}
          onSelect={handleCarSelect}
          onClose={() => setShowCarModal(false)}
          renderItem={(car) =>
            `${car.id} | ${car.make} ${car.model} | ${car.licensePlate || 'No Plate'}`
          }
        />
      )}
    </div>
  );
};

export default SaleCreatePage;
