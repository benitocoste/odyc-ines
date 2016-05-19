using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using interface_vente_ody_c_ines.Modele;
using System.IO;
using System.Web.Script.Serialization;
using System.Xml;
using System.Windows.Forms;

namespace interface_vente_ody_c_ines.Controleur
{
    public class Manip
    {
        public Infoco infconnexion;
        public List<vente> mesventes = new List<vente>();
        public List<vente> mesventesok = new List<vente>();

        //surcharge constructeur
        //constructeur natif on ne fait rien
        public Manip() { }
        //surcharge
        public Manip(Infoco theinfoco)
        {
            this.infconnexion = theinfoco;
        }
        //déplacer les le fichier de vente
        public void deplacerFichierTraite()
        {
            //on recupere le rep du fichier
            string dossierdufichier = Path.GetDirectoryName(this.infconnexion.chemincsv);
            MessageBox.Show("le rep est :" + dossierdufichier);
            //si un rep traité n'existe pas, on le créé
            if (!Directory.Exists(dossierdufichier+@"\fait\"))
            {
                MessageBox.Show("le rep n'existe pas");
                //on se créé le rep
                Directory.CreateDirectory(dossierdufichier + @"\fait\");
            }
            else
            {
                MessageBox.Show("le rep existe");
            }


            //on déplace le fichier
            File.Move(this.infconnexion.chemincsv, dossierdufichier + @"\fait\" + Path.GetFileName(this.infconnexion.chemincsv));




        }

        //envoyer les ventes
        public void envoyerToutesLesVentes()
        {
            //c'est parti !
            foreach (vente mavente in mesventes)
            {
                //on charge une vente ici
                WsInes webserviceines = new WsInes();
                Infoco maconnexion = new Infoco();
                maconnexion.compte = this.infconnexion.compte;
                maconnexion.login  = this.infconnexion.login;
                maconnexion.mdp    = this.infconnexion.mdp;
                //faudrait faire la connexion ici
                maconnexion = webserviceines.login(maconnexion);
                
                //et ensuite aller charger toutes les ventes
                mesventesok.Add(webserviceines.chargerVente(mavente, maconnexion));

                //et ensuite faire la deconnexion



            }
        }
        public void rercupererVentesCsv()
        {

            try
            {
                // Code to try goes here.
                var reader = new StreamReader(File.OpenRead(this.infconnexion.chemincsv));

                //on lit la premiere ligne(entete)
                reader.ReadLine();
                //le prochain sera la deuxieme ligne

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
                    if (values[4] != "")
                    {
                        //MessageBox.Show(values[4].Trim());

                        mavente.total_general = Int32.Parse(values[4].Replace(" ", String.Empty));

                    }
                    mavente.tri_ic = values[5];
                    mavente.tri_tmk = values[6];

                    //je fais une liste de vente
                    mesventes.Add(mavente);

                }//fin du while
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("Le fichier n'existe pas ou le chemin n'est pas correct");
                // Code to handle the exception goes here.
            }
            catch(ArgumentException)
            {
                MessageBox.Show("La longueur du chemin de fichier est vide/nulle");
            }
                      
            
            
            
           
        }
        public void recupererParametrage()
        {
            Infoco monparametrage = new Infoco();
            
        }
        public void ecritParametrage(Infoco moninfoco)
        {
            //on ecrit notre fichier
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;

            XmlWriter writer = XmlWriter.Create("param.xml", settings);
            writer.WriteStartDocument();
            writer.WriteComment("fichier de paramétrage pour ODY C");
            writer.WriteStartElement("compte");
            writer.WriteAttributeString("ID", "001");
            writer.WriteAttributeString("Name", "Soap");
            writer.WriteElementString("Price", "10.00");
            writer.WriteStartElement("OtherDetails");
            writer.WriteElementString("BrandName", "X Soap");
            writer.WriteElementString("Manufacturer", "X Company");
            writer.WriteEndElement();
            writer.WriteEndDocument();

            writer.Flush();
            writer.Close();

        }




    }
}
