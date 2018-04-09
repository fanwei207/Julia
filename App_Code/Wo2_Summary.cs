using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using CommClass;
using System.Reflection;
using Microsoft.ApplicationBlocks.Data;

/// <summary>
/// 加工单流程汇总
/// </summary>
public partial class QCExcel
{
    /// <summary>
    /// 整灯
    /// </summary>
    /// <param name="date"></param>
    /// <param name="uID"></param>
    /// <returns></returns>
    private DataSet GetRepSummary(string storeprocedure, string date, int uID)
    {//"sp_wo2_rep_wosummary_ZD"
        try
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@date", date);
            param[1] = new SqlParameter("@uID", uID);

            return SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, storeprocedure, param);
        }
        catch
        {
            return null;
        }
    }
    public void ExportRepSummary(string storeprocedure, string date, int uID)
    {
        // 创建一个Application对象并使其可见 
        Excel.Application app = new Excel.ApplicationClass();
        app.Visible = false;

        // 打开模板文件，得到WorkBook对象 
        Excel.Workbook workBook = app.Workbooks.Open(_templetFile, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);

        // 得到WorkSheet对象 
        Excel.Worksheet workSheet = (Excel.Worksheet)workBook.Sheets.get_Item(1);

        //输出明细信息
        DataSet _dataset = GetRepSummary(storeprocedure, date, uID);

        workSheet.Cells[3, 2] = "日期：" + DateTime.Now.ToString();

        DataRow row = _dataset.Tables[0].Rows[0];
        //一、生产指标
        workSheet.Cells[5, 5] = date;
        workSheet.Cells[6, 5] = Convert.ToDateTime(date).Day.ToString();
        workSheet.Cells[7, 4] = row["qty_yscl"].ToString();
        workSheet.Cells[8, 4] = row["month_days"].ToString();
        workSheet.Cells[9, 4] = row["cn_available_days"].ToString();
        workSheet.Cells[12, 5] = row["qty_acct"].ToString();
        workSheet.Cells[13, 5] = row["wh_acc_ab"].ToString();
        workSheet.Cells[14, 5] = row["qty_comp"].ToString();
        workSheet.Cells[15, 5] = row["wh_ab"].ToString();
        workSheet.Cells[17, 3] = row["std_gs"].ToString();
        workSheet.Cells[18, 5] = row["hr_hb"].ToString();
        workSheet.Cells[19, 5] = row["hr"].ToString();
        workSheet.Cells[21, 5] = row["qty_mgly"].ToString();
        workSheet.Cells[22, 5] = row["qty_mgly_pln"].ToString();
        workSheet.Cells[24, 5] = row["qty_tc_pln"].ToString();
        //生产指标：附表
        workSheet.Cells[29, 5] = row["person_a"].ToString();
        workSheet.Cells[30, 5] = row["person_b"].ToString();

        //每月预算的费率 计算所得
        //A: 直接人工成本
        workSheet.Cells[44, 3] = row["rate_wage"].ToString();
        workSheet.Cells[45, 3] = row["rate_wage_a"].ToString();
        workSheet.Cells[46, 3] = row["rate_wage_ab"].ToString();

        workSheet.Cells[47, 4] = row["total_shbx"].ToString();
        workSheet.Cells[47, 6] = row["total_shbx"].ToString();

        workSheet.Cells[48, 4] = row["total_flf"].ToString();
        workSheet.Cells[48, 6] = row["total_flf"].ToString();
        //B: 车间制造费用
        workSheet.Cells[50, 4] = row["total_cjglry"].ToString();
        workSheet.Cells[50, 6] = row["total_cjglry"].ToString();

        workSheet.Cells[51, 4] = row["total_zj"].ToString();
        workSheet.Cells[51, 6] = row["total_zj"].ToString();

        workSheet.Cells[52, 3] = row["rate_nyqt"].ToString();
        workSheet.Cells[52, 6] = row["cost_nyqt"].ToString();

        workSheet.Cells[53, 3] = row["rate_dzbj"].ToString();
        workSheet.Cells[53, 6] = row["cost_dzbj"].ToString();

        workSheet.Cells[54, 3] = row["rate_qt"].ToString();
        workSheet.Cells[54, 6] = row["cost_qt"].ToString();
        //C: 间接费用
        workSheet.Cells[56, 4] = row["total_jjglry"].ToString();
        workSheet.Cells[56, 6] = row["total_jjglry"].ToString();

        workSheet.Cells[57, 4] = row["total_zjtx"].ToString();
        workSheet.Cells[57, 6] = row["total_zjtx"].ToString();

        workSheet.Cells[58, 3] = row["rate_nywlxl"].ToString();
        workSheet.Cells[58, 6] = row["cost_nywlxl"].ToString();

        workSheet.Cells[59, 4] = row["total_bgfy"].ToString();
        workSheet.Cells[59, 6] = row["total_bgfy"].ToString();

        workSheet.Cells[60, 4] = row["total_sjbx"].ToString();
        workSheet.Cells[60, 6] = row["total_sjbx"].ToString();

        workSheet.Cells[61, 4] = row["total_ysfy"].ToString();
        workSheet.Cells[61, 6] = row["total_ysfy"].ToString();

        workSheet.Cells[62, 4] = row["total_tsfy"].ToString();
        workSheet.Cells[62, 6] = row["total_tsfy"].ToString();

        workSheet.Cells[67, 3] = row["std_cost_fl"].ToString();
        workSheet.Cells[68, 3] = row["fac_cost_fl"].ToString();


        // 输出Excel文件并退出 
        try
        {
            //workBook.Protect(strRnd, true, false);
            workBook.SaveAs(_outputFile, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Excel.XlSaveAsAccessMode.xlExclusive, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
            workBook.Close(null, null, null);
            app.Workbooks.Close();
            app.Application.Quit();
            app.Quit();

            System.Runtime.InteropServices.Marshal.ReleaseComObject(workSheet);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(workBook);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(app);

            workSheet = null;
            workBook = null;
            app = null;

            GC.Collect();
        }
        catch (Exception e)
        {
            throw e;
        }
        finally
        {
            KillProcess("EXCEL");
        }
    }
}
