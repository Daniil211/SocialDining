using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LB5_1._Database;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace LB5_1
{
    public partial class FormAdminInformation : Form
    {
        private Musician user;
        public FormAdminInformation(Musician user)
        {
            InitializeComponent();
            this.user = user;
            LoadUserData();
            textBoxLogin.ReadOnly = true;
            textBoxEmail.ReadOnly = true;
        }

        private void LoadUserData()
        {
            // Заполняем элементы управления данными о пользователе
            textBoxLogin.Text = user.FirstName;
            textBoxEmail.Text = user.Phone;

            if (user.Photo != null)
            {
                using (var ms = new MemoryStream(user.Photo))
                {
                    var image = Image.FromStream(ms);
                    var thumbnail = image.GetThumbnailImage(300, 400, null, IntPtr.Zero);
                    pictureBox1.Image = thumbnail;
                }
            }
            else
            {
                var testImage = System.Drawing.Image.FromFile("C:\\Users\\id202\\Desktop\\Важные репозитории\\LB5_1\\LB5_1\\_Image\\profiles.png");
                var thumbnail = testImage.GetThumbnailImage(300, 400, null, IntPtr.Zero);
                pictureBox1.Image = thumbnail;
            }
        }

    }
}
