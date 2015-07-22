using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

//identity package references
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;

namespace Lab_4
{
    public partial class logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //logout and redirect 
                var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
                authenticationManager.SignOut();
                Response.Redirect("~/login.aspx");
            }
            catch (Exception err)
            {
                Server.Transfer("/error.aspx");
            }
        }
    }
}