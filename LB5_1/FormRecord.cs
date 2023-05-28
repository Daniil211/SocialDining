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
    public partial class FormRecord : Form
    {
        public FormRecord()
        {
            using (DataContext db = new DataContext())
            {
                InitializeComponent();
                db.Records.Load();
                dataGridView1.DataSource = db.Records.Local.ToBindingList();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (DataContext db = new DataContext())
            {
                try
                {
                    if (!string.IsNullOrWhiteSpace(txtDuration.Text) && !string.IsNullOrWhiteSpace(txtPrice.Text) && !string.IsNullOrWhiteSpace(txtNumber.Text))
                    {
                        int duration = int.Parse(txtDuration.Text);
                        decimal price = decimal.Parse(txtPrice.Text);
                        int number = int.Parse(txtNumber.Text);
                        int studioId = int.Parse(textBox1.Text);

                        Record record = new Record
                        {
                            Date = dtpDate.Value,
                            Duration = duration,
                            Price = price,
                            Number = number,
                        };
                        db.Records.Load();
                        dataGridView1.DataSource = db.Records.Local.ToBindingList();
                        db.Records.Add(record);
                        db.SaveChanges();

                        MessageBox.Show("Запись успешно добавлена");
                        ClearForm();
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

        private void ClearForm()
        {
            dtpDate.Value = DateTime.Now;
            txtDuration.Text = "";
            txtPrice.Text = "";
            txtNumber.Text = "";
        }


    }
}
