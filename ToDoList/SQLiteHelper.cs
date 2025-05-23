using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using static ToDoList.Form1;

namespace ToDoList
{
    public static class SQLiteHelper
    {
        private static string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "tasks.db");

        public static void InitializeDatabase()
        {
            if (!File.Exists(dbPath))
                SQLiteConnection.CreateFile(dbPath);

            using (var conn = new SQLiteConnection($"Data Source={dbPath};Version=3;"))
            {
                conn.Open();
                string sql = @"CREATE TABLE IF NOT EXISTS Tasks (
                                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                Text TEXT NOT NULL,
                                IsChecked INTEGER NOT NULL,
                                Mode TEXT NOT NULL
                            );";
                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void AddTask(TodoTask task)
        {
            using (var conn = new SQLiteConnection($"Data Source={dbPath};Version=3;"))
            {
                conn.Open();
                string sql = "INSERT INTO Tasks (Text, IsChecked, Mode) VALUES (@text, @checked, @mode)";
                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@text", task.Text);
                    cmd.Parameters.AddWithValue("@checked", task.IsChecked ? 1 : 0);
                    cmd.Parameters.AddWithValue("@mode", task.Mode);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static List<TodoTask> GetTasks(string mode)
        {
            var tasks = new List<TodoTask>();

            using (var conn = new SQLiteConnection($"Data Source={dbPath};Version=3;"))
            {
                conn.Open();
                var cmd = new SQLiteCommand("SELECT Id, Text, IsChecked FROM Tasks WHERE Mode = @mode", conn);
                cmd.Parameters.AddWithValue("@mode", mode);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tasks.Add(new TodoTask
                        {
                            Id = reader.GetInt32(0),
                            Text = reader.GetString(1),
                            IsChecked = reader.GetInt32(2) == 1,
                            Mode = mode
                        });
                    }
                }
            }

            return tasks;
        }

        public static void UpdateCheckState(TodoTask task)
        {
            using (var conn = new SQLiteConnection($"Data Source={dbPath};Version=3;"))
            {
                conn.Open();
                var cmd = new SQLiteCommand("UPDATE Tasks SET IsChecked = @isChecked WHERE Text = @text AND Mode = @mode", conn);
                cmd.Parameters.AddWithValue("@isChecked", task.IsChecked ? 1 : 0);
                cmd.Parameters.AddWithValue("@text", task.Text);
                cmd.Parameters.AddWithValue("@mode", task.Mode);
                cmd.ExecuteNonQuery();
            }
        }


        public static void ClearTasks(string mode)
        {
            using (var conn = new SQLiteConnection($"Data Source={dbPath};Version=3;"))
            {
                conn.Open();
                string sql = "DELETE FROM Tasks WHERE Mode = @mode";
                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@mode", mode);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void DeleteCheckedTasks(string mode, List<string> taskTextsToDelete)
        {
            using (var conn = new SQLiteConnection($"Data Source={dbPath};Version=3;"))
            {
                conn.Open();
                foreach (string taskText in taskTextsToDelete)
                {
                    string sql = "DELETE FROM Tasks WHERE Mode = @mode AND Text = @text";
                    using (var cmd = new SQLiteCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@mode", mode);
                        cmd.Parameters.AddWithValue("@text", taskText);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}

