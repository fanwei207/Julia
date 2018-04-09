using adamFuncs;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class part_Npart_ShowPartMasterList : BasePage
{
    adamClass chk = new adamClass();
    Npart_help help = new Npart_help();
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            hidFlowID.Value = Request.QueryString["Flow_Id"];
            hidNodeID.Value = Request.QueryString["Node_Id"];
            bindDDLType();
            //ddlModule.SelectedValue = ddlModule.SelectedValue.ToString();
            //ddlModule.SelectedValue = "00000000-0000-0000-0000-000000000000";
            
        }
        Bind();
        //txtApplyNo.Text = "0";
        //txtQad.Text = "0";
        //ddlSatus.SelectedValue = "1";

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (ddlModule.SelectedValue == "0")
        {
            ltlAlert.Text = "alter('请选择模板')";
            return;
        }
        else
        {
            Bind();
        }

    }    
    private DataTable getGvColByTypeDetForSupplier(string mstrID)
    {
        string sqlstr = "sp_Npart_getGvColByTypeDetForNPartMasterList";

        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@mstrID",mstrID)
            
            };

        return SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.StoredProcedure, sqlstr, param).Tables[0];
    }

    private void Bind()
    {
        DataTable dt = getNpartMstr();
        gvDet.DataSource = dt;
        //if (dt.Rows.Count > 0)
            gvDet.DataBind();
        //else
        //    ltlAlert.Text = "alert('无数据')";
    }
    private DataTable getNpartMstr()
    {
        string str = "sp_NPart_SelectMstrList";
        SqlParameter[] param = new SqlParameter[4];
        param[0] = new SqlParameter("@moduleNo", ddlModule.SelectedValue.ToString());
        param[1] = new SqlParameter("@applyNo", txtApplyNo.Text);
        param[2] = new SqlParameter("@qad", txtQad.Text);
        param[3] = new SqlParameter("@status", ddlSatus.SelectedValue.ToString());

        return SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.StoredProcedure, str, param).Tables[0];
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        DataTable dtdown = exportExcelFromPage(ddlModule.SelectedValue.ToString(), txtApplyNo.Text, txtQad.Text, ddlSatus.SelectedValue.ToString());
        StringBuilder title = new StringBuilder("");

        foreach (DataColumn dc in dtdown.Columns)
        {
            title.Append("100^<b>");
            title.Append(dc.ColumnName);
            title.Append("</b>~^");
        }


        if (dtdown != null && dtdown.Rows.Count > 0)
        {
            ExportExcel(title.ToString(), dtdown, false);
        }
        else
        {
            ltlAlert.Text = "alert('没有数据')";
        }
    }
    private DataTable exportExcelFromPage(string ModuleNo, string applyNo, string qad,string status)
    {
        string str = "sp_NPart_exportMasterListExcelFromPage";

        SqlParameter[] param = new SqlParameter[4];
        param[0] = new SqlParameter("@moduleNo", ModuleNo);
        param[1] = new SqlParameter("@applyNo", applyNo);
        param[2] = new SqlParameter("@qad", qad);
        param[3] = new SqlParameter("@status", status);

        return SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.StoredProcedure, str, param).Tables[0];
    }
    protected void ddlModule_SelectedIndexChanged(object sender, EventArgs e)
    {
        Bind();
    }
    private void bindDDLType()
    {
        ddlModule.Items.Clear();
        ddlModule.DataSource = help.selectAllTypeMstr();
        ddlModule.DataBind();
        ddlModule.Items.Insert(0, new ListItem("--", "0"));
    }
    protected void ddlSatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        Bind();
    }

    //private void bindStatus()
    //{
    //    ddlSatus.Items.Clear();
    //    ddlSatus.DataSource = getNPartMasterStatus();
    //    ddlSatus.DataBind();
    //    ddlSatus.Items.Insert(0, new ListItem("--", "1"));
    //}

    //private DataTable getNPartMasterStatus()
    //{
    //    string sqlstr = "sp_Npart_selectMstrStatus";

    //    return SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.StoredProcedure, sqlstr).Tables[0];
    //}
    protected void gvDet_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        gvDet.CurrentPageIndex = e.NewPageIndex;
        Bind();
    }
}