using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace BagolyvarRendes
{
    public partial class kolcsonzescs : Form
    {
        public kolcsonzescs()
        {
            InitializeComponent();
        }
        class Kolcsonzes
        {
            public string kolcsonzottkonyv;
            public string kolcsonzottkonyvID;
            public string kolcsonzonev;
            public string kolcsonzonevID;
            public int kolcsonzottkonyvdb;
            public DateTime kolcsonzottdatum;

            public Kolcsonzes(string kolcsonzottkonyv, string kolcsonzottkonyvID, string kolcsonzonev, string kolcsonzonevID, int kolcsonzottkonyvdb, DateTime kolcsonzottdatum)
            {
                this.kolcsonzottkonyv = kolcsonzottkonyv;
                this.kolcsonzottkonyvID = kolcsonzottkonyvID;
                this.kolcsonzonev = kolcsonzonev;
                this.kolcsonzonevID = kolcsonzonevID;
                this.kolcsonzottkonyvdb = kolcsonzottkonyvdb;
                this.kolcsonzottdatum = kolcsonzottdatum;
            }
        }
        static List<Kolcsonzes> adatok = new List<Kolcsonzes>();
        private void kolcsonzescs_Load(object sender, EventArgs e)
        {
            foreach (berlok item in Program.berlok)
            {
                comboBox2.Items.Add(item.Nev.ToString());
            }
            foreach (konyvek item in Program.Konyvek)
            {
                comboBox1.Items.Add(item.Cim.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string listaba = comboBox1.SelectedItem.ToString() + "-" + comboBox2.SelectedItem.ToString() + "-" + numericUpDown1.Value;
            listBox1.Items.Add(listaba);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //listBox1.Items.Clear();
            foreach (var item in listBox1.Items)
            {
                string elem = item.ToString();
                string[] tordel = elem.Split('-');

                string konyvid = "", kolcsonzoid = "";
                var kolcsonzottid = Program.berlok.FindAll(a => a.Nev.Equals(tordel[1])).Select(c => new { c.Id });
                foreach (var item2 in kolcsonzottid)
                {
                    kolcsonzoid = item2.Id.ToString();
                }
                var konyvID = Program.Konyvek.FindAll(a => a.Cim.Equals(tordel[0])).Select(c => new { c.Kod });
                foreach (var item3 in konyvID)
                {
                    konyvid = item3.Kod.ToString();
                }

                Kolcsonzes uj = new Kolcsonzes(tordel[1], konyvid, tordel[0], kolcsonzoid, Convert.ToInt32(tordel[2]), DateTime.Now);
                adatok.Add(uj);
            }
            foreach (var item in adatok)
            {


                Program.sqlCommand.CommandText = "INSERT INTO `kolcsonzes` (`konyvID`, `kolcsonzoID`, `kivetelDatum`, peldany) VALUES (@konyvID, @kolcsonzoID, @kivetelDatum, @pld);";
                Program.sqlCommand.Parameters.Clear();
                Program.sqlCommand.Parameters.AddWithValue("@konyvID", item.kolcsonzottkonyvID);
                Program.sqlCommand.Parameters.AddWithValue("@kolcsonzoID", item.kolcsonzonevID);
                Program.sqlCommand.Parameters.AddWithValue("@kivetelDatum", item.kolcsonzottdatum);
                Program.sqlCommand.Parameters.AddWithValue("@pld", item.kolcsonzottkonyvdb);
                try
                {
                    Program.sqlCommand.ExecuteNonQuery();
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
            }
            MessageBox.Show("A listában szereplő adatokat sikeresen kiírtam az adatbázisba!");
        }
    }
}
