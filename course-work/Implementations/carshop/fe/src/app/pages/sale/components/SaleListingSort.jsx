import React from 'react';

const SaleListingSort = ({ sortOptions, onSortChange }) => {
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
          <option value="saleId">Default</option>
          <option value="carMake">Car Make</option>
          <option value="carModel">Car Model</option>
          <option value="manufactureYear">Manufacture Year</option>
          <option value="customerFirstName">Customer First Name</option>
          <option value="customerLastName">Customer Last Name</option>
          <option value="saleDate">Sale Date</option>
          <option value="finalPrice">Final Price</option>
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

export default SaleListingSort;
