using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace EPATEC_Service_2
{
    public class DataBase
    {

        public static SqlConnection getConnection()
        {
            SqlConnection connection = new SqlConnection("Data source=PABLO-WINDOWS\\SQL2014; Initial Catalog=EPATEC_MASTER; User Id=PABLO; Password=gremory1212951995");

            return connection;
        }
        
    }
}