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
        internal string[] getRegistro()
        {
            DataTable dt = getDataTable();
            if (dt.Rows.Count == 0) return null;
            return System.Array.ConvertAll(dt.Rows[0].ItemArray, x => x.ToString().Trim());
        }
        internal string[][] getRegistros()
        {
            DataTable dt = getDataTable();
            if (dt.Rows.Count == 0) return null;
            int i = 0;
            string[][] mRegistros = new string[dt.Rows.Count][];
            foreach (DataRow dr in dt.Rows)
                mRegistros[i++] = System.Array.ConvertAll(dr.ItemArray, x => x.ToString().Trim());
            return mRegistros;
        }
    }
}
