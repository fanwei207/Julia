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
using System.IO;
using System.Net;
using CommClass;
using System.Text.RegularExpressions;
using Stockingbase;

public partial class plan_Stocking_delay : BasePage
{

    adamClass adam = new adamClass();
    Stocking sk = new Stocking();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
        }
    }
   
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        BindData();
    }
    protected void BindData()
    {
        //定义参数
        string strnbr = txtNbr.Text.Trim().Replace("*", "%");
        string strvent = txtvent.Text.Trim();
        string strpart = txtpart.Text.Trim().Replace("*", "%");
        string strQAD = txtQAD.Text.Trim();



        gvSID.DataSource = sk.SelectStockingdelay(strnbr, strvent, strpart, strQAD, ddlstatus.SelectedValue);
        gvSID.DataBind();
    }

    protected string chkSelect()
    {
        //定义参数
        string strSelect = "";

        //判断是否有选择
        for (int i = 0; i < gvSID.Rows.Count; i++)
        {
            CheckBox cb = (CheckBox)gvSID.Rows[i].FindControl("chk_Select");
            if (cb.Checked)
            {
                strSelect = strSelect + gvSID.DataKeys[i].Value.ToString() + ",";
            }
        }
        return strSelect;
    }


    protected void gvSID_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvSID.EditIndex = e.NewEditIndex;
        BindData();
    }
    protected void gvSID_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvSID.EditIndex = -1;
        BindData();
    }
    protected void gvSID_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        String id = gvSID.DataKeys[e.RowIndex].Values["sk_nbr"].ToString();
        TextBox txtponbr = (TextBox)gvSID.Rows[e.RowIndex].FindControl("txtponbr");
        TextBox txtpolinr = (TextBox)gvSID.Rows[e.RowIndex].FindControl("txtpolinr");

        if (txtponbr.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('订单号不能为空!');";
            return;
        }
        else if (txtpolinr.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('订单行不能为空!');";
            return;
        }
        sk.updatestockingorder(id, txtponbr.Text, txtpolinr.Text);
        gvSID.EditIndex = -1;
        BindData();
    }

  
  
  
  

    protected void gvSID_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvSID.PageIndex = e.NewPageIndex;
        BindData();
    }
}