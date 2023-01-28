import logo from "./logo.svg";
import "./App.css";
import Layout from "./components/shared/Layout";
import { BrowserRouter, Routes, Route } from "react-router-dom";
import Home from "./pages/Home";
import Login from "./pages/Login";
import Register from "./pages/Register";
import AddProduct from "./pages/AddProduct";
import AllProducts from "./pages/AllProducts";
import AddOrder from "./pages/AddOrder";
import AddOrder1 from "./pages/AddOrder1";
import { AuthContextProvider } from "./components/shared/AuthContext";
import { AdOrderContextProvider } from "./components/AddOrderContext";

function App() {
  return (
    <>
      <AuthContextProvider>
        <AdOrderContextProvider>
          <Layout>
            <Routes>
              <Route path="/" element={<Home />}></Route>
              <Route path="/login" element={<Login />}></Route>
              <Route path="/registration" element={<Register />}></Route>
              <Route path="/addNewProduct" element={<AddProduct />}></Route>
              <Route path="/getProducts" element={<AllProducts />}></Route>
              <Route path="/addOrder" element={<AddOrder />}></Route>
              <Route
                path="/szczegolyZamowienia"
                element={<AddOrder1 />}
              ></Route>
            </Routes>
          </Layout>
        </AdOrderContextProvider>
      </AuthContextProvider>
    </>
  );
}

export default App;
