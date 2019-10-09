using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.Data;

namespace liena107.conexion
{
    class conexion
    {
        private static string server = "localhost";
        private static string database = "db_linea107";
        private static string user = "root";
        private static string pass = "";
       
          private static string cad =
            string.Format("SERVER={0};DATABASE={1};UID={2};PASSWORD={3};CHARSET=utf8;"
            , server, database, user, pass);
        MySqlConnection con = new MySqlConnection(cad);
        public conexion() { }
        public void open()
        {
            con.Open();
        }
        public void close()
        {
            con.Close();
        }
        public void ejecutar_sql(string sql)
        {

            open();
            MySqlCommand comando = new MySqlCommand(sql, con);
            comando.ExecuteNonQuery();
            close();

        }
        public void cargar_datagrid(DataGridView dg, string sql)
        {
            open();
            MySqlDataAdapter Adapter = new MySqlDataAdapter(sql, con);



            DataSet set = new DataSet();

            Adapter.Fill(set);

            dg.DataSource = set.Tables[0];
            dg.Refresh();

            close();



        }
        public void cargar_combox(ComboBox cb, string sql, string display, string id)
        {
            open();
            MySqlCommand cmd = new MySqlCommand(sql, con);
            MySqlDataAdapter da1 = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            da1.Fill(dt);

            cb.ValueMember = id;
            cb.DisplayMember = display;
            cb.DataSource = dt;
            close();

        }





    }
}
