import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { getFetcher } from "../axios/AxiosInstance";
import Ports from "../consts/Ports";

const fetcher = getFetcher(Ports.ExamServer);

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
          .catch((err) => handleError(err));
      };

      const handleAuthorizationInfo = (data) => {
        if (data) {
          localStorage.setItem("access-token", data.token);
          navigate("/");
        }
      };
      const handleError = (err) => {
        if (err && err.response && err.response.data) {
            setError(err.response.data);
            return;
        }
        setError(err.message);
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