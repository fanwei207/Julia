using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class price_pc_Check_PC_mstr : BasePage
{

    PC_price pc = new PC_price();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindDDLType();
            bind();
            
        }
    }
    private void BindDDLType()
    {
        ddlType.Items.Add(new ListItem("--", "0"));
        ddlType.Items.Add(new ListItem("非大宗商品", "1"));
        SqlDataReader sdr = pc.SelectType();
        while (sdr.Read())
        {
            ddlType.Items.Add(new ListItem(sdr["pcut_type"].ToString(), sdr["pcut_type"].ToString()));
        }
    }

  

    protected void btnSelect_Click(object sender, EventArgs e)
    {
        bind();
    }

    private void bind()
    {
        string QAD = txtQAD.Text.ToString().Trim();
        string vender = txtVender.Text.ToString().Trim();
        long qadNo = 0;
        if (!long.TryParse(QAD, out qadNo) && !string.Empty.Equals(QAD))
        {
            ltlAlert.Text = "alert('QAD号必须是数字');";
            txtQAD.Focus();
            return;
        }
        if (QAD.Length > 14)
        {
            ltlAlert.Text = "alert('QAD号是14位，不可能大于14');";
            txtQAD.Focus();
            return;
        }
        if (vender.Length > 8)
        {

            ltlAlert.Text = "alert('供应商号不会超过8位');";
            txtVender.Focus();
            return;
        }
        gvInfo.DataSource = pc.select100AndQADPCMSTR(QAD, vender, txtVenderName.Text, (ckbCheck.Checked ? 1 : 0), ddlDomain.SelectedItem.Text, (chkDiff.Checked ? 1 : 0), (chkIs100.Checked ? 1 : 0),ddlType.SelectedItem.Value);
        gvInfo.DataBind();
    }

    protected void btnCheck_OnClick(object sender, EventArgs e)
    {
        Button btnCheck = (Button)sender;
        int index = ((GridViewRow)((btnCheck.NamingContainer))).RowIndex;
        string id=gvInfo.DataKeys[index].Values["pc_id"].ToString();
        if (pc.checkPricePass(id))
        {
            ltlAlert.Text = "alert('审核成功');";
            bind();
        }
        else 
        {
            ltlAlert.Text = "alert('失败成功');";
        }
        
    }

    protected void gvInfo_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        { 
          

            string price1 = e.Row.Cells[6].Text.ToString();
            string price2 = e.Row.Cells[7].Text.ToString();
            

            if (price1.Equals(price2))
            { 
                ((Label)e.Row.Cells[10].FindControl("lbDiff")).Text="无";
                e.Row.Cells[10].ForeColor = System.Drawing.Color.LightGreen;
            }
            else
            {
                ((Label)e.Row.Cells[10].FindControl("lbDiff")).Text = "有";
                ((Button)e.Row.Cells[11].FindControl("btnCheck")).Visible = false;
                e.Row.Cells[10].ForeColor = System.Drawing.Color.Red;
            }

 
            bool isCheck = (((Label)e.Row.Cells[12].FindControl("lbCheck")).Text.ToString().Equals("True") ? true : false);

            if (isCheck)
            {
                ((Button)e.Row.Cells[11].FindControl("btnCheck")).Visible = false;
                ((Label)e.Row.Cells[10].FindControl("lbDiff")).Text = "";
            }
        }
    }
   
    protected void ckbCheck_CheckedChanged(object sender, EventArgs e)
    {
        bind();
    }
    protected void chkDiff_CheckedChanged(object sender, EventArgs e)
    {
        bind();
    }
    protected void gvInfo_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        gvInfo.PageIndex = e.NewPageIndex;
        bind();
    }
    protected void chkIs100_CheckedChanged(object sender, EventArgs e)
    {
        bind();
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        string QAD = txtQAD.Text.ToString().Trim();
        string vender = txtVender.Text.ToString().Trim();
        long qadNo = 0;
        if (!long.TryParse(QAD, out qadNo) && !string.Empty.Equals(QAD))
        {
            ltlAlert.Text = "alert('QAD号必须是数字');";
            txtQAD.Focus();
            return;
        }
        if (QAD.Length > 14)
        {
            ltlAlert.Text = "alert('QAD号是14位，不可能大于14');";
            txtQAD.Focus();
            return;
        }
        if (vender.Length > 8)
        {

            ltlAlert.Text = "alert('供应商号不会超过8位');";
            txtVender.Focus();
            return;
        }
        DataTable errDt = pc.select100AndQADPCMSTR(QAD, vender, txtVenderName.Text, (ckbCheck.Checked ? 1 : 0), ddlDomain.SelectedItem.Text, (chkDiff.Checked ? 1 : 0), (chkIs100.Checked ? 1 : 0),ddlType.SelectedItem.Value);
        string title = "100^<b>100系统id</b>~^100^<b>供应商</b>~^150^<b>QAD</b>~^" + 
            "50^<b>单位</b>~^100^<b>100系统价格</b>~^150^<b>价格起始</b>~^150^<b>价格截止</b>~^" +
            "80^<b>域</b>~^80^<b>币种</b>~^50^<b>是否检验</b>~^100^<b>100系统价格</b>~^" +
            "100^<b>QAD系统价格</b>~^200^<b>供应商名称</b>~^";
        if (errDt != null && errDt.Rows.Count > 0)
        {
            ExportExcel(title, errDt, false);
        }
        
    }
}