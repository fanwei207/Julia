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

public partial class TSK_TestingView : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
        }
    }
    protected void BindData()
    {
        string _tskNbr = Request.QueryString["tskNbr"];
        Int32 _detID = Convert.ToInt32(Request.QueryString["detID"]);

        gv.DataSource = TaskHelper.SelectTaskTesting(_tskNbr, _detID);
        gv.DataBind();
    }
    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView rowView = (DataRowView)e.Row.DataItem;
            if (!Convert.ToBoolean(rowView["tskr_table"]))
            {
                e.Row.Cells[0].Text = "-";
                e.Row.Cells[0].Style.Add("text-align", "right");
            } 
        }
    }
}