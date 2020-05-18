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
    public partial class storicoOrdini : System.Web.UI.Page
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
                else if (Session["TipoUtente"].ToString().ToUpper() != "CLIENTE")
                    Response.Redirect("prodotti.aspx");
            }
            //Gestito fuori dal postback per poter agganciare l'evento al bottone di logout
            gestUtenteLoggato();
            //Gestito fuori dal postback per poter agganciare gli eventi ai bottoni
            stampaElencoOrdini();
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
            navAndamentoVendite.Visible = false;
            navCategorie.Visible = false;
            navGestioneOrdini.Visible = false;
            navGestioneProdotti.Visible = false;
            navGestioneUtenti.Visible = false;
            navTipiCarte.Visible = false;
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

        /*************************/
        /* Stampa Storico Ordini */
        /*************************/
        private void stampaElencoOrdini()
        {
            adoNet ado = new adoNet();
            string codSql = String.Empty;
            DataTable tab = new DataTable();
            TableRow riga;
            TableCell cella;
            LinkButton btnEliminaOrdine;
            LinkButton btnDetOrdine;
            CheckBox chkVal;

            msgElencoOrdini.Visible = false;
            sezDetOrdine.Visible = false;
            corpoTabElencoOrdini.Controls.Clear();
            codSql = "SELECT * " +
                "FROM Ordini AS O " +
                "INNER JOIN Carte AS C " +
                "ON C.IdCarta = O.IdCarta " +
                "WHERE O.IdCliente = " + Session["IdUtente"].ToString();
            try
            {
                tab = ado.eseguiQuery(codSql, CommandType.Text);
                for (int i = 0; i < tab.Rows.Count; i++)
                {
                    riga = new TableRow();

                    cella = new TableCell();
                    cella.Text = Convert.ToDateTime(tab.Rows[i].ItemArray[1].ToString()).ToString(@"dd/MM/yyyy");
                    riga.Cells.Add(cella);

                    cella = new TableCell();
                    if (tab.Rows[i].ItemArray[2].ToString() != String.Empty)
                        cella.Text = Convert.ToDateTime(tab.Rows[i].ItemArray[2].ToString()).ToString(@"dd/MM/yyyy");
                    else
                        cella.Text = String.Empty;
                    riga.Cells.Add(cella);

                    cella = new TableCell();
                    cella.Text = tab.Rows[i].ItemArray[3].ToString();
                    riga.Cells.Add(cella);

                    cella = new TableCell();
                    cella.Text = tab.Rows[i].ItemArray[tab.Columns["CodiceCarta"].Ordinal].ToString();
                    riga.Cells.Add(cella);

                    cella = new TableCell();
                    chkVal = new CheckBox();
                    if (tab.Rows[i].ItemArray[6].ToString() == "A")
                        chkVal.Checked = false;
                    else
                        chkVal.Checked = true;
                    chkVal.Enabled = false;
                    cella.Controls.Add(chkVal);
                    riga.Cells.Add(cella);

                    cella = new TableCell();
                    if (tab.Rows[i].ItemArray[6].ToString() != "A" && tab.Rows[i].ItemArray[2].ToString() == String.Empty)
                    {
                        btnEliminaOrdine = new LinkButton();
                        btnEliminaOrdine.Text = "<i class='fa fa-trash'></i> Elimina";
                        btnEliminaOrdine.CssClass = "btn btn-danger circle";
                        btnEliminaOrdine.Attributes.Add("data-codOrdine", tab.Rows[i].ItemArray[0].ToString());
                        btnEliminaOrdine.Click += BtnEliminaOrdine_Click;
                        cella.Controls.Add(btnEliminaOrdine);
                        riga.Cells.Add(cella);
                        Label lbl = new Label();
                        lbl.Text = "&nbsp;";
                        cella.Controls.Add(lbl);
                    }

                    btnDetOrdine = new LinkButton();
                    btnDetOrdine.Attributes.Add("data-codOrdine", tab.Rows[i].ItemArray[0].ToString());
                    btnDetOrdine.Text = "<i class='fa fa-eye'></i> Dettagli";
                    btnDetOrdine.Attributes.Add("data-TipoOp", "DETTAGLI");
                    btnDetOrdine.CssClass = "btn btn-success circle";
                    btnDetOrdine.Click += BtnDetOrdine_Click;
                    cella.Controls.Add(btnDetOrdine);
                    riga.Cells.Add(cella);

                    corpoTabElencoOrdini.Controls.Add(riga);
                }
            }
            catch (Exception ex)
            {
                stampaErrori(msgElencoOrdini, "Errore: " + ex.Message);
            }
        }

        /**************************************/
        /* Gestione Apertura Dettaglio Ordine */
        /**************************************/
        private void BtnDetOrdine_Click(object sender, EventArgs e)
        {
            LinkButton btnSender = sender as LinkButton;
            if (btnSender.Attributes["data-TipoOp"].ToUpper() == "DETTAGLI")
            {
                if (Int32.TryParse(btnSender.Attributes["data-codOrdine"], out int cdOrdine))
                    stampaDettagliOrdine(cdOrdine);
                else
                    stampaErrori(msgElencoOrdini, "Codice ordine non valido");
                btnSender.Attributes["data-TipoOp"] = "CHIUDI";
                btnSender.Text = "<i class='fa fa-times'></i> Chiudi";
            }
            else if(btnSender.Attributes["data-TipoOp"].ToUpper() == "CHIUDI")
            {
                msgDetOrdine.Visible = false;
                corpoTabDetOrdine.Controls.Clear();
                sezDetOrdine.Visible = false;
                btnSender.Attributes["data-TipoOp"] = "DETTAGLI";
                btnSender.Text = "<i class='fa fa-eye'></i> Dettagli";
            }
        }

        /************************************/
        /* Gestione Stampa Dettaglio Ordine */
        /************************************/
        private void stampaDettagliOrdine(int codOrdine)
        {
            adoNet ado = new adoNet();
            string codSql = String.Empty;
            DataTable tab = new DataTable();
            TableRow riga;
            TableCell cella;
            CheckBox chkVal;

            msgDetOrdine.Visible = false;
            corpoTabDetOrdine.Controls.Clear();
            codSql = "SELECT * " +
                "FROM DettaglioOrdini AS D " +
                "INNER JOIN Prodotti AS P " +
                "ON D.IdProdotto = P.IdProdotto " +
                "WHERE D.IdOrdine = " + codOrdine;

            try
            {
                tab = ado.eseguiQuery(codSql, CommandType.Text);
                for (int i = 0; i < tab.Rows.Count; i++)
                {
                    riga = new TableRow();

                    cella = new TableCell();
                    cella.Text = tab.Rows[i].ItemArray[tab.Columns["ModelloProdotto"].Ordinal].ToString();
                    riga.Cells.Add(cella);

                    cella = new TableCell();
                    cella.Text = tab.Rows[i].ItemArray[2].ToString();
                    riga.Cells.Add(cella);

                    cella = new TableCell();
                    cella.Text = tab.Rows[i].ItemArray[3].ToString();
                    riga.Cells.Add(cella);

                    cella = new TableCell();
                    chkVal = new CheckBox();
                    if (tab.Rows[i].ItemArray[4].ToString() == "A")
                        chkVal.Checked = false;
                    else
                        chkVal.Checked = true;
                    chkVal.Enabled = false;
                    cella.Controls.Add(chkVal);
                    riga.Cells.Add(cella);

                    corpoTabDetOrdine.Controls.Add(riga);
                }
            }
            catch (Exception ex)
            {
                stampaErrori(msgElencoOrdini, "Errore: " + ex.Message);
            }

            sezDetOrdine.Visible = true;
            SetFocus(sezDetOrdine);
        }

        /********************************/
        /* Gestione Eliminazione Ordine */
        /********************************/
        private void BtnEliminaOrdine_Click(object sender, EventArgs e)
        {
            LinkButton btnSender = sender as LinkButton;
            string codOrdine = btnSender.Attributes["data-codOrdine"];

            if (Int32.TryParse(codOrdine, out int cdOrdine))
            {
                Session["codOrdine"] = cdOrdine;
                ClientScript.RegisterStartupScript(typeof(Page), "apriModal", "apriModal();", true);
            }
            else
                stampaErrori(msgElencoOrdini, "Codice ordine non valido");
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

        /*****************************************/
        /* Gestione Conferma Eliminazione Ordine */
        /*****************************************/
        protected void btnConfermaElimOrdine_Click(object sender, EventArgs e)
        {
            adoNet ado = new adoNet();
            string codSql = String.Empty;
            string codOrdine = Session["codOrdine"].ToString();

            if (Int32.TryParse(codOrdine, out int cdOrdine))
            {
                codSql = "UPDATE Ordini SET ValOrdine = 'A' WHERE IdOrdine = " + cdOrdine;
                try
                {
                    ado.eseguiNonQuery(codSql, CommandType.Text);
                    ClientScript.RegisterStartupScript(typeof(Page), "autoPostback", ClientScript.GetPostBackEventReference(this, String.Empty), true);
                }
                catch (Exception ex)
                {
                    stampaErrori(msgConfermaElimOrdine, "Errore: " + ex.Message);
                }
            }
            else
                stampaErrori(msgConfermaElimOrdine, "Codice ordine non valido");
        }
    }
}