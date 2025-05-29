import React from 'react';
import { Container, Row, Col } from 'react-bootstrap';

const Footer = () => {
  return (
    <footer className="bg-dark text-light py-4 mt-auto">
      <Container>
        <Row className="text-center text-md-left">
          <Col md={6}>
            <h5>CarShop</h5>
            <p>&copy; {new Date().getFullYear()} All rights reserved.</p>
          </Col>
          <Col md={6} className="mt-3 mt-md-0">
            <h6>Contact Us</h6>
            <p>Email: support@carshop.com</p>
            <p>Phone: +1 (555) 123-4567</p>
          </Col>
        </Row>
      </Container>
    </footer>
  );
};

export default Footer;
