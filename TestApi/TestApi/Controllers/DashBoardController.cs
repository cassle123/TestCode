using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestApi.Data;
using TestApi.Model.Responses;
using TestApi.Model;
using TestApi.Extensions;
using System.Data;
using Newtonsoft.Json;

namespace TestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashBoardController : ControllerBase
    {
        private readonly PurchaseOrderData _purchaseOrderData;
        public DashBoardController(PurchaseOrderData purchaseOrderData)
        {
            _purchaseOrderData = purchaseOrderData;
        }

        [HttpGet("ListPO")]
        [Authorize]
        public IActionResult GetListPurchaseOrders()
        {
            var orders = _purchaseOrderData.GetListOrder().ToClass<PurchaseOrders>();

            if (_purchaseOrderData.GetListOrder().Rows.Count > 0)
            {
                return Ok(ApiResponse.Success("Get List Orders success", orders));
            }
            else
            {
                return BadRequest(ApiResponse.Failed(new Error(404, "User not exists !!!")));
            }
        }

        [HttpGet("DetailsPO/{orderId}")]
        [Authorize]
        public IActionResult GetDetailsPO(string orderId) {

            var list = _purchaseOrderData.GetDetailsOrder(Convert.ToInt32(orderId));
            var details = list.ToClass<PurchaseOrders>();

            if (list.Rows.Count > 0)
            {
                return Ok(ApiResponse.Success("Get Details Orders success", details));
            }
            else
            {
                return BadRequest(ApiResponse.Failed(new Error(404, "Can't not details Order !!!")));
            }
        }


        [HttpGet("CheckStatus/{orderId}")]
        [Authorize]

        public IActionResult CheckStatusOrder(string orderId)
        {
            DataTable dt = _purchaseOrderData.GetDetailsOrder(Convert.ToInt32(orderId));
            var order = dt.ToClass<PurchaseOrders>();

            List<object> listResult = new List<object>();
            foreach (DataRow row in dt.Rows) {
                var require = row["Quantity"].ConvertType<int>();
                var instock = row["Instock"].ConvertType<int>();
                var product = row["ProductName"].ConvertType<string>();
                if (require > instock)
                {
                    listResult.Add(new { product = $"{product}", Miss = require - instock, Stock = instock });
                }
            }

            object response = new
            {
                Status = listResult.Count == 0,
                Required = listResult
            };
            if (listResult.Count == 0)
            {
                return Ok(ApiResponse.Success("Order status available", null));
            }
            else {
                return Ok(ApiResponse.Success("Order status not available", response));
            }
        }
    }
}
