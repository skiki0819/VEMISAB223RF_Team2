import { useState } from 'react';
import { useNavigate } from 'react-router-dom';

export const Login = ({ onLogin }: { onLogin: () => void }) => {
  const [isLoggedIn, setIsLoggedIn] = useState(false);
  const navigate = useNavigate();

  const handleLogin = () => {
    onLogin();
    setIsLoggedIn(true);
    navigate('/home');
  };

  if (isLoggedIn) {
    return null;
  }

  return (
    <div>
      <h1>Login Page</h1>
      <button onClick={handleLogin}>Login</button>
    </div>
  );
};
