using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data.Sql;

namespace Library.DataConnections
{
    public class DB:IDataManager
    {
        public void AddNewUser(string fn, string ln, string log, string pass, int? ph, int? pid, int? ps)
        {
            
        }

        public void Connect()
        {
            
            string connStr = "server=localhost;user=librarygovan;database=id1564197;password=rael1122;";
            MySqlConnection conn = new MySqlConnection(connStr);
            using (conn)
            {
                string sql = "SELECT * FROM test";
                MySqlCommand command = new MySqlCommand(sql, conn);
                string name = command.ExecuteScalar().ToString();

            }
        }

        public void CreateNew()
        {
            // new table create
        }

        public void CreateBook()
        {
            throw new NotImplementedException();
        }

        public List<Models.Book> GetBooks()
        {
            
            throw new NotImplementedException();
        }
    }
}
