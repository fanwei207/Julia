using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using adamFuncs;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Text;
using System.IO;
using System.Net;
using CommClass;
using System.Text.RegularExpressions;

public partial class plan_PCD_2101View : System.Web.UI.Page
{
    adamClass adam = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string nbr = Request.QueryString["nbr"];
            string lot = Request.QueryString["lot"];
            BindGridView(nbr, lot);
        }
    }
    public void BindGridView(string nbr, string lot)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@nbr", nbr.Trim());
            param[1] = new SqlParameter("@lot", lot.Trim());

            DataSet ds = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, "sp_PCD_selectPCD_2101View", param);
            gvlist.DataSource = ds;
            gvlist.DataBind();
        }
        catch (Exception ee)
        { ;}
    }

}