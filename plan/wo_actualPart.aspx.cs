using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.IO;
using adamFuncs;
using System.Configuration;
using System.Collections;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using QCProgress;
using CommClass;


public partial class plan_wo_actualPart : BasePage
{
    static string strConn = ConfigurationManager.AppSettings["SqlConn.BarCodeSys"];
    protected void Page_Load(object sender, EventArgs e)
    {
        string id = Request.QueryString["id"];
        BindData(id);
    }

    private void BindData(string id)
    {



        gvlist.DataSource = GetWoActRelList(id);
        gvlist.DataBind();
    }
    public DataTable GetWoActRelList(string id)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@id", id);

            return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "sp_wo_selectActualPart", param).Tables[0];
        }
        catch
        {
            return null;
        }

    }
    protected void btn_back_Click(object sender, EventArgs e)
    {
        Redirect("wo_actualReleaseEX.aspx?rt=" + DateTime.Now.ToFileTime().ToString());
    }
    protected void gvlist_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Link")
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;

            this.Redirect("/plan/qad_bomviewdoc.aspx?cmd=newwo&part=" + gvlist.DataKeys[index].Values["wod_part"].ToString());
        }
    }
}