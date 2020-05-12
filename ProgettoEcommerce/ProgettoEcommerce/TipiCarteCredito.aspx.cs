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
    public partial class TipiCarteCredito : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                adoNet.impostaConnessione("App_Data/DBEcommerce.mdf");
                if (Session["IdUtente"] == null && Session["TipoUtente"] == null)
                    Response.Redirect("login.aspx");
                else if(Session["TipoUtente"].ToString().ToUpper() != "ADMIN")
                    Response.Redirect("prodotti.aspx");
            }

            stampaElencoTipiCarte();

        }

        private void stampaElencoTipiCarte()
        {
            adoNet ado = new adoNet();
            string codSql = String.Empty;
            DataTable tab = new DataTable();
            TableRow riga;
            TableCell cella;
            LinkButton btnModifica;
            LinkButton btnGestCat;
            CheckBox chkVal;

            msgElencoTipiCarte.Visible = false;
            codSql = "SELECT * FROM TipiCarte";
            corpoTabElencoTipiCarte.Controls.Clear();
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
                    btnModifica.Attributes.Add("data-codTipoCarta", tab.Rows[i].ItemArray[0].ToString());
                    btnModifica.Click += BtnModifica_Click;
                    cella.Controls.Add(btnModifica);
                    riga.Cells.Add(cella);
                    Label lbl = new Label();
                    lbl.Text = "&nbsp;";
                    cella.Controls.Add(lbl);
                    btnGestCat = new LinkButton();
                    btnGestCat.Attributes.Add("data-codTipoCarta", tab.Rows[i].ItemArray[0].ToString());
                    if (tab.Rows[i].ItemArray[2].ToString() == "A")
                    {
                        btnGestCat.Text = "<i class='fa fa-edit'></i> Ripristina";
                        btnGestCat.CssClass = "btn btn-success circle";
                        btnGestCat.Attributes.Add("data-tipoOp", "0"); //Ripristina
                    }
                    else
                    {
                        btnGestCat.Text = "<i class='fa fa-trash'></i> Elimina";
                        btnGestCat.CssClass = "btn btn-danger circle";
                        btnGestCat.Attributes.Add("data-tipoOp", "1");//Elimina
                    }
                    btnGestCat.Click += BtnGestCat_Click;
                    cella.Controls.Add(btnGestCat);
                    riga.Cells.Add(cella);

                    corpoTabElencoTipiCarte.Controls.Add(riga);
                }
            }
            catch (Exception ex)
            {
                stampaErrori(msgElencoTipiCarte, "Errore: " + ex.Message);
            }
        }

        private void BtnModifica_Click(object sender, EventArgs e)
        {
            LinkButton btnSender = sender as LinkButton;
            adoNet ado = new adoNet();
            string codSql = String.Empty;
            string codTipoCarta = btnSender.Attributes["data-codTipoCarta"];
            DataTable tab = new DataTable();

            if (Int32.TryParse(codTipoCarta, out int cdTipoCarta))
            {
                codSql = "SELECT * FROM TipiCarte WHERE IdTipoCarte = " + cdTipoCarta;
                try
                {
                    tab = ado.eseguiQuery(codSql, CommandType.Text);
                    descrizioneInsModTipiCarte.Value = tab.Rows[0].ItemArray[1].ToString();
                    titoloSezInsModTipiCarte.InnerText = "Modifica Tipo Carta";
                    btnInsModTipiCarte.Text = "<i class='fa fa-edit' aria-hidden='true'></i> Salva";
                    Session["IdTipoCarta"] = cdTipoCarta;
                    Session["DescTipoCarta"] = descrizioneInsModTipiCarte.Value;
                    SetFocus(sezInsModTipiCarte);
                }
                catch (Exception ex)
                {
                    stampaErrori(msgElencoTipiCarte, "Errore: " + ex.Message);
                }
            }
            else
                stampaErrori(msgElencoTipiCarte, "Codice categoria non valido");
        }

        private void BtnGestCat_Click(object sender, EventArgs e)
        {
            LinkButton btnSender = sender as LinkButton;
            adoNet ado = new adoNet();
            string codSql = String.Empty;
            string codTipoCarta = btnSender.Attributes["data-codTipoCarta"];
            string tipoOp = btnSender.Attributes["data-tipoOp"];

            if (Int32.TryParse(codTipoCarta, out int cdTipoCarta) && tipoOp != String.Empty)
            {
                if (tipoOp == "1")
                    codSql = "UPDATE TipiCarte SET ValTipoCarte = 'A' WHERE IdTipoCarte = " + cdTipoCarta;
                else if (tipoOp == "0")
                    codSql = "UPDATE TipiCarte SET ValTipoCarte = ' ' WHERE IdTipoCarte = " + cdTipoCarta;
                try
                {
                    ado.eseguiNonQuery(codSql, CommandType.Text);
                    Response.Redirect(Request.RawUrl);
                }
                catch (Exception ex)
                {
                    stampaErrori(msgElencoTipiCarte, "Errore: " + ex.Message);
                }
            }
            else
                stampaErrori(msgElencoTipiCarte, "Dati mancanti. Ricaricare la pagina");
        }

        private void stampaErrori(HtmlGenericControl contMsg, string msgErrore)
        {
            contMsg.InnerText = msgErrore;
            contMsg.Attributes.Add("class", "alert alert-danger");
            contMsg.Visible = true;
        }

        protected void btnInsModTipiCarte_Click(object sender, EventArgs e)
        {
            adoNet ado = new adoNet();
            string codSql = String.Empty;
            DataTable tab = new DataTable();

            //Se questo parametro sono settati significa che si vuole fare una modifica
            //altrimenti faccio l'inserimento
            if (Session["IdTipoCarta"] != null)
            {
                codSql = "SELECT COUNT(*) AS NTipiCarte FROM TipiCarte WHERE UPPER(DescTipoCarte) = '" + descrizioneInsModTipiCarte.Value.ToUpper() + "' AND NOT IdTipoCarte = "+ Session["IdTipoCarta"].ToString();
                if (Convert.ToInt32(ado.eseguiScalar(codSql, CommandType.Text)) == 0)
                {
                    codSql = "UPDATE TipiCarte SET DescTipoCarte = '" + descrizioneInsModTipiCarte.Value + "' WHERE IdTipoCarte = " + Session["IdTipoCarta"].ToString();
                    ado.eseguiNonQuery(codSql, CommandType.Text);
                    Session["IdTipoCarta"] = null;
                    Session["DescTipoCarta"] = null;
                    Response.Redirect(Request.RawUrl);
                }
                else
                    stampaErrori(msgInsModTipiCarte, "Esiste già un tipo carta di credito con la medesima descrizione");
            }
            else
            {
                if (descrizioneInsModTipiCarte.Value != String.Empty)
                {
                    codSql = "SELECT COUNT(*) AS NTipiCarte FROM TipiCarte WHERE UPPER(DescTipoCarte) = '" + descrizioneInsModTipiCarte.Value.ToUpper() + "' ";
                    if (Convert.ToInt32(ado.eseguiScalar(codSql, CommandType.Text)) == 0)
                    {
                        codSql = "INSERT INTO TipiCarte " +
                            "VALUES ('" + descrizioneInsModTipiCarte.Value + "', ' ')";
                        ado.eseguiNonQuery(codSql, CommandType.Text);
                        Response.Redirect(Request.RawUrl);
                    }
                    else
                        stampaErrori(msgInsModTipiCarte, "Esiste già un tipo carta di credito con la medesima descrizione");
                }
                else
                    stampaErrori(msgInsModTipiCarte, "Inserire una descrizione");
            }

            descrizioneInsModTipiCarte.Value = String.Empty;
            titoloSezInsModTipiCarte.InnerText = "Inserimento Tipo Carta";
            btnInsModTipiCarte.Text = "<i class='fa fa-plus' aria-hidden='true'></i> Salva";
        }
    }
}