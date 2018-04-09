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
using TCPNEW;

public partial class PieceWorkPro : BasePage
{
    adamClass adam = new adamClass(); 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) 
        {
            
            Session["orderby"] = "pw.id";
            Session["orderdir"] = "ASC";

            this.dropWorkKinds.DataSource = ProgressDataTcp.GetWorkKinds();
            this.dropWorkKinds.DataBind();
            this.dropWorkKinds.Items.Insert(0,new ListItem("--","0"));

            this.dropType.DataSource = ProgressDataTcp.GetSystemCode();
            this.dropType.DataBind();
            this.dropType.Items.Insert(0, new ListItem("--", "0"));

            this.dvPieceWorkPro.DataSource = ProgressDataTcp.GetPieceWorkPro(strSQL());
            this.dvPieceWorkPro.DataBind();
        }
    }
    /// <summary>
    /// get qurey string 
    /// </summary>
    /// <returns></returns>
    public string strSQL() 
    {
        StringBuilder strbSQL = new StringBuilder("");
        strbSQL.Append("select pw.id,sdprice,coefficient,price,wk.name wkinds,sc.systemCodeName wtype,comment from tcpc1.dbo.PieceWorkPro pw ");
        strbSQL.Append("join tcpc1.dbo.workkinds wk on pw.wKindsID=wk.id ");
        strbSQL.Append("join tcpc0.dbo.systemCode sc on pw.wTypeID=sc.systemCodeID where 1=1 ");
        if (txtSdprice.Text.Trim() != "")
        {
            strbSQL.Append(" and pw.sdprice=" + txtSdprice.Text.Trim());
        }
        if (txtCoeff.Text.Trim() != "")
        {
            strbSQL.Append(" and pw.coefficient=" + txtCoeff.Text.Trim());
        }
        if (txtPrice.Text.Trim() != "")
        {
            strbSQL.Append(" and pw.price=" + txtPrice.Text.Trim());
        }
        if (dropType.SelectedIndex != 0)
        {
            strbSQL.Append(" and wtypeId=" + dropType.SelectedItem.Value);
        }
        if (dropWorkKinds.SelectedIndex != 0)
        {
            strbSQL.Append(" and wkindsId=" + dropWorkKinds.SelectedItem.Value);
        }
        strbSQL.Append(" order by " + Session["orderby"].ToString() + " " + Session["orderdir"].ToString());

        return strbSQL.ToString();
    }
    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        this.Page.Response.Redirect("PieceWorkProEdit.aspx");
    }
    protected void dvPieceWorkPro_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "edit") 
        {
            int index = Convert.ToInt32(e.CommandArgument);
            this.Page.Response.Redirect("PieceWorkProEdit.aspx?id=" + this.dvPieceWorkPro.DataKeys[index].Value.ToString());
        }
    }
    protected void dvPieceWorkPro_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.dvPieceWorkPro.PageIndex = e.NewPageIndex;
        this.dvPieceWorkPro.DataSource = ProgressDataTcp.GetPieceWorkPro(strSQL());
        this.dvPieceWorkPro.DataBind();
    }
    protected void dvPieceWorkPro_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        ProgressDataTcp.DeletePieceWorkPro(this.dvPieceWorkPro.DataKeys[e.RowIndex].Value.ToString());

        this.dvPieceWorkPro.DataSource = ProgressDataTcp.GetPieceWorkPro(strSQL());
        this.dvPieceWorkPro.DataBind();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.dvPieceWorkPro.DataSource = ProgressDataTcp.GetPieceWorkPro(strSQL());
        this.dvPieceWorkPro.DataBind();
    }
    protected void dvPieceWorkPro_Sorting(object sender, GridViewSortEventArgs e)
    {
        Session["orderby"] = e.SortExpression;
        if(Session["orderdir"] == "ASC")
        {
            Session["orderdir"] = "DESC";
        }
        else
        {
           Session["orderdir"] = "ASC";
        }
        this.dvPieceWorkPro.DataSource = ProgressDataTcp.GetPieceWorkPro(strSQL());
        this.dvPieceWorkPro.DataBind();
    }
}
