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
using adamFuncs;
using Microsoft.ApplicationBlocks.Data;
using System.Text;

public partial class admin_access2 : System.Web.UI.Page
{
    adamClass chk = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["uid"] != null)
            {
                btnBack.Visible = true;
            }
            else
            {
                btnBack.Visible = false;
            }

            BindDeparts();
            BindUsers();
            BindMenuRoots();
            //20140922 fanwei remark
           // BindData();
        }
    }

    protected void BindData()
    {
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@userID", dropUsers.SelectedValue);
        param[1] = new SqlParameter("@parentID", dropMenuRoots.SelectedValue);

        DataSet ds = SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.StoredProcedure, "sp_admin_selectAccessRules", param);

        gvAccessRules.DataSource = ds;
        gvAccessRules.DataBind();

        ds.Dispose();
    }

    protected void BindDeparts()
    {
        string strSQL = "SELECT departmentID,name From departments where issalary=1 order by name";
        try
        {
            dropDeparts.Items.Clear();
            SqlDataReader reader = SqlHelper.ExecuteReader(chk.dsnx(), CommandType.Text, strSQL);
            while (reader.Read())
            {
                dropDeparts.Items.Add(new ListItem(reader["name"].ToString(), reader["departmentID"].ToString()));
            }
            reader.Close();
            reader.Dispose();
        }
        catch { }
        finally
        {
            dropDeparts.Items.Insert(0, new ListItem(" -- ", "0"));
            dropDeparts.SelectedIndex = 0;
        }
    }

    protected void BindUsers()
    {
        int uID = 0;
        if (Request.QueryString["uid"] != null)
        {
            try
            {
                uID = Convert.ToInt32(Request.QueryString["uid"]);
            }
            catch { }
            dropDeparts.Enabled = false;
            dropDeparts.SelectedIndex = 0;
        }

        dropMenuRoots.SelectedIndex = 0;

        string strSQL = string.Empty;

        if (uID > 0)
        {
            strSQL = "select userID, userName, userNo from tcpc0.dbo.Users where plantCode = " + Session["plantCode"].ToString() + " and leaveDate is null and deleted = 0 and isActive = 1 and isnull( roleID,0) <> 1 and userID = " + uID.ToString() + " and organizationID = " + Session["orgID"].ToString() + " Order by RTRIM(LTRIM(userno)) ";
        }
        else
        {
            strSQL = "SELECT userID,userName,userno From tcpc0.dbo.users Where plantCode= " + Session["plantCode"].ToString() + " and leaveDate is null and deleted=0 and leavedate is null and isactive=1 and isnull( roleID,0)<>1 And departmentID=" + dropDeparts.SelectedValue + " and organizationID=" + Session["orgID"].ToString() + " Order by RTRIM(LTRIM(userno))  ";
        }
        try
        {
            dropUsers.Items.Clear();
            SqlDataReader reader = SqlHelper.ExecuteReader(chk.dsn0(), CommandType.Text, strSQL);
            while (reader.Read())
            {
                dropUsers.Items.Add(new ListItem(reader["userName"].ToString() + "-" + reader["userNo"].ToString(), reader["userID"].ToString()));
            }
            reader.Close();
            reader.Dispose();
        }
        catch { }
        finally
        {
            dropUsers.Items.Insert(0, new ListItem(" -- ", "0"));
        }

        if (uID > 0)
        {
            dropUsers.SelectedValue = uID.ToString();
            dropUsers.Enabled = false;
        }
    }

    protected void BindMenuRoots()
    {
        string strSQL = "SELECT id,name,description,isMenu From tcpc0.dbo.Menu where parentID = 0 and sortOrder is not null and isDisable = 0 Order by sortOrder";
        try
        {
            SqlDataReader reader = SqlHelper.ExecuteReader(chk.dsn0(), CommandType.Text, strSQL);
            while (reader.Read())
            {
                dropMenuRoots.Items.Add(new ListItem(reader["name"].ToString() + "-" + reader["description"].ToString(), reader["id"].ToString()));
            }
        }
        catch { }
        finally
        {
            dropMenuRoots.Items.Insert(0, new ListItem(" -- ", "0"));
            dropMenuRoots.SelectedIndex = 0;
        }
    }

    protected void dropDeparts_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindUsers();
        gvAccessRules.Visible = false;
    }

    protected void gvAccessRules_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (gvAccessRules.Rows.Count > 0)
        {
            int access = 0, total = 0;
            foreach (GridViewRow row in gvAccessRules.Rows)
            {
                total++;
                if (gvAccessRules.DataKeys[row.RowIndex]["isAccessed"].ToString().ToLower() == "true")
                {
                    access++;
                }
            }
            lblCount.Text = access.ToString() + "/" + total.ToString();
        }
        else
        {
            lblCount.Text = "";
        }
    }

    protected void dropUsers_SelectedIndexChanged(object sender, EventArgs e)
    {
        checkDDLFull();
    }

    protected void dropMenuRoots_SelectedIndexChanged(object sender, EventArgs e)
    {
        checkDDLFull();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (Session["uID"] == null)
        {
            ltlAlert.Text = "alert('请重新登录！')";
        }
        else if (dropUsers.SelectedValue != "0" && gvAccessRules.Rows.Count > 0)
        {
            StringBuilder accessRules = new StringBuilder();
            bool originalStatus = false;
            bool currentStatus = false;
            foreach (GridViewRow row in gvAccessRules.Rows)
            {
                if (gvAccessRules.DataKeys[row.RowIndex]["isAccessed"].ToString().ToLower() == "true")
                {
                    originalStatus = true;
                }
                else
                {
                    originalStatus = false;
                }
                currentStatus = ((CheckBox)row.FindControl("chkAccess")).Checked;
                if (originalStatus != currentStatus)
                {
                    if (accessRules.Length <= 0)
                    {
                        accessRules.Append(gvAccessRules.DataKeys[row.RowIndex]["moduleID"].ToString());
                    }
                    else
                    {
                        accessRules.Append(";" + gvAccessRules.DataKeys[row.RowIndex]["moduleID"].ToString());
                    }
                }
            }
            if (accessRules.Length > 0)
            {
                #region 将获取到的权限ID号插入到临时表tempMenu中
                DataTable table = new DataTable("temp");
                DataRow row;
                DataColumn column;

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = "moduleID";
                table.Columns.Add(column);

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.Int32");
                column.ColumnName = "createdBy";
                table.Columns.Add(column);

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = "createdDate";
                table.Columns.Add(column);
                #endregion

                if (ClearTempMenu(Session["uID"].ToString()))
                {
                    foreach (string str in accessRules.ToString().Split(';'))
                    {
                        row = table.NewRow();
                        row["moduleID"] = str;
                        row["createdBy"] = Session["uID"].ToString();
                        row["createdDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        table.Rows.Add(row);
                    }
                    if (table.Rows.Count > 0)
                    {
                        using (SqlBulkCopy bulkCopy = new SqlBulkCopy(chk.dsn0()))
                        {
                            bulkCopy.DestinationTableName = "dbo.tempMenu";
                            bulkCopy.ColumnMappings.Add("moduleID", "moduleID");
                            bulkCopy.ColumnMappings.Add("createdBy", "createdBy");
                            bulkCopy.ColumnMappings.Add("createdDate", "createdDate");
                            try
                            {
                                bulkCopy.WriteToServer(table);
                            }
                            catch
                            {
                                ltlAlert.Text = "alert('保存失败！\\nFail to write to server.')";
                                return;
                            }
                            finally
                            {
                                table.Dispose();
                            }
                        }

                        try
                        {
                            SqlParameter[] param = new SqlParameter[3];
                            param[0] = new SqlParameter("@userID", dropUsers.SelectedValue);
                            param[1] = new SqlParameter("@uID", Session["uID"].ToString());
                            param[2] = new SqlParameter("@retValue", SqlDbType.Bit);
                            param[2].Direction = ParameterDirection.Output;

                            SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.StoredProcedure, "sp_admin_updateAccessRules", param);
                            if (Convert.ToBoolean(param[2].Value))
                            {
                                ltlAlert.Text = "alert('保存成功！')";
                            }
                            else
                            {
                                ltlAlert.Text = "alert('保存失败，请退出重试A！')";
                            }
                        }
                        catch
                        {
                            ltlAlert.Text = "alert('保存异常，请联系管理员！')";
                        }
                    }
                }
                else
                {
                    ltlAlert.Text = "alert('临时表清空失败！')";
                }
            }
            else
            {
                ltlAlert.Text = "alert('无需保存！该员工没有对权限项做修改！')";
            }
        }
        chkAll.Checked = false;
        checkDDLFull();
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect(chk.urlRand("/admin/personnellist1.aspx"));
    }

    protected bool ClearTempMenu(string uID)
    {
        string strSQL = "delete from tcpc0.dbo.tempMenu where createdBy = " + uID;
        try
        {
            SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQL);
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// 检查所有的ddl下拉菜单是否有值
    /// add 20140922 fanwei
    /// </summary>
    /// <returns></returns>
    private void checkDDLFull()
    {
       // if (dropDeparts.SelectedIndex != 0 && dropUsers.SelectedIndex != 0 && dropMenuRoots.SelectedIndex != 0)
        if (dropUsers.SelectedIndex != 0 && dropMenuRoots.SelectedIndex != 0)
        {
            BindData();
            gvAccessRules.Visible = true;
        }
        else
        {
            gvAccessRules.Visible = false;
           
        }
       
    
    }
}
