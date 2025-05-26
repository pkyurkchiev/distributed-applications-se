import { Navigate } from 'react-router-dom';
import { useAuth } from './AuthProvider';

const RequireUser = ({ children }) => {
  const { user, loading } = useAuth();

  if (loading) return <div>Loading...</div>;

  if (!user) {

    return <Navigate to="/login" replace />;
  }

  return children;
};

export default RequireUser;