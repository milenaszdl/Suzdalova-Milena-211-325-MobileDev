

using Microsoft.Data.Sqlite;

internal class Program
{
    private static SqliteConnection db;
    private static SqliteCommand cmdDB;

    static void Main(string[] args)
    {
        db = new SqliteConnection();
        db.Open();

        cmdDB = new SqliteCommand();
        cmdDB.Connection = db;

        cmdDB.CommandText = "CREATE TABLE Users (id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, name varchar(50) NOT NULL, age INTEGER NOT NULL) ";
        cmdDB.ExecuteNonQuery();

        cmdDB.CommandText = "INSERT INTO Users (name, age) VALUES ('Миша', 21)";
        cmdDB.ExecuteNonQuery();        
        
        cmdDB.CommandText = "INSERT INTO Users (name, age) VALUES (@name, @age)";
        cmdDB.Parameters.Add(new("@name", "Юра"));
        cmdDB.Parameters.Add(new("@age", 25));
        cmdDB.ExecuteNonQuery();
        cmdDB.Parameters.Clear();

        cmdDB.CommandText = "INSERT INTO Users (name, age) VALUES ('Акакий', 20); SELECT last_insert_rowid();";
        Console.WriteLine($"id new = {cmdDB.ExecuteScalar()}");

        cmdDB.CommandText = "SELECT COUNT(*) FROM Users";
        Console.WriteLine($"count={ cmdDB.ExecuteScalar()}");


        cmdDB.CommandText = "SELECT * FROM Users";
        using (var r = cmdDB.ExecuteReader()) 
        {
            while (r.Read())
            {
                var id = r["id"];
                var name = r["name"];
                var age = r["age"];
                Console.WriteLine($"{id} | {name} | {age} ");
            }
        }
    }
}