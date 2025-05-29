import React, { useEffect, useState } from "react";
import { useNavigate, Link } from 'react-router-dom';
import apiClient from '../../../services/ApiClient';
import CarListingFilters from "./components/CarListingFilters";
import CarListingSort from "./components/CarListingSort";
import { useAuth } from '../../../auth/AuthProvider';

const CarsListPage = () => {

  const { user, loading: authLoading } = useAuth();

  const [cars, setCars] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [page, setPage] = useState(0);
  const [size] = useState(6);
  const [totalPages, setTotalPages] = useState(0);
  const [filters, setFilters] = useState({isInStock: true});
  const [applyFilters, setApplyFilters] = useState(true);

  const [sortOptions, setSortOptions] = useState({
    sortBy: 'id',
    asc: false,
  });

  const navigate = useNavigate();

  useEffect(() => {
    setLoading(true);
    setError(null);

    apiClient.post('/cars', {
      page: page,
      size: size,
      sortBy: sortOptions.sortBy,
      asc: sortOptions.asc,
      filters: filters
    })
      .then((res) => {
        setCars(res.data.content);
        setTotalPages(res.data.totalPages);
      })
      .catch(() => {
        setError("Failed to load cars");
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

  const handleCarClick = (id) => {
    navigate(`/cars/${id}`);
  };

  if (loading) return <p>Loading cars...</p>;
  if (error) return <p>{error}</p>;

  return (
    <>
      
      <CarListingSort sortOptions={sortOptions} onSortChange={setSortOptions} />
      <CarListingFilters filters={filters} onFilterChange={setFilters} onApplyFilters={setApplyFilters}/>
      {user?.role === 'EMPLOYEE' ? <Link to="/cars/create" className="btn btn-primary">Create New Car</Link> : null}
      <div className="container my-4">
        <h2 className="mb-4">Available Cars</h2>
        <div className="row">
          {cars.map((car) => (
            <div
              key={car.id}
              className="col-md-4 mb-3"
              style={{ cursor: 'pointer' }}
              onClick={() => handleCarClick(car.id)}  // Click handler here
              role="button"
              tabIndex={0}
              onKeyDown={e => { if (e.key === 'Enter') handleCarClick(car.id); }}
            >
              <div className="card h-100">
                {car.imageBase64 ? (
                  <img
                    src={`data:image/jpeg;base64,${car.imageBase64}`}
                    className="card-img-top"
                    alt={`${car.make} ${car.model}`}
                    style={{ height: "180px", objectFit: "cover" }}
                  />
                ) : (
                  <div
                    className="card-img-top bg-secondary d-flex align-items-center justify-content-center"
                    style={{ height: "180px", color: "#fff" }}
                  >
                    No Image
                  </div>
                )}
                <div className="card-body">
                  <h5 className="card-title">
                    {car.make} {car.model}
                  </h5>
                  <p className="card-text">
                    Year: {car.manufactureYear} <br />
                    Price: ${car.price}
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

export default CarsListPage;
