import React from 'react';
import Header from './Header';
import Footer from './Footer';
import { Container } from 'react-bootstrap';
import { Outlet } from 'react-router-dom';

const MainLayout = () => {
  return (
    <div className="d-flex flex-column min-vh-100">
      <Header />
      <Container className="flex-grow-1 my-4">
        <Outlet></Outlet>
      </Container>
      <Footer />
    </div>
  );
};

export default MainLayout;
