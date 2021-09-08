using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;

namespace SpiderDocs_ClientControl
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["userName"] = null;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("Client.aspx");
            /*
            bool valid = false;
            using(PrincipalContext context = new PrincipalContext(ContextType.Domain,"spider"))
            {
                valid = context.ValidateCredentials(txtUserName.Text, txtPassword.Text);

                if(valid)
                {
                    Session["userName"] = txtUserName.Text;
                    Response.Redirect("Client.aspx");
                }
                else
                {
                    lblMsg.Visible = true;
                }

            }
            */
        }


    }
}