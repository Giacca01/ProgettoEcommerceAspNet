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
    public partial class ModificaProfilo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
                adoNet.impostaConnessione("App_Data/DBEcommerce.mdf");
            if (Session["IdUtente"] == null && Session["TipoUtente"] == null)
                Response.Redirect("login.aspx");
            else
            {
                stampaFormModifica();
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
                    k++;
                    pwdModProfilo.Value = tab.Rows[i].ItemArray[k].ToString();
                    k++;
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
    }
}