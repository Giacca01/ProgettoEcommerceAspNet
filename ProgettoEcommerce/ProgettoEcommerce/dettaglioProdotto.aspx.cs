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
    public partial class single_product : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                adoNet.impostaConnessione("App_Data/DBEcommerce.mdf");

            }
            if (Session["IdUtente"] != null && Session["TipoUtente"] != null)
            {
                gestUtenteLoggato();
            }
            else
            {
                gestUtenteNonLoggato();
            }
            stampaDettaglioProdotto();
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

        private void gestUtenteNonLoggato()
        {
            navUtenteCarrrello.Visible = false;
        }

        private void BtnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("login.aspx");
        }

        private void stampaDettaglioProdotto()
        {
            int codProd = -1;
            adoNet ado = new adoNet();
            string codSql = String.Empty;
            DataTable tab = new DataTable();
            string codHtml = String.Empty;
            string statoProd = String.Empty;

            if (Int32.TryParse(Request.QueryString["codProd"], out codProd))
            {
                codSql = "SELECT * FROM Prodotti AS P " +
                    "INNER JOIN Categorie AS C " +
                    "ON C.IdCategoria = P.IdCategoria " +
                    "WHERE P.ValProdotto = ' ' AND P.IdProdotto = " + codProd;
                try
                {
                    tab = ado.eseguiQuery(codSql, CommandType.Text);
                    if (tab.Rows.Count == 1)
                    {
                        if (Convert.ToInt32(tab.Rows[0].ItemArray[tab.Columns["QtaGiacenza"].Ordinal].ToString()) > 0)
                            statoProd = "Disponibile";
                        else
                            statoProd = "Non Disponibile";
                        codHtml = "<img class='d-block w-100' src='img/product/" + tab.Rows[0].ItemArray[4].ToString() + "'/>";
                        contImgProd.InnerHtml = codHtml;
                        codHtml = "<h3>" + tab.Rows[0].ItemArray[1].ToString() + "</h3>";
                        codHtml += "<h2>" + string.Format("{0:N2}", Convert.ToDouble(tab.Rows[0].ItemArray[7].ToString())) + "&euro;</h2>";
                        codHtml += "<ul class='list'>";
                        codHtml += "<li>";
                        codHtml += "<a><span>Categoria</span> "+ tab.Rows[0].ItemArray[tab.Columns["DescrizioneCategoria"].Ordinal].ToString() + "</a>";
                        codHtml += "</li>";
                        codHtml += "<li>";
                        codHtml += "<a> <span>Stato:</span> "+ statoProd + "</a>";
                        codHtml += "</li>";
                        codHtml += "</ul>";
                        

                        if (statoProd != "Non Disponibile")
                        {
                            codHtml += "<div class='product_count'>";
                            codHtml += "<label for='qtaProdotto'>Quantit&agrave;:</label>";
                            codHtml += "<input type='number' name='qtaProdotto' id='qtaProdotto' maxlength='12' value='1' title='Quantit&agrave;:' class='input-text qty' />";
                            codHtml += "</div>";
                            codHtml += "<div class='card_area'>";
                            contDetProd.InnerHtml = codHtml;
                            LinkButton btnAddCarrello = new LinkButton();
                            btnAddCarrello.Text = "Aggiungi al carrello";
                            btnAddCarrello.CssClass = "main_btn";
                            btnAddCarrello.Click += BtnAddCarrello_Click;
                            contDetProd.Controls.Add(btnAddCarrello);
                        }
                        else
                            contDetProd.InnerHtml = codHtml;                       
                        descProdotto.InnerText = tab.Rows[0].ItemArray[2].ToString();
                        linkCurrentPage.HRef = "dettaglioProdotto.aspx?codProd=" + tab.Rows[0].ItemArray[0].ToString();
                        contDettaglioProdotto.Visible = true;
                        contMsgNoProd.Visible = false;
                    }
                    else
                        throw new Exception("Nessun prodotto individuato");
                }
                catch (Exception ex)
                {
                    stampaErroreCreazioneDetProd("Errore: "+ex.Message);
                }
                
            }
            else
                stampaErroreCreazioneDetProd("Il proddoto richiesto non è disponibile");
            
        }

        private void BtnAddCarrello_Click(object sender, EventArgs e)
        {
            
            if (Int32.TryParse(Request.Form["qtaProdotto"], out int qta))
            {
                Session["CodProd"] = Request.QueryString["codProd"];
                Session["Qta"] = Request.Form["qtaProdotto"];
                Response.Redirect("carrello.aspx");
            }
            else
            {
                //Stampare errore in boostrap alert
            }
        }

        private void stampaErroreCreazioneDetProd(string msgErrore)
        {
            contDettaglioProdotto.Visible = false;
            contMsgNoProd.Visible = true;
            contMsgErroreCreazioneDetProd.InnerText = msgErrore;
        }
    }
}