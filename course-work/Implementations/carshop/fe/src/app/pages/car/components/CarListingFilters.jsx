import React from 'react';

const CarListingFilters = ({ filters, onFilterChange, onApplyFilters }) => {
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


  const handleCheckboxChange = (e) => {
    const { name, checked } = e.target;
    onFilterChange((prevFilters) => ({
      ...prevFilters,
      [name]: checked,
    }));
  };

  const handleApplyFilters = (e) => {
    onApplyFilters(prev => !prev)
  };

  return (
    <div className="card p-3 mb-4">
      <h5 className="mb-3">Filter Cars</h5>

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
          <label className="form-label">Year</label>
          <input
            type="number"
            name="manufactureYear"
            className="form-control"
            value={filters.manufactureYear || ''}
            onChange={handleInputChange}
          />
        </div>

        <div className="col-md-2 mb-3">
          <label className="form-label">Price From</label>
          <input
            type="number"
            step="0.01"
            name="priceFrom"
            className="form-control"
            value={filters.priceFrom || ''}
            onChange={handleInputChange}
          />
        </div>

        <div className="col-md-2 mb-3">
          <label className="form-label">Price To</label>
          <input
            type="number"
            step="0.01"
            name="priceTo"
            className="form-control"
            value={filters.priceTo || ''}
            onChange={handleInputChange}
          />
        </div>

        <div className="col-md-2 mb-3">
          <div className="form-check">
            <input
              type="checkbox"
              name="isInStock"
              className="form-check-input"
              checked={filters.isInStock || false}
              onChange={handleCheckboxChange}
              id="isInStock"
            />
            <label className="form-check-label" htmlFor="isInStock">
              In Stock Only
            </label>
          </div>
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

export default CarListingFilters;
