using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace MyDataCenter.DAL.SystemManage
{
    public class SmartPages : BaseDAL
    {
        public static List<Models.SystemManage.SmartPagesModel> GetPages()
        {
            using (var sqlconn = new SqlConnection(SqlConnString))
            {
                return sqlconn.Query<Models.SystemManage.SmartPagesModel>("SELECT * FROM dbo.SmartPages").AsList();
            }
        }
    }
}
