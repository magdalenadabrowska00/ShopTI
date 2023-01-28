import { AddOrder } from "./AddOrder";
import { Container, Row, Col } from "react-bootstrap";
import Form from "react-bootstrap/Form";
import Button from "react-bootstrap/Button";
import { useRef } from "react";
import { useNavigate } from "react-router-dom";
import axios from "axios";

export default function AddOrder1(props) {
  const { ilosc, orderDetails } = props;
  console.log(JSON.stringify(orderDetails));
  console.log(JSON.stringify(ilosc));

  const userEmail = useRef("");
  const paymentMethod = useRef("");
  const navigate = useNavigate();

  const zamowienieSubmit = async () => {
    let zamowienie = {
      userEmail: userEmail.current.value,
      paymentMethod: paymentMethod.current.value,
    };
    await order(zamowienie);
  };

  const order = async (zamowienie) => {
    const apiResponse = await axios.post(
      "https://localhost:7177/api/order",
      zamowienie
    );
    console.log(apiResponse.data);
    navigate("/");
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
                <Form.Control type="text" ref={userEmail} />
              </Form.Group>
              <Form.Group className="mb-3" controlId="formPassword">
                <Form.Label>Payment method</Form.Label>
                <Form.Control type="text" ref={paymentMethod} />
              </Form.Group>
              <Button
                variant="primary"
                type="button"
                onClick={zamowienieSubmit}
              >
                Zamów
              </Button>
            </form>
          </Col>
        </Row>
      </Container>
    </>
  );
}
