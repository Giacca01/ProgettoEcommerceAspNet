using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//Using Specifiche
using adoNetWebSQlServer;
using System.Security.Cryptography; //funzioni di sicurezza per la cifratura
using System.Data;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;

namespace ProgettoEcommerce
{
    public partial class reimpostaPwd : System.Web.UI.Page
    {
        /**********************/
        /* Routine Principale */
        /**********************/
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                adoNet.impostaConnessione("App_Data/DBEcommerce.mdf");
                if (Session["IdUtente"] != null && Session["TipoUtente"] != null)
                    Response.Redirect("prodotti.aspx");
            }
        }

        /************************************/
        /* Gestione Reimpostazione Password */
        /************************************/
        protected void btnReimpPwd_Click(object sender, EventArgs e)
        {
            string codSql = String.Empty;
            adoNet ado = new adoNet();
            DataTable tab = new DataTable();
            string newPwd = String.Empty;
            bool errore = false;
            Regex regTel = new Regex(@"^[0-9]{10,10}$");
            Regex regPwd = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#\$%\^&\*])(?=.{8,})");

            //Controllo dati di input
            if (usernameReimpPwd.Value != String.Empty)
            {
                if (validaEmail(emailReimpPwd.Value))
                {
                    if (regTel.IsMatch(telReimpPwd.Value))
                    {
                        if (regPwd.IsMatch(pwdReimpPwd.Value))
                        {
                            if (lstTipoUtenteReimpPwd.SelectedIndex != -1)
                            {
                                //Controllo se l'utente esiste su db
                                if (lstTipoUtenteReimpPwd.Value == "admin")
                                    codSql = "SELECT IdAdmin FROM Admin WHERE UserAdmin = '" + usernameReimpPwd.Value + "' AND MailAdmin = '" + emailReimpPwd.Value + "' AND TelefonoAdmin = '" + telReimpPwd.Value + "' AND ValAdmin = ' '";
                                else if (lstTipoUtenteReimpPwd.Value == "cliente")
                                    codSql = "SELECT IdCliente FROM Clienti WHERE UserCliente = '" + usernameReimpPwd.Value + "' AND MailCliente = '" + emailReimpPwd.Value + "' AND TelefonoCliente = '" + telReimpPwd.Value + "' AND ValCliente = ' '";
                                else if (lstTipoUtenteReimpPwd.Value == "fornitore")
                                    codSql = "SELECT IdFornitore FROM Fornitori WHERE UserFornitore = '" + usernameReimpPwd.Value + "' AND Email = '"+ emailReimpPwd.Value + "' AND Telefono = '"+ telReimpPwd.Value + "' AND ValFornitore = ' '";
                                else
                                {
                                    stampaErrori(msgReimpPwd, "Tipo utente non valido");
                                    errore = true;
                                }

                                if (!errore)
                                {
                                    try
                                    {
                                        tab = ado.eseguiQuery(codSql, CommandType.Text);
                                        if (tab.Rows.Count == 1)
                                        {
                                            //Calcolo l'MD5 della nuova password
                                            //e la salvo nel db
                                            newPwd = calcolaMD5(pwdReimpPwd.Value);
                                            if (lstTipoUtenteReimpPwd.Value == "admin")
                                                codSql = "UPDATE Admin SET PwdAdmin = '" + newPwd + "' WHERE IdAdmin = " + tab.Rows[0].ItemArray[0].ToString();
                                            else if(lstTipoUtenteReimpPwd.Value == "cliente")
                                                codSql = "UPDATE Clienti SET PwdCliente = '" + newPwd + "' WHERE IdCliente = " + tab.Rows[0].ItemArray[0].ToString();
                                            else
                                                codSql = "UPDATE Fornitori SET PwdFornitore = '" + newPwd + "' WHERE IdFornitore = " + tab.Rows[0].ItemArray[0].ToString();
                                            ado.eseguiNonQuery(codSql, CommandType.Text);
                                            chiudiSessione();
                                        }
                                        else
                                            stampaErrori(msgReimpPwd, "Credenziali errate");
                                    }
                                    catch (Exception ex)
                                    {
                                        stampaErrori(msgReimpPwd, "Attenzione!!! Errore: " + ex.Message);
                                    }
                                }
                            }
                            else
                                stampaErrori(msgReimpPwd, "Specificare il tipo utente");
                        }
                        else
                            stampaErrori(msgReimpPwd, "Nuova password non valida");

                    }
                    else
                        stampaErrori(msgReimpPwd, "Inserire il Telefono");
                }
                else
                    stampaErrori(msgReimpPwd, "Inserire la Password");
            }
            else
                stampaErrori(msgReimpPwd, "Inserire lo Username");
        }

        /******************************/
        /* Gestione Chiusura Sessione */
        /******************************/
        private void chiudiSessione()
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("login.aspx");
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
    }
}