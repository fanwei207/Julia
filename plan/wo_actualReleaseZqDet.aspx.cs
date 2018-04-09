using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using Microsoft.ApplicationBlocks.Data;
using System.IO;
using IT;
using System.Diagnostics;

public partial class wo_actualReleaseZqDet : System.Web.UI.Page
{
    static string strConn = ConfigurationManager.AppSettings["SqlConn.Qad_Data"];

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
        }
    }
    protected void BindData()
    {
        try
        {
            string strName = "Select wod.*, pt_desc = pt_desc1 + '  ' + pt_desc2 ";
            strName += " From Qad_Data.dbo.wod_det wod";
            strName += " Left Join Qad_Data.dbo.pt_mstr pt On pt_domain = wod_domain And pt_part = wod_part";
            strName += " Where wod_nbr = '" + Request.QueryString["nbr"] + "' And wod_lot = '" + Request.QueryString["lot"] + "'";
            strName += "";
            gv.DataSource = SqlHelper.ExecuteDataset(strConn, CommandType.Text, strName);
            gv.DataBind();
        }
        catch
        {
            ;
        } 
    }
    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv.PageIndex = e.NewPageIndex;
        BindData();
    }

}