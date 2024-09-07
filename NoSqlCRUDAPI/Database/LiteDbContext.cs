using LiteDB;

namespace NoSqlCRUDAPI.Database;

public class LiteDbContext
{
    // Path to the database file
    private readonly string _databasePath = Path.Combine(Environment.CurrentDirectory, @"Database\LiteDb.db");

    public LiteDbContext()
    {
        // Create a new instance of the LiteDatabase
        Database = new LiteDatabase(_databasePath);
    }

    // Get the LiteDatabase instance
    public LiteDatabase Database { get; set; }
}