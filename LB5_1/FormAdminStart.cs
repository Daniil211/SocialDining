using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LB5_1._Database;
using static System.Reflection.Metadata.BlobBuilder;
using System.IO;
using static System.Net.Mime.MediaTypeNames;

namespace LB5_1
{
    public partial class FormAdminStart : Form
    {
        private Musician currentUser;
        private List<Project> Project;
        public FormAdminStart(Musician musician)
        {
            InitializeComponent();
            this.currentUser = musician;
            LoadProject();

            // Создаем таймер
            var timer = new System.Windows.Forms.Timer();
            timer.Interval = 5000; // Интервал обновления в миллисекундах
            timer.Tick += timer1_Tick;
            timer.Start();

            if (currentUser.Photo != null)
            {
                using (var ms = new MemoryStream(currentUser.Photo))
                {
                    pictureBox1.Image = System.Drawing.Image.FromStream(ms);
                }
            }
            if (currentUser.Photo != null)
            {
                using (var ms = new MemoryStream(currentUser.Photo))
                {
                    var image = System.Drawing.Image.FromStream(ms);
                    var thumbnail = image.GetThumbnailImage(100, 100, null, IntPtr.Zero);
                    pictureBox1.Image = thumbnail;
                }
            }
            else
            {
                var testImage = System.Drawing.Image.FromFile("C:\\Users\\id202\\Desktop\\Важные репозитории\\LB5_1\\LB5_1\\_Image\\profiles.png");
                var thumbnail = testImage.GetThumbnailImage(100, 100, null, IntPtr.Zero);
                pictureBox1.Image = thumbnail;
            }

            pictureBox1.Click += (sender, e) =>
            {
                // Создаем новую форму для отображения данных о пользователе
                var userForm = new FormAdminInformation(currentUser);
                userForm.ShowDialog();
            };
        }


        private void buttonExit_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }
        private void LoadProject()
        {
            using (var db = new DataContext())
            {
                Project = db.Projects.ToList();
            }

            // Очищаем flowLayoutPanel1
            flowLayoutPanel1.Controls.Clear();

            foreach (var Project in Project)
            {
                var card = new Panel();
                card.BorderStyle = BorderStyle.FixedSingle;
                card.Margin = new Padding(10);
                card.Width = 200;
                card.Height = 250;

                var image = new PictureBox();
                image.SizeMode = PictureBoxSizeMode.StretchImage;
                image.Width = 180;
                image.Height = 180;
                image.Top = 10;
                image.Left = 10;
                if (Project.Cover != null)
                {
                    using (var ms = new MemoryStream(Project.Cover))
                    {
                        image.Image = System.Drawing.Image.FromStream(ms);
                    }
                }
                card.Controls.Add(image);

                var title = new Label();
                title.Text = Project.Name;
                title.Width = 180;
                title.Top = 200;
                title.Left = 10;
                card.Controls.Add(title);

                var author = new Label();
                author.Text = Project.Genre;
                author.Width = 180;
                author.Top = 220;
                author.Left = 10;
                card.Controls.Add(author);

                card.Click += (sender, e) =>
                {
                    var form = new FormProjectInformations(Project);
                    form.ShowDialog();
                };

                flowLayoutPanel1.Controls.Add(card);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormProjects form = new FormProjects();
            form.ShowDialog();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            LoadProject();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FormProjectEditAndDelete form = new FormProjectEditAndDelete();
            form.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FormStudio form = new FormStudio();
            form.ShowDialog();
        }
    }
}
