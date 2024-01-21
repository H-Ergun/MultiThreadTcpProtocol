using Microsoft.Data.SqlClient;
using Shared;

namespace Sender
{
    public class SqlContext
    {
        private static string connectionString = Sql.ConnectionString;            

        private static string tableName = "ThreadPools";
        private static string counterColumnName = "Count";
        private static string dateColumnName = "Date";
        private static string threadIdColumnName = "ThreadId";
        private readonly int Id = 0;

        private string updateQuery = $@"UPDATE {tableName} SET [{counterColumnName}] = [{counterColumnName}] + 1 ,
                                               [{dateColumnName}] = TRY_CAST(@Date AS DATETIME),
                                               [{threadIdColumnName}] = @ThreadId WHERE Id = @Id";

        private string getQuery = $@"Select * from {tableName}";

        public int Set()
        {

            using SqlConnection connection = new(connectionString);

            connection.Open();

            using SqlCommand command = connection.CreateCommand();
            command.CommandText = updateQuery;
            // Parametreler set ediliyor
            command.Parameters.AddWithValue("@Id", Id);
            command.Parameters.AddWithValue("@ThreadId", Environment.CurrentManagedThreadId);
            command.Parameters.AddWithValue("@Date", DateTime.Now);

            // Sorguyu yürüt
            int rowsAffected = command.ExecuteNonQuery();

            if (rowsAffected > 0)
            {
                Console.WriteLine("Belge başarıyla güncellendi.");
            }
            else
            {
                Console.WriteLine("Belirtilen ID ile eşleşen bir belge bulunamadı veya güncelleme yapılmadı.");
            }

            return rowsAffected;
        }


        public ThreadPoolModel Get()
        {
            using SqlConnection connection = new(connectionString);

            connection.Open();

            using SqlCommand command = connection.CreateCommand();
            command.CommandText = getQuery;

            // Sorguyu yürüt
            var reader = command.ExecuteReader();

            reader.Read();

            int id = Convert.ToInt32(reader["Id"]);
            int count = Convert.ToInt32(reader[counterColumnName]);
            DateTime date = Convert.ToDateTime(reader[dateColumnName] == DBNull.Value ? new DateTime(1900, 01, 01) : reader[dateColumnName]);
            string threadId = reader[threadIdColumnName]?.ToString() ?? "";

            ThreadPoolModel model = new()
            {
                Id = id,
                Count = count,
                Date = date,
                ThreadId = threadId
            };

            return model;



        }






    }
}
