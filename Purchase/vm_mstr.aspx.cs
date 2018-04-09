using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Purchase_vm_mstr : BasePage
{
    Mold mold = new Mold();
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        { 
            //供应商绑定
            ddl_vend.DataSource = mold.BindVend();
            ddl_vend.DataBind();
            ddl_vend.Items.Insert(0, new ListItem("--", "0"));
            ddl_vend.SelectedIndex = 0;
           
            //模具状态
            ddl_Status.Items.Insert(0, new ListItem("--", "0"));
            ddl_Status.Items.Insert(1, new ListItem("可用", "1"));
            ddl_Status.Items.Insert(2, new ListItem("不可用", "2"));
            ddl_Status.SelectedIndex = 0;

            //Mold Details
            if(Request.QueryString["vm_id"]!=null)
            {
                mold = mold.SelectMoldByID(Request.QueryString["vm_id"]);

                ddl_vend.SelectedValue = mold.vendCode;
                ddl_Status.SelectedValue = mold.intStatus.ToString();
                txt_Cavity.Text = mold.Cavity;
                txt_Drawing.Text = mold.drawingID;
                txt_MoldID.Text = mold.moldCode;
                txt_QAD.Text = mold.QAD;
                txt_Qty.Text = mold.moldQty.ToString();
                txt_remark.Text = mold.Remark;

            }

            //新建时，不显示返回
            if (Request.QueryString["modify"] == null) btn_back.Visible = false;
            //没有权限不可修改
            else if (Request.QueryString["modify"] == "0") btn_save.Enabled = false;
        }

    }
    protected void btn_save_Click(object sender, EventArgs e)
    {
        
        if (ddl_vend.SelectedIndex == 0)
        {
            ltlAlert.Text = "alert('必须选择供应商！')";
            return;
        }
        if (txt_MoldID.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('必须填写模具编号！')";
            return;
        }
        if (txt_Qty.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('必须填写模具数量！')";
            return;
        }
        else if(txt_Qty.Text.Trim().IndexOf(".")>=0)
        {
            ltlAlert.Text = "alert('模具数量必须为整数！')";
            return;
        }

        if (ddl_Status.SelectedIndex == 0)
        {
            ltlAlert.Text = "alert('必须选择状态！')";
            return;
        }
        if (txt_QAD.Text.Trim().Length == 0 || txt_QAD.Text.Trim().Length < 14)
        {
            ltlAlert.Text = "alert('必须填写14位QAD号！')";
            return;
        }
        if (txt_Cavity.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('必须填写模具型腔！')";
            return;
        }
        if (txt_Drawing.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('必须填图纸图号！')";
            return;
        }

        string vendName = ddl_vend.SelectedItem.Text.Substring(ddl_vend.SelectedItem.Text.IndexOf("--")+2);
        string vendCode = ddl_vend.SelectedValue;
        string moldID = txt_MoldID.Text.Trim();
        int Qty;
        try
        {
            if (txt_Qty.Text.Trim().IndexOf(".")>=0)
            {
                ltlAlert.Text = "alert('模具数量必须是正整数！');";
                return ;
            }

            Qty = Convert.ToInt32(txt_Qty.Text.Trim());

            if(Qty<0)
            {
                ltlAlert.Text = "alert('模具数量必须是正整数！');";
                return ;
            }

        }
        catch
        {
            ltlAlert.Text = "alert('模具数量必须为正整数！')";
            return;
        }
        int status = Convert.ToInt32(ddl_Status.SelectedValue);
        string QAD = txt_QAD.Text.Trim();
        string Cavity = txt_Cavity.Text.Trim();
        string remark = txt_remark.Text.Trim();
        string drawID = txt_Drawing.Text.Trim();
        if (Request.QueryString["vm_id"] == null)
        {
            if (mold.AddVendMold(vendName, vendCode, moldID, Qty, status, QAD, Cavity, drawID, remark, Convert.ToInt32(Session["uID"])))
                ltlAlert.Text = "alert('保存成功！')";
            else
                ltlAlert.Text = "alert('保存失败！')";
        }
        else 
        {
            if (mold.ModifyVendMold(vendName, vendCode, moldID, Qty, status, QAD, Cavity, drawID, remark, Convert.ToInt32(Session["uID"]), Convert.ToInt32(Request.QueryString["vm_id"])))
                ltlAlert.Text = "alert('修改成功！')";
            else
                ltlAlert.Text = "alert('修改失败！')";
        }
        
    }
    protected void btn_back_Click(object sender, EventArgs e)
    {
        Response.Redirect("vm_mstrList.aspx");
    }
}