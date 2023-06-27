using LB5_1._Database;
using LB5_1.Models_UI;
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
    public partial class FormItem : Form
    {
        public FormItem()
        {
            using (DataContext db = new DataContext())
            {
                InitializeComponent();
                db.Records.Load();
                dataGridView1.DataSource = db.Records.Local.ToBindingList();
            }

            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=StudioRecordi;Integrated Security=True";

            #region ComboBox for ComboSet
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Заполнение ComboBox данными из таблицы "ComboSets"
                string query = "SELECT Id, Name FROM dbo.СomboSet";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int id = (int)reader["Id"];
                    string name = (string)reader["Name"];
                    comboBox1.Items.Add(new ComboBoxItem(id, name));
                }
                reader.Close();
            }
            #endregion
            #region DataGrid for SqlTable
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Заполнение DataGridView данными из таблицы "ComboSets"
                string query = "SELECT dbo.Items.Id, dbo.Items.Name, dbo.Items.Date, dbo.Items.Duration, dbo.Items.Price, dbo.СomboSet.Name AS Name_Combo, dbo.DiningRooms.Address FROM dbo.DiningRooms INNER JOIN dbo.Items ON dbo.DiningRooms.Id = dbo.Items.DiningRoom_Id INNER JOIN dbo.СomboSet ON dbo.Items.СomboSet_Id = dbo.СomboSet.Id";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable table = new DataTable();
                adapter.Fill(table);
                dataGridView1.DataSource = table;
            }
            #endregion
            #region ComboBox for DiningRoom
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT Id, Address FROM dbo.DiningRooms";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int id = (int)reader["Id"];
                    string name = (string)reader["Address"];
                    comboBox2.Items.Add(new ComboBoxItem(id, name));
                }
                reader.Close();
            }
            #endregion
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (DataContext db = new DataContext())
            {
                try
                {
                    if (!string.IsNullOrWhiteSpace(txtDuration.Text) && !string.IsNullOrWhiteSpace(txtPrice.Text) && !string.IsNullOrWhiteSpace(txtNumber.Text))
                    {
                        Item item = new Item();
                        int duration = int.Parse(txtDuration.Text);
                        decimal price = decimal.Parse(txtPrice.Text);
                        string name = txtNumber.Text;
                        int diningRoomId = int.Parse(comboBox1.SelectedValue.ToString());
                        DiningRoom diningRoom = db.Studios.Find(diningRoomId);
                        item.DiningRoom = diningRoom;

                        Item record = new Item
                        {
                            Date = dtpDate.Value,
                            Duration = duration,
                            Price = price,
                            Name = name,
                            DiningRoom = item.DiningRoom,
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
