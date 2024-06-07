import { Routes, Route, Navigate } from "react-router-dom";
import Register from "./Components/Login/Register";
import Dashboard from "./Components/Dashboard/Dashboard";
import Login from "./Components/Login/Login";
import { useContext } from "react";
import { AuthContext } from "./contexts/AuthContext";
import Profile from "./Components/Profile/Profile";
import Sidebar from "./Components/Sidebar/Sidebar";
import Layout from "./Components/Layout";

const routes = [
  {
    path: `/`,
    element: <Dashboard />,
    name: "Dashboard",
    icon: "fa-solid fa-house",
  },
  {
    path: "/profile",
    element: <Profile />,
    name: "Profile",
    icon: "fa-solid fa-circle-info ",
  },
];

function App() {
  const { isAuthenticated } = useContext(AuthContext);

  return (
    <>
      <Routes>
        <Route path="/login" element={<Login />} />
        <Route path="/register" element={<Register />} />

        {!isAuthenticated && (
          <Route path="*" element={<Navigate to="/login" />} />
        )}
      </Routes>

      {isAuthenticated && (
        <>
          <Sidebar routes={routes} />
          <Layout />
        </>
      )}
    </>
  );
}

export default App;
