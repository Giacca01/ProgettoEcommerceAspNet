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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
                adoNet.impostaConnessione("App_Data/DBEcommerce.mdf");

            if (Session["IdUtente"] == null && Session["TipoUtente"] == null)
                Response.Redirect("login.aspx");
            else if (Session["IdUtente"] != null && Session["TipoUtente"].ToString().ToUpper() == "ADMIN")
            {
                creaGraficoCategorie();
                creaGraficoProdotti();
            }
            else
                Response.Redirect("prodotti.aspx");
        }

        private void creaGraficoCategorie()
        {
            adoNet ado = new adoNet();
            string codSql = String.Empty;
            DataTable tab = new DataTable();
            Series serie = new Series();

            codSql = "SELECT C.IdCategoria, C.DescrizioneCategoria, COUNT(C.IdCategoria) AS NVendite " +
                "FROM Prodotti AS P " +
                "INNER JOIN Categorie AS C " +
                "ON P.IdCategoria = C.IdCategoria " +
                "INNER JOIN DettaglioOrdini AS D " +
                "ON D.IdProdotto = P.IdProdotto " +
                "WHERE P.ValProdotto = ' ' " +
                "AND C.ValCategoria = ' ' " +
                "GROUP BY C.IdCategoria, C.DescrizioneCategoria ";

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

        private void creaGraficoProdotti()
        {
            adoNet ado = new adoNet();
            string codSql = String.Empty;
            DataTable tab = new DataTable();
            Series serie = new Series();

            codSql = "SELECT P.IdProdotto, P.ModelloProdotto, COUNT(*) AS NVenditeProd " +
                "FROM Prodotti AS P " +
                "INNER JOIN DettaglioOrdini AS D " +
                "ON P.IdProdotto = D.IdProdotto " +
                "WHERE P.ValProdotto = ' ' AND D.ValDettaglioOrdini = ' ' " +
                "GROUP BY P.IdProdotto, P.ModelloProdotto ";

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

        private void stampaErrori(HtmlGenericControl contMsg, string msgErrore)
        {
            contMsg.InnerText = msgErrore;
            contMsg.Attributes.Add("class", "alert alert-danger");
            contMsg.Visible = true;
        }
    }
}