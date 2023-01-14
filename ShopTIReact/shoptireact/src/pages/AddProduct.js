import { Container, Row, Col } from "react-bootstrap";
import Form from "react-bootstrap/Form";
import Button from "react-bootstrap/Button";
import { useNavigate } from "react-router-dom";
import axios from "axios";
import { useRef, useState } from "react";
import jwtInterceptor from "../components/shared/jwtInterceptor";

const AddProduct = () => {
  const productName = useRef("");
  const price = useRef("");
  const navigate = useNavigate();

  const newProductSubmit = async () => {
    let productData = {
      productName: productName.current.value,
      price: price.current.value,
    };
    await newProduct(productData);
  };

  const newProduct = async (productData) => {
    const apiResponse = await jwtInterceptor.post(
      "https://localhost:7177/api/product/newProduct",
      productData
    );
    console.log(apiResponse.data);
    navigate("/");
  };

  return (
    <>
      <Container className="mt-2">
        <Row>
          <Col className="col-md-8 offset-md-2">
            <legend>Adding new products Form</legend>
            <form>
              <Form.Group className="mb-3" controlId="formUserName">
                <Form.Label>Product name</Form.Label>
                <Form.Control type="text" ref={productName} />
              </Form.Group>
              <Form.Group className="mb-3" controlId="formPassword">
                <Form.Label>Product price</Form.Label>
                <Form.Control type="text" ref={price} />
              </Form.Group>
              <Button
                variant="primary"
                type="button"
                onClick={newProductSubmit}
              >
                Add
              </Button>
            </form>
          </Col>
        </Row>
      </Container>
    </>
  );
};

export default AddProduct;
