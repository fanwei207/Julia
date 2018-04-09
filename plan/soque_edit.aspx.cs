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
using System.Text.RegularExpressions;

public partial class soque_edit : BasePage
{
    adamClass adam = new adamClass();
    protected override void OnInit(EventArgs e)
    {
        if (!IsPostBack)
        {
            this.Security.Register("44000015", "注册编辑所有明细的权限");
        }

        base.OnInit(e);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["id"] != null)
            {
                GetMstrInfo();
                string strSQL = "select top 1 Convert(varchar(10),soques_dueDate,120) from soque_det where soques_mstr_id = " + Request.QueryString["id"] + " and isnull(soques_check,0)= 0 and isnull(soques_confirmName,'') <> '' ";
                if (!this.Security["44000015"].isValid)
                {
                    strSQL += " and soques_userNo = '" + Session["loginName"].ToString() + "' and soques_plant = " + Session["plantCode"].ToString();
                }
                strSQL += "order by soques_dueDate desc";
                SqlDataReader reader = SqlHelper.ExecuteReader(adam.dsn0(), CommandType.Text, strSQL);
                if (reader.Read())
                {
                    txtDueDate.Text = reader[0].ToString();
                }
                reader.Close();
            }
            else
            {
                ltlAlert.Text = "alert('Error！');";
            }

