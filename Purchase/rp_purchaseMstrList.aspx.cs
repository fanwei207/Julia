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
public partial class Purchase_rp_purchaseMstrLise : BasePage
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
        string str = "sp_rp_SelectPurchaseMstrList";
        SqlParameter[] param = new SqlParameter[10];
        param[0] = new SqlParameter("@plant", ddlPlant.SelectedValue.ToString());
        param[1] = new SqlParameter("@busDept", ddlBusDept.SelectedValue.ToString());
        param[2] = new SqlParameter("@status", ddlProc.SelectedValue.ToString());
        param[3] = new SqlParameter("@uID", Session["uID"].ToString());
        param[4] = new SqlParameter("@dept", ddlDept.SelectedValue.ToString());
        param[5] = new SqlParameter("@qad", txtQAD.Text.Trim());

        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, str, param).Tables[0];
    }
    protected void gv_RowCommand(object sender, GridViewCommandEventArgs e)
    {//定义参数
        if (e.CommandName.ToString() == "ViewEdit")
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;

            this.Redirect("../Purchase/rp_purchaseDet.aspx?type=det&ID=" + gv.DataKeys[index].Values["ID"].ToString().Trim()
                                                                          + "&no=" + gv.DataKeys[index].Values["rp_No"].ToString().Trim()
                                                                          + "&deptid=" + gv.DataKeys[index].Values["rp_BusinessDept"].ToString().Trim()
                                                                            );
        }
        if (e.CommandName == "Detial")
        {
            int intRow = Convert.ToInt32(e.CommandArgument.ToString());
            string _ID = gv.DataKeys[intRow].Values["ID"].ToString();
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
            this.Redirect("../Purchase/rp_purchaseMstrDetial.aspx?type=edit&ID=" + gv.DataKeys[intRow].Values["ID"].ToString());
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
                e.Row.Cells[6].BackColor = System.Drawing.Color.Red;
                e.Row.Cells[6].Enabled = false;
                e.Row.Cells[7].Text = "申请人未提交";
            }
            else
            {
                //e.Row.Cells[2].BackColor = System.Drawing.Color.Red;
                e.Row.Cells[2].Enabled = false;
                //e.Row.Cells[8].BackColor = System.Drawing.Color.Red;
                e.Row.Cells[8].Enabled = false;
                if (Convert.ToInt32(gv.DataKeys[e.Row.RowIndex].Values["Status"]) == 10)
                {
                    e.Row.Cells[7].Text = "已提交待部门主管签字";
                }
                else if (Convert.ToInt32(gv.DataKeys[e.Row.RowIndex].Values["Status"]) == 20)
                {
                    e.Row.Cells[7].Text = "部门主管已签字待业务人员审核";
                }
                else if (Convert.ToInt32(gv.DataKeys[e.Row.RowIndex].Values["Status"]) == 30)
                {
                    e.Row.Cells[7].Text = "业务人员已提交待业务主管签字";
                }
                else if (Convert.ToInt32(gv.DataKeys[e.Row.RowIndex].Values["Status"]) == 40)
                {
                    e.Row.Cells[7].Text = "业务主管已签字待设备部审核";
                }
                else if (Convert.ToInt32(gv.DataKeys[e.Row.RowIndex].Values["Status"]) == 50)
                {
                    if (Convert.ToInt32(gv.DataKeys[e.Row.RowIndex].Values["supplierType"]) == 20)
                    {
                        e.Row.Cells[7].Text = "供应商已提交财务核价中";
                    }
                    else if (Convert.ToInt32(gv.DataKeys[e.Row.RowIndex].Values["supplierType"]) == 1)
                    {
                        e.Row.Cells[7].Text = "设备部已签字供应商核价中";
                    }
                    else
                    {
                        e.Row.Cells[7].Text = "设备部已签字待供应商核价";
                    }
                }
                else if (Convert.ToInt32(gv.DataKeys[e.Row.RowIndex].Values["Status"]) == 60)
                {
                    e.Row.Cells[7].Text = "供应商已核价待副总签字";
                }
                else if (Convert.ToInt32(gv.DataKeys[e.Row.RowIndex].Values["Status"]) == 70)
                {
                    e.Row.Cells[7].Text = "副总已签字";
                }
            }

            if (gv.DataKeys[e.Row.RowIndex].Values["createBy"].ToString() != Session["uID"].ToString())//|| Convert.ToInt32(gv.DataKeys[e.Row.RowIndex].Values["status"]) == 10)
            {
                //e.Row.Cells[2].BackColor = System.Drawing.Color.Red;
                e.Row.Cells[2].Enabled = false;
                //e.Row.Cells[8].BackColor = System.Drawing.Color.Red;
                e.Row.Cells[8].Enabled = false;
            }
        }
    }
    protected void gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //定义参数
        string ID = gv.DataKeys[e.RowIndex].Values["ID"].ToString();
        SqlParameter[] sqlParam = new SqlParameter[1];
        sqlParam[0] = new SqlParameter("@ID", ID);
        string str = "sp_rp_DeletePurchaseMstr";
        SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, str, sqlParam);

        BindGridView();
    }
    protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv.PageIndex = e.NewPageIndex;

        BindGridView();
    }
}