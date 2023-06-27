using LB5_1._Database;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;

namespace LB5_1
{
    public partial class FormComboSetInformations : Form
    {
        private СomboSet project;
        string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=StudioRecordi;Integrated Security=True";
        public FormComboSetInformations(СomboSet project)
        {
            InitializeComponent();
            this.project = project;
            LoadProject();
            LoadItems();
        }

        private void LoadProject()
        {
            titleLabel.Text = project.Name;
            authorLabel.Text = project.Genre;
            yearLabel.Text = project.СaloricСontent.ToString();
            if (project.Cover != null)
            {
                using (var ms = new MemoryStream(project.Cover))
                {
                    imagePictureBox.Image = Image.FromStream(ms);
                }
            }
        }
        private void LoadItems()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT * FROM dbo.Items WHERE DiningRoom_Id = @DiningRoom_Id", connection);
                command.Parameters.AddWithValue("@DiningRoom_Id", project.Id);
                var adapter = new SqlDataAdapter(command);
                var table = new DataTable();
                adapter.Fill(table);
                dataGridView1.DataSource = table;
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
