using LB5_1._Database;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace LB5_1
{
    public partial class FormDiningRoom : Form
    {
        public FormDiningRoom()
        {
            using (DataContext db = new DataContext())
            {
                InitializeComponent();
                db.Studios.Load();
                dataGridView1.DataSource = db.Studios.Local.ToBindingList();
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            using (DataContext db = new DataContext())
            {
                try
                {
                    if (!string.IsNullOrWhiteSpace(textBox1.Text) && !string.IsNullOrWhiteSpace(textBox2.Text) && !string.IsNullOrWhiteSpace(textBox3.Text) && !string.IsNullOrWhiteSpace(textBox4.Text))
                    {
                        DiningRoom Studio = new DiningRoom
                        {
                            Address = textBox1.Text,
                            Time = textBox2.Text,
                            Phone = textBox3.Text,
                            Email = textBox4.Text,
                        };
                        db.Studios.Load();
                        dataGridView1.DataSource = db.Studios.Local.ToBindingList();
                        db.Studios.Add(Studio);
                        db.SaveChanges();

                        MessageBox.Show("Запись успешно добавлена");
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
