import { useNavigate } from 'react-router-dom';
import { useState } from 'react';
import axios from 'axios';
import '../styles/Login.css';

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
  
      const userId = response.data.data.id;
      localStorage.setItem('userId', userId);
  
      console.log(response.data);
      navigate('/home', { state: { userId: userId } });
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
    <div className='container'>
      <div className='login-form'>
        <h1>Login</h1>
        <div>
          <label>Username:</label><br />
          <input type="text" value={username} onChange={handleUsernameChange} />
        </div>
        <div>
          <label>Password:</label><br />
          <input type="password" value={password} onChange={handlePasswordChange} />
        </div>
        {error && (
          <div className='error-message'>{error}</div>
        )}
        <button onClick={handleLogin}>Login</button>
      </div>
    </div>
  );
};

export default Login;