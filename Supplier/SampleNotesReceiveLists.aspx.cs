using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SampleManagement;

public partial class supplier_SampleNotesReceiveLists : BasePage
{
    Sample sap = new Sample();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //this.SecurityCheck = securityCheck.issecurityCheck(Convert.ToInt32(Session["uID"]), Convert.ToInt32(Session["uRole"]), 320130);
            
            BindVend(); 
            Bindgv_Bos(); 
        }
    }

    private void Bindgv_Bos()
    {
        string strBosnbr = txt_bosnbr.Text.Trim().ToString();
        string strVend = ddl_vend.SelectedValue.ToString();
        string IsRecieve  = ddl_ReceiveState.SelectedValue.ToString();

        DataTable dt = sap.getBosMstrForRecieve(strBosnbr, strVend, IsRecieve);
        if (dt.Rows.Count == 0)
        {
            dt.Rows.Add(dt.NewRow());
            gv_bos_mstr.DataSource = dt;
            gv_bos_mstr.DataBind();
            int columnCount = gv_bos_mstr.Rows[0].Cells.Count;
            gv_bos_mstr.Rows[0].Cells.Clear();
            gv_bos_mstr.Rows[0].Cells.Add(new TableCell());
            gv_bos_mstr.Rows[0].Cells[0].ColumnSpan = columnCount;
            gv_bos_mstr.Rows[0].Cells[0].Text = "没有记录";
            gv_bos_mstr.RowStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
        }
        else
        {
            gv_bos_mstr.DataSource = dt;
            gv_bos_mstr.DataBind();
        }
    }

    protected void BindVend()
    {
        ddl_vend.DataSource = sap.getBosSuppliers((SysRole)Enum.Parse(typeof(SysRole), "Supplier", true)); //ddlUserType.SelectedValue
        ddl_vend.DataBind();
        ddl_vend.Items.Insert(0, new ListItem("--", "0"));
    }
    protected void btn_Cancel_Click(object sender, EventArgs e)
    {
        txt_bosnbr.Text = "";
        ddl_vend.SelectedIndex = -1; 
        Bindgv_Bos(); 
    }
    protected void btn_Search_Click(object sender, EventArgs e)
    {
        Bindgv_Bos();
    }

    protected void gv_bos_mstr_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (gv_bos_mstr.DataKeys[e.Row.RowIndex]["bos_receiptIsConfirm"].ToString().ToLower() == "true")
            {
                ((LinkButton)e.Row.FindControl("linkDoReceive")).Text = "已收";
                //((LinkButton)e.Row.FindControl("linkDoReceive")).Font.Underline = false;

            }
            if (e.Row.Cells[5].Text.ToString().ToLower() == "true")
            {
                e.Row.Cells[5].Text = "已确认";
            }
            else
            {
                e.Row.Cells[5].Text = " ";
            }

            if (gv_bos_mstr.DataKeys[e.Row.RowIndex]["bos_isCanceled"].ToString().ToLower() == "true")
            {
                //((LinkButton)e.Row.FindControl("linkConfirm")).Text = "已确认";
                e.Row.Cells[6].Text = "取消";
            }
            else
            {
                e.Row.Cells[6].Text = " ";
            }
        }

    }

    protected void gv_bos_mstr_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_bos_mstr.PageIndex = e.NewPageIndex;
        Bindgv_Bos();
    }

    protected void gv_bos_mstr_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.ToString() == "DoReceive")
        {   
            string bosNbr = e.CommandArgument.ToString(); 
            Response.Redirect("SampleNotesReceiveConfirm.aspx?bos_nbr=" + bosNbr );
        } 
    }
}
