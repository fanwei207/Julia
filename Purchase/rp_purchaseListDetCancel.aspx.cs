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
using Purchase;
public partial class Purchase_rp_purchaseListDetCancel : System.Web.UI.Page
{
    adamClass adam = new adamClass();
    RPPurchase rp = new RPPurchase();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        if (txtReason.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('请填写拒绝原因！'); ";
            return;
        }
        if (CancelPurchaseDetByID())
        {
            ltlAlert.Text = "alert('更新失败，请联系管理员！'); ";
            return;
        }
        else
        {
            #region 取消后发送邮件给申请人
            string Topical = "采购单：" + selectRPNo();
            string mto = selectCreateByEmail();
            string body = "<html>";
            body += "<body>";
            body += "<form>";
            body += "<br>";
            body += "您好:" + "<br>";
            body += "   您在采购单" + selectRPNo() + "中申请的物料" + selectQAD() + "被采购员拒绝了,请重新进行申请，拒绝原因如下<br>";
            body += "   " + txtReason.Text + "<br>";
            body += "<br>";
            body += "</body>";
            body += "</form>";
            body += "</html>";
            rp.SendEmail222(Session["email"].ToString(), mto, Topical, body);
            #endregion

            ltlAlert.Text = "alert('操作成功！'); ";
            return;
        }
    }
    private bool CancelPurchaseDetByID()
    {
        string str = "sp_rp_CancelPurchaseDetByID";
        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@ID", Request["ID"].ToString());
        param[1] = new SqlParameter("@reason", txtReason.Text.Trim());

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, str, param));
    }
    private string selectRPNo()
    {
        string sql = "Select rp_No From rp_purchaseDet Where ID = '" + Request["ID"].ToString() + "'";
        return Convert.ToString(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.Text, sql));
    }
    private string selectCreateByEmail()
    { 
        string sql = "select email From Users u ";
        sql += " left join rp_purchaseMstr mstr on u.userID = mstr.createBy ";
        sql += " left join rp_purchaseDet det on mstr.ID = det.MID ";
        sql += " where det.ID = '" + Request["ID"].ToString() + "'";
        return Convert.ToString(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.Text, sql));
    }
    private string selectQAD()
    {
        string sql = "Select rp_qad From rp_purchaseDet Where ID = '" + Request["ID"].ToString() + "'";
        return Convert.ToString(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.Text, sql));
    }
}