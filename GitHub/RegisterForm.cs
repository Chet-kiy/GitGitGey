using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GitHub
{
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();

            this.passField.AutoSize = false;
            this.loginField.AutoSize = false;
            this.nameField.AutoSize = false;
            this.surnameField.AutoSize = false;
            this.loginField.Size = new Size(loginField.Size.Width, 48);
            this.passField.Size = new Size(passField.Size.Width, 48);
            this.nameField.Size = new Size(nameField.Size.Width, 48);
            this.surnameField.Size = new Size(surnameField.Size.Width, 48);

            this.loginField.Text = "login";
            this.loginField.ForeColor = Color.Gray;

            this.passField.Text = "password";
            this.passField.UseSystemPasswordChar = false;
            this.passField.ForeColor = Color.Gray;

            this.nameField.Text = "name";
            this.nameField.ForeColor = Color.Gray;

            this.surnameField.Text = "surname";
            this.surnameField.ForeColor = Color.Gray;
        }
        private string HashPass(string text)
        {
            text += "ecoding";
            byte[] data = Encoding.Default.GetBytes(text);
            var result = new SHA256Managed().ComputeHash(data);
            return BitConverter.ToString(result).Replace("-", "").ToLower();
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void closeButton_MouseEnter(object sender, EventArgs e)
        {
            this.closeButton.ForeColor = Color.Red;
        }

        private void closeButton_MouseLeave(object sender, EventArgs e)
        {
            this.closeButton.ForeColor = Color.Silver;
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

        private void loginField_Enter(object sender, EventArgs e)
        {
            if (this.loginField.Text == "login")
            {
                this.loginField.Text = "";
                this.loginField.ForeColor = Color.Black;
            }
        }

        private void loginField_Leave(object sender, EventArgs e)
        {
            if (this.loginField.Text == "")
            {
                this.loginField.Text = "login";
                this.loginField.ForeColor = Color.Gray;
            }
        }

        private void passField_Enter(object sender, EventArgs e)
        {
            if (this.passField.Text == "password")
            {
                this.passField.Text = "";
                this.passField.UseSystemPasswordChar = true;
                this.passField.ForeColor = Color.Black;
            }
        }

        private void passField_Leave(object sender, EventArgs e)
        {
            if (this.passField.Text == "")
            {
                this.passField.Text = "password";
                this.passField.UseSystemPasswordChar = false;
                this.passField.ForeColor = Color.Gray;
            }
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

        private void surnameField_Enter(object sender, EventArgs e)
        {
            if (this.surnameField.Text == "surname")
            {
                this.surnameField.Text = "";
                this.surnameField.ForeColor = Color.Black;
            }
        }

        private void surnameField_Leave(object sender, EventArgs e)
        {
            if (this.surnameField.Text == "")
            {
                this.surnameField.Text = "surname";
                this.surnameField.ForeColor = Color.Gray;
            }
        }

        private void ButtonRegister_Click(object sender, EventArgs e)
        {
            string login = loginField.Text;
            string pass = HashPass(passField.Text);
            string name = nameField.Text;
            string surname = surnameField.Text;

            if (loginField.Text == "login" || loginField.Text == "")
            {
                MessageBox.Show("Введите логин", "message");
                return;
            }
            if (passField.Text == "password" || passField.Text == "")
            {
                MessageBox.Show("Введите пароль", "message");
                return;
            }
            if (nameField.Text == "name" || nameField.Text == "")
            {
                MessageBox.Show("Введите имя","message");
                return;
            }
            if (surnameField.Text == "surname" || surnameField.Text == "")
            {
                MessageBox.Show("Введите фамилию", "message");
                return;
            }

            if (checkLogin())
                return;

            DB db = new DB();

            MySqlCommand command = new MySqlCommand("INSERT INTO `users` (`login`, `pass`, `name`, `surname`) VALUES (@login, @pass, @name, @sname)",db.getConnection());
            
            command.Parameters.Add("@login", MySqlDbType.VarChar).Value = login;
            command.Parameters.Add("@pass", MySqlDbType.VarChar).Value = pass;
            command.Parameters.Add("@name", MySqlDbType.VarChar).Value = name;
            command.Parameters.Add("@sname", MySqlDbType.VarChar).Value = surname;

            db.openConnection();

            if (command.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Вы создали аккаунт", "message");
                LoginForm loginForm = new LoginForm();
                loginForm.Show();
                this.Close();
            }
            else
                MessageBox.Show("Не удалось создать аккаунт","message");

            db.closeConnection();
        }
        public Boolean checkLogin()
        {
            string login = loginField.Text;

            foreach (char g in login)
            {
                if (!(char.IsLetterOrDigit(g)))
                {
                    MessageBox.Show("Вы ввели запрещенный символ", "message");
                    return true;
                }
            }

            DB db = new DB();

            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command = new MySqlCommand("SELECT * FROM `users` WHERE `login` = @uL", db.getConnection());
            command.Parameters.Add("@uL", MySqlDbType.VarChar).Value = login;

            adapter.SelectCommand = command;
            adapter.Fill(table);

            

            if (table.Rows.Count > 0)
            {
                MessageBox.Show("Такой логин уже есть", "message");
                return true;
            } 
            else
                return false;
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
            this.Close();
        }
    }
}
