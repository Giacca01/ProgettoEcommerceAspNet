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

namespace ProgettoEcommerce
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["IdUtente"] != null && Session["TipoUtente"] != null)
            {
                Response.Redirect("prodotti.aspx");
            }

            //Se sbagli le credenziali non funge
            if (!Page.IsPostBack)
            {
                adoNet.impostaConnessione("App_Data/DBEcommerce.mdf");
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string codSql = String.Empty;
            adoNet ado = new adoNet();
            DataTable tab = new DataTable();
            string hashPwdLogin = String.Empty;
            string tipoUtente = String.Empty;
            bool errore = false;

            if (usernameLogin.Value != String.Empty)
            {
                if (pwdLogin.Value != String.Empty)
                {
                    if (lstTipoUtente.SelectedIndex != -1)
                    {
                        if (lstTipoUtente.Value == "admin")
                        {
                            codSql = "SELECT PwdAdmin AS Pwd, IdAdmin AS Id FROM Admin WHERE UserAdmin = '" + usernameLogin.Value+ "' AND ValAdmin = ' '";
                            tipoUtente = "Admin";
                        }
                        else if(lstTipoUtente.Value == "cliente")
                        {
                            codSql = "SELECT PwdCliente AS Pwd, IdCliente AS Id FROM Clienti WHERE UserCliente = '" + usernameLogin.Value+ "' AND ValCliente = ' '";
                            tipoUtente = "Cliente";
                        }
                        else if(lstTipoUtente.Value == "fornitore")
                        {
                            codSql = "SELECT PwdFornitore AS Pwd, IdFornitore AS Id FROM Fornitori WHERE UserFornitore = '" + usernameLogin.Value + "' AND ValFornitore = ' '";
                            tipoUtente = "Fornitore";
                        }
                        else
                        {
                            printErrori("Tipo utente non valido");
                            errore = true;
                        }

                        if (!errore)
                        {
                            try
                            {
                                tab = ado.eseguiQuery(codSql, CommandType.Text);
                                if (tab.Rows.Count == 1)
                                {
                                    hashPwdLogin = calcolaMD5(pwdLogin.Value);
                                    StringComparer comparer = StringComparer.OrdinalIgnoreCase;
                                    if (comparer.Compare(hashPwdLogin, tab.Rows[0].ItemArray[0].ToString()) == 0)
                                    {
                                        Session["IdUtente"] = tab.Rows[0].ItemArray[1].ToString();
                                        Session["TipoUtente"] = tipoUtente;
                                        Response.Redirect("prodotti.aspx");
                                    }
                                    else
                                    {
                                        printErrori("Credenziali errate");
                                    }
                                }
                                else
                                {
                                    printErrori("Credenziali errate");
                                }
                            }
                            catch (Exception ex)
                            {
                                printErrori("Attenzione!!! Errore: " + ex.Message);
                            }
                        }
                    }
                    else
                    {
                        printErrori("Specificare il tipo utente");
                    }
                }
                else
                {
                    printErrori("Inserire la Password");
                }
            }
            else
            {
                printErrori("Inserire uno Username");
            }
        }

        private void printErrori(string msgErrore)
        {
            msgLogin.InnerHtml = msgErrore;
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