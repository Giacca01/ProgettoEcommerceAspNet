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
    public partial class categorie : System.Web.UI.Page
    {
        /**********************/
        /* Routine Principale */
        /**********************/
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["IdUtente"] == null && Session["TipoUtente"] == null)
                    Response.Redirect("login.aspx");
                else if(Session["TipoUtente"].ToString().ToUpper() != "ADMIN")
                    Response.Redirect("prodotti.aspx");
                adoNet.impostaConnessione("App_Data/DBEcommerce.mdf");
            }
            //Gestito fuori dal postback per poter agganciare gli eventi ai bottoni delle categorie
            stampaElencoCategorie();
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

        /***************************/
        /* Stampa Elenco Categorie */
        /***************************/
        private void stampaElencoCategorie()
        {
            adoNet ado = new adoNet();
            string codSql = String.Empty;
            DataTable tab = new DataTable();
            TableRow riga;
            TableCell cella;
            LinkButton btnModifica;
            LinkButton btnElimina;
            CheckBox chkVal;

            msgElencoCategorie.Visible = false;
            codSql = "SELECT * FROM Categorie";
            corpoTabElencoCategorie.Controls.Clear();
            try
            {
                tab = ado.eseguiQuery(codSql, CommandType.Text);
                for (int i = 0; i < tab.Rows.Count; i++)
                {
                    riga = new TableRow();
                    
                    cella = new TableCell();
                    cella.Text = tab.Rows[i].ItemArray[1].ToString();
                    riga.Cells.Add(cella);

                    cella = new TableCell();
                    chkVal = new CheckBox();
                    if (tab.Rows[i].ItemArray[2].ToString() == "A")
                        chkVal.Checked = false;
                    else
                        chkVal.Checked = true;
                    chkVal.Enabled = false;
                    cella.Controls.Add(chkVal);
                    riga.Cells.Add(cella);

                    cella = new TableCell();
                    btnModifica = new LinkButton();
                    btnModifica.Text = "<i class='fa fa-edit'></i> Modifica";
                    btnModifica.CssClass = "btn btn-success circle";
                    btnModifica.Attributes.Add("data-codCat", tab.Rows[i].ItemArray[0].ToString());
                    btnModifica.Click += BtnModifica_Click;
                    cella.Controls.Add(btnModifica);
                    riga.Cells.Add(cella);
                    Label lbl = new Label();
                    lbl.Text = "&nbsp;";
                    cella.Controls.Add(lbl);
                    btnElimina = new LinkButton();
                    if (tab.Rows[i].ItemArray[2].ToString() == "A")
                    {
                        btnElimina.Text = "<i class='fa fa-edit'></i> Ripristina";
                        btnElimina.CssClass = "btn btn-success circle";
                        btnElimina.Attributes.Add("data-tipoOp", "0"); //Ripristina
                    }
                    else
                    {
                        btnElimina.Text = "<i class='fa fa-trash'></i> Elimina";
                        btnElimina.CssClass = "btn btn-danger circle";
                        btnElimina.Attributes.Add("data-tipoOp", "1");//Elimina
                    }
                    btnElimina.Attributes.Add("data-codCat", tab.Rows[i].ItemArray[0].ToString());
                    btnElimina.Click += BtnElimina_Click;
                    cella.Controls.Add(btnElimina);
                    riga.Cells.Add(cella);

                    corpoTabElencoCategorie.Controls.Add(riga);
                }
            }
            catch (Exception ex)
            {
                stampaErroriElencoCat("Errore: " + ex.Message);
            }
        }

        /***********************************/
        /* Gestione Eliminazione Categorie */
        /***********************************/
        protected void BtnElimina_Click(object sender, EventArgs e)
        {
            LinkButton btnSender = sender as LinkButton;
            adoNet ado = new adoNet();
            string codSql = String.Empty;
            string codCat = btnSender.Attributes["data-codCat"];
            string tipoOp = btnSender.Attributes["data-tipoOp"];

            if (Int32.TryParse(codCat, out int cdCat) && tipoOp != String.Empty)
            {
                if (tipoOp == "1")
                    codSql = "UPDATE Categorie SET ValCategoria = 'A' WHERE IdCategoria = " + cdCat;
                else if (tipoOp == "0")
                    codSql = "UPDATE Categorie SET ValCategoria = ' ' WHERE IdCategoria = " + cdCat;
                try
                {
                    ado.eseguiNonQuery(codSql, CommandType.Text);
                    ClientScript.RegisterStartupScript(typeof(Page), "autoPostback", ClientScript.GetPostBackEventReference(this, String.Empty), true);
                }
                catch (Exception ex)
                {
                    stampaErroriElencoCat("Errore: " + ex.Message);
                }
            }
            else
                stampaErroriElencoCat("Codice categoria non valido");
        }

        /***************************************/
        /* Caricamento Dati Modifica Categorie */
        /***************************************/
        private void BtnModifica_Click(object sender, EventArgs e)
        {
            LinkButton btnSender = sender as LinkButton;
            adoNet ado = new adoNet();
            string codSql = String.Empty;
            string codCat = btnSender.Attributes["data-codCat"];
            DataTable tab = new DataTable();

            if (Int32.TryParse(codCat, out int cdCat))
            {
                codSql = "SELECT * FROM Categorie WHERE IdCategoria = " + cdCat;
                try
                {
                    tab = ado.eseguiQuery(codSql, CommandType.Text);
                    descrizioneInsCategoria.Value = tab.Rows[0].ItemArray[1].ToString();
                    Session["IdCat"] = cdCat;
                    Session["DescCat"] = descrizioneInsCategoria.Value;
                    titoloSezInsCat.InnerText = "Modifica Categoria";
                    btnInsCat.Text = "<i class='fa fa-edit' aria-hidden='true'></i> Salva";
                    SetFocus(sezInsCat);
                }
                catch (Exception ex)
                {
                    stampaErroriElencoCat("Errore: " + ex.Message);
                }
            }
            else
                stampaErroriElencoCat("Codice categoria non valido");
        }

        /*******************/
        /* Gestione Errori */
        /*******************/
        private void stampaErroriElencoCat(string msgErrore)
        {
            msgElencoCategorie.InnerText = msgErrore;
            msgElencoCategorie.Attributes.Add("class", "alert alert-danger");
            msgElencoCategorie.Visible = true;
        }

        /**********************************/
        /* Gestione Inserimento Categorie */
        /**********************************/
        protected void btnInsCat_Click(object sender, EventArgs e)
        {
            adoNet ado = new adoNet();
            string codSql = String.Empty;
            DataTable tab = new DataTable();

            if (Session["IdCat"] != null || Session["DescCat"] != null)
            {
                //Controllo dati di input
                if (descrizioneInsCategoria.Value != String.Empty)
                {
                    if (Session["IdCat"] != null || Session["DescCat"] != null)
                    {
                        codSql = "SELECT COUNT(*) AS NCat FROM Categorie WHERE UPPER(DescrizioneCategoria) = '" + descrizioneInsCategoria.Value.ToUpper() + "' AND NOT IdCategoria = " + Session["IdCat"].ToString();
                        if (Convert.ToInt32(ado.eseguiScalar(codSql, CommandType.Text)) == 0)
                        {
                            codSql = "UPDATE Categorie SET DescrizioneCategoria = '" + descrizioneInsCategoria.Value + "' WHERE IdCategoria = " + Session["IdCat"].ToString();
                            ado.eseguiNonQuery(codSql, CommandType.Text);
                            Session["IdCat"] = null;
                            Session["DescCat"] = null;
                        }
                        else
                            stampaErroriInsCat("Esiste già una categoria con la medesima descrizione");
                    }
                    else
                        stampaErroriInsCat("Dati categoria mancanti. Ricaricare la pagina");
                }
                else
                    stampaErroriInsCat("Inserire una descrizione");
            }
            else
            {
                if (descrizioneInsCategoria.Value != String.Empty)
                {
                    codSql = "SELECT COUNT(*) AS NCat FROM Categorie WHERE UPPER(DescrizioneCategoria) = '" + descrizioneInsCategoria.Value.ToUpper() + "' ";
                    if (Convert.ToInt32(ado.eseguiScalar(codSql, CommandType.Text)) == 0)
                    {
                        codSql = "INSERT INTO Categorie " +
                            "VALUES ('" + descrizioneInsCategoria.Value + "', ' ')";
                        ado.eseguiNonQuery(codSql, CommandType.Text);
                    }
                    else
                        stampaErroriInsCat("Esiste già una categoria con la medesima descrizione");
                }
                else
                    stampaErroriInsCat("Inserire una descrizione");
            }
            descrizioneInsCategoria.Value = String.Empty;
            titoloSezInsCat.InnerText = "Inserimento Categoria";
            btnInsCat.Text = "<i class='fa fa-plus' aria-hidden='true'></i> Salva";
            ClientScript.RegisterStartupScript(typeof(Page), "autoPostback", ClientScript.GetPostBackEventReference(this, String.Empty), true);
        }

        /*******************/
        /* Gestione Errori */
        /*******************/
        private void stampaErroriInsCat(string msgErrore)
        {
            msgInsCat.InnerText = msgErrore;
            msgInsCat.Attributes.Add("class", "alert alert-danger");
            msgInsCat.Visible = true;
        }
    }
}