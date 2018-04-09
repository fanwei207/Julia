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

public partial class wsline_wl_woTrackReport2 : BasePage
{
    adamClass chk = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //初始化显示两天前的
            txtWoCompDate1.Text = String.Format("{0:yyyy-MM-dd}", DateTime.Today.AddDays(-2));
            txtWoCompDate2.Text = String.Format("{0:yyyy-MM-dd}", DateTime.Today);

            BindPlants();
            BindData();
        }
    }

    protected void BindPlants()
    {
        string strSQL = "SELECT plantID,description From Plants where isAdmin=0 and plantID in(1, 2, 5, 8) order by plantID";
        dropPlants.Items.Clear();

        try
        {
            SqlDataReader reader = SqlHelper.ExecuteReader(chk.dsn0(), CommandType.Text, strSQL);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    dropPlants.Items.Add(new ListItem(reader["description"].ToString(), reader["plantID"].ToString()));
                }
                reader.Close();
            }
        }
        catch
        { }
    }

    protected DataSet GetData()
    {
        try
        {
            string strPalntCode = dropPlants.SelectedIndex == -1 ? Session["plantCode"].ToString() : dropPlants.SelectedValue;

            SqlParameter[] param = new SqlParameter[15];
            param[0] = new SqlParameter("@nbr", txbNbr.Text.Trim());
            param[1] = new SqlParameter("@pcbBeginDate", pcbBeginDate.Text.Trim());
            param[2] = new SqlParameter("@pcbEndDate", pcbEndDate.Text.Trim());
            param[3] = new SqlParameter("@mgBeginDate", mgBeginDate.Text.Trim());
            param[4] = new SqlParameter("@mgEndDate", mgEndDate.Text.Trim());
            param[5] = new SqlParameter("@onlineBeginDate", onlineBeginDate.Text.Trim());
            param[6] = new SqlParameter("@onlineEndDate", onlineEndDate.Text.Trim());
            param[7] = new SqlParameter("@offlineBeginDate", offlineBeginDate.Text.Trim());
            param[8] = new SqlParameter("@offlineEndDate", offlineEndDate.Text.Trim());
            param[9] = new SqlParameter("@plantCode", strPalntCode);
            param[10] = new SqlParameter("@wo_due_date1", txtWoDueDate1.Text.Trim());
            param[11] = new SqlParameter("@wo_due_date2", txtWoDueDate2.Text.Trim());
            param[12] = new SqlParameter("@wo_comp_date1", txtWoCompDate1.Text.Trim());
            param[13] = new SqlParameter("@wo_comp_date2", txtWoCompDate2.Text.Trim());

            return SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.StoredProcedure, "sp_wo2_selectWoTrackReport", param);
        }
        catch
        {
            return null;
        }
    }

    protected void BindData()
    {
        DataSet ds = this.GetData();

        gvWoTrackReports.DataSource = ds.Tables[0];
        gvWoTrackReports.DataBind();
        gvWoTrackReports.PageIndex = 0;

        if (ds != null)
        {
            ds.Dispose();
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
    }

    protected void gvWoTrackReports_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void gvWoTrackReports_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvWoTrackReports.PageIndex = e.NewPageIndex;
        BindData();
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        DataSet ds = this.GetData();

        if (ds.Tables[0].Rows.Count == 0)
        {
            ltlAlert.Text = "alert('没有要导出的内容！')";
        }
        else
        {
            string ExTitle = "<b>域</b>~^<b>地点</b>~^<b>工单号</b>~^<b>工单ID</b>~^<b>下达日期</b>~^<b>工单状态</b>~^工单类型</b>~^<b>工单数</b>~^<b>完工数</b>~^<b>入库数</b>~^<b>入库日期</b>~^<b>部件号</b>~^<b>线路板发料</b>~^<b>线路板发料汇报人</b>~^<b>毛管发料</b>~^<b>毛管发料汇报人</b>~^<b>上线</b>~^<b>上线汇报人</b>~^<b>质检下线</b>~^<b>质检汇报人</b>~^<b>下线</b>~^<b>下线汇报人</b>~^";
            ExTitle += "<b>(1008A)胶灯管准备</b>~^<b>(1008B)胶灯管准备</b>~^<b>(1008C)胶灯管准备</b>~^<b>(1008D)胶灯管准备</b>~^<b>(1010A)胶灯管</b>~^<b>(1010B)胶灯管</b>~^<b>(1010C)胶灯管</b>~^<b>(1010D)胶灯管</b>~^<b>(1020A)灯头准备</b>~^<b>(1020B)灯头准备</b>~^<b>(1020C)灯头准备</b>~^<b>(1020D)灯头准备</b>~^<b>(1030A)组装</b>~^<b>(1030B)组装</b>~^<b>(1030C)组装</b>~^<b>(1030D)组装</b>~^<b>(1040A)胶罩</b>~^<b>(1040B)胶罩</b>~^<b>(1040C)胶罩</b>~^<b>(1040D)胶罩</b>~^<b>(1090A)包装</b>~^<b>(1090B)包装</b>~^<b>(1090C)包装</b>~^<b>(1090D)包装</b>~^<b>(2010A)检明管</b>~^<b>(2010B)检明管</b>~^<b>(2010C)检明管</b>~^<b>(2010D)检明管</b>~^<b>(2020A)洗明管</b>~^<b>(2020B)洗明管</b>~^<b>(2020C)洗明管</b>~^<b>(2020D)洗明管</b>~^<b>(2030A)涂粉</b>~^<b>(2030B)涂粉</b>~^<b>(2030C)涂粉</b>~^<b>(2030D)涂粉</b>~^<b>(2040A)绷丝</b>~^<b>(2040B)绷丝</b>~^<b>(2040C)绷丝</b>~^<b>(2040D)绷丝</b>~^<b>(2045A)手工封口</b>~^<b>(2045B)手工封口</b>~^<b>(2045C)手工封口</b>~^<b>(2045D)手工封口</b>~^<b>(2050A)封口排气</b>~^<b>(2050B)封口排气</b>~^<b>(2050C)封口排气</b>~^<b>(2050D)封口排气</b>~^<b>(2060A)灯管总检</b>~^<b>(2060B)灯管总检</b>~^<b>(2060C)灯管总检</b>~^<b>(2060D)灯管总检</b>~^<b>(3010A)直管生产</b>~^<b>(3010B)直管生产</b>~^<b>(3010C)直管生产</b>~^<b>(3010D)直管生产</b>~^<b>(3090A)直管包装</b>~^<b>(3090B)直管包装</b>~^<b>(3090C)直管包装</b>~^<b>(3090D)直管包装</b>~^<b>(4005A)割管</b>~^<b>(4005B)割管</b>~^<b>(4005C)割管</b>~^<b>(4005D)割管</b>~^<b>(4010A)弯管</b>~^<b>(4010B)弯管</b>~^<b>(4010C)弯管</b>~^<b>(4010D)弯管</b>~^<b>(4020A)弯脚</b>~^<b>(4020B)弯脚</b>~^<b>(4020C)弯脚</b>~^<b>(4020D)弯脚</b>~^<b>(4030A)割脚</b>~^<b>(4030B)割脚</b>~^<b>(4030C)割脚</b>~^<b>(4030D)割脚</b>~^<b>(4040A)退火</b>~^<b>(4040B)退火</b>~^<b>(4040C)退火</b>~^<b>(4040D)退火</b>~^<b>(4050A)烘口</b>~^<b>(4050B)烘口</b>~^<b>(4050C)烘口</b>~^<b>(4050D)烘口</b>~^<b>(4060A)吹泡</b>~^<b>(4060B)吹泡</b>~^<b>(4060C)吹泡</b>~^<b>(4060D)吹泡</b>~^<b>(4080A)检验</b>~^<b>(4080B)检验</b>~^<b>(4080C)检验</b>~^<b>(4080D)检验</b>~^<b>(5020A)插件</b>~^<b>(5020B)插件</b>~^<b>(5020C)插件</b>~^<b>(5020D)插件</b>~^<b>(5030A)二波</b>~^<b>(5030B)二波</b>~^<b>(5030C)二波</b>~^<b>(5030D)二波</b>~^<b>(5040A)总检</b>~^<b>(5040B)总检</b>~^<b>(5040C)总检</b>~^<b>(5040D)总检</b>~^<b>(6020A)喷码</b>~^<b>(6020B)喷码</b>~^<b>(6020C)喷码</b>~^<b>(6020D)喷码</b>~^<b>(6030A)贴片（SMT）</b>~^<b>(6030B)贴片（SMT）</b>~^<b>(6030C)贴片（SMT）</b>~^<b>(6030D)贴片（SMT）</b>~^<b>(6040A)机插针/敲铆钉</b>~^<b>(6040B)机插针/敲铆钉</b>~^<b>(6040C)机插针/敲铆钉</b>~^<b>(6040D)机插针/敲铆钉</b>~^<b>(6050A)机插（AI）</b>~^<b>(6050B)机插（AI）</b>~^<b>(6050C)机插（AI）</b>~^<b>(6050D)机插（AI）</b>~^<b>(6060A)手插件</b>~^<b>(6060B)手插件</b>~^<b>(6060C)手插件</b>~^<b>(6060D)手插件</b>~^<b>(6070A)总检</b>~^<b>(6070B)总检</b>~^<b>(6070C)总检</b>~^<b>(6070D)总检</b>~^<b>(7010A)线缆段</b>~^<b>(7010B)线缆段</b>~^<b>(7010C)线缆段</b>~^<b>(7010D)线缆段</b>~^<b>(7020A)接线盒</b>~^<b>(7020B)接线盒</b>~^<b>(7020C)接线盒</b>~^<b>(7020D)接线盒</b>~^<b>(7025A)移印</b>~^<b>(7025B)移印</b>~^<b>(7025C)移印</b>~^<b>(7025D)移印</b>~^";
            this.ExportExcel(ExTitle, ds.Tables[0], false);
        }
    }
}
