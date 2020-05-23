using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//Using Specifiche
using adoNetWebSQlServer;
using System.Data;
using System.Web.UI.HtmlControls;

namespace ProgettoEcommerce
{
    public partial class category : System.Web.UI.Page
    {
        /**********************/
        /* Routine Principale */
        /**********************/
        protected void Page_Load(object sender, EventArgs e)
        {
            //Gestito fuori dal postback per poter agganciare l'evento al bottone di logout
            if (Session["IdUtente"] != null && Session["TipoUtente"] != null)
                gestUtenteLoggato();
            else
                gestUtenteNonLoggato();
            if (!Page.IsPostBack)
            {
                adoNet.impostaConnessione("App_Data/DBEcommerce.mdf");
                ricercaIniziale();
                stampaElCategorie();
            }
        }

        /**********************************/
        /* Gestione NavBar Utente Loggato */
        /**********************************/
        private void gestUtenteLoggato()
        {
            navUtenteCarrrello.Visible = true;
            LinkButton btnLogout = new LinkButton();
            btnLogout.CssClass = "icons";
            btnLogout.Text = "<i class='fa fa-sign-out' aria-hidden='true'></i> Esci";
            btnLogout.Click += BtnLogout_Click;
            contLogout.Controls.Add(btnLogout);
            if (Session["TipoUtente"].ToString().ToUpper() == "ADMIN")
            {
                navCarrello.Visible = false;
                navStoricoOrdini.Visible = false;
            }
            else if(Session["TipoUtente"].ToString().ToUpper() == "CLIENTE")
            {
                navAndamentoVendite.Visible = false;
                navCategorie.Visible = false;
                navGestioneOrdini.Visible = false;
                navGestioneProdotti.Visible = false;
                navGestioneUtenti.Visible = false;
                navTipiCarte.Visible = false;
            }
            else
            {
                navCarrello.Visible = false;
                navGestioneUtenti.Visible = false;
                navStoricoOrdini.Visible = false;
                navTipiCarte.Visible = false;
                navCategorie.Visible = false;
            }
            navHome.Visible = false;
            navRegistrati.Visible = false;
            navLogin.Visible = false;
            navReimpostaPwd.Visible = false;
        }

        /*******************/
        /* Gestione Logout */
        /*******************/
        private void BtnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("login.aspx");
        }

        /**************************************/
        /* Gestione NavBar Utente Non Loggato */
        /**************************************/
        private void gestUtenteNonLoggato()
        {
            navUtenteCarrrello.Visible = false;
            navAndamentoVendite.Visible = false;
            navCarrello.Visible = false;
            navGestioneProdotti.Visible = false;
            navGestioneUtenti.Visible = false;
            navStoricoOrdini.Visible = false;
            navTipiCarte.Visible = false;
            navCategorie.Visible = false;
            navGestioneOrdini.Visible = false;
            contNavBar.Attributes.Add("class", "col-lg-12 pr-0");
        }

        /****************************/
        /* Recupero Elenco Prodotti */
        /****************************/
        private void ricercaIniziale()
        {
            adoNet ado = new adoNet();
            string codSql = String.Empty;

            codSql = "SELECT * " +
                "FROM Prodotti AS P " +
                "INNER JOIN Fornitori AS F " +
                "ON P.IdFornitore = F.IdFornitore " +
                "INNER JOIN Categorie AS C " +
                "ON C.IdCategoria = P.IdCategoria " +
                "WHERE P.ValProdotto = ' ' " +
                "AND F.ValFornitore = ' ' " +
                "AND C.ValCategoria = ' ' ";
            try
            {
                stampaElProdotti(ado.eseguiQuery(codSql, CommandType.Text));
            }
            catch (Exception ex)
            {
                stampaErrori(msgErroreElProd, "Attenzione!!! Errore: " + ex.Message);
            }
        }

