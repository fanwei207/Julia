using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text;

public partial class price_pc_newPriceCheckBetweenQADAnd100 : BasePage
{
    PC_price pc = new PC_price();

   



    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {


            BindDDLType();
            
            this.Bind();
            

        }
    }



    private void BindDDLType()
    {
        ddlType.Items.Add(new ListItem("--", "0"));
        ddlType.Items.Add(new ListItem("非大宗商品", "1"));
        SqlDataReader sdr = pc.SelectType();
        while (sdr.Read())
        {
            ddlType.Items.Add(new ListItem(sdr["pcut_type"].ToString(), sdr["pcut_type"].ToString()));
        }
    }

    private void Bind()
    {
        string part  = txtQAD.Text.Trim().ToString();
        string vender = txtVender.Text.Trim().ToString();
        string venderName = txtVenderName.Text.Trim().ToString();
        string domain  = ddlDomain.SelectedItem.Text.Trim().ToString();
        string type = ddlType.SelectedItem.Value.Trim().ToString();
        string diff = ddlDiff.SelectedItem.Value.Trim().ToString();

        DataTable dt = pc.PriceCheckBetweenQadAnd100(part, vender, domain, type, diff, venderName);
        gvInfo.DataSource = dt;
        gvInfo.DataBind();
    }


    protected void gvInfo_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvInfo.PageIndex = e.NewPageIndex;
        Bind();
    }
    protected void gvInfo_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //制作颜色图例
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int gvIndex = e.Row.RowIndex;//获取行号

        

            bool isDiffPrimaryKey =Convert.ToBoolean(gvInfo.DataKeys[gvIndex].Values["isDiffPrimaryKey"].ToString());
            bool isDiffUM = Convert.ToBoolean(gvInfo.DataKeys[gvIndex].Values["isDiffUM"].ToString());
            bool isDiffCurr = Convert.ToBoolean(gvInfo.DataKeys[gvIndex].Values["isDiffCurr"].ToString());
            bool isDiffPrice = Convert.ToBoolean(gvInfo.DataKeys[gvIndex].Values["isDiffPrice"].ToString());
            bool isDiffAmt = Convert.ToBoolean(gvInfo.DataKeys[gvIndex].Values["isDiffAmt"].ToString());
            bool isDiffExpire = Convert.ToBoolean(gvInfo.DataKeys[gvIndex].Values["isDiffExpire"].ToString());
            /**
             如果 主键有问题，整条红色其他不显示
             * 其他字段有问题，显示不同的背景颜色
             */
            if (isDiffPrimaryKey)
            {
                e.Row.BackColor = System.Drawing.Color.Red;
            }
            else
            {
                if (isDiffUM)
                { 
                    e.Row.Cells[11].BackColor = System.Drawing.Color.LightGreen;
                    e.Row.Cells[12].BackColor = System.Drawing.Color.LightGreen;
                }

                if (isDiffCurr)
                {
                    e.Row.Cells[13].BackColor = System.Drawing.Color.LightBlue;
                    e.Row.Cells[14].BackColor = System.Drawing.Color.LightBlue;
                }

                if (isDiffPrice)
                {
                    e.Row.Cells[7].BackColor = System.Drawing.Color.Yellow;
                    e.Row.Cells[8].BackColor = System.Drawing.Color.Yellow;
                }

                if (isDiffAmt)
                {
                    e.Row.Cells[9].BackColor = System.Drawing.Color.Orange;
                    e.Row.Cells[10].BackColor = System.Drawing.Color.Orange;
                }

                if (isDiffExpire)
                {
                    e.Row.Cells[5].BackColor = System.Drawing.Color.Tomato;
                    e.Row.Cells[6].BackColor = System.Drawing.Color.Tomato;
                }
            }


        }
    }
    protected void btnSelect_Click(object sender, EventArgs e)
    {
        this.Bind();
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        string part = txtQAD.Text.Trim().ToString();
        string vender = txtVender.Text.Trim().ToString();
        string venderName = txtVenderName.Text.Trim().ToString();
        string domain = ddlDomain.SelectedItem.Text.Trim().ToString();
        string type = ddlType.SelectedItem.Value.Trim().ToString();
        string diff = ddlDiff.SelectedItem.Value.Trim().ToString();

        DataTable dt = pc.PriceCheckBetweenQadAnd100(part, vender, domain, type, diff, venderName);


        string title = "100^<b>100系统id</b>~^150^<b>QAD</b>~^100^<b>供应商</b>~^100^<b>供应商名称</b>~^" +
            "50^<b>域</b>~^150^<b>开始时间</b>~^150^<b>100系统价格终止</b>~^150^<b>QAD系统价格终止</b>~^" +
            "100^<b>100系统单位</b>~^100^<b>QAD系统单位</b>~^100^<b>100系统币种</b>~^100^<b>QAD系统币种</b>~^" +
            "100^<b>100系统价格</b>~^100^<b>QAD系统价格</b>~^100^<b>参考含税价</b>~^200^<b>100系统折扣表</b>~^200^<b>QAD系统折扣表</b>~^" +
            "150^<b>价格录入日期</b>~^250^<b>备注</b>~^50^<b>主键错误</b>~^50^<b>单位错误</b>~^" +
            "50^<b>货币错误</b>~^50^<b>价格错误</b>~^100^<b>价格表错误</b>~^100^<b>终止日期错误</b>~^";
        if (dt != null && dt.Rows.Count > 0)
        {
            ExportExcel(title, dt, false);
        }
    }
}