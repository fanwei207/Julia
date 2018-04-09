using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;
using System.Data;
using NPOI.SS.Formula.Functions;
using System.Configuration;

public partial class Rec_RecipientConfig : System.Web.UI.Page
{
    String strConn = ConfigurationSettings.AppSettings["SqlConn.Conn_WF"];
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string sql="select reportname,id from WorkFlow.dbo.Rec_RecipientConfig";
            ddl_report.DataSource = SqlHelper.ExecuteDataset(strConn, CommandType.Text, sql);
            
            ddl_report.DataBind();
            ddl_report.Items.Add(new ListItem("请选择", "0"));
            if (Request.QueryString["reportid"]!=null)
            {
                ddl_report.SelectedValue = Request.QueryString["reportid"];
                string id = "'" + ddl_report.SelectedValue + "'";

                string strsql = string.Empty;

                if ("0".Equals(ddl_report.SelectedValue.ToString()))
                {
                    txtCC.Text = "";
                    txtConsignee.Text = "";
                }
                else
                {
                    strsql = "select sendto,copyto from WorkFlow.dbo.Rec_RecipientConfig where id=" + id;
                    SqlDataReader reader = SqlHelper.ExecuteReader(strConn, CommandType.Text, strsql);
                    while (reader.Read())
                    {
                        txtCC.Text = reader["copyto"].ToString();
                        txtConsignee.Text = reader["sendto"].ToString();
                    }
                    reader.Close();
                }
                
            }
            else  ddl_report.SelectedValue = "0";
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("Rec_AddReport.aspx");
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string sento = txtConsignee.Text.Trim();
        string copyto = txtCC.Text.Trim();

        string sql = "sp_Rec_updateEmial";

        SqlParameter[] param = new SqlParameter[5];

        param[0] = new SqlParameter("@id",ddl_report.SelectedValue.ToString());
        param[1] = new SqlParameter("@sendto",txtConsignee.Text.Trim());
        param[2] = new SqlParameter("@copyto",txtCC.Text.Trim());
        param[3] = new SqlParameter("@modifyby",Convert.ToInt32(Session["uID"]));
        param[4] = new SqlParameter("@modifydate", DateTime.Now.Date);

        if (SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, sql, param)>0)
        {
            ltlAlert.Text = "alert('邮箱修改成功');";
        }
        else
            ltlAlert.Text = "alert('邮箱修改失败');";

    }
    protected void ddl_report_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id ="'"+ ddl_report.SelectedValue.ToString()+"'";

        string sql = string.Empty;

        if ("0".Equals(ddl_report.SelectedValue.ToString()))
        {
            txtCC.Text = "";
            txtConsignee.Text = "";
        }
        else
        {
            sql = "select sendto,copyto from WorkFlow.dbo.Rec_RecipientConfig where id=" + id;
            SqlDataReader reader = SqlHelper.ExecuteReader(strConn, CommandType.Text, sql);
            while (reader.Read())
            {
                txtCC.Text = reader["copyto"].ToString();
                txtConsignee.Text = reader["sendto"].ToString();
            }
            reader.Close();
        }
    }
    protected void btnModSendEml_Click(object sender, EventArgs e)
    {
        //Response.Redirect("Rec_modifyEmial.aspx?type=sendto" + "&id=" +ddl_report.SelectedValue);
        ltlAlert.Text = "var w=window.open('Rec_modifyEmial.aspx?type=sendto" + "&id=" + ddl_report.SelectedValue + "'); w.focus();";
    }
    protected void btnModCopyEml_Click(object sender, EventArgs e)
    {
       // Response.Redirect("Rec_modifyEmial.aspx?type=copyto" + "&id=" + ddl_report.SelectedValue);
        ltlAlert.Text = "var w=window.open('Rec_modifyEmial.aspx?type=copyto" + "&id=" + ddl_report.SelectedValue + "'); w.focus(); ";
    }
}