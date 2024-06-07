import { Route, Routes } from "react-router-dom";
import Dashboard from "./Dashboard/Dashboard";
import Profile from "./Profile/Profile";
import PODetails from "./PO/PODetails";

function Layout() {
  return (
    <main className="main">
      <Routes>
        <Route path="/" element={<Dashboard />} />
        <Route path="/profile" element={<Profile />} />
        <Route path="/PODetails/:orderId" element={<PODetails />} />
      </Routes>
    </main>
  );
}

export default Layout;
