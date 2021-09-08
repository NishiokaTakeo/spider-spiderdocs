using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.Common;
using SpiderDocs_ClientControl.Helpers;

namespace SpiderDocs_ClientControl
{
    public partial class _Default : System.Web.UI.Page
    {
        string sql;
        protected static String conn = ConfigurationManager.ConnectionStrings["spiderdocs_conn"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            
            // if( string.IsNullOrWhiteSpace(Session["userName"]?.ToString()))
            //     Response.Redirect("login.aspx");          
            
       }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

            clearClientFiels();

            DbConnection sqlConn = DbManager.GetSqlConnection(conn);
            sqlConn.Open();

            sql = "select * from client where id = " + GridView1.SelectedRow.Cells[1].Text;

            DbCommand command = DbManager.GetSqlCommand(sql, sqlConn);
            DbDataReader dr = command.ExecuteReader();

            dr.Read();

            txtClientId.Text            = (dr["id"]                 == DBNull.Value) ? string.Empty : (string)dr["id"];
            txtClientName.Text          = (dr["client_name"]        == DBNull.Value) ? string.Empty : (string)dr["client_name"];
            txtClientCurVersion.Text    = (dr["current_version"]    == DBNull.Value) ? string.Empty : (string)dr["current_version"];
            txtClientProductKey.Text    = (dr["product_key"]        == DBNull.Value) ? string.Empty : (string)dr["product_key"];
            txtDateActivade.Text        = (dr["date_activated"]     == DBNull.Value) ? string.Empty : dr["date_activated"].ToString();
            ckActive.Checked            = (dr["enable"]             == DBNull.Value) ? false        : (bool)dr["enable"];
            txtClientId.Focus();



            dr.Close();
            sqlConn.Close();


            checkUpdateAvailable();

        }

        private void checkUpdateAvailable()
        {


            if(txtClientCurVersion.Text != "")
            {

                DbConnection sqlConn = DbManager.GetSqlConnection(conn);


                try
                {
               
                    sqlConn.Open();

                    sql =       " select id,version from updates where cast(REPLACE(version,'.','') as int) > " + txtClientCurVersion.Text.Replace(".", "");

                    DbCommand sqlCommand = DbManager.GetSqlCommand(sql, sqlConn);
                    DbDataReader dr2;
                    dr2 = sqlCommand.ExecuteReader();

                    cboUpdateAvailables.DataSource      = dr2;
                    cboUpdateAvailables.DataValueField  = "id";
                    cboUpdateAvailables.DataTextField   = "version";
                    cboUpdateAvailables.DataBind();

                    cboUpdateAvailables.Items.Insert(0, new ListItem("(Please Select)", "0"));
                    cboUpdateAvailables.SelectedIndex = 0;
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
            else
            {
                cboUpdateAvailables.Items.Clear();
            }

            
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            clearClientFiels();
            GridView1.SelectedIndex = -1;
            cboUpdateAvailables.Enabled = false;
        }

        private void clearClientFiels()
        {
            txtClientId.Text            = "";
            txtClientName.Text          = "";
            txtClientCurVersion.Text    = "";
            txtClientProductKey.Text    = "";
            txtDateActivade.Text        = "";
            ckActive.Checked            = false;
            cboUpdateAvailables.Items.Clear();
            txtClientId.Focus();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

                DbConnection sqlConn = DbManager.GetSqlConnection(conn);
                sqlConn.Open();

                try
                {



                    sql = "select * from client where id = " + txtClientId.Text;

                    DbCommand command = DbManager.GetSqlCommand(sql, sqlConn);
                    DbDataReader dr = command.ExecuteReader();

                    bool flagEdit = dr.HasRows;
                    dr.Close();

                    if(flagEdit)
                    {
                        sql = "update  client set ";
                        sql = sql + "  client_name      = '" + txtClientName.Text.Replace("'", "''") + "'";
                        sql = sql + " ,current_version  = '" + txtClientCurVersion.Text + "'";
                        sql = sql + " ,product_key      = '" + txtClientProductKey.Text + "'";
                        sql = sql + " ,enable           =  " + (ckActive.Checked ? 1 : 0);
                        sql = sql + "  where id         = '" + txtClientId.Text + "'";

                        command = DbManager.GetSqlCommand(sql, sqlConn);
                        command.ExecuteNonQuery();

                        if(cboUpdateAvailables.SelectedValue != "-1" || cboUpdateAvailables.SelectedValue != "0")
                        {

                            //delete updates availables but not installed yet
                            sql = " delete from client_update where client_id = '" + txtClientId.Text + "' and date is null";
                            command = DbManager.GetSqlCommand(sql, sqlConn);
                            command.ExecuteNonQuery();

                            //insert the update choosen
                            sql = "insert into  client_update ( ";
                            sql = sql + "  client_id";
                            sql = sql + "  ,update_id";

                            sql = sql + " ) values (";

                            sql = sql + txtClientId.Text;
                            sql = sql + "," + Convert.ToInt32(cboUpdateAvailables.SelectedValue);

                            sql = sql + " )";

                            command = DbManager.GetSqlCommand(sql, sqlConn);
                            command.ExecuteNonQuery();
                            GridView3.DataBind();

                        }

                    }
                    else
                    {
                        sql = "insert into  client ( ";
                        sql = sql + "  Id";
                        sql = sql + "  ,client_name";
                        sql = sql + "  ,current_version";
                        sql = sql + "  ,product_key";
                        sql = sql + "  ,enable";

                        sql = sql + " ) values (";

                        sql = sql + txtClientId.Text;
                        sql = sql + ",'" + txtClientName.Text.Replace("'", "''") + "'";
                        sql = sql + ",'" + txtClientCurVersion.Text + "'";
                        sql = sql + ",'" + txtClientProductKey.Text + "'";
                        sql = sql + "," + (ckActive.Checked ? 1 : 0);

                        sql = sql + " )";

                        command = DbManager.GetSqlCommand(sql, sqlConn);
                        command.ExecuteNonQuery();
                    }

                    GridView1.DataBind();
                    lblMsg.Text = "Operation Performed!";
                    cboUpdateAvailables.Enabled = true;
                    checkUpdateAvailable();

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




    }
}
