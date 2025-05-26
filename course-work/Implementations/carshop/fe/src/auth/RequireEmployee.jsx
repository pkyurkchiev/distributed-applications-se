import { Navigate } from 'react-router-dom';
import { useAuth } from './AuthProvider';

const RequireEmployee = ({ children }) => {
  const { user, loading } = useAuth();

  if (loading) return <div>Loading...</div>;

  if (!user || !user.role.includes('EMPLOYEE')) {

    return <Navigate to="/" replace />;
  }

  return children;
};

export default RequireEmployee;
