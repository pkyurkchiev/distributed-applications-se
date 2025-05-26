import React from 'react';

const CarListingSort = ({ sortOptions, onSortChange }) => {
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
          <option value="price">Price</option>
          <option value="make">Make</option>
          <option value="manufactureYear">Year</option>
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


export default CarListingSort;
