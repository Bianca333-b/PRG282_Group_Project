using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperheroDatabase.Data
{
    internal class HeroDataAccess
    {
        private readonly string _filePath;

        public HeroDataAccess(string filePath)
        {
            _filePath = filePath;
        }

        public void SaveHero(string record)
        {
            File.AppendAllText(_filePath, record + Environment.NewLine);
        }

        public List<string> LoadHeroes()
        {
            if (!File.Exists(_filePath))
                return new List<string>();

            return new List<string>(File.ReadAllLines(_filePath));
        }

        public bool HeroExists(string heroID)
        {
            if (!File.Exists(_filePath))
                return false;

            foreach (string line in File.ReadAllLines(_filePath))
            {
                if (line.StartsWith(heroID + ",", StringComparison.InvariantCultureIgnoreCase))
                    return true;
            }

            return false;
        }
    }
}