            BindDegree();
            BindDetails();
            GetTracker();
        }
    }

    protected void GetMstrInfo()
    {
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@id", Request.QueryString["id"]);

        DataSet ds = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, "sp_plan_selectSoQueList", param);

        if (ds != null)
        {
            txtNbr.Text = ds.Tables[0].Rows[0]["soque_nbr"].ToString();
            txtLine.Text = ds.Tables[0].Rows[0]["soque_line"].ToString();
            txtCustPart.Text = ds.Tables[0].Rows[0]["soque_cus_part"].ToString();
            txtQty.Text = ds.Tables[0].Rows[0]["soque_qty_ord"].ToString();
            txtPart.Text = ds.Tables[0].Rows[0]["soque_part"].ToString();
            txtCust.Text = ds.Tables[0].Rows[0]["soque_cus"].ToString();
            if (ds.Tables[0].Rows[0]["soque_date_ord"].ToString() != "")
            {
                txtOrdDate.Text = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(ds.Tables[0].Rows[0]["soque_date_ord"]));
            }
            if (ds.Tables[0].Rows[0]["soque_date_ship"].ToString() != "")
            {
                txtShipDate.Text = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(ds.Tables[0].Rows[0]["soque_date_ship"]));
            }
            txtRmks.Text = ds.Tables[0].Rows[0]["soque_remarks"].ToString();

            txtNbr.Enabled = false;
            txtLine.Enabled = false;
            txtCustPart.Enabled = false;
            txtQty.Enabled = false;
            //txtPart.Enabled = false;
            txtCust.Enabled = false;
            txtOrdDate.Enabled = false;
            txtShipDate.Enabled = false;

            dropDegree.Enabled = false;
        }
    }

    protected void GetTracker()
    {
        DataSet ds = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, "sp_plan_selectSoqueTracker");

        if (ds != null)
        {
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                txb_cc.Text += "," + row["userName"].ToString();
                txb_ccid.Text += "," + row["userID"].ToString();
            }
        }

        if (txb_cc.Text.Length > 0)
        {
            txb_cc.Text += ",";
            txb_ccid.Text += ",";
        }
    }

    protected void BindDegree()
    {
        DataSet ds = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, "sp_plan_selectSoqueDegree");

        dropDegree.DataSource = ds;
        dropDegree.DataBind();
    }

    protected void BindDetails()
    {
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@mstr_id", Request.QueryString["id"]);

        DataSet ds = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, "sp_plan_selectSoqueDetails", param);

        chklDetails.DataSource = ds;
        chklDetails.DataBind();

        //设置Check属性
        if (ds != null)
        {
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                if (Convert.ToBoolean(row["soques_check"]))
                {
                    //soques_index = id=123;usr=456
                    chklDetails.Items.FindByValue(row["soques_index"].ToString()).Selected = true;

                    if (!txb_choose.Text.Contains(row["soques_userName"].ToString()))
                    {
                        txb_choose.Text += "," + row["soques_userName"].ToString();
                        txb_chooseid.Text += "," + row["userId"].ToString();
                    }
                }
                else
                {
                    chklDetails.Items.FindByValue(row["soques_index"].ToString()).Selected = false;
                }

                //设置Rmks
                //if (row["soques_index"].ToString().Contains("usr=" + Session["uID"].ToString() + ";"))
                //{
                //    txtRmks.Text = row["soques_rmks"].ToString();
                //}
            }

            if (txb_choose.Text.Length > 0)
            {
                txb_choose.Text += ",";
                txb_chooseid.Text += ",";
            }
        }
        //登陆人只能操作自己的选项
        if (!this.Security["44000015"].isValid)
        {
            for (int i = 0; i < chklDetails.Items.Count; i++)
            {
                chklDetails.Items[i].Enabled = false;

                if (chklDetails.Items[i].Value.Contains("usr=" + Session["uID"].ToString() + ";"))
                {
                    chklDetails.Items[i].Enabled = true;
                }
            }
        }
    }

    protected void btn_choose_Click(object sender, EventArgs e)
    {
        ltlAlert.Text = "var w=window.open('soque_choose2.aspx?id=" + Request["id"] + "','docitem','menubar=No,scrollbars = No,resizable=No,width=500,height=500,top=200,left=300'); w.focus();";
    }
    protected void btn_cc_Click(object sender, EventArgs e)
    {
        ltlAlert.Text = "var w=window.open('soque_choose3.aspx?id=" + Request["id"] + "','docitem','menubar=No,scrollbars = No,resizable=No,width=500,height=500,top=200,left=300'); w.focus();";
    }
    protected void btn_back_Click(object sender, EventArgs e)
    {
        Response.Redirect("soque_list.aspx?rt=" + DateTime.Now.ToString());
    }
    protected void btn_next_Click(object sender, EventArgs e)
    {
        //if (txtEmail.Text.Trim().Length == 0)
        //{
        //    ltlAlert.Text = "alert('E-mail address is required！');";
        //    return;
        //}

        bool flag = true;

        for (int i = 0; i < chklDetails.Items.Count; i++)
        {
            //只能操作自己的
            if (chklDetails.Items[i].Enabled == true)
            {
                if (chklDetails.Items[i].Selected == true)
                {
                    //当明细项确认存在问题的时候必须填写一个问题处理的截止日期--Wangdl--2014-07-17
                    if (txtDueDate.Text.Trim() == string.Empty)
                    {
                        ltlAlert.Text = "alert('Due Date is required！');";
                        return;
                    }
                }

                try
                {
                    SqlParameter[] param = new SqlParameter[7];
                    param[0] = new SqlParameter("@det_id", chklDetails.Items[i].Value);
                    param[1] = new SqlParameter("@det_value", chklDetails.Items[i].Selected);
                    param[2] = new SqlParameter("@rmks", txtRmks.Text.Trim());
                    param[3] = new SqlParameter("@uID", Session["uID"]);
                    param[4] = new SqlParameter("@uName", Session["uName"]);
                    param[5] = new SqlParameter("@dueDate", txtDueDate.Text.Trim());
                    param[6] = new SqlParameter("@part", txtPart.Text.Trim());

                    SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, "sp_plan_confirmSoqueDetails", param);
                }
                catch
                {
                    flag = false;
                    ltlAlert.Text = "alert('Operation Failure！');";
                    break;
                }
            }

        }

        if (flag)
        {
            ltlAlert.Text = "alert('Operation Success！');";
        }
        //发送邮件

    }
}
