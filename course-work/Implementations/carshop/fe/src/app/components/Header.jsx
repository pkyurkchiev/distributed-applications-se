import React from "react";
import { useAuth } from "../../auth/AuthProvider";
import { Container, Navbar, Nav, Button } from "react-bootstrap";
import { useNavigate, NavLink } from "react-router-dom";

const Header = () => {
  const { user, logout } = useAuth();
  const navigate = useNavigate();

  const handleLogout = () => {
    logout();
    navigate("/");
  };


  return (
    <header>
      <Navbar bg="primary" variant="dark" expand="lg" className="mb-4">
        <Container>
          <Navbar.Brand as={NavLink} to="/">CarShop</Navbar.Brand>
          <Navbar.Toggle aria-controls="basic-navbar-nav" />
          <Navbar.Collapse id="basic-navbar-nav">
            <Nav className="me-auto">
              <Nav.Link as={NavLink} to="/cars">Cars</Nav.Link>

              {user?.role?.includes("EMPLOYEE") && (
                <>
                  <Nav.Link as={NavLink} to="/sales">Sales</Nav.Link>
                  <Nav.Link as={NavLink} to="/service-records">Services</Nav.Link>
                </>
              )}

              {user?.role?.includes("CUSTOMER") && (
                <>
                  <Nav.Link as={NavLink} to="/service-cars/my">My Cars</Nav.Link>
                  <Nav.Link as={NavLink} to="/service-records">My Services</Nav.Link>
                </>
              )}
            </Nav>

            <Nav className="ms-auto">
              {user ? (
                <>
                  <Navbar.Text className="me-3">
                    Welcome, <strong>{user.username}</strong>
                  </Navbar.Text>
                  <Button variant="outline-light" onClick={handleLogout}>
                    Logout
                  </Button>
                </>
              ) : (
                <>
                  <Nav.Link as={NavLink} to="/login">Login</Nav.Link>
                  <Nav.Link as={NavLink} to="/register">Register</Nav.Link>
                </>
              )}
            </Nav>
          </Navbar.Collapse>
        </Container>
      </Navbar>
    </header>
  );
};

export default Header;
