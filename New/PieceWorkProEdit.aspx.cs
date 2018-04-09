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
using TCPNEW;


public partial class PieceWorkProEdit : BasePage
{
    adamClass adam = new adamClass(); 

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) 
        { 
            this.dropWorkKinds.DataSource = ProgressDataTcp.GetWorkKinds();
            this.dropWorkKinds.DataBind();
            this.dropWorkKinds.Items.Insert(0, new ListItem("--", "0"));

            this.dropType.DataSource = ProgressDataTcp.GetSystemCode();
            this.dropType.DataBind();
            this.dropType.Items.Insert(0, new ListItem("--", "0"));

            if (this.Request.QueryString["id"] == null)//When Adding a new record 新建模式
            {
                this.btnModify.Visible = false;
            }
            else //When Modifying a record 修改模式
            {
                this.btnSave.Visible = false;

                string strName = "select * from tcpc1.dbo.PieceWorkPro where id=" + this.Request.QueryString["id"].ToString();

                DataTable dtPieceWorkPro = null;
                dtPieceWorkPro = ProgressDataTcp.GetPieceWorkPro(strName);
                if (dtPieceWorkPro != null && dtPieceWorkPro.Rows.Count > 0) 
                {
                    this.dropType.Items.FindByValue(dtPieceWorkPro.Rows[0][1].ToString()).Selected = true;
                    this.dropWorkKinds.Items.FindByValue(dtPieceWorkPro.Rows[0][2].ToString()).Selected = true;
                    this.txtPrice.Text = dtPieceWorkPro.Rows[0][3].ToString();
                    this.txtCoeff.Text = dtPieceWorkPro.Rows[0][4].ToString();
                    this.txtSdprice.Text = dtPieceWorkPro.Rows[0][5].ToString();
                    this.txtDate.Text = dtPieceWorkPro.Rows[0][7].ToString();
                    this.txtComment.Value = dtPieceWorkPro.Rows[0][6].ToString();
                }

            }
        }

        this.btnSave.Attributes.Add("onclick" , this.GetPostBackEventReference(this.btnSave) + ";this.disabled=true");
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        this.Page.Response.Redirect("PieceWorkPro.aspx");
    }
    protected void btnModify_Click(object sender, EventArgs e)
    {
        //when dropType or dropWorkShop select the first item,then return 系统要求“类型”和“组别”必须有选择

        bool blnFlag = true;
        if (this.dropType.SelectedIndex == 0)
        {
            this.lblType.Visible = true;
            blnFlag = false;
        }
        else 
        {
            this.lblType.Visible = false;
        }
        if (this.dropWorkKinds.SelectedIndex == 0)
        {
            this.lblWorkKinds.Visible = true;
            blnFlag = false;
        }
        else
        {
            this.lblWorkKinds.Visible = false;
        }

        if (!blnFlag)//when blnFlag equals "false" then renturn 一旦blnFlag的值为false就返回
        {
            return;
        }

        //excute db here 以下是有关数据库的操作
        int result;

        result = ProgressDataTcp.ModifyPieceWorkPro(this.Request.QueryString["id"].ToString(),
                                            this.dropType.SelectedValue,
                                            this.dropWorkKinds.SelectedValue,
                                            this.txtPrice.Text,
                                            this.txtCoeff.Text,
                                            this.txtSdprice.Text,
                                            this.txtComment.Value,
                                            this.txtDate.Text,
                                            Session["uID"].ToString());
        if (result > 0)
        {
            ltlAlert.Text = "alert('修改成功!');"; 
            this.Page.Response.Redirect("PieceWorkPro.aspx");
        }
        else
        {
            ltlAlert.Text = "alert('修改失败!');";
            return;
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        //when dropType or dropWorkShop select the first item,then return 系统要求“类型”和“组别”必须有选择
        bool blnFlag = true;

        if (this.dropType.SelectedIndex == 0)
        {
            this.lblType.Visible = true;
            blnFlag = false;
        }
        else
        {
            this.lblType.Visible = false;
        }
        if (this.dropWorkKinds.SelectedIndex == 0)
        {
            this.lblWorkKinds.Visible = true;
            blnFlag = false;
        }
        else
        {
            this.lblWorkKinds.Visible = false;
        }

        if (!blnFlag)//when blnFlag equals "false" then renturn 一旦blnFlag的值为false就返回
        {
            return;
        }

        //excute db here 以下是有关数据库的操作
        int result;
        result = ProgressDataTcp.SavePieceWorkPro(  this.dropType.SelectedValue,
                                                    this.dropWorkKinds.SelectedValue,
                                                    this.txtPrice.Text,
                                                    this.txtCoeff.Text,
                                                    this.txtSdprice.Text,
                                                    this.txtComment.Value,
                                                    this.txtDate.Text,
                                                    Session["uID"].ToString());
        if (result > 0)
        { 
            this.dropType.SelectedIndex = 0;
            this.dropWorkKinds.SelectedIndex = 0;
            this.txtPrice.Text = "1";
            this.txtCoeff.Text = "1";
            this.txtSdprice.Text = "1";
            this.txtComment.Value = "";
            this.txtDate.Text = "";
            ltlAlert.Text = "alert('保存成功!');";
            return;
        }
        else 
        {
            ltlAlert.Text = "alert('保存失败!');";
            return;
             
        }
    }
}
