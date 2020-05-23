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
using System.Web.UI.DataVisualization.Charting;

namespace ProgettoEcommerce
{
    public partial class andamentoVendite : System.Web.UI.Page
    {
        /**********************/
        /* Routine Principale */
        /**********************/
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                adoNet.impostaConnessione("App_Data/DBEcommerce.mdf");
                if (Session["IdUtente"] == null && Session["TipoUtente"] == null)
                    Response.Redirect("login.aspx");
                else if (Session["IdUtente"] != null && Session["TipoUtente"].ToString().ToUpper() != "CLIENTE")
                {
                    creaGraficoCategorie();
                    creaGraficoProdotti();
                }
                else
                    Response.Redirect("prodotti.aspx");
            }
            //Gestito fuori dal postback per poter agganciare l'evento al bottone di logout
            gestUtenteLoggato();
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
            if (Session["TipoUtente"].ToString().ToUpper() == "FORNITORE")
            {
                navGestioneUtenti.Visible = false;
                navTipiCarte.Visible = false;
                navCategorie.Visible = false;
            }

            navCarrello.Visible = false;
            navStoricoOrdini.Visible = false;
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

        /***************************************/
        /* Creazione Grafico Vendite Categorie */
        /***************************************/
        private void creaGraficoCategorie()
        {
            adoNet ado = new adoNet();
            string codSql = String.Empty;
            DataTable tab = new DataTable();
            Series serie = new Series();

            if (Session["TipoUtente"].ToString().ToUpper() == "ADMIN")
            {
                codSql = "SELECT C.IdCategoria, C.DescrizioneCategoria, COUNT(C.IdCategoria) AS NVendite " +
                "FROM Prodotti AS P " +
                "INNER JOIN Categorie AS C " +
                "ON P.IdCategoria = C.IdCategoria " +
                "INNER JOIN DettaglioOrdini AS D " +
                "ON D.IdProdotto = P.IdProdotto " +
                "WHERE P.ValProdotto = ' ' " +
                "AND C.ValCategoria = ' ' " +
                "GROUP BY C.IdCategoria, C.DescrizioneCategoria ";
            }
            else
            {
                codSql = "SELECT C.IdCategoria, C.DescrizioneCategoria, COUNT(C.IdCategoria) AS NVendite " +
                "FROM Prodotti AS P " +
                "INNER JOIN Categorie AS C " +
                "ON P.IdCategoria = C.IdCategoria " +
                "INNER JOIN DettaglioOrdini AS D " +
                "ON D.IdProdotto = P.IdProdotto " +
                "WHERE P.ValProdotto = ' ' " +
                "AND C.ValCategoria = ' ' " +
                "AND P.IdFornitore = "+ Session["IdUtente"].ToString() +
                "GROUP BY C.IdCategoria, C.DescrizioneCategoria ";
            }
            

            try
            {
                tab = ado.eseguiQuery(codSql, CommandType.Text);
                if (tab.Rows.Count > 0)
                {
                    contGraficiVendite.Visible = true;
                    serie = graficoVenditeCategorie.Series["serieVenditeTotaliCategorie"];
                    serie.Points.Clear();
                    for (int i = 0; i < tab.Rows.Count; i++)
                        serie.Points.AddXY(tab.Rows[i].ItemArray[1].ToString(), tab.Rows[i].ItemArray[2].ToString());
                }
                else
                {
                    contGraficiVendite.Visible = false;
                    stampaErrori(msgGraficiVendite, "Nessun dato relativo alle Vendite individuato");
                }
            }
            catch (Exception ex)
            {
                stampaErrori(msgGraficiVendite, "Errore: " + ex.Message);
            }
        }

        /**************************************/
        /* Creazione Grafico Vendite Prodotti */
        /**************************************/
        private void creaGraficoProdotti()
        {
            adoNet ado = new adoNet();
            string codSql = String.Empty;
            DataTable tab = new DataTable();
            Series serie = new Series();

            if (Session["TipoUtente"].ToString().ToUpper() == "ADMIN")
            {
                codSql = "SELECT P.IdProdotto, P.ModelloProdotto, COUNT(*) AS NVenditeProd " +
                "FROM Prodotti AS P " +
                "INNER JOIN DettaglioOrdini AS D " +
                "ON P.IdProdotto = D.IdProdotto " +
                "WHERE P.ValProdotto = ' ' AND D.ValDettaglioOrdini = ' ' " +
                "GROUP BY P.IdProdotto, P.ModelloProdotto ";
            }
            else
            {
                codSql = "SELECT P.IdProdotto, P.ModelloProdotto, COUNT(*) AS NVenditeProd " +
                "FROM Prodotti AS P " +
                "INNER JOIN DettaglioOrdini AS D " +
                "ON P.IdProdotto = D.IdProdotto " +
                "WHERE P.ValProdotto = ' ' " +
                "AND D.ValDettaglioOrdini = ' ' " +
                "AND P.IdFornitore = "+ Session["IdUtente"].ToString() +
                "GROUP BY P.IdProdotto, P.ModelloProdotto ";
            }
            

            try
            {
                tab = ado.eseguiQuery(codSql, CommandType.Text);
                if (tab.Rows.Count > 0)
                {
                    contGraficiVendite.Visible = true;
                    serie = graficoVenditeProdotti.Series["serieVenditeTotaliProdotti"];
                    serie.Points.Clear();
                    for (int i = 0; i < tab.Rows.Count; i++)
                    {
                        serie.Points.AddXY(tab.Rows[i].ItemArray[1].ToString(), tab.Rows[i].ItemArray[2].ToString());
                        serie.Points[i].Label = "#PERCENT\n#VALX";
                    }
                    serie.ChartType = SeriesChartType.Pie;
                }
                else
                {
                    contGraficiVendite.Visible = false;
                    stampaErrori(msgGraficiVendite, "Nessun dato relativo ai Prodotti individuato");
                }
            }
            catch (Exception ex)
            {
                stampaErrori(msgGraficiVendite, "Errore: " + ex.Message);
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