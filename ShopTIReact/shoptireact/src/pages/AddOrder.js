import axios from "axios";
import { Button } from "react-bootstrap";
import { useContext, useEffect, useRef, useState } from "react";
import Card from "react-bootstrap/Card";
import CardHeader from "react-bootstrap/esm/CardHeader";
import ListGroup from "react-bootstrap/ListGroup";
import Container from "react-bootstrap/Container";
import AddOrderContext from "../components/AddOrderContext";

export default function AddOrder() {
  const [products, setProducts] = useState([]);
  const [ilosc, setIlosc] = useState(0);
  const { handleAdd } = useContext(AddOrderContext);

  const [orderDetails, setOrderDetails] = useState([]);

  console.log(JSON.stringify(orderDetails));

  const handleOnChange = (event) => {
    const { name, value } = event.target;
    setIlosc({ ...ilosc, [name]: value });
  };

  //const {id} = useParams(); -> do useEffect(`URL cały/${id}`)
  useEffect(() => {
    axios
      .get("https://localhost:7177/api/product/getProducts")
      .then((response) => {
        setProducts(response.data);
      });
  }, []);

  return products.map((item) => (
    <>
      <Card key={item.productId} style={{ width: "18rem" }}>
        <CardHeader title={item.productId}></CardHeader>
        <Card.Body>
          <Card.Text>{item.price}</Card.Text>
          <Card.Text>{item.productName}</Card.Text>
          <Card.Text>
            <input
              type="text"
              name="ilosc1"
              value={ilosc}
              onChange={(e) => setIlosc(e.target.value)}
            />
          </Card.Text>
          <Button variant="primary" onClick={(e) => handleAdd(item, ilosc)}>
            Dodaj
          </Button>
        </Card.Body>
      </Card>
    </>
  ));
}
