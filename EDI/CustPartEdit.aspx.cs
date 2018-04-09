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
public partial class CustPartEdit : BasePage
{
    adamClass adam = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request["id"] != null)
            {
                lbID.Text = Request["id"].ToString();
                BindGridView();
                Edit();

            }
            else
            {
                gvlist.Visible = false;
                btnback.Visible = false;
            }
        }

    }
    public void Edit()
    {
        btnSave.Visible = true;
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@id", lbID.Text.Trim());
        SqlDataReader reader = SqlHelper.ExecuteReader(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_edi_selectCustPartDet", param);
        while (reader.Read())
        {
            txtCust.Text = reader["cp_cust"].ToString();
            txtPart.Text = reader["cp_cust_part"].ToString();
            txtQad.Text = reader["cp_part"].ToString();
            txtSku.Text = reader["Xord_Cust_Item"].ToString();
            txtComment.Text = reader["cp_comment"].ToString();
            txtPartd.Text = reader["cp_cust_partd"].ToString();
            dropDomain.SelectedValue = reader["cp_domain"].ToString();
            txtStdDate.Text = reader["cp_start_date"].ToString();
            txtEndDate.Text = reader["cp_end_date"].ToString();
            txtUL.Text = reader["cp_ul"].ToString();
            lblckickQAD.Text = reader["cp_part"].ToString().Trim();
            lblchickUL.Text = reader["xxwkf_chr02"].ToString().Trim();
            if (txtUL.Text == string.Empty)
            {
                string strSql = "Select QAD_Data.dbo.fn_getULInfo('" + txtQad.Text.Trim() + "')";
                txtUL.Text = SqlHelper.ExecuteScalar(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.Text, strSql).ToString();

                if (string.IsNullOrEmpty(txtUL.Text))
                {
                    this.Alert("UL认证尚未通过！请联系技术部！");
                }
            }
            if (reader["appvResult"].ToString() == "1")
            {
                txtPart.ReadOnly = true;
                txtCust.ReadOnly = true;
                txtQad.ReadOnly = true;
            }
        }
        reader.Close();
        string qad = txtQad.Text;
        if (qad.Substring(0, 1) == "1" && qad.Substring(qad.Length - 1) == "0")
        {
            try
            {
                SqlParameter[] param1 = new SqlParameter[2];
                param1[0] = new SqlParameter("@part", qad.Trim());
                param1[1] = new SqlParameter("@retValue", SqlDbType.NVarChar, 50);
                param1[1].Direction = ParameterDirection.Output;
                SqlHelper.ExecuteNonQuery(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_edi_checkULInf", param1);

                if (param1[1].Value.ToString().Length == 0)
                {
                    this.Alert("整灯未锁定！请联系技术部!");
                }
                else
                {
                    lblchickUL.Text = param1[1].Value.ToString();
                }
            }
            catch
            {
                lblchickUL.Text = string.Empty;
            }
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        DateTime std_date = DateTime.Now;
        DateTime now = std_date;
        DateTime end_date = DateTime.Now;
        if (txtCust.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('客户货物/发往 不能为空!');";
            BindGridView();
            return;
        }
        if (txtPart.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('客户物料 不能为空!');";
            BindGridView();
            return;
        }
        if (txtQad.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('物料号 不能为空!');";
            BindGridView();
            return;
        }
        //if (txtQad.Text.Substring(0, 1) == "1" && (txtQad.Text.Substring(txtQad.Text.Length - 1) == "0" || txtQad.Text.Substring(txtQad.Text.Length - 1) == "1"))
        //{
        //    if (string.IsNullOrEmpty(txtUL.Text))
        //    {
        //        ltlAlert.Text = "alert('UL认证尚未通过！请联系技术部!');";
        //        BindGridView();
        //        return;
        //    }
        //}
        string qad = txtQad.Text.Trim();
        if (qad.Substring(0, 1) == "1" && qad.Substring(qad.Length - 1) == "0")
        {
            if (lblchickUL.Text.Trim() == string.Empty)
            {
                ltlAlert.Text = "alert('整灯未锁定，请联系技术部!');";
                BindGridView();
                return;
            }
            //else if (lblchickUL.Text.Trim() != txtPart.Text.Trim())
            //{
            //    ltlAlert.Text = "alert('整灯锁定信息客户物料号为" + lblchickUL.Text.Trim() + "!');";
            //    BindGridView();
            //    return;
            //}
        }
        if (qad.Substring(0, 1) == "1")
        {
            if (string.IsNullOrEmpty(txtUL.Text))
            {
                ltlAlert.Text = "alert('UL认证尚未通过！请联系技术部!');";
                BindGridView();
                return;
            }
        }
       
        string txtStdDate2 = txtStdDate.Text;
        string txtEndDate2 = txtEndDate.Text;
        if (txtStdDate2.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('生效日期不能为空！');";
            BindGridView();
            return;
        }
        if (txtStdDate2.Trim().Length > 0)
        {
            try
            {
                std_date = Convert.ToDateTime(txtStdDate2.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('生效日期格式不正确!正确格式如：2012-01-01');";
                BindGridView();
                return;
            }
        }
        if (txtEndDate2.Trim().Length > 0)
        {
            try
            {
                end_date = Convert.ToDateTime(txtEndDate2.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('截止日期格式不正确!正确格式如：2012-01-01');";
                BindGridView();
                return;
            }
        }
        if (end_date < std_date && std_date != now && txtEndDate2 != string.Empty && txtStdDate2 != string.Empty)
        {
            ltlAlert.Text = "alert('截止日期必须晚于开始日期!');";
            BindGridView();
            return;
        }
        try
        {
            SqlParameter[] param = new SqlParameter[15];
            param[0] = new SqlParameter("@id", lbID.Text.Trim());
            param[1] = new SqlParameter("@domain", dropDomain.SelectedValue);
            param[2] = new SqlParameter("@custCode", txtCust.Text.Trim());
            param[3] = new SqlParameter("@custPart", txtPart.Text.Trim());
            param[4] = new SqlParameter("@qad", txtQad.Text.Trim());
            param[5] = new SqlParameter("@stdDate", txtStdDate2);
            param[6] = new SqlParameter("@endDate", txtEndDate2);
            param[7] = new SqlParameter("@comment", txtComment.Text.Trim());
            param[8] = new SqlParameter("@partd", txtPartd.Text.Trim());
            param[9] = new SqlParameter("@uID", Session["uID"].ToString());
            param[10] = new SqlParameter("@uName", Session["uName"].ToString());
            param[11] = new SqlParameter("@retValue", SqlDbType.NVarChar, 50);
            param[11].Direction = ParameterDirection.Output;
            param[12] = new SqlParameter("@sku", txtSku.Text.Trim());
            param[13] = new SqlParameter("@ul", txtUL.Text.Trim());
            //param[14] = new SqlParameter("@ckickQAD", lblckickQAD.Text.Trim());
            SqlHelper.ExecuteNonQuery(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_edi_updateCustPart", param);

            if (param[11].Value.ToString().Trim().Length == 0)
            {
                ltlAlert.Text = "alert('保存成功！');";
                btnSave.Attributes.Clear();
                btnupdate.Attributes.Clear();
                BindGridView();
            }
            else
            {
                ltlAlert.Text = "alert('" + param[11].Value.ToString().Trim() + "');";
                //if (param[11].Value.ToString().Trim() == "物料号发生变化，如果确定请再按一次保存。")
                //{
                //    lblckickQAD.Text = "1";

                //}
                //else
                //{
                //    lblckickQAD.Text = "0";
                //}
                BindGridView();
            }
        }
        catch
        {
            ltlAlert.Text = "alert('保存失败！请刷新后重新操作一次！');";
            BindGridView();
        }
    }
    protected void txtCust_TextChanged(object sender, EventArgs e)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@custCode", txtCust.Text.Trim());
            param[1] = new SqlParameter("@retValue", SqlDbType.NVarChar, 50);
            param[1].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_edi_checkCustCode", param);

            if (param[1].Value.ToString().Length == 0)
            {
                txtCust.Text = string.Empty;
                lbCustName.Text = string.Empty;
                ltlAlert.Text = "alert('客户 不存在！请确认代码输入是否正确！');";
                BindGridView();
            }
            else
            {
                lbCustName.Text = param[1].Value.ToString();
            }
        }
        catch
        {
            txtCust.Text = string.Empty;
            lbCustName.Text = string.Empty;
            ltlAlert.Text = "alert('客户 验证失败！请刷新后重新操作一次！');";
            BindGridView();
        }
        ActiveControl();
    }
    protected void txtPart_TextChanged(object sender, EventArgs e)
    {
        this.ltlAlert.Text = "form1.txtQad.focus();";
        if (string.IsNullOrEmpty(txtCust.Text))
        {
            this.ltlAlert.Text = "alert('客户 不能为空！'); form1.txtCust.focus();";
            BindGridView();
            return;
        }
        else if (txtCust.Text.Length != 8)
        {
            this.ltlAlert.Text = "alert('客户 长度是8位！'); form1.txtCust.focus();";
            BindGridView();
            return;
        }
        try
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@cust", txtCust.Text.Trim());
            param[1] = new SqlParameter("@custPart", txtPart.Text.Trim());
            param[2] = new SqlParameter("@retValue", SqlDbType.NVarChar, 50);
            param[2].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_edi_selectCustPartSku", param);

            txtSku.Text = param[2].Value.ToString().Trim();
        }
        catch
        {
            txtSku.Text = string.Empty;
        }
        ActiveControl();
    }
    protected void gvlist_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void gvlist_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvlist.PageIndex = e.NewPageIndex;
        BindGridView();
    }
    protected void BindGridView()
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@id", lbID.Text.Trim());
            DataSet ds = SqlHelper.ExecuteDataset(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_edi_selectCustPartdate", param);
            gvlist.DataSource = ds;
            gvlist.DataBind();
        }
        catch (Exception ee)
        { ;}
    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {

        DateTime std_date = DateTime.Now;
        DateTime now = std_date;
        DateTime end_date = DateTime.Now;
        if (txtCust.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('客户货物/发往 不能为空!');";
            BindGridView();
            return;
        }
        if (txtPart.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('客户物料 不能为空!');";
            BindGridView();
            return;
        }
        if (txtQad.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('物料号 不能为空!');";
            BindGridView();
            return;
        }
        //if (txtQad.Text.Substring(0, 1) == "1" && (txtQad.Text.Substring(txtQad.Text.Length - 1) == "0" || txtQad.Text.Substring(txtQad.Text.Length - 1) == "1"))
        //{
        //    if (string.IsNullOrEmpty(txtUL.Text))
        //    {
        //        ltlAlert.Text = "alert('UL认证尚未通过！请联系技术部!');";
        //        BindGridView();
        //        return;
        //    }
        //}
        string qad = txtQad.Text.Trim();
        if (qad.Substring(0, 1) == "1" && qad.Substring(qad.Length - 1) == "0")
        {

            if (lblchickUL.Text.Trim() == string.Empty)
            {
                ltlAlert.Text = "alert('整灯未锁定，请联系技术部!');";
                BindGridView();
                return;
            }
            //else if (lblchickUL.Text.Trim() != txtPart.Text.Trim())
            //{
            //    ltlAlert.Text = "alert('整灯锁定信息客户物料号为" + lblchickUL.Text.Trim() + "!');";
            //    BindGridView();
            //    return;
            //}
        }
         if (qad.Substring(0, 1) == "1")
        {
            if (string.IsNullOrEmpty(txtUL.Text))
            {
                ltlAlert.Text = "alert('UL认证尚未通过！请联系技术部!');";
                BindGridView();
                return;
            }
        }
      
        string txtStdDate2 = txtStdDate.Text;
        if (txtStdDate2.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('生效日期不能为空！');";
            BindGridView();
            return;
        }
        if (txtStdDate2.Trim().Length > 0)
        {
            try
            {
                std_date = Convert.ToDateTime(txtStdDate2.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('生效日期格式不正确!正确格式如：2012-01-01');";
                BindGridView();
                return;
            }
        }
        string txtEndDate2 = txtEndDate.Text.Trim();
        if (txtEndDate2.Trim().Length > 0)
        {
            try
            {
                end_date = Convert.ToDateTime(txtEndDate2.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('截止日期格式不正确!正确格式如：2012-01-01');";
                BindGridView();
                return;
            }
        }
        if (end_date < std_date && std_date != now && txtEndDate2 != string.Empty && txtStdDate2 != string.Empty)
        {
            ltlAlert.Text = "alert('截止日期必须晚于开始日期!');";
            BindGridView();
            return;
        }
        try
        {
            SqlParameter[] param = new SqlParameter[14];
            param[0] = new SqlParameter("@id", lbID.Text.Trim());
            param[1] = new SqlParameter("@domain", dropDomain.SelectedValue);
            param[2] = new SqlParameter("@custCode", txtCust.Text.Trim());
            param[3] = new SqlParameter("@custPart", txtPart.Text.Trim());
            param[4] = new SqlParameter("@qad", txtQad.Text.Trim());
            param[5] = new SqlParameter("@stdDate", txtStdDate2);
            param[6] = new SqlParameter("@endDate", txtEndDate2);
            param[7] = new SqlParameter("@comment", txtComment.Text.Trim());
            param[8] = new SqlParameter("@partd", txtPartd.Text.Trim());
            param[9] = new SqlParameter("@uID", Session["uID"].ToString());
            param[10] = new SqlParameter("@uName", Session["uName"].ToString());
            param[11] = new SqlParameter("@retValue", SqlDbType.NVarChar, 50);
            param[11].Direction = ParameterDirection.Output;
            param[12] = new SqlParameter("@sku", txtSku.Text.Trim());
            param[13] = new SqlParameter("@ul", txtUL.Text.Trim());
            SqlHelper.ExecuteNonQuery(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_edi_saveCustPart", param);

            if (param[11].Value.ToString().Trim().Length == 0)
            {
                ltlAlert.Text = "alert('保存成功！');";
                btnupdate.Attributes.Clear();
                btnSave.Attributes.Clear();
                BindGridView();
                if (lbID.Text.Trim() == "0")
                {
                    txtCust.Text = "";
                    txtPart.Text = "";
                    txtSku.Text = "";
                    txtQad.Text = "";
                    txtUL.Text = "";
                    txtStdDate.Text = "";
                    txtEndDate.Text = "";
                    txtComment.Text = ""; 
                    txtPartd.Text = "";
                }
                //else
                //{
                //    this.Redirect("CustPart.aspx?cust=" + txtCust.Text.Trim() + "&custPart=" + txtPart.Text.Trim() + "&QAD=" + txtQad.Text.Trim() + "&rt=" + DateTime.Now.ToFileTime().ToString());
                //}
            }
            else
            {

                ltlAlert.Text = "alert('" + param[11].Value.ToString().Trim() + "');";
                BindGridView();
            }
        }
        catch
        {
            ltlAlert.Text = "alert('保存失败！请刷新后重新操作一次！');";
            BindGridView();
        }
    }
    protected void btnback_Click(object sender, EventArgs e)
    {
        this.Redirect("CustPart.aspx?rt=" + DateTime.Now.ToFileTime().ToString());
    }
    protected void txtQad_TextChanged(object sender, EventArgs e)
    {
        lblchickUL.Text = string.Empty;
        string qad = txtQad.Text.Trim();
        if (qad.Length == 14)
        {
            if (qad.Substring(0, 1) == "1" && qad.Substring(qad.Length - 1) == "0")
            {
                try
                {
                    SqlParameter[] param = new SqlParameter[2];
                    param[0] = new SqlParameter("@part", qad.Trim());
                    param[1] = new SqlParameter("@retValue", SqlDbType.NVarChar, 50);
                    param[1].Direction = ParameterDirection.Output;
                    SqlHelper.ExecuteNonQuery(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_edi_checkULInf", param);

                    if (param[1].Value.ToString().Length == 0)
                    {
                        this.Alert("整灯未锁定！请联系技术部!");
                    }
                    else
                    {
                        lblchickUL.Text = param[1].Value.ToString();
                    }
                }
                catch
                {
                    lblchickUL.Text = string.Empty;

                }
            }
            //裸灯要验证UL
            //if (qad.Substring(0, 1) == "1" && qad.Substring(qad.Length - 1) == "1")
            if (qad.Substring(0, 1) == "1" )
            {
                try
                {
                    string strSql = "Select QAD_Data.dbo.fn_getULInfo('" + qad.Trim() + "')";
                    txtUL.Text = SqlHelper.ExecuteScalar(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.Text, strSql).ToString();

                    if (string.IsNullOrEmpty(txtUL.Text))
                    {
                        this.Alert("UL认证尚未通过！请联系技术部！");
                    }
                    else if (lblchickUL.Text.Trim() != txtPart.Text.Trim())
                    {
                        if (txtPart.Text.Trim() != string.Empty)
                        {
                            btnSave.Attributes.Add("onclick", "return confirm('整灯锁定信息客户物料号为" + lblchickUL.Text.Trim() + "!')");
                            btnupdate.Attributes.Add("onclick", "return confirm('整灯锁定信息客户物料号为" + lblchickUL.Text.Trim() + "!')");
                        }
                    }
                   
                }
                catch
                {
                    txtUL.Text = string.Empty;
                    this.Alert("UL获取失败！请联系管理员！");
                }
                 

            }
            else
            {
                txtUL.Text = string.Empty;
            }
        }
        else
        {
            this.Alert("QAD号长度是14位！");
        }
        if (lbID.Text.Trim() != "0")
        {
            if (txtQad.Text.Trim() != lblckickQAD.Text.Trim())
            {
                btnSave.Attributes.Add("onclick", "return confirm('物料号发生变化,确认保存么?')");
                btnupdate.Attributes.Add("onclick", "return confirm('物料号发生变化,确认新增么?')");
            }
            else
            {
                btnSave.Attributes.Clear();
                btnupdate.Attributes.Clear();
            }
        }
        ActiveControl();
       
    }

    public void ActiveControl()
    {
        if (txtCust.Text.Trim() != string.Empty)
        {
            if (txtPart.Text.Trim() != string.Empty)
            {
                if (txtPart.Text.Trim() != string.Empty)
                {
                    //txtPart.Focus();
                    //txtPart.Attributes.Add("ontextchanged", "document.getElementById('txtPart').focus(); ");
                }
                else
                {
                   // txtPart.Attributes.Add("ontextchanged", "document.getElementById('txtPart').focus(); ");
                }
            }
            else
            {
                txtPart.Focus();
            }
            
        }
    }
}
