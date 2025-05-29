import React, { useEffect, useState } from "react";
import { useNavigate, Link } from 'react-router-dom';
import apiClient from '../../../services/ApiClient';

import ServiceCarFilters from "./components/ServiceCarFilters";
import ServiceCarSort from "./components/ServiceCarSort";
import { useAuth } from '../../../auth/AuthProvider';

const ServiceCarListPage = () => {

  const { user } = useAuth();

  const [serviceCars, setServiceCars] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [page, setPage] = useState(0);
  const [size] = useState(6);
  const [totalPages, setTotalPages] = useState(0);
  const [filters, setFilters] = useState({});
  const [applyFilters, setApplyFilters] = useState(true);

  const [sortOptions, setSortOptions] = useState({
    sortBy: 'id',
    asc: false,
  });

  const navigate = useNavigate();

  useEffect(() => {
    setLoading(true);
    setError(null);

    apiClient.post(`/service-cars/my/${user?.id}`, {
      page,
      size,
      sortBy: sortOptions.sortBy,
      asc: sortOptions.asc,
      filters
    })
      .then((res) => {
        setServiceCars(res.data.content);
        setTotalPages(res.data.totalPages);
      })
      .catch(() => {
        setError("Failed to load service cars");
      })
      .finally(() => {
        setLoading(false);
      });
  }, [page, size, applyFilters, sortOptions]);

  const goPrev = () => {
    if (page > 0) setPage(page - 1);
  };

  const goNext = () => {
    if (page < totalPages - 1) setPage(page + 1);
  };

  const handleCardClick = (id) => {
    navigate(`/service-cars/my/${id}`);
  };

  if (loading) return <p>Loading service cars...</p>;
  if (error) return <p>{error}</p>;

  return (
    <>
      <ServiceCarSort sortOptions={sortOptions} onSortChange={setSortOptions} />
      <ServiceCarFilters filters={filters} onFilterChange={setFilters} onApplyFilters={setApplyFilters} />
      {user?.role === 'CUSTOMER' ? <Link to="/service-cars/my/create" className="btn btn-primary">Create New Car</Link> : null}
      <div className="container my-4">
        <h2 className="mb-4">Service Cars</h2>
        <div className="row">
          {serviceCars.map((car) => (
            <div
              key={car.id}
              className="col-md-4 mb-3"
              style={{ cursor: 'pointer' }}
              onClick={() => handleCardClick(car.id)}
              role="button"
              tabIndex={0}
              onKeyDown={e => { if (e.key === 'Enter') handleCardClick(car.id); }}
            >
              <div className="card h-100">
                <div className="card-body">
                  <h5 className="card-title">{car.make} {car.model}</h5>
                  <p className="card-text">
                    Year: {car.manufactureYear}<br />
                    License Plate: {car.licensePlate}
                  </p>
                </div>
              </div>
            </div>
          ))}
        </div>

        <div className="d-flex justify-content-between my-4">
          <button
            className="btn btn-primary"
            onClick={goPrev}
            disabled={page === 0}
          >
            Previous
          </button>
          <span>
            Page {page + 1} of {totalPages}
          </span>
          <button
            className="btn btn-primary"
            onClick={goNext}
            disabled={page === totalPages - 1}
          >
            Next
          </button>
        </div>
      </div>
    </>
  );
};

export default ServiceCarListPage;
