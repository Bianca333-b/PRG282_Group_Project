using SuperheroDatabase.Business;
using SuperheroDatabase.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SuperheroDatabase
{
    public partial class Form1 : Form
    {

        private string connString = "Server=.; Initial Catalog=DB_Bianca; User ID=sa; Password=sa2025@1";

        private HeroManager manager;

        public Form1()
        {
            InitializeComponent();
            manager = new HeroManager();

            // Wire event handlers — if designer already wired them, duplicates are harmless
            btnAdd.Click += btnAdd_Click;
            btnViewAll.Click += btnViewAll_Click;
            btnUpdate.Click += btnUpdate_Click;
            btnDelete.Click += btnDelete_Click;
            
            btnGenerateReport.Click += btnGenerateReport_Click;
            if (this.Controls.ContainsKey("btnUploadFile"))
            {
                Button uploadBtn = (Button)this.Controls["btnUploadFile"];
                uploadBtn.Click += btnUploadFile_Click;
            }

            dgvSuperheroes.CellClick += dgvSuperheroes_CellClick;
            if (this.Controls.ContainsKey("btnUploadFile"))
            {
                // If your designer includes btnUploadFile
                Button uploadBtn = (Button)this.Controls["btnUploadFile"];
                uploadBtn.Click += btnUploadFile_Click;
            }
            dgvSuperheroes.CellClick += dgvSuperheroes_CellClick;

            // ensure numeric range
            nudExamScore.Minimum = 0;
            nudExamScore.Maximum = 100;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            RefreshGrid();
        }

        private void RefreshGrid()
        {
            dgvSuperheroes.Rows.Clear();
            List<Hero> heroes = manager.GetAllHeroes();
            for (int i = 0; i < heroes.Count; i++)
            {
                Hero h = heroes[i];
                dgvSuperheroes.Rows.Add(h.HeroID, h.Name, h.Age, h.Superpower, h.Score, h.Rank, h.ThreatLevel);
            }
        }

        private bool ValidateInput()
        {
            int age;
            if (string.IsNullOrWhiteSpace(txtHeroID.Text) ||
                string.IsNullOrWhiteSpace(txtName.Text) ||
                string.IsNullOrWhiteSpace(txtAge.Text) ||
                string.IsNullOrWhiteSpace(txtSuperpower.Text))
            {
                MessageBox.Show("Please fill in all fields.");
                return false;
            }
            if (!int.TryParse(txtAge.Text.Trim(), out age))
            {
                MessageBox.Show("Age must be a number.");
                return false;
            }
            if (age < 0 || age > 120)
            {
                MessageBox.Show("Enter a realistic age (0-120).");
                return false;
            }
            if ((int)nudExamScore.Value < 0 || (int)nudExamScore.Value > 100)
            {
                MessageBox.Show("Score must be 0-100.");
                return false;
            }
            return true;
        }

        private void ClearInputs()
        {
            txtHeroID.Text = "";
            txtName.Text = "";
            txtAge.Text = "";
            txtSuperpower.Text = "";
            nudExamScore.Value = 0;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;
            Hero h = new Hero();
            h.HeroID = txtHeroID.Text.Trim();
            h.Name = txtName.Text.Trim();
            h.Age = int.Parse(txtAge.Text.Trim());
            h.Superpower = txtSuperpower.Text.Trim();
            h.Score = (int)nudExamScore.Value;

            try
            {
                manager.AddHero(h);
                RefreshGrid();
                ClearInputs();
                MessageBox.Show("Added hero.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding hero: " + ex.Message);
            }
        }

        private void btnViewAll_Click(object sender, EventArgs e)
        {
            RefreshGrid();
        }

        private void dgvSuperheroes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            DataGridViewRow row = dgvSuperheroes.Rows[e.RowIndex];
            txtHeroID.Text = Convert.ToString(row.Cells[0].Value);
            txtName.Text = Convert.ToString(row.Cells[1].Value);
            txtAge.Text = Convert.ToString(row.Cells[2].Value);
            txtSuperpower.Text = Convert.ToString(row.Cells[3].Value);
            nudExamScore.Value = Convert.ToDecimal(row.Cells[4].Value);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;
            Hero h = new Hero();
            h.HeroID = txtHeroID.Text.Trim();
            h.Name = txtName.Text.Trim();
            h.Age = int.Parse(txtAge.Text.Trim());
            h.Superpower = txtSuperpower.Text.Trim();
            h.Score = (int)nudExamScore.Value;

            try
            {
                manager.UpdateHero(h);
                RefreshGrid();
                ClearInputs();
                MessageBox.Show("Updated hero.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating hero: " + ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string id = txtHeroID.Text.Trim();
            if (string.IsNullOrWhiteSpace(id))
            {
                MessageBox.Show("Enter a Hero ID to delete (or select row).");
                return;
            }
            try
            {
                manager.DeleteHero(id);
                RefreshGrid();
                ClearInputs();
                MessageBox.Show("Deleted hero.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting hero: " + ex.Message);
            }
        }

        private void btnGenerateReport_Click(object sender, EventArgs e)
        {
            try
            {
                string report = manager.GenerateSummary();
                // Save to summary.txt too
                File.WriteAllText("summary.txt", report);
                MessageBox.Show(report, "Summary Report");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error generating report: " + ex.Message);
            }
        }

        // CSV Upload: expects rows: HeroID,Name,Age,Superpower,Score  (5 columns)
        private void btnUploadFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "CSV files (*.csv)|*.csv|Text files (*.txt)|*.txt";
            ofd.Title = "Select a superhero data file";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string[] lines = File.ReadAllLines(ofd.FileName);
                    if (lines.Length > 0)
                    {
                        dgvSuperheroes.Rows.Clear();
                        for (int i = 1; i < lines.Length; i++)
                        {
                            var values = lines[i].Split(',');
                            dgvSuperheroes.Rows.Add(
                                values.ElementAtOrDefault(0),
                                values.ElementAtOrDefault(1),
                                values.ElementAtOrDefault(2),
                                values.ElementAtOrDefault(3),
                                values.ElementAtOrDefault(4),
                                values.ElementAtOrDefault(5),
                                values.ElementAtOrDefault(6)
                            );
                        }
                    }
                    MessageBox.Show("File loaded successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error reading file: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void LoadHeroesFromDB()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connString))
                {
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Superheroes", con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvSuperheroes.Rows.Clear();
                    foreach (DataRow row in dt.Rows)
                    {
                        dgvSuperheroes.Rows.Add(row.ItemArray);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("DB Load error: " + ex.Message);
            }
        }

    }
}
