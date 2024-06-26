import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import { Login } from "./pages/Login";
import { Home } from "./pages/Home";
import { MyCourses } from "./pages/MyCourses";
import Register from "./pages/Register";
import "./styles/App.css";
import WebSocketService from "./services/WebSocketService";
import { useEffect } from "react";

function App() {
  useEffect(() => {
    WebSocketService.getInstance();
  }, []);

  return (
    <div className="App">
      <Router>
        <Routes>
          <Route path="/" element={<Login />} />
          <Route path="/register" element={<Register />} />
          <Route path="/home" element={<Home />} />
          <Route path="/myCourses" element={<MyCourses />} />
          <Route path="*" element={<h1>PAGE NOT FOUND</h1>} />
        </Routes>
      </Router>
    </div>
  );
}

export default App;
