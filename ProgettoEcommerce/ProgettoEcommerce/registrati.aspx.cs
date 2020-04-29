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

namespace ProgettoEcommerce
{
    public partial class registrati : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["IdUtente"] != null && Session["TipoUtente"] != null)
            {
                Response.Redirect("prodotti.aspx");
            }

            if (!Page.IsPostBack)
            {
                adoNet.impostaConnessione("App_Data/DBEcommerce.mdf");
                caricaComboCitta();
            }

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
                lstCittaReg.DataSource = tab;
                lstCittaReg.DataTextField = "DescCitta";
                lstCittaReg.DataValueField = "IdCitta";
                lstCittaReg.DataBind();
            }
            catch (Exception ex)
            {
                printErrori("Attenzione!!! Errore: " + ex.Message);
            }
        }

        private void printErrori(string msgErrore)
        {
            msgReg.InnerHtml = msgErrore;
        }

        protected void btnRegistrati_Click(object sender, EventArgs e)
        {
            DateTime dataNas = DateTime.MinValue;
            Regex regTel = new Regex(@"^[0-9]{10,10}$");
            Regex regPwd = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#\$%\^&\*])(?=.{8,})");
            adoNet ado = new adoNet();
            string codSql = String.Empty;
            DataTable tab = new DataTable();

            try
            {
                if (nomeReg.Value != String.Empty)
                {
                    if (cognomeReg.Value != String.Empty)
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
                                                            codSql = "SELECT * FROM Clienti WHERE TelefonoCliente = '" + telReg.Value + "'";
                                                            tab = ado.eseguiQuery(codSql, CommandType.Text);
                                                            if (tab.Rows.Count == 0)
                                                            {
                                                                codSql = "SELECT * FROM Clienti WHERE MailCliente = '" + mailReg.Value + "'";
                                                                tab = ado.eseguiQuery(codSql, CommandType.Text);
                                                                if (tab.Rows.Count == 0)
                                                                {
                                                                    codSql = "SELECT * FROM Clienti WHERE UserCliente = '" + usernameReg.Value + "'";
                                                                    tab = ado.eseguiQuery(codSql, CommandType.Text);
                                                                    if (tab.Rows.Count == 0)
                                                                    {
                                                                        codSql = "INSERT INTO Clienti ([NomeCliente], [CognomeCliente], [DataNascitaCliente], [TelefonoCliente], [MailCliente], [UserCliente], [PwdCliente], [ViaCliente], [CivicoCliente], [IdCittaClienti], [ValCliente])" +
                                                                            "VALUES ('" + nomeReg.Value + "', '" + cognomeReg.Value + "', '" + dataNas.ToString("yyyy/MM/dd") + "', '" + telReg.Value + "', '" + mailReg.Value + "', '" + usernameReg.Value + "', '" + calcolaMD5(pwdReg.Value) + "', '" + viaReg.Value + "', '" + civicoReg.Value + "', " + lstCittaReg.Value + ", ' ')";
                                                                        ado.eseguiNonQuery(codSql, CommandType.Text);
                                                                        Response.Redirect("login.aspx");
                                                                    }
                                                                    else
                                                                    {
                                                                        printErrori("Username non disponibile");
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    printErrori("Email non disponibile");
                                                                }
                                                            }
                                                            else
                                                            {
                                                                printErrori("Telefono non disponibile");
                                                            }
                                                        }
                                                        else
                                                        {
                                                            printErrori("Selezionare la città di residenza");
                                                        }
                                                    }
                                                    else
                                                    {
                                                        printErrori("Inserire il numero civico");
                                                    }
                                                }
                                                else
                                                {
                                                    printErrori("Inserire la via di residenza");
                                                }
                                            }
                                            else
                                            {
                                                printErrori("Inserire una password valida");
                                            }
                                        }
                                        else
                                        {
                                            printErrori("Inserire uno username");
                                        }
                                    }
                                    else
                                    {
                                        printErrori("Inserire un telefono valido");
                                    }
                                }
                                else
                                {
                                    printErrori("Inserire una mail valida");
                                }
                            }
                            else
                            {
                                printErrori("Occorre aver compiuto almeno 18 anni");
                            }
                        }
                        else
                        {
                            printErrori("Data di nascita non valida");
                        }
                    }
                    else
                    {
                        printErrori("Inserire il Cognome dell' utente");
                    }
                }
                else
                {
                    printErrori("Inserire il Nome dell' utente");
                }
            }
            catch (Exception ex)
            {
                printErrori("Attenzione!!! Errore: " + ex.Message);
            }
            
        }

        bool validaEmail(string email)
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

        //Conversione della stringa in input in MD5
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