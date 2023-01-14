import logo from "./logo.svg";
import "./App.css";
import Layout from "./components/shared/Layout";
import { BrowserRouter, Routes, Route } from "react-router-dom";
import Home from "./pages/Home";
import Login from "./pages/Login";
import Register from "./pages/Register";
import AddProduct from "./pages/AddProduct";
import { AuthContextProvider } from "./components/shared/AuthContext";

function App() {
  return (
    // <BrowserRouter>
    //   <Routes>
    //     {/* <Route path="/" element={<Home />} />
    //     <Route path="login" element={<Login />} /> */}
    //   </Routes>
    // </BrowserRouter>
    <>
      <AuthContextProvider>
        <Layout>
          <Routes>
            <Route path="/" element={<Home />}></Route>
            <Route path="/login" element={<Login />}></Route>
            <Route path="/registration" element={<Register />}></Route>
            <Route path="/addNewProduct" element={<AddProduct />}></Route>
          </Routes>
        </Layout>
      </AuthContextProvider>
    </>
  );
}

export default App;
