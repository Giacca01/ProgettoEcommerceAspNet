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
    public partial class single_product : System.Web.UI.Page
    {
        /**********************/
        /* Routine Principale */
        /**********************/
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
                adoNet.impostaConnessione("App_Data/DBEcommerce.mdf");

            //Gestito fuori dal postback per poter agganciare l'evento al bottone di logout
            if (Session["IdUtente"] != null && Session["TipoUtente"] != null)
                gestUtenteLoggato();
            else
                gestUtenteNonLoggato();
            //Gestito fuori dal postback per poter agganciare l'evento al bottone di aggiunta al carrello 
            stampaDettaglioProdotto();
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
            else if (Session["TipoUtente"].ToString().ToUpper() == "CLIENTE")
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

        /*******************/
        /* Gestione Logout */
        /*******************/
        private void BtnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("login.aspx");
        }

        /*****************************/
        /* Stampa Dettaglio Prodotto */
        /*****************************/
        private void stampaDettaglioProdotto()
        {
            int codProd = -1;
            adoNet ado = new adoNet();
            string codSql = String.Empty;
            DataTable tab = new DataTable();
            string codHtml = String.Empty;
            string statoProd = String.Empty;

            //Controllo Parametri Get
            if (Int32.TryParse(Request.QueryString["codProd"], out codProd))
            {
                //Recupero i dati del prodotto
                codSql = "SELECT * FROM Prodotti AS P " +
                    "INNER JOIN Categorie AS C " +
                    "ON C.IdCategoria = P.IdCategoria " +
                    "INNER JOIN Fornitori AS F " +
                    "ON F.IdFornitore = P.IdFornitore " +
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
                        codHtml += "<h2>" + Convert.ToDouble(tab.Rows[0].ItemArray[7].ToString()) + "&euro;</h2>";
                        codHtml += "<ul class='list'>";
                        codHtml += "<li>";
                        codHtml += "<a><span>Fornitore:</span> " + tab.Rows[0].ItemArray[tab.Columns["NomeFornitore"].Ordinal].ToString() + "</a>";
                        codHtml += "</li>";
                        codHtml += "<li>";
                        codHtml += "<a><span>Categoria:</span> "+ tab.Rows[0].ItemArray[tab.Columns["DescrizioneCategoria"].Ordinal].ToString() + "</a>";
                        codHtml += "</li>";
                        codHtml += "<li>";
                        codHtml += "<a> <span>Stato:</span> "+ statoProd + "</a>";
                        codHtml += "</li>";
                        codHtml += "</ul>";

                        //Stampo il bottone di aggiunta solo se esso è disponibile
                        //e solo se l'utente loggato è un cliente
                        if (Session["TipoUtente"] != null)
                        {
                            if (statoProd != "Non Disponibile" && Session["TipoUtente"].ToString().ToUpper() == "CLIENTE")
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
                    stampaErrori(contMsgErroreCreazioneDetProd, "Errore: " +ex.Message);
                }
                
            }
            else
                stampaErrori(contMsgErroreCreazioneDetProd, "Il proddoto richiesto non è disponibile");
            
        }

        /******************************************/
        /* Gestione Aggiunta Prodotto Al Carrello */
        /******************************************/
        private void BtnAddCarrello_Click(object sender, EventArgs e)
        {
            //Controllo Parametri
            if (Int32.TryParse(Request.Form["qtaProdotto"], out int qta))
            {
                Session["CodProd"] = Request.QueryString["codProd"];
                Session["Qta"] = Request.Form["qtaProdotto"];
                Response.Redirect("carrello.aspx");
            }
            else
                stampaErrori(contMsgErroreCreazioneDetProd, "Impossibile aggiungere il prodotto");
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