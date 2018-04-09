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

public partial class EDI_FIFOshowDet : BasePage
{
    adamClass chk = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindGridView();
        }
    }
    protected void BindGridView()
    {
        try
        {
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@CustomerPO", txtSOPO.Text.Trim());
            DataSet ds = SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.StoredProcedure, "sp_FIFO_selectFIFODet",parm);
            gvlist.DataSource = ds;
            gvlist.DataBind();
        }
        catch (Exception ee)
        { ;}
    }
    protected void gvlist_PageIndexChanged(object sender, EventArgs e)
    {

    }
    protected void gvlist_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvlist.PageIndex = e.NewPageIndex;
        BindGridView();
    }
    protected void btnOK_Click(object sender, EventArgs e)
    {
        try
        {
            int name = Convert.ToInt32(Session["uID"]);
            string strSQL = "sp_FIFO_insertFIFODethis";
            SqlParameter[] parm = new SqlParameter[2];
            parm[0] = new SqlParameter("@uID", Convert.ToInt32(Session["uID"]));
           
            parm[1] = new SqlParameter("@retValue", SqlDbType.Bit);
            parm[1].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, strSQL, parm);
            ltlAlert.Text = "alert('操作成功.');";
            BindGridView();
        }
        catch (Exception)
        {
            ltlAlert.Text = "alert('操作失败.');";
        }

    }
    protected void btnquery_Click(object sender, EventArgs e)
    {
        BindGridView();
    }
}