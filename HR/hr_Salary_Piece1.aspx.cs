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
using System.Data.Common;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;
using Wage;

/*
 * marked by shanzm 2013-08-05:
 * 工资条加上 社保缴费告知
 */
public partial class hr_Salary_Piece1 : System.Web.UI.Page
{
    adamClass adam = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
             
            if (Session["EXTitlePrint"] == null || Session["EXSQLPrint"] == null)
            {
                Response.Redirect("public/exportExcelClose.aspx",true );
                return;
            }


            int PAGECOUNT = 36;//每页包含行数
            int rowIndex = 1;

            AppLibrary.WriteExcel.XlsDocument doc = new AppLibrary.WriteExcel.XlsDocument();
            doc.FileName = "salary.xls";
            AppLibrary.WriteExcel.Worksheet sheet = doc.Workbook.Worksheets.Add("计件工资");


            SqlDataReader reader = SqlHelper.ExecuteReader(adam.dsnx(), CommandType.Text, Convert.ToString(Session["EXSQLPrint"]));
            while (reader.Read())
            {
                decimal _insuranceAmount = 0;

                if (Convert.ToDecimal(reader["hr_Salary_rfound"]) > 0)
                {
                    _insuranceAmount = Convert.ToDecimal(reader["insuranceAmount"]);
                }

                PrintHeader(doc, sheet, rowIndex, _insuranceAmount
                                , reader["userNo"], reader["userName"], reader["hr_Salary_salaryDate"], reader["hr_Salary_basic"], reader["hr_Salary_over"]
                                , reader["hr_Salary_benefit"], reader["hr_Salary_LengService"], reader["hr_Salary_Fullattendence"], reader["hr_Salary_oallowance"], reader["hr_Salary_nightMoney"]
                                , reader["hr_Salary_subsidies"], reader["hr_Salary_holiday"], reader["hr_Salary_duereward"], reader["hr_Salary_duct"], reader["hr_Salary_hfound"]
                                , reader["hr_Salary_rfound"], reader["hr_Salary_memship"], reader["hr_Salary_tax"], reader["hr_Salary_workpay"]); 

                rowIndex = rowIndex + 6;

                //A4纸每页只能打印36行，刚好是5个人
                if (rowIndex % PAGECOUNT == 0)
                {
                    rowIndex = rowIndex + 1;
                }
            }

            reader.Close();

            doc.Send();

