import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { getFetcher } from "../axios/AxiosInstance";

const fetcher = getFetcher(7290);

export const AuthorizationForm = () => {
    const navigate = useNavigate();
    const [error, setError] = useState();
    const [credentials, setCredentials] = useState({
        username: "",
        password: ""
    });
    const updateCredentials = (name, value) => {
        credentials[name] = value;
        setCredentials({ ...credentials });
    };

    const handleSubmitForm = (event) => {
        event.preventDefault();
        setError("")

        fetcher
          .post("auth/login", credentials)
          .then((res) => handleAuthorizationInfo(res.data))
          .catch((err) => setError(err.response.data));
      };

      const handleAuthorizationInfo = (data) => {
        if (data) {
          localStorage.setItem("access-token", data.token);
          navigate("/");
        }
      };
    
    return (
        <>
            <form onSubmit={handleSubmitForm}>
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
                <input type="submit" value="OK" />
                <div>{error}</div>
            </form>   
        </>
    )

}