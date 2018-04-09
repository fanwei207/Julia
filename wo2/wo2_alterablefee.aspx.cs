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

public partial class wo2_alterablefee : BasePage
{
    adamClass adam = new adamClass();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!this.Security["600010301"].isValid)
            {
                btnAdd.Visible = false;
                gv.Columns[4].Visible = false;
                gv.Columns[5].Visible = false;
            }

            if (Convert.ToInt32(Session["PlantCode"].ToString()) == 1)
            {
                dropGroup.Items.Insert(0, new ListItem("整灯", "ZD"));
            }
            else if (Convert.ToInt32(Session["PlantCode"].ToString()) == 2)
            {
                dropGroup.Items.Insert(0, new ListItem("线路板", "PCB"));
                dropGroup.Items.Insert(0, new ListItem("毛管", "MG"));
                dropGroup.Items.Insert(0, new ListItem("整灯", "ZD"));
            }
            else
            {
                dropGroup.Items.Insert(0, new ListItem("直管", "ZH"));
                dropGroup.Items.Insert(0, new ListItem("明管", "MI"));
                dropGroup.Items.Insert(0, new ListItem("毛管", "MG"));
                dropGroup.Items.Insert(0, new ListItem("整灯", "ZD"));
            }

            BindFeeType();

            BindData();
        }
    }

    protected void BindData()
    {
        DataTable table = GetData();

        bool b = false;

        if (table.Rows.Count == 0)
        {
            table = table.Clone();
            table.Rows.Add(table.NewRow());

            b = true;
        }

        gv.DataSource = table;
        gv.DataBind();

        if (b)
        {
            int columnCount = gv.Rows[0].Cells.Count;
            gv.Rows[0].Cells.Clear();
            gv.Rows[0].Cells.Add(new TableCell());
            gv.Rows[0].Cells[0].ColumnSpan = columnCount;
            gv.Rows[0].Cells[0].Text = "没有数据";
            gv.Rows[0].Cells[0].Style.Add("text-align", "center");
        }
    }

    protected DataTable GetData()
    {
        try
        {
            string strSql = "sp_wo2_selectAlterableFee";

            SqlParameter[] sqlParam = new SqlParameter[3];
            sqlParam[0] = new SqlParameter("@group", dropGroup.SelectedValue);
            sqlParam[1] = new SqlParameter("@type", dropType.SelectedValue);
            sqlParam[2] = new SqlParameter("@effdate", txtEffdate.Text);

            return SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strSql, sqlParam).Tables[0];
        }
        catch
        {
            return null;
        }
    }

    protected void BindFeeType()
    {
        DataTable table = GetFeeType();

        dropType.DataSource = table;
        dropType.DataBind();

        dropType.Items.Insert(0, new ListItem("--", "0"));
    }

    protected DataTable GetFeeType()
    {
        try
        {
            string strSql = "sp_wo2_selectFeeType";
            SqlParameter[] sqlParam = new SqlParameter[1];
            sqlParam[0] = new SqlParameter("@type", "alterable");

            return SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strSql, sqlParam).Tables[0];
        }
        catch
        {
            return null;
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        gv.EditIndex = -1;
        if (btnSearch.Text == "取消")
        {
            btnAdd.Text = "新增";
            btnSearch.Text = "查询";
            txtEffdate.Text = string.Empty;
            txtAmt.Text = string.Empty;
        }
        BindData();
    }

    protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv.PageIndex = e.NewPageIndex;
        BindData();
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (dropType.SelectedIndex == 0)
        {
            ltlAlert.Text = "alert('请选择一项费用类别!');";
            return;
        }

        System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"^\d+(.\d+)?$");
        if (!regex.IsMatch(txtAmt.Text.Trim()))
        {
            ltlAlert.Text = "alert('费用必须是数字!');";
            return;
        }

        if (txtEffdate.Text == string.Empty)
        {
            ltlAlert.Text = "alert('生效日期不能为空!');";
            return;
        }
        else
        {
            try
            {
                DateTime _dt = Convert.ToDateTime(txtEffdate.Text);
            }
            catch
            {
                ltlAlert.Text = "alert('生效日期格式不正确!');";
                return;
            }
        }

        try
        {
            string strSql = "sp_wo2_insertAlterableFee";

            SqlParameter[] sqlParam = new SqlParameter[5];
            sqlParam[0] = new SqlParameter("@group", dropGroup.SelectedValue);
            sqlParam[1] = new SqlParameter("@type", dropType.SelectedValue);
            sqlParam[2] = new SqlParameter("@amt", txtAmt.Text.Trim());
            sqlParam[3] = new SqlParameter("@effdate", txtEffdate.Text);
            sqlParam[4] = new SqlParameter("@uName", Session["uName"].ToString());

            SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strSql, sqlParam);
        }
        catch
        {
            ;
        }

        txtEffdate.Text = string.Empty;

        btnAdd.Text = "新增";
        btnSearch.Text = "查询";

        BindData();
    }

    protected void gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string strSql = "sp_wo2_deleteAlterableFee";

            SqlParameter[] sqlParam = new SqlParameter[1];
            sqlParam[0] = new SqlParameter("@id", gv.DataKeys[e.RowIndex].Values["af_id"].ToString());

            SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strSql, sqlParam);
        }
        catch
        {
            ;
        }

        BindData();
    }

    protected void gv_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "myEdit")
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;

            dropType.SelectedIndex = -1;
            dropType.Items.FindByValue(gv.DataKeys[index].Values["af_type_id"].ToString()).Selected = true;

            txtAmt.Text = gv.Rows[index].Cells[1].Text;

            txtEffdate.Text = gv.Rows[index].Cells[2].Text;

            btnAdd.Text = "保存";
            btnSearch.Text = "取消";
        }
    }

    protected void dropType_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }

    protected void dropGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }
}
