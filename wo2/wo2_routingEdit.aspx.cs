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
using WO2_RI;

public partial class wo2_wo2_routingEdit : BasePage
{
    adamClass chk = new adamClass();
    WO2_routingImport wo2 = new WO2_routingImport();

    protected void Page_Load(object sender, EventArgs e)
    {
        ltlAlert.Text = "";

        if (!IsPostBack)
        {
            BindData();
        }
    }

    protected void BindData()
    {
        DataTable dt = wo2.GetRouting(txbRouting.Text.Trim(), txbMop.Text.Trim());

        if (dt.Rows.Count == 0)
        {
            dt.Rows.Add(dt.NewRow());

        }



        gvRouting.DataSource = dt;
        gvRouting.DataBind();
    }

    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        BindData();
        txbRouting.Text = "";
        txbMop.Text = "";
        txbRun.Text = "";
    }

    protected void BtnAdd_Click(object sender, EventArgs e)
    {
        if (txbRouting.Text.Trim().Length < 14 || txbRouting.Text.Trim().Length > 15)
        {
            ltlAlert.Text = "alert('工艺输入错误！');";
            return;
        }

        if (wo2.IsMop(txbMop.Text.Trim()) <= 0)
        {
            ltlAlert.Text = "alert('大工序名称不存在!');";
            return;
        }

        if (txbMop.Text.Trim().Length > 0)
        {
            try
            {
                decimal _dc = Convert.ToDecimal(txbMop.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('大工序 只能为数字!');";
                return;
            }
        }

        if (!wo2.IsNumber(txbRun.Text.Trim()))
        {
            ltlAlert.Text = "alert('加工时间要为数字!');";
            return;
        }

        Int32 IErr = 0;
        IErr = wo2.InsertRouting(Convert.ToInt32(Session["uID"]), txbRouting.Text.Trim(), txbMop.Text.Trim(), Convert.ToDecimal(txbRun.Text.Trim()));
        if (IErr < 0)
        {
            ltlAlert.Text = "alert('增加失败，请检查新增工艺是否已存在！');";
            return;
        }
        else
        {
            BindData();
            txbRouting.Text = "";
            txbRun.Text = "";
            txbMop.Text = "";
        }
    }

    protected void gvRouting_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        for (int i = 0; i < gvRouting.Rows.Count; i++)
        {
            gvRouting.Rows[i].Cells[5].Attributes.Add("onclick", "return confirm('您确定删除吗？')");

        }

    }

    protected void gvRouting_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.ToString() == "Del1")
        {
            Int32 IErr = 0;
            IErr = wo2.DelRouting(Convert.ToInt32(Session["uID"]), Convert.ToInt32(gvRouting.DataKeys[Convert.ToInt32(e.CommandArgument)].Value.ToString().Trim()));
            if (IErr < 0)
            {
                ltlAlert.Text = "alert('删除失败！');";
                return;
            }
            else
            {
                BindData();
            }
        }

        if (e.CommandName.ToString() == "Edit1")
        {

            int index = int.Parse(e.CommandArgument.ToString());
            txbRouting.Text = gvRouting.Rows[index].Cells[0].Text.Trim();
            txbMop.Text = gvRouting.Rows[index].Cells[1].Text.Trim();
            txbRun.Text = gvRouting.Rows[index].Cells[3].Text.Trim();

            LblRID.Text = gvRouting.DataKeys[index].Value.ToString().Trim();

        }
    }
    protected void gvRouting_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvRouting.PageIndex = e.NewPageIndex;
        BindData();
    }

    protected void BtnModify_Click(object sender, EventArgs e)
    {
        if (LblRID.Text.Trim().Length <= 0)
        {
            ltlAlert.Text = "alert('请选择要修改的工艺！');";
            return;
        }
        else
        {
            if (txbRouting.Text.Trim().Length < 14 || txbRouting.Text.Trim().Length > 15)
            {
                ltlAlert.Text = "alert('工艺输入错误！');";
                return;
            }

            if (wo2.IsMop(txbMop.Text.Trim()) <= 0)
            {
                ltlAlert.Text = "alert('大工序名称不存在!');";
                return;
            }

            if (txbMop.Text.Trim().Length > 0)
            {
                try
                {
                    decimal _dc = Convert.ToDecimal(txbMop.Text.Trim());
                }
                catch
                {
                    ltlAlert.Text = "alert('大工序 只能为数字!');";
                    return;
                }
            }

            if (!wo2.IsNumber(txbRun.Text.Trim()))
            {
                ltlAlert.Text = "alert('加工时间要为数字!');";
                return;
            }

            Int32 IErr = 0;
            IErr = wo2.EditRouting(Convert.ToInt32(Session["uID"]), Convert.ToInt32(LblRID.Text.Trim()), txbRouting.Text.Trim(), txbMop.Text.Trim(), Convert.ToDecimal(txbRun.Text.Trim()));
            if (IErr < 0)
            {
                ltlAlert.Text = "alert('修改失败！');";
                return;
            }
            else
            {
                BindData();
                txbMop.Text = "";
                txbRouting.Text = "";
                txbRun.Text = "";
                LblRID.Text = "";
            }
        }
    }
}
