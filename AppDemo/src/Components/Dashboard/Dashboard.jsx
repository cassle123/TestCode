import { useEffect, useState } from "react";
import axios from "axios";
import "./Dashboard.css";
import { Link } from "react-router-dom";

function Dashboard() {
  const [listOrder, setListOrder] = useState([]);

  useEffect(() => {
    const token = localStorage.getItem("token");

    if (token) {
      axios
        .get("https://localhost:7137/api/DashBoard/ListPO", {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        })
        .then((response) => {
          const formattedOrders = response.data.data.map((order) => {
            return {
              ...order,
              orderDate: new Date(order.orderDate).toLocaleDateString("en-US", {
                year: "numeric",
                month: "2-digit",
                day: "2-digit",
              }),
            };
          });
          setListOrder(formattedOrders);
        })
        .catch((error) => {
          console.error("Lỗi khi lấy dữ liệu", error);
        });
    }
  }, []);

  return (
    <div className="container dashboard">
      <h1 className="section-title">DashBoard</h1>
      <div>
        <table>
          <thead>
            <tr>
              <th>Order ID</th>
              <th>User Name</th>
              <th>Description</th>
              <th>Date Create</th>
              <th>Details</th>
            </tr>
          </thead>
          <tbody>
            {listOrder &&
              listOrder.map((order, index) => (
                <tr key={index}>
                  <td data-cell="Order ID">{order.orderId}</td>
                  <td data-cell="User Name">{order.username}</td>
                  <td data-cell="Description">{order.description}</td>
                  <td data-cell="Date Create">{order.orderDate}</td>
                  <td className="view-details">
                    <Link
                      to={`/PODetails/${order.orderId}`}
                      className="view-btn"
                    >
                      <i className="fa-regular fa-eye icon-view-table"></i>
                    </Link>
                  </td>
                </tr>
              ))}
          </tbody>
        </table>
      </div>
    </div>
  );
}

export default Dashboard;
