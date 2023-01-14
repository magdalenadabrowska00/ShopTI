import Navbar from "react-bootstrap/Navbar";
import Container from "react-bootstrap/Container";
import Nav from "react-bootstrap/Nav";
import { Link } from "react-router-dom";
import { useContext, useEffect, useState } from "react";
import AuthContext from "./AuthContext";
import { Button } from "react-bootstrap";

const Layout = ({ children }) => {
  const { user, logout } = useContext(AuthContext);

  const [hideRole, setHideRole] = useState(false);

  useEffect(() => {
    if (
      user &&
      user["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"] ===
        "User"
    ) {
      setHideRole(true);
    } else {
      setHideRole(false);
    }
  });

  return (
    <>
      <Navbar bg="primary" variant="dark">
        <Navbar.Brand>
          <Nav.Link as={Link} to="/">
            Shop TI Project
          </Nav.Link>
        </Navbar.Brand>
        <Navbar.Toggle aria-controls="basic-navbar-nav" />
        <Navbar.Collapse id="basic-navbar-nav">
          <Nav className="ms-auto">
            {!user && (
              <Nav.Link as={Link} to="/registration">
                Register
              </Nav.Link>
            )}
            {!user && (
              <Nav.Link as={Link} to="/login">
                Login
              </Nav.Link>
            )}
            {user && (
              <Nav.Link href="#">
                {
                  user[
                    "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"
                  ]
                }
              </Nav.Link>
            )}
            {user && !hideRole && (
              <Nav.Link as={Link} to="/addNewProduct">
                New product
              </Nav.Link>
            )}
          </Nav>
          {user && (
            <Button
              variant="primary"
              type="button"
              onClick={() => {
                logout();
              }}
            >
              Logout
            </Button>
          )}
        </Navbar.Collapse>
      </Navbar>
      <Container>{children}</Container>
    </>
  );
};
export default Layout;
