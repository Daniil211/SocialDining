using LB5_1._Database;
using System.Diagnostics;

namespace LB5_1
{
    public partial class FormComboSetInformations : Form
    {
        private СomboSet project;

        public FormComboSetInformations(СomboSet project)
        {
            InitializeComponent();
            this.project = project;
            LoadProject();
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

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
