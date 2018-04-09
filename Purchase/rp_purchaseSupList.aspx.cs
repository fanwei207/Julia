using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;
using System.IO;
using System.Text;
using System.Configuration;
public partial class Purchase_rp_purchaseSupList : BasePage
{
    adamClass adam = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Label1.Text = Request["ID"].ToString();
            hidID.Value = Request["ID"];
            BindGV();
        }
    }
    private void BindGV()
    {
        DataTable dt = SelectPurchaseDet();
        gv.DataSource = dt;
        gv.DataBind();
    }

    private DataTable SelectPurchaseDet()
    {
        string sql = "Select * From rp_purchaseDet Where MID = '" + hidID.Value.ToString() + "' And isnull(rp_price,0.0000) = 0.0000";
        
        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.Text, sql).Tables[0];
    }
    protected void btnInquiry_Click(object sender, EventArgs e)
    {
        if (InsertPurchaseToInquiryDet())
        {
            BindGV();
        }
        else
        {
            
            ltlAlert.Text = "alert('核价失败，请联系管理员'); ";
            return;
        }
    }
    private bool InsertPurchaseToInquiryDet()
    {
        string str = "sp_rp_InsertPurchaseToInquiryDet";
        SqlParameter param = new SqlParameter("@ID", hidID.Value.ToString());

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, str, param));
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        if (UpdateSupListDetByID())
        {
            ltlAlert.Text = "alert('更新失败，请联系管理员！'); ";
            return;
        }
        BindGV();
        hidGvID.Value = string.Empty;
        txtQAD.Text = string.Empty;
        txtVend.Text = string.Empty;
        txtVendName.Text = string.Empty;
        txtUm.Text = string.Empty;
        txtQty.Text = string.Empty;
        txtPrice.Text = string.Empty;
        txtQADDesc1.Text = string.Empty;
        txtQADDesc2.Text = string.Empty;
        txtUses.Text = string.Empty;
        txtDescript.Text = string.Empty;
        txtFormat.Text = string.Empty;


        string rp_no = string.Empty;
        string CEOEmail = string.Empty;

        if (SelectIsExistsPriceForSupplist(out rp_no, out CEOEmail))
        {
            #region 设备部同意后如果价格全都都有发送邮件给副总
            string Topical = "采购单：" + rp_no;
            string mto = CEOEmail;
            StringBuilder body = new StringBuilder();
            //body.Append("<html>");
            //body.Append("<body>");
            //body.Append("<form>");
            body.Append("<br>");
            body.Append("您好:" + "<br>");
            body.Append( "   有新的采购单已被设备部同意，请您在系统中进行相应操作，采购单号：" + rp_no + "<br>");
            body.Append( "<br>");
            //body.Append("</body>");
            //body.Append("</form>");
            //body.Append("</html>");
            SendEmail(ConfigurationManager.AppSettings["AdminEmail"].ToString(), mto, "", Topical, body.ToString());
            #endregion
        }
    }

 
    private bool SelectIsExistsPriceForSupplist(out string rp_no, out string CEOEmail)
    {
        string str = "sp_rp_SelectIsExistsPriceForSupplist";
        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@id", hidID.Value.ToString())
            ,new SqlParameter("@rp_no",SqlDbType.NVarChar,50)
            ,new SqlParameter("@CEOEmail",SqlDbType.NVarChar,200)
        
        };

        param[1].Direction = ParameterDirection.Output;
        param[2].Direction = ParameterDirection.Output;
            
        bool returnFlag = Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, str, param));

        rp_no = param[1].Value.ToString();
        CEOEmail = param[2].Value.ToString();
        return returnFlag;
    }
    private bool UpdateSupListDetByID()
    {
        string str = "sp_rp_UpdateSupListDetByID";
        SqlParameter[] param = new SqlParameter[18];
        param[0] = new SqlParameter("@vend", txtVend.Text.Trim());
        param[1] = new SqlParameter("@vendname", txtVendName.Text.Trim());
        param[2] = new SqlParameter("@price", hidPrice.Value.ToString());
        param[3] = new SqlParameter("@qaddesc1", hidDesc1.Value.ToString());
        param[4] = new SqlParameter("@qaddesc2", hidDesc2.Value.ToString());
        param[5] = new SqlParameter("@ID", hidGvID.Value.ToString());
        param[6] = new SqlParameter("@qad", txtQAD.Text.Trim());
        param[7] = new SqlParameter("@um", hidUm.Value.ToString());
        param[8] = new SqlParameter("@uID", Session["uID"].ToString());
        param[9] = new SqlParameter("@uName", Session["uName"].ToString());

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, str, param));
    }
}