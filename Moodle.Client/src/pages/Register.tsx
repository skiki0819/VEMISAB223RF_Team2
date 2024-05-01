// Register.tsx
import { useState, useEffect } from "react";
import axios from "axios";
import { useNavigate } from "react-router-dom";
import "../styles/Register.css";
import { sha256 } from "js-sha256";

const Register = () => {
  const navigate = useNavigate();
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [name, setName] = useState("");
  const [degreeOptions, setDegreeOptions] = useState<any[]>([]);
  const [roleIdOptions, setRoleIdOptions] = useState<any[]>([]);
  const [selectedDegree, setSelectedDegree] = useState("");
  const [selectedRoleId, setSelectedRoleId] = useState("");
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    // Degree options lekérése az API-ról
    axios
      .get("http://localhost:5191/api/Degree/GetAll")
      .then((response) => {
        if (response.data.success) {
          setDegreeOptions(response.data.data);
        } else {
          console.error("Diploma adatok lekérése sikertelen");
        }
      })
      .catch((error) => {
        console.error("Hiba történt a diploma adatok lekérésekor:", error);
      });

    // RoleId options lekérése az API-ról
    axios
      .get("http://localhost:5191/api/Role/GetRoles")
      .then((response) => {
        if (response.data.success) {
          setRoleIdOptions(response.data.data);
        } else {
          console.error("Szerep adatok lekérése sikertelen");
        }
      })
      .catch((error) => {
        console.error("Hiba történt a szerep adatok lekérésekor:", error);
      });
  }, []);

  const handleUsernameChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setUsername(event.target.value);
  };

  const handlePasswordChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setPassword(event.target.value);
  };

  const handleNameChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setName(event.target.value);
  };

  const handleDegreeChange = (event: React.ChangeEvent<HTMLSelectElement>) => {
    setSelectedDegree(event.target.value);
  };

  const handleRoleIdChange = (event: React.ChangeEvent<HTMLSelectElement>) => {
    setSelectedRoleId(event.target.value);
  };

  const handleRegister = async () => {
    try {
      const hashedPassword = sha256(password).toString();
      const response = await axios.post(
        "http://localhost:5191/api/Auth/Register",
        {
          username: username,
          password: hashedPassword,
          name: name,
          degreeId: selectedDegree,
          roleId: selectedRoleId,
        }
      );

      console.log(response.data);
      // Sikeres regisztráció után navigálás a bejelentkező oldalra
      navigate("/");
    } catch (error) {
      console.error("Hiba történt a regisztráció során:", error);
      setError("Regisztráció sikertelen");
    }
  };

  const handleLoginClick = () => {
    navigate("/");
  };

  return (
    <div className="container">
      <div className="register-form">
        <h1>Register</h1>
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
        <div>
          <label>Name:</label>
          <br />
          <input type="text" value={name} onChange={handleNameChange} />
        </div>
        <div>
          <label>Degree:</label>
          <br />
          <select value={selectedDegree} onChange={handleDegreeChange}>
            <option value="">Select degree</option>
            {degreeOptions.map((degree) => (
              <option key={degree.id} value={degree.id}>
                {degree.name}
              </option>
            ))}
          </select>
        </div>
        <div>
          <label>Role:</label>
          <br />
          <select value={selectedRoleId} onChange={handleRoleIdChange}>
            <option value="">Select role</option>
            {roleIdOptions.map((role) => (
              <option key={role.id} value={role.id}>
                {role.name}
              </option>
            ))}
          </select>
        </div>
        {error && <div className="error-message">{error}</div>}
        <div className="buttons">
          <button onClick={handleRegister}>Register</button>
          <button onClick={handleLoginClick}>Login</button>
        </div>
      </div>
    </div>
  );
};

export default Register;
