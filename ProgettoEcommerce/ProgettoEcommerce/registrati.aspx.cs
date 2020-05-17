using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//Using Specifiche
using adoNetWebSQlServer;
using System.Data;
using System.Text.RegularExpressions;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Web.UI.HtmlControls;

namespace ProgettoEcommerce
{
    public partial class registrati : System.Web.UI.Page
    {
        /**********************/
        /* Routine Principale */
        /**********************/
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //Se l'utente è già loggato lo mando ai prodotti
                if (Session["IdUtente"] != null && Session["TipoUtente"] != null)
                    Response.Redirect("prodotti.aspx");
                adoNet.impostaConnessione("App_Data/DBEcommerce.mdf");
                caricaComboCitta();
            }

        }

        /******************************/
        /* Gestione Caricamento Città */
        /******************************/
        private void caricaComboCitta()
        {
            adoNet ado = new adoNet();
            string codSql = String.Empty;
            DataTable tab = new DataTable();

            try
            {
                codSql = "SELECT * FROM Citta WHERE ValCitta = ' '";
                tab = ado.eseguiQuery(codSql, CommandType.Text);
                lstCittaReg.DataSource = tab;
                lstCittaReg.DataTextField = "DescCitta";
                lstCittaReg.DataValueField = "IdCitta";
                lstCittaReg.DataBind();
            }
            catch (Exception ex)
            {
                stampaErrori(msgReg, "Attenzione!!! Errore: " + ex.Message);
            }
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

        /**************************/
        /* Gestione Registrazione */
        /**************************/
        protected void btnRegistrati_Click(object sender, EventArgs e)
        {
            DateTime dataNas = DateTime.MinValue;
            Regex regTel = new Regex(@"^[0-9]{10,10}$");
            Regex regPwd = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#\$%\^&\*])(?=.{8,})");
            adoNet ado = new adoNet();
            string codSql = String.Empty;
            DataTable tab = new DataTable();
            bool cognOk;

            //Controllo dati di input
            try
            {
                if (lstTipoUtenteReg.SelectedValue == "fornitore" || lstTipoUtenteReg.SelectedValue == "cliente" || lstTipoUtenteReg.SelectedValue == "admin")
                {
                    if (nomeReg.Value != String.Empty)
                    {
                        if (lstTipoUtenteReg.SelectedValue == "cliente" || lstTipoUtenteReg.SelectedValue == "admin")
                        {
                            if (cognomeReg.Value != String.Empty)
                                cognOk = true;
                            else
                            {
                                stampaErrori(msgReg, "Inserire il Cognome dell' utente");
                                cognOk = false;
                            }
                        }
                        else
                            cognOk = true;

                        if (cognOk)
                        {
                            if (DateTime.TryParse(dataNascitaReg.Value, out dataNas))
                            {
                                if (DateTime.Now.AddYears(-18) >= dataNas)
                                {
                                    if (validaEmail(mailReg.Value))
                                    {
                                        if (regTel.IsMatch(telReg.Value))
                                        {
                                            if (usernameReg.Value != String.Empty)
                                            {
                                                if (regPwd.IsMatch(pwdReg.Value))
                                                {
                                                    if (viaReg.Value != String.Empty)
                                                    {
                                                        if (civicoReg.Value != String.Empty)
                                                        {
                                                            if (lstCittaReg.SelectedIndex != -1)
                                                            {
                                                                //Controli su db per evitare duplicazione dei dati
                                                                if (lstTipoUtenteReg.SelectedValue == "fornitore")
                                                                    codSql = "SELECT * FROM Fornitori WHERE Telefono = '" + telReg.Value + "'";
                                                                else if (lstTipoUtenteReg.SelectedValue == "cliente")
                                                                    codSql = "SELECT * FROM Clienti WHERE TelefonoCliente = '" + telReg.Value + "'";
                                                                else
                                                                    codSql = "SELECT * FROM Admin WHERE TelefonoAdmin = '" + telReg.Value + "'";

                                                                tab = ado.eseguiQuery(codSql, CommandType.Text);

                                                                if (tab.Rows.Count == 0)
                                                                {
                                                                    if (lstTipoUtenteReg.SelectedValue == "fornitore")
                                                                        codSql = "SELECT * FROM Fornitori WHERE Email = '" + mailReg.Value + "'";
                                                                    else if (lstTipoUtenteReg.SelectedValue == "cliente")
                                                                        codSql = "SELECT * FROM Clienti WHERE MailCliente = '" + mailReg.Value + "'";
                                                                    else
                                                                        codSql = "SELECT * FROM Admin WHERE MailAdmin = '" + mailReg.Value + "'";

                                                                    tab = ado.eseguiQuery(codSql, CommandType.Text);

                                                                    if (tab.Rows.Count == 0)
                                                                    {
                                                                        if (lstTipoUtenteReg.SelectedValue == "fornitore")
                                                                            codSql = "SELECT * FROM Fornitori WHERE UserFornitore = '" + usernameReg.Value + "'";
                                                                        else if (lstTipoUtenteReg.SelectedValue == "cliente")
                                                                            codSql = "SELECT * FROM Clienti WHERE UserCliente = '" + usernameReg.Value + "'";
                                                                        else
                                                                            codSql = "SELECT * FROM Admin WHERE UserAdmin = '" + usernameReg.Value + "'";

                                                                        tab = ado.eseguiQuery(codSql, CommandType.Text);

                                                                        if (tab.Rows.Count == 0)
                                                                        {
                                                                            //Inserimento Utente su DB
                                                                            if (lstTipoUtenteReg.SelectedValue == "fornitore")
                                                                            {
                                                                                codSql = "INSERT INTO Fornitori ([NomeFornitore], [Telefono], [Email], [UserFornitore], [PwdFornitore], [ViaFornitore], [CivicoFornitore], [IdCitta], [ValFornitore])" +
                                                                                "VALUES ('" + nomeReg.Value + "', '" + telReg.Value + "', '" + mailReg.Value + "', '" + usernameReg.Value + "', '" + calcolaMD5(pwdReg.Value) + "', '" + viaReg.Value + "', '" + civicoReg.Value + "', " + lstCittaReg.Value + ", ' ')";
                                                                            }
                                                                            else if (lstTipoUtenteReg.SelectedValue == "cliente")
                                                                            {
                                                                                codSql = "INSERT INTO Clienti ([NomeCliente], [CognomeCliente], [DataNascitaCliente], [TelefonoCliente], [MailCliente], [UserCliente], [PwdCliente], [ViaCliente], [CivicoCliente], [IdCittaClienti], [ValCliente])" +
                                                                                "VALUES ('" + nomeReg.Value + "', '" + cognomeReg.Value + "', '" + dataNas.ToString("yyyy/MM/dd") + "', '" + telReg.Value + "', '" + mailReg.Value + "', '" + usernameReg.Value + "', '" + calcolaMD5(pwdReg.Value) + "', '" + viaReg.Value + "', '" + civicoReg.Value + "', " + lstCittaReg.Value + ", ' ')";
                                                                            }
                                                                            else
                                                                            {
                                                                                codSql = "INSERT INTO Admin ([NomeAdmin], [CognomeAdmin], [DataNascitaAdmin], [TelefonoAdmin], [MailAdmin], [UserAdmin], [PwdAdmin], [ViaAdmin], [CivicoAdmin], [IdCittaAdmin], [ValAdmin])" +
                                                                                "VALUES ('" + nomeReg.Value + "', '" + cognomeReg.Value + "', '" + dataNas.ToString("yyyy/MM/dd") + "', '" + telReg.Value + "', '" + mailReg.Value + "', '" + usernameReg.Value + "', '" + calcolaMD5(pwdReg.Value) + "', '" + viaReg.Value + "', '" + civicoReg.Value + "', " + lstCittaReg.Value + ", ' ')";
                                                                            }

                                                                            ado.eseguiNonQuery(codSql, CommandType.Text);
                                                                            Response.Redirect("login.aspx");
                                                                        }
                                                                        else
                                                                            stampaErrori(msgReg, "Username non disponibile");
                                                                    }
                                                                    else
                                                                        stampaErrori(msgReg, "Email non disponibile");
                                                                }
                                                                else
                                                                    stampaErrori(msgReg, "Telefono non disponibile");
                                                            }
                                                            else
                                                                stampaErrori(msgReg, "Selezionare la Città di residenza");
                                                        }
                                                        else
                                                            stampaErrori(msgReg, "Inserire il Numero Civico");
                                                    }
                                                    else
                                                        stampaErrori(msgReg, "Inserire la via di residenza");
                                                }
                                                else
                                                    stampaErrori(msgReg, "Inserire una password valida");
                                            }
                                            else
                                                stampaErrori(msgReg, "Inserire uno username");
                                        }
                                        else
                                            stampaErrori(msgReg, "Inserire un telefono valido");
                                    }
                                    else
                                        stampaErrori(msgReg, "Inserire una mail valida");
                                }
                                else
                                    stampaErrori(msgReg, "Occorre aver compiuto almeno 18 anni");
                            }
                            else
                                stampaErrori(msgReg, "Data di nascita non valida");
                        }
                    }
                    else
                        stampaErrori(msgReg, "Inserire il Nome dell' utente");
                }
                else
                    stampaErrori(msgReg, "Tipo utente non valido");
            }
            catch (Exception ex)
            {
                stampaErrori(msgReg, "Attenzione!!! Errore: " + ex.Message);
            }
            
        }

        /*********************/
        /* Validazione Email */
        /*********************/
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

        /*********************************************/
        /* Conversione della stringa in input in MD5 */
        /*********************************************/
        public string calcolaMD5(string strIn)
        {
            string ret = String.Empty;
            int I = 0;

            //Buffer: vettori di byte, sono usati per le conversioni per considerare ogni elemento "uguale" dal punto di vista della memoria occupata
            Byte[] buf1;
            Byte[] buf2;

            MD5CryptoServiceProvider md5Obj = new MD5CryptoServiceProvider();

            //Serializzazione: conversione di un oggetto in un flusso di byte
            //Step 1: Serializzo la stringa di input
            buf1 = new Byte[strIn.Length];
            foreach (char c in strIn)
            {
                buf1[I++] = Convert.ToByte(c);
            }

            //Step 2: Converto in MD5
            //computeHash riceve come parametro una stringa serializzata
            //e restituisce la corrispondente stringa md5 (sempre serializzata)
            buf2 = md5Obj.ComputeHash(buf1); //calcola l'hash di tutti gli elementi di buf1 e li carica su MD5 dove quindi abbiamo la stringa MD5

            //Step 3: Deserializzo il risultato
            foreach (Byte b in buf2)
            {
                //Posso visualizzare il contenuto di buf1 in 3 modo diversi
                ret += b.ToString("X2"); //X2 indica che sto applicando una conversione in esadecimale (X) su 2 cifre (2) (cambia poco anche senza il 2)
            }

            return ret;
        }

        /**********************/
        /* Gestione Dati Form */
        /**********************/
        protected void lstTipoUtenteReg_SelectedIndexChanged(object sender, EventArgs e)
        {
            //In base al tipo utente nascondo alcuni campi del form
            //che il fornitore non deve inserire
            if (lstTipoUtenteReg.SelectedValue == "fornitore")
                gestFormFornitore();
            else
                gestFormAdmiCliente();
        }

        /********************************/
        /* Gestione Dati Form Fornitore */
        /********************************/
        private void gestFormFornitore()
        {
            cognomeReg.Visible = false;
            contDataNascitaReg.Visible = false;
        }

        /******************************/
        /* Gestione Dati Form Cliente */
        /******************************/
        private void gestFormAdmiCliente()
        {
            cognomeReg.Visible = true;
            contDataNascitaReg.Visible = true;
        }
    }
}