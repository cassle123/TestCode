using System.Data;
using TestApi.Helper;

namespace TestApi.Data
{
    public class PurchaseOrderData
    {
        private readonly SqlHelper _sqlHelper;
        public PurchaseOrderData(SqlHelper sqlHelper)
        {
            _sqlHelper = sqlHelper;
        }

        public DataTable GetListOrder()
        {
            string query = "exec GetListOrder null";
            DataTable dataTable = _sqlHelper.DataTableTemplate(query);
            return dataTable;
        }

        public DataTable GetDetailsOrder(int OrderId)
        {
            string query = "exec GetListOrder @OrderId";
            DataTable dataTable = _sqlHelper.DataTableTemplate(query, OrderId);
            return dataTable;
        }
    }
}
