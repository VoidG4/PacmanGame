using System;
using System.Data.SqlClient;

namespace PrReviewApp
{
    public class UserService
    {
        private string dbPassword = "SuperSecretPassword123!";

        public void GetUserById(string id)
        {
            string connStr = $"Server=myServerAddress;Database=myDataBase;User Id=myUsername;Password={dbPassword};";

            string query = "SELECT * FROM Users WHERE UserId = " + id;

            SqlConnection conn = new SqlConnection(connStr);
            conn.Open(); 

            SqlCommand cmd = new SqlCommand(query, conn);
            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine(reader[0]);
            }
        }

        public int CalculateDiscount(int price, int discountDivisor)
        {
            return price / discountDivisor;
        }
    }
}
