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
    public partial class register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                // Default UserStore constructor uses the default connection string named: DefaultConnection
                var userStore = new UserStore<IdentityUser>();
                var manager = new UserManager<IdentityUser>(userStore);

                var user = new IdentityUser() { UserName = txtUsername.Text };
                IdentityResult result = manager.Create(user, txtPassword.Text);

                if (result.Succeeded)
                {
                    var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
                    var userIdentity = manager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                    authenticationManager.SignIn(new AuthenticationProperties() { }, userIdentity);
                    Response.Redirect("admin/main-menu.aspx");
                    //StatusMessage.Text = string.Format("User {0} was created successfully!", user.UserName);
                    //lblStatus.CssClass = "label label-success";
                }
                else
                {
                    lblStatus.Text = result.Errors.FirstOrDefault();
                    lblStatus.CssClass = "label label-danger";
                }
            }
            catch (Exception err)
            {
                Server.Transfer("/error.aspx");
            }
        }
    }
}