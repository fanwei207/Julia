using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using adamFuncs;
using Microsoft.ApplicationBlocks.Data;
using CommClass;

public partial class EDI_EdiApproveSummary : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
        }
    }

    private DataSet GetSummary()
    {

        DataSet ds = SqlHelper.ExecuteDataset(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_edi_NotPassSummary");
        return ds;
    }

    private void BindData()
    {
        DataSet ds = GetSummary();
        lblTotal.Text = ds.Tables[0].Rows[0][0].ToString();
        lblTotalR19.Text = ds.Tables[1].Rows[0][0].ToString();
        gvlist.DataSource = ds.Tables[2];
        gvlist.DataBind();
    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        BindData();
    }
}