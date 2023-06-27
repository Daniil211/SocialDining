using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Mail;
using System.Net;
using System.Data.Entity;
using LB5_1._Database;
using System.Security.Cryptography;

namespace LB5_1
{
    public partial class FormRecovery : Form
    {
        private User currentUser;

        public FormRecovery(User user)
        {
            InitializeComponent();
            this.currentUser = user;
        }
        private void buttonRecover_Click(object sender, EventArgs e)
        {
            using (DataContext db = new DataContext())
            {
                try
                {
                    string email = textBoxEmail.Text;
                    if (string.IsNullOrEmpty(email))
                    {
                        MessageBox.Show("Введите email");
                        return;
                    }
                    User user = db.Users.FirstOrDefault(u => u.Email == email);
                    if (user == null)
                    {
                        MessageBox.Show("Пользователь с таким email не найден");
                        return;
                    }
                    if (user.Id != currentUser.Id)
                    {
                        MessageBox.Show("Вы не можете восстановить пароль для другого пользователя");
                        return;
                    }
                    string newPassword = GeneratePassword();
                    user.Password = GetHashString(newPassword);
                    db.SaveChanges();
                    textBoxNewPas.Text = newPassword;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message);
                }
            }
        }
        private string GeneratePassword()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();
            return new string(Enumerable.Repeat(chars, 8).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private string GetHashString(string input)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        private void btnAuth_Click(object sender, EventArgs e)
        {
            var form = new FormAuthorization();
            this.Hide();
            form.Show();
        }
    }
}
