import { useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import axios from "axios";
import Swal from "sweetalert2";
import "./Login.css";

function Register() {
  const [username, setUsername] = useState("");
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const navigate = useNavigate();

  const validateEmail = (email) => {
    const regex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return regex.test(email);
  };

  const handleRegister = async () => {
    try {
      if (!validateEmail(email)) {
        Swal.fire({
          icon: 'error',
          title: 'Oops...',
          text: 'Please enter a valid email address.'
        });
        return;
      }

      const response = await axios.post(
        "https://localhost:7137/api/Login/register",
        {
          UserName: username,
          Email: email,
          Password: password,
        }
      );

      const message = response.data.message;
      console.log(message);
      if (message) {
        Swal.fire({
          icon: 'success',
          title: 'Success!',
          text: message
        }).then(() => {
          navigate("/login");
        });
      }
    } catch (error) {
      Swal.fire({
        icon: 'error',
        title: 'Oops...',
        text: error.response.data.error.message
      });
    }
  };

  return (
    <div className="container register">
      <div className="card grid">
        <h1 className="section-title">Register</h1>
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
          <label className="form-tag">Email</label>
          <input
            className="form-input"
            type="text"
            placeholder="Email"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
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
          <Link to="/login" className="btn">
            Sign In
          </Link>
          <button className="btn" onClick={() => handleRegister()}>
            Register
          </button>
        </div>
      </div>
    </div>
  );
}

export default Register;
