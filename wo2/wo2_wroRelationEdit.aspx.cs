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
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;

public partial class wo2_wo2_wroRelationEdit : BasePage
{
    adamClass chk = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindWo2Mop();
            if (Request.QueryString["wroID"] != null)
            {
                string wro_id = Request.QueryString["wroID"].ToString();
                txbParent.Enabled = false;
                txbParentName.Enabled = false;
                lblWroID.Text = wro_id;
                GetCurrentWro(wro_id);
            }
            else
            {
                lblWroID.Text = string.Empty;
            }
        }
    }

    protected void GetCurrentWro(string wroID)
    {
        try
        {
            string strSQL = "select wro_parent, wro_parentName, wro_child from wo2_relation where wro_id = " + wroID;
            SqlDataReader reader = SqlHelper.ExecuteReader(chk.dsnx(), CommandType.Text, strSQL);
            while (reader.Read())
            {
                txbParent.Text = reader["wro_parent"].ToString();
                txbParentName.Text = reader["wro_parentName"].ToString();
                dropChild.SelectedValue = "0";
            }
            if (reader != null)
            {
                reader.Close();
            }
        }
        catch
        { }
    }

    /// <summary>
    /// 绑定子工序
    /// </summary>
    protected void BindWo2Mop()
    {
        dropChild.Items.Clear();
        try
        {
            string strSQL = "select wo2_mop_proc, wo2_mop_procname from wo2_mop order by wo2_mop_proc";
            SqlDataReader reader = SqlHelper.ExecuteReader(chk.dsn0(), CommandType.Text, strSQL);

            while (reader.Read())
            {
                dropChild.Items.Add(new ListItem(reader["wo2_mop_procname"].ToString(), reader["wo2_mop_proc"].ToString()));
            }
            if (reader != null)
            {
                reader.Close();
            }
        }
        catch
        { }
        finally
        {
            dropChild.Items.Insert(0, new ListItem("--请选择一个子工序--", "0"));
        }
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        Response.Redirect("/wo2/wo2_wroRelation.aspx?rt=" + DateTime.Now.ToString());
    }

    protected bool IsInterger(string val)
    {
        try
        {
            int n = Convert.ToInt32(val);
            if (n > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch
        {
            return false;
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (Session["uID"] == null || lblWroID.Text == string.Empty)
        {
            ltlAlert.Text = "alert('请重新登录！')";
        }
        else
        {
            if (txbParent.Text.Trim() == string.Empty)
            {
                ltlAlert.Text = "alert('父工序不能为空！')";
            }
            else if (!IsInterger(txbParent.Text.Trim()))
            {
                ltlAlert.Text = "alert('父工序必须是大于0的整数！')";
            }
            else if (txbParentName.Text.Trim() == string.Empty)
            {
                ltlAlert.Text = "alert('父工序名称不能为空！')";
            }
            else if (dropChild.SelectedValue == "0")
            {
                ltlAlert.Text = "alert('请选择一个子工序！')";
            }
            else
            {
                try
                {
                    SqlParameter[] param = new SqlParameter[7];
                    param[0] = new SqlParameter("@parentCode", txbParent.Text.Trim());
                    param[1] = new SqlParameter("@parentName", txbParentName.Text.Trim());
                    param[2] = new SqlParameter("@childCode", dropChild.SelectedValue);
                    param[3] = new SqlParameter("@childName", dropChild.SelectedItem.Text);
                    param[4] = new SqlParameter("@createdBy", Convert.ToInt32(Session["uID"]));
                    param[5] = new SqlParameter("@createdName", Session["uName"]);
                    param[6] = new SqlParameter("@retValue", DbType.Boolean);
                    param[6].Direction = ParameterDirection.Output;

                    SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.StoredProcedure, "sp_insertWo2Relation", param);
                    int retValue = Convert.ToInt32(param[6].Value);
                    if (retValue == 1)
                    {
                        ltlAlert.Text = "alert('新增成功！');window.close()";
                    }
                    else if (retValue == -1)
                    {
                        ltlAlert.Text = "alert('父工序：" + txbParent.Text.Trim() + " 不能与任何的子工序一致！')";
                    }
                    else if (retValue == -2)
                    {
                        ltlAlert.Text = "alert('父工序：" + txbParent.Text.Trim() + "的名称：" + txbParentName.Text.Trim() + " 不正确！')";
                    }
                    else if (retValue == -3)
                    {
                        ltlAlert.Text = "alert('父工序名称：" + txbParentName.Text.Trim() + "所对应的父工序：" + txbParent.Text.Trim() + " 不正确！')";
                    }
                    else if (retValue == -4)
                    {
                        ltlAlert.Text = "alert('父工序：" + txbParent.Text.Trim() + "--子工序：" + dropChild.SelectedItem.Text + " 已存在！')";
                    }
                    else if (retValue == 0)
                    {
                        ltlAlert.Text = "alert('新增失败，请重试！')";
                    }
                }
                catch
                {
                    ltlAlert.Text = "alert('新增失败，请重试！')";
                }
            }
        }
    }
}
