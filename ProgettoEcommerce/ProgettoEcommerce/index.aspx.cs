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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["IdUtente"] != null && Session["TipoUtente"] != null)
            {
                Response.Redirect("prodotti.aspx");
            }
        }
    }
}