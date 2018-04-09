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


public partial class HR_hr_Salary_Piece : System.Web.UI.Page
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
            // Session["EXHeaderPrint"] = " 0-类别,1-每页的记录条数,2-标头,3-宽度,4-是否需要本页小计,5-是否要总人数，6-是否需要行间隔,7-是否需要输出页码,8-标头内容"
            string strHeader = Convert.ToString (Session["EXHeaderPrint"]);
            string strTitle =  Convert.ToString (Session["EXTitlePrint"]);
            string strSQL =  Convert.ToString (Session["EXSQLPrint"]);
            string strFooter = Convert.ToString(Session["EXFooterPrint"]);

            int intIndex,intTotal,intFlag,intAmount,intKind;
            intTotal = 0;
            intAmount = 0;
            intKind = 0;
           

            string[] strInfo = strHeader.Split(',');
           
           
            string[] strNameH,strNameF;
            strNameH = strTitle.Split('~');
            strNameF = strFooter.Split('~');
            intAmount = strNameH.Length;

            intFlag = (Convert.ToInt32(strInfo[4]) - 1 == 0) ? 0:1;

            ArrayList arrNameHeader = new ArrayList();
            ArrayList arrBottom = new ArrayList();
            ArrayList arrUnder = new ArrayList();
            string strQuery;
         


            #region Print Data

            PrintHeader(strInfo[8], Convert.ToInt32(strInfo[3]), intAmount, Convert.ToInt32(strInfo[7]),1);

            int intpage = 1;
         
            //string[] strLine,strBottom,strUnder;

            Table tbLine = new Table();
            tbLine.CellPadding = 0;
            tbLine.CellSpacing = 2;
            tbLine.GridLines = GridLines.Both;
            tbLine.BorderWidth = new Unit(1);
            tbLine.BorderColor = System.Drawing.Color.Black;
            tbLine.Style.Add("border-collapse", "collapse");
            tbLine.Width = new Unit(Convert.ToInt32(strInfo[3]));


            Table tbFooter = new Table();
            tbFooter.CellPadding = 0;
            tbFooter.CellSpacing = 2;
            tbFooter.GridLines = GridLines.Both;
            tbFooter.BorderWidth = new Unit(0);
            tbFooter.Width = new Unit(Convert.ToInt32(strInfo[3]));

            SqlDataReader reader = SqlHelper.ExecuteReader(adam.dsnx(), CommandType.Text, strSQL);
            while (reader.Read())
            {
                // 分页 + 页小结  
                if ((intpage - 1) % Convert.ToInt32(strInfo[1]) == 0 && intpage != 1)
                {
                   
                    arrBottom[0] = "小计：";
                    PrintFooter(strFooter, arrBottom, Convert.ToInt32(strInfo[3]), 0, intFlag, tbFooter);

                    PageTagHeader();
                    PageTagFooter();

                  
                    arrBottom.Clear();
                    arrBottom = new ArrayList();

                    Table tbLineTemp = new Table();
                    tbLineTemp.CellPadding = 0;
                    tbLineTemp.CellSpacing = 2;
                    tbLineTemp.GridLines = GridLines.Both ;
                    tbLineTemp.BorderWidth = new Unit(1);
                    tbLineTemp.BorderColor = System.Drawing.Color.Black;
                    tbLineTemp.Style.Add("border-collapse", "collapse");
                    tbLineTemp.Width = new Unit(Convert.ToInt32(strInfo[3]));

                    tbLine = tbLineTemp;

                    tbFooter = new Table();
                    tbFooter.CellPadding = 0;
                    tbFooter.CellSpacing = 2;
                    tbFooter.GridLines = GridLines.Both;
                    tbFooter.BorderWidth = new Unit(0);
                    tbFooter.Width = new Unit(Convert.ToInt32(strInfo[3]));

                    //是否每页都需要输出表头 1-输出 / 0 -不输出
                    if (strInfo[2] == "1")
                    {
                        // Header for a new page
                        PrintHeader(strInfo[8], Convert.ToInt32(strInfo[3]), intAmount, Convert.ToInt32(strInfo[7]), (intpage - 1) / Convert.ToInt32(strInfo[1]) + 1);
                    }

                    intKind = 0; // 输出行头
                }

                   for (int i = 0; i < strNameH.Length-1; i++)
                   {
                       if (Request["ty"] != null)
                       {
                           if (i == 0)
                               arrNameHeader.Add(Convert.ToString(intpage));
                           else
                               arrNameHeader.Add(Convert.ToString(reader[i]));
                       }
                       else
                           arrNameHeader.Add(Convert.ToString(reader[i]));


                       //赋值给页尾
                       if (arrBottom.Count < strNameH.Length - 1)
                       {
                           if (strNameF[i].IndexOf('@') == -1)
                               arrBottom.Add(Convert.ToString(reader[i]));
                           else
                               arrBottom.Add("@");

                       }
                       else
                       {
                           if (strNameF[i].IndexOf('@') == -1 && i != 0)
                               arrBottom[i] = Convert.ToString(Convert.ToDecimal(arrBottom[i]) + Convert.ToDecimal(reader[i]));
                           else
                               arrBottom[i] = "@";
                       }
                       //赋值给最后一页
                       if (arrUnder.Count < strNameH.Length - 1)
                       {
                           if (strNameF[i].IndexOf('@') == -1)
                               arrUnder.Add(Convert.ToString(reader[i]));
                           else
                               arrUnder.Add("@");
                       }
                       else
                       {
                           if (strNameF[i].IndexOf('@') == -1 && i != 0)
                               arrUnder[i] = Convert.ToString(Convert.ToDecimal(arrUnder[i]) + Convert.ToDecimal(reader[i]));
                           else
                               arrUnder[i] = "@";
                       }

                       
                   }
                   
                   PrintLine(strTitle, arrNameHeader,tbLine, intKind);

                   if (Convert.ToInt32(strInfo[6]) == 1) //是否输出间隔--空行
                        PrintEmpty(strNameH.Length-1, tbLine);

                   arrNameHeader.Clear();
                   arrNameHeader = new ArrayList();

                   intpage = intpage + 1;

                   if (strInfo[0] == "1")
                       intKind = 1;

            }  // End while reader

            reader.Close();
            if (intpage > 1)
            {
                if (strInfo[5] == "1") // 是否输出总人数
                {
                    intFlag = -1;
                    arrUnder[0] = Convert.ToString(intpage - 1);
                    PrintFooter(strFooter, arrUnder, Convert.ToInt32(strInfo[3]), 0, intFlag, tbFooter);
                    intFlag = 1;
                }

                arrBottom[0] = "本月小计：";
                PrintFooter(strFooter, arrBottom, Convert.ToInt32(strInfo[3]), 0, intFlag, tbFooter);
            }

            arrNameHeader.Clear();
            arrBottom.Clear();
            arrUnder.Clear();
            #endregion
        }
    }

    #region Split Page
    private void PageTagHeader()
    {
        Label lbPageHeader = new Label();
        lbPageHeader.Text = "<div Style = 'page-break-after:always;'>";
        phPieceSalary.Controls.Add(lbPageHeader);
    }

    private void PageTagFooter()
    {
        Label lbPageFooter = new Label();
        lbPageFooter.Text = "</div>";
        phPieceSalary.Controls.Add(lbPageFooter);
    }
    #endregion


    private void PrintHeader(string strHD,int strSize,int intAmount,int intType,int intpage)
    {
        Table tbHeader = new Table();
        tbHeader.CellPadding = 0;
        tbHeader.CellSpacing = 2;
        tbHeader.GridLines = GridLines.Both;
        tbHeader.BorderWidth = new Unit(0);
        tbHeader.Width = new Unit(strSize);

        TableRow tbrTemp = new TableRow();
        tbrTemp.BackColor = System.Drawing.Color.White;
        tbrTemp.VerticalAlign = VerticalAlign.Top;

        TableCell tbcTemp = new TableCell();
        tbcTemp.Text = strHD;
        if (intType == 1)
            tbcTemp.Width = new Unit(strSize * 4/5);
        else
            tbcTemp.Width = new Unit(strSize);

        tbcTemp.Font.Size = new FontUnit(12);
        tbcTemp.Font.Bold = true ;
        tbcTemp.HorizontalAlign = HorizontalAlign.Center;
        tbrTemp.Cells.Add(tbcTemp);

        if (intType == 1)
        {
            tbcTemp = new TableCell();
            tbcTemp.Text = DateTime.Now.ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;页码：" + intpage.ToString();
            tbcTemp.Width = new Unit(strSize / 5);
            tbcTemp.HorizontalAlign = HorizontalAlign.Right;
            tbrTemp.Cells.Add(tbcTemp);
        }
       
        tbHeader.Rows.Add(tbrTemp);
        phPieceSalary.Controls.Add(tbHeader);
    }

    private void PrintFooter(string strFooter, ArrayList arrFooter,int intSize,int intAmount,int intType,Table tbFootTemp)
    {
        //Setup footer table
       

        int j = 0;
        for (int i = 0; i < arrFooter.Count; i++)  // 移除arrFooter里值为"@"的项
        {
            if (arrFooter[j].ToString() == "@")
            {
                arrFooter.RemoveAt(j);
            }
            else
            {
                j = j + 1;
            }
        }

        ProcLine(strFooter, arrFooter, tbFootTemp, intType);

        phPieceSalary.Controls.Add(tbFootTemp);

    }


    private void PrintLine(string strQuery, ArrayList arrName, Table tbLine, int intType)
    {


        ProcLine(strQuery, arrName, tbLine, intType);

        phPieceSalary.Controls.Add(tbLine);
    }


    private void ProcLine(string strLine, ArrayList arrName, Table tbTemp, int intType)
    {
        TableRow tbrTemp = new TableRow();
        tbrTemp.BackColor = System.Drawing.Color.White;
        tbrTemp.HorizontalAlign = HorizontalAlign.Center;
        tbrTemp.VerticalAlign = VerticalAlign.Bottom;

        if (intType <= 0) // 控制行头输出
        {
            string strTemp,strValue,strSize;
            int intFlag;
            while (strLine.Length > 0)
            {
                intFlag = strLine.IndexOf("~^");
                if (intFlag != -1)
                {
                    strTemp = strLine.Substring(0, intFlag);

                    strLine = strLine.Substring(intFlag + 2);

                    intFlag = strTemp.IndexOf("^");        
                }
                else
                {
                    strTemp = strLine;
                    intFlag = strTemp.IndexOf("^");
                    strLine = "";
                }
                strSize = strTemp.Substring(0, intFlag);
                strValue = strTemp.Substring(intFlag + 1);


                if (strValue != "@")
                {
                    TableCell tbcTemp = new TableCell();
                    tbcTemp.Text = strValue;
                    tbcTemp.Width = new Unit(Convert.ToInt32(strSize));
                    tbrTemp.Cells.Add(tbcTemp);
                }

            }
            if (intType == -1)
                tbrTemp.Cells[0].Text = "总人数";
 
            tbTemp.Rows.Add(tbrTemp);
            tbrTemp = new TableRow();
        }
        
        
        for (int i=0; i< arrName.Count ;i++)
        {
            TableCell tbcTemp = new TableCell();
          
            tbcTemp.HorizontalAlign = HorizontalAlign.Center;
            if (Convert.ToString(arrName[i]) != "@")
                tbcTemp.Text = Convert.ToString(arrName[i]);
            else
                tbcTemp.Text = "&nbsp;";
        
            tbrTemp.Cells.Add(tbcTemp);
        }
        
        tbTemp.Rows.Add(tbrTemp);
    }


    private void PrintEmpty(int intSize, Table tbTemp)
    {
        TableRow tbrTemp = new TableRow();
        tbrTemp.BackColor = System.Drawing.Color.White;

        TableCell tbcTemp = new TableCell();
        tbcTemp.Text = "&nbsp;";
        tbcTemp.Height = new Unit(3);
        tbcTemp.ColumnSpan = intSize;
        tbTemp.Rows.Add(tbrTemp);
    }
}
