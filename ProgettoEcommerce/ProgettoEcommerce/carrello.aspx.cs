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
using System.Net.Mail;

namespace ProgettoEcommerce
{
    public partial class cart : System.Web.UI.Page
    {
        /**********************/
        /* Routine Principale */
        /**********************/
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
                adoNet.impostaConnessione("App_Data/DBEcommerce.mdf");
            //Gestito fuori dal postback per poter agganciare l'evento al bottone di eliminazione
            if (Session["IdUtente"] == null && Session["TipoUtente"] == null)
                Response.Redirect("login.aspx");
            else if(Session["TipoUtente"].ToString().ToUpper() == "CLIENTE")
                chkTipoChiamata();
            else
                Response.Redirect("prodotti.aspx");
        }

        /************************************/
        /* Gestione Elenco Carte di Credito */
        /************************************/
        private void gestCarteDiCredito()
        {
            adoNet ado = new adoNet();
            string codSql = String.Empty;
            DataTable tab = new DataTable();

            codSql = "SELECT * FROM Carte AS C " +
                "WHERE C.IdCliente = " + Session["IdUtente"] + " " +
                "AND C.ValCarta = ' '";
            try
            {
                tab = ado.eseguiQuery(codSql, CommandType.Text);
                if (tab.Rows.Count > 0)
                {
                    lstCartePagamento.DataSource = tab;
                    lstCartePagamento.DataTextField = "CodiceCarta";
                    lstCartePagamento.DataValueField = "IdCarta";
                    lstCartePagamento.DataBind();
                    lstCartePagamento.Visible = true;
                    btnAddCarta.Visible = false;
                }
                else
                {
                    lstCartePagamento.Visible = false;
                    btnAddCarta.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lstCartePagamento.Visible = false;
                btnAddCarta.Visible = false;
                msgErrCartaCredito.InnerText = "Errore: " + ex.Message;
            }
        }

        /***************************/
        /* Controllo Chiama Pagine */
        /***************************/
        private void chkTipoChiamata()
        {
            //Caricamento NavBar
            gestUtenteLoggato();
            //Se sono presenti il codice prodotto e la quantità
            //si vuole aggiungere un prodotto
            //altrimenti stampo l'elenco dei prodotti prensenti nel carrello
            if (Session["CodProd"] != null && Session["Qta"] != null)
                gestAddProdotto();
            else
                stampaElencoProdotti();
            gestCarteDiCredito();
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
            navAndamentoVendite.Visible = false;
            navCategorie.Visible = false;
            navGestioneOrdini.Visible = false;
            navGestioneProdotti.Visible = false;
            navGestioneUtenti.Visible = false;
            navTipiCarte.Visible = false;
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

        /******************************/
        /* Gestione Aggiunta Prodotto */
        /* al Carrello                */
        /******************************/
        private void gestAddProdotto()
        {
            adoNet ado = new adoNet();
            string codSql = String.Empty;
            int codProd, newQta;
            int nProd = 0;
            Double newPrezzo = 0;
            int qtaTotale = 0;
            DataTable tab = new DataTable();
            DataTable tabCarrello = new DataTable();
            System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";
            customCulture.NumberFormat.NumberGroupSeparator = "";
            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;

            //Controllo parametri in get
            if (Int32.TryParse(Session["CodProd"].ToString(), out codProd))
            {
                if (Int32.TryParse(Session["Qta"].ToString(), out newQta))
                {
                    //Controllo se il prodotto scelto è disponibile
                    //nella quantità scelta
                    codSql = "SELECT * FROM Prodotti WHERE IdProdotto = " + Session["CodProd"].ToString() + " AND (QtaGiacenza - " + Session["Qta"].ToString() + ") >= 0 ";
                    try
                    {
                        
                        tab = ado.eseguiQuery(codSql, CommandType.Text);
                        if (tab.Rows.Count > 0)
                        {
                            //Gestione Prodotti Carrello
                            codSql = "SELECT * FROM Carrello WHERE IdProdotto = " + codProd + " AND Ordinato = 0 AND IdCliente = " + Session["IdUtente"].ToString();
                            tabCarrello = ado.eseguiQuery(codSql, CommandType.Text);
                            nProd = tabCarrello.Rows.Count;
                            codSql = "SELECT * FROM Prodotti WHERE IdProdotto = " + codProd;
                            tab = ado.eseguiQuery(codSql, CommandType.Text);
                            if (tab.Rows[0].ItemArray[tab.Columns["Sconto"].Ordinal].ToString() != String.Empty)
                                newPrezzo = ((Convert.ToDouble(tab.Rows[0].ItemArray[tab.Columns["Prezzo"].Ordinal].ToString()) * Convert.ToDouble(tab.Rows[0].ItemArray[tab.Columns["Sconto"].Ordinal].ToString())) / 100);
                            else
                                newPrezzo = Convert.ToDouble(tab.Rows[0].ItemArray[tab.Columns["Prezzo"].Ordinal].ToString());
                            //Se il prodotto è già presente aumento la quantità
                            //altrimenti lo aggiungo
                            if (nProd > 0)
                            {
                                qtaTotale = Convert.ToInt32(tabCarrello.Rows[0].ItemArray[3]) + newQta;
                                codSql = "UPDATE Carrello SET QtaProd = " + qtaTotale + ", PrezzoUnitario = " + (newPrezzo * qtaTotale) + ", ValCarrello = ' ', DataAggiunta = '" + DateTime.Now + "' WHERE IdProdotto = " + codProd + " AND Ordinato = 0 AND IdCliente = " + Session["IdUtente"];
                                ado.eseguiNonQuery(codSql, CommandType.Text);
                            }
                            else
                            {
                                codSql = "INSERT INTO Carrello ([IdCliente], [IdProdotto], [DataAggiunta], [QtaProd], [PrezzoUnitario], [Ordinato], [ValCarrello]) " +
                                    "VALUES  " +
                                    "(" + Session["IdUtente"] + ", " + codProd + ", '" + DateTime.Now.ToString(@"yyyy/MM/dd HH:mm:ss") + "' ," + newQta + ", " + (newPrezzo * newQta) + ", 0, ' ')";
                                ado.eseguiNonQuery(codSql, CommandType.Text);
                            }
                            Session["CodProd"] = null;
                            Session["Qta"] = null;
                            stampaElencoProdotti();
                        }
                        else
                            stampaErrori(msgErroreElProdCar, "Il prodotto scelto non è disponibile in questa quantità");
                    }
                    catch (Exception ex)
                    {
                        stampaErrori(msgErroreElProdCar, "Errore: " + ex.Message);
                    }
                }
                else
                    stampaErrori(msgErroreElProdCar, "Quantità non valida");
            }
            else
                stampaErrori(msgErroreElProdCar, "Prodotto non valido");
        }

        /*************************************/
        /* Gestione Elenco Prodotti Carrello */
        /*************************************/
        private void stampaElencoProdotti()
        {
            adoNet ado = new adoNet();
            string codSql = String.Empty;
            DataTable tab = new DataTable();
            string codHtml = String.Empty;
            double prezzoFinale;
            double totale = 0;
            TableRow riga = new TableRow();
            TableCell cella = new TableCell();

            corpoTabCarrello.InnerHtml = "";

            codSql = "SELECT C.*, P.ImmagineProdotto AS ImmagineProdotto, P.ModelloProdotto AS Modello, P.Prezzo AS Prezzo, P.Sconto AS Sconto FROM Carrello AS C " +
                "INNER JOIN Prodotti AS P " +
                "ON P.IdProdotto = C.IdProdotto " +
                "WHERE C.ValCarrello = ' ' AND C.IdCliente = " + Session["IdUtente"] + " AND C.Ordinato = 0";
            try
            {
                tab = ado.eseguiQuery(codSql, CommandType.Text);
                if (tab.Rows.Count > 0)
                {
                    for (int i = 0; i < tab.Rows.Count; i++)
                    {
                        if (tab.Rows[i].ItemArray[tab.Columns["Sconto"].Ordinal].ToString() != String.Empty)
                            prezzoFinale = (Convert.ToDouble(tab.Rows[i].ItemArray[tab.Columns["Prezzo"].Ordinal].ToString()) * Convert.ToDouble(tab.Rows[i].ItemArray[tab.Columns["Sconto"].Ordinal].ToString())) / 100;
                        else
                            prezzoFinale = Convert.ToDouble(tab.Rows[i].ItemArray[tab.Columns["Prezzo"].Ordinal].ToString());
                        riga = new TableRow();

                        cella = new TableCell();
                        cella.Text = "<div class='media'>" +
                            "<div class='d-flex'>" +
                            "<img src = 'img/product/" + tab.Rows[i].ItemArray[tab.Columns["ImmagineProdotto"].Ordinal].ToString() + "' height='100' width='150'/>" +
                            "</div>" +
                            "<div class='media-body'>" +
                            "<p>" + tab.Rows[i].ItemArray[tab.Columns["Modello"].Ordinal].ToString() + "</p>" +
                            "</div>" +
                            "</div>";
                        riga.Controls.Add(cella);

                        cella = new TableCell();
                        cella.Text = "<h5>" + prezzoFinale + "&euro;</h5>";
                        riga.Controls.Add(cella);

                        cella = new TableCell();
                        cella.Text = tab.Rows[i].ItemArray[3].ToString();
                        riga.Controls.Add(cella);

                        cella = new TableCell();
                        cella.Text = "<h5>" + (prezzoFinale * Convert.ToInt32(tab.Rows[i].ItemArray[3].ToString())) + "&euro;</h5>";
                        riga.Controls.Add(cella);

                        cella = new TableCell();
                        LinkButton btnEl = new LinkButton();
                        btnEl.CssClass = "btn success circle";
                        btnEl.Text = "<i class='fa fa-trash'></i>";
                        btnEl.Attributes.Add("data-CodProd", tab.Rows[i].ItemArray[tab.Columns["IdProdotto"].Ordinal].ToString());
                        btnEl.Click += new EventHandler(BtnEl_Click);
                        cella.Controls.Add(btnEl);
                        riga.Controls.Add(cella);

                        totale += (prezzoFinale * Convert.ToInt32(tab.Rows[i].ItemArray[3].ToString()));

                        corpoTabCarrello.Controls.Add(riga);
                    }

                    riga = new TableRow();
                    riga.Controls.Add(new TableCell());
                    riga.Controls.Add(new TableCell());
                    cella = new TableCell();
                    cella.Text = "<h5>Totale</h5>";
                    riga.Controls.Add(cella);

                    cella = new TableCell();
                    cella.Text = "<h5>" + totale + "&euro;</h5>";
                    riga.Controls.Add(cella);

                    corpoTabCarrello.Controls.Add(riga);
                }
                else
                    btnGestOrdine.Visible = false;                
            }
            catch (Exception ex)
            {
                stampaErrori(msgErroreElProdCar, "Errore: " + ex.Message);
            }
        }

        /*******************************************/
        /* Gestione Eliminazione Prodotto Carrello */
        /*******************************************/
        private void BtnEl_Click(object sender, EventArgs e)
        {
            adoNet ado = new adoNet();
            string codSql = String.Empty;
            DataTable tab = new DataTable();
            LinkButton btnSender = (LinkButton)sender;

            if (Int32.TryParse(btnSender.Attributes["data-CodProd"], out int codProd))
            {
                codSql = "UPDATE Carrello SET ValCarrello = 'A', QtaProd = 0 WHERE IdCliente = " + Session["IdUtente"] + " AND IdProdotto = " + codProd;
                try
                {
                    ado.eseguiNonQuery(codSql, CommandType.Text);
                    stampaElencoProdotti();
                }
                catch (Exception ex)
                {
                    stampaErrori(msgErroreElProdCar, "Errore: " + ex.Message);
                }
            }
            else
                stampaErrori(msgErroreElProdCar, "Codice Prodotto non valido");
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

        /****************************/
        /* Gestione Conferma Ordine */
        /****************************/
        protected void btnConfermaOrdine_Click(object sender, EventArgs e)
        {
            adoNet ado = new adoNet();
            string codSql = String.Empty;
            string codSqlElProdOrdine = String.Empty;
            string codQuery1 = String.Empty;
            string codQuery2 = String.Empty;
            string codQuery3 = String.Empty;
            string codQuery4 = String.Empty;
            string testoMail = String.Empty;
            string ausIndirizzo = String.Empty;
            int codOrdine = -1;
            DataTable tab = new DataTable();
            DataTable tab1 = new DataTable();
            DataTable tab2 = new DataTable();
            string totale = String.Empty;

            //Cambio il separatore dei decimali e delle migliaia per adattarmi alla notazione SQL
            System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";
            customCulture.NumberFormat.NumberGroupSeparator = "";
            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;
            

            if (lstCartePagamento.SelectedIndex != -1)
            {
                //Controllo se il prodotto è disponibile
                codSql = "SELECT COUNT(*) " +
                    "FROM Carrello AS C " +
                    "INNER JOIN Prodotti AS P " +
                    "ON C.IdProdotto = P.IdProdotto " +
                    "WHERE C.IdCliente = " + Session["IdUtente"].ToString() + " " +
                    "AND C.Ordinato = 0 " +
                    "AND C.ValCarrello = ' ' " +
                    "AND (P.QtaGiacenza - C.QtaProd) < 0";
                try
                {
                    if (Convert.ToInt32(ado.eseguiScalar(codSql, CommandType.Text)) == 0)
                    {
                        //Recupero i dati del cliente
                        codSql = "SELECT * FROM Carrello WHERE IdCliente = " + Session["IdUtente"].ToString() + " AND ValCarrello = ' ' AND Ordinato = 0";
                        tab = ado.eseguiQuery(codSql, CommandType.Text);
                        //Recupero la spesa totale per la tabella ordini
                        codSql = "SELECT IdCliente, SUM(PrezzoUnitario) AS Totale FROM Carrello WHERE IdCliente = " + Session["IdUtente"] + " AND ValCarrello = ' ' AND Ordinato = 0 GROUP BY IdCliente";
                        tab1 = ado.eseguiQuery(codSql, CommandType.Text);
                        codOrdine = getCodiceOrdine();
                        totale = tab1.Rows[0].ItemArray[1].ToString();
                        //Preparo le query per la transazione
                        codQuery1 = "SET IDENTITY_INSERT [dbo].[Ordini] ON;INSERT INTO Ordini ([IdOrdine], [DataOrdine], [PrezzoTotale], [IdCarta], [ValOrdine]) " +
                            "VALUES (" + codOrdine + ", '" + DateTime.Now.ToString(@"yyyy/MM/dd") + "', " + tab1.Rows[0].ItemArray[1].ToString() + ", " + lstCartePagamento.Value + ", ' ');SET IDENTITY_INSERT[dbo].[Ordini] OFF";
                        codQuery2 = "INSERT INTO DettaglioOrdini ([IdOrdine], [IdProdotto], [QtaOrdine], [PrezzoUnitario], [ValDettaglioOrdini]) " +
                            "VALUES ";
                        for (int i = 0; i < tab.Rows.Count; i++)
                        {
                            codQuery2 += "(" + codOrdine + ", " + tab.Rows[i].ItemArray[1].ToString() + ", " + tab.Rows[i].ItemArray[3].ToString() + ", " + tab.Rows[i].ItemArray[4].ToString() + ", ' ')";
                            if (i != tab.Rows.Count - 1)
                                codQuery2 += ",";
                        }

                        codQuery3 = "UPDATE Carrello SET Ordinato = 1 WHERE IdCliente = " + Session["IdUtente"] + " AND ValCarrello = ' ' AND Ordinato = 0";
                        codQuery4 = "UPDATE Prodotti " +
                            "SET QtaGiacenza = QtaGiacenza-(SELECT C.QtaProd FROM Carrello AS C WHERE C.IdProdotto = Prodotti.IdProdotto AND C.IdCliente = " + Session["IdUtente"].ToString() + " AND C.ValCarrello = ' ' AND C.Ordinato = 0) " +
                            "WHERE Prodotti.IdProdotto IN (SELECT C1.IdProdotto FROM Carrello AS C1 WHERE C1.IdCliente = " + Session["IdUtente"].ToString() + " AND C1.ValCarrello = ' ' AND C1.Ordinato = 0)";
                        //Eseguo la transazione per confermare l'ordine
                        //se fallisce c'è un rollback è tutte le modifiche sono cancellate
                        ado.transazioneOrdine(codQuery1, codQuery2, codQuery3, codQuery4, CommandType.Text);
                        codSqlElProdOrdine = "SELECT P.*, D.* FROM Prodotti AS P " +
                            "INNER JOIN DettaglioOrdini AS D " +
                            "ON P.IdProdotto = D.IdProdotto " +
                            "WHERE D.IdOrdine = " + codOrdine;
                        tab2 = ado.eseguiQuery(codSqlElProdOrdine, CommandType.Text);
                        //Scrivo la mail di conferma all' utente
                        codSql = "SELECT MailCliente FROM Clienti WHERE ValCliente = ' ' AND IdCliente = " + Session["IdUtente"];
                        tab = ado.eseguiQuery(codSql, CommandType.Text);
                        testoMail = "Gentile Cliente,\nle confermiamo l'avvenuta registrazione del suo ordine, effettuato sulla nostra piattaforma in data " + DateTime.Now.ToString(@"dd/MM/yyyy") + ".\n" +
                            "Prodotti Ordinati: \n";
                        for (int j = 0; j < tab2.Rows.Count; j++)
                            testoMail += "-" + tab2.Rows[j].ItemArray[tab2.Columns["ModelloProdotto"].Ordinal].ToString() + " Quantità: " + tab2.Rows[j].ItemArray[tab2.Columns["QtaOrdine"].Ordinal].ToString() + "\n";
                        testoMail += "Totale: " + string.Format("{0:N2}€", totale);
                        inviaMailOrdine("noreplyambulatoriogiacardi@gmail.com", tab.Rows[0].ItemArray[0].ToString(), "Conferma Ordine", testoMail);
                        //Scrivo le mail di conferma a tutti i fornitori coinvolti
                        codSql = "SELECT * " +
                            "FROM Prodotti AS P " +
                            "INNER JOIN DettaglioOrdini AS D " +
                            "ON P.IdProdotto = D.IdProdotto " +
                            "INNER JOIN Fornitori AS F " +
                            "ON F.IdFornitore = P.IdFornitore " +
                            "WHERE D.IdOrdine = " + codOrdine+" " +
                            "ORDER BY F.IdFornitore";
                        tab = ado.eseguiQuery(codSql, CommandType.Text);
                        testoMail = String.Empty;
                        for (int k = 0; k < tab.Rows.Count; k++)
                        {
                            if (tab.Rows[k].ItemArray[tab.Columns["Email"].Ordinal].ToString() != ausIndirizzo)
                            {
                                if (testoMail != String.Empty)
                                    inviaMailOrdine("noreplyambulatoriogiacardi@gmail.com", ausIndirizzo, "Notifica Ordine", testoMail);
                                ausIndirizzo = tab.Rows[k].ItemArray[tab.Columns["Email"].Ordinal].ToString();
                                testoMail = "Gentile Fornitore,\nle notifichiamo l'avvenuta registrazione di un ordine collegato ai suoi prodotti indicati in seguito.\n" +
                                    "Prodotti Ordinati: \n";
                            }
                            testoMail += "-" + tab.Rows[k].ItemArray[tab.Columns["ModelloProdotto"].Ordinal].ToString() + " Quantità: " + tab.Rows[k].ItemArray[tab.Columns["QtaOrdine"].Ordinal].ToString() + "\n";
                        }
                        inviaMailOrdine("noreplyambulatoriogiacardi@gmail.com", ausIndirizzo, "Notifica Ordine", testoMail);
                        stampaConfOrdine(msgConfermaOrdine, "Ordine Completato con successo");
                    }
                    else
                        stampaErrori(msgConfermaOrdine, "Uno o più prodotti non sono disponibili nella quantità ordinata");
                }
                catch (Exception ex)
                {
                    stampaErrori(msgConfermaOrdine, "Errore: " + ex.Message);
                }
            }
            else
                stampaErrori(msgConfermaOrdine, "Indicare una carta di credito"); 
        }

        /**********************************/
        /* Gestione Esito Conferma Ordine */
        /**********************************/
        private void stampaConfOrdine(HtmlGenericControl contMsg, string msgErrore)
        {
            contMsg.InnerText = msgErrore;
            contMsg.Attributes.Remove("class");
            contMsg.Attributes.Add("class", "alert alert-success");
            contMsg.Visible = true;
        }

        /********************************/
        /* Recupero Codice Nuovo Ordine */
        /********************************/
        private int getCodiceOrdine()
        {
            adoNet ado = new adoNet();
            string codSql = String.Empty;
            int cod = -1;

            codSql = "SELECT COUNT(*) FROM Ordini WHERE ValOrdine = ' '";
            try
            {
                cod = Convert.ToInt32(ado.eseguiScalar(codSql, CommandType.Text)) + 1;
            }
            catch (Exception ex)
            {
                stampaErrori(msgConfermaOrdine, "Errore: " + ex.Message);
            }

            return cod;
        }

        /**************************/
        /* Funzione di Invio Mail */
        /**************************/
        private void inviaMailOrdine(string mittente, string destinatario, string oggetto, string testo)
        {
            MailMessage m1 = new MailMessage(mittente, destinatario, oggetto, testo);

            //Controlli opzione 2 messi quì per non appesantire troppo il try catch
            MailMessage m = new MailMessage();
            //Imposto la priorità della mail
            m.Priority = MailPriority.Normal;

            //Imposto i singoli valori della mia mail
            try
            {
                //Opzione 2: Ottimale
                //Mittente
                m.From = new MailAddress(mittente); //Trasformo la stringa in mail address
                //Destinatario
                m.To.Add(new MailAddress(destinatario)); //se si hanno più destinatari si utilizza MailAddressCollection(....)
                //Copia Carbone
                //m.CC.Add(new MailAddress(txtCc.Text));
                //Copia Carbone Nascosta
                //m.Bcc.Add(new MailAddress(txtBcc.Text));
                //Oggetto Mail
                m.Subject = oggetto;
                //Testo della Mail
                m.Body = testo;

                //credenziali di accesso al server mail scelto
                System.Net.NetworkCredential crd = new System.Net.NetworkCredential();
                crd.UserName = "noreplyambulatoriogiacardi@gmail.com";
                crd.Password = "NoreplyGiac";

                SmtpClient s = new SmtpClient();
                s.Host = "smtp.gmail.com";
                s.Port = 587; // 25;
                s.Credentials = crd;
                s.EnableSsl = true; //https
                s.Send(m);

            }
            catch (Exception ex)
            {
                throw new Exception("Invio email di conferma fallito");
            }
        }
    }
}