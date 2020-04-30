using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//Using Specifiche
using adoNetWebSQlServer;
using System.Data;

namespace ProgettoEcommerce
{
    public partial class category : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
                adoNet.impostaConnessione("App_Data/DBEcommerce.mdf");
            if (Session["IdUtente"] != null && Session["TipoUtente"] != null)
            {
                gestUtenteLoggato();
            }
            else
            {
                gestUtenteNonLoggato();
            }
            ricercaIniziale();
            stampaElCategorie();
        }

        private void gestUtenteLoggato()
        {
            navUtenteCarrrello.Visible = true;
            LinkButton btnLogout = new LinkButton();
            btnLogout.CssClass = "nav-link";
            btnLogout.Text = "<i class='fa fa-sign-out' aria-hidden='true'></i> Esci";
            btnLogout.Click += BtnLogout_Click;
            dropDownLogout.Controls.Add(btnLogout);
        }

        private void BtnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("login.aspx");
        }

        private void gestUtenteNonLoggato()
        {
            navUtenteCarrrello.Visible = false;
        }

        private void ricercaIniziale()
        {
            adoNet ado = new adoNet();
            string codSql = String.Empty;

            codSql = "SELECT * FROM Prodotti WHERE ValProdotto = ' '";
            try
            {
                stampaElProdotti(ado.eseguiQuery(codSql, CommandType.Text));
            }
            catch (Exception ex)
            {
                msgErroreElProd.InnerHtml = "Attenzione!!! Errore: " + ex.Message;
            }
        }

        private void stampaElProdotti(DataTable elProd)
        {
            string codHtml = String.Empty;
            double ausPrezzo = 0;

            for (int i = 0; i < elProd.Rows.Count; i++)
            {
                if (elProd.Rows[i].ItemArray[8].ToString() != String.Empty)
                    ausPrezzo = (Convert.ToDouble(elProd.Rows[i].ItemArray[8].ToString()) * 100) / (Convert.ToDouble(elProd.Rows[i].ItemArray[7].ToString()));
                else
                    ausPrezzo = Convert.ToDouble(elProd.Rows[i].ItemArray[7].ToString());
                codHtml += "<div class='col-lg-4 col-md-6'>" +
                    "<div class='single-product'>" +
                    "<div class='product-img'>" +
                    "<img class='card-img' src='img/product/'" + elProd.Rows[i].ItemArray[4].ToString() + "' alt=''/>" +
                    "<div class='p_icon'>" +
                    "<a href='dettaglioProdotto.aspx?codProd=" + elProd.Rows[i].ItemArray[0].ToString() + "'>" +
                    "<i class='ti-eye'></i>" +
                    "</a>" +
                    "</div>" +
                    "</div>" +
                    "<div class='product-btm'>" +
                    "<a href='dettaglioProdotto.aspx?codProd=" + elProd.Rows[i].ItemArray[0].ToString() + "'>" +
                    "<h4>"+ elProd.Rows[i].ItemArray[1].ToString() + "</h4>" +
                    "</a>" +
                    "</div>" +
                    "<div class='mt-3'>" +
                    "<span class='mr-4'>"+ elProd.Rows[i].ItemArray[7].ToString() + "&euro;</span>" +
                    "<del>"+ string.Format("{0:N2}%", ausPrezzo) + "&euro;</del>" +
                    "</div>" +
                    "</div>"+
                    "</div>"+
                    "</div>";     
            }
            contProdotti.InnerHtml = codHtml;
        }

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
                for (int i = 0; i < tab.Rows.Count; i++)
                {
                    codHtml += "<li>" +
                        "<a href='#'>" + tab.Rows[i].ItemArray[1].ToString() + "</a>" +
                        "</li>";
                }
                elencoCategorieRic.InnerHtml = codHtml;
            }
            catch (Exception ex)
            {
                msgRicProd.InnerHtml = "Attenzione!!! Errore: " + ex.Message;
            }
        }

        protected void btnRicercaProdotto_Click(object sender, EventArgs e)
        {

        }
    }
}