import React, { useEffect, useState } from 'react';
import { useNavigate, useParams, Link } from 'react-router-dom';
import apiClient from '../../../services/ApiClient';
import SelectionModal from '../../components/SelectionModal';

const ServiceRecordUpdatePage = () => {
  const { id } = useParams();
  const navigate = useNavigate();

  const [formData, setFormData] = useState({
    id: id,
    serviceCarId: '',
    employeeId: '',
    serviceDate: '',
    serviceDescription: '',
    serviceCost: ''
  });

  const [car, setCar] = useState(null);
  const [employee, setEmployee] = useState(null);

  const [showCarModal, setShowCarModal] = useState(false);
  const [showEmployeeModal, setShowEmployeeModal] = useState(false);

  const [cars, setCars] = useState([]);
  const [employees, setEmployees] = useState([]);

  useEffect(() => {
    const fetchRecord = async () => {
      try {
        const { data: record } = await apiClient.get(`/service-records/${id}`);
        console.log(record)
        setFormData({
          id: record.id,
          serviceCarId: record.serviceCarId,
          employeeId: record.employeeId,
          serviceDate: record.serviceDate,
          serviceDescription: record.serviceDescription,
          serviceCost: record.serviceCost
        });

        const [carRes, employeeRes] = await Promise.all([
          apiClient.get(`/service-cars/my/${record.serviceCarId}`),
          apiClient.get(`/employees/${record.employeeId}`)
        ]);
        console.log(carRes.data)
        console.log(employeeRes.data)

        setCar(carRes.data);
        setEmployee(employeeRes.data);
      } catch (err) {
        console.error('Failed to load service record', err);
        alert('Error loading record.');
        navigate('/service-records');
      }
    };

    fetchRecord();
  }, [id, navigate]);

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setFormData(prev => ({ ...prev, [name]: value }));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
        console.log(formData)
      await apiClient.post(`/service-records/update/${id}`, {
        ...formData
      });
      navigate('/service-records');
    } catch (err) {
      console.error('Error updating service record:', err);
      alert('Update failed.');
    }
  };

  const openCarModal = async () => {
    const { data } = await apiClient.get('/service-cars/my');
    setCars(data);
    setShowCarModal(true);
  };

  const openEmployeeModal = async () => {
    const { data } = await apiClient.get('/employees');
    setEmployees(data);
    setShowEmployeeModal(true);
  };

  const handleCarSelect = (selectedCar) => {
    setCar(selectedCar);
    setFormData(prev => ({ ...prev, serviceCarId: selectedCar.id }));
    setShowCarModal(false);
  };

  const handleEmployeeSelect = (selectedEmployee) => {
    setEmployee(selectedEmployee);
    setFormData(prev => ({ ...prev, employeeId: selectedEmployee.id }));
    setShowEmployeeModal(false);
  };

  return (
    <div className="container mt-4">
      <h2>Update Service Record</h2>
      <form onSubmit={handleSubmit}>
        <div className="mb-3">
          <label>Service Date</label>
          <input
            type="date"
            name="serviceDate"
            value={formData.serviceDate}
            onChange={handleInputChange}
            className="form-control"
            required
          />
        </div>

        <div className="mb-3">
          <label>Description</label>
          <textarea
            name="serviceDescription"
            value={formData.serviceDescription}
            onChange={handleInputChange}
            className="form-control"
            required
          />
        </div>

        <div className="mb-3">
          <label>Cost</label>
          <input
            type="number"
            name="serviceCost"
            value={formData.serviceCost}
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
                ? `${car.make} ${car.model} (${car.licensePlate})`
                : 'No car selected'}
            </span>
            <button type="button" className="btn btn-outline-secondary btn-sm" onClick={openCarModal}>
              Select Car
            </button>
          </div>
        </div>

        <div className="mb-3">
          <label>Selected Employee</label>
          <div className="d-flex align-items-center justify-content-between">
            <span>
              {employee
                ? `${employee.firstName} ${employee.lastName}`
                : 'No employee selected'}
            </span>
            <button type="button" className="btn btn-outline-secondary btn-sm" onClick={openEmployeeModal}>
              Select Employee
            </button>
          </div>
        </div>

        <button type="submit" className="btn btn-success">Update Record</button>
        <Link to="/services" className="btn btn-outline-secondary ms-2">Cancel</Link>
      </form>

      {showCarModal && (
        <SelectionModal
          title="Select Car"
          records={cars}
          onSelect={handleCarSelect}
          onClose={() => setShowCarModal(false)}
          renderItem={(car) => `${car.id} | ${car.make} ${car.model} | ${car.licensePlate || 'No Plate'}`}
        />
      )}

      {showEmployeeModal && (
        <SelectionModal
          title="Select Employee"
          records={employees}
          onSelect={handleEmployeeSelect}
          onClose={() => setShowEmployeeModal(false)}
          renderItem={(e) => `${e.id} | ${e.firstName} ${e.lastName}`}
        />
      )}
    </div>
  );
};

export default ServiceRecordUpdatePage;
