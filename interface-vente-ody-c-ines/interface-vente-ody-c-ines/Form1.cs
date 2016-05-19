using interface_vente_ody_c_ines.Controleur;
using interface_vente_ody_c_ines.Modele;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace interface_vente_ody_c_ines
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            textBox3.PasswordChar = '*';
            textBox1.Text = Properties.Settings.Default.ines_compte;
            textBox2.Text = Properties.Settings.Default.ines_login;
            textBox3.Text = Properties.Settings.Default.ines_mdp;
            textBox4.Text = Properties.Settings.Default.csv_chemin;

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Infoco moninfoco = new Infoco(Properties.Settings.Default.ines_compte, Properties.Settings.Default.ines_login, Properties.Settings.Default.ines_mdp, Properties.Settings.Default.csv_chemin);

            Manip mamanip = new Manip(moninfoco);
            
            mamanip.rercupererVentesCsv();
            mamanip.envoyerToutesLesVentes();
            mamanip.deplacerFichierTraite();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.ines_login = textBox2.Text;
            Properties.Settings.Default.Save();
            MessageBox.Show("Modification ok");
        }

        private void btn_compte_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.ines_compte = textBox1.Text;
            Properties.Settings.Default.Save();
            MessageBox.Show("Modification ok");


        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.ines_mdp = textBox3.Text;
            Properties.Settings.Default.Save();
            MessageBox.Show("Modification ok");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.csv_chemin = textBox4.Text;
            Properties.Settings.Default.Save();
            MessageBox.Show("Modification ok");
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        

        

        

        private void button2_Click(object sender, EventArgs e)
        {
            //on va chercher le fichier
            string filename = "";

            OpenFileDialog ofd = new OpenFileDialog();
            DialogResult dr = ofd.ShowDialog();

            if (dr == DialogResult.OK)
            {
                filename = ofd.FileName;
                textBox4.Text = filename;

            }
            //on met ça dans le parametrage
            Properties.Settings.Default.csv_chemin = textBox4.Text;
            Properties.Settings.Default.Save();
            MessageBox.Show("Modification ok");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            //on traite le fichier
            Infoco moninfoco = new Infoco();
            moninfoco.chemincsv = Properties.Settings.Default.csv_chemin;
            Manip mamanip = new Manip();
            mamanip.infconnexion = moninfoco;
            //c'est parti
            mamanip.deplacerFichierTraite();

            
        }
    }
}
