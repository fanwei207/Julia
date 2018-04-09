using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;

public partial class plan_Stocking_ViewDocs : System.Web.UI.Page
{
    adamClass chk = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
 
        if (Request.QueryString["sk_nbr"] != null)
        {
            string nbr = Request.QueryString["sk_nbr"];
            BindUpload(nbr);

        }
    }

    private void BindUpload(string nbr)
    {

        SqlParameter[] param = new SqlParameter[1];

        string strsql = "select fileName,filePath,CreateName from stocking_mstr where sk_nbr=@nbr and isnull(filePath,'')<>''";
        
        param[0] = new SqlParameter("@nbr", nbr);

        DataSet ds = SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.Text, strsql, param);

        gvUpload.DataSource = ds;
        gvUpload.DataBind();
    }

    protected void gvUpload_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //定义参数
        if (e.CommandName.ToString() == "View")
        {
            int intRow = Convert.ToInt32(e.CommandArgument.ToString());
            string strPath = gvUpload.DataKeys[intRow].Values["filePath"].ToString().Trim().Replace("\\", "/");
            ltlAlert.Text = "var w=window.open('" + strPath  + "','DocView','menubar=No,scrollbars = No,resizable=No,width=800,height=600,top=0,left=0'); w.focus();";

        }
    }
}