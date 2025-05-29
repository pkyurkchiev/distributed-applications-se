import React from 'react';

const ServiceCarFilters = ({ filters, onFilterChange, onApplyFilters }) => {
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
      <h5 className="mb-3">Filter Service Cars</h5>

      <div className="row">
        <div className="col-md-3 mb-3">
          <label className="form-label">Make</label>
          <input
            type="text"
            name="make"
            className="form-control"
            value={filters.make || ''}
            onChange={handleInputChange}
          />
        </div>

        <div className="col-md-3 mb-3">
          <label className="form-label">Model</label>
          <input
            type="text"
            name="model"
            className="form-control"
            value={filters.model || ''}
            onChange={handleInputChange}
          />
        </div>

        <div className="col-md-2 mb-3">
          <label className="form-label">Manufacture Year</label>
          <input
            type="number"
            name="manufactureYear"
            className="form-control"
            value={filters.manufactureYear || ''}
            onChange={handleInputChange}
          />
        </div>

        <div className="col-md-2 mb-3">
          <label className="form-label">License Plate</label>
          <input
            type="text"
            name="licensePlate"
            className="form-control"
            value={filters.licensePlate || ''}
            onChange={handleInputChange}
          />
        </div>

        <div className="col-md-2 mb-3">
          <label className="form-label">Customer ID</label>
          <input
            type="number"
            name="customerId"
            className="form-control"
            value={filters.customerId || ''}
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

export default ServiceCarFilters;
