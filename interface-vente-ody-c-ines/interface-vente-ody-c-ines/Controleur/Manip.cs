using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using interface_vente_ody_c_ines.Modele;
using System.IO;
using System.Web.Script.Serialization;

namespace interface_vente_ody_c_ines.Controleur
{
    public class Manip
    {
        public Infoco infconnexion;
        public string chemincsv;
        public List<vente> mesventes = new List<vente>();
        public List<vente> mesventesok = new List<vente>();
        //moncommentaire ici
        public void envoyerToutesLesVentes()
        {
            //c'est parti !
            foreach (vente mavente in mesventes)
            {
                //on charge une vente ici
                WsInes webserviceines = new WsInes();
                Infoco maconnexion = new Infoco();
                maconnexion.compte = "fluxdemo3";
                maconnexion.login = "bcoste";
                maconnexion.mdp = "admin2015";
                //faudrait faire la connexion ici
                maconnexion = webserviceines.login(maconnexion);
                
                //et ensuite aller charger toutes les ventes
                mesventesok.Add(webserviceines.chargerVente(mavente, maconnexion));

                //et ensuite faire la deconnexion



            }
        }
        public void rercupererVentesCsv()
        {
            //ici on lit du csv
            var reader = new StreamReader(File.OpenRead(@chemincsv));
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
                
            }
            
           
        }



    }
}
