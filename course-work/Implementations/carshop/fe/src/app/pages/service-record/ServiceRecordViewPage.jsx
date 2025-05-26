import React, { useEffect, useState } from 'react';
import { useParams, Link, useNavigate } from 'react-router-dom';
import apiClient from '../../../services/ApiClient';
import { useAuth } from '../../../auth/AuthProvider';

const ServiceRecordViewPage = () => {
  const { id } = useParams();
  const [record, setRecord] = useState(null);
  const [car, setCar] = useState(null);
  const [employee, setEmployee] = useState(null);
  const [loading, setLoading] = useState(true);

  const { user, loading: authLoading } = useAuth();
  const navigate = useNavigate();

  useEffect(() => {
    const fetchServiceRecord = async () => {
      try {
        console.log(id)
        const { data: serviceData } = await apiClient.get(`/service-records/${id}`);
        setRecord(serviceData);


        const [carRes, employeeRes] = await Promise.all([
          apiClient.get(`/service-cars/my/${serviceData.serviceCarId}`),
          apiClient.get(`/employees/${serviceData.employeeId}`)
        ]);

        setCar(carRes.data);
        setEmployee(employeeRes.data);
      } catch (err) {
        console.error('Error fetching service record:', err);
        alert('Failed to load service information.');
      } finally {
        setLoading(false);
      }
    };

    fetchServiceRecord();
  }, [id]);

  const deleteRecord = async () => {
    if (window.confirm('Are you sure you want to delete this service record?')) {
      try {
        await apiClient.post(`/service-records/delete/${record.id}`);
        navigate('/service-records');
      } catch (err) {
        console.error('Failed to delete service record:', err);
        alert('Error deleting service record.');
      }
    }
  };

  if (loading) return <div className="container mt-4">Loading...</div>;
  if (!record) return <div className="container mt-4">Service record not found.</div>;

  return (
    <div className="container mt-4">
      <h2>Service Record Details</h2>

      <div className="card p-3 mb-4">
        <h5>Service Info</h5>
        <p><strong>ID:</strong> {record.id}</p>
        <p><strong>Date:</strong> {record.serviceDate}</p>
        <p><strong>Description:</strong> {record.serviceDescription}</p>
        <p><strong>Cost:</strong> ${parseFloat(record.serviceCost).toFixed(2)}</p>
      </div>

      {car && (
        <div className="card p-3 mb-4">
          <h5>Car Info</h5>
          <p><strong>ID:</strong> {car.id}</p>
          <p><strong>Make:</strong> {car.make}</p>
          <p><strong>Model:</strong> {car.model}</p>
          <p><strong>License Plate:</strong> {car.licensePlate || 'N/A'}</p>
          {user?.role?.includes('CUSTOMER') && (
            <Link to={`/service-cars/my/${car.id}`} className="btn btn-outline-secondary btn-sm">
              View Car
            </Link>
          )}
        </div>
      )}

      {employee && (
        <div className="card p-3 mb-4">
          <h5>Employee Info</h5>
          <p><strong>ID:</strong> {employee.id}</p>
          <p><strong>Name:</strong> {employee.firstName} {employee.lastName}</p>
          <p><strong>Email:</strong> {employee.email}</p>
        </div>
      )}
  
      {user?.role?.includes('EMPLOYEE') && (
        <>
          <Link to={`/service-records/update/${record.id}`} className="btn btn-primary me-2">Edit</Link> 
          <button className="btn btn-danger me-2" onClick={deleteRecord}>
            Delete Record
          </button>
        </>
      )}

      <Link to="/service-records" className="btn btn-secondary">Back to Services</Link>
    </div>
  );
};

export default ServiceRecordViewPage;
