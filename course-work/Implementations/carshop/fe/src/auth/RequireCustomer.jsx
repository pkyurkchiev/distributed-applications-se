import { Navigate } from 'react-router-dom';
import { useAuth } from './AuthProvider';

const RequireCustomer = ({ children }) => {
  const { user, loading } = useAuth();

  if (loading) return <div>Loading...</div>;

  if (!user || !user.role.includes('CUSTOMER')) {

    return <Navigate to="/" replace />;
  }

  return children;
};

export default RequireCustomer;
