import axios from "axios";
import { useEffect, useRef, useState } from "react";
import Card from "react-bootstrap/Card";
import CardHeader from "react-bootstrap/esm/CardHeader";
import ListGroup from "react-bootstrap/ListGroup";

const AllProducts = (props) => {
  // const productId = useRef("");
  // const price = useRef("");
  // const productName = useRef("");

  const [products, setProducts] = useState([]);

  //const {id} = useParams(); -> do useEffect(`URL cały/${id}`)
  useEffect(() => {
    axios
      .get("https://localhost:7177/api/product/getProducts")
      .then((response) => {
        // productId.current.value = response.data.productId;
        // price.current.value = response.data.price;
        // productName.current.value = response.data.productName;
        setProducts(response.data);
      });
  }, []);

  return products.map((item) => (
    <Card key={item.productId} style={{ width: "18rem" }}>
      <CardHeader title={item.productId}></CardHeader>
      <Card.Body>
        <Card.Text>{item.price}</Card.Text>
        <Card.Text>{item.productName}</Card.Text>
      </Card.Body>
    </Card>
  ));

  // <Card style={{ width: "18rem" }}>
  //   <ListGroup>
  //     <ListGroup.Item>Cras justo odio</ListGroup.Item>
  //     <ListGroup.Item>Dapibus ac facilisis in</ListGroup.Item>
  //     <ListGroup.Item>Vestibulum at eros</ListGroup.Item>
  //     <ListGroup.Item>Cras justo odio</ListGroup.Item>
  //     <ListGroup.Item>Dapibus ac facilisis in</ListGroup.Item>
  //     <ListGroup.Item>Vestibulum at eros</ListGroup.Item>
  //     <ListGroup.Item>Cras justo odio</ListGroup.Item>
  //     <ListGroup.Item>Dapibus ac facilisis in</ListGroup.Item>
  //     <ListGroup.Item>Vestibulum at eros</ListGroup.Item>
  //     <ListGroup.Item>Cras justo odio</ListGroup.Item>
  //     <ListGroup.Item>Dapibus ac facilisis in</ListGroup.Item>
  //     <ListGroup.Item>Vestibulum at eros</ListGroup.Item>
  //   </ListGroup>
  // </Card>
};

export default AllProducts;
