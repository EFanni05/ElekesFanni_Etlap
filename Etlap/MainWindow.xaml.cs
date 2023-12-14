using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Etlap
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static MySqlConnection connection;
        List<Etlap> menu;

        public MainWindow()
        {
            InitializeComponent();
            menu = ReadIn();
            for (int i = 0; i < menu.Count; i++)
            {
                menu[i].ToString();
            }
        }

        public static List<Etlap> ReadIn()
        {
            List<Etlap> menu = new List<Etlap>();
            MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
            builder.Server = "127.0.0.1";
            builder.Port = 3306;
            builder.UserID = "root";
            builder.Password = "";
            builder.Database = "etlapdb";
            connection = new MySqlConnection(builder.ToString());
            try{
                connection.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = "SELECT * FROM etlap;";
                MySqlDataReader reader = cmd.ExecuteReader();
                if(!reader.HasRows)
                {
                    MessageBox.Show("Query sucessful");
                }
                else
                {
                    while (reader.Read())
                    {
                        Etlap a = new(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3), reader.GetString(4));
                        menu.Add(a);
                    }
                }
                connection.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Query Error [{ex.Message}]");
            }
            return menu;
        }

        private void EmelesSzazalek(object sender, RoutedEventArgs e)
        {
            if(!int.TryParse(EmelesSzazalekText.Text.Trim(), out int szazalek) && szazalek % 5 != 0){
                MessageBox.Show("Hibás Adat");
            }
            else
            {
                ListBox.Items.Clear();
                string sql = "UPDATE etlap SET `ar` = @ar WHERE `id` = @id";
                try
                {
                    for(int i = 0; i < menu.Count; i++)
                    {
                        MySqlCommand cmd = new MySqlCommand(sql);
                        cmd.Parameters.AddWithValue("@ar", (menu[i].Price * szazalek) / 100);
                        cmd.Parameters.AddWithValue("@id", i);
                        MySqlDataReader reader = cmd.ExecuteReader();
                        if (!reader.HasRows)
                        {
                            MessageBox.Show("0 line query");
                        }
                        else
                        {
                            menu[i].Price = (menu[i].Price * szazalek) / 100;
                            menu[i].ToString();
                        }
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Query Error [{ex.Message}]");
                }
            }
        }

        private void EmelesForint(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(EmelesForintText.Text.Trim(), out int forint) && forint % 50 != 0)
            {
                MessageBox.Show("Hibás Adat");
            }
            else
            {
                ListBox.Items.Clear();
                string sql = "UPDATE etlap SET `ar` = @ar WHERE `id` = @id";
                try
                {
                    for (int i = 0; i < menu.Count; i++)
                    {
                        MySqlCommand cmd = new MySqlCommand(sql);
                        cmd.Parameters.AddWithValue("@ar", menu[i].Price + forint);
                        cmd.Parameters.AddWithValue("@id", i);
                        MySqlDataReader reader = cmd.ExecuteReader();
                        if (!reader.HasRows)
                        {
                            MessageBox.Show("0 line query");
                        }
                        else
                        {
                            menu[i].Price = menu[i].Price + forint;
                            menu[i].ToString();
                        }
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Query Error [{ex.Message}]");
                }
            }
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            string name = NameAdd.Text.Trim();
            string description = LeirasAdd.Text.Trim();
            string type = TipusAdd.Text.Trim();
            int.TryParse(ArAdd.Text.Trim(), out int price);
            string sql = "INSERT INTO `etlap`(`id`, `nev`, `leiras`, `ar`, `kategoria`) VALUES (NULL,'@name','@desciprition','@price','@type')";
            if(name == "" || description == "" || type == "")
            {
                MessageBox.Show("Empty input");
            }
            else
            {
                try
                {
                    Etlap x = new(0, name, description, price, type);
                    connection.Open();
                    MySqlCommand cmd = new MySqlCommand(sql);
                    cmd.Parameters.AddWithValue("@name", x.Name);
                    cmd.Parameters.AddWithValue("@desciprition", x.Description);
                    cmd.Parameters.AddWithValue("@price", x.Price);
                    cmd.Parameters.AddWithValue("@type", x.Category);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    if (!reader.HasRows)
                    {
                        MessageBox.Show("Insert unsuccessful");
                    }
                    else
                    {
                        MessageBox.Show("Insert successful");
                        x.Id = reader.GetInt32(0);
                        menu.Add(x);
                        ListBox.Items.Add(x.ToString());
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error [{ex.Message}]");
                }
            }
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            string item = ListBox.SelectedItem.ToString();

        }
    }
}
