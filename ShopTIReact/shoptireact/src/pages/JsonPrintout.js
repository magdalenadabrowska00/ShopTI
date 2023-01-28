import { Container, Row, Col } from "react-bootstrap";
import Button from "react-bootstrap/Button";
import axios from "axios";
import { useNavigate } from "react-router-dom";

const JsonPrintout = () => {
  const navigate = useNavigate();

  const orderSubmit = async () => {
    const apiResponse = await axios.post(
      "https://localhost:7177/api/jsonFiles"
    );
    navigate("/");
  };

  return (
    <>
      <Container className="mt-2">
        <Row>
          <Col className="col-md-8 offset-md-2">
            <legend>Print orders</legend>
            <form>
              <Button variant="primary" type="button" onClick={orderSubmit}>
                Print
              </Button>
            </form>
          </Col>
        </Row>
      </Container>
    </>
  );
};
export default JsonPrintout;
