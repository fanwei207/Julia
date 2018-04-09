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

public partial class wo2_calendar : BasePage
{
    adamClass adam = new adamClass();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (this.Security["600010400"].isValid)//维护权限
            {
                if (!this.Security["600010401"].isValid)//查看权限
                {
                    chkView.Checked = false;
                    btnAdd.Visible = false;
                }
            }
            else
            {
                chkView.Checked = true;
            }

            for (int i = 2010; i <= DateTime.Now.Year; i++)
            {
                dropYear.Items.Insert(0, new ListItem(i.ToString() + "年", i.ToString()));
            }

            dropYear.Items.Insert(0, new ListItem("--", "0"));

            BindData();
        }
    }

    protected void BindData()
    {
        try
        {
            string strSql = "sp_wo2_selectCalendar";

            SqlParameter[] sqlParam = new SqlParameter[2];
            sqlParam[0] = new SqlParameter("@year", dropYear.SelectedValue);
            sqlParam[1] = new SqlParameter("@month", dropMonth.SelectedValue);

            DataTable table = SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strSql, sqlParam).Tables[0];

            DataList1.DataSource = table;
            DataList1.DataBind();
        }
        catch
        {
            ;
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        foreach (DataListItem item in DataList1.Items)
        {
            Label lbDate = (Label)item.FindControl("lbDate");
            CheckBox chkDate = (CheckBox)item.FindControl("chkDate");

            try
            {
                string strSql = "sp_wo2_insertCalendar";

                SqlParameter[] sqlParam = new SqlParameter[3];
                sqlParam[0] = new SqlParameter("@date", lbDate.Text);
                sqlParam[1] = new SqlParameter("@chk", chkDate.Checked);
                sqlParam[2] = new SqlParameter("@uName", Session["uName"].ToString());

                SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strSql, sqlParam);
            }
            catch
            {
                ;
            }
        }

        BindData();
    }

    protected void dropYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(dropYear.SelectedValue) != DateTime.Now.Year)
        {
            btnAdd.Enabled = false;
        }
        else
        {
            btnAdd.Enabled = true;
        }

        BindData();
    }

    protected void dropMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }

    protected void DataList1_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (!chkView.Checked)
        {
            CheckBox chkDate = (CheckBox)e.Item.FindControl("chkDate");

            chkDate.Enabled = false;
        }
    }
}
