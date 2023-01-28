import { Container, Row, Col } from "react-bootstrap";
import Form from "react-bootstrap/Form";
import Button from "react-bootstrap/Button";
import { useNavigate } from "react-router-dom";
import axios from "axios";
import { useRef, useState } from "react";

const Register = () => {
  const navigate = useNavigate();

  const [checked, setChecked] = useState(true);

  const firstName = useRef("");
  const lastName = useRef("");
  const email = useRef("");
  const password = useRef("");
  const confirmPassword = useRef("");
  const role = useRef("");
  const country = useRef("");
  const city = useRef("");
  const street = useRef("");
  const postalCode = useRef("");

  const registerSubmit = async () => {
    let userRegisterData = {
      firstName: firstName.current.value,
      lastName: lastName.current.value,
      email: email.current.value,
      password: password.current.value,
      confirmPassword: confirmPassword.current.value,
      role: role.current.value,
      country: country.current.value,
      city: city.current.value,
      street: street.current.value,
      postalCode: postalCode.current.value,
    };
    await register(userRegisterData);
  };

  const register = async (userRegisterData) => {
    const apiResponse = await axios.post(
      "https://localhost:7177/api/account/register",
      userRegisterData
    );
    //console.log(apiResponse.data);
    navigate("/login");
  };

  return (
    <>
      <Container className="mt-2">
        <Row>
          <Col className="col-md-8 offset-md-2">
            <legend>Registration form</legend>
            <form>
              <Form.Group className="mb-3" controlId="formFirstName">
                <Form.Label>First name</Form.Label>
                <Form.Control type="text" ref={firstName} />
              </Form.Group>
              <Form.Group className="mb-3" controlId="formLastName">
                <Form.Label>Last name</Form.Label>
                <Form.Control type="text" ref={lastName} />
              </Form.Group>
              <Form.Group className="mb-3" controlId="formEmail">
                <Form.Label>Email</Form.Label>
                <Form.Control type="text" ref={email} />
              </Form.Group>
              <Form.Group className="mb-3" controlId="formPassword">
                <Form.Label>Password</Form.Label>
                <Form.Control type="password" ref={password} />
              </Form.Group>
              <Form.Group className="mb-3" controlId="formConfirmPassword">
                <Form.Label>Confirm password</Form.Label>
                <Form.Control type="password" ref={confirmPassword} />
              </Form.Group>
              <Form.Group className="mb-3" controlId="formCountry">
                <Form.Label>Country</Form.Label>
                <Form.Control type="text" ref={country} />
              </Form.Group>
              <Form.Group className="mb-3" controlId="formCity">
                <Form.Label>City</Form.Label>
                <Form.Control type="text" ref={city} />
              </Form.Group>
              <Form.Group className="mb-3" controlId="formStreet">
                <Form.Label>Street</Form.Label>
                <Form.Control type="text" ref={street} />
              </Form.Group>
              <Form.Group className="mb-3" controlId="formPostalCode">
                <Form.Label>Postal code</Form.Label>
                <Form.Control type="text" ref={postalCode} />
              </Form.Group>
              <Form.Check
                inline
                label="Admin"
                name="checkbox"
                type="radio"
                value="Admin"
                ref={role}
                onClick={() => setChecked(checked)}
              />

              <Form.Check
                inline
                label="User"
                name="checkbox"
                type="radio"
                value="User"
                ref={role}
                defaultChecked={true}
                onClick={() => setChecked(checked)}
              />
              <Button variant="primary" type="button" onClick={registerSubmit}>
                Register
              </Button>
            </form>
          </Col>
        </Row>
      </Container>
    </>
  );
};

export default Register;
