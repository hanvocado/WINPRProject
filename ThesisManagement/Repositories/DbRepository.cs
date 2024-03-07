using System.Data.SqlClient;
using System.Windows.Markup;

namespace ThesisManagement.Repositories
{
    public class DbRepository
    {
        private readonly string _conn;

        public DbRepository()
        {
            _conn = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ThesisManagement;Integrated Security=True";
          //  _conn= Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = ThesisManagement; Integrated Security = True
        }

        protected SqlConnection GetConnection()
        {
            return new SqlConnection(_conn);
        }
    }
}
