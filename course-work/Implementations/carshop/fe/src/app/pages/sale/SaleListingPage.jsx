import React, { useEffect, useState } from "react";
import { useNavigate, Link } from 'react-router-dom';
import apiClient from '../../../services/ApiClient';
import SaleListingFilters from "./components/SaleListingFilters";
import SaleListingSort from "./components/SaleListingSort.jsx";
import { useAuth } from '../../../auth/AuthProvider';

const SaleListingPage = () => {

  const { user, loading: authLoading } = useAuth();

  const [sales, setSales] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [page, setPage] = useState(0);
  const [size] = useState(6);
  const [totalPages, setTotalPages] = useState(0);

  const [filters, setFilters] = useState({});
  const [applyFilters, setApplyFilters] = useState(true);

  const [sortOptions, setSortOptions] = useState({
    sortBy: 'saleDate',
    asc: false,
  });

  const navigate = useNavigate();

  useEffect(() => {
    setLoading(true);
    setError(null);

    apiClient.post('/sales', {
      page,
      size,
      sortBy: sortOptions.sortBy,
      asc: sortOptions.asc,
      filters
    })
      .then((res) => {
        setSales(res.data.content);
        setTotalPages(res.data.totalPages);
      })
      .catch(() => {
        setError("Failed to load sales");
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

  const handleSaleClick = (id) => {
    navigate(`/sales/${id}`);
  };

  if (loading) return <p>Loading sales...</p>;
  if (error) return <p>{error}</p>;

  return (
    <>
      <SaleListingSort sortOptions={sortOptions} onSortChange={setSortOptions} />
      <SaleListingFilters filters={filters} onFilterChange={setFilters} onApplyFilters={setApplyFilters} />
      {user?.role === 'EMPLOYEE' ? <Link to="/sales/create" className="btn btn-primary">Create New Sale</Link> : null}
      <div className="container my-4">
        <h2 className="mb-4">Sales</h2>
        <div className="row">
          {sales.map((sale) => (
            <div
              key={sale.saleId}
              className="col-md-4 mb-3"
              style={{ cursor: 'pointer' }}
              onClick={() => handleSaleClick(sale.saleId)}
              role="button"
              tabIndex={0}
              onKeyDown={e => { if (e.key === 'Enter') handleSaleClick(sale.saleId); }}
            >
              <div className="card h-100">
                <div className="card-body">
                  <h5 className="card-title">{sale.carMake} {sale.carModel}</h5>
                  <p className="card-text">
                    Year: {sale.manufactureYear}<br />
                    Customer: {sale.customerFirstName} {sale.customerLastName}<br />
                    Sale Date: {sale.saleDate}<br />
                    Final Price: ${sale.finalPrice}
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

export default SaleListingPage;
