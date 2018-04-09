using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Net.Mail;
using System.Configuration;
using SampleManagement;

public partial class supplier_SampleNotesMaintain : BasePage
{
    Sample sap = new Sample();
    Int32 UserId;
    protected override void OnInit(EventArgs e)
    {
        if (!IsPostBack)
        {
            this.Security.Register("320120", "打样单新增权限");
        }

        base.OnInit(e);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (String.IsNullOrEmpty(Request.QueryString["bos_nbr"]) || Request.QueryString["bos_nbr"].ToString() == "0")
            {

                //Response.Redirect("/Supplier/SampleNotesMaintain.aspx?mid=" + Convert.ToString(Request.QueryString["mid"]) + "&did=" + Convert.ToString(Request.QueryString["did"]) + "&fr=" + Convert.ToString(Request.QueryString["fr"]) + "&@__pn=" + Request.QueryString["@__pn"] + "&@__pc=" + Request.QueryString["@__pc"] + "&@__sd=" + Request.QueryString["@__sd"] + "&@__st=" + Request.QueryString["@__st"] + "&@__sk=" + Request.QueryString["@__sk"] + "&@__pg=" + Request.QueryString["@__pg"] + "&rm=" + DateTime.Now.ToString(), true);
                string strBosnbrReturn;
                if (!String.IsNullOrEmpty(Request.QueryString["did"]))  // 从项目管理进入建立打样单
                {
                    int RDWDid = Convert.ToInt32(Request.QueryString["did"].ToString());
                    strBosnbrReturn = sap.addBosNbr("", "nothing", "获取打样单", "", Convert.ToInt32(Session["uID"]), RDWDid);
                }
                else
                {
                    strBosnbrReturn = sap.addBosNbr("", "nothing", "获取打样单", "", Convert.ToInt32(Session["uID"]));
                }
                txt_bosnbr.Text = strBosnbrReturn;

                BindVend();
                btn_Add.Visible = true;
                trCompleteDate.Visible = false;
                btn_Save.Visible = false;
                tb_bosdet.Visible = false;
                txt_bosDate.Text = System.DateTime.Now.ToString("yyyy-MM-dd HH:MM:ss");
                //txt_bosdetreqDate.Text = System.DateTime.Now.AddDays(7).ToString("yyyy-MM-dd");

                tr_Receipt.Visible = false;
                tr_Vend.Visible = false;
            }
            else
            {

                if (!this.Security["320120"].isValid)
                {
                    tr_bosdet.Visible = false;
                    btn_Save.Visible = false;
                    btn_Delete.Visible = false;
                }
                else
                {
                    btn_Save.Visible = true;
                }

                btn_Add.Visible = false;
                trCompleteDate.Visible = true;
                BindVend();
                txt_bosnbr.Text = Request.QueryString["bos_nbr"].ToString();
                ddl_vend.Enabled = false;
                BindBosData();
                Bindgv_BosDet();


            }
            lblAddTip.Visible = btn_Add.Visible;
        }

    }

    private void BindBosData()
    {
        string strBosnbr = txt_bosnbr.Text.Trim().ToString();
        DataTable dt = sap.getBosMstr(strBosnbr, "0");

        ddl_vend.SelectedValue = dt.Rows[0]["bos_vend"].ToString();
        txtCompleteDate.Text = dt.Rows[0]["bos_completeDate"].ToString();

        txt_bosDate.Text = String.Format("{0:yyyy-MM-dd HH:MM:ss}", Convert.ToDateTime(dt.Rows[0]["bos_createddate"].ToString()));
        txt_bosDate.Enabled = false;
        txtRmks.Text = dt.Rows[0]["bos_rmks"].ToString();
        chk_isVendConfirm.Checked = Convert.ToBoolean(dt.Rows[0]["bos_vendIsConfirm"].ToString());
        chk_isReciept.Checked = Convert.ToBoolean(dt.Rows[0]["bos_receiptIsConfirm"].ToString());
        //btn_emailToVend.Visible = ! Convert.ToBoolean(dt.Rows[0]["bos_isSendEmail"].ToString());
        lblState.Visible = Convert.ToBoolean(dt.Rows[0]["bos_isCanceled"].ToString());
        if (chk_isVendConfirm.Checked || chk_isReciept.Checked)
        {
            btn_Save.Enabled = false;
            btn_Delete.Enabled = false;
            tr_bosdet.Visible = false;
            if (chk_isVendConfirm.Checked)
            {
                lbl_VendNote.Text = "确认日期:" + String.Format("{0:yyyy-MM-dd HH:MM:ss}", Convert.ToDateTime(dt.Rows[0]["bos_vendConfirmDate"].ToString())) + ",留言:" + dt.Rows[0]["bos_vendMessage"].ToString();
            }
            if (chk_isReciept.Checked)
            {
			 btn_Cancel.Visible = false;
                lbl_RecpitpNote.Text = "收货日期:" + String.Format("{0:yyyy-MM-dd HH:MM:ss}", Convert.ToDateTime(dt.Rows[0]["bos_receiptDate"].ToString())) + ",收货说明:" + dt.Rows[0]["Bos_receiptRmks"].ToString();
            }
        }
        if (lblState.Visible)
        {
            btn_Save.Enabled = false;
            btn_Delete.Enabled = false;
            btn_Cancel.Visible = false;
        } 

    }

    private void Bindgv_BosDet()
    {
        string strBosnbr = txt_bosnbr.Text.ToString();

        DataTable dt = sap.getBosDet(strBosnbr);
        gv_det.DataSource = dt;
        if (chk_isVendConfirm.Checked || chk_isReciept.Checked)
        {
            gv_det.Columns[7].Visible = false;
            gv_det.Columns[8].Visible = false;
        }
        gv_det.DataBind();
    }

    protected void BindVend()
    {
        ddl_vend.DataSource = sap.getBosSuppliers((SysRole)Enum.Parse(typeof(SysRole), "Supplier", true));//ddlUserType.SelectedValue

        ddl_vend.DataBind();
        ddl_vend.Items.Insert(0, new ListItem("--", "0"));

    }
    protected void btn_Add_Click(object sender, EventArgs e)
    {
        if (ddl_vend.SelectedValue == "0")
        {
            this.Alert("请选择供应商");
            return;
        }
        string strBosnbr = txt_bosnbr.Text.Trim().ToString();
        string strVend = ddl_vend.SelectedValue.ToString();
        string strVendName = ddl_vend.SelectedItem.Text.Trim().Substring(ddl_vend.SelectedItem.Text.IndexOf(')') + 1);
        string strRmks = txtRmks.Text.Trim().ToString();
        UserId = Convert.ToInt32(Session["uID"]);
        string strBosnbrReturn = sap.addBosNbr(strBosnbr, strVend, strVendName, strRmks, UserId);
        if (strBosnbrReturn != "0")
        {

            txt_bosnbr.Text = strBosnbrReturn;
            txt_bosDate.Text = DateTime.Now.ToString();
            tb_bosdet.Visible = true;
            tr_bosdet.Visible = true;
            Bindgv_BosDet();
            btn_Add.Visible = false;
            trCompleteDate.Visible = true;
            btn_Save.Visible = true;
            lblAddTip.Visible = btn_Add.Visible;
            //btn_Reset.Visible = true;
            //this.Alert("打样单" + strBosnbrReturn + "已添加成功');";
            //return;
        }
        else
        {
            txt_bosnbr.Text = "";
            Bindgv_BosDet();
            this.Alert("打样单" + strBosnbr + "已添加失败");
            return;

        }
    }
    protected void btn_Back_Click(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(Request.QueryString["did"]))
        {
            Response.Redirect("SampleNotesLists.aspx?mid=" + Convert.ToString(Request.QueryString["mid"]) + "&did=" + Convert.ToString(Request.QueryString["did"]) + "&fr=" + Convert.ToString(Request.QueryString["fr"]) + "&@__pn=" + Request.QueryString["@__pn"] + "&@__pc=" + Request.QueryString["@__pc"] + "&@__sd=" + Request.QueryString["@__sd"] + "&@__st=" + Request.QueryString["@__st"] + "&@__sk=" + Request.QueryString["@__sk"] + "&@__pg=" + Request.QueryString["@__pg"] + "&rm=" + DateTime.Now.ToString(), true);
        }
        else
        {
            Response.Redirect("SampleNotesLists.aspx?bos_nbr=" + txt_bosnbr.Text);
        }
    }

    protected void btn_Save_Click(object sender, EventArgs e)
    {
        if (ddl_vend.SelectedValue == "0")
        {
            this.Alert("请选择供应商");
            return;
        }
        string strBosnbr = txt_bosnbr.Text.Trim().ToString();
        string strVend = ddl_vend.SelectedValue.ToString();
        string strVendName = ddl_vend.SelectedItem.Text.Trim().Substring(ddl_vend.SelectedItem.Text.IndexOf(')') + 1);
        string strRmks = txtRmks.Text.Trim().ToString();

        if (sap.updateBosNbr(strBosnbr, strVend, strVendName, strRmks))
        {
            this.Alert("样品单据号" + strBosnbr + "已修改成功");
            return;
        }
        else
        {
            this.Alert("样品单据号" + strBosnbr + "修改失败");
            return;

        }

    }
    protected void btnSaveDet_Click(object sender, EventArgs e)
    {
        string strbosnbr = txt_bosnbr.Text.ToString();
        int ibosdetline;
        try
        {
            if (txt_bosdetLine.Text.ToString().Trim() == string.Empty)
            {
                ibosdetline = 0;

            }
            else
            {
                ibosdetline = Convert.ToInt16(txt_bosdetLine.Text.Trim().ToString());
            }
        }
        catch
        {
            this.Alert("物料订单行必须是整数");
            return;
        }
        string strbosdetCode = txt_bosdetCode.Text.ToString();
        //if (strbosdetCode.ToString() == string.Empty)
        //{
        //    this.Alert("部件号不能为空，请填入;若还没有部件号,请填样品的描述,并在备注说明');";
        //    return; 
        //}
        string strbosdetQad = txt_bosdetQAD.Text.ToString();
        float strbosdetQty;
        try
        {
            if (txt_bosdetQty.Text.ToString() == string.Empty)
            {
                strbosdetQty = 1;
            }
            else
            {
                strbosdetQty = Convert.ToSingle(txt_bosdetQty.Text.Trim().ToString());
            }
        }
        catch
        {
            this.Alert("样品物料的数量必须是数字");
            return;
        }
        string strbosdetRmks = txt_bosdetRmks.Text.ToString();
        if (txt_bosdetCode.Text.ToString().Trim() != string.Empty)
        {
            string strCode = txt_bosdetCode.Text.Trim().ToString();
            string strQAD = sap.ConfirmExistsCode(strCode);
            if (strQAD == "0")
            {
                this.Alert("此部件号不存在");
                return;
            }
            else
            {
                if (strQAD == "stop")
                {
                    this.Alert("此部件号" + strCode + "的已停用");
                    return;
                }
            }
        }

        string strbosdetReqDate = txt_bosdetreqDate.Text.ToString();
        try
        {
            if (txt_bosdetreqDate.Text.ToString() == string.Empty)
            {
                this.Alert("请填写物料的需求日期");
                return;
            }
            else
            {
                DateTime reqdate = Convert.ToDateTime(txt_bosdetreqDate.Text.Trim().ToString());
            }
        }
        catch
        {
            this.Alert("物料的需求日期格式不对");
            return;
        }

        if (sap.addbosDet(strbosnbr, ibosdetline, strbosdetCode, strbosdetQad, strbosdetQty, strbosdetRmks, strbosdetReqDate))
        {
            Bindgv_BosDet();

            txt_bosdetCode.Text = "";
            txt_bosdetLine.Text = "";
            txt_bosdetQAD.Text = "";
            txt_bosdetQty.Text = "";
            txt_bosdetRmks.Text = "";
            txt_bosdetreqDate.Text = "";
            //this.Alert("打样单号" + strbosnbr + "已添加物料成功');";
            //return;
        }
        else
        {
            this.Alert("打样单号" + strbosnbr + "已添加物料行失败，行号需唯一");
            return;
        }

    }

    protected void txt_bosdetCode_TextChanged(object sender, EventArgs e)
    {
        btnSaveDet.Enabled = true;
        if (txt_bosdetCode.Text != string.Empty)
        {
            string strCode = txt_bosdetCode.Text.Trim().ToString();
            string strQAD = sap.ConfirmExistsCode(strCode);
            if (strQAD == "0")
            {
                txt_bosdetQAD.Text = "";
                btnSaveDet.Enabled = false;
                this.Alert("此部件号不存在！");
                return;
            }
            else
            {
                btnSaveDet.Enabled = true;
                txt_bosdetQAD.Text = strQAD;
                if (strQAD == "not have QAD")
                {
                    txt_bosdetQAD.Text = ""; ;
                }
                else
                {
                    if (strQAD == "stop")
                    {
                        txt_bosdetQAD.Text = "";
                        btnSaveDet.Enabled = false;
                        this.Alert("此部件号" + strCode + "的已停用");
                        return;
                    }
                }
            }
            Bindgv_BosDet();
        }

    }

    protected void btn_detCancel_Click(object sender, EventArgs e)
    {
        txt_bosdetCode.Text = "";
        txt_bosdetLine.Text = "";
        txt_bosdetQAD.Text = "";
        txt_bosdetQty.Text = "";
        txt_bosdetRmks.Text = "";

        btnSaveDet.Enabled = true;
        //Bindgv_BosDet();

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
            ((LinkButton)e.Row.Cells[7].Controls[0]).Visible = tr_bosdet.Visible;
            if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
            {
                ((LinkButton)e.Row.Cells[8].Controls[0]).Visible = tr_bosdet.Visible;
                ((LinkButton)e.Row.Cells[8].Controls[0]).Style.Add("font-weight", "normal");
                if (tr_bosdet.Visible == true)
                {
                    e.Row.Cells[8].Attributes.Add("onclick", "return confirm('你确认要要删除这行吗?')");
                }
            }
        }

    }
    protected void gv_det_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string strBosnbr = txt_bosnbr.Text.ToString();
        string strBosdetLine = gv_det.Rows[e.RowIndex].Cells[0].Text.ToString();
        if (sap.deleteBosDet(strBosnbr, strBosdetLine))
        {
            Bindgv_BosDet();
            this.Alert("打样单号" + strBosnbr + "行号" + strBosdetLine + "已删除");
            return;
        }
        else
        {
            this.Alert("打样单号" + strBosnbr + "行号" + strBosdetLine + "删除失败");
            return;
        }
    }
    protected void gv_det_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gv_det.EditIndex = -1;
        Bindgv_BosDet();
    }
    protected void gv_det_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gv_det.EditIndex = e.NewEditIndex;
        Bindgv_BosDet();
    }
    protected void gv_det_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string strbosnbr = txt_bosnbr.Text.ToString();
        int bosLine = Convert.ToInt32(gv_det.Rows[e.RowIndex].Cells[0].Text.ToString());
        TextBox txtbosdetQty = (TextBox)gv_det.Rows[e.RowIndex].FindControl("txt_gvdetqty");
        float fbosdetQty;
        try
        {
            if (txtbosdetQty.Text.ToString() == string.Empty)
            {
                fbosdetQty = 1;
            }
            else
            {
                fbosdetQty = Convert.ToSingle(txtbosdetQty.Text.Trim().ToString());
            }
        }
        catch
        {
            this.Alert("样品物料的数量必须是数字");
            return;
        }

        string strbosdetDoc = ""; //((TextBox)gv_det.Rows[e.RowIndex].FindControl("txt_gvdetDoc")).Text;
        string strbosdetrmks = ((TextBox)gv_det.Rows[e.RowIndex].FindControl("txt_gvdetrmks")).Text;
        TextBox txtbosdetRequiredDate = (TextBox)gv_det.Rows[e.RowIndex].FindControl("txt_gvdetrequireDate");
        try
        {
            if (txtbosdetRequiredDate.Text.ToString() == string.Empty)
            {
                this.Alert("请填写物料的需求日期");
                return;
            }
            else
            {
                DateTime reqdate = Convert.ToDateTime(txtbosdetRequiredDate.Text.Trim().ToString());
            }
        }
        catch
        {
            this.Alert("物料的需求日期格式不对");
            return;
        }
        string strbosdetRequiredDate = ((TextBox)gv_det.Rows[e.RowIndex].FindControl("txt_gvdetrequireDate")).Text;
        string strbosdetQty = fbosdetQty.ToString();
        if (sap.updateBosdet(strbosnbr, bosLine, strbosdetQty, strbosdetDoc, strbosdetrmks, strbosdetRequiredDate))
        {
            gv_det.EditIndex = -1;
            Bindgv_BosDet();
        }
        else
        {
            this.Alert("更新失败");
            return;
        }
    }
    protected void gv_det_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.ToString() == "EditDoc")
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            string strBosNbr = txt_bosnbr.Text.ToString();
            string strBosdetline = gv_det.Rows[index].Cells[0].Text.ToString();
            string strBosdetCode = Server.UrlEncode(gv_det.Rows[index].Cells[1].Text.ToString()).ToString();
            string strBosdetQad = gv_det.Rows[index].Cells[2].Text.ToString();
            if (strBosdetQad == string.Empty) strBosdetQad = string.Empty;
            if (!String.IsNullOrEmpty(Request.QueryString["did"]))
            {
                Response.Redirect("SampleNotesAccDoc.aspx?Mode=Maintain&strNbr=" + strBosNbr + "&line=" + strBosdetline + "&code=" + Server.UrlEncode(strBosdetCode).ToString() + "&qad=" + strBosdetQad +"&mid=" + Convert.ToString(Request.QueryString["mid"]) + "&did=" + Convert.ToString(Request.QueryString["did"]) + "&fr=" + Convert.ToString(Request.QueryString["fr"]) + "&@__pn=" + Request.QueryString["@__pn"] + "&@__pc=" + Request.QueryString["@__pc"] + "&@__sd=" + Request.QueryString["@__sd"] + "&@__st=" + Request.QueryString["@__st"] + "&@__sk=" + Request.QueryString["@__sk"] + "&@__pg=" + Request.QueryString["@__pg"] + "&rm=" + DateTime.Now.ToString(), true);
            }
            else
            {
                Response.Redirect("SampleNotesAccDoc.aspx?Mode=Maintain&strNbr=" + strBosNbr + "&line=" + strBosdetline + "&code=" + Server.UrlEncode(strBosdetCode).ToString() + "&qad=" + strBosdetQad);

            }
        }

    }

    protected void btn_Delete_Click(object sender, EventArgs e)
    {
        if (txt_bosnbr.Text.ToString() == string.Empty)
        {
            this.Alert("打样单为空,没有要删除的打样单号");
            return;
        }
        else
        {
            string strBosnbr = txt_bosnbr.Text.ToString();
            if (sap.deleteBos(strBosnbr))
            {
                Bindgv_BosDet();
                txt_bosnbr.Text = "";
                ddl_vend.SelectedIndex = -1;
                txtRmks.Text = "";
                tb_bosdet.Visible = false;
                btn_Save.Visible = false;
                //btn_Reset.Visible = true;
                btn_Delete.Visible = false;
                this.Alert("样品单据号" + strBosnbr + "已删除");
                Response.Redirect("SampleNotesLists.aspx");
                return;

            }
            else
            {
                this.Alert("样品单据号" + strBosnbr + "删除失败，请先删除其详细物料行");
                return;
            }
        }
    }
    protected void btn_Reset_Click(object sender, EventArgs e)
    {
        Response.Redirect("SampleNotesMaintain.aspx");
    }
    protected void btn_Cancel_Click(object sender, EventArgs e)
    {
        string strBosnbr = txt_bosnbr.Text.ToString();

        if (sap.updateBosNbrToCancel(strBosnbr))
        {
            BindBosData();
            this.Alert("样品单据号" + strBosnbr + "已取消");
            return;
        }
        else
        {
            BindBosData();
            this.Alert("样品单据号" + strBosnbr + "取消失败");
            return;

        }

    }
}