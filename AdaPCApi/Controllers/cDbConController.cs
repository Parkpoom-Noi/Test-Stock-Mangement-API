using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AdaPCApi.Controllers
{
    public class cDbConController : ApiController
    {
        public SqlConnection C_PRCoDbCon()
        {
            string tDbPath = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=AdaProcess;Integrated Security=true";
            SqlConnection oDbCon = new SqlConnection(tDbPath);
            oDbCon.Open();
            return oDbCon;
        }
    }
}
