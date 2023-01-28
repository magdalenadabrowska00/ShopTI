import { AddOrder } from "./AddOrder";
import { Container, Row, Col } from "react-bootstrap";
import Form from "react-bootstrap/Form";
import Button from "react-bootstrap/Button";
import { useContext, useRef, useState } from "react";
import { useNavigate } from "react-router-dom";
import axios from "axios";
import addOrderContext from "../components/AddOrderContext";

export default function AddOrder1({ childer }) {
  const { orderDetails } = useContext(addOrderContext);
  console.log(orderDetails);
  console.log(JSON.stringify(orderDetails));

  const [userEmail, setUserEmail] = useState();
  const [paymentMethod, setPaymentMethod] = useState();

  console.log(JSON.stringify(userEmail));
  console.log(JSON.stringify(paymentMethod));

  const navigate = useNavigate();

  const zamowienieSubmit = async () => {
    let zamowienie = {
      orderId: 0,
      userEmail: userEmail,
      paymentMethod: paymentMethod,
      totalPrice: 0,
      orderDetails: orderDetails,
    };
    console.log(JSON.stringify(zamowienie));
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
                <Form.Control
                  type="text"
                  value={userEmail}
                  onChange={(e) => setUserEmail(e.target.value)}
                />
              </Form.Group>
              <Form.Group className="mb-3" controlId="formPassword">
                <Form.Label>Payment method</Form.Label>
                <Form.Control
                  type="text"
                  value={paymentMethod}
                  onChange={(e) => setPaymentMethod(e.target.value)}
                />
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
