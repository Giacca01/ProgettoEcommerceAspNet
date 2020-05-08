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
using System.Text.RegularExpressions;

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
                }
            }

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
    }
}