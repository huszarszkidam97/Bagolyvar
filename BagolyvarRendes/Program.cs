using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace BagolyvarRendes
{
    static class Program
    {
        public static MySqlConnection conn = null;
        public static MySqlCommand sqlCommand = null;
        public static List<konyvek> Konyvek = new List<konyvek>();
        public static List<berlok> berlok = new List<berlok>();
        [STAThread]

        static void Main()
        {
            MySqlConnectionStringBuilder sb = new MySqlConnectionStringBuilder();
            sb = new MySqlConnectionStringBuilder();
            sb.Server = "localhost";
            sb.UserID = "root";
            sb.Password = "";
            sb.Database = "bagolyvar";
            sb.CharacterSet = "utf8";
            conn = new MySqlConnection(sb.ToString());
            try
            {
                conn.Open();
                sqlCommand = conn.CreateCommand();
                Berlok_betoltese();
                Konyvek_betoltese();
            }
            catch (MySqlException es)
            {
                MessageBox.Show(es.Message);
                Environment.Exit(0);
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
        public static void Berlok_betoltese()
        {
            berlok.Clear();
            //-- Bérlő adatainak a betöltése ---------------
            sqlCommand.CommandText = "SELECT `ID`,`nev` FROM `kolcsonzo` ORDER BY `nev`";
            try
            {
                using (MySqlDataReader dr = Program.sqlCommand.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        berlok uj = new berlok(dr.GetInt32("ID"), dr.GetString("nev"));
                        berlok.Add(uj);
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                Environment.Exit(0);
            }
        }
        public static void Konyvek_betoltese()
        {
            Konyvek.Clear();
            //-- Könyv adatok betöltése ------------------------
            sqlCommand.CommandText = "SELECT `Kod`,`Szerzo`,`Cím`,`KiadasEve`,`Ar`,`ISBN` FROM `konyvek`";

            try
            {
                using (MySqlDataReader dr = Program.sqlCommand.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        konyvek uj = new konyvek(dr.GetString("Kod"), dr.GetString("Szerzo"), dr.GetString("Cím"), dr.GetInt32("KiadasEve"), dr.GetInt32("Ar"), dr.GetString("ISBN"));
                        Program.Konyvek.Add(uj);
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                Environment.Exit(0);
            }

        }
    }
}
