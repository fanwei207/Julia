using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using SampleManagement;

public partial class supplier_SampleReceiveNotesMaintain : BasePage
{
    Sample sap = new Sample();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //this.SecurityCheck = securityCheck.issecurityCheck(Convert.ToInt32(Session["uID"]), Convert.ToInt32(Session["uRole"]), 320150);
            

            if (String.IsNullOrEmpty(Request.QueryString["bos_nbr"]) || Request.QueryString["bos_nbr"].ToString() == "0")
            {
                Response.Redirect("SampleNotesReceiveLists.aspx");
            }
            else
            {
                txt_bosnbr.Enabled = false;
                txt_bosnbr.Text = Request.QueryString["bos_nbr"].ToString();
                BindBosData();
            }

            Bindgv_BosDet();
        }
    }

    private void BindBosData()
    {
        string strBosnbr = txt_bosnbr.Text.Trim().ToString();
        DataTable dt = sap.getBosMstr(strBosnbr, "0");
        txt_vend.Text = dt.Rows[0]["bos_vend"].ToString();
        txt_vendName.Text = dt.Rows[0]["bos_vendName"].ToString();
        txt_bosDate.Text = String.Format("{0:yyyy-MM-dd HH:MM:ss}", Convert.ToDateTime(dt.Rows[0]["bos_createddate"].ToString()));
        txt_Bosrmks.Text = dt.Rows[0]["bos_rmks"].ToString();

        txt_ReceiveNotes.Text = dt.Rows[0]["Bos_receiptRmks"].ToString();
        chkIsReceipt.Checked = Convert.ToBoolean(dt.Rows[0]["bos_receiptIsConfirm"].ToString());
    }

    private void Bindgv_BosDet()
    {
        string strBosnbr = txt_bosnbr.Text.ToString();
        DataTable dt = sap.getBosDet(strBosnbr);
        gv_det.DataSource = dt;
        gv_det.DataBind();
    }

    protected void gv_det_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_det.PageIndex = e.NewPageIndex;
        Bindgv_BosDet();
    }
    protected void gv_det_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //if (e.Row.Cells[11].Text == "2")
            //{ 
            //    e.Row.Cells[11].Text = "--"; 
            //}
            //else if (e.Row.Cells[11].Text == "1")
            //{ 
            //    e.Row.Cells[11].Text = "通过"; 
            //}
            //else
            //{ 
            //    e.Row.Cells[11].Text = "不通过"; 
            //}

            if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
            {
                Label lblTech = (Label)e.Row.FindControl("lblTech");

                if (lblTech.Text == "2")
                { 
                    lblTech.Text = "--"; 
                }
                else if (lblTech.Text == "1")
                { 
                    lblTech.Text = "通过"; 
                }
                else
                { 
                    lblTech.Text = "不通过"; 
                }
            }
        }
    }
    protected void gv_det_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.ToString() == "EditDoc")
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            string strBosNbr = txt_bosnbr.Text.ToString();
            string strBosdetline = gv_det.Rows[index].Cells[1].Text.ToString();
            string strBosdetCode = Server.UrlEncode(gv_det.Rows[index].Cells[2].Text.ToString());
            string strBosdetQad = gv_det.Rows[index].Cells[3].Text.ToString();
            if (strBosdetQad == string.Empty) strBosdetQad = string.Empty;
            Response.Redirect("SampleNotesAccDoc.aspx?Mode=TechCheck&strNbr=" + strBosNbr + "&line=" + strBosdetline + "&code=" + Server.UrlEncode(strBosdetCode) + "&qad=" + strBosdetQad);
        }

    }
    protected void btn_Back_Click(object sender, EventArgs e)
    {
        Response.Redirect("SampleNotesTechCheckLists.aspx?bos_nbr=" + txt_bosnbr.Text);
    }
    protected void gv_det_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gv_det.EditIndex = -1;
        Bindgv_BosDet();
    }
    protected void gv_det_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        //Mark By Shanzm 2014-02-26:单独一行收货了，也可以验收，暂时放为真
        chkIsReceipt.Checked = true;
        if (chkIsReceipt.Checked)
        {
            string strbosnbr = txt_bosnbr.Text.ToString();
            int bosLine = Convert.ToInt32(gv_det.Rows[e.RowIndex].Cells[1].Text.ToString());
            string strbosdetcode = gv_det.Rows[e.RowIndex].Cells[2].Text.ToString();
            string techChkResult = ((DropDownList)gv_det.Rows[e.RowIndex].FindControl("ddl_techConfirm")).SelectedValue;
            string uID = Session["uID"].ToString();
            string reason = ((TextBox)gv_det.Rows[e.RowIndex].FindControl("txtReason")).Text.Trim();
            if (techChkResult == "0" && reason == "")
            {
                this.Alert("验收未通过时必须注明原因");
                return;
            }

            if (Convert.ToInt32(techChkResult) < 2)
            {
                if (sap.updateBosdetTechResult(strbosnbr, bosLine, strbosdetcode, uID, techChkResult,reason))
                {
                    gv_det.EditIndex = -1;
                }
                else
                {
                    gv_det.EditIndex = -1;
                    this.Alert("验收失败：部件号尚未维护，或尚未收货！");
                }
            }
            else
            {
                this.Alert("请选择一项验收结果！");
            }
        }
        else 
        {
            this.Alert("无法验收！该打样单尚未收货！");
        }

        Bindgv_BosDet();
    }
    protected void gv_det_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gv_det.EditIndex = e.NewEditIndex;
        Bindgv_BosDet();
    }
}