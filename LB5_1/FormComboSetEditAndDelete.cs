using LB5_1._Database;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LB5_1
{
    public partial class FormComboSetEditAndDelete : Form
    {
        DbContext db;
        public FormComboSetEditAndDelete()
        {
            using (DataContext db = new DataContext())
            {
                InitializeComponent();
                db.Projects.Load();
                dataGridView1.DataSource = db.Projects.Local.ToBindingList();
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            using (DataContext db = new DataContext())
            {
                try
                {
                    if (dataGridView1.SelectedRows.Count > 0)
                    {
                        int index = dataGridView1.SelectedRows[0].Index;
                        int id = 0;
                        bool converted = Int32.TryParse(dataGridView1[0, index].Value.ToString(), out id);
                        if (converted == false)
                            return;
                        СomboSet Project = db.Projects.Find(id);
                        db.Projects.Remove(Project);
                        db.SaveChanges();
                        db.Projects.Load();
                        dataGridView1.DataSource = db.Projects.Local.ToBindingList();
                        MessageBox.Show("Deleted");
                    }
                }
                catch
                {
                    MessageBox.Show("Error");
                }
            }
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            using (DataContext db = new DataContext())
            {
                try
                {
                    int index = dataGridView1.SelectedRows[0].Index;
                    int id = 0;
                    bool converted = Int32.TryParse(dataGridView1[0, index].Value.ToString(), out id);
                    if (converted == false)
                        return;
                    СomboSet Project = db.Projects.Find(id);
                    FormEditComboSet ordForm = new FormEditComboSet();
                    ordForm.textBoxTitle.Text = Project.Name;
                    ordForm.textBoxGenre.Text = Project.Genre;
                    ordForm.textBoxDr.Text = Project.СaloricСontent.ToString();
                    //ordForm.textBoxText.Text = Project.Musician_Id;
                    //ordForm.textBox1.Text = Project.Record_Id;
                    //ordForm.textBox2.Text = Project.User_Id;

                    DialogResult result = ordForm.ShowDialog(this);
                    if (result == DialogResult.Cancel)
                        return;

                    Project.Name = ordForm.textBoxTitle.Text;
                    Project.Genre = ordForm.textBoxGenre.Text;
                    Project.СaloricСontent = Convert.ToInt32(ordForm.textBoxDr.Text);
                    //Project.Musician_Id = ordForm.textBoxText.Text;
                    //Project.Record_Id = ordForm.textBox1.Text;
                    //Project.User_Id = ordForm.textBox2.Text;


                    db.SaveChanges();
                    db.Projects.Load();
                    dataGridView1.DataSource = db.Projects.Local.ToBindingList();
                    dataGridView1.Refresh();
                    MessageBox.Show("DB is refreshed");
                }
                catch
                {
                    MessageBox.Show("Сhoose row");
                }
            }
        }

        private void buttonHide_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
