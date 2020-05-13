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
    public partial class gestioneProdotti : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                adoNet.impostaConnessione("App_Data/DBEcommerce.mdf");
                if (Session["IdUtente"] == null && Session["TipoUtente"] == null)
                    Response.Redirect("login.aspx");
                else if (Session["TipoUtente"].ToString().ToUpper() == "CLIENTE")
                    Response.Redirect("prodotti.aspx");
                setListaImg();
                elencoCategorie();
            }
            stampaElencoProdotti();
        }

        private void setListaImg()
        {
            contLstGestImg.Visible = false;
            contImgProd.Visible = true;
            //Page.ClientScript.RegisterStartupScript(GetType(), "lstGestImg", "refreshLista('lstGestImg', -1);", true);
        }

        private void stampaElencoProdotti()
        {
            adoNet ado = new adoNet();
            string codSql = String.Empty;
            DataTable tab = new DataTable();
            TableRow riga;
            TableCell cella;
            LinkButton btnModifica;
            LinkButton btnGestProd;
            CheckBox chkVal;

            msgElencoProdotti.Visible = false;
            corpoTabElencoProdotti.Controls.Clear();
            if (Session["TipoUtente"].ToString().ToUpper() == "ADMIN")
                codSql = "SELECT * FROM Prodotti AS P INNER JOIN Categorie AS C ON C.IdCategoria = P.IdCategoria INNER JOIN Fornitori AS F ON F.IdFornitore = P.IdFornitore";
            else
                codSql = "SELECT * FROM Prodotti AS P INNER JOIN Categorie AS C ON C.IdCategoria = P.IdCategoria INNER JOIN Fornitori AS F ON F.IdFornitore = P.IdFornitore WHERE P.IdFornitore = " + Session["IdUtente"].ToString();
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
                    cella.Text = tab.Rows[i].ItemArray[3].ToString();
                    riga.Cells.Add(cella);

                    cella = new TableCell();
                    cella.Text = tab.Rows[i].ItemArray[tab.Columns["DescrizioneCategoria"].Ordinal].ToString();
                    riga.Cells.Add(cella);

                    cella = new TableCell();
                    cella.Text = tab.Rows[i].ItemArray[tab.Columns["NomeFornitore"].Ordinal].ToString();
                    riga.Cells.Add(cella);

                    cella = new TableCell();
                    cella.Text = string.Format("{0:C}", tab.Rows[i].ItemArray[7].ToString()) + "&euro;";
                    riga.Cells.Add(cella);

                    cella = new TableCell();
                    cella.Text = tab.Rows[i].ItemArray[8].ToString();
                    if (cella.Text != String.Empty)
                        cella.Text += "%";
                    riga.Cells.Add(cella);

                    cella = new TableCell();
                    cella.Text = tab.Rows[i].ItemArray[9].ToString();
                    riga.Cells.Add(cella);

                    cella = new TableCell();
                    chkVal = new CheckBox();
                    if (tab.Rows[i].ItemArray[tab.Columns["ValProdotto"].Ordinal].ToString() == "A")
                        chkVal.Checked = false;
                    else
                        chkVal.Checked = true;
                    chkVal.Enabled = false;
                    cella.Controls.Add(chkVal);
                    riga.Cells.Add(cella);

                    cella = new TableCell();
                    if (Session["TipoUtente"].ToString().ToUpper() == "FORNITORE")
                    {
                        btnModifica = new LinkButton();
                        btnModifica.Text = "<i class='fa fa-edit'></i> Modifica";
                        btnModifica.CssClass = "btn btn-success circle";
                        btnModifica.Attributes.Add("data-codProdotto", tab.Rows[i].ItemArray[0].ToString());
                        btnModifica.Click += BtnModifica_Click;
                        cella.Controls.Add(btnModifica);
                        riga.Cells.Add(cella);
                        Label lbl = new Label();
                        lbl.Text = "&nbsp;";
                        cella.Controls.Add(lbl);
                    }
                    
                    btnGestProd = new LinkButton();
                    btnGestProd.Attributes.Add("data-codProdotto", tab.Rows[i].ItemArray[0].ToString());
                    if (tab.Rows[i].ItemArray[12].ToString() == "A")
                    {
                        btnGestProd.Text = "<i class='fa fa-edit'></i> Ripristina";
                        btnGestProd.CssClass = "btn btn-success circle";
                        btnGestProd.Attributes.Add("data-tipoOp", "0"); //Ripristina
                    }
                    else
                    {
                        btnGestProd.Text = "<i class='fa fa-trash'></i> Elimina";
                        btnGestProd.CssClass = "btn btn-danger circle";
                        btnGestProd.Attributes.Add("data-tipoOp", "1");//Elimina
                    }
                    btnGestProd.Click += BtnGestProd_Click;
                    cella.Controls.Add(btnGestProd);
                    riga.Cells.Add(cella);

                    corpoTabElencoProdotti.Controls.Add(riga);
                }
            }
            catch (Exception ex)
            {
                stampaErrori(msgElencoProdotti, "Errore: " + ex.Message);
            }
        }

        private void elencoCategorie()
        {
            adoNet ado = new adoNet();
            string codSql = String.Empty;
            DataTable tab = new DataTable();
            string codHtml = String.Empty;

            codSql = "SELECT * FROM Categorie WHERE ValCategoria = ' '";
            try
            {
                tab = ado.eseguiQuery(codSql, CommandType.Text);
                lstCatInsModProdotti.DataSource = tab;
                lstCatInsModProdotti.DataValueField = "IdCategoria";
                lstCatInsModProdotti.DataTextField = "DescrizioneCategoria";
                Page.ClientScript.RegisterStartupScript(GetType(), "refreshLista", "refreshLista('lstCatInsModProdotti', -1);", true);
                lstCatInsModProdotti.DataBind();
            }
            catch (Exception ex)
            {
                stampaErrori(msgInsModProdotti, "Errore: " + ex.Message);
            }
        }
        private void BtnGestProd_Click(object sender, EventArgs e)
        {
            LinkButton btnSender = sender as LinkButton;
            adoNet ado = new adoNet();
            string codSql = String.Empty;
            string codProdotto = btnSender.Attributes["data-codProdotto"];
            string tipoOp = btnSender.Attributes["data-tipoOp"];

            if (Int32.TryParse(codProdotto, out int cdProdotto) && tipoOp != String.Empty)
            {
                if (tipoOp == "1")
                    codSql = "UPDATE Prodotti SET ValProdotto = 'A' WHERE IdProdotto = " + cdProdotto;
                else if (tipoOp == "0")
                    codSql = "UPDATE Prodotti SET ValProdotto = ' ' WHERE IdProdotto = " + cdProdotto;
                try
                {
                    ado.eseguiNonQuery(codSql, CommandType.Text);
                    ClientScript.RegisterStartupScript(typeof(Page), "autoPostback", ClientScript.GetPostBackEventReference(this, String.Empty), true);
                }
                catch (Exception ex)
                {
                    stampaErrori(msgElencoProdotti, "Errore: " + ex.Message);
                }
            }
            else
                stampaErrori(msgElencoProdotti, "Dati mancanti. Ricaricare la pagina");
        }

        private void BtnModifica_Click(object sender, EventArgs e)
        {
            LinkButton btnSender = sender as LinkButton;
            adoNet ado = new adoNet();
            string codSql = String.Empty;
            string codProdotto = btnSender.Attributes["data-codProdotto"];
            DataTable tab = new DataTable();

            if (Int32.TryParse(codProdotto, out int cdProdotto))
            {
                codSql = "SELECT * FROM Prodotti WHERE IdProdotto = " + cdProdotto;
                try
                {
                    tab = ado.eseguiQuery(codSql, CommandType.Text);
                    modelloInsModProdotti.Value = tab.Rows[0].ItemArray[1].ToString();
                    descrizioneInsModProdotti.Value = tab.Rows[0].ItemArray[2].ToString();
                    marcaInsModProdotti.Value = tab.Rows[0].ItemArray[3].ToString();
                    lstCatInsModProdotti.SelectedIndex = Convert.ToInt32(tab.Rows[0].ItemArray[5].ToString());
                    prezzoInsModProdotti.Value = tab.Rows[0].ItemArray[7].ToString();
                    scontoInsModProdotti.Value = tab.Rows[0].ItemArray[8].ToString();
                    giacenzaInsModProdotti.Value = tab.Rows[0].ItemArray[9].ToString();
                    lstGestImg.SelectedIndex = 0;
                    titoloSezInsModProdotti.InnerText = "Modifica Tipo Carta";
                    btnInsModProdotti.Text = "<i class='fa fa-edit' aria-hidden='true'></i> Salva";
                    Session["IdProdotto"] = cdProdotto;
                    contLstGestImg.Visible = true;
                    Page.ClientScript.RegisterStartupScript(GetType(), "lstGestImg", "refreshLista('lstGestImg', 0);", true);
                    SetFocus(sezInsModProdotti);
                }
                catch (Exception ex)
                {
                    stampaErrori(msgElencoProdotti, "Errore: " + ex.Message);
                }
            }
            else
                stampaErrori(msgElencoProdotti, "Codice prodotto non valido");
        }

        private void stampaErrori(HtmlGenericControl contMsg, string msgErrore)
        {
            contMsg.InnerText = msgErrore;
            contMsg.Attributes.Add("class", "alert alert-danger");
            contMsg.Visible = true;
        }

        protected void btnInsModProdotti_Click(object sender, EventArgs e)
        {
            adoNet ado = new adoNet();
            string codSql = String.Empty;
            DataTable tab = new DataTable();
            bool imgOk = false;
            bool scontoOk = false;
            bool datiProdOk = false;
            bool erroreUpload = false;
            int sconto;

            if (modelloInsModProdotti.Value != String.Empty)
            {
                if (descrizioneInsModProdotti.Value != String.Empty)
                {
                    if (marcaInsModProdotti.Value != String.Empty)
                    {
                        if (Session["IdProdotto"] != null)
                        {
                            if (lstGestImg.Value == "nuova")
                            {
                                if (immagineInsModProdotti.PostedFile.FileName != String.Empty)
                                {
                                    if (immagineInsModProdotti.PostedFile.ContentType.Contains("image/"))
                                        imgOk = true;
                                    else
                                        stampaErrori(msgInsModProdotti, "Formato immagine prodotto non valido");
                                }
                                else
                                    stampaErrori(msgInsModProdotti, "Inserire l'immagine del prodotto");
                            }
                            else
                                imgOk = true;
                        }
                        else
                        {
                            if (immagineInsModProdotti.PostedFile.FileName != String.Empty)
                            {
                                if (immagineInsModProdotti.PostedFile.ContentType.Contains("image/"))
                                    imgOk = true;
                                else
                                    stampaErrori(msgInsModProdotti, "Formato immagine prodotto non valido");
                            }
                            else
                                stampaErrori(msgInsModProdotti, "Inserire l'immagine del prodotto");
                        }
                        

                        if (imgOk)
                        {
                            if (lstCatInsModProdotti.SelectedIndex != -1)
                            {
                                if (int.TryParse(prezzoInsModProdotti.Value, out _))
                                {
                                    if (scontoInsModProdotti.Value != String.Empty)
                                    {
                                        if (int.TryParse(scontoInsModProdotti.Value, out sconto))
                                        {
                                            if (sconto < 100 && sconto > 0)
                                                scontoOk = true;
                                            else
                                                stampaErrori(msgInsModProdotti, "Sconto del prodotto non valido");
                                        }
                                        else
                                            stampaErrori(msgInsModProdotti, "Sconto del prodotto non valido");
                                    }
                                    else
                                        scontoOk = true;
                                    if (scontoOk)
                                    {
                                        if (int.TryParse(giacenzaInsModProdotti.Value, out _))
                                            datiProdOk = true;
                                    }
                                }
                                else
                                    stampaErrori(msgInsModProdotti, "Prezzo del prodotto non valido");
                            }
                            else
                                stampaErrori(msgInsModProdotti, "Indicare la categoria del prodotto");
                        }
                        
                    }
                    else
                        stampaErrori(msgInsModProdotti, "Inserire la marca del prodotto");
                }
                else
                    stampaErrori(msgInsModProdotti, "Inserire la descrizione del prodotto");
            }
            else
                stampaErrori(msgInsModProdotti, "Inserire il modello del prodotto");

            if (datiProdOk)
            {
                //Se questo parametro sono settati significa che si vuole fare una modifica
                //altrimenti faccio l'inserimento
                if (Session["IdProdotto"] != null)
                {
                    //Controllare
                    codSql = "SELECT COUNT(*) AS NProd " +
                        "FROM Prodotti " +
                        "WHERE UPPER(ModelloProdotto) = '" + modelloInsModProdotti.Value.ToUpper() + "' " +
                        "AND UPPER(DescrizioneProdotto) = '"+descrizioneInsModProdotti.Value.ToUpper()+"' " +
                        "AND MarcaProdotto = '"+marcaInsModProdotti.Value.ToUpper()+"' " +
                        "AND NOT IdProdotto = " + Session["IdProdotto"].ToString();
                    if (Convert.ToInt32(ado.eseguiScalar(codSql, CommandType.Text)) == 0)
                    {
                        codSql = "UPDATE Prodotti SET ModelloProdotto = '" + modelloInsModProdotti.Value + "', DescrizioneProdotto = '" + descrizioneInsModProdotti.Value + "', MarcaProdotto = '" + marcaInsModProdotti.Value + "', IdCategoria = " + lstCatInsModProdotti.Value + ", Prezzo = " + prezzoInsModProdotti.Value + ", QtaGiacenza = " + giacenzaInsModProdotti.Value;
                        if (scontoInsModProdotti.Value != String.Empty)
                            codSql += ", Sconto = " + scontoInsModProdotti.Value;
                        if (immagineInsModProdotti.PostedFile.FileName != String.Empty)
                        {
                            erroreUpload = uploadImgProdotto();
                            codSql = ", ImmagineProdotto = '" + immagineInsModProdotti.PostedFile.FileName + "'";
                        }
                        codSql += " WHERE IdProdotto = " + Session["IdProdotto"].ToString();
                        if (!erroreUpload)
                        {
                            ado.eseguiNonQuery(codSql, CommandType.Text);
                            Session["IdProdotto"] = null;
                            ClientScript.RegisterStartupScript(typeof(Page), "autoPostback", ClientScript.GetPostBackEventReference(this, String.Empty), true);
                        }
                    }
                    else
                        stampaErrori(msgInsModProdotti, "Esiste già un prodotto con la stessa descrizione, modello e marca");
                }
                else
                {
                    //Controllare
                    codSql = "SELECT COUNT(*) AS NProd " +
                        "FROM Prodotti " +
                        "WHERE UPPER(ModelloProdotto) = '" + modelloInsModProdotti.Value.ToUpper() + "' " +
                        "AND UPPER(DescrizioneProdotto) = '" + descrizioneInsModProdotti.Value.ToUpper() + "' " +
                        "AND MarcaProdotto = '" + marcaInsModProdotti.Value.ToUpper()+"'";
                    if (Convert.ToInt32(ado.eseguiScalar(codSql, CommandType.Text)) == 0)
                    {
                        codSql = "INSERT INTO Prodotti " +
                            "VALUES ('" + modelloInsModProdotti.Value + "', '"+descrizioneInsModProdotti.Value+"', '"+marcaInsModProdotti.Value+"', '"+ immagineInsModProdotti.PostedFile.FileName + "', "+lstCatInsModProdotti.Value+", "+ Session["IdUtente"].ToString()+ ", '"+prezzoInsModProdotti.Value+"'";
                        if (scontoInsModProdotti.Value != String.Empty)
                            codSql += ", " + scontoInsModProdotti.Value;
                        else
                            codSql += ", null";
                        codSql += ", " + giacenzaInsModProdotti.Value + ", ' ')";
                        ado.eseguiNonQuery(codSql, CommandType.Text);
                        Response.Redirect(Request.RawUrl);
                    }
                    else
                        stampaErrori(msgInsModProdotti, "Esiste già un prodotto con la stessa descrizione, modello e marca");
                }

                modelloInsModProdotti.Value = String.Empty;
                descrizioneInsModProdotti.Value = String.Empty;
                marcaInsModProdotti.Value = String.Empty;
                lstCatInsModProdotti.SelectedIndex = -1;
                prezzoInsModProdotti.Value = String.Empty;
                scontoInsModProdotti.Value = String.Empty;
                giacenzaInsModProdotti.Value = String.Empty;
                titoloSezInsModProdotti.InnerText = "Inserimento Tipo Carta";
                btnInsModProdotti.Text = "<i class='fa fa-plus' aria-hidden='true'></i> Salva";
            }
            
        }

        private bool uploadImgProdotto()
        {
            bool ret = false;

            try
            {
                immagineInsModProdotti.PostedFile.SaveAs(Server.MapPath("img/") + immagineInsModProdotti.PostedFile.FileName);
            }
            catch (Exception ex)
            {
                ret = true;
                stampaErrori(msgInsModProdotti, "Errore: "+ex.Message);
            }

            return ret;
        }

        protected void lstGestImg_ServerChange(object sender, EventArgs e)
        {
            if (lstGestImg.Value == "nuova")
                contImgProd.Visible = true;
            else
                contImgProd.Visible = false;
        }
    }
}