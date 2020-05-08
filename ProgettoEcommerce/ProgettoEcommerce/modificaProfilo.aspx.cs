//Using Specifiche
using adoNetWebSQlServer;
using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ProgettoEcommerce
{
    public partial class ModificaProfilo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                adoNet.impostaConnessione("App_Data/DBEcommerce.mdf");
                if (Session["IdUtente"] == null && Session["TipoUtente"] == null)
                    Response.Redirect("login.aspx");
                else
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "impostaLista", "impostaLista();", true);
                    stampaFormModifica();
                    caricaComboCitta();
                    if (Session["TipoUtente"].ToString() == "Cliente")
                        caricaTipiCarte();
                    else
                    {
                        sezElencoCarte.Visible = false;
                        sezInsCarta.Visible = false;
                    }
                }

            }

            caricaCarteDiCredito();

            Page.ClientScript.RegisterStartupScript(GetType(), "impostaLista", "impostaLista();", true);

        }

        private void caricaComboCitta()
        {
            adoNet ado = new adoNet();
            string codSql = String.Empty;
            DataTable tab = new DataTable();

            try
            {
                codSql = "SELECT * FROM Citta WHERE ValCitta = ' '";
                tab = ado.eseguiQuery(codSql, CommandType.Text);
                lstCittaModProfilo.DataSource = tab;
                lstCittaModProfilo.DataTextField = "DescCitta";
                lstCittaModProfilo.DataValueField = "IdCitta";
                lstCittaModProfilo.DataBind();
            }
            catch (Exception ex)
            {
                stampaErrori(msgModificaProfilo, "Attenzione!!! Errore: " + ex.Message);
            }
        }

        private void stampaFormModifica()
        {
            adoNet ado = new adoNet();
            string codSql = String.Empty;
            DataTable tab = new DataTable();
            int k = 1;

            msgModificaProfilo.Visible = false;
            msgModificaProfilo.InnerText = String.Empty;
            if (Session["TipoUtente"].ToString() == "Cliente")
                codSql = "SELECT * FROM Clienti WHERE IdCliente = " + Session["IdUtente"].ToString();
            else if(Session["TipoUtente"].ToString() == "Fornitore")
                codSql = "SELECT * FROM Fornitori WHERE IdFornitore = " + Session["IdUtente"].ToString();
            else
                codSql = "SELECT * FROM Admin WHERE IdAdmin = " + Session["IdUtente"].ToString();

            try
            {
                tab = ado.eseguiQuery(codSql, CommandType.Text);
                for (int i = 0; i < tab.Rows.Count; i++)
                {
                    nomeModProfilo.Value = tab.Rows[i].ItemArray[k].ToString();
                    k++;
                    if (Session["TipoUtente"].ToString() == "Fornitore")
                    {
                        contCognomeModProfilo.Visible = false;
                        contDataNascitaModProfilo.Visible = false;
                    }
                    else
                    {
                        
                        cognomeModProfilo.Value = tab.Rows[i].ItemArray[k].ToString();
                        k++;
                        dataNascitaModProfilo.Value = Convert.ToDateTime(tab.Rows[i].ItemArray[k].ToString()).ToString(@"yyyy-MM-dd");
                        k++;
                    }

                    if (Session["TipoUtente"].ToString() == "Fornitore")
                    {
                        mailModProfilo.Value = tab.Rows[i].ItemArray[k].ToString();
                        k++;
                        telModProfilo.Value = tab.Rows[i].ItemArray[k].ToString();
                    }
                    else
                    {
                        telModProfilo.Value = tab.Rows[i].ItemArray[k].ToString();
                        k++;
                        mailModProfilo.Value = tab.Rows[i].ItemArray[k].ToString();
                    }
                    k++;
                    usernameModProfilo.Value = tab.Rows[i].ItemArray[k].ToString();
                    k += 2;
                    viaModProfilo.Value = tab.Rows[i].ItemArray[k].ToString();
                    k++;
                    civicoModProfilo.Value = tab.Rows[i].ItemArray[k].ToString();
                    k++;
                    Page.ClientScript.RegisterStartupScript(GetType(), "refreshListaCitta", "refreshListaCitta("+ tab.Rows[i].ItemArray[k].ToString()+");", true);
                }
            }
            catch (Exception ex)
            {
                stampaErrori(msgModificaProfilo, "Errore: " + ex.Message);
            }
        }

        private void caricaTipiCarte()
        {
            adoNet ado = new adoNet();
            string codSql = String.Empty;
            DataTable tab = new DataTable();

            try
            {
                codSql = "SELECT * FROM TipiCarte WHERE ValTipoCarte = ' '";
                tab = ado.eseguiQuery(codSql, CommandType.Text);
                lstTipoCarta.DataSource = tab;
                lstTipoCarta.DataTextField = "DescTipoCarte";
                lstTipoCarta.DataValueField = "IdTipoCarte";
                lstTipoCarta.DataBind();
            }
            catch (Exception ex)
            {
                stampaErrori(msgModificaProfilo, "Attenzione!!! Errore: " + ex.Message);
            }
        }

        private void caricaCarteDiCredito()
        {
            adoNet ado = new adoNet();
            string codSql = String.Empty;
            DataTable tab = new DataTable();
            TableRow riga;
            TableCell cella;
            LinkButton btnGestioneCarta;
            CheckBox chkVal;

            sezElencoCarte.Visible = true;
            sezInsCarta.Visible = true;
            codSql = "SELECT C.*, T.* FROM Carte AS C " +
                "INNER JOIN TipiCarte AS T " +
                "ON C.IdTipoCarta = T.IdTipoCarte " +
                "WHERE IdCliente = " + Session["IdUtente"].ToString();
            tab = ado.eseguiQuery(codSql, CommandType.Text);
            for (int i = 0; i < tab.Rows.Count; i++)
            {
                riga = new TableRow();

                cella = new TableCell();
                cella.Text = tab.Rows[i].ItemArray[6].ToString();
                riga.Cells.Add(cella);

                cella = new TableCell();
                cella.Text = tab.Rows[i].ItemArray[1].ToString();
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

                cella = new TableCell();
                btnGestioneCarta = new LinkButton();
                btnGestioneCarta.Attributes.Add("data-codCarta", tab.Rows[i].ItemArray[0].ToString());
                if (tab.Rows[i].ItemArray[4].ToString() == "A")
                {
                    btnGestioneCarta.Text = "<i class='fa fa-edit'></i> Ripristina";
                    btnGestioneCarta.CssClass = "btn btn-success circle";
                    btnGestioneCarta.Attributes.Add("data-tipoOp", "0"); //Ripristina
                }
                else
                {
                    btnGestioneCarta.Text = "<i class='fa fa-trash'></i> Elimina";
                    btnGestioneCarta.CssClass = "btn btn-danger circle";
                    btnGestioneCarta.Attributes.Add("data-tipoOp", "1");//Elimina
                }
                btnGestioneCarta.Click += BtnGestioneCarta_Click;
                cella.Controls.Add(btnGestioneCarta);
                riga.Cells.Add(cella);

                corpoTabElencoCarte.Controls.Add(riga);
            }
        }

        private void BtnGestioneCarta_Click(object sender, EventArgs e)
        {
            LinkButton btnSender = sender as LinkButton;
            adoNet ado = new adoNet();
            string codSql = String.Empty;
            string codCat = btnSender.Attributes["data-codCarta"];
            string tipoOp = btnSender.Attributes["data-tipoOp"];

            if (Int32.TryParse(codCat, out int cdCat) && tipoOp != String.Empty)
            {
                if (tipoOp == "1")
                    codSql = "UPDATE Carte SET ValCarta = 'A' WHERE IdCarta = " + cdCat;
                else if(tipoOp == "0")
                    codSql = "UPDATE Carte SET ValCarta = ' ' WHERE IdCarta = " + cdCat;
                try
                {
                    ado.eseguiNonQuery(codSql, CommandType.Text);
                    Response.Redirect(Request.RawUrl);
                }
                catch (Exception ex)
                {
                    stampaErrori(msgElencoCarte, "Errore: " + ex.Message);
                }
            }
            else
                stampaErrori(msgElencoCarte, "Dati mancanti. Ricaricare la pagina");
        }

        private void stampaErrori(HtmlGenericControl contMsg, string msgErrore)
        {
            contMsg.InnerText = msgErrore;
            contMsg.Attributes.Add("class", "alert alert-danger");
            contMsg.Visible = true;
        }

        protected void btnModificaProfilo_Click(object sender, EventArgs e)
        {
            DateTime dataNas = DateTime.MinValue;
            Regex regTel = new Regex(@"^[0-9]{10,10}$");
            adoNet ado = new adoNet();
            string codSql = String.Empty;
            DataTable tab = new DataTable();
            bool datiClienteAdmin;

            try
            {
                if (nomeModProfilo.Value != String.Empty)
                {
                    if (Session["TipoUtente"].ToString() == "Cliente" || Session["TipoUtente"].ToString() == "Admin")
                    {
                        if (cognomeModProfilo.Value != String.Empty)
                        {
                            if (DateTime.TryParse(dataNascitaModProfilo.Value, out dataNas))
                            {
                                if (DateTime.Now.AddYears(-18) >= dataNas)
                                    datiClienteAdmin = true;
                                else
                                {
                                    stampaErrori(msgModificaProfilo, "Occorre aver compiuto almeno 18 anni");
                                    datiClienteAdmin = false;
                                }
                            }
                            else
                            {
                                stampaErrori(msgModificaProfilo, "Inserire la Data di Nascita dell' utente");
                                datiClienteAdmin = false;
                            }
                        }
                        else
                        {
                            stampaErrori(msgModificaProfilo, "Inserire il Cognome dell' utente");
                            datiClienteAdmin = false;
                        }
                    }
                    else
                        datiClienteAdmin = true;

                    if (datiClienteAdmin)
                    {
                        if (validaEmail(mailModProfilo.Value))
                        {
                            if (regTel.IsMatch(telModProfilo.Value))
                            {
                                if (usernameModProfilo.Value != String.Empty)
                                {
                                    if (viaModProfilo.Value != String.Empty)
                                    {
                                        if (civicoModProfilo.Value != String.Empty)
                                        {
                                            if (lstCittaModProfilo.SelectedIndex != -1)
                                            {
                                                if (Session["TipoUtente"].ToString() == "Fornitore")
                                                    codSql = "SELECT * FROM Fornitori WHERE Telefono = '" + telModProfilo.Value + "' AND NOT IdFornitore = " + Session["IdUtente"].ToString();
                                                else if (Session["TipoUtente"].ToString() == "Cliente")
                                                    codSql = "SELECT * FROM Clienti WHERE TelefonoCliente = '" + telModProfilo.Value + "' AND NOT IdCliente = " + Session["IdUtente"].ToString();
                                                else
                                                    codSql = "SELECT * FROM Admin WHERE TelefonoAdmin = '" + telModProfilo.Value + "' AND NOT IdAdmin = " + Session["IdUtente"].ToString();

                                                tab = ado.eseguiQuery(codSql, CommandType.Text);

                                                if (tab.Rows.Count == 0)
                                                {
                                                    if (Session["TipoUtente"].ToString() == "Fornitore")
                                                        codSql = "SELECT * FROM Fornitori WHERE UPPER(Email) = '" + mailModProfilo.Value.ToUpper() + "' AND NOT IdFornitore = " + Session["IdUtente"].ToString();
                                                    else if (Session["TipoUtente"].ToString() == "Cliente")
                                                        codSql = "SELECT * FROM Clienti WHERE UPPER(MailCliente) = '" + mailModProfilo.Value.ToUpper() + "' AND NOT IdCliente = " + Session["IdUtente"].ToString();
                                                    else
                                                        codSql = "SELECT * FROM Admin WHERE UPPER(MailAdmin) = '" + mailModProfilo.Value.ToUpper() + "' AND NOT IdAdmin = " + Session["IdUtente"].ToString();

                                                    tab = ado.eseguiQuery(codSql, CommandType.Text);

                                                    if (tab.Rows.Count == 0)
                                                    {
                                                        if (Session["TipoUtente"].ToString() == "Fornitore")
                                                            codSql = "SELECT * FROM Fornitori WHERE UPPER(UserFornitore) = '" + usernameModProfilo.Value.ToUpper() + "' AND NOT IdFornitore = " + Session["IdUtente"].ToString();
                                                        else if (Session["TipoUtente"].ToString() == "Cliente")
                                                            codSql = "SELECT * FROM Clienti WHERE UPPER(UserCliente) = '" + usernameModProfilo.Value.ToUpper() + "' AND NOT IdCliente = " + Session["IdUtente"].ToString();
                                                        else
                                                            codSql = "SELECT * FROM Admin WHERE UPPER(UserAdmin) = '" + usernameModProfilo.Value.ToUpper() + "' AND NOT IdAdmin = " + Session["IdUtente"].ToString();

                                                        tab = ado.eseguiQuery(codSql, CommandType.Text);

                                                        if (tab.Rows.Count == 0)
                                                        {
                                                            if (Session["TipoUtente"].ToString() == "Fornitore")
                                                                codSql = "UPDATE Fornitori SET NomeFornitore = '" + nomeModProfilo.Value + "', Email = '" + mailModProfilo.Value + "', Telefono = '" + telModProfilo.Value + "', UserFornitore = '" + usernameModProfilo.Value + "', ViaFornitore = '" + viaModProfilo.Value + "', CivicoFornitore = '" + civicoModProfilo.Value + "', IdCitta = " + lstCittaModProfilo.Value + " WHERE IdFornitore = " + Session["IdUtente"].ToString();
                                                            else if (Session["TipoUtente"].ToString() == "Cliente")
                                                                codSql = "UPDATE Clienti SET NomeCliente = '" + nomeModProfilo.Value + "', CognomeCliente = '" + cognomeModProfilo.Value + "', DataNascitaCliente = '" + Convert.ToDateTime(dataNascitaModProfilo.Value).ToString(@"yyyy/MM/dd") + "', TelefonoCliente = '" + telModProfilo.Value + "', MailCliente = '" + mailModProfilo.Value + "', UserCliente = '" + usernameModProfilo.Value + "', ViaCliente = '" + viaModProfilo.Value + "', CivicoCliente = '" + civicoModProfilo.Value + "', IdCittaClienti = " + lstCittaModProfilo.Value + " WHERE IdCliente = " + Session["IdUtente"].ToString();
                                                            else
                                                                codSql = "UPDATE Admin SET NomeAdmin = '" + nomeModProfilo.Value + "', CognomeAdmin = '" + cognomeModProfilo.Value + "', DataNascitaAdmin = '" + Convert.ToDateTime(dataNascitaModProfilo.Value).ToString(@"yyyy/MM/dd") + "', TelefonoAdmin = '" + telModProfilo.Value + "', MailAdmin = '" + mailModProfilo.Value + "', UserAdmin = '" + usernameModProfilo.Value + "', ViaAdmin = '" + viaModProfilo.Value + "', CivicoAdmin = '" + civicoModProfilo.Value + "', IdCittaAdmin = " + lstCittaModProfilo.Value + " WHERE IdAdmin = " + Session["IdUtente"].ToString();
                                                            ado.eseguiNonQuery(codSql, CommandType.Text);
                                                            msgModificaProfilo.InnerText = "Modifica Completata";
                                                            msgModificaProfilo.Attributes.Add("class", "alert alert-success");
                                                            msgModificaProfilo.Visible = true;
                                                        }
                                                        else
                                                            stampaErrori(msgModificaProfilo, "Username non disponibile");
                                                    }
                                                    else
                                                        stampaErrori(msgModificaProfilo, "Email non disponibile");
                                                }
                                                else
                                                    stampaErrori(msgModificaProfilo, "Telefono non disponibile");
                                            }
                                            else
                                                stampaErrori(msgModificaProfilo, "Inserire la Città di Residenza");
                                        }
                                        else
                                            stampaErrori(msgModificaProfilo, "Inserire il Numero Civico di Residenza");
                                    }
                                    else
                                        stampaErrori(msgModificaProfilo, "Inserire la Via di Residenza");
                                }
                                else
                                    stampaErrori(msgModificaProfilo, "Inserire lo Username");
                            }
                            else
                                stampaErrori(msgModificaProfilo, "Inserire un Telefono valido");
                        }
                        else
                            stampaErrori(msgModificaProfilo, "Inserire una Mail valida");
                    }
                }
                else
                    stampaErrori(msgModificaProfilo, "Inserire il Nome dell' utente");
            }
            catch (Exception ex)
            {
                stampaErrori(msgModificaProfilo, "Errore: " + ex.Message);
            }
        }

        private bool validaEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        protected void btnInsCarta_Click(object sender, EventArgs e)
        {
            adoNet ado = new adoNet();
            string codSql = String.Empty;
            DataTable tab = new DataTable();
            Regex regCodCarta = new Regex(@"^[0-9]*$");

            if (codiceInsCarta.Value.Length >= 13 && codiceInsCarta.Value.Length <= 16)
            {
                if (regCodCarta.IsMatch(codiceInsCarta.Value))
                {
                    if (lstTipoCarta.SelectedIndex != -1)
                    {
                        codSql = "SELECT COUNT(*) AS NCarte FROM Carte WHERE CodiceCarta = '" + codiceInsCarta.Value.ToString() + "' AND IdTipoCarta = " + lstTipoCarta.Value.ToString() + " AND IdCliente = " + Session["IdUtente"].ToString();
                        if (Convert.ToInt32(ado.eseguiScalar(codSql, CommandType.Text)) == 0)
                        {
                            codSql = "INSERT INTO Carte " +
                                "VALUES (" + codiceInsCarta.Value.ToString() + ", "+ lstTipoCarta.Value.ToString() + ", "+ Session["IdUtente"].ToString() + ", ' ')";
                            ado.eseguiNonQuery(codSql, CommandType.Text);
                            codiceInsCarta.Value = String.Empty;
                            lstTipoCarta.SelectedIndex = -1;
                            msgInsCarta.Visible = false;
                            Response.Redirect(Request.RawUrl);
                        }
                        else
                            stampaErrori(msgInsCarta, "Dati carta non validi");
                    }
                    else
                        stampaErrori(msgInsCarta, "Inserire il Tipo Carta");
                }
                else
                    stampaErrori(msgInsCarta, "Il Codice Carta deve essere numerico");
            }
            else
                stampaErrori(msgInsCarta, "Il Codice Carta deve essere compreso tra 13 e 16 cifre");
        }
    }
}