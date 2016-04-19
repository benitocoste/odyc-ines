using interface_vente_ody_c_ines.Modele;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace interface_vente_ody_c_ines
{
    class WsInes
    {

        public void Login()
        {

        }
        public vente chargerVente(vente unevente, Infoco maconnexion)
        {
            //une nouvelle vente
            

            //on instancie la classe client soap
            interface_vente_ody_c_ines.Log.LoginSoapClient monlogin = new interface_vente_ody_c_ines.Log.LoginSoapClient();

            //on construit l'objet de requete
            interface_vente_ody_c_ines.Log.AuthRequest monrequest = new interface_vente_ody_c_ines.Log.AuthRequest();
            monrequest.compte = maconnexion.compte;
            monrequest.userName = maconnexion.login;
            monrequest.password = maconnexion.mdp;
            //on construit l'objet de reponse
            interface_vente_ody_c_ines.Log.AuthResponse maresponse = new interface_vente_ody_c_ines.Log.AuthResponse();

            //on execute tout ça
            maresponse = monlogin.authenticationWs(monrequest);

            //on garde l'id au cas ou
            maconnexion.idsession = maresponse.idSession;


            //on enchaine et on essaye de trouver le client avec le siret.
            //on se fait un objet client soap
            interface_vente_ody_c_ines.Cm.WSICMSoapClient soapcm = new interface_vente_ody_c_ines.Cm.WSICMSoapClient();


            //on construit un sessionid
            interface_vente_ody_c_ines.Cm.SessionID masession = new interface_vente_ody_c_ines.Cm.SessionID();
            masession.ID = maresponse.idSession;

            //on execute tout ça
            int numclient = soapcm.GetSiren(ref masession, unevente.siret);
            //MessageBox.Show(numclient.ToString());

            //maintenant qu'on à a ref client on peut aller ajouter la vente

            //on se fait une réponse
            interface_vente_ody_c_ines.Cm.ClientInfo monclient = new Cm.ClientInfo();

            if (numclient > 0)
            {
                monclient = soapcm.GetClient(ref masession, numclient);
            }
            else
            {
                unevente.resultat = "client introuvable avec le siret";
                return unevente;
            }



            //on se fait le premier articleVente (mobile)
            interface_vente_ody_c_ines.Ebs.ArticleData articlemobile = new interface_vente_ody_c_ines.Ebs.ArticleData();
            articlemobile.SalePrice = unevente.montant_mobile;
            articlemobile.Quantity = 1;
            articlemobile.ArticleName = "TELEPHONIE MOBILE";
            articlemobile.ArticleDescription = "Téléphonie Mobile";
            articlemobile.InternalRef = 9;

            //on se fait le deuxieme articleVente (fixe)
            interface_vente_ody_c_ines.Ebs.ArticleData articlefixe = new interface_vente_ody_c_ines.Ebs.ArticleData();
            articlefixe.SalePrice = unevente.montant_fixe;
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

            unevente.resultat = "le numero de vente généré est : " + maventeines.SaleInternalReference;
            return unevente;
        }
        

    }
}
