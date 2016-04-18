using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace interface_vente_ody_c_ines
{

    //je pense qu'on peut déclarer des structures ici : 
    public struct vente
    {
        public string nom_societe;
        public decimal montant_fixe;
        public decimal montant_mobile;
        public decimal total_general;
        public string tri_ic;
        public string tri_tmk;
        public string siret;
    }


    static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
            //je passe mon bouton en passeword
        }
    }
}
