using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text;

public partial class price_PCF_madeInquiryDet : BasePage
{

    PCF_helper helper = new PCF_helper();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lbVender.Text = Request["vender"].ToString();
            lbVenberName.Text = Request["venderName"].ToString();

            bind();
        }
    }

    private void bind()
    {

        string vender = lbVender.Text;
        string venderName = lbVenberName.Text;
        string qty = txtQty.Text.Trim();
        int qtyint = 0;

        if(!int.TryParse(qty,out qtyint))
        {

            ltlAlert.Text = "alert('限制数量必须是大于0的整数！');";
            return ;
        }
        else if(qtyint <=0)
        {
            ltlAlert.Text = "alert('限制数量必须是大于0的整数！');";
            return ;
        }

        gvNotInquiryList.DataSource = helper.selectMadeInquiryDet(vender, venderName, qty);
        gvNotInquiryList.DataBind();

    }
    protected void txtQty_TextChanged(object sender, EventArgs e)
    {
        bind();
    }

    protected void btnReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("PCF_MadeInquiryList.aspx");
    }




    protected void btnAddIM_Click(object sender, EventArgs e)
    {

        string vender = lbVender.Text;
        string venderName = lbVenberName.Text;

        DataTable TempTable = new DataTable("gvTable");
        DataColumn TempColumn;
        DataRow TempRow;


        TempColumn = new DataColumn();
        TempColumn.DataType = System.Type.GetType("System.String");
        TempColumn.ColumnName = "PCF_ID";
        TempTable.Columns.Add(TempColumn);


        string PCF_inquiryID = string.Empty;
        for (int i = 0; i < gvNotInquiryList.Rows.Count; i++)
        {
            if (((CheckBox)(gvNotInquiryList.Rows[i].Cells[0].FindControl("chk"))).Checked)
            {
                TempRow = TempTable.NewRow();
                TempRow["PCF_ID"] = gvNotInquiryList.DataKeys[i].Values["PCF_ID"].ToString();

                TempTable.Rows.Add(TempRow);
            }
        }
        if (TempTable.Rows.Count <=0)
        {
            ltlAlert.Text = "alert('您未选中生成询价单的项目');";
            return;
        }

        if (helper.insertTOInquiry(TempTable, Session["uID"].ToString(), Session["uName"].ToString(), vender, venderName, out PCF_inquiryID))
        {
            ltlAlert.Text = "alert('生成询价单成功');";
            //转跳到报价页面
            Response.Redirect("pcf_InquiryDet.aspx?PCF_inquiryID=" + PCF_inquiryID);
        }
        else
        {
            ltlAlert.Text = "alert('生成询价单失败，请联系管理员');";
        }
        bind();

    }
    protected void gvNotInquiryList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if(e.CommandName == "lkbDelete")
        {
            string PCF_ID = e.CommandArgument.ToString();

            //标示位 0,数据库操作失败 1，删除成功 2，其他
            int flag =  helper.deletePCFDetByPCFID(PCF_ID,Session["uID"].ToString(),Session["uName"].ToString());


            if (flag == 0)
            {
                ltlAlert.Text = "alert('删除失败，请联系管理员！');";
                return;
            }
            else if (flag == 1)
            {
                ltlAlert.Text = "alert('删除成功！');";
                bind();
            }

        }
        if (e.CommandName == "lkbApply")
        { 
            string PCF_sourceID = e.CommandArgument.ToString();
            string id = helper.selectApplyMstrID(PCF_sourceID);

            Response.Redirect("../Purchase/rp_purchaseMstrDetial.aspx?ID=" + id + "&vender=" + Request["vender"].ToString() + "&venderName=" + Request["venderName"].ToString());
        }
    }
    protected void btnSelect_Click(object sender, EventArgs e)
    {
        bind();
    }
}