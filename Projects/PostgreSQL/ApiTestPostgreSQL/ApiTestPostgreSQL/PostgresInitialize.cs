using Npgsql;

namespace ApiTestPostgreSQL;

public class PostgresInitialize
{
    private static string Host = "localhost";
    private static string User = "postgres";
    private static string DBname = "myfirstdatabase";
    private static string Password = "example";
    private static string Port = "5432";

    public static void Initialize()
    {
        string connString =
            String.Format(
                "Server={0};Username={1};Database={2};Port={3};Password={4};SSLMode=Prefer",
                Host,
                User,
                DBname,
                Port,
                Password);


        using (var conn = new NpgsqlConnection(connString))

        {
            Console.Out.WriteLine("Opening connection");
            conn.Open();

            using (var command = new NpgsqlCommand("DROP TABLE IF EXISTS inventory", conn))
            {
                command.ExecuteNonQuery();
                Console.Out.WriteLine("Finished dropping table (if existed)");

            }

            using (var command = new NpgsqlCommand("CREATE TABLE inventory(id serial PRIMARY KEY, name VARCHAR(50), quantity INTEGER)", conn))
            {
                command.ExecuteNonQuery();
                Console.Out.WriteLine("Finished creating table");
            }

            using (var command = new NpgsqlCommand("INSERT INTO inventory (name, quantity) VALUES (@n1, @q1), (@n2, @q2), (@n3, @q3)", conn))
            {
                command.Parameters.AddWithValue("n1", "banana");
                command.Parameters.AddWithValue("q1", 150);
                command.Parameters.AddWithValue("n2", "orange");
                command.Parameters.AddWithValue("q2", 154);
                command.Parameters.AddWithValue("n3", "apple");
                command.Parameters.AddWithValue("q3", 100);

                int nRows = command.ExecuteNonQuery();
                Console.Out.WriteLine(String.Format("Number of rows inserted={0}", nRows));
            }
        }
    }
}