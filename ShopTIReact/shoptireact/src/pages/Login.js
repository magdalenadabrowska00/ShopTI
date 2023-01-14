import { Container, Row, Col } from "react-bootstrap";
import Form from "react-bootstrap/Form";
import Button from "react-bootstrap/Button";
import { useContext, useRef } from "react";
import AuthContext from "../components/shared/AuthContext";

const Login = () => {
  const email = useRef("");
  const password = useRef("");

  const { login } = useContext(AuthContext);

  const loginSubmit = async () => {
    let userLoginData = {
      email: email.current.value,
      password: password.current.value,
    };
    await login(userLoginData);
  };
  return (
    <>
      <Container className="mt-2">
        <Row>
          <Col className="col-md-8 offset-md-2">
            <legend>Login Form</legend>
            <form>
              <Form.Group className="mb-3" controlId="formUserName">
                <Form.Label>Email address</Form.Label>
                <Form.Control type="text" ref={email} />
              </Form.Group>
              <Form.Group className="mb-3" controlId="formPassword">
                <Form.Label>Password</Form.Label>
                <Form.Control type="password" ref={password} />
              </Form.Group>
              <Button variant="primary" type="button" onClick={loginSubmit}>
                Login
              </Button>
            </form>
          </Col>
        </Row>
      </Container>
    </>
  );
};

export default Login;
