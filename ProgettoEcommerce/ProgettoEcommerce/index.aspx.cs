using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProgettoEcommerce
{
    public partial class index : System.Web.UI.Page
    {
        /**********************/
        /* Routine Principale */
        /**********************/
        protected void Page_Load(object sender, EventArgs e)
        {
            //Se l'utente è loggato lo mando alla pagina prodotti
            if (Session["IdUtente"] != null && Session["TipoUtente"] != null)
                Response.Redirect("prodotti.aspx");
        }
    }
}