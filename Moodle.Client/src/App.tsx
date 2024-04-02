import { useState } from 'react';
import { BrowserRouter as Router, Routes, Route, Link } from 'react-router-dom';
import { Login } from './pages/Login';
import { Home } from './pages/Home';
import { MyCourses } from './pages/MyCourses';

function App() {
  const [isLoggedIn, setIsLoggedIn] = useState(false);

  return (
    <div className='App'>
      <Router>
        {isLoggedIn && (
          <>
            <Link to="/home"> Home </Link>
            <Link to="/myCourses"> My Courses </Link>
          </>
        )}
        <Routes>
          <Route path="/" element={<Login onLogin={() => setIsLoggedIn(true)} />} />
          <Route path="/home" element={<Home />} />
          <Route path="/myCourses" element={<MyCourses />} />
          <Route path="*" element={<h1>PAGE NOT FOUND</h1>} />
        </Routes>
      </Router>
    </div>
  );
}

export default App;
