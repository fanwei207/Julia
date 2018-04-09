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
using System.IO;
using System.Collections.Generic;
using System.Web.Mail;
using System.Text;

using RD_WorkFlow;

public partial class RDW_RDW_DocList : BasePage
{
    RDW rdw = new RDW();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           
            BindData();
            
        }
    }
    protected void BindData()
    {
        //定义参数
        string strMID = Convert.ToString(Request.QueryString["mid"]);

        RDW_Header rh = rdw.SelectRDWHeader(strMID);
        lblProjectData.Text = rh.RDW_Project;
        lblProdCodeData.Text = rh.RDW_ProdCode;
        lblProdDescData.Text = rh.RDW_ProdDesc;
        lblStartDateData.Text = rh.RDW_StartDate;
        lblEndDateData.Text = rh.RDW_EndDate;

        //BindMessage();
        //BindUpload();
        BindAll();
        MergeRows(gv_all, 0, "lk_StepName");
    }

    protected void BindAll()
    {
        string strMID = Convert.ToString(Request.QueryString["mid"]);
        gv_all.DataSource = rdw.SelectRDWMessageAndDocs(strMID);
        gv_all.DataBind();
    }
    protected void BindMessage()
    {
        //定义参数
        string strMID = Convert.ToString(Request.QueryString["mid"]);

        gvMessage.DataSource = rdw.SelectRDWMessage(strMID);
        gvMessage.DataBind();
    }
    protected void BindUpload()
    {
        //定义参数
        string strMID = Convert.ToString(Request.QueryString["mid"]);

        gvUpload.DataSource = rdw.SelectRDWDocs(strMID);
        gvUpload.DataBind();
    }
    protected void gvUpload_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //定义参数
        int intRow = 0;
        string strDocID = string.Empty;

        if (e.CommandName.ToString() == "View")
        {
            intRow = Convert.ToInt32(e.CommandArgument.ToString());
            string strDetID = gvUpload.DataKeys[intRow].Values["RDW_DetID"].ToString();
            string strPhysicalName = gvUpload.DataKeys[intRow].Values["RDW_PhysicalName"].ToString();
            string path = gvUpload.DataKeys[intRow].Values["RDW_Path"].ToString();
            if (string.IsNullOrEmpty(path))
            {
                ltlAlert.Text = "var w=window.open('/TecDocs/ProjectTracking/" + strDetID + "/" + strPhysicalName + "','DocView','menubar=No,scrollbars = No,resizable = Yes,width=8,height=6,top=0,left=0'); w.focus();";
            }
            else
            {
                ltlAlert.Text = "var w=window.open('" + path + strPhysicalName + "','DocView','menubar=No,scrollbars = No,resizable = Yes,width=8,height=6,top=0,left=0'); w.focus();";
            }
        }
        if (e.CommandName.ToString() == "StepName")
        {
            intRow = ((e.CommandSource as Control).Parent.Parent as GridViewRow).RowIndex;
            string strDetID = gvUpload.DataKeys[intRow].Values["RDW_DetID"].ToString();
            string strMID = Convert.ToString(Request.QueryString["mid"]);

            Response.Redirect("/RDW/RDW_DetailEdit.aspx?mid=" + strMID + "&did=" + strDetID + "&fr=doclist");
        }
    }
    
    protected void btnClose_Click(object sender, EventArgs e)
    {
        ltlAlert.Text = "window.close();";
    }

    protected void gvUpload_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //e.Row.Cells[0].Text = e.Row.Cells[0].Text.Replace("\r\n", "<br />");
            LinkButton link = e.Row.FindControl("lk_StepName") as LinkButton;
            link.Text = link.Text.Replace("\r\n", "<br />");
        }
    }
    protected void gvMessage_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //e.Row.Cells[0].Text = e.Row.Cells[0].Text.Replace("\r\n", "<br />");
            LinkButton link = e.Row.FindControl("lk_StepName") as LinkButton;
            link.Text = link.Text.Replace("\r\n", "<br />");
        }
    }
    protected void gvMessage_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.ToString() == "StepName")
        {
            int intRow = ((e.CommandSource as Control).Parent.Parent as GridViewRow).RowIndex;
            string strDetID = gvMessage.DataKeys[intRow].Values["RDW_DetID"].ToString();
            string strMID = Convert.ToString(Request.QueryString["mid"]);

            Response.Redirect("/RDW/RDW_DetailEdit.aspx?mid=" + strMID + "&did=" + strDetID + "&fr=doclist");
        }
    }
    protected void gv_all_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int intRow = 0;
        string strDetID = string.Empty;
        if (e.CommandName.ToString() == "StepName")
        {
            intRow = ((e.CommandSource as Control).Parent.Parent as GridViewRow).RowIndex;
            strDetID = gv_all.DataKeys[intRow].Values["RDW_DetID"].ToString();
            string strMID = Convert.ToString(Request.QueryString["mid"]);

            Response.Redirect("/RDW/RDW_DetailEdit.aspx?mid=" + strMID + "&did=" + strDetID + "&fr=doclist");
        }
        if (e.CommandName.ToString() == "View")
        {
            //intRow = ((e.CommandSource as Control).Parent.Parent as GridViewRow).RowIndex;;
            intRow = Convert.ToInt32(e.CommandArgument.ToString());
            strDetID = gv_all.DataKeys[intRow].Values["RDW_DetID"].ToString();
            string strPhysicalName = gv_all.DataKeys[intRow].Values["RDW_PhysicalName"].ToString();
            string path = gv_all.DataKeys[intRow].Values["RDW_Path"].ToString();
            if (string.IsNullOrEmpty(path))
            {
                ltlAlert.Text = "var w=window.open('/TecDocs/ProjectTracking/" + strDetID + "/" + strPhysicalName + "','DocView','menubar=No,scrollbars = No,resizable = Yes,width=8,height=6,top=0,left=0'); w.focus();";
            }
            else
            {
                ltlAlert.Text = "var w=window.open('" + path + strPhysicalName + "','DocView','menubar=No,scrollbars = No,resizable = Yes,width=8,height=6,top=0,left=0'); w.focus();";
            }
        }
    }
    protected void gv_all_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //e.Row.Cells[0].Text = e.Row.Cells[0].Text.Replace("\r\n", "<br />");
            LinkButton link = e.Row.FindControl("lk_StepName") as LinkButton;
            link.Text = link.Text.Replace("\r\n", "<br />");
        }
    }
    public static void MergeRows(GridView gvw, int col, string controlName)
    {
        for (int rowIndex = gvw.Rows.Count - 2; rowIndex >= 0; rowIndex--)
        {
            GridViewRow row = (GridViewRow)gvw.Rows[rowIndex];

            GridViewRow previousRow = (GridViewRow)gvw.Rows[rowIndex + 1];

            LinkButton row_lnb = row.Cells[col].FindControl(controlName) as LinkButton;
            LinkButton previousRow_lnb = previousRow.Cells[col].FindControl(controlName) as LinkButton;

            Label row_lbl = row.Cells[col + 1].FindControl("lbl_message") as Label;
            Label previousRow_lbl = previousRow.Cells[col + 1].FindControl("lbl_message") as Label;
            string me1 = (row.Cells[col + 1].FindControl("lbl_message") as Label).Text;
            (row.Cells[col + 1].FindControl("lbl_message") as Label).Text = (row.Cells[col + 1].FindControl("lbl_message") as Label).Text.Replace("\\r\\n", "<br/>").Replace("&#x0D;&lt;br&gt;","<br/>");
            
            if (row_lnb != null && previousRow_lnb != null)
            {
                if (row_lnb.Text == previousRow_lnb.Text)
                {
                    
                    row.Cells[col].RowSpan = previousRow.Cells[col].RowSpan < 1 ? 2 : previousRow.Cells[col].RowSpan + 1;
                    row.Cells[col + 1].RowSpan = row.Cells[col].RowSpan;
                    
                    previousRow.Cells[col].Visible = false;
                    previousRow.Cells[col+1].Visible = false;
                }
            }
        }
    }
}
