import React, { useEffect, useState } from 'react';
import { useParams, Link, useNavigate } from 'react-router-dom';
import apiClient from '../../../services/ApiClient';
import { useAuth } from '../../../auth/AuthProvider';

const SaleViewPage = () => {
  const { id } = useParams();
  const [sale, setSale] = useState(null);
  const [customer, setCustomer] = useState(null);
  const [car, setCar] = useState(null);
  const [loading, setLoading] = useState(true);

  const { user, loading: authLoading } = useAuth();

  const navigate = useNavigate();

  useEffect(() => {
    const fetchSale = async () => {
      try {
        console.log(id)
        const { data: saleData } = await apiClient.get(`/sales/${id}`);
        setSale(saleData);
        console.log(saleData)

        const [customerRes, carRes] = await Promise.all([
          apiClient.get(`/customers/${saleData.customerId}`),
          apiClient.get(`/cars/${saleData.carId}`)
        ]);

        setCustomer(customerRes.data);
        setCar(carRes.data);
      } catch (err) {
        console.error('Error fetching sale details:', err);
        alert('Failed to load sale information.');
      } finally {
        setLoading(false);
      }
    };

    fetchSale();
  }, [id]);

  const deleteSale = async () => {
      if (window.confirm('Are you sure you want to delete this sale?')) {
        try {
          await apiClient.post(`/sales/delete/${sale.id}`);
          navigate('/sales');
        } catch (err) {
          console.error('Failed to delete sale', err);
          alert('Error deleting sale.');
        }
      }
    }

  if (loading) return <div className="container mt-4">Loading...</div>;
  if (!sale) return <div className="container mt-4">Sale not found.</div>;

  return (
    <div className="container mt-4">
      <h2>Sale Details</h2>
      <div className="card p-3 mb-4">
        <h5>Sale Info</h5>
        <p><strong>ID:</strong> {sale.id}</p>
        <p><strong>Date:</strong> {sale.saleDate}</p>
        <p><strong>Price:</strong> ${parseFloat(sale.finalPrice).toFixed(2)}</p>
      </div>

      {customer && (
        <div className="card p-3 mb-4">
          <h5>Customer Info</h5>
          <p><strong>ID:</strong> {customer.id}</p>
          <p><strong>Name:</strong> {customer.firstName} {customer.lastName}</p>
          <p><strong>Email:</strong> {customer.email}</p>
          <p><strong>Phone:</strong> {customer.phone}</p>
        </div>
      )}

      {car && (
        <div className="card p-3 mb-4">
          <h5>Car Info</h5>
          <p><strong>ID:</strong> {car.id}</p>
          <p><strong>Make:</strong> {car.make}</p>
          <p><strong>Model:</strong> {car.model}</p>
          <p><strong>Year:</strong> {car.manufactureYear}</p>
          <p><strong>License Plate:</strong> {car.licensePlate || 'N/A'}</p>
          <Link to={`/cars/${car.id}`} className="btn btn-outline-secondary btn-sm">
            View Car
          </Link>
        </div>
      )}

      <Link to={`/sales/update/${sale.id}`} className="btn btn-primary">Edit</Link>    
      {user?.role?.includes('EMPLOYEE') && (
        <button
          className="btn btn-primary"
          onClick={deleteSale}
        >
          Delete Sale
        </button>
      )}
      <Link to="/sales" className="btn btn-primary">Back to Sales</Link>

    </div>
  );
};

export default SaleViewPage;
