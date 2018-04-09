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
    /// ���ӹ���
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
            dropChild.Items.Insert(0, new ListItem("--��ѡ��һ���ӹ���--", "0"));
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
            ltlAlert.Text = "alert('�����µ�¼��')";
        }
        else
        {
            if (txbParent.Text.Trim() == string.Empty)
            {
                ltlAlert.Text = "alert('��������Ϊ�գ�')";
            }
            else if (!IsInterger(txbParent.Text.Trim()))
            {
                ltlAlert.Text = "alert('����������Ǵ���0��������')";
            }
            else if (txbParentName.Text.Trim() == string.Empty)
            {
                ltlAlert.Text = "alert('���������Ʋ���Ϊ�գ�')";
            }
            else if (dropChild.SelectedValue == "0")
            {
                ltlAlert.Text = "alert('��ѡ��һ���ӹ���')";
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
                        ltlAlert.Text = "alert('�����ɹ���');window.close()";
                    }
                    else if (retValue == -1)
                    {
                        ltlAlert.Text = "alert('������" + txbParent.Text.Trim() + " �������κε��ӹ���һ�£�')";
                    }
                    else if (retValue == -2)
                    {
                        ltlAlert.Text = "alert('������" + txbParent.Text.Trim() + "�����ƣ�" + txbParentName.Text.Trim() + " ����ȷ��')";
                    }
                    else if (retValue == -3)
                    {
                        ltlAlert.Text = "alert('���������ƣ�" + txbParentName.Text.Trim() + "����Ӧ�ĸ�����" + txbParent.Text.Trim() + " ����ȷ��')";
                    }
                    else if (retValue == -4)
                    {
                        ltlAlert.Text = "alert('������" + txbParent.Text.Trim() + "--�ӹ���" + dropChild.SelectedItem.Text + " �Ѵ��ڣ�')";
                    }
                    else if (retValue == 0)
                    {
                        ltlAlert.Text = "alert('����ʧ�ܣ������ԣ�')";
                    }
                }
                catch
                {
                    ltlAlert.Text = "alert('����ʧ�ܣ������ԣ�')";
                }
            }
        }
    }
}
