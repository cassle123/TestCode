import axios from "axios";
import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import Swal from "sweetalert2";
import "./PODetails.css";

function PODetails() {
  const [listDetails, setlistDetails] = useState([]);
  const { orderId } = useParams();
  const navigate = useNavigate();

  const token = localStorage.getItem("token");

  useEffect(() => {
    if (token) {
      axios
        .get(`https://localhost:7137/api/DashBoard/DetailsPO/${orderId}`, {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        })
        .then((response) => {
          setlistDetails(response.data.data);
        })
        .catch((error) => {
          console.error("Error fetching data", error);
        });
    }
  }, [orderId, token]);

  const handleBack = () => {
    navigate("/");
  };

  const handleCheckOrder = async () => {
    if (token) {
      try {
        const response = await axios.get(
          `https://localhost:7137/api/DashBoard/CheckStatus/${orderId}`,
          {
            headers: {
              Authorization: `Bearer ${token}`,
            },
          }
        );

        let message = "";
        if (response.data.data === null) {
          message = response.data.message;
          Swal.fire({
            title: 'Order Status',
            text: message,
            icon: 'success',
          });
        } else {
          const result = response.data.data.required;
          const message = result.map((item) => `<div>Product: ${item.product}, Miss: ${item.miss}, Instock: ${item.stock} </div>`).join("\n\n");
          Swal.fire({
            title: 'Order Check',
            html: message,
            icon: 'info',
          });
        }
        console.log(response);
      } catch (error) {
        console.error("Error fetching data", error);
        Swal.fire({
          title: 'Error',
          text: 'There was an error fetching the data',
          icon: 'error',
        });
      }
    } else {
      console.error("Token not found");
      Swal.fire({
        title: 'Error',
        text: 'Token not found',
        icon: 'error',
      });
    }
  };

  return (
    <div className="container dashboard">
      <h1 className="section-title">Details Order ID: {orderId}</h1>
      <div className="div-btn-back">
        <button className="btn-back" onClick={handleBack}>
          <i className="fa-solid fa-arrow-left-long"></i>
          Back to list
        </button>
      </div>
      <table>
        <thead>
          <tr>
            <th>Product Name</th>
            <th>Quantity</th>
            <th>Instock</th>
            <th>Price</th>
            <th>Total</th>
          </tr>
        </thead>
        <tbody>
          {listDetails &&
            listDetails.map((order, index) => (
              <tr key={index}>
                <td data-cell="Product Name">{order.productName}</td>
                <td data-cell="Quantity">{order.quantity}</td>
                <td data-cell="Instock">{order.instock}</td>
                <td data-cell="Price">{order.price}</td>
                <td data-cell="Total">{order.price * order.quantity}</td>
              </tr>
            ))}
        </tbody>
      </table>
      <div className="div-btn-check">
        <button className="btn" onClick={handleCheckOrder}>
          Check Order
        </button>
      </div>
    </div>
  );
}

export default PODetails;
