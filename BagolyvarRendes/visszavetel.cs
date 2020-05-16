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
    public partial class visszavetel : Form
    {
        public visszavetel()
        {
            InitializeComponent();
        }
        class Ember
        {
            public string konyv;
            public string ISBN;
            public int peldanydb;

            public Ember(string konyv, string iSBN, int peldanydb)
            {
                this.konyv = konyv;
                ISBN = iSBN;
                this.peldanydb = peldanydb;
            }
        }

        private void visszavetel_Load(object sender, EventArgs e)
        {
            foreach (var item in Program.berlok)
            {
                comboBox1.Items.Add(item.Nev);
            }
        }
        static List<Ember> adat = new List<Ember>();
        public void refresh()
        {
            adat.Clear();
            listBox1.Items.Clear();
            string emberke = comboBox1.SelectedItem.ToString();
            Program.sqlCommand.CommandText = "SELECT DISTINCT kolcsonzo.nev, konyvek.Cím,konyvek.ISBN,kolcsonzes.peldany FROM kolcsonzes " +
                "INNER JOIN kolcsonzo ON(kolcsonzo.ID = kolcsonzes.kolcsonzoID) " +
                "INNER JOIN konyvek ON(kolcsonzes.konyvID = konyvek.Kod) " +
                "WHERE kolcsonzo.nev = '" + emberke + "'";
            try
            {
                using (MySqlDataReader dr = Program.sqlCommand.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        for (int i = 0; i < dr.GetInt32(3); i++)
                        {
                            listBox1.Items.Add(dr.GetString(1));
                        }

                        adat.Add(new Ember(dr.GetString(1), dr.GetString(2), dr.GetInt32(3)));
                    }
                }
            }
            catch
            {

            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string ISBN = textBox1.Text;
            string cim = listBox1.SelectedItem.ToString();

            foreach (var item in adat)
            {
                if (item.konyv == cim && ISBN == item.ISBN)
                {
                    int db = item.peldanydb - 1;
                    Program.sqlCommand.CommandText = "UPDATE kolcsonzes INNER JOIN kolcsonzo ON(kolcsonzo.ID = kolcsonzes.kolcsonzoID) INNER JOIN konyvek ON(kolcsonzes.konyvID = konyvek.Kod)SET kolcsonzes.peldany = '"+db+"'WHERE konyvek.Cím = '"+cim+"' AND kolcsonzo.nev = '"+comboBox1.SelectedItem.ToString()+"'";
                    Program.sqlCommand.ExecuteNonQuery();
                    MessageBox.Show("Sikeresen átálítva: " + db + "-ra");
                }

            }
            refresh();

        }
    }
}
