import React, { useEffect, useState } from 'react';
import { useNavigate, useParams, Link } from 'react-router-dom';
import apiClient from '../../../services/ApiClient';
import SelectionModal from '../../components/SelectionModal';

const SaleUpdatePage = () => {
  const { id } = useParams();
  const navigate = useNavigate();

  const [formData, setFormData] = useState({
    id: id,
    carId: '',
    customerId: '',
    finalPrice: '',
    saleDate: ''
  });

  const [car, setCar] = useState(null);
  const [customer, setCustomer] = useState(null);

  const [showCarModal, setShowCarModal] = useState(false);
  const [showCustomerModal, setShowCustomerModal] = useState(false);

  const [cars, setCars] = useState([]);
  const [customers, setCustomers] = useState([]);

  useEffect(() => {
    const fetchSale = async () => {
      try {
        const { data: sale } = await apiClient.get(`/sales/${id}`);
        setFormData({
          id: sale.id,
          carId: sale.carId,
          customerId: sale.customerId,
          finalPrice: sale.finalPrice,
          saleDate: sale.saleDate
        });

        const [carRes, customerRes] = await Promise.all([
          apiClient.get(`/cars/${sale.carId}`),
          apiClient.get(`/customers/${sale.customerId}`)
        ]);

        setCar(carRes.data);
        setCustomer(customerRes.data);
      } catch (err) {
        console.error('Failed to load sale data', err);
        alert('Error loading sale data');
        navigate('/sales');
      }
    };

    fetchSale();
  }, [id, navigate]);

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setFormData(prev => ({ ...prev, [name]: value }));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    console.log(formData)
    console.log(id)

    try {
      await apiClient.post(`/sales/update/${id}`, {
        ...formData
      });
      navigate('/sales');
    } catch (err) {
      console.error('Error updating sale:', err);
      alert('Something went wrong while updating the sale.');
    }
  };

  const openCarModal = async () => {
    const { data } = await apiClient.get('/cars/in-stock');
    setCars(data);
    setShowCarModal(true);
  };

  const openCustomerModal = async () => {
    const { data } = await apiClient.get('/customers');
    setCustomers(data);
    setShowCustomerModal(true);
  };

  const handleCarSelect = (selectedCar) => {
    setCar(selectedCar);
    setFormData(prev => ({ ...prev, carId: selectedCar.id }));
    setShowCarModal(false);
  };

  const handleCustomerSelect = (selectedCustomer) => {
    setCustomer(selectedCustomer);
    setFormData(prev => ({ ...prev, customerId: selectedCustomer.id }));
    setShowCustomerModal(false);
  };

  return (
    <div className="container mt-4">
      <h2>Update Sale</h2>
      <form onSubmit={handleSubmit}>
        <div className="mb-3">
          <label>Sale Date</label>
          <input
            type="date"
            name="saleDate"
            value={formData.saleDate}
            onChange={handleInputChange}
            className="form-control"
            required
          />
        </div>

        <div className="mb-3">
          <label>Price</label>
          <input
            type="number"
            name="finalPrice"
            value={formData.finalPrice}
            onChange={handleInputChange}
            className="form-control"
            required
          />
        </div>

        <div className="mb-3">
          <label>Selected Car</label>
          <div className="d-flex align-items-center justify-content-between">
            <span>
              {car
                ? `${car.make} ${car.model} (${car.manufactureYear})`
                : 'No car selected'}
            </span>
            <button type="button" className="btn btn-outline-secondary btn-sm" onClick={openCarModal}>
              Select Car
            </button>
          </div>
        </div>

        <div className="mb-3">
          <label>Selected Customer</label>
          <div className="d-flex align-items-center justify-content-between">
            <span>
              {customer
                ? `${customer.firstName} ${customer.lastName} (${customer.email})`
                : 'No customer selected'}
            </span>
            <button type="button" className="btn btn-outline-secondary btn-sm" onClick={openCustomerModal}>
              Select Customer
            </button>
          </div>
        </div>

        <button type="submit" className="btn btn-success">Update Sale</button>
      </form>
      <Link to="/sales" className="btn btn-outline-secondary">Cancel</Link>

      {showCarModal && (
        <SelectionModal
          title="Select a Car"
          records={cars}
          onSelect={handleCarSelect}
          onClose={() => setShowCarModal(false)}
        />
      )}

      {showCustomerModal && (
        <SelectionModal
          title="Select a Customer"
          records={customers}
          onSelect={handleCustomerSelect}
          onClose={() => setShowCustomerModal(false)}
        />
      )}
    </div>
  );
};

export default SaleUpdatePage;
