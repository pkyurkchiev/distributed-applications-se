import React, { useEffect, useState } from "react";
import { useNavigate, Link } from 'react-router-dom';
import apiClient from '../../../services/ApiClient';
import { useAuth } from '../../../auth/AuthProvider';
import ServiceRecordListingFilters from "./components/ServiceRecordListingFilters";
import ServiceRecordListingSort from "./components/ServiceRecordListingSort";

const ServiceRecordListingPage = () => {

  const { user, loading: authLoading } = useAuth();

  const [services, setServices] = useState([]);
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
    if (authLoading) return; 
    setLoading(true);
    setError(null);
    if(user?.role === 'EMPLOYEE'){
      console.log("emp")
      apiClient.post('/service-records', {
            page,
            size,
            asc: sortOptions.asc,
            filters
          })
            .then((res) => {
              setServices(res.data.content);
              setTotalPages(res.data.totalPages);
            })
            .catch(() => {
              setError("Failed to load services");
            })
            .finally(() => {
              setLoading(false);
            });
    }else{
      console.log("customer")
      apiClient.post(`/service-records/my/${user.id}`, {
            page,
            size,
            asc: sortOptions.asc,
            filters
          })
            .then((res) => {
              setServices(res.data.content);
              setTotalPages(res.data.totalPages);
            })
            .catch(() => {
              setError("Failed to load services");
            })
            .finally(() => {
              setLoading(false);
            });
    }

    
  }, [page, size, applyFilters, sortOptions, authLoading]);

  const goPrev = () => {
    if (page > 0) setPage(page - 1);
  };

  const goNext = () => {
    if (page < totalPages - 1) setPage(page + 1);
  };

  const handleServiceClick = (id) => {
    navigate(`/service-records/${id}`);
  };

  if (loading) return <p>Loading services...</p>;
  if (error) return <p>{error}</p>;

  return (
    <>
      <ServiceRecordListingSort sortOptions={sortOptions} onSortChange={setSortOptions} /> 
      <ServiceRecordListingFilters filters={filters} onFilterChange={setFilters} onApplyFilters={setApplyFilters}/>
      {user?.role === 'EMPLOYEE' ? <Link to="/service-records/create" className="btn btn-primary">Create New Service</Link> : null}
      <div className="container my-4">
        <h2 className="mb-4">Service Records</h2>
        <div className="row">
          {services.map((service) => (
            <div
              key={service.id}
              className="col-md-4 mb-3"
              style={{ cursor: 'pointer' }}
              onClick={() => handleServiceClick(service.id)}
              role="button"
              tabIndex={0}
              onKeyDown={e => { if (e.key === 'Enter') handleServiceClick(service.id); }}
            >
              <div className="card h-100">
                <div className="card-body">
                  <h5 className="card-title">{service.carMake} {service.carModel}</h5>
                  <p className="card-text">
                    License Plate: {service.licensePlate}<br />
                    Employee: {service.employeeFirstName} {service.employeeLastName}<br />
                    Date: {service.serviceDate}<br />
                    Cost: ${service.serviceCost}<br />
                    <em>{service.serviceDescription}</em>
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

export default ServiceRecordListingPage;
