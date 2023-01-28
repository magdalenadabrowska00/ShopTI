import { createContext, useState } from "react";

const AdOrderContext = createContext();

export const AdOrderContextProvider = ({ children }) => {
  const [orderDetails, setOrderDetails] = useState([]);
  function handleAdd(item, ilosc) {
    var newItem = [
      {
        orderDetailId: 0,
        orderId: 0,
        productId: item.productId,
        productPrice: item.price,
        quantity: ilosc,
      },
    ];
    const od = orderDetails.concat(newItem);
    setOrderDetails(od);
  }

  return (
    <AdOrderContext.Provider value={{ orderDetails, handleAdd }}>
      {children}
    </AdOrderContext.Provider>
  );
};

export default AdOrderContext;
