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

public partial class wo2_wo2_wroRelation : BasePage
{
    adamClass chk = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindWo2Mop();
            BindData();
        }
    }

    protected void BindData()
    {
        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@parentCode", txbParent.Text.Trim());
        param[1] = new SqlParameter("@parentName", txbParentName.Text.Trim());
        param[2] = new SqlParameter("@chidCode", dropChild.SelectedValue);

        DataSet ds = SqlHelper.ExecuteDataset(chk.dsnx(), CommandType.StoredProcedure, "sp_selectWo2Relation", param);
        gvWo2Relation.DataSource = ds.Tables[0];
        gvWo2Relation.DataBind();
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
        }
        catch
        { }
        finally
        {
            dropChild.Items.Insert(0, new ListItem("--请选择一个子工序--", "0"));
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
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

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (Session["uID"] == null)
        {
            ltlAlert.Text = "alert('请重新登录！')";
        }
        else
        {
            if (btnAdd.Text.Trim() == "新增")
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
                            txbParent.Text = string.Empty;
                            txbParentName.Text = string.Empty;
                            dropChild.SelectedValue = "0";
                            BindData();
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
            else if (btnAdd.Text.Trim() == "保存")
            {
                try
                {
                    int retValue = UpdateParentName(txbParent.Text.Trim(), txbParentName.Text.Trim());


                    if (retValue == 1 || retValue == -2)
                    {
                        txbParent.Text = string.Empty;
                        txbParentName.Text = string.Empty;

                        txbParent.Enabled = true;

                        dropChild.SelectedValue = "0";

                        dropChild.Enabled = true;

                        BindData();
                    }
                    else if (retValue == -1)
                    {
                        ltlAlert.Text = "alert('父工序：" + txbParent.Text.Trim() + " 不存在！')";
                    }
                    else if (retValue == 0)
                    {
                        ltlAlert.Text = "alert('保存失败，请重试！')";
                    }
                }
                catch
                {
                    ltlAlert.Text = "alert('保存失败，请重试！')";
                }
            }
        }
    }

    protected void gvWo2Relation_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "myAdd")
        {
            Response.Redirect("/wo2/wo2_wroRelationEdit.aspx?wroID=" + e.CommandArgument.ToString().Trim() + "&rm=" + DateTime.Now.ToString());
        }
        else if (e.CommandName == "myDelete")
        {
            try
            {
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@wroID", e.CommandArgument.ToString());
                param[1] = new SqlParameter("@retValue", DbType.Boolean);
                param[1].Direction = ParameterDirection.Output;
                SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.StoredProcedure, "sp_deleteWo2Relation", param);
                if (Convert.ToBoolean(param[1].Value))
                {
                    BindData();
                }
                else
                {
                    ltlAlert.Text = "alert('删除失败，请重试！')";
                }
            }
            catch
            {
                ltlAlert.Text = "alert('删除失败，请重试！')";
            }
        }
        else if (e.CommandName == "myEdit")
        {
            SqlDataReader reader = GetParentName(e.CommandArgument.ToString());
            if (reader.Read())
            {
                txbParent.Text = reader["wro_parent"].ToString();
                txbParentName.Text = reader["wro_parentName"].ToString();

                btnAdd.Text = "保存";
                txbParent.Enabled = false;
                dropChild.Enabled = false;
            }
        }
    }

    protected void gvWo2Relation_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvWo2Relation.PageIndex = e.NewPageIndex;
        BindData();
    }

    private int UpdateParentName(string code, string name)
    {
        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@parentCode", code);
        param[1] = new SqlParameter("@parentName", name);
        param[2] = new SqlParameter("@retValue", DbType.Boolean);
        param[2].Direction = ParameterDirection.Output;

        SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.StoredProcedure, "sp_updateWo2RelationParentName", param);

        return Convert.ToInt32(param[2].Value);
    }

    private SqlDataReader GetParentName(string code)
    {
        SqlParameter param = new SqlParameter("@parentCode", code);
        return SqlHelper.ExecuteReader(chk.dsnx(), CommandType.StoredProcedure, "sp_selectWo2RelationParentName", param);
    }
}
