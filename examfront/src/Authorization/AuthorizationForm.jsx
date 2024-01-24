import { useState } from "react";
import { useNavigate } from "react-router-dom";

export const AuthorizationForm = () => {
    const navigate = useNavigate();
    const [credentials, setCredentials] = useState({
        username: "",
        password: ""
    });
    const updateCredentials = (name, value) => {
        credentials[name] = value;
        setCredentials({ ...credentials });
      };
    const [error, setError] = useState();
    return (
        <>
            <input
              type="text"
              placeholder="User name"
              onChange={(e) => updateCredentials("username", e.target.value.trim())}
            />
            <input
              type="password"
              placeholder="Password"
              onChange={(e) => updateCredentials("password", e.target.value.trim())}
            />
        </>
    )

}