import React from 'react';

const ServiceRecordListingFilters = ({ filters, onFilterChange, onApplyFilters }) => {
  const handleInputChange = (e) => {
    const { name, value } = e.target;

    onFilterChange((prevFilters) => {
      const newFilters = { ...prevFilters };
      if (value === '') {
        delete newFilters[name];
      } else {
        newFilters[name] = value;
      }
      return newFilters;
    });
  };

  const handleApplyFilters = () => {
    onApplyFilters(prev => !prev);
  };

  return (
    <div className="card p-3 mb-4">
      <h5 className="mb-3">Filter Services</h5>
      <div className="row">
        <div className="col-md-3 mb-3">
          <label className="form-label">Car Make</label>
          <input
            type="text"
            name="carMake"
            className="form-control"
            value={filters.carMake || ''}
            onChange={handleInputChange}
          />
        </div>

        <div className="col-md-3 mb-3">
          <label className="form-label">Car Model</label>
          <input
            type="text"
            name="carModel"
            className="form-control"
            value={filters.carModel || ''}
            onChange={handleInputChange}
          />
        </div>

        <div className="col-md-3 mb-3">
          <label className="form-label">License Plate</label>
          <input
            type="text"
            name="licensePlate"
            className="form-control"
            value={filters.licensePlate || ''}
            onChange={handleInputChange}
          />
        </div>

        <div className="col-md-3 mb-3">
          <label className="form-label">Employee First Name</label>
          <input
            type="text"
            name="employeeFirstName"
            className="form-control"
            value={filters.employeeFirstName || ''}
            onChange={handleInputChange}
          />
        </div>

        <div className="col-md-3 mb-3">
          <label className="form-label">Employee Last Name</label>
          <input
            type="text"
            name="employeeLastName"
            className="form-control"
            value={filters.employeeLastName || ''}
            onChange={handleInputChange}
          />
        </div>

        <div className="col-md-3 mb-3">
          <label className="form-label">Service Date</label>
          <input
            type="date"
            name="serviceDate"
            className="form-control"
            value={filters.serviceDate || ''}
            onChange={handleInputChange}
          />
        </div>

        <div className="col-md-3 mb-3">
          <label className="form-label">Service Cost From</label>
          <input
            type="number"
            step="0.01"
            name="costFrom"
            className="form-control"
            value={filters.costFrom || ''}
            onChange={handleInputChange}
          />
        </div>

        <div className="col-md-3 mb-3">
          <label className="form-label">Service Cost To</label>
          <input
            type="number"
            step="0.01"
            name="costTo"
            className="form-control"
            value={filters.costTo || ''}
            onChange={handleInputChange}
          />
        </div>

        <div className="col-md-3 mb-3 d-flex align-items-end">
          <button className="btn btn-primary" onClick={handleApplyFilters}>
            Apply
          </button>
        </div>
      </div>
    </div>
  );
};

export default ServiceRecordListingFilters;
