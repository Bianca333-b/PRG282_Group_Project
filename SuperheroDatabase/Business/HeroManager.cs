using SuperheroDatabase.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperheroDatabase.Business
{
    internal class HeroManager
    {
        private readonly HeroDataAccess _dataAccess;

        public HeroManager(string filePath)
        {
            _dataAccess = new HeroDataAccess(filePath);
        }

        public bool AddHero(string heroID, string name, string ageText, string superpower, int score, out string message)
        {
            message = string.Empty;

            // Validation
            if (string.IsNullOrWhiteSpace(heroID) ||
                string.IsNullOrWhiteSpace(name) ||
                string.IsNullOrWhiteSpace(ageText) ||
                string.IsNullOrWhiteSpace(superpower))
            {
                message = "Please fill in all fields.";
                return false;
            }

            if (!int.TryParse(ageText, out int age) || age < 1 || age > 120)
            {
                message = "Please enter a valid age (1–120).";
                return false;
            }

            if (_dataAccess.HeroExists(heroID))
            {
                message = "A hero with that ID already exists.";
                return false;
            }

            string rank = GetRank(score);
            string threat = GetThreat(rank);

            string record = $"{heroID},{EscapeCommas(name)},{age},{EscapeCommas(superpower)},{score},{rank},{EscapeCommas(threat)}";
            _dataAccess.SaveHero(record);

            message = "Superhero added successfully!";
            return true;
        }

        public List<string[]> GetAllHeroes()
        {
            List<string> lines = _dataAccess.LoadHeroes();
            return lines.Select(line => line.Split(',')).ToList();
        }

        private string EscapeCommas(string input) => input.Replace(",", ";");

        private string GetRank(int score)
        {
            if (score >= 81) return "S";
            if (score >= 61) return "A";
            if (score >= 41) return "B";
            return "C";
        }

        private string GetThreat(string rank)
        {
            return rank switch
            {
                "S" => "Finals Week",
                "A" => "Midterm Madness",
                "B" => "Group Project Gone Wrong",
                _ => "Pop Quiz"
            };
        }
    }
}
