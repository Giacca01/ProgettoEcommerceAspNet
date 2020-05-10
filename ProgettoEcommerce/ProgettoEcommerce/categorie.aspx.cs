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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
                adoNet.impostaConnessione("App_Data/DBEcommerce.mdf");
                

            if (Session["IdUtente"] == null && Session["TipoUtente"] == null)
                Response.Redirect("login.aspx");
            else if(Session["IdUtente"] != null && Session["TipoUtente"].ToString().ToUpper() == "ADMIN")
                stampaElencoCategorie();
            else
                Response.Redirect("prodotti.aspx");
        }

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
            sezModCategorie.Visible = false;
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
                    btnModifica.Click -= new EventHandler(BtnModifica_Click);
                    btnModifica.Click += new EventHandler(BtnModifica_Click);
                    btnModifica.CausesValidation = true;
                    cella.Controls.Add(btnModifica);
                    riga.Cells.Add(cella);
                    Label lbl = new Label();
                    lbl.Text = "&nbsp;";
                    cella.Controls.Add(lbl);
                    btnElimina = new LinkButton();
                    btnElimina.Text = "<i class='fa fa-trash'></i> Elimina";
                    btnElimina.CssClass = "btn btn-danger circle";
                    btnElimina.Attributes.Add("data-codCat", tab.Rows[i].ItemArray[0].ToString());
                    btnElimina.Click -= new EventHandler(BtnElimina_Click);
                    btnElimina.Click += new EventHandler(BtnElimina_Click);
                    btnElimina.CausesValidation = true;
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

        protected void BtnElimina_Click(object sender, EventArgs e)
        {
            LinkButton btnSender = sender as LinkButton;
            adoNet ado = new adoNet();
            string codSql = String.Empty;
            string codCat = btnSender.Attributes["data-codCat"];

            if (Int32.TryParse(codCat, out int cdCat))
            {
                codSql = "UPDATE Categorie SET ValCategoria = 'A' WHERE IdCategoria = " + cdCat;
                try
                {
                    ado.eseguiNonQuery(codSql, CommandType.Text);
                    Response.Redirect(Request.RawUrl);
                }
                catch (Exception ex)
                {
                    stampaErroriElencoCat("Errore: " + ex.Message);
                }
            }
            else
                stampaErroriElencoCat("Codice categoria non valido");
        }

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
                    txtDescModCat.Value = tab.Rows[0].ItemArray[1].ToString();
                    if (tab.Rows[0].ItemArray[2].ToString() == "A")
                        chkValModCat.Checked = false;
                    else
                        chkValModCat.Checked = true;
                    sezModCategorie.Visible = true;
                    Session["IdCat"] = cdCat;
                    Session["DescCat"] = txtDescModCat.Value;
                    SetFocus(sezModCategorie);
                }
                catch (Exception ex)
                {
                    stampaErroriElencoCat("Errore: " + ex.Message);
                }
            }
            else
                stampaErroriElencoCat("Codice categoria non valido");
        }

        private void stampaErroriElencoCat(string msgErrore)
        {
            msgElencoCategorie.InnerText = msgErrore;
            msgElencoCategorie.Attributes.Add("class", "alert alert-danger");
            msgElencoCategorie.Visible = true;
        }

        protected void btnInsCat_Click(object sender, EventArgs e)
        {
            adoNet ado = new adoNet();
            string codSql = String.Empty;
            DataTable tab = new DataTable();

            if (descrizioneInsCategoria.Value != String.Empty)
            {
                codSql = "SELECT COUNT(*) AS NCat FROM Categorie WHERE UPPER(DescrizioneCategoria) = '" + descrizioneInsCategoria.Value.ToUpper()+"' ";
                if (Convert.ToInt32(ado.eseguiScalar(codSql, CommandType.Text)) == 0)
                {
                    codSql = "INSERT INTO Categorie " +
                        "VALUES ('" + descrizioneInsCategoria.Value + "', ' ')";
                    ado.eseguiNonQuery(codSql, CommandType.Text);
                    descrizioneInsCategoria.Value = String.Empty;
                    msgInsCat.Visible = false;
                    stampaElencoCategorie();
                }
                else
                    stampaErroriInsCat("Esiste già una categoria con la medesima descrizione");
            }
            else
                stampaErroriInsCat("Inserire una descrizione");
        }

        private void stampaErroriInsCat(string msgErrore)
        {
            msgInsCat.InnerText = msgErrore;
            msgInsCat.Attributes.Add("class", "alert alert-danger");
            msgInsCat.Visible = true;
        }

        protected void btnModCat_Click(object sender, EventArgs e)
        {
            adoNet ado = new adoNet();
            string codSql = String.Empty;
            DataTable tab = new DataTable();
            char valCat = ' ';
            bool modOk;

            if (txtDescModCat.Value != String.Empty)
            {
                if (Session["IdCat"] != null || Session["DescCat"] != null)
                {
                    //if (Session["DescCat"].ToString().ToUpper() == txtDescModCat.Value.ToUpper())
                    //    modOk = true;
                    //else
                    //{
                    //    codSql = "SELECT COUNT(*) AS NCat FROM Categorie WHERE UPPER(DescrizioneCategoria) = '" + txtDescModCat.Value.ToUpper() + "' ";
                    //    if (Convert.ToInt32(ado.eseguiScalar(codSql, CommandType.Text)) == 0)
                    //        modOk = true;
                    //    else
                    //        modOk = false;
                    //}

                    codSql = "SELECT COUNT(*) AS NCat FROM Categorie WHERE UPPER(DescrizioneCategoria) = '" + txtDescModCat.Value.ToUpper() + "' AND NOT IdCategoria = "+ Session["IdCat"].ToString();
                    if (Convert.ToInt32(ado.eseguiScalar(codSql, CommandType.Text)) == 0)
                    {
                        if (chkValModCat.Checked)
                            valCat = ' ';
                        else
                            valCat = 'A';

                        codSql = "UPDATE Categorie SET DescrizioneCategoria = '" + txtDescModCat.Value + "', ValCategoria = '" + valCat + "' WHERE IdCategoria = " + Session["IdCat"].ToString();
                        ado.eseguiNonQuery(codSql, CommandType.Text);
                        Session["IdCat"] = null;
                        Session["DescCat"] = null;
                        msgModCat.Visible = false;
                        sezModCategorie.Visible = false;
                        txtDescModCat.Value = String.Empty;
                        chkValModCat.Checked = false;
                        Response.Redirect(Request.RawUrl);
                    }
                    else
                        stampaErroriModCat("Esiste già una categoria con la medesima descrizione");
                }
                else
                    stampaErroriModCat("Dati categoria mancanti. Ricaricare la pagina");
            }
            else
                stampaErroriModCat("Inserire una descrizione");
        }

        private void stampaErroriModCat(string msgErrore)
        {
            msgModCat.InnerText = msgErrore;
            msgModCat.Attributes.Add("class", "alert alert-danger");
            msgModCat.Visible = true;
        }
    }
}