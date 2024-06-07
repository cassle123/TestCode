import axios from "axios";
import { useEffect, useState } from "react";
import "./Profile.css";

function Profile() {
  const [profile, setProfile] = useState(null);

  const token = localStorage.getItem("token");

  useEffect(() => {
    if (token) {
      axios
        .get(`https://localhost:7137/api/Profile`, {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        })
        .then((response) => {
          console.log(response.data.data);
          setProfile(response.data.data);
        })
        .catch((error) => {
          console.error("Lỗi khi lấy dữ liệu", error);
        });
    }
  }, [token]);

  return (
    <div className="container">
      {profile ? (
        <div>
          <h1 className="section-title">User Profile</h1>
          <div className="profile-data">
            <p>
              <strong>Username:</strong> {profile.username}
            </p>
            <p>
              <strong>Email:</strong> {profile.email}
            </p>
            <p>
              <strong>Joined Date:</strong>{" "}
              {new Date(profile.createdAt).toLocaleDateString("en-US", {
                year: "numeric",
                month: "2-digit",
                day: "2-digit",
              })}
            </p>
          </div>
        </div>
      ) : (
        <p>Loading profile...</p>
      )}
    </div>
  );
}

export default Profile;
