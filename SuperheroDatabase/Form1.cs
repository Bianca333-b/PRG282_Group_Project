using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SuperheroDatabase.Business;


namespace SuperheroDatabase
{
    public partial class Form1 : Form
    {
        private HeroManager heroManager;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string heroesFilePath = Path.Combine(Application.StartupPath, "superheroes.txt");
            heroManager = new HeroManager(heroesFilePath);

            // Configure grid
            dgvSuperheroes.AllowUserToAddRows = false;
            dgvSuperheroes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSuperheroes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            if (dgvSuperheroes.Columns.Count == 0)
            {
                dgvSuperheroes.Columns.Add("HeroID", "Hero ID");
                dgvSuperheroes.Columns.Add("Name", "Name");
                dgvSuperheroes.Columns.Add("Age", "Age");
                dgvSuperheroes.Columns.Add("Superpower", "Superpower");
                dgvSuperheroes.Columns.Add("Score", "Exam Score");
                dgvSuperheroes.Columns.Add("Rank", "Rank");
                dgvSuperheroes.Columns.Add("Threat", "Threat Level");
            }

            nudExamScore.Minimum = 0;
            nudExamScore.Maximum = 100;
            LoadHeroesToGrid();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            bool success = heroManager.AddHero(
                txtHeroID.Text,
                txtName.Text,
                txtAge.Text,
                txtSuperpower.Text,
                (int)nudExamScore.Value,
                out string message
            );

            MessageBox.Show(message,
                success ? "Success" : "Error",
                MessageBoxButtons.OK,
                success ? MessageBoxIcon.Information : MessageBoxIcon.Warning);

            if (success)
            {
                ClearInputs();
                LoadHeroesToGrid();
            }
        }

        private void LoadHeroesToGrid()
        {
            dgvSuperheroes.Rows.Clear();
            var heroes = heroManager.GetAllHeroes();
            foreach (var h in heroes)
            {
                if (h.Length >= 7)
                    dgvSuperheroes.Rows.Add(h[0], h[1], h[2], h[3], h[4], h[5], h[6]);
            }
        }

        private void ClearInputs()
        {
            txtHeroID.Clear();
            txtName.Clear();
            txtAge.Clear();
            txtSuperpower.Clear();
            nudExamScore.Value = 0;
        }
    }

}


