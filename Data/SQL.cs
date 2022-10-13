using System.Data.SqlClient;

namespace H1AfsluttendeOpgaveSuperVigtig.Data
{
    public class SQL
    {
        private SqlConnection conn = new SqlConnection("" +
            "Data Source=192.168.2.2;" +
            "Initial Catalog=BreakfastDb;" +
            "User ID=sa;Password=Passw0rd;" +
            "Connect Timeout=30;Encrypt=False;" +
            "TrustServerCertificate=False;" +
            "ApplicationIntent=ReadWrite;" +
            "MultiSubnetFailover=False");
        public List<Food> ReadFood()
        {
            conn.Open();
            List<Food> foodList = new List<Food>();
            SqlCommand command = new SqlCommand("Select * from [Food]", conn);
            //command.Parameters.AddWithValue("@zip", "india");
            // int result = command.ExecuteNonQuery();
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Food f = new()
                    {
                        Id = int.Parse(reader["id"].ToString()),
                        Item = reader["item"].ToString(),
                        Price = double.Parse(reader["price"].ToString()),
                        Amount = int.Parse(reader["amount"].ToString())
                    };
                    foodList.Add(f);

                    Console.WriteLine(String.Format("{0}", reader["id"]));
                }
            }
            conn.Close();
            return foodList;
        }

    }
}
