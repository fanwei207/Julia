using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using Microsoft.ApplicationBlocks.Data;


public partial class plan_pur_AddReviewConf : System.Web.UI.Page
{
    string conn = System.Configuration.ConfigurationManager.AppSettings["SqlConn.qadplan"];
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string type = Request["type"].ToString();
            if (type == "add")
            {
                ddlNote2.Enabled = false;
                ddlNote3.Enabled = false;
                btnTJ.Visible = false;
                BindAllSupplier();
                BindNode();
            }
            if (type == "edit")
            {
                ddlSupplier.Enabled = false;
                btnSubmit.Visible = false;
                BindAllSupplier();
                BindNode();
                ddlSupplier.SelectedValue = Request["ad_addr"].ToString();
                ddlNote1.SelectedValue = Request["node1"].ToString();
                ddlNote2.SelectedValue = Request["node2"].ToString();
                ddlNote3.SelectedValue = Request["Node_Id3"].ToString();
                txtMoney.Text = Request["money"].ToString();
                chkIsShow.Checked = Convert.ToBoolean(Request["isShow"].ToString());
            }
        }
    }
    /// <summary>
    /// 绑定所有供应商
    /// </summary>
    private void BindAllSupplier()
    {
        DataTable dt = GetAllSupplier();
        ddlSupplier.DataSource = dt;
        ddlSupplier.DataBind();
        ddlSupplier.Items.Insert(0, new ListItem("--供应商--", "0"));
    }
    /// <summary>
    /// 获取所有供应商
    /// </summary>
    private DataTable GetAllSupplier()
    {
        return SqlHelper.ExecuteDataset(conn, CommandType.StoredProcedure, "sp_pur_GetAllSupplier").Tables[0];
    }

    /// <summary>
    /// 绑定所有节点
    /// </summary>
    private void BindNode()
    {
        DataTable dt = GetNode();
        ddlNote1.DataSource = dt;
        ddlNote1.DataBind();
        ddlNote1.Items.Insert(0, new ListItem("--节点1--", "0"));

        ddlNote2.DataSource = dt;
        ddlNote2.DataBind();
        ddlNote2.Items.Insert(0, new ListItem("--节点2--", "0"));

        ddlNote3.DataSource = dt;
        ddlNote3.DataBind();
        ddlNote3.Items.Insert(0, new ListItem("--节点3--", "0"));
    }
    /// <summary>
    /// 获取所有节点
    /// </summary>
    private DataTable GetNode()
    {
        return SqlHelper.ExecuteDataset(conn, CommandType.StoredProcedure, "sp_pur_GetNode").Tables[0];
    }
    protected void ddlNote1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlNote1.SelectedValue.ToString() != "0")
        {
            ddlNote2.Enabled = true;
        }
        else
        {
            ddlNote2.Enabled = false;
            ddlNote3.Enabled = false;
            ddlNote2.SelectedValue = "0";
            ddlNote3.SelectedValue = "0";
        }
    }
    protected void ddlNote2_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlNote2.SelectedValue.ToString() != "0")
        {
            ddlNote3.Enabled = true;
        }
        else
        {
            ddlNote3.Enabled = false;
            ddlNote3.SelectedValue = "0";
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (ddlSupplier.SelectedValue == "0")
        {
            ltlAlert.Text = "alert('请选择供应商!')";
            return;
        }
        else if (txtMoney.Text == string.Empty)
        {
            ltlAlert.Text = "alert('请填写金额!')";
            return;
        }
        else if (ddlNote1.SelectedValue == "0")
        {
            ltlAlert.Text = "alert('请选择节点一!')";
            return;
        }
        string supplier = ddlSupplier.SelectedValue.ToString();
        if (isExistSupp(supplier))
        {
            ltlAlert.Text = "alert('此供应商已配置过!')";
            return;
        }
        string money = txtMoney.Text;
        int isshow = Convert.ToInt32(chkIsShow.Checked);
        Guid nodes1= Guid.NewGuid();
        string node1 = string.Empty;
        if (ddlNote1.SelectedValue == "0")
        {
            node1 = "7117E430-276C-47ED-8CF4-26D4CB696A2C";
        }
        else
        {
            node1 = ddlNote1.SelectedValue.ToString();
        }
        Guid nodess1 = new Guid(node1);

        Guid nodes2= Guid.NewGuid();
        string node2 = string.Empty;
        if (ddlNote2.SelectedValue == "0")
        {
            node2 = "7117E430-276C-47ED-8CF4-26D4CB696A2C";
        }
        else
        {
            node2 = ddlNote2.SelectedValue.ToString();
        }
        Guid nodess2 = new Guid(node2);

        Guid userID= Guid.NewGuid();
        string node3 = string.Empty;
        if (ddlNote3.SelectedValue == "0")
        {
            node3 = "7117E430-276C-47ED-8CF4-26D4CB696A2C";
        }
        else
        {
            node3 = ddlNote3.SelectedValue.ToString();
        }
        Guid nodess3 = new Guid(node3);
        if (addSuppReviewConf(supplier, money, isshow, nodess1, nodess2, nodess3))
        {
            ltlAlert.Text = "alert('添加成功!')";
            return;
        }
        else
        {
            ltlAlert.Text = "alert('添加失败!')";
            return;
        }
    }
    private bool addSuppReviewConf(string supplier, string money, int isshow, Guid node1, Guid node2, Guid node3)
    {
        SqlParameter[] pram = new SqlParameter[6];
        pram[0] = new SqlParameter("@supplier",supplier);
        pram[1] = new SqlParameter("@money",money);
        pram[2] = new SqlParameter("@isshow",isshow);
        pram[3] = new SqlParameter("@node1",node1);
        pram[4] = new SqlParameter("@node2",node2);
        pram[5] = new SqlParameter("@node3", node3);
        return Convert.ToBoolean(SqlHelper.ExecuteScalar(conn, CommandType.StoredProcedure, "sp_pur_addSuppReviewConf", pram));
    }
    /// <summary>
    /// 判断是否已存在供应商配置
    /// </summary>
    /// <returns></returns>
    private bool isExistSupp(string supplier)
    { 
        SqlParameter[] pram = new SqlParameter[1];
        pram[0] = new SqlParameter("@ad_addr", supplier);

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(conn, CommandType.StoredProcedure, "sp_pur_isExistSupp", pram));
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("pur_ReviewConfList.aspx");
    }
    protected void btnTJ_Click(object sender, EventArgs e)
    {
        if (ddlSupplier.SelectedValue == "0")
        {
            ltlAlert.Text = "alert('请选择供应商!')";
            return;
        }
        else if (txtMoney.Text == string.Empty)
        {
            ltlAlert.Text = "alert('请填写金额!')";
            return;
        }
        else if (ddlNote1.SelectedValue == "0")
        {
            ltlAlert.Text = "alert('请选择节点一!')";
            return;
        }
        else if (ddlNote3.SelectedValue != "0")
        {
            if (ddlNote2.SelectedValue == "0")
            {
                ltlAlert.Text = "alert('请选择节点二!')";
                ddlNote3.SelectedValue = "0";
                return;
            }
        }
        string supplier = ddlSupplier.SelectedValue.ToString();
        string money = txtMoney.Text;
        int isshow = Convert.ToInt32(chkIsShow.Checked);
        Guid nodes1 = Guid.NewGuid();
        string node1 = string.Empty;
        if (ddlNote1.SelectedValue == "0")
        {
            node1 = "7117E430-276C-47ED-8CF4-26D4CB696A2C";
        }
        else
        {
            node1 = ddlNote1.SelectedValue.ToString();
        }
        Guid nodess1 = new Guid(node1);

        Guid nodes2 = Guid.NewGuid();
        string node2 = string.Empty;
        if (ddlNote2.SelectedValue == "0")
        {
            node2 = "7117E430-276C-47ED-8CF4-26D4CB696A2C";
        }
        else
        {
            node2 = ddlNote2.SelectedValue.ToString();
        }
        Guid nodess2 = new Guid(node2);

        Guid userID = Guid.NewGuid();
        string node3 = string.Empty;
        if (ddlNote3.SelectedValue == "0")
        {
            node3 = "7117E430-276C-47ED-8CF4-26D4CB696A2C";
        }
        else
        {
            node3 = ddlNote3.SelectedValue.ToString();
        }
        Guid nodess3 = new Guid(node3);

        if (updateSuppReviewConf(supplier, money, isshow, nodess1, nodess2, nodess3))
        {
            ltlAlert.Text = "alert('保存成功!')";
            return;
        }
        else
        {
            ltlAlert.Text = "alert('保存失败!')";
            return;
        }
    }
    /// <summary>
    /// 更新配置
    /// </summary>
    /// <returns></returns>
    private bool updateSuppReviewConf(string supplier, string money, int isshow, Guid node1, Guid node2, Guid node3)
    {
        SqlParameter[] pram = new SqlParameter[6];
        pram[0] = new SqlParameter("@supplier", supplier);
        pram[1] = new SqlParameter("@money", money);
        pram[2] = new SqlParameter("@isshow", isshow);
        pram[3] = new SqlParameter("@node1", node1);
        pram[4] = new SqlParameter("@node2", node2);
        pram[5] = new SqlParameter("@node3", node3);
        return Convert.ToBoolean(SqlHelper.ExecuteScalar(conn, CommandType.StoredProcedure, "sp_pur_updateSuppReviewConf", pram));
    }
}