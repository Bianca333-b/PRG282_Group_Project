using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace SuperheroDatabase.Data
{
    public class Hero
    {
        public string HeroID { get; set; }
        public string Name { get; set; }           // maps to DB column Name
        public int Age { get; set; }
        public string Superpower { get; set; }
        public int Score { get; set; }
        public string Rank { get; set; }
        public string ThreatLevel { get; set; }
    }

    public class HeroDataAccess
    {
        //  (Server=.;Initial Catalog=DB_Bianca;...)
        private string connectionString = @"Server=.;Initial Catalog=DB_Bianca; User ID=sa; Password=sa2025@1;";

        public void AddHero(Hero hero)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;
            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();
                string sql = "INSERT INTO Superheroes (HeroID, Name, Age, Superpower, Score, Rank, ThreatLevel) " +
                             "VALUES (@HeroID, @Name, @Age, @Superpower, @Score, @Rank, @ThreatLevel)";
                cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@HeroID", hero.HeroID);
                cmd.Parameters.AddWithValue("@Name", hero.Name);
                cmd.Parameters.AddWithValue("@Age", hero.Age);
                cmd.Parameters.AddWithValue("@Superpower", hero.Superpower);
                cmd.Parameters.AddWithValue("@Score", hero.Score);
                cmd.Parameters.AddWithValue("@Rank", hero.Rank);
                cmd.Parameters.AddWithValue("@ThreatLevel", hero.ThreatLevel);
                cmd.ExecuteNonQuery();
            }
            finally
            {
                if (cmd != null) cmd.Dispose();
                if (conn != null) conn.Close();
            }
        }

        public List<Hero> LoadHeroes()
        {
            List<Hero> heroes = new List<Hero>();
            SqlConnection conn = null;
            SqlCommand cmd = null;
            SqlDataReader reader = null;
            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();
                string sql = "SELECT HeroID, Name, Age, Superpower, Score, Rank, ThreatLevel FROM Superheroes";
                cmd = new SqlCommand(sql, conn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Hero h = new Hero();
                    h.HeroID = reader["HeroID"].ToString();
                    h.Name = reader["Name"].ToString();
                    h.Age = Convert.ToInt32(reader["Age"]);
                    h.Superpower = reader["Superpower"].ToString();
                    h.Score = Convert.ToInt32(reader["Score"]);
                    h.Rank = reader["Rank"].ToString();
                    h.ThreatLevel = reader["ThreatLevel"].ToString();
                    heroes.Add(h);
                }
            }
            finally
            {
                if (reader != null) reader.Close();
                if (cmd != null) cmd.Dispose();
                if (conn != null) conn.Close();
            }
            return heroes;
        }

        public void UpdateHero(Hero hero)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;
            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();
                string sql = "UPDATE Superheroes SET Name=@Name, Age=@Age, Superpower=@Superpower, " +
                             "Score=@Score, Rank=@Rank, ThreatLevel=@ThreatLevel WHERE HeroID=@HeroID";
                cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@HeroID", hero.HeroID);
                cmd.Parameters.AddWithValue("@Name", hero.Name);
                cmd.Parameters.AddWithValue("@Age", hero.Age);
                cmd.Parameters.AddWithValue("@Superpower", hero.Superpower);
                cmd.Parameters.AddWithValue("@Score", hero.Score);
                cmd.Parameters.AddWithValue("@Rank", hero.Rank);
                cmd.Parameters.AddWithValue("@ThreatLevel", hero.ThreatLevel);
                cmd.ExecuteNonQuery();
            }
            finally
            {
                if (cmd != null) cmd.Dispose();
                if (conn != null) conn.Close();
            }
        }

        public void DeleteHero(string heroID)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;
            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();
                string sql = "DELETE FROM Superheroes WHERE HeroID=@HeroID";
                cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@HeroID", heroID);
                cmd.ExecuteNonQuery();
            }
            finally
            {
                if (cmd != null) cmd.Dispose();
                if (conn != null) conn.Close();
            }
        }

        // Bulk insert used by CSV upload (basic transaction)
        public void BulkInsert(List<Hero> heroes)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;
            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();
                SqlTransaction tx = conn.BeginTransaction();
                try
                {
                    string sql = "INSERT INTO Superheroes (HeroID, Name, Age, Superpower, Score, Rank, ThreatLevel) " +
                                 "VALUES (@HeroID, @Name, @Age, @Superpower, @Score, @Rank, @ThreatLevel)";
                    foreach (Hero hero in heroes)
                    {
                        cmd = new SqlCommand(sql, conn, tx);
                        cmd.Parameters.AddWithValue("@HeroID", hero.HeroID);
                        cmd.Parameters.AddWithValue("@Name", hero.Name);
                        cmd.Parameters.AddWithValue("@Age", hero.Age);
                        cmd.Parameters.AddWithValue("@Superpower", hero.Superpower);
                        cmd.Parameters.AddWithValue("@Score", hero.Score);
                        cmd.Parameters.AddWithValue("@Rank", hero.Rank);
                        cmd.Parameters.AddWithValue("@ThreatLevel", hero.ThreatLevel);
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                    }
                    tx.Commit();
                }
                catch
                {
                    tx.Rollback();
                    throw;
                }
            }
            finally
            {
                if (cmd != null) cmd.Dispose();
                if (conn != null) conn.Close();
            }
        }
    }
}
