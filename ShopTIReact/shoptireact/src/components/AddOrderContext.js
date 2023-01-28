import { createContext, useState } from "react";
import axios from "axios";
import jwt_decode from "jwt-decode";
import { useNavigate } from "react-router-dom";

const AdOrderContext = createContext();

export const AdOrderContextProvider = ({ children }) => {
  const [orderDetails, setOrderDetails] = useState([]);
  function handleAdd(item, ilosc) {
    const od = orderDetails.concat({ item, ilosc });
    setOrderDetails(od);
  }

  return (
    <AdOrderContext.Provider value={{ orderDetails, handleAdd }}>
      {children}
    </AdOrderContext.Provider>
  );
};

export default AdOrderContext;
