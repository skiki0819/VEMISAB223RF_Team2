import { useNavigate } from 'react-router-dom';
import { useState } from 'react';
import axios from 'axios';

export const Login = () => {
  const navigate = useNavigate();
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');
  const [error, setError] = useState<string | null>(null);

  const handleUsernameChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setUsername(event.target.value);
  };

  const handlePasswordChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setPassword(event.target.value);
  };

  const handleLogin = async () => {
    try {
      const response = await axios.post('http://localhost:5191/api/Auth/Login', {
        username: username,
        password: password
      });
      console.log(response.data);
      navigate('/home');
    } catch (error: any) {
      if (axios.isAxiosError(error) && error.response && error.response.status === 404) {
        const errorMessage = error.response.data;
        console.log(errorMessage);
        setError(errorMessage);
      } else {
        console.error('Hiba történt a bejelentkezés során:', error);
      }
    }
  };
  
  return (
    <div>
      <h1>Login Page</h1>
      <div>
        <label>Username:</label>
        <input type="text" value={username} onChange={handleUsernameChange} />
      </div>
      <div>
        <label>Password:</label>
        <input type="password" value={password} onChange={handlePasswordChange} />
      </div>
      {error && (
        <div style={{ color: 'red', margin: '10px 0' }}>{error}</div>
      )}
      <button onClick={handleLogin}>Login</button>
    </div>
  );
};