        /**************************/
        /* Stampa Elenco Prodotti */
        /**************************/
        private void stampaElProdotti(DataTable elProd)
        {
            string codHtml = String.Empty;
            string ausPrezzo = String.Empty;

            for (int i = 0; i < elProd.Rows.Count; i++)
            {
                if (elProd.Rows[i].ItemArray[8].ToString() != String.Empty)
                    ausPrezzo = ((Convert.ToDouble(elProd.Rows[i].ItemArray[8].ToString()) * 100) / (Convert.ToDouble(elProd.Rows[i].ItemArray[7].ToString()))).ToString();
                else
                    ausPrezzo = String.Empty;
                codHtml += "<div class='col-lg-4 col-md-6'>" +
                    "<div class='single-product'>" +
                    "<div class='product-img'>" +
                    "<img class='card-img' src='img/product/" + elProd.Rows[i].ItemArray[4].ToString() + "' alt=''/>" +
                    "<div class='p_icon'>" +
                    "<a href='dettaglioProdotto.aspx?codProd=" + elProd.Rows[i].ItemArray[0].ToString() + "'>" +
                    "<i class='ti-eye'></i>" +
                    "</a>" +
                    "</div>" +
                    "</div>" +
                    "<div class='product-btm'>" +
                    "<a href='dettaglioProdotto.aspx?codProd=" + elProd.Rows[i].ItemArray[0].ToString() + "'>" +
                    "<h4>" + elProd.Rows[i].ItemArray[1].ToString() + "</h4>" +
                    "</a>" +
                    "<div class='mt-3'>" +
                    "<span class='mr-4'>" + Convert.ToDouble(elProd.Rows[i].ItemArray[7].ToString()) + "&euro;</span>";
                if (ausPrezzo != String.Empty)
                    codHtml += "<del>" + ausPrezzo + "&euro;</del>";
                codHtml += "</div>" +
                    "</div>" +
                    "</div>" +
                    "</div>";

            }
            contProdotti.InnerHtml = codHtml;
        }

        /***************************/
        /* Stampa Elenco Categorie */
        /***************************/
        private void stampaElCategorie()
        {
            adoNet ado = new adoNet();
            string codSql = String.Empty;
            DataTable tab = new DataTable();
            string codHtml = String.Empty;

            codSql = "SELECT * FROM Categorie WHERE ValCategoria = ' '";
            try
            {
                tab = ado.eseguiQuery(codSql, CommandType.Text);
                //Aggiungo al recordset una voce che indichi tutte le categorie
                //per la ricerca
                DataRow aus = tab.NewRow();
                aus[0] = 0;
                aus[1] = "Tutte";
                aus[2] = ' ';
                tab.Rows.InsertAt(aus, 0);
                elencoCatRic.DataSource = tab;
                elencoCatRic.DataValueField = "IdCategoria";
                elencoCatRic.DataTextField = "DescrizioneCategoria";
                elencoCatRic.SelectedIndex = 0;
                elencoCatRic.DataBind();
            }
            catch (Exception ex)
            {
                stampaErrori(msgRicProd, "Attenzione!!! Errore: " + ex.Message);
            }
        }

        /***************************/
        /* Stampa Ricerca Prodotti */
        /***************************/
        protected void btnRicercaProdotto_Click(object sender, EventArgs e)
        {
            adoNet ado = new adoNet();
            string codSql = String.Empty;

            codSql = "SELECT * FROM Prodotti WHERE ValProdotto = ' '";
            if (txtNomeProdRic.Value != String.Empty)
                codSql += "AND ModelloProdotto LIKE '%" + txtNomeProdRic.Value + "%'";
            if (elencoCatRic.SelectedIndex > 0)
                codSql += "AND IdCategoria = " + elencoCatRic.SelectedValue.ToString() + "";

            try
            {
                stampaElProdotti(ado.eseguiQuery(codSql, CommandType.Text));
            }
            catch (Exception ex)
            {
                stampaErrori(msgErroreElProd, "Attenzione!!! Errore: " + ex.Message);
            }

        }

        /*******************/
        /* Gestione Errori */
        /*******************/
        private void stampaErrori(HtmlGenericControl contMsg, string msgErrore)
        {
            contMsg.InnerText = msgErrore;
            contMsg.Attributes.Add("class", "alert alert-danger");
            contMsg.Visible = true;
        }
    }
}