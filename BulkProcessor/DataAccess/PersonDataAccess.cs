using Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace BulkProcessor.DataAccess
{
    public interface IPersonDataAccess
    {
        void InsertPerson(string title, string first, string last);
    }

    public class PersonDataAccess : IPersonDataAccess
    {
        private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Code\ProtoTypes\BulkProcessing-master\BulkProcessor\Data\Database.mdf;Integrated Security=True";

        public void InsertPerson(string title, string first, string last)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                db.Open();
                using (IDbTransaction transactionScope = db.BeginTransaction(IsolationLevel.Serializable))
                {
                    string insertQuery = @"INSERT INTO [dbo].[People](
    [Title],     
    [FirstName], 
    [LastName]  
  ) VALUES (
    @Title,
	@FirstName, 
    @LastName 
)";

                    db.Execute(insertQuery, new
                    {
                        Title = title,
                        FirstName = first,
                        LastName = last
                    }, transactionScope);

                    transactionScope.Commit();
                }
            }
        }
    }
}
