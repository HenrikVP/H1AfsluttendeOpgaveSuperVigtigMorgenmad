using System.Data;
using System.Data.SqlClient;

namespace H1AfsluttendeOpgaveSuperVigtig.Data
{
    public class SQL
    {
        private SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=BreakfastDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        public List<Food> ReadFood()
        {          
            List<Food> foodList = new List<Food>();
            SqlCommand command = new SqlCommand("Select * from [Food]", conn);
            conn.Open();
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
                }
            }
            conn.Close();
            return foodList;
        }

        public bool CreateFood(Food f)
        {
            using (conn)
            {
                var cmd = new SqlCommand(
                    "INSERT INTO [Food] " +
                    "VALUES (@item, @amount, @price)", conn);
                cmd.Parameters.Add("@item", SqlDbType.NVarChar).Value = f.Item;
                cmd.Parameters.Add("@amount", SqlDbType.Int).Value = f.Amount;
                cmd.Parameters.Add("@price", SqlDbType.Decimal).Value = f.Price;
                conn.Open();
                if (cmd.ExecuteNonQuery() == 1) return true; else return false;
            }
        }

    }
}
