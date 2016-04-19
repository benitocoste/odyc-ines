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
            Manip mamanip = new Manip();
            mamanip.chemincsv = @"c:\test\ventes.csv";
            MessageBox.Show(mamanip.chemincsv);
            mamanip.rercupererVentesCsv();
            mamanip.envoyerToutesLesVentes();
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

        

        

        private void button6_Click(object sender, EventArgs e)
        {
            Manip mamanip = new Manip();
            mamanip.chemincsv = @"c:\test\ventes.csv";
            MessageBox.Show(mamanip.chemincsv);
            mamanip.rercupererVentesCsv();
           
        }

        

        
    }
}
