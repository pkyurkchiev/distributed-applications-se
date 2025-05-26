import React, { useState, useEffect } from 'react';
import apiClient from '../../../services/ApiClient';
import { useNavigate } from 'react-router-dom';
import SelectionModal from '../../components/SelectionModal';

const ServiceRecordCreatePage = () => {
  const [formData, setFormData] = useState({
    serviceDate: '',
    serviceDescription: '',
    serviceCost: '',
    serviceCarId: null,
    employeeId: null,
  });

  const [serviceCars, setServiceCars] = useState([]);
  const [employees, setEmployees] = useState([]);
  const [showCarModal, setShowCarModal] = useState(false);
  const [showEmployeeModal, setShowEmployeeModal] = useState(false);

  const navigate = useNavigate();

  useEffect(() => {
    if (showCarModal && serviceCars.length === 0) {
      apiClient.get('/service-cars/my')
        .then(res => setServiceCars(res.data))
        .catch(() => alert('Failed to fetch cars.'));
    }

    if (showEmployeeModal && employees.length === 0) {
      apiClient.get('/employees')
        .then(res => setEmployees(res.data))
        .catch(() => alert('Failed to fetch employees.'));
    }
  }, [showCarModal, showEmployeeModal]);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData(prev => ({ ...prev, [name]: value }));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    
    if (!formData.serviceCarId || !formData.employeeId) {
      alert('Please select both a car and an employee.');
      return;
    }

    if (!formData.serviceCost || parseFloat(formData.serviceCost) <= 0) {
      alert('Please enter a valid service cost.');
      return;
    }

    try {
        console.log(formData)
      await apiClient.post('/service-records/create', formData);
      navigate('/service-records');
    } catch (err) {
      console.error('Error creating service record:', err);
      alert('Failed to create service record. Check inputs.');
    }
  };

  const handleCarSelect = (car) => {
    setFormData(prev => ({ ...prev, serviceCarId: car.id }));
    setShowCarModal(false);
  };

  const handleEmployeeSelect = (employee) => {
    setFormData(prev => ({ ...prev, employeeId: employee.id }));
    setShowEmployeeModal(false);
  };

  return (
    <div className="container mt-4">
      <h2>Create Service Record</h2>
      <form onSubmit={handleSubmit}>
        <div className="mb-3">
          <label>Service Date</label>
          <input
            name="serviceDate"
            type="date"
            value={formData.serviceDate}
            onChange={handleChange}
            className="form-control"
            required
          />
        </div>

        <div className="mb-3">
          <label>Service Description</label>
          <textarea
            name="serviceDescription"
            value={formData.serviceDescription}
            onChange={handleChange}
            className="form-control"
            rows="3"
            required
          />
        </div>

        <div className="mb-3">
          <label>Service Cost</label>
          <input
            name="serviceCost"
            type="number"
            step="0.01"
            value={formData.serviceCost}
            onChange={handleChange}
            className="form-control"
            required
          />
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
              {formData.serviceCarId ? 'Change Car' : 'Select Car'}
            </button>
            {formData.serviceCarId && <span>Selected ID: {formData.serviceCarId}</span>}
          </div>
        </div>

        {/* Employee Selection */}
        <div className="mb-3">
          <label>Employee</label>
          <div className="d-flex align-items-center gap-3">
            <button
              type="button"
              className="btn btn-outline-primary"
              onClick={() => setShowEmployeeModal(true)}
            >
              {formData.employeeId ? 'Change Employee' : 'Select Employee'}
            </button>
            {formData.employeeId && <span>Selected ID: {formData.employeeId}</span>}
          </div>
        </div>

        <button type="submit" className="btn btn-success">Create Service Record</button>
      </form>

      {showCarModal && (
        <SelectionModal
          title="Select Car"
          records={serviceCars}
          onSelect={handleCarSelect}
          onClose={() => setShowCarModal(false)}
          renderItem={(car) =>
            `${car.id} | ${car.make} ${car.model} | ${car.licensePlate || 'No Plate'}`
          }
        />
      )}

      {showEmployeeModal && (
        <SelectionModal
          title="Select Employee"
          records={employees}
          onSelect={handleEmployeeSelect}
          onClose={() => setShowEmployeeModal(false)}
          renderItem={(emp) =>
            `${emp.id} | ${emp.firstName} ${emp.lastName} | ${emp.email}`
          }
        />
      )}
    </div>
  );
};

export default ServiceRecordCreatePage;
