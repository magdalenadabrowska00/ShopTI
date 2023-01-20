import Card from "react-bootstrap/Card";

const Home = () => {
  return (
    <>
      <div
        className="d-flex justify-content-center align-items-center"
        style={{ minHeight: "500px", minWidth: "500px" }}
      >
        <Card>
          <Card.Body>
            <Card.Title>Welcome on home page!</Card.Title>
          </Card.Body>
        </Card>
      </div>
    </>
  );
};
export default Home;
