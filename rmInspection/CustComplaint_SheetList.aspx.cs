using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;

public partial class EDI_CustComplaint_SheetList : BasePage
{
    adamClass adam = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Bind();
        }
    }
    private void Bind()
    {
        DataTable dt = getCustCompMstr();
        gv.DataSource = dt;
        gv.DataBind();
    }
    private DataTable getCustCompMstr()
    {
        string str = "sp_CustComp_SelectCustCompMstr";
        SqlParameter[] param = new SqlParameter[10];
        param[0] = new SqlParameter("@no", txtNo.Text);
        param[1] = new SqlParameter("@cust", txtCust.Text);
        param[2] = new SqlParameter("@order", txtOrder.Text);
        param[3] = new SqlParameter("@createdate", txtCreateDate.Text);
        param[4] = new SqlParameter("@status", ddlSatus.SelectedValue.ToString());

        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, str, param).Tables[0];
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Bind();
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("../rmInspection/CustComplaint_NewSheet.aspx?type=new");
    }
    protected void gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //定义参数
        string strID = gv.DataKeys[e.RowIndex].Values["ID"].ToString();
        SqlParameter[] sqlParam = new SqlParameter[1];
        sqlParam[0] = new SqlParameter("@ID", strID);
        SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, "sp_CustComp_DeleteCustCompMstr", sqlParam);

        Bind();
    }
    protected void gv_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //定义参数
        if (e.CommandName.ToString() == "Detial")
        {
            int intRow = Convert.ToInt32(e.CommandArgument.ToString());

            Response.Redirect("../rmInspection/CustComplaint_SheetDetial.aspx?ID=" + gv.DataKeys[intRow].Values["ID"].ToString().Trim()// + 
                                                                //"&No=" + gv.DataKeys[intRow].Values["CustComp_No"].ToString().Trim() +                                                                     
                                                                //"&Customer=" + gv.DataKeys[intRow].Values["CustComp_Customer"].ToString().Trim() + 
                                                                //"&Order=" + gv.DataKeys[intRow].Values["CustComp_OrderID"].ToString().Trim() + 
                                                                //"&Describe=" + gv.DataKeys[intRow].Values["CustComp_Describe"].ToString().Trim() + 
                                                                //"&Finance=" + gv.DataKeys[intRow].Values["CustComp_Finance"].ToString().Trim() + 
                                                                //"&FinanceBy=" + gv.DataKeys[intRow].Values["CustComp_FinanceBy"].ToString().Trim() + 
                                                                //"&FinanceName=" + gv.DataKeys[intRow].Values["CustComp_FinanceName"].ToString().Trim() + 
                                                                //"&AfterSaleService=" + gv.DataKeys[intRow].Values["CustComp_AfterSaleService"].ToString().Trim() + 
                                                                     
                                                                //"&AfterSaleServiceBy=" + gv.DataKeys[intRow].Values["CustComp_AfterSaleServiceBy"].ToString().Trim() + 
                                                                //"&AfterSaleServiceName=" + gv.DataKeys[intRow].Values["CustComp_AfterSaleServiceName"].ToString().Trim() + 
                                                                //"&Factory=" + gv.DataKeys[intRow].Values["CustComp_Factory"].ToString().Trim() + 
                                                                //"&ResponsiblePerson=" + gv.DataKeys[intRow].Values["CustComp_ResponsiblePerson"].ToString().Trim() + 
                                                                //"&Payment=" + gv.DataKeys[intRow].Values["CustComp_Payment"].ToString().Trim() + 
                                                                //"&Staus=" + gv.DataKeys[intRow].Values["CustComp_Staus"].ToString().Trim() +
                                                                //"&DetModeifyBy=" + gv.DataKeys[intRow].Values["CustComp_DetModeifyBy"].ToString().Trim() + 
                                                                //"&DetModeifyName=" + gv.DataKeys[intRow].Values["CustComp_DetModeifyName"].ToString().Trim() + 
                                                                //"&createBy=" + gv.DataKeys[intRow].Values["createBy"].ToString().Trim() +
                                                                //"&createName=" + gv.DataKeys[intRow].Values["createName"].ToString().Trim() +
                                                                //"&DeptStatus=" + gv.DataKeys[intRow].Values["CustComp_DeptStatus"].ToString().Trim() +
                                                                //"&DeptStatusBy=" + gv.DataKeys[intRow].Values["CustComp_DeptStatusBy"].ToString().Trim() +
                                                                //"&DeptStatusName=" + gv.DataKeys[intRow].Values["CustComp_DeptStatusName"].ToString().Trim() +
                                                                //"&ReqDate=" + gv.DataKeys[intRow].Values["CustComp_ReqDate"].ToString().Trim() +
                                                                //"&DueDate=" + gv.DataKeys[intRow].Values["CustComp_DueDate"].ToString().Trim()
                                                                     );
        }
        if (e.CommandName == "ViewEdit")
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
            string _no = gv.DataKeys[index].Values["CustComp_No"].ToString();
            /*
            string _leaderIsAgree = gv.DataKeys[index].Values["supplier_LeaderIsAgree"].ToString();
            //有签名则不能编辑
            if (_leaderIsAgree != "0")
            {
                //ltlAlert.Text = "alert('主管已签字不能修改！');";
                //return;
                Response.Redirect("supplier_newApply1.aspx?type=edit&no=" + _no);
            }
            else
            {
                Response.Redirect("supplier_newApply1.aspx?type=edit&no=" + _no);
            }*/
            Response.Redirect("../rmInspection/CustComplaint_NewSheet.aspx?type=edit&no=" + _no +
                                                                "&cust=" + gv.DataKeys[index].Values["CustComp_Customer"].ToString() +
                                                                "&order=" + gv.DataKeys[index].Values["CustComp_OrderID"].ToString() +
                                                                "&id=" + gv.DataKeys[index].Values["ID"].ToString() +
                                                                "&idtype=" + gv.DataKeys[index].Values["CustComp_IDType"].ToString() +
                                                                "&datecode=" + gv.DataKeys[index].Values["CustComp_DateCode"].ToString() +
                                                                "&duedate=" + gv.DataKeys[index].Values["CustComp_DueDate"].ToString() +
                                                                "&problemContent=" + gv.DataKeys[index].Values["CustComp_Describe"].ToString().Replace("\r\n",""));
        }
    }
    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (Convert.ToInt32(gv.DataKeys[e.Row.RowIndex].Values["CustComp_DeptStatus"]) != 0)
            {
                //e.Row.Cells[0].BackColor = System.Drawing.Color.Red;
                //e.Row.Cells[9].BackColor = System.Drawing.Color.Red;
                e.Row.Cells[0].Enabled = false;
                //e.Row.Cells[9].Enabled = false;
            }
        }
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        DataTable errDt = getCustCompMstrExport();//输出错误信息
        string title = "100^<b>投诉单号</b>~^100^<b>客户</b>~^100^<b>原订单</b>~^100^<b>Date Code</b>~^100^<b>Due Date</b>~^100^<b>问题描述</b>~^100^<b>赔付总计($)</b>~^100^<b>创建人</b>~^100^<b>创建日期</b>~^";
        if (errDt != null && errDt.Rows.Count > 0)
        {
            ExportExcel(title, errDt, false);
        }
    }

    private DataTable getCustCompMstrExport()
    {
        string str = "sp_CustComp_SelectCustCompMstrExport";
        SqlParameter[] param = new SqlParameter[10];
        param[0] = new SqlParameter("@no", txtNo.Text);
        param[1] = new SqlParameter("@cust", txtCust.Text);
        param[2] = new SqlParameter("@order", txtOrder.Text);
        param[3] = new SqlParameter("@createdate", txtCreateDate.Text);
        param[4] = new SqlParameter("@status", ddlSatus.SelectedValue.ToString());

        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, str, param).Tables[0];
    }
}