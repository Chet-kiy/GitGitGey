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
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            this.passField.AutoSize = false;
            this.loginField.AutoSize = false;
            this.loginField.Size = new Size(loginField.Size.Width, 48);
            this.passField.Size = new Size(passField.Size.Width, 48);

            this.loginField.Text = "Введите логин";
            this.loginField.ForeColor = Color.Gray;

            this.passField.Text = "Введите пароль";
            this.passField.UseSystemPasswordChar = false;
            this.passField.ForeColor = Color.Gray;
        }
        private string HashPass(string text)
        {
            text += "ecoding";
            byte[] data = Encoding.Default.GetBytes(text);
            var result = new SHA256Managed().ComputeHash(data);
            return BitConverter.ToString(result).Replace("-", "").ToLower();
        }

        public string UserID;
        private void passField_Enter(object sender, EventArgs e)
        {
            if (this.passField.Text == "Введите пароль")
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
                this.passField.Text = "Введите пароль";
                this.passField.UseSystemPasswordChar = false;
                this.passField.ForeColor = Color.Gray;
            }
        }

        private void loginField_Enter(object sender, EventArgs e)
        {
            if (this.loginField.Text == "Введите логин")
            {
                this.loginField.Text = "";
                this.loginField.ForeColor = Color.Black;
            }
        }
        private void loginField_Leave(object sender, EventArgs e)
        {
            if (this.loginField.Text == "")
            {
                this.loginField.Text = "Введите логин";
                this.loginField.ForeColor = Color.Gray;
            }           
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

        private void registerLabel_MouseEnter(object sender, EventArgs e)
        {
            this.registerLabel.ForeColor = Color.Red;
        }

        private void registerLabel_MouseLeave(object sender, EventArgs e)
        {
            this.registerLabel.ForeColor = Color.Silver;
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

        public void ButtonLogin_Click(object sender, EventArgs e)
        {
            string loginUser = loginField.Text;
            string passUser = HashPass(passField.Text);

            DB db = new DB();

            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command = new MySqlCommand("SELECT * FROM `users` WHERE `login` = @uL AND `pass` = @uP", db.getConnection());
            command.Parameters.Add("@uL", MySqlDbType.VarChar).Value = loginUser;
            command.Parameters.Add("@uP", MySqlDbType.VarChar).Value = passUser;

            adapter.SelectCommand = command;
            adapter.Fill(table);  

            if (table.Rows.Count > 0)
            {
                foreach (DataRow dr in table.Rows)
                {
                    object ID = dr.ItemArray[0];
                    UserID = Convert.ToString(ID);
                    
                }
                MessageBox.Show("Вы успешно авторизировались", "message");
                MainForm mainform = new MainForm();
                mainform.IdUser = UserID;
                mainform.Show();
                this.Hide();
            }  
            else
                MessageBox.Show("Неверные имя пользователя или пароль","message");
        }

        private void registerLabel_Click(object sender, EventArgs e)
        {
            RegisterForm registerForm = new RegisterForm();          
            registerForm.Show();
            this.Hide();
        }     
    }
}
