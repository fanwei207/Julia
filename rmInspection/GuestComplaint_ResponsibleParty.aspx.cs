using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class rmInspection_GuestComplaint_ResponsibleParty : System.Web.UI.Page
{
    string strconn = ConfigurationManager.AppSettings["SqlConn.rmInspection"];
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            BindData();
            //txtEndtime.Enabled = false;
        }
    }

    private void BindData()
    {
        gv.DataSource = SelectDuty(txtDuty.Text.Trim(),txtResponsiblePerson.Text.Trim());
        gv.DataBind();
    }

    private DataSet SelectDuty(string dutyname,string responsiblename)
    {
        string str = "sp_selectResponsibleParty";
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@dutyname", dutyname);
        param[1] = new SqlParameter("@responsiblename", responsiblename);
        

        return SqlHelper.ExecuteDataset(strconn, CommandType.StoredProcedure, str, param); 
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        if (txtResponsiblePerson.Text.Trim() == "")
        {
            lblResponsiblePersonId.Value = "";
        }
        string responsiblePersonId = lblResponsiblePersonId.Value;
       // string handlepersonId = lblDuty.Value;
        if (btnNew.Text == "新增")
        {
            
            if (txtDuty.Text.Trim().Length <= 0)
            {
                ltlAlert.Text = "alter('请输入责任方');";
                return;
            }
            if (txtResponsiblePerson.Text.Trim() == "")
            {
                ltlAlert.Text = "alert('请添加责任人！');";
                return;
            }
            

            string strSql = "sp_insertResponsibleParty";
            SqlParameter[] sqlParam = new SqlParameter[5];
            sqlParam[0] = new SqlParameter("@dutyname", txtDuty.Text.Trim());
            sqlParam[1] = new SqlParameter("@responsiblePersonId", responsiblePersonId);
            sqlParam[2] = new SqlParameter("@uID", Session["uID"]);
            sqlParam[3] = new SqlParameter("@uName", Session["uName"]);
            sqlParam[4] = new SqlParameter("@retValue", SqlDbType.Bit);
            sqlParam[4].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(strconn, CommandType.StoredProcedure, strSql, sqlParam);

            if (Convert.ToBoolean(sqlParam[4].Value))
            {
                ltlAlert.Text = "alert('此类别已存在')";
            }
            txtDuty.Text = "";
            txtResponsiblePerson.Text = "";
            BindData();
        }
        else if (btnNew.Text == "保存")
        {
            //txtEndtime.Enabled = true;
            string id = lblId.Text;

            string strSql = "sp_updateResponsibleParty";
            SqlParameter[] sqlParam = new SqlParameter[7];
            sqlParam[0] = new SqlParameter("@id",id);
            //sqlParam[0] = new SqlParameter("@dutyid", dutyid);
            sqlParam[1] = new SqlParameter("@dutyname", txtDuty.Text.Trim().ToString());
            sqlParam[2] = new SqlParameter("@responsiblePerson", responsiblePersonId);
            //sqlParam[3] = new SqlParameter("@endtime", txtEndtime.Text.Trim().ToString());
            sqlParam[3] = new SqlParameter("@uID", Session["uID"]);
            sqlParam[4] = new SqlParameter("@uName", Session["uName"]);
            sqlParam[5] = new SqlParameter("@retValue", SqlDbType.Bit);
            sqlParam[5].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(strconn, CommandType.StoredProcedure, strSql, sqlParam);

            if (Convert.ToBoolean(sqlParam[5].Value))
            {
                ltlAlert.Text = "alert('此类别已存在!');";
                return;
            }
        }
        txtDuty.Text = "";
        txtResponsiblePerson.Text = "";
        
        BindData();
        //txtEndtime.Text = "";
        btnNew.Text = "新增";
        }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        BindData();
    }

    protected void btnResponsiblePerson_Click(object sender, EventArgs e)
    {
        ltlAlert.Text = "var w=window.open('/admin/DeptLineSelectUser.aspx?type=6&selectedId=" + lblResponsiblePersonId.Value + "&selectedName=" + txtResponsiblePerson.Text.Trim() + "','','menubar=No,scrollbars = No,resizable=No,width=500,height=500,top=200,left=300'); w.focus();";
    }
    protected void gv_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        gv.CurrentPageIndex = e.NewPageIndex;
        BindData();
    }
    protected void gv_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "UpdateBtn")
        {
            lblId.Text = gv.DataKeys[e.Item.ItemIndex].ToString();
            txtDuty.Text = e.Item.Cells[0].Text.Replace("<br/>", ";").Replace("&nbsp;", ""); ;
            //ddlModule.Items.FindByText(e.Item.Cells[0].Text).Selected = true;
            txtResponsiblePerson.Text = e.Item.Cells[1].Text.Replace("<br/>", ";").Replace("&nbsp;", "");            
            //txtEndtime.Text = e.Item.Cells[4].Text.Replace("<br/>", ";").Replace("&nbsp;", "");
            lblResponsiblePersonId.Value = e.Item.Cells[2].Text;
            //btnHandlePerson.Enabled = true;
            //txtEndtime.Enabled = true;
            
            btnNew.Text = "保存";
        }
        else if (e.CommandName == "DeleteBtn")
        {
            string id = gv.DataKeys[e.Item.ItemIndex].ToString();
            if (DeleteHandlePersons(id))
            {
                BindData();
                ltlAlert.Text = "alert('删除成功！');";
            }
            else
            {
                ltlAlert.Text = "alert('删除失败！');";
            }
        }
        else if (e.CommandName == "CancelBtn")
        {
            txtDuty.Text = "";
            txtResponsiblePerson.Text = "";
            BindData();
        }
    }

    private bool DeleteHandlePersons(string id)
    {
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@id", id);

        if (SqlHelper.ExecuteNonQuery(strconn, CommandType.StoredProcedure, "sp_deleteResponsibleParty", param) > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}