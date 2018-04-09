using System;
using System.Data;
using System.Web.UI.WebControls;
using adamFuncs;
using Microsoft.ApplicationBlocks.Data;
using Wage;

public partial class wo_cost_wo_workRate : BasePage
{
    adamClass adam = new adamClass();
    HR hr_salary = new HR();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtYear.Text = DateTime.Now.Year.ToString();
            dropMonth.SelectedValue = Convert.ToString(DateTime.Now.Month);
            BindCompany();
            BindDept();
            DataGridBind();
        }
    }

    private void BindCompany()
    {
        ListItem item;
        string str = "SELECT plantID,description From tcpc0.dbo.plants where plantid <> 99 And isAdmin = 0 order by plantid ";
        DataTable dtCompany = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.Text, str).Tables[0];
        if (dtCompany.Rows.Count > 0)
        {
            for (int i = 0; i < dtCompany.Rows.Count; i++)
            {
                item = new ListItem(dtCompany.Rows[i].ItemArray[1].ToString(), dtCompany.Rows[i].ItemArray[0].ToString());
                dropCompany.Items.Add(item);
            }
        }
        dropCompany.SelectedValue = Convert.ToString(Session["plantcode"]);
    }

    private void BindDept()
    {

        ListItem item;
        item = new ListItem("--", "0");
        dropDept.Items.Add(item);

        DataTable dtDropDept = HR.GetDepartment(Convert.ToInt32(dropCompany.SelectedValue));
        if (dtDropDept.Rows.Count > 0)
        {
            for (int i = 0; i < dtDropDept.Rows.Count; i++)
            {
                item = new ListItem(dtDropDept.Rows[i].ItemArray[1].ToString(), dtDropDept.Rows[i].ItemArray[0].ToString());
                dropDept.Items.Add(item);
            }
        }
        dropDept.SelectedIndex = 0;
    }



    protected void dropCompany_TextChanged(object sender, EventArgs e)
    {
        dropDept.Items.Clear();
        BindDept();
        DataGridBind();
    }


    private void DataGridBind()
    {
        try
        {
            string strSql = "SELECT w.id,ISNULL(d.name,'') as department,w.workdate,ISNULL(w.workRate,0) as workRate  FROM wo_workrate w ";
            strSql = strSql + " LEFT OUTER JOIN tcpc" + dropCompany.SelectedValue + "..departments d ON d.departmentID = w.departmentID";
            strSql = strSql + " WHERE plantID='" + dropCompany.SelectedValue + "' And Year(workdate)='" + txtYear.Text.Trim() + "' And Month(workdate)='" + dropMonth.SelectedValue + "' ";
            if (dropDept.SelectedIndex > 0)
                strSql = strSql + " And w.departmentID ='" + dropDept.SelectedValue + "' ";

            strSql = strSql + " Order by w.departmentID ";

            DataSet ds = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.Text, strSql);
            dgWorkRate.DataSource = ds;
            dgWorkRate.DataBind();

            ds.Dispose();
        }
        catch
        {
            ;
        }
    }



    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        if (txtYear.Text.Trim() != String.Empty)
        {
            try
            {
                DateTime dd = Convert.ToDateTime(txtYear.Text + "-1-1");
            }
            catch
            {
                ltlAlert.Text = "alert('年月格式不正确!');";
                return;
            }
        }
        DataGridBind();
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        //验证
        if (dropDept.SelectedItem.Text == "--")
        {
            ltlAlert.Text = "alert('请选择部门!');";
            return;
        }

        if (txtYear.Text.Trim() == String.Empty)
        {
            ltlAlert.Text = "alert('年月不能为空!');";
            return;
        }
        else
        {
            try
            {
                DateTime dd = Convert.ToDateTime(txtYear.Text + "-1-1");
            }
            catch
            {
                ltlAlert.Text = "alert('年月格式不正确!');";
                return;
            }
        }

        if (txtRate.Text.Trim() == String.Empty)
        {
            ltlAlert.Text = "alert('预估工效不能为空!');";
            return;
        }
        else
        {
            try
            {
                Double dd = Convert.ToDouble(txtRate.Text);
            }
            catch
            {
                ltlAlert.Text = "alert('预估工效格式不正确!');";
                return;
            }
        }
        // End 验证

        bool blflag;
        try
        {
            string str = "IF EXists (SELECT * FROM wo_workrate Where plantID='" + dropCompany.SelectedValue + "' And Year(workdate)='" + txtYear.Text.Trim() + "' And Month(workdate)='" + dropMonth.SelectedValue + "' and departmentID='" + dropDept.SelectedValue + "')  Select 1 Else Select 0  ";

            blflag = Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.Text, str));

            if (!blflag)
            {
                string strDate = txtYear.Text.Trim() + "-" + dropMonth.SelectedValue + "-1";
                str = " INSERT INTO wo_workrate(workdate,plantID,departmentID, workRate,createdBy,createdDate) Values ('" + strDate + "','" + dropCompany.SelectedValue + "','" + dropDept.SelectedValue + "','" + txtRate.Text.Trim() + "','" + Convert.ToString(Session["plantcode"]) + "',getdate() ) ";

            }
            else
            {
                str = " Update wo_workrate SET workRate ='" + txtRate.Text.Trim() + "', modifiedBy='" + Session["uID"].ToString() + "' ,modifiedDate=getdate()  ";
                str = str + " Where  plantcode='" + dropCompany.SelectedValue + "' And Year(workdate)='" + txtYear.Text.Trim() + "' And Month(workdate)='" + dropMonth.SelectedValue + "' and departmentID='" + dropDept.SelectedValue + "' ";
            }

            SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.Text, str);

            ltlAlert.Text = "alert('保存成功！');";
            txtRate.Text = "";
            dropDept.SelectedIndex = 0;

            DataGridBind();
        }
        catch
        {
            ltlAlert.Text = "alert('操作有错误，请重新操作!');";
            return;
        }
    }



    protected void dgCenter_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        dgWorkRate.CurrentPageIndex = e.NewPageIndex;
        DataGridBind();
    }

    protected void dgCenter_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        try
        {
            string str = "DELETE FROM wo_workrate WHERE id='" + dgWorkRate.Items[e.Item.ItemIndex].Cells[0].Text + "'  ";

            SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.Text, str);

            ltlAlert.Text = "alert('删除成功!');";
            DataGridBind();

        }
        catch
        {
            ltlAlert.Text = "alert('删除错误，请重新操作!');";
            return;
        }
    }

}
