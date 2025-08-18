using Microsoft.Data.Sqlite;
using System.IO;

namespace GunlukUygulamasi
{
    public static class DatabaseHelper
    {
        private static string dbPath = "Gunlukler.db";
        private static string connString = $"Data Source={dbPath}";

        static DatabaseHelper()
        {
            if (!File.Exists(dbPath))
            {
                using (var conn = new SqliteConnection(connString))
                {
                    conn.Open();
                    string sql = @"CREATE TABLE Gunlukler(
                                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                    Tarih TEXT,
                                    Sukur1 TEXT,
                                    Sukur2 TEXT,
                                    Sukur3 TEXT,
                                    Yanlislar TEXT,
                                    Duzeltmeler TEXT,
                                    Dogrular TEXT,
                                    GunIcinde TEXT
                                  )";
                    var cmd = new SqliteCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static SqliteConnection GetConnection()
        {
            return new SqliteConnection(connString);
        }
    }
}
