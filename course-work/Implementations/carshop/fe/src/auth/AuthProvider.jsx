import React, { createContext, useContext, useState, useEffect } from 'react';
import apiClient from '../services/ApiClient';
import { useNavigate } from 'react-router-dom';

const AuthContext = createContext(null);

export const AuthProvider = ({ children }) => {
  const [token, setToken] = useState(() => sessionStorage.getItem('token'));
  const [user, setUser] = useState(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    if (token) {
      setLoading(true);
      apiClient.post('/auth/me')
        .then(response => {
          setUser(response.data);
          setLoading(false);
        })
        .catch(() => {
          setUser(null);
          setToken(null);
          sessionStorage.removeItem('token');
          setLoading(false);
        });
    } else {
      setUser(null);
      setLoading(false);
    }
  }, [token]);

  const login = (newToken) => {
    sessionStorage.setItem('token', newToken);
    setToken(newToken);
  };

  const logout = () => {
    sessionStorage.removeItem('token');
    setToken(null);
    setUser(null);
  };

  return (
    <AuthContext.Provider value={{ token, user, login, logout, loading }}>
      {children}
    </AuthContext.Provider>
  );
};

export const useAuth = () => useContext(AuthContext);
