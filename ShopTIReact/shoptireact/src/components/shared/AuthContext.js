import { createContext, useState } from "react";
import axios from "axios";
import jwt_decode from "jwt-decode";
import { useNavigate } from "react-router-dom";

const AuthContext = createContext();

export const AuthContextProvider = ({ children }) => {
  const [user, setUser] = useState(() => {
    if (localStorage.getItem("tokens")) {
      let tokenData = JSON.parse(localStorage.getItem("tokens"));
      let accessToken = jwt_decode(tokenData.token);
      return accessToken;
    }
    return null;
  });
  const navigate = useNavigate();

  const login = async (userLoginData) => {
    const apiResponse = await axios.post(
      "https://localhost:7177/api/account/login",
      userLoginData
    );
    let accessToken = jwt_decode(apiResponse.data.token);
    console.log(accessToken);
    setUser(accessToken);
    localStorage.setItem("tokens", JSON.stringify(apiResponse.data));
    navigate("/");
  };

  const logout = () => {
    localStorage.removeItem("tokens");
    setUser(null);
    navigate("/");
  };
  return (
    <AuthContext.Provider value={{ login, user, logout }}>
      {children}
    </AuthContext.Provider>
  );
};

export default AuthContext;
