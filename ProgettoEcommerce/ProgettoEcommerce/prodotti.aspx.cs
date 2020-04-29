using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProgettoEcommerce
{
    public partial class category : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["IdUtente"] != null && Session["TipoUtente"] != null)
            {
                gestUtenteLoggato();
            }
            else
            {
                gestUtenteNonLoggato();
            }
        }

        private void gestUtenteLoggato()
        {
            navUtenteCarrrello.InnerHtml = "<li class='nav-item'><a href='#' class='icons'><i class='ti-user' aria-hidden='true'></i></a></li><li class='nav-item'><a href='#' class='icons'><i class='ti-shopping-cart' aria-hidden='true'></i></a></li>";
        }

        private void gestUtenteNonLoggato()
        {
            navUtenteCarrrello.InnerHtml = "";
        }
    }
}