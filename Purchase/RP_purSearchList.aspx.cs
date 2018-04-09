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
using System.Configuration;

public partial class Purchase_RP_purSearchList : BasePage
{
    adamClass adam = new adamClass();
    String strConn = ConfigurationSettings.AppSettings["SqlConn.BarCodeSys"];
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ddlPlant.SelectedValue = Session["plantCode"].ToString();
            //BindGridView();
            BindBusDept();
            BindDept();
        }
    }
    protected void txtNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Purchase/rp_purchaseDet.aspx?type=new");
    }
    private void BindDept()
    {
        ddlDept.Items.Clear();
        ddlDept.Items.Add(new ListItem("-请选择部门-", "0"));
        try
        {
            string str = Convert.ToString(Session["plantcode"]);
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@plantID", ddlPlant.SelectedValue.ToString());
            SqlDataReader reader = SqlHelper.ExecuteReader(adam.dsn0(), CommandType.StoredProcedure, "sp_rp_selectDeptByPlant", param);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    ddlDept.Items.Add(new ListItem(reader["name"].ToString(), reader["departmentID"].ToString()));
                }
                reader.Close();
            }
        }
        catch { }
    }
    protected override void BindGridView()
    {
        DataTable dt = SelectPurchaseMstrList();
        gv.DataSource = dt;
        gv.DataBind();
    }
    private DataTable SelectPurchaseMstrList()
    {
        string str = "sp_rp_SelectPurchaseSearchList";
        SqlParameter[] param = new SqlParameter[10];
        param[0] = new SqlParameter("@plant", ddlPlant.SelectedValue.ToString());
        param[1] = new SqlParameter("@busDept", ddlBusDept.SelectedValue.ToString());
        param[2] = new SqlParameter("@status", ddlProc.SelectedValue.ToString());
        param[3] = new SqlParameter("@uID", Session["uID"].ToString());
        param[4] = new SqlParameter("@dept", ddlDept.SelectedValue.ToString());
        param[5] = new SqlParameter("@qad", txtQAD.Text.Trim());
        param[6] = new SqlParameter("@part", txtpart.Text.Trim());
        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, str, param).Tables[0];
    }
    protected void gv_RowCommand(object sender, GridViewCommandEventArgs e)
    {//定义参数
        
        if (e.CommandName == "Detial")
        {
            int intRow = Convert.ToInt32(e.CommandArgument.ToString());
            string _ID = gv.DataKeys[intRow].Values["ID"].ToString();
         
            this.Redirect("../Purchase/RP_purSearchdet.aspx?type=edit&ID=" + gv.DataKeys[intRow].Values["ID"].ToString());
        }
        if (e.CommandName == "ViewEdit")
        {
            int intRow = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
            //int intRow = Convert.ToInt32(e.CommandArgument.ToString());
            string pur_Nbr = gv.DataKeys[intRow].Values["pur_Nbr"].ToString();
            string pur_Line = gv.DataKeys[intRow].Values["pur_Line"].ToString();
            this.Redirect("../Purchase/RP_purSearchdet.aspx?type=edit&pur_Nbr=" + pur_Nbr + "&pur_Line=" + pur_Line);
        }
        if (e.CommandName == "ViewEditqty")
        {
            int intRow = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
            //int intRow = Convert.ToInt32(e.CommandArgument.ToString());
            string pur_Nbr = gv.DataKeys[intRow].Values["pur_Nbr"].ToString();
            string pur_Line = gv.DataKeys[intRow].Values["pur_Line"].ToString();
            this.Redirect("../Purchase/RP_purSearchdet.aspx?type=qty&pur_Nbr=" + pur_Nbr + "&pur_Line=" + pur_Line);
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindGridView();
    }
    protected void ddlPlant_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindBusDept();
        BindDept();
    }
    private void BindBusDept()
    {
        DataTable dt = SelectBusDept();

        ddlBusDept.DataSource = dt;
        ddlBusDept.DataBind();

        ddlBusDept.Items.Insert(0, new ListItem("--全部--", "0"));
    }
    private DataTable SelectBusDept()
    {
        string sql = "select departmentid,departmentname from RP_department Where plantcode = " + ddlPlant.SelectedValue.ToString();
        //SqlParameter[] param = new SqlParameter[10];
        //param[0] = new SqlParameter("@plant", ddlPlant.SelectedValue.ToString());

        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.Text, sql).Tables[0];
    }
    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (Convert.ToInt32(gv.DataKeys[e.Row.RowIndex].Values["rp_plantCode"]) == 1)
            {
                e.Row.Cells[0].Text = "SZX";
            }
            else if (Convert.ToInt32(gv.DataKeys[e.Row.RowIndex].Values["rp_plantCode"]) == 2)
            {
                e.Row.Cells[0].Text = "ZQL";
            }
            else if (Convert.ToInt32(gv.DataKeys[e.Row.RowIndex].Values["rp_plantCode"]) == 5)
            {
                e.Row.Cells[0].Text = "YQL";
            }
            else if (Convert.ToInt32(gv.DataKeys[e.Row.RowIndex].Values["rp_plantCode"]) == 8)
            {
                e.Row.Cells[0].Text = "HQL";
            }

            if (Convert.ToInt32(gv.DataKeys[e.Row.RowIndex].Values["Status"]) == 0)
            {
                //e.Row.Cells[6].BackColor = System.Drawing.Color.Red;
                //e.Row.Cells[6].Enabled = false;
                e.Row.Cells[15].Text = "申请人未提交";
            }
            else
            {
                //e.Row.Cells[2].BackColor = System.Drawing.Color.Red;
                e.Row.Cells[2].Enabled = false;
                //e.Row.Cells[8].BackColor = System.Drawing.Color.Red;
               
                if (Convert.ToInt32(gv.DataKeys[e.Row.RowIndex].Values["Status"]) == 10)
                {
                    e.Row.Cells[15].Text = "已提交待部门主管签字";
                }
                else if (Convert.ToInt32(gv.DataKeys[e.Row.RowIndex].Values["Status"]) == 20)
                {
                    e.Row.Cells[15].Text = "部门主管已签字待业务人员审核";
                }
                else if (Convert.ToInt32(gv.DataKeys[e.Row.RowIndex].Values["Status"]) == 30)
                {
                    e.Row.Cells[15].Text = "业务人员已提交待业务主管签字";
                }
                else if (Convert.ToInt32(gv.DataKeys[e.Row.RowIndex].Values["Status"]) == 40)
                {
                    e.Row.Cells[15].Text = "业务主管已签字待设备部审核";
                }
                else if (Convert.ToInt32(gv.DataKeys[e.Row.RowIndex].Values["Status"]) == 50)
                {
                    if (Convert.ToInt32(gv.DataKeys[e.Row.RowIndex].Values["supplierType"]) == 20)
                    {
                        e.Row.Cells[15].Text = "供应商已提交财务核价中";
                    }
                    else if (Convert.ToInt32(gv.DataKeys[e.Row.RowIndex].Values["supplierType"]) == 1)
                    {
                        e.Row.Cells[15].Text = "设备部已签字供应商核价中";
                    }
                    else
                    {
                        e.Row.Cells[15].Text = "设备部已签字待供应商核价";
                    }
                }
                else if (Convert.ToInt32(gv.DataKeys[e.Row.RowIndex].Values["Status"]) == 60)
                {
                    e.Row.Cells[15].Text = "供应商已核价待副总签字";
                }
                else if (Convert.ToInt32(gv.DataKeys[e.Row.RowIndex].Values["Status"]) == 70)
                {
                    e.Row.Cells[15].Text = "副总已签字";
                }
            }

            if (gv.DataKeys[e.Row.RowIndex].Values["createBy"].ToString() != Session["uID"].ToString())//|| Convert.ToInt32(gv.DataKeys[e.Row.RowIndex].Values["status"]) == 10)
            {
                //e.Row.Cells[2].BackColor = System.Drawing.Color.Red;
                e.Row.Cells[2].Enabled = false;
                //e.Row.Cells[8].BackColor = System.Drawing.Color.Red;
             
            }
        }
    }
    protected void gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
       
    }
    protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv.PageIndex = e.NewPageIndex;

        BindGridView();
    }
    protected void btnexcell_Click(object sender, EventArgs e)
    {
        DataTable dt = SelectPurchaseMstrList();
        string title = "<b>申请单号</b>~^<b>创建人</b>~^<b>创建时间</b>~^<b>QAD</b>~^<b>价格</b>~^100^<b>描述1</b>~^100^<b>描述2</b>~^100^<b>单位</b>~^<b>数量</b>~^100^<b>订单号</b>~^<b>行号</b>~^100^<b>送货数</b>~^";
        this.ExportExcel(title, dt, true);
    }
}