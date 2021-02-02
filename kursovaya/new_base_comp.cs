using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kursovaya
{
    public partial class new_base_comp : Form
    {
        public new_base_comp()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            arch2.Columns.Add("ID", "ID");
            arch2.Columns.Add("First_name", "Имя");
            arch2.Columns.Add("Second_name", "Фамилия");
            arch2.Columns.Add("Date", "Дата рождения");
            arch2.Columns.Add("Number", "Номер телефона");
            arch2.Columns.Add("character1", "Характер знакомства");
            arch2.Columns.Add("Place_of_work", "Место работы");
            arch2.Columns.Add("Adress", "Адрес");
            arch2.Columns.Add("Post", "Должность");
            arch2.Columns.Add("Business_qualities", "Рабочие качества");
            arch2.Columns.Add("regt", "Дата изменения");

            date_text.Text = DateTime.Today.ToString("dd.MM");
        }



        private void button_backtomenu2_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;


        }




        /// КНОПКА ДОБАВЛЕНИЯ КОНТАКТА
        /// 

        private void add_data_button_Click(object sender, EventArgs e)
        {

            DateBase db = new DateBase(); //выделяем память под объект 
            MySqlCommand command = new MySqlCommand("INSERT INTO `users` (`First_name`, `Second_name`, `Date`, `Number`, `character1`, `Place_of_work`, `Adress`, `Post`, `Business_qualities`, `regt`) VALUES (@name1, @surname, @data, @num, @char2,@work,@adres,@post2,@buz,@reg)", db.getConnection());
            command.Parameters.Add("@name1", MySqlDbType.VarChar).Value = name_textbox.Text;
            command.Parameters.Add("@surname", MySqlDbType.VarChar).Value = surname_textbox.Text;
            command.Parameters.Add("@data", MySqlDbType.VarChar).Value = date_textbox.Text;
            command.Parameters.Add("@num", MySqlDbType.VarChar).Value = num_textbox.Text;
            command.Parameters.Add("@char2", MySqlDbType.VarChar).Value = character_textbox.Text;

            command.Parameters.Add("@work", MySqlDbType.VarChar).Value = place_f_w_textbox.Text;
            command.Parameters.Add("@adres", MySqlDbType.VarChar).Value = adress_textbox.Text;

            command.Parameters.Add("@post2", MySqlDbType.VarChar).Value = post_textbox.Text;

            command.Parameters.Add("@buz", MySqlDbType.VarChar).Value = bus_qual_textbox.Text;
            command.Parameters.Add("@reg", MySqlDbType.VarChar).Value = time_reg_textbox.Text;
            db.OpenConnetion();

            if (command.ExecuteNonQuery() == 1) //проверка на корректность работы
            {
                if (time_reg_textbox.Text == " " || name_textbox.Text == " " || surname_textbox.Text == " " && date_textbox.Text == " " && num_textbox.Text == " " && character_textbox.Text == " " && place_f_w_textbox.Text == " " && adress_textbox.Text == " " && post_textbox.Text == " " && bus_qual_textbox.Text == " ")
                {
                    MessageBox.Show("Введены не все данные");

                }
                MessageBox.Show("Контакт был добавлен в базу данных"); //проверка на корректность работы


            }
            else
                MessageBox.Show("Контакт НЕ был добавлен в базу данных");



        }





        /// КНОПКА УДАЛЕНИЯ 
        /// 

        private void del_data_button_Click(object sender, EventArgs e)
        {
            DateBase db = new DateBase(); //выделяем память под объект 

            DataTable table = new DataTable(); //табличка, в которой можно работать 

            MySqlDataAdapter adapter = new MySqlDataAdapter(); //adapter позволяет выбрать данные из базы данных


            MySqlCommand command = new MySqlCommand("SELECT `First_name` FROM `users` WHERE `First_name` = @fn ", db.getConnection());

            command.Parameters.Add("@fn", MySqlDbType.VarChar).Value = name_textbox.Text;

            //заполняем table заданной sql командой
            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count > 0 && name_textbox.Text != "") //если есть такой индекс
            {

                MySqlCommand command2 = new MySqlCommand("DELETE FROM `users` WHERE `users`.`First_name` = @fn", db.getConnection());

                command2.Parameters.Add("@fn", MySqlDbType.VarChar).Value = name_textbox.Text;

                db.OpenConnetion();

                if (command2.ExecuteNonQuery() == 1) //проверка на корректность работы
                {
                    MessageBox.Show("Контакт был УДАЛЕН из базы данных"); //проверка на корректность работы


                }
                else
                    MessageBox.Show("Контакт НЕ был удален из базы данных");


                db.CloseConnetion(); //если не закрыть, будет большая нагрузка
            }
            else
                MessageBox.Show("УДАЛЯТЬ НЕЧЕГО");
        }


        /// КНОПКА  ЗАМЕНЫ КОНТАКТА
        /// 
        private void change_data_button_Click(object sender, EventArgs e)
        {

            DateBase db = new DateBase();
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("SELECT `First_name` FROM `users` WHERE `First_name` = @name AND `Second_name` = @surname", db.getConnection());

            command.Parameters.Add("@name", MySqlDbType.VarChar).Value = name_textbox.Text;
            command.Parameters.Add("@surname", MySqlDbType.VarChar).Value = surname_textbox.Text;

            adapter.SelectCommand = command;
            adapter.Fill(table);


            if (table.Rows.Count > 0 && (date_textbox.Text != "" || num_textbox.Text != "" || character_textbox.Text != "" || place_f_w_textbox.Text != "" || adress_textbox.Text != "" || post_textbox.Text != "" || bus_qual_textbox.Text != "" && time_reg_textbox.Text != ""))
            {
                MySqlCommand command2 = new MySqlCommand("UPDATE `users` SET `First_name` = @name1, `Second_name`=@surname1, `Date`=@data, `Number`=@num, `character1`=@char2, `Place_of_work` = @work, `Adress` = @adres, `Post` = @post2, `Business_qualities` =@buz, `regt` = @reg WHERE  `First_name` = @name1", db.getConnection());

                command2.Parameters.Add("@name1", MySqlDbType.VarChar).Value = name_textbox.Text;
                command2.Parameters.Add("@surname1", MySqlDbType.VarChar).Value = surname_textbox.Text;
                command2.Parameters.Add("@data", MySqlDbType.VarChar).Value = date_textbox.Text;
                command2.Parameters.Add("@num", MySqlDbType.VarChar).Value = num_textbox.Text;
                command2.Parameters.Add("@char2", MySqlDbType.VarChar).Value = character_textbox.Text;

                command2.Parameters.Add("@work", MySqlDbType.VarChar).Value = place_f_w_textbox.Text;
                command2.Parameters.Add("@adres", MySqlDbType.VarChar).Value = adress_textbox.Text;

                command2.Parameters.Add("@post2", MySqlDbType.VarChar).Value = post_textbox.Text;

                command2.Parameters.Add("@buz", MySqlDbType.VarChar).Value = bus_qual_textbox.Text;
                command2.Parameters.Add("@reg", MySqlDbType.VarChar).Value = time_reg_textbox.Text;

                db.OpenConnetion();


                if (command2.ExecuteNonQuery() == 1) //проверка на корректность работы
                {
                    MessageBox.Show("Контакт был изменен");

                }
                else
                    MessageBox.Show("Контакт НЕ был изменен");

                db.CloseConnetion(); //если не закрыть, будет большая нагрузка
            }
            else // если нет
            {
                MessageBox.Show("ТАКОГО Контакта НЕТ ИЛИ ВЫ НИЧЕГО НЕ ИЗМЕНИЛИ");
            }

        }


        /// КНОПКА ОТКРЫТИЯ КОНТАКТОВ
        /// 
        private void button1_Click(object sender, EventArgs e)
        {


            DateBase db = new DateBase();
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command = new MySqlCommand("SELECT * FROM `users`  ", db.getConnection());
            adapter.SelectCommand = command;
            adapter.Fill(table);
            foreach (DataRow row in table.Rows)
                arch2.Rows.Clear();

            foreach (DataRow row in table.Rows)
                arch2.Rows.Add(row.ItemArray);

            adapter.SelectCommand = command;
            adapter.Fill(table);



        }

        // КНОПКА ПОИСКА
        private void find_button1_Click(object sender, EventArgs e)
        {

            String name_panel3 = name_textbox.Text;
            String surname_panel3 = surname_textbox.Text;



            DateBase db = new DateBase(); //выделяем память под объект 

            DataTable table = new DataTable(); //табличка, в которой можно работать 

            MySqlDataAdapter adapter = new MySqlDataAdapter(); //adapter позволяет выбрать данные из базы данных


            //находим наличие по коду и имени
            MySqlCommand command = new MySqlCommand("SELECT `ID`, `First_Name`, `Second_Name`, `Date`, `Number`, `character1`, `Place_of_work`, `Adress`, `Post`, `Business_qualities` FROM `users` WHERE `Second_name` = @surname OR `First_name`= @name", db.getConnection());
            command.Parameters.Add("@name", MySqlDbType.VarChar).Value = name_textbox.Text;
            command.Parameters.Add("@surname", MySqlDbType.VarChar).Value = surname_textbox.Text;


            //заполняем table заданной sql командой
            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {

                MySqlCommand command2 = new MySqlCommand("SELECT * FROM `users`  ", db.getConnection());
                adapter.SelectCommand = command2;


                foreach (DataRow row in table.Rows)
                    arch2.Rows.Clear();
                foreach (DataRow row in table.Rows)
                    arch2.Rows.Add(row.ItemArray);
                MessageBox.Show("Контакт найден");

            }
            else
            {
                MessageBox.Show("Контакта не существует");

            }
        }

        /// КНОПКА В ГЛАВНОМ МЕНЮ
        /// 

        private void button2_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;

        }

        /// КНОПКА ПРОВЕРКИ ДНЯ РОЖДЕНИЯ
        /// 

        private void check_dr_Click(object sender, EventArgs e)
        {

            String today_panel = date_text.Text;

            DateBase db = new DateBase(); //выделяем память под объект 

            DataTable table = new DataTable(); //табличка, в которой можно работать 

            MySqlDataAdapter adapter = new MySqlDataAdapter(); //adapter позволяет выбрать данные из базы данных


            //находим наличие по коду и имени
            MySqlCommand command = new MySqlCommand("SELECT `ID`, `First_Name`, `Second_name`," +
                " `Date`  FROM `users` WHERE `Date` = @date OR `First_name`= @name", db.getConnection());

            command.Parameters.Add("@date", MySqlDbType.VarChar).Value = date_text.Text;

            command.Parameters.Add("@name", MySqlDbType.VarChar).Value = name_textbox.Text;

            //заполняем table заданной sql командой
            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {

                MySqlCommand command2 = new MySqlCommand("SELECT * FROM `users`  ", db.getConnection());
                adapter.SelectCommand = command2;


                foreach (DataRow row in table.Rows)
                    arch2.Rows.Clear();
                foreach (DataRow row in table.Rows)
                    arch2.Rows.Add(row.ItemArray);
                MessageBox.Show("У вашего контакта сегодня день рождения!!!!!");

            }
        }

        /// КНОПКА ОБНОВЛЕНИЯ ТЕКУЩЕЙ ДАТЫ
        /// 

        private void restart_time_Click(object sender, EventArgs e)
        {
            time_reg_textbox.Text = DateTime.Now.ToString();
        }

        private void num_textbox_KeyPress(object sender, KeyPressEventArgs e)
          
        {
            char number = e.KeyChar;
            if(!Char.IsDigit(number)&& number != 8 || number == 45)
            {
                e.Handled = true;
            }
        }
    }
    }


   


