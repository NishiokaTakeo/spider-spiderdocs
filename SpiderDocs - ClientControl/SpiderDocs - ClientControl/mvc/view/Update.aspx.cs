using SpiderDocs_ClientControl.Helpers;
using System;
using System.Configuration;
using System.Data.Common;

namespace SpiderDocs_ClientControl
{
    public partial class About : System.Web.UI.Page
    {

        string sql;
        protected static String conn = ConfigurationManager.ConnectionStrings["spiderdocs_conn"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            // if(Session["userName"] == null || Session["userName"].ToString() == "")
            //     Response.Redirect("login.aspx");  
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            clearClientFiels();

            DbConnection sqlConn = DbManager.GetSqlConnection(conn);
            sqlConn.Open();

            sql = "select * from updates where id = " + GridView1.SelectedRow.Cells[1].Text;

            DbCommand command = DbManager.GetSqlCommand(sql, sqlConn);
            DbDataReader dr = command.ExecuteReader();

            dr.Read();

            txtId.Text          =  dr["id"].ToString();
            txtVersion.Text     = (dr["version"]        == DBNull.Value) ? string.Empty : (string)dr["version"];
            txtUrl.Text         = (dr["url_download"]   == DBNull.Value) ? string.Empty : (string)dr["url_download"];
            txtVersion.Focus();
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            clearClientFiels();
            GridView1.SelectedIndex = -1;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            DbConnection sqlConn = DbManager.GetSqlConnection(conn);
            DbCommand command;
            sqlConn.Open();

            try
            {


                if(txtId.Text != "")
                {
                    sql = "update  updates set ";
                    sql = sql + "  version          = '" + txtVersion.Text + "'";
                    sql = sql + " ,url_download     = '" + txtUrl.Text + "'";
                    sql = sql + "  where id         = '" + txtId.Text + "'";

                    command = DbManager.GetSqlCommand(sql, sqlConn);
                    command.ExecuteNonQuery();

                }
                else
                {
                    sql = "insert into updates ( ";
                    sql = sql + "   version";
                    sql = sql + "  ,url_download";

                    sql = sql + " ) values (";

                    sql = sql + "'"  + txtVersion.Text + "'";
                    sql = sql + ",'" + txtUrl.Text + "'";

                    sql = sql + " )";

                    command = DbManager.GetSqlCommand(sql, sqlConn);
                    command.ExecuteNonQuery();
                }

                GridView1.DataBind();
                lblMsg.Text = "Operation Performed!";

            }

            catch(Exception error)
            {
                lblMsg.Text = error.Message;
            }

            finally
            {
                sqlConn.Close();
            }
        }

        private void clearClientFiels()
        {
            txtId.Text      = "";
            txtVersion.Text = "";
            txtUrl.Text     = "";
            txtVersion.Focus();
        }

        protected void AjaxFileUpload_UploadComplete(object sender, AjaxControlToolkit.AjaxFileUploadEventArgs e)
        {

            string FileName;
            string filePath = e.FileName;
            string simbol = "";

            try
            {


                if(e.FileName.IndexOf("\\") != -1)
                {
                    simbol = "\\";
                }

                if(e.FileName.IndexOf("//") != -1)
                {
                    simbol = "//";
                }

                if(simbol != "")
                {
                    FileName = e.FileName.Substring(e.FileName.LastIndexOf(simbol)).Replace(simbol, "");
                }
                else
                {
                    FileName = e.FileName;
                }

                AjaxFileUpload.SaveAs(@"\\blackhouse\Updates\" + FileName);

                lblMsg.Text = "The file has been saved at \\blackhouse\\Updates\\" + FileName;

            }
            catch(Exception error)
            {
                lblMsg.Text = error.Message;

            }
        }


    }
}
