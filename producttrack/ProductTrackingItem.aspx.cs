using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using adamFuncs;
using Microsoft.ApplicationBlocks.Data;
using System.Configuration;

public partial class producttrack_ProductTrackingItem : BasePage
{
    public static int id;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        { 
			
            dropTyepBind();
            dropDetail.Items.Insert(0, new ListItem("--", "-1"));
            DataBind();
        }

    }


    protected void dropTyepBind()
    {
        DataTable dt = SelectPtDropType();

        if (dt.Rows.Count > 0)
        {
            ListItem item;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                item = new ListItem(dt.Rows[i].ItemArray[0].ToString());
                dropType.Items.Add(item);
            }
            dropType.Items.Insert(0, new ListItem("--", "-1"));
        }
        else
        {
            dropType.Items.Insert(0, new ListItem("--", "-1"));
        }
    
    }


    protected void DataBind()
    {
        gvlist.DataSource = SelectPtItem(txtCode.Text.Trim(),txtQad.Text.Trim(),dropType.SelectedItem.Text,dropDetail.SelectedItem.Text);
        gvlist.DataBind();
    
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (dropType.SelectedIndex > 0)
        {
            if (dropDetail.SelectedIndex == 0)
            {
                ltlAlert.Text = "alert('请选择详细分类');";
                return;
            }
        }

        DataBind();
    }
    protected void dropType_changed(object sender, EventArgs e)
    {
        //dropDetail.Enabled = true;
        if (dropType.SelectedIndex > 0)
        {
            DataTable dt = SelectPtDropDetail(dropType.SelectedItem.Text);
            dropDetail.Items.Clear();

            if (dt.Rows.Count > 0)
            {
                ListItem item;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    item = new ListItem(dt.Rows[i].ItemArray[0].ToString());
                    dropDetail.Items.Add(item);
                }
                dropDetail.Items.Insert(0, new ListItem("--", "-1"));
            }
            else
            {
                dropDetail.Items.Insert(0, new ListItem("--", "-1"));
            }
        }

    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (dropType.SelectedIndex == 0)
        {
            ltlAlert.Text = "alert('Type不能为空');";
            return;
        }

        if (dropDetail.SelectedIndex == 0)
        {
            ltlAlert.Text = "alert('Detail不能为空');";
            return;
        }

        if (IsExistItem(txtCode.Text.Trim(), txtQad.Text.Trim(), dropType.SelectedItem.Text, dropDetail.SelectedItem.Text))
        {
            ltlAlert.Text = "alert('已存在相同的记录！');";
        }
        else
        {
           

            if (InsertOrModifyItem(txtCode.Text.Trim(), txtQad.Text.Trim(), dropType.SelectedItem.Text, dropDetail.SelectedItem.Text, Convert.ToInt32(Session["uID"]), Session["uName"].ToString(),id))
            {
                id = 0;
                dropDetail.SelectedIndex = 0;
                txtQad.Enabled = true;
                txtCode.Enabled = true;
                //dropDetail.Enabled = false;
                btnAdd.Text = "Add";
                DataBind();
                //ltlAlert.Text = "alert('操作成功！');";

            }
            else
            {
                ltlAlert.Text = "alert('操作失败！');";
            }
        
        }

    }

    protected void Edit_Command(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName.ToString() == "TrEdit")
        {
            //dropDetail.Enabled = true;
            int index = ((GridViewRow)(((LinkButton)(e.CommandSource)).Parent.Parent)).RowIndex;
            txtCode.Text = gvlist.Rows[index].Cells[0].Text.Trim().Replace("&nbsp;","");
            txtQad.Text = gvlist.Rows[index].Cells[1].Text.Trim();
            dropType.SelectedIndex = dropType.Items.IndexOf(dropType.Items.FindByText(gvlist.Rows[index].Cells[2].Text.Trim()));
            DataTable dt = SelectPtDropDetail(dropType.SelectedItem.Text);
            dropDetail.Items.Clear();

            if (dt.Rows.Count > 0)
            {
                ListItem item;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    item = new ListItem(dt.Rows[i].ItemArray[0].ToString());
                    dropDetail.Items.Add(item);
                }
                dropDetail.Items.Insert(0, new ListItem("--", "-1"));
            }
            else
            {
                dropDetail.Items.Insert(0, new ListItem("--", "-1"));
            }
            dropDetail.SelectedIndex = dropDetail.Items.IndexOf(dropDetail.Items.FindByText(gvlist.Rows[index].Cells[3].Text.Trim()));
            id = Convert.ToInt32(gvlist.DataKeys[index].Value.ToString());
            txtCode.Enabled = false;
            txtQad.Enabled = false;
            btnAdd.Text = "Save";
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txtCode.Enabled = true;
        txtQad.Enabled = true;
        btnAdd.Text = "Add";
        id = 0;
        txtCode.Text = string.Empty;
        txtQad.Text = string.Empty;
        dropType.SelectedIndex = 0;
        dropDetail.SelectedIndex = 0;
    }
    protected void gvlist_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvlist.PageIndex = e.NewPageIndex;
        DataBind();
    }

    protected void gvlist_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DeletePtItem(Convert.ToInt32(gvlist.DataKeys[e.RowIndex].Value.ToString()));
        DataBind();
    }
    protected void txtCode_TextChanged(object sender, EventArgs e)
    {
        SqlDataReader reader = SelectQad(txtCode.Text.Trim());
        if (reader.HasRows)
        {
            while (reader.Read())
            {
                txtQad.Text = reader["item_qad"].ToString();
            }
        }
        else
        {
            ltlAlert.Text = "alert('请手工输入QAD号！');";
            txtQad.Focus();
        }
    }

    public static DataTable SelectPtItem(string code, string qad, string type, string detail)
    {
        try
        {
            DataSet ds = null;
            string strName = "sp_SelectProductTrackingItem";
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@code", code);
            param[1] = new SqlParameter("@qad", qad);
            param[2] = new SqlParameter("@type", type);
            param[3] = new SqlParameter("@detail", detail);
            ds = SqlHelper.ExecuteDataset(ConfigurationManager.AppSettings["SqlConn.Conn_qaddoc"], CommandType.StoredProcedure, strName, param);
            return ds.Tables[0];
        }
        catch
        {
            return null;
        }

    }

    public static bool InsertOrModifyItem(string code, string qad, string type, string detail, int uID, string uName, int id)
    {
        try
        {
            string strName = "sp_InsertOrModifyProductTrackingItem";
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@code", code);
            param[1] = new SqlParameter("@qad", qad);
            param[2] = new SqlParameter("@type", type);
            param[3] = new SqlParameter("@detail", detail);
            param[4] = new SqlParameter("@uID", uID);
            param[5] = new SqlParameter("@uName", uName);
            param[6] = new SqlParameter("@id", id);
            param[7] = new SqlParameter("@retValue", SqlDbType.Bit);
            param[7].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(ConfigurationManager.AppSettings["SqlConn.Conn_qaddoc"], CommandType.StoredProcedure, strName, param);
            return Convert.ToBoolean(param[7].Value);

        }
        catch (Exception ex)
        {
            return false;
        }

    }

    public static bool IsExistItem(string code, string qad, string type, string detail)
    {
        try
        {
            string strName = "sp_CheckProductTrackingItem";
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@code", code);
            param[1] = new SqlParameter("@qad", qad);
            param[2] = new SqlParameter("@type", type);
            param[3] = new SqlParameter("@detail", detail);
            param[4] = new SqlParameter("@retValue", SqlDbType.Bit);
            param[4].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(ConfigurationManager.AppSettings["SqlConn.Conn_qaddoc"], CommandType.StoredProcedure, strName, param);
            return Convert.ToBoolean(param[4].Value);

        }
        catch (Exception ex)
        {
            return false;
        }

    }

    public static bool DeletePtItem(int id)
    {
        try
        {
            string strName = "sp_DeleteProductTrackingItem";
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@id", id);
            param[1] = new SqlParameter("@retValue", SqlDbType.Bit);
            param[1].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(ConfigurationManager.AppSettings["SqlConn.Conn_qaddoc"], CommandType.StoredProcedure, strName, param);
            return Convert.ToBoolean(param[1].Value);

        }
        catch (Exception ex)
        {
            return false;
        }

    }

    public static DataTable SelectPtDropType()
    {
        try
        {
            DataSet ds = null;
            string strName = "sp_SelectProductTrackingItemType";
            ds = SqlHelper.ExecuteDataset(ConfigurationManager.AppSettings["SqlConn.Conn_qaddoc"], CommandType.StoredProcedure, strName);
            return ds.Tables[0];
        }
        catch
        {
            return null;
        }

    }

    public static DataTable SelectPtDropDetail(string type)
    {
        try
        {
            DataSet ds = null;
            string strName = "sp_SelectProductTrackingItemDetail";
            SqlParameter param = new SqlParameter("@type", type);
            ds = SqlHelper.ExecuteDataset(ConfigurationManager.AppSettings["SqlConn.Conn_qaddoc"], CommandType.StoredProcedure, strName, param);
            return ds.Tables[0];
        }
        catch
        {
            return null;
        }

    }

    public static SqlDataReader SelectQad(string code)
    {
        try
        {
            SqlDataReader reader = null;
            string strName = "sp_SelectProductTrackingItemQad";
            SqlParameter param = new SqlParameter("@code", code);
            return reader = SqlHelper.ExecuteReader(ConfigurationManager.AppSettings["SqlConn.Conn_qaddoc"], CommandType.StoredProcedure, strName, param);
        }
        catch
        {
            return null;
        }

    }
}