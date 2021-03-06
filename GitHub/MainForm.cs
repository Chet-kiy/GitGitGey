﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GitHub
{
    public partial class MainForm : Form
    {
        
        public MainForm()
        {
            InitializeComponent();

            this.passField.AutoSize = false;
            this.nameField.AutoSize = false;

            this.nameField.Size = new Size(nameField.Size.Width, 48);
            this.passField.Size = new Size(passField.Size.Width, 48);

            this.nameField.Text = "name";
            this.nameField.ForeColor = Color.Gray;

            this.passField.Text = "pass";
            this.passField.ForeColor = Color.Gray;

            this.descriptionField.Text = "description";
            this.descriptionField.ForeColor = Color.Gray;

            
        }
        public string IdUser;
        DataTable table;
        private void LoadDataUsersInfo(string IdUser)
        {
            DB db = new DB();

            table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command = new MySqlCommand("SELECT * FROM `infousers` WHERE `idUser` = @id", db.getConnection());
            command.Parameters.Add("@id", MySqlDbType.VarChar).Value = IdUser;

            adapter.SelectCommand = command;
            adapter.Fill(table);

            DataInfoUsers.Rows.Clear();
            int num = 1;
            foreach (DataRow dr in table.Rows)
            {
                object crypt_name = dr.ItemArray[1];
                object crypt_pass = dr.ItemArray[2];
                object crypt_description = dr.ItemArray[3];

                string name = db.decrypt((string) crypt_name);
                string pass = db.decrypt((string) crypt_pass);
                string description = db.decrypt((string) crypt_description);
                
                DataInfoUsers.Rows.Add(num, name, pass, description);
                num++;
                
            }
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void closeButton_MouseEnter(object sender, EventArgs e)
        {
            closeButton.ForeColor = Color.Red;
        }

        private void closeButton_MouseLeave(object sender, EventArgs e)
        {
            closeButton.ForeColor = Color.Silver;
        }
        Point lastpoint;

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastpoint.X;
                this.Top += e.Y - lastpoint.Y;
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            lastpoint = new Point(e.X, e.Y);
        }

        private void panel2_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastpoint.X;
                this.Top += e.Y - lastpoint.Y;
            }
        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            lastpoint = new Point(e.X, e.Y);
        }

        private void LoadDataButton_Click(object sender, EventArgs e)
        {
            LoadDataUsersInfo(IdUser);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            LoginForm loginform = new LoginForm();           
            loginform.Show();
            this.Close();
            GC.Collect();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            string name = nameField.Text;
            string pass = passField.Text;
            string description = descriptionField.Text;

            if (name == "" || name == "name")
            {
                MessageBox.Show("Введите название", "message");
                return;
            }
            if (pass == "" || pass == "pass")
            {
                MessageBox.Show("Введите пароль", "message");
                return;
            }
            

            DB db = new DB();
            string crypt_name = db.encrypt(name);
            string crypt_pass = db.encrypt(pass);
            string crypt_description = db.encrypt(description);
            

            MySqlCommand command = new MySqlCommand("INSERT INTO `infousers` (`name`, `password`, `Description`, `idUser`) VALUES(@name, @pass, @desc, @id)", db.getConnection());
            command.Parameters.Add("@name", MySqlDbType.VarChar).Value = crypt_name;
            command.Parameters.Add("@pass", MySqlDbType.VarChar).Value = crypt_pass;
            command.Parameters.Add("@desc", MySqlDbType.VarChar).Value = crypt_description;
            command.Parameters.Add("@id", MySqlDbType.Int32).Value = IdUser;

            db.openConnection();

            if (command.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Данные обновлены", "message");
                nameField.Text = "name";
                nameField.ForeColor = Color.Gray;
                passField.Text = "pass";
                passField.ForeColor = Color.Gray;
                descriptionField.Text = "description";
                descriptionField.ForeColor = Color.Gray;

                LoadDataUsersInfo(IdUser);


            }
            else
                MessageBox.Show("Ошибка", "message");

            db.closeConnection();
        }

        private void nameField_Enter(object sender, EventArgs e)
        {
            if (this.nameField.Text == "name")
            {
                this.nameField.Text = "";
                this.nameField.ForeColor = Color.Black;
            }
        }

        private void nameField_Leave(object sender, EventArgs e)
        {
            if (this.nameField.Text == "")
            {
                this.nameField.Text = "name";
                this.nameField.ForeColor = Color.Gray;
            }
        }

        private void passField_Enter(object sender, EventArgs e)
        {
            if (this.passField.Text == "pass")
            {
                this.passField.Text = "";
                this.passField.ForeColor = Color.Black;
            }
        }

        private void passField_Leave(object sender, EventArgs e)
        {
            if (this.passField.Text == "")
            {
                this.passField.Text = "pass";
                this.passField.ForeColor = Color.Gray;
            }
        }

        private void descriptionField_Enter(object sender, EventArgs e)
        {
            if (this.descriptionField.Text == "description")
            {
                this.descriptionField.Text = "";
                this.descriptionField.ForeColor = Color.Black;
            }
        }

        private void descriptionField_Leave(object sender, EventArgs e)
        {
            if (this.descriptionField.Text == "")
            {
                this.descriptionField.Text = "description";
                this.descriptionField.ForeColor = Color.Gray;
            }
        }

        private void downButton_Click(object sender, EventArgs e)
        {
            if (panel3.Enabled == false)
            {
                panel3.Enabled = true;
                panel3.Visible = true;
                downButton.Visible = false;
                downButton.Enabled = false;
                upButton.Visible = true;
                upButton.Enabled = true;
            }
        }

        private void upButton_Click(object sender, EventArgs e)
        {
            if (panel3.Enabled == true)
            {
                panel3.Enabled = false; ;
                panel3.Visible = false; ;
                downButton.Visible = true;
                downButton.Enabled = true;
                upButton.Visible = false;
                upButton.Enabled = false;
            }
        }

        private void DataInfoUsers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4)
            {
                if (MessageBox.Show("Вы действительно хотите удалить запись?","message",MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int rowID = e.RowIndex;
                    DB db = new DB();
                    string name = db.encrypt((string)DataInfoUsers[1, rowID].Value);
                    string pass = db.encrypt((string)DataInfoUsers[2, rowID].Value);
                    string description = db.encrypt((string)DataInfoUsers[3, rowID].Value);

                    MySqlCommand command = new MySqlCommand("DELETE FROM `infousers` WHERE `infousers`.`name` = @name AND `infousers`.`password` = @pass AND `infousers`.`Description` = @desc", db.getConnection());
                    command.Parameters.Add("@name", MySqlDbType.VarChar).Value = name;
                    command.Parameters.Add("@pass", MySqlDbType.VarChar).Value = pass;
                    command.Parameters.Add("@desc", MySqlDbType.VarChar).Value = description;

                    db.openConnection();

                    if (command.ExecuteNonQuery() == 1)
                    {
                        MessageBox.Show("Запись удалена", "message");
                        LoadDataUsersInfo(IdUser);
                    }

                    db.closeConnection();
                }
                
            }
        }
    }

}
