using System;
using System.Collections.Generic;
using SuperheroDatabase.Data;

namespace SuperheroDatabase.Business
{
    public class HeroManager
    {
        private HeroDataAccess dataAccess;
        private List<Hero> heroes;

        public HeroManager()
        {
            dataAccess = new HeroDataAccess();
            heroes = dataAccess.LoadHeroes();
        }

        // Add a hero object
        public void AddHero(Hero hero)
        {
            CalculateRankAndThreat(hero);
            dataAccess.AddHero(hero);
            heroes.Add(hero);
        }

        public List<Hero> GetAllHeroes()
        {
            // Refresh from DB to ensure up-to-date
            heroes = dataAccess.LoadHeroes();
            return heroes;
        }

        public void UpdateHero(Hero hero)
        {
            CalculateRankAndThreat(hero);
            dataAccess.UpdateHero(hero);

            // update in-memory list
            for (int i = 0; i < heroes.Count; i++)
            {
                if (heroes[i].HeroID == hero.HeroID)
                {
                    heroes[i] = hero;
                    break;
                }
            }
        }

        public void DeleteHero(string heroID)
        {
            dataAccess.DeleteHero(heroID);
            heroes.RemoveAll(delegate (Hero h) { return h.HeroID == heroID; });
        }

        private void CalculateRankAndThreat(Hero hero)
        {
            if (hero.Score >= 81) hero.Rank = "S-Rank";
            else if (hero.Score >= 61) hero.Rank = "A-Rank";
            else if (hero.Score >= 41) hero.Rank = "B-Rank";
            else hero.Rank = "C-Rank";

            if (hero.Rank == "S-Rank") hero.ThreatLevel = "Finals Week";
            else if (hero.Rank == "A-Rank") hero.ThreatLevel = "Midterm Madness";
            else if (hero.Rank == "B-Rank") hero.ThreatLevel = "Group Project Gone Wrong";
            else hero.ThreatLevel = "Pop Quiz";
        }

        // Bulk insert via DataAccess (used by CSV upload)
        public void BulkAddHeroes(List<Hero> heroesToAdd)
        {
            // calculate rank/threat for each
            for (int i = 0; i < heroesToAdd.Count; i++)
            {
                CalculateRankAndThreat(heroesToAdd[i]);
            }
            dataAccess.BulkInsert(heroesToAdd);
            // refresh in-memory list
            heroes = dataAccess.LoadHeroes();
        }


        // Summary helper (if needed)
        public string GenerateSummary()
        {
            heroes = dataAccess.LoadHeroes();
            int total = heroes.Count;
            double avgAge = 0;
            double avgScore = 0;
            if (total > 0)
            {
                int sumAge = 0;
                int sumScore = 0;
                for (int i = 0; i < heroes.Count; i++)
                {
                    sumAge += heroes[i].Age;
                    sumScore += heroes[i].Score;
                }
                avgAge = (double)sumAge / total;
                avgScore = (double)sumScore / total;
            }

            int sRank = 0, aRank = 0, bRank = 0, cRank = 0;
            for (int i = 0; i < heroes.Count; i++)
            {
                if (heroes[i].Rank == "S-Rank") sRank++;
                else if (heroes[i].Rank == "A-Rank") aRank++;
                else if (heroes[i].Rank == "B-Rank") bRank++;
                else if (heroes[i].Rank == "C-Rank") cRank++;
            }

            string summary = "Total Heroes: " + total + Environment.NewLine +
                             "Average Age: " + avgAge.ToString("F2") + Environment.NewLine +
                             "Average Exam Score: " + avgScore.ToString("F2") + Environment.NewLine +
                             "S-Rank: " + sRank + Environment.NewLine +
                             "A-Rank: " + aRank + Environment.NewLine +
                             "B-Rank: " + bRank + Environment.NewLine +
                             "C-Rank: " + cRank;
            return summary;
        }
    }
}
