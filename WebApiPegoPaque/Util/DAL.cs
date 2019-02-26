using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace WebApiPegoPaque.Util
{
    public class DAL
    {
        private static string Server = "localhost";
        private static string Database = "webapi";
        private static string User = "root";
        private static string Senha = "vs130986";
        private MySqlConnection Connection;

        private string ConnectionString = $"Server={Server};Database={Database};Uid={User};Pwd={Senha};Sslmode=none;";

        public DAL()
        {
            Connection = new MySqlConnection(ConnectionString);
            Connection.Open();
        }

        public void ExecutarComandoSql(string sql)
        {
            MySqlCommand Command = new MySqlCommand(sql, Connection);
            Command.ExecuteNonQuery();
        }

        public DataTable RetornarDataTable(string sql)
        {
            MySqlCommand Command = new MySqlCommand(sql, Connection);
            MySqlDataAdapter DataAdaptar = new MySqlDataAdapter(Command);
            DataTable Dados = new DataTable();
            DataAdaptar.Fill(Dados);
            return Dados;
        }
    }
}
