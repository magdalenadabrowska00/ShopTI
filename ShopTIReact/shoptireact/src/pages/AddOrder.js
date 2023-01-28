import axios from "axios";
import { Button } from "react-bootstrap";
import { useEffect, useRef, useState } from "react";
import Card from "react-bootstrap/Card";
import CardHeader from "react-bootstrap/esm/CardHeader";
import ListGroup from "react-bootstrap/ListGroup";
import Container from "react-bootstrap/Container";

// const getFreshModelObject = () => ({
//   orderDetailId: 0,
//   orderId: 0,
//   productId: 0,
//   productPrice: 0,
//   quantity: 0,
//   // orderDetails: [],
// });

export default function AddOrder() {
  const [products, setProducts] = useState([]);
  const [ilosc, setIlosc] = useState(0);

  const [orderDetails, setOrderDetails] = useState([]);
  function handleAdd(item) {
    const od = orderDetails.concat({ item, ilosc });
    setOrderDetails(od);
  }

  console.log(JSON.stringify(orderDetails));

  const handleOnChange = (event) => {
    const { name, value } = event.target;
    setIlosc({ ...ilosc, [name]: value });
  };

  // const { values, setValues } = useState(getFreshModelObject);

  // const addProductItem = (productItem) => {
  //   let x = {
  //     orderDetailId: 0,
  //     orderId: 0,
  //     productId: productItem.productId,
  //     productPrice: productItem.price,
  //     quantity: ilosc,
  //   };
  //   setOrderDetails({
  //     ...values,
  //     orderDetails: [...values.orderDetails, x],
  //   });
  // };

  // setValues({
  //   ...values,
  //   orderDetails: [...values.orderDetails, x],
  // });

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
    <>
      <Card
        key={item.productId}
        style={{ width: "18rem" }}
        // onClick={(e) => addProductItem(item)}
      >
        <CardHeader title={item.productId}></CardHeader>
        <Card.Body>
          <Card.Text>{item.price}</Card.Text>
          <Card.Text>{item.productName}</Card.Text>
          <Card.Text>
            <input type="text" name="ilosc" onChange={handleOnChange} />
          </Card.Text>
          <Button
            variant="primary"
            // onClick={() =>
            //   setProduct((product) => [...product, { item, ilosc }])
            // }
            // onClick={(e) => addProductItem(item)}
            onClick={(e) => handleAdd({ item })}
          >
            Dodaj
          </Button>
        </Card.Body>
      </Card>
    </>
  ));
}
