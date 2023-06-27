using LB5_1._Database;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Data.Entity;

namespace LB5_1
{
    public partial class FormComboSet : Form
    {
        public FormComboSet()
        {
            using (DataContext db = new DataContext())
            {
                InitializeComponent();
                db.Musicians.Load();
            }
        }

        private void btnPhoto_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image Files (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                    pictureBox1.Image = Image.FromFile(dialog.FileName);
                    pictureBox1.Size = new Size(100, 140);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message);
                }
            }
        }

        private byte[] GetImageBytes(Image image)
        {
            if (image == null)
            {
                return null;
            }
            using (Bitmap bitmap = new Bitmap(image, new Size(300, 400)))
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    bitmap.Save(stream, ImageFormat.Jpeg);
                    return stream.ToArray();
                }
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            using (DataContext db = new DataContext())
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(textBoxTitle.Text) || string.IsNullOrWhiteSpace(textBoxDr.Text) || string.IsNullOrWhiteSpace(textBoxGenre.Text))
                    {
                        MessageBox.Show("Заполните все поля");
                        return;
                    }
                    if (pictureBox1.Image == null)
                    {
                        MessageBox.Show("Выберите изображение");
                        return;
                    }
                    СomboSet project = new СomboSet
                    {
                        Name = textBoxTitle.Text,
                        СaloricСontent = Convert.ToInt32(textBoxDr.Text),
                        Genre = textBoxGenre.Text,
                        Cover = GetImageBytes(pictureBox1.Image),
                    };

                    db.Projects.Add(project);
                    db.SaveChanges();
                    MessageBox.Show($"Комбо-сет {textBoxTitle.Text} добавлен");
                    textBoxTitle.Clear();
                    textBoxDr.Clear();
                    textBoxGenre.Clear();
                    pictureBox1.Image = null;
                    Hide();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
