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
            //this.SecurityCheck = securityCheck.issecurityCheck(Convert.ToInt32(Session["uID"]), Convert.ToInt32(Session["uRole"]), 320130);
            

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

        lblState.Visible = Convert.ToBoolean(dt.Rows[0]["bos_isCanceled"].ToString());
        btn_Receive.Enabled = !lblState.Visible;

        txt_ReceiveNotes.Text = dt.Rows[0]["Bos_receiptRmks"].ToString();
        chk_isVendConfirm.Checked = Convert.ToBoolean(dt.Rows[0]["bos_vendIsConfirm"].ToString());
        btn_Receive.Enabled = chk_isVendConfirm.Checked;
        if (Convert.ToBoolean(dt.Rows[0]["bos_receiptIsConfirm"].ToString()))
        {
            btn_Receive.Visible = false;
        }

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
            if (gv_det.DataKeys[e.Row.RowIndex].Values["bos_det_isReceipt"].ToString().ToLower() == "true")
            {
                e.Row.Cells[6].Text = "已收";
                e.Row.Cells[9].Text = "";
            }
            else 
            {  
                LinkButton linkbtnRecieve = (LinkButton)e.Row.FindControl("btn_detRecieve");
                linkbtnRecieve.Enabled = btn_Receive.Enabled;

                if (linkbtnRecieve.Enabled)
                {
                    linkbtnRecieve.Attributes["onclick"] = "return confirm('确认收货即表示你认可收到了打样单列出的样品；是否继续?');";
                }
            }

            if (e.Row.Cells[11].Text == "2")
            { e.Row.Cells[11].Text = "--"; }
            else if (e.Row.Cells[11].Text == "1")
            { e.Row.Cells[11].Text = "通过"; }
            else
            { e.Row.Cells[11].Text = "不通过"; }

            if (e.Row.Cells[13].Text == "2")
            { e.Row.Cells[13].Text = "--"; }
            else if (e.Row.Cells[13].Text == "1")
            { e.Row.Cells[13].Text = "通过"; }
            else
            { e.Row.Cells[13].Text = "不通过"; }
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
            Response.Redirect("SampleNotesAccDoc.aspx?Mode=Receive&strNbr=" + strBosNbr + "&line=" + strBosdetline + "&code=" + Server.UrlEncode(strBosdetCode) + "&qad=" + strBosdetQad);
        }
        else if (e.CommandName.ToString() == "EditRecieve")
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            string strBosNbr = txt_bosnbr.Text.ToString();
            string strBosdetline = gv_det.Rows[index].Cells[1].Text.ToString();
            int uID = Convert.ToInt32(Session["uID"]);
            //验证打样单是否上传了全部所需文档-----By Wangdl 2014-03-17
            bool IsExist = sap.IsExistDoc(strBosNbr, Convert.ToInt32(strBosdetline));
            if (IsExist == false)
            {
                this.Alert("供应商没有上传打样单所需的全部文档，不能收货！");
                Bindgv_BosDet();
                return;
            }
            if (sap.updateBosdetReciept(strBosNbr, strBosdetline, uID))
            {
                Bindgv_BosDet();
            }
            else
            {
                this.Alert("收货失败:供应商未确认或者未填写零件号！"); 
                return;
            }
        }
    }
    protected void btn_Receive_Click(object sender, EventArgs e)
    {
        string strbos_nbr = txt_bosnbr.Text.ToString();
        string receiveRmks = txt_ReceiveNotes.Text.ToString().Trim();
        Int32 uId = Convert.ToInt32(Session["uId"].ToString());
        //验证打样单是否上传了全部所需文档-----By Wangdl 2014-03-17
        bool IsExist = sap.IsExistDoc(strbos_nbr, 0);
        if (IsExist == false)
        {
            this.Alert("没有上传打样单所需文档，不能全部收货");
            Bindgv_BosDet();
            return;
        }
        if (sap.confirmRecieveBos(strbos_nbr, receiveRmks, uId))
        {
            this.Alert("收货确认保存成功");
            btn_Receive.Visible = false;

            Bindgv_BosDet();
            return; 
        }
        else
        {
            this.Alert("收货确认保存失败:供应商未确认或者未填写零件号！");
            return; 
        }
    }
    protected void btn_Back_Click(object sender, EventArgs e)
    {
        Response.Redirect("SampleNotesReceiveLists.aspx?bos_nbr=" + txt_bosnbr.Text);
    }
    protected void gv_det_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gv_det.EditIndex = -1;
        Bindgv_BosDet();
    }
    protected void gv_det_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string strbosnbr = txt_bosnbr.Text.ToString();
        int bosLine = Convert.ToInt32(gv_det.Rows[e.RowIndex].Cells[1].Text.ToString());
        string strbosdetcode = ((TextBox)gv_det.Rows[e.RowIndex].FindControl("txt_gvdetcode")).Text;

        if (sap.updateBosdet(strbosnbr, bosLine, strbosdetcode))
        {
            gv_det.EditIndex = -1;
            Bindgv_BosDet();
        }
        else
        {
            this.Alert("更新失败：部件号不存在！");
            return;
        }

    }
    protected void gv_det_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gv_det.EditIndex = e.NewEditIndex;
        Bindgv_BosDet();
    }
}