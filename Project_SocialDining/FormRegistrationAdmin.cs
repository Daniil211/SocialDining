﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using LB5_1._Database;
using System.Drawing.Imaging;
using System.Data.SqlClient;

namespace LB5_1
{
    public partial class FormRegistrationAdmin : Form
    {
        public FormRegistrationAdmin()
        {
            InitializeComponent();
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

        private void button2_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image Files (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    pictureBoxPhoto.SizeMode = PictureBoxSizeMode.StretchImage;
                    pictureBoxPhoto.Image = Image.FromFile(dialog.FileName);
                    pictureBoxPhoto.Width = 300;
                    pictureBoxPhoto.Height = 400;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message);
                }
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            using (DataContext db = new DataContext())
            {
                try
                {
                    if (!string.IsNullOrWhiteSpace(textBoxLog.Text) && !string.IsNullOrWhiteSpace(textBoxPas.Text) && !string.IsNullOrWhiteSpace(textBoxQual.Text))
                    {
                        if (textBoxPas.Text.Length < 3)
                        {
                            MessageBox.Show("Пароль должен содержать не менее 3 символов");
                            return;
                        }
                        if (db.Musicians.Any(u => u.FirstName == textBoxLog.Text))
                        {
                            MessageBox.Show("Пользователь с таким логином уже существует");
                            return;
                        }
                        Admin user = new Admin
                        {
                            FirstName = textBoxLog.Text,
                            LastName = textBoxLN.Text,
                            MiddleName = textBoxMN.Text,
                            Phone = textBoxPhone.Text,
                            Password = GetHashString(textBoxPas.Text),
                            Qualification = textBoxQual.Text,
                            Photo = GetImageBytes(pictureBoxPhoto.Image),
                        };
                        db.Musicians.Add(user);
                        db.SaveChanges();
                        MessageBox.Show("Аккаунт " + textBoxLog.Text + " зарегистрирован");
                        textBoxLog.Clear();
                        textBoxPas.Clear();
                        textBoxQual.Clear();
                        pictureBoxPhoto.Image = null;
                        FormAuthorization form = new FormAuthorization();
                        this.Hide();
                        form.Show();
                    }
                    else
                    {
                        MessageBox.Show("Заполните все поля");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message);
                }
            }
        }
    }
}

