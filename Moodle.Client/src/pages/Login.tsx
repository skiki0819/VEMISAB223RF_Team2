// Login.tsx
import { useNavigate } from "react-router-dom";
import { useState } from "react";
import "../styles/Login.css";
import axios from "axios";

export const Login = () => {
  const navigate = useNavigate();
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [error, setError] = useState<string | null>(null);

  const handleUsernameChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setUsername(event.target.value);
  };

  const handlePasswordChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setPassword(event.target.value);
  };

  const handleLogin = async () => {
    try {
      const response = await axios.post(
        "http://localhost:5191/api/Auth/Login",
        {
          username: username,
          password: password,
        }
      );

      const userId = response.data.data.id;
      const token = response.data.data.token;
      const roleId = response.data.data.role.id;
      localStorage.setItem("userId", userId);
      localStorage.setItem("token", token);
      localStorage.setItem("roleId", roleId);

      console.log(response.data);
      navigate("/home", { state: { userId: userId } });
    } catch (error) {
      if (
        axios.isAxiosError(error) &&
        error.response &&
        error.response.status === 404
      ) {
        const errorMessage = error.response.data;
        console.log(errorMessage);
        setError(errorMessage);
      } else {
        console.error("Hiba történt a bejelentkezés során:", error);
      }
    }
  };

  const handleRegisterClick = () => {
    navigate("/register");
  };

  return (
    <div className="container">
      <div className="login-form">
        <h1>Login</h1>
        <div>
          <label>Username:</label>
          <br />
          <input type="text" value={username} onChange={handleUsernameChange} />
        </div>
        <div>
          <label>Password:</label>
          <br />
          <input
            type="password"
            value={password}
            onChange={handlePasswordChange}
          />
        </div>
        {error && <div className="error-message">{error}</div>}
        <div className="buttons">
          <button onClick={handleLogin}>Login</button>
          <button onClick={handleRegisterClick}>Register</button>
        </div>
      </div>
    </div>
  );
};

export default Login;
