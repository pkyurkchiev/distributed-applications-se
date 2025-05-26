import React from 'react';

const SaleListingFilters = ({ filters, onFilterChange, onApplyFilters }) => {
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
      <h5 className="mb-3">Filter Sales</h5>
      <div className="row">
        <div className="col-md-3 mb-3">
          <label className="form-label">Customer First Name</label>
          <input
            type="text"
            name="customerFirstName"
            className="form-control"
            value={filters.customerFirstName || ''}
            onChange={handleInputChange}
          />
        </div>

        <div className="col-md-3 mb-3">
          <label className="form-label">Customer Last Name</label>
          <input
            type="text"
            name="customerLastName"
            className="form-control"
            value={filters.customerLastName || ''}
            onChange={handleInputChange}
          />
        </div>

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
          <label className="form-label">Manufacture Year</label>
          <input
            type="number"
            name="manufactureYear"
            className="form-control"
            value={filters.manufactureYear || ''}
            onChange={handleInputChange}
          />
        </div>

        <div className="col-md-3 mb-3">
          <label className="form-label">Sale Date</label>
          <input
            type="date"
            name="saleDate"
            className="form-control"
            value={filters.saleDate || ''}
            onChange={handleInputChange}
          />
        </div>

        <div className="col-md-3 mb-3">
          <label className="form-label">Final Price From</label>
          <input
            type="number"
            step="0.01"
            name="priceFrom"
            className="form-control"
            value={filters.priceFrom || ''}
            onChange={handleInputChange}
          />
        </div>

        <div className="col-md-3 mb-3">
          <label className="form-label">Final Price To</label>
          <input
            type="number"
            step="0.01"
            name="priceTo"
            className="form-control"
            value={filters.priceTo || ''}
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

export default SaleListingFilters;
