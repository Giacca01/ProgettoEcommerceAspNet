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
    public partial class gestioneUtenti : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
                adoNet.impostaConnessione("App_Data/DBEcommerce.mdf");

            if (Session["IdUtente"] == null && Session["TipoUtente"] == null)
                Response.Redirect("login.aspx");
            else if (Session["IdUtente"] != null && Session["TipoUtente"].ToString().ToUpper() == "ADMIN")
            {
                stampaElencoClienti();
                stampaElencoFornitori();
            }
            else
                Response.Redirect("prodotti.aspx");
        }

        private void stampaElencoClienti()
        {
            adoNet ado = new adoNet();
            string codSql = String.Empty;
            DataTable tab = new DataTable();
            TableRow riga;
            TableCell cella;
            LinkButton btnGestCliente;
            CheckBox chkVal;

            msgElencoClienti.Visible = false;
            codSql = "SELECT * FROM Clienti AS C INNER JOIN Citta AS CI " +
                "ON C.IdCittaClienti = CI.IdCitta";
            corpoTabElencoClienti.Controls.Clear();
            try
            {
                tab = ado.eseguiQuery(codSql, CommandType.Text);
                for (int i = 0; i < tab.Rows.Count; i++)
                {
                    riga = new TableRow();

                    cella = new TableCell();
                    cella.Text = tab.Rows[i].ItemArray[tab.Columns["NomeCliente"].Ordinal].ToString();
                    riga.Cells.Add(cella);

                    cella = new TableCell();
                    cella.Text = tab.Rows[i].ItemArray[tab.Columns["CognomeCliente"].Ordinal].ToString();
                    riga.Cells.Add(cella);

                    cella = new TableCell();
                    cella.Text = Convert.ToDateTime(tab.Rows[i].ItemArray[tab.Columns["DataNascitaCliente"].Ordinal].ToString()).ToString(@"dd/MM/yyyy");
                    riga.Cells.Add(cella);

                    cella = new TableCell();
                    cella.Text = tab.Rows[i].ItemArray[tab.Columns["TelefonoCliente"].Ordinal].ToString();
                    riga.Cells.Add(cella);

                    cella = new TableCell();
                    cella.Text = tab.Rows[i].ItemArray[tab.Columns["MailCliente"].Ordinal].ToString();
                    riga.Cells.Add(cella);

                    cella = new TableCell();
                    cella.Text = tab.Rows[i].ItemArray[tab.Columns["UserCliente"].Ordinal].ToString();
                    riga.Cells.Add(cella);

                    cella = new TableCell();
                    cella.Text = tab.Rows[i].ItemArray[tab.Columns["ViaCliente"].Ordinal].ToString();
                    riga.Cells.Add(cella);

                    cella = new TableCell();
                    cella.Text = tab.Rows[i].ItemArray[tab.Columns["CivicoCliente"].Ordinal].ToString();
                    riga.Cells.Add(cella);

                    cella = new TableCell();
                    cella.Text = tab.Rows[i].ItemArray[tab.Columns["DescCitta"].Ordinal].ToString();
                    riga.Cells.Add(cella);

                    cella = new TableCell();
                    chkVal = new CheckBox();
                    if (tab.Rows[i].ItemArray[tab.Columns["ValCliente"].Ordinal].ToString() == "A")
                        chkVal.Checked = false;
                    else
                        chkVal.Checked = true;
                    chkVal.Enabled = false;
                    cella.Controls.Add(chkVal);
                    riga.Cells.Add(cella);

                    cella = new TableCell();
                    btnGestCliente = new LinkButton();
                    btnGestCliente.Attributes.Add("data-codCliente", tab.Rows[i].ItemArray[tab.Columns["IdCliente"].Ordinal].ToString());
                    if (tab.Rows[i].ItemArray[tab.Columns["ValCliente"].Ordinal].ToString() == "A")
                    {
                        btnGestCliente.Text = "<i class='fa fa-edit'></i> Ripristina";
                        btnGestCliente.CssClass = "btn btn-success circle";
                        btnGestCliente.Attributes.Add("data-tipoOp", "0"); //Ripristina
                    }
                    else
                    {
                        btnGestCliente.Text = "<i class='fa fa-trash'></i> Elimina";
                        btnGestCliente.CssClass = "btn btn-danger circle";
                        btnGestCliente.Attributes.Add("data-tipoOp", "1");//Elimina
                    }
                    btnGestCliente.Click += BtnGestCliente_Click;
                    cella.Controls.Add(btnGestCliente);
                    riga.Cells.Add(cella);

                    corpoTabElencoClienti.Controls.Add(riga);
                }
            }
            catch (Exception ex)
            {
                stampaErrori(msgElencoClienti, "Errore: " + ex.Message);
            }
        }

        private void BtnGestCliente_Click(object sender, EventArgs e)
        {
            LinkButton btnSender = sender as LinkButton;
            adoNet ado = new adoNet();
            string codSql = String.Empty;
            string codCliente = btnSender.Attributes["data-codCliente"];
            string tipoOp = btnSender.Attributes["data-tipoOp"];

            if (Int32.TryParse(codCliente, out int cdCliente) && tipoOp != String.Empty)
            {
                if (tipoOp == "1")
                    codSql = "UPDATE Clienti SET ValCliente = 'A' WHERE IdCliente = " + cdCliente;
                else if(tipoOp == "0") 
                    codSql = "UPDATE Clienti SET ValCliente = ' ' WHERE IdCliente = " + cdCliente;

                try
                {
                    ado.eseguiNonQuery(codSql, CommandType.Text);
                    Response.Redirect(Request.RawUrl);
                }
                catch (Exception ex)
                {
                    stampaErrori(msgElencoClienti, "Errore: " + ex.Message);
                }
            }
            else
                stampaErrori(msgElencoClienti, "Parametri mancanti, ricaricare la pagina");
        }

        private void stampaElencoFornitori()
        {
            adoNet ado = new adoNet();
            string codSql = String.Empty;
            DataTable tab = new DataTable();
            TableRow riga;
            TableCell cella;
            LinkButton btnGestioneFornitore;
            CheckBox chkVal;

            msgElencoFornitori.Visible = false;
            codSql = "SELECT * FROM Fornitori AS F INNER JOIN Citta AS CI " +
                "ON F.IdCitta = CI.IdCitta";
            corpoTabElencoFornitori.Controls.Clear();
            try
            {
                tab = ado.eseguiQuery(codSql, CommandType.Text);
                for (int i = 0; i < tab.Rows.Count; i++)
                {
                    riga = new TableRow();

                    cella = new TableCell();
                    cella.Text = tab.Rows[i].ItemArray[tab.Columns["NomeFornitore"].Ordinal].ToString();
                    riga.Cells.Add(cella);

                    cella = new TableCell();
                    cella.Text = tab.Rows[i].ItemArray[tab.Columns["Telefono"].Ordinal].ToString();
                    riga.Cells.Add(cella);

                    cella = new TableCell();
                    cella.Text = tab.Rows[i].ItemArray[tab.Columns["Email"].Ordinal].ToString();
                    riga.Cells.Add(cella);

                    cella = new TableCell();
                    cella.Text = tab.Rows[i].ItemArray[tab.Columns["UserFornitore"].Ordinal].ToString();
                    riga.Cells.Add(cella);

                    cella = new TableCell();
                    cella.Text = tab.Rows[i].ItemArray[tab.Columns["ViaFornitore"].Ordinal].ToString();
                    riga.Cells.Add(cella);

                    cella = new TableCell();
                    cella.Text = tab.Rows[i].ItemArray[tab.Columns["CivicoFornitore"].Ordinal].ToString();
                    riga.Cells.Add(cella);

                    cella = new TableCell();
                    cella.Text = tab.Rows[i].ItemArray[tab.Columns["DescCitta"].Ordinal].ToString();
                    riga.Cells.Add(cella);

                    cella = new TableCell();
                    chkVal = new CheckBox();
                    if (tab.Rows[i].ItemArray[tab.Columns["ValFornitore"].Ordinal].ToString() == "A")
                        chkVal.Checked = false;
                    else
                        chkVal.Checked = true;
                    chkVal.Enabled = false;
                    cella.Controls.Add(chkVal);
                    riga.Cells.Add(cella);

                    cella = new TableCell();
                    btnGestioneFornitore = new LinkButton();
                    btnGestioneFornitore.Attributes.Add("data-codFornitore", tab.Rows[i].ItemArray[tab.Columns["IdFornitore"].Ordinal].ToString());
                    if (tab.Rows[i].ItemArray[tab.Columns["ValFornitore"].Ordinal].ToString() == "A")
                    {
                        btnGestioneFornitore.Text = "<i class='fa fa-edit'></i> Ripristina";
                        btnGestioneFornitore.CssClass = "btn btn-success circle";
                        btnGestioneFornitore.Attributes.Add("data-tipoOp", "0"); //Ripristina
                    }
                    else
                    {
                        btnGestioneFornitore.Text = "<i class='fa fa-trash'></i> Elimina";
                        btnGestioneFornitore.CssClass = "btn btn-danger circle";
                        btnGestioneFornitore.Attributes.Add("data-tipoOp", "1"); //Elimina
                    }

                    btnGestioneFornitore.Click += BtnGestioneFornitore_Click;
                    cella.Controls.Add(btnGestioneFornitore);
                    riga.Cells.Add(cella);

                    corpoTabElencoFornitori.Controls.Add(riga);
                }
            }
            catch (Exception ex)
            {
                stampaErrori(msgElencoFornitori, "Errore: " + ex.Message);
            }
        }

        private void BtnGestioneFornitore_Click(object sender, EventArgs e)
        {
            LinkButton btnSender = sender as LinkButton;
            adoNet ado = new adoNet();
            string codSql = String.Empty;
            string codFornitore = btnSender.Attributes["data-codFornitore"];
            string tipoOp = btnSender.Attributes["data-tipoOp"];

            if (Int32.TryParse(codFornitore, out int cdFornitore) && tipoOp != String.Empty)
            {
                if (tipoOp == "1")
                    codSql = "UPDATE Fornitori SET ValFornitore = 'A' WHERE IdFornitore = " + cdFornitore;
                else if(tipoOp == "0")
                    codSql = "UPDATE Fornitori SET ValFornitore = ' ' WHERE IdFornitore = " + cdFornitore;

                try
                {
                    ado.eseguiNonQuery(codSql, CommandType.Text);
                    Response.Redirect(Request.RawUrl);
                }
                catch (Exception ex)
                {
                    stampaErrori(msgElencoClienti, "Errore: " + ex.Message);
                }
            }
            else
                stampaErrori(msgElencoClienti, "Parametri mancanti, ricaricare la pagina");
        }

        private void stampaErrori(HtmlGenericControl contMsg, string msgErrore)
        {
            contMsg.InnerText = msgErrore;
            contMsg.Attributes.Add("class", "alert alert-danger");
            contMsg.Visible = true;
        }
    }
}