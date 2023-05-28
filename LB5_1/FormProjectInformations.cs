using LB5_1._Database;
using System.Diagnostics;

namespace LB5_1
{
    public partial class FormProjectInformations : Form
    {
        private Project project;

        public FormProjectInformations(Project project)
        {
            InitializeComponent();
            this.project = project;
            LoadProject();
        }

        private void LoadProject()
        {
            titleLabel.Text = project.Name;
            authorLabel.Text = project.Genre;
            yearLabel.Text = project.Duration.ToString();
            if (project.Cover != null)
            {
                using (var ms = new MemoryStream(project.Cover))
                {
                    imagePictureBox.Image = Image.FromStream(ms);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
