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
            //une nouvelle vente
            vente mavente = new vente();
            mavente.montant_fixe = 200;
            mavente.nom_societe = "orkeis";
            mavente.siret = "48357333300026";
            mavente.montant_mobile = 300;
            mavente.total_general = 500;
            mavente.tri_ic = "ERT";
            mavente.tri_tmk = "FGH";

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
            //MessageBox.Show(maresponse.codeReturn.ToString());


            //on enchaine et on essaye de trouver le client avec le siret.
            //on se fait un objet client soap
            interface_vente_ody_c_ines.ServiceReference2.WSICMSoapClient soapcm = new interface_vente_ody_c_ines.ServiceReference2.WSICMSoapClient();


            //on construit un sessionid
            interface_vente_ody_c_ines.ServiceReference2.SessionID masession = new interface_vente_ody_c_ines.ServiceReference2.SessionID();
            masession.ID = maresponse.idSession;            
            
            //on execute tout ça
            int numclient =  soapcm.GetSiren(ref masession, mavente.siret);
            //MessageBox.Show(numclient.ToString());

            //maintenant qu'on à a ref client on peut aller ajouter la vente

            //on se fait une réponse
            interface_vente_ody_c_ines.ServiceReference2.ClientInfo monclient = new ServiceReference2.ClientInfo();

            if(numclient > 0)
            {
                monclient = soapcm.GetClient(ref masession, numclient);
                //MessageBox.Show(monclient.Address1);
            }
            else
            {
                MessageBox.Show("Le client n'a pas été trouvé");
            }


            //on va envoyer une vente.

            //on construit les infos du client qui a la vente, le facturé, et le livré est le meme
            //on le recupere depuis le GetClient du contact Manager
            

            //on se fait le premier articleVente (mobile)
            interface_vente_ody_c_ines.Ebs.ArticleData articlemobile = new interface_vente_ody_c_ines.Ebs.ArticleData();
            articlemobile.SalePrice = mavente.montant_mobile;
            articlemobile.Quantity = 1;
            articlemobile.ArticleName = "TELEPHONIE MOBILE";
            articlemobile.ArticleDescription = "Téléphonie Mobile";
            articlemobile.InternalRef = 9;

            //on se fait le deuxieme articleVente (fixe)
            interface_vente_ody_c_ines.Ebs.ArticleData articlefixe = new interface_vente_ody_c_ines.Ebs.ArticleData();
            articlefixe.SalePrice = mavente.montant_fixe;
            articlefixe.Quantity = 1;
            articlefixe.ArticleName = "TELEPHONIE FIXE";
            articlefixe.ArticleDescription = "Téléphonie Fixe";
            articlefixe.InternalRef = 10;

            //on se fait un tableau d'articleVente
            var montabart = new interface_vente_ody_c_ines.Ebs.ArticleData[2];
            montabart[0] = articlefixe;
            montabart[1] = articlemobile;

            //on se fait un client soap
            interface_vente_ody_c_ines.Ebs.WSEBSSoapClient soapebs = new Ebs.WSEBSSoapClient();


            //on reconstruit le ClientInfo
            interface_vente_ody_c_ines.Ebs.ClientInfo monclient2 = new Ebs.ClientInfo();
            monclient2.AccountingCode = monclient.AccountingCode;
            monclient2.InternalRef = monclient.InternalRef;
            monclient2.TaxType = monclient.TaxType;
            //on reconstruit la session
            interface_vente_ody_c_ines.Ebs.SessionID masession2 = new Ebs.SessionID();
            masession2.ID = masession.ID;

            //je construis la réponse 
            interface_vente_ody_c_ines.Ebs.SaleInfo maventeines = new Ebs.SaleInfo();

            
            
            //on exécute tout ça


            maventeines = soapebs.AddSaleFromArticles(ref masession2, monclient2, monclient2, monclient2, montabart);

            MessageBox.Show(maventeines.SaleInternalReference);




        }

        

        private void button6_Click(object sender, EventArgs e)
        {
            //ici on lit du csv
            var reader = new StreamReader(File.OpenRead(@textBox4.Text));
            //on lit la premiere ligne
            reader.ReadLine();
            //le prochain sera la deuxieme ligne
            List<vente> mesventes = new List<vente>();

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine(); //ça c'est une ligne
                //MessageBox.Show(line);
                var values = line.Split(';'); //ca c'est un tableau de chaine de ma liste
                vente mavente = new vente();

                mavente.nom_societe = values[0];
                mavente.siret = values[1];
               
                //string result = txt.Trim(charsToTrim);

                //je teste pour voir s'il y a de la donnée.
                if (values[2] != "")
                {
                    //MessageBox.Show(values[2]);
                    mavente.montant_fixe = Int32.Parse(values[2].Replace(" ", String.Empty));

                }
                if (values[3] != "")
                {
                    //MessageBox.Show(values[3].Trim());
                    mavente.montant_mobile = Int32.Parse(values[3].Replace(" ", String.Empty));
                }
                if (values[4]!= "")
                {
                    //MessageBox.Show(values[4].Trim());

                    mavente.total_general = Int32.Parse(values[4].Replace(" ", String.Empty));

                }
                mavente.tri_ic = values[5];
                mavente.tri_tmk = values[6];

                //je fais une liste de vente
                mesventes.Add(mavente);

              
            }
            //on essaye de voir s'il y a des données dans notre tableau.
            JavaScriptSerializer js = new JavaScriptSerializer();
            string json = js.Serialize(mesventes);
            output.Text = json;
          
            //MessageBox.Show(json);//show me
        }

        private void viderbtn_Click(object sender, EventArgs e)
        {
            output.Text = "";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
