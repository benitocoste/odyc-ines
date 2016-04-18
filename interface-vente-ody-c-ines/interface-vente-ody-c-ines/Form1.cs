using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        private void soumettreBTN_Click(object sender, EventArgs e)
        {
            output.Text = "nous allons consommer notre webservice";

            //on instancie la classe client soap
            interface_vente_ody_c_ines.ServiceReference1.LoginSoapClient monlogin = new interface_vente_ody_c_ines.ServiceReference1.LoginSoapClient();

            //on construit l'objet de requete
            interface_vente_ody_c_ines.ServiceReference1.AuthRequest monrequest = new interface_vente_ody_c_ines.ServiceReference1.AuthRequest();
            monrequest.compte = "fluxdemo3";
            monrequest.userName = "coste";
            monrequest.password = "admin2015";
            //on construit l'objet de reponse
            interface_vente_ody_c_ines.ServiceReference1.AuthResponse maresponse = new interface_vente_ody_c_ines.ServiceReference1.AuthResponse();

            //on execute tout ça
            maresponse = monlogin.authenticationWs(monrequest);

            string monidsession = maresponse.idSession;
            MessageBox.Show(maresponse.codeReturn.ToString());


            //on enchaine et on essaye de trouver le client avec le siret.
            //on se fait un objet client soap
            interface_vente_ody_c_ines.ServiceReference2.WSICMSoapClient soapcm = new interface_vente_ody_c_ines.ServiceReference2.WSICMSoapClient();


            //on construit un sessionid
            interface_vente_ody_c_ines.ServiceReference2.SessionID masession = new interface_vente_ody_c_ines.ServiceReference2.SessionID();
            masession.ID = maresponse.idSession;            
            
            //on execute tout ça
            int numclient =  soapcm.GetSiren(ref masession, "483573330026");
            MessageBox.Show(numclient.ToString());

            //on ferme tout ça 

        }
    }
}
