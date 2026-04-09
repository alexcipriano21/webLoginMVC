using Microsoft.Data.SqlClient;
using System.Data;
namespace webLoginMVC.Controllers.bd
{
    public class clsBD
    {
        SqlConnection cn = null;
        SqlCommand cmd = null;
        SqlDataAdapter da = null;

        public clsBD(IConfiguration config, string BD)
        { 
            cn = new SqlConnection(config.GetConnectionString(BD));
            cmd = new SqlCommand("", cn);
            da = new SqlDataAdapter(cmd);
        }
        internal void Sentencia(string SQL)
        { 
            cmd.CommandText = SQL; 
            cmd.Parameters.Clear(); 
        }
        internal DataTable getDataTable()
        {
            DataTable dt = new DataTable();
            da.Fill(dt);    
            return dt;
        }
        internal DataRow getDataRow()
        {
            DataTable dt = getDataTable();
            if (dt.Rows.Count == 0) return null; 
            return dt.Rows[0]; 
        }
    }
}
