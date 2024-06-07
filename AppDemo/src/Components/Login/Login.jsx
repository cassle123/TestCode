import { useContext, useState } from "react";
import axios from "axios";
import { Link, useNavigate } from "react-router-dom";
import Swal from 'sweetalert2';
import "./Login.css";
import { AuthContext } from "../../contexts/AuthContext";

function Login() {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const navigate = useNavigate();
  const { login } = useContext(AuthContext);

  const handleLogin = async () => {
    try {
      const response = await axios.post(
        "https://localhost:7137/api/Login/login",
        {
          UserName: username,
          Password: password,
        }
      );

      const token = response.data.data;

      if (token) {
        localStorage.setItem("token", token);
        login();
        navigate("/");
      }
    } catch (error) {
      Swal.fire({
        icon: 'error',
        title: 'Oops...',
        text: error.response.data.error.message,
      });
    }
  };

  return (
    <div className="container login">
      <div className="card grid">
        <h1 className="section-title">Login</h1>
        <div className="form-div">
          <label className="form-tag">Username</label>
          <input
            className="form-input"
            type="text"
            placeholder="Username"
            value={username}
            onChange={(e) => setUsername(e.target.value)}
          />
        </div>
        <div className="form-div">
          <label className="form-tag">Password</label>
          <input
            className="form-input"
            type="password"
            placeholder="Password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
          />
        </div>
        <div className="form-btn">
          <Link to="/register" className="btn">
            Sign Up
          </Link>
          <button className="btn" onClick={() => handleLogin()}>
            Login
          </button>
        </div>
      </div>
    </div>
  );
}

export default Login;