            Response.Flush();
            Response.End();
        }
    }

    private void PrintHeader(AppLibrary.WriteExcel.XlsDocument doc, AppLibrary.WriteExcel.Worksheet sheet, int rowIndex, decimal insuranceAmount
        , object userNo, object userName, object date, object baseSalary, object overSalary, object benefitSalary
        , object lengServiceSalary, object fullattendenceSalary, object oallowanceSalary, object nightMoney, object subsidies, object holidaySalary, object dueSalary, object duct, object hfound
        , object rfound, object memship, object tax, object workpay)
    {
        rowIndex++;
        //行1：空行
        //行2：标题
        AppLibrary.WriteExcel.XF xf = doc.NewXF();
        xf.HorizontalAlignment = AppLibrary.WriteExcel.HorizontalAlignments.Centered;
        xf.VerticalAlignment = AppLibrary.WriteExcel.VerticalAlignments.Centered;
        xf.Font.FontName = "宋体";
        xf.UseMisc = true;
        xf.Font.Bold = false;
        xf.Font.Height = 9 * 256 / 13; //对应size = 13

        xf.LeftLineStyle = 1;
        xf.TopLineStyle = 1;
        xf.RightLineStyle = 1;
        xf.BottomLineStyle = 1;

        sheet.Cells.Add(rowIndex, 1, "工号", xf);
        sheet.Cells.Add(rowIndex, 2, "姓名", xf);
        sheet.Cells.Add(rowIndex, 3, "年月", xf);
        sheet.Cells.Add(rowIndex, 4, "基本工资", xf);
        sheet.Cells.Add(rowIndex, 5, "加班工资", xf);
        sheet.Cells.Add(rowIndex, 6, "绩效奖", xf);
        sheet.Cells.Add(rowIndex, 7, "工龄工资", xf);
        sheet.Cells.Add(rowIndex, 8, "全勤奖", xf);
        sheet.Cells.Add(rowIndex, 9, "津贴", xf);
        sheet.Cells.Add(rowIndex, 10, "中夜津贴", xf);
        sheet.Cells.Add(rowIndex, 11, "补贴", xf);
        sheet.Cells.Add(rowIndex, 12, "国假", xf);
        sheet.Cells.Add(rowIndex, 13, "应发金额", xf);
        sheet.Cells.Add(rowIndex, 14, "扣款", xf);
        sheet.Cells.Add(rowIndex, 15, "公积", xf);
        sheet.Cells.Add(rowIndex, 16, "社保", xf);
        sheet.Cells.Add(rowIndex, 17, "工会", xf);
        sheet.Cells.Add(rowIndex, 18, "所得税", xf);
        sheet.Cells.Add(rowIndex, 19, "实发金额", xf);
        sheet.Cells.Add(rowIndex, 20, "签名", xf);

        //行3：数据行
        rowIndex++;

        sheet.Cells.Add(rowIndex, 1, userNo, xf);
        sheet.Cells.Add(rowIndex, 2, userName, xf);
        sheet.Cells.Add(rowIndex, 3, date, xf);
        sheet.Cells.Add(rowIndex, 4, baseSalary, xf);
        sheet.Cells.Add(rowIndex, 5, overSalary, xf);
        sheet.Cells.Add(rowIndex, 6, benefitSalary, xf);
        sheet.Cells.Add(rowIndex, 7, lengServiceSalary, xf);
        sheet.Cells.Add(rowIndex, 8, fullattendenceSalary, xf);
        sheet.Cells.Add(rowIndex, 9, oallowanceSalary, xf);
        sheet.Cells.Add(rowIndex, 10, nightMoney, xf);
        sheet.Cells.Add(rowIndex, 11, subsidies, xf);
        sheet.Cells.Add(rowIndex, 12, holidaySalary, xf);
        sheet.Cells.Add(rowIndex, 13, dueSalary, xf);
        sheet.Cells.Add(rowIndex, 14, duct, xf);
        sheet.Cells.Add(rowIndex, 15, hfound, xf);
        sheet.Cells.Add(rowIndex, 16, rfound, xf);
        sheet.Cells.Add(rowIndex, 17, memship, xf);
        sheet.Cells.Add(rowIndex, 18, tax, xf);
        sheet.Cells.Add(rowIndex, 19, workpay, xf);
        sheet.Cells.Add(rowIndex, 20, string.Empty, xf);

        //行4-6的字体：
        xf.HorizontalAlignment = AppLibrary.WriteExcel.HorizontalAlignments.Left;
        xf.Font.Height = 9 * 256 / 13; //对应size = 13
        xf.LeftLineStyle = 0;
        xf.TopLineStyle = 0;
        xf.RightLineStyle = 0;
        xf.BottomLineStyle = 0;

        //行4
        rowIndex++;
        sheet.Cells.Add(rowIndex, 1, "根据中华人民共和国社会保障法的规定，现将社保缴费情况告知如下：", xf);

        //行5：字体小一号


        rowIndex++;
        sheet.Cells.Add(rowIndex, 2, "公司社保缴费金额：" + insuranceAmount.ToString(), xf);

        //行6：字体10
        rowIndex++;
        sheet.Cells.Add(rowIndex, 1, "此凭证一式二份，一份交于员工本人，一份员工签字并交回公司存档。", xf);

        //行5：尾行，虚线行
        xf.LeftLineStyle = 0;
        xf.TopLineStyle = 0;
        xf.RightLineStyle = 0;
        xf.BottomLineStyle = 4;

        rowIndex++;

        sheet.Cells.Add(rowIndex, 1, string.Empty, xf);
        sheet.Cells.Add(rowIndex, 2, string.Empty, xf);
        sheet.Cells.Add(rowIndex, 3, string.Empty, xf);
        sheet.Cells.Add(rowIndex, 4, string.Empty, xf);
        sheet.Cells.Add(rowIndex, 5, string.Empty, xf);
        sheet.Cells.Add(rowIndex, 6, string.Empty, xf);
        sheet.Cells.Add(rowIndex, 7, string.Empty, xf);
        sheet.Cells.Add(rowIndex, 8, string.Empty, xf);
        sheet.Cells.Add(rowIndex, 9, string.Empty, xf);
        sheet.Cells.Add(rowIndex, 10, string.Empty, xf);
        sheet.Cells.Add(rowIndex, 11, string.Empty, xf);
        sheet.Cells.Add(rowIndex, 12, string.Empty, xf);
        sheet.Cells.Add(rowIndex, 13, string.Empty, xf);
        sheet.Cells.Add(rowIndex, 14, string.Empty, xf);
        sheet.Cells.Add(rowIndex, 15, string.Empty, xf);
        sheet.Cells.Add(rowIndex, 16, string.Empty, xf);
        sheet.Cells.Add(rowIndex, 17, string.Empty, xf);
        sheet.Cells.Add(rowIndex, 18, string.Empty, xf);
        sheet.Cells.Add(rowIndex, 19, string.Empty, xf);
        sheet.Cells.Add(rowIndex, 20, string.Empty, xf);
    }

    private void PrintFooter(string strFooter, ArrayList arrFooter,int intSize,int intAmount,int intType,Table tbFootTemp)
    {


    }
}
