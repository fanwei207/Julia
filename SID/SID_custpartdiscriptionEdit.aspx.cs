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
using QADSID;

public partial class SID_SID_custpartdiscriptionEdit : System.Web.UI.Page
{
    SID sid = new SID();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request["id"] != null)
            {
                lbID.Text = Request["id"].ToString();
                Edit();
               

            }
        }
    }

    public void Edit()
    {
        btnSave.Text = "保存修改";
        btnback.Visible = true;
       
        SqlDataReader reader = sid.selectCustDiscription(lbID.Text.Trim());
        while (reader.Read())
        {
            txtCust.Text = reader["SID_cust"].ToString();
            txtPart.Text = reader["SID_partID"].ToString();
            txtHST.Text = reader["SID_HST"].ToString();
            txtdis.Text = reader["SID_description"].ToString();
            txtCust.Enabled = false;
            txtPart.Enabled = false;
          
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (txtCust.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('客户 不能为空!');";
            return;
        }

        if (txtPart.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('客户物料 不能为空!');";
            return;
        }
        //if (txtHST.Text.Trim().Length == 0)
        //{
        //    ltlAlert.Text = "alert('HST 不能为空!');";
        //    return;
        //}
        if (txtdis.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('描述 不能为空!');";
            return;
        }

        string value = sid.saveCustDiscription(lbID.Text.Trim(), txtCust.Text.Trim(), txtPart.Text.Trim(), txtHST.Text.Trim(), txtdis.Text.Trim(), Session["uID"].ToString(), Session["uName"].ToString());

          
             ltlAlert.Text = "alert('"+value+"');";
          
            if (value == "保存成功！")
            {
               ;
                if (lbID.Text.Trim() != "0")
                {
                    Response.Redirect("SID_custpartdiscriptionshow.aspx?id=" + lbID.Text.Trim() + "&cust=" + txtCust.Text.Trim() + "&part=" + txtPart.Text.Trim() + "&rt=" + DateTime.Now.ToFileTime().ToString());
                }
            }
          

    }
    protected void txtCust_TextChanged(object sender, EventArgs e)
    {
        if (txtCust.Text.Trim().ToUpper() != "TCP")
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
                    //txtCust.Text = string.Empty;
                    lbCustName.Text = string.Empty;

                   // ltlAlert.Text = "alert('客户 不存在！请确认代码输入是否正确！');";
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
            }
        }
    }
    protected void btnback_Click(object sender, EventArgs e)
    {
        Response.Redirect("SID_custpartdiscriptionshow.aspx?rt=" + DateTime.Now.ToFileTime().ToString());
    }
}