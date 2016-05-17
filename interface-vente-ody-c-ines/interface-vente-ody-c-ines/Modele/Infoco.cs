using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace interface_vente_ody_c_ines.Modele
{
    public class Infoco
    {
        public string compte;
        public string login;
        public string mdp;
        public string idsession;
        public string chemincsv;
        public Infoco()
        {
            //constructeur initial vide
        }
        public Infoco(string pcompte, string plogin, string pmdp, string pchemin)
        {
            //Surcharge constructeur
            this.compte = pcompte;
            this.login = plogin;
            this.mdp = pmdp;
            this.chemincsv = pchemin;
        }
    }
}
