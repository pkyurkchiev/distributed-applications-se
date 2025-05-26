import React from 'react';

const ServiceRecordListingSort = ({ sortOptions, onSortChange }) => {
  const handleSortChange = (e) => {
    const { name, value } = e.target;
    onSortChange(prev => ({
      ...prev,
      [name]: name === 'asc' ? value === 'true' : value
    }));
  };

  return (
    <div className="d-flex gap-3 mb-3">
      <div>
        <label htmlFor="sortBy" className="form-label">Sort by:</label>
        <select
          name="sortBy"
          id="sortBy"
          className="form-select"
          onChange={handleSortChange}
          value={sortOptions.sortBy}
        >
          <option value="id">Default</option>
          <option value="carMake">Car Make</option>
          <option value="carModel">Car Model</option>
          <option value="licensePlate">License Plate</option>
          <option value="employeeFirstName">Employee First Name</option>
          <option value="employeeLastName">Employee Last Name</option>
          <option value="serviceDate">Service Date</option>
          <option value="serviceCost">Service Cost</option>
        </select>
      </div>

      <div>
        <label htmlFor="asc" className="form-label">Order:</label>
        <select
          name="asc"
          id="asc"
          className="form-select"
          onChange={handleSortChange}
          value={sortOptions.asc.toString()}
        >
          <option value="true">Ascending</option>
          <option value="false">Descending</option>
        </select>
      </div>
    </div>
  );
};

export default ServiceRecordListingSort;
