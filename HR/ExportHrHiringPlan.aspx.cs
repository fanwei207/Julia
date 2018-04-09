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
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;
using System.Collections;
using System.Data.SqlClient;

public partial class HR_ExportHrHiringPlan : System.Web.UI.Page
{
    adamClass adam = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int nRet = adam.securityCheck(Convert.ToString(Session["uID"]), Convert.ToString(Session["uRole"]), Convert.ToString(Session["orgID"]), "14020810", true, false);
            if (nRet <= 0)
            {
                Response.Redirect("~/public/Message.aspx?type=" + nRet.ToString(), true);
            }

            DataBind();


            //Response.ContentType = "application/vnd.ms-excel";
            //Response.Charset = "utf-8";
            //Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
            //Response.AppendHeader("content-disposition", "attachment; filename=HiringPlanning.xls");
        }
    }

    protected void DataBind()
    {
        int intYear = Convert.ToInt32(Request["ye"]);
        int intMin = Convert.ToInt32(Request["m1"]);
        int intMax = Convert.ToInt32(Request["m2"]);
        int intTotal = (intMax -intMin) * 7 + 6;
        HeardBuilt(intYear, intMin, intMax);

        SqlDataReader reader;
        //try
        //{
            string str = "sp_Hr_SelectHringPlaningExcel";
            SqlParameter[] sqlParam = new SqlParameter[4];
            sqlParam[0] = new SqlParameter("@year", intYear);
            sqlParam[1] = new SqlParameter("@monthmin", intMin);
            sqlParam[2] = new SqlParameter("@monthmax", intMax);
            sqlParam[3] = new SqlParameter("@Plant", Session["PlantCode"]);

            reader = SqlHelper.ExecuteReader(adam.dsn0(), CommandType.StoredProcedure, str, sqlParam);


            int intdept, intMonth;
            intdept = 0;
            intMonth = intMin;

            decimal[] decTotal = new decimal[intTotal+1];
            for (int i = 0; i < intTotal; i++)
            {
                decTotal[i] = 0;
            }
            
            bool blflag = true;

            TableRow rowtable = new TableRow();
            rowtable.HorizontalAlign = HorizontalAlign.Center;
            rowtable.BorderWidth = new Unit(0);
            rowtable.Font.Size = 10;

            while (reader.Read())
            {
                decimal[] decLine = new decimal[7];

                if (intdept != Convert.ToInt32(reader[0]))
                {
                    if (!blflag)
                    {
                       //Max month data aren't existing. 
                        if (intMonth <= intMax)
                        {
                           for (int j=intMonth;j<=intMax; j++)
                           {
                             complement(rowtable,"",false,true );
                           }
                        }

                        rowtable = new TableRow();
                        rowtable.HorizontalAlign = HorizontalAlign.Center;
                        rowtable.BorderWidth = new Unit(0);
                        rowtable.Font.Size = 10;

                        //Resetup the month when the department had changed.
                        intMonth =intMin ;
                        blflag = true;
                    }
                    intdept = Convert.ToInt32(reader[0]);

                     //Min month data aren't existing. 
                    if (intMonth != Convert.ToInt32(reader[5]))
                    {
                        for (int j=intMonth;j<Convert.ToInt32(reader[5]); j++)
                        {
                            complement(rowtable, reader[6].ToString(), blflag,false );
                            blflag = false;
                        }
                        intMonth =Convert.ToInt32(reader[5]);
                        
                    }
                   
                  
                }

               
              
                for (int j = 0; j < 4; j++)
                {         
                    decLine[j] = Convert.ToDecimal(reader[j + 1]);                       
                }
                
                decLine[4] = decLine[0] + decLine[1];
                decLine[5] = decLine[2] + decLine[3];
                decLine[6] = decLine[4] == 0 ? 0 : Math.Round(decLine[5] / decLine[4], 2);

                for (int j = (intMonth - intMin) * 7; j < (intMonth - intMin + 1) * 7; j++)
                {
                    decTotal[j] = decTotal[j] + decLine[j - (intMonth - intMin) * 7];
                }

                Tableline(reader[6].ToString(), decLine, blflag, intMonth == intMax ? true : false, rowtable);

                

                blflag = false;

                intMonth++;

            }
            reader.Close();

            //Max month data aren't existing. 
            if (intMonth <= intMax)
            {
                for (int j = intMonth; j <= intMax; j++)
                {
                    complement(rowtable, "", false, true);
                }
            }

            EmptyLine(intTotal);
            EmptyLine(intTotal);

            TableFooter(decTotal);
        //}
        //catch
        //{

        //}
    }


    /// <summary>
    /// Built Table Header
    /// </summary>
    /// <param name="intYear"></param>
    /// <param name="intMin"></param>
    /// <param name="intMax"></param>
    private void HeardBuilt(int intYear,int intMin,int intMax)
    {
        TableRow row = new TableRow();
        row.HorizontalAlign = HorizontalAlign.Center;
        row.BorderWidth = new Unit(0);
        row.Font.Size = 10;

        string strTitle;
        if (intMin == intMax)
            strTitle =intMin.ToString()+"月";
        else
            strTitle =intMin.ToString()+"月~"+ intMax.ToString()+"月";
        int intLong = 120 + (intMax - intMin + 1) * 560;
        TableCell cell = new TableCell();
        cell.Text = "TCP-CHINA —— " + Convert.ToString(Session["orgName"]) + "     " + strTitle +" 人事招聘考核报表";
        cell.Font.Bold = true;
        cell.Width = new Unit(intLong);
        cell.HorizontalAlign = HorizontalAlign.Left;
        cell.ColumnSpan = (intMax - intMin + 1) * 7 + 1;
        row.Cells.Add(cell);
        exl.Rows.Add(row);
        // --End Tile

        row = new TableRow();
        row.HorizontalAlign = HorizontalAlign.Center;
        row.BorderWidth = new Unit(0);
        row.Font.Size = 10;

        cell = new TableCell();
        cell.Text = "部门名称";
        cell.Font.Bold = true;
        cell.Width = new Unit(120);
        cell.HorizontalAlign = HorizontalAlign.Center;
        cell.RowSpan = 3;
        row.Cells.Add(cell);
        for (int i = intMin; i <= intMax; i++)
        {
            cell = new TableCell();
            cell.Text = i.ToString() + "月";
            cell.Width = new Unit(560);
            cell.Font.Bold = true;
            cell.HorizontalAlign = HorizontalAlign.Center;
            cell.ColumnSpan = 7;
            row.Cells.Add(cell);
        }
        exl.Rows.Add(row);
        //-- End 1 Row

        row = new TableRow();
        row.HorizontalAlign = HorizontalAlign.Center;
        row.BorderWidth = new Unit(0);
        row.Font.Size = 10;
        for (int i = intMin; i <= intMax; i++)
        {
            cell = new TableCell();
            cell.Text = "计划招聘";
            cell.Width = new Unit(160);
            cell.Font.Bold = true;
            cell.HorizontalAlign = HorizontalAlign.Center;
            cell.ColumnSpan = 2;
            row.Cells.Add(cell);

            cell = new TableCell();
            cell.Text = "实际招聘";
            cell.Width = new Unit(160);
            cell.Font.Bold = true;
            cell.HorizontalAlign = HorizontalAlign.Center;
            cell.ColumnSpan = 2;
            row.Cells.Add(cell);

            cell = new TableCell();
            cell.Text = "完成情况";
            cell.Width = new Unit(240);
            cell.Font.Bold = true;
            cell.HorizontalAlign = HorizontalAlign.Center;
            cell.ColumnSpan = 3;
            row.Cells.Add(cell);
        }
        exl.Rows.Add(row);
        //-- End 2 Row

        row = new TableRow();
        row.HorizontalAlign = HorizontalAlign.Center;
        row.BorderWidth = new Unit(0);
        row.Font.Size = 10;



        for (int i = intMin; i <= intMax; i++)
        {
            cell = new TableCell();
            cell.Text = "上半月计划";
            cell.Width = new Unit(80);
            cell.HorizontalAlign = HorizontalAlign.Center;
            row.Cells.Add(cell);

            cell = new TableCell();
            cell.Text = "下半月计划";
            cell.Width = new Unit(80);
            cell.HorizontalAlign = HorizontalAlign.Center;
            row.Cells.Add(cell);

            cell = new TableCell();
            cell.Text = "上半月";
            cell.Width = new Unit(80);
            cell.HorizontalAlign = HorizontalAlign.Center;
            row.Cells.Add(cell);

            cell = new TableCell();
            cell.Text = "下半月";
            cell.Width = new Unit(80);
            cell.HorizontalAlign = HorizontalAlign.Center;
            row.Cells.Add(cell);

            cell = new TableCell();
            cell.Text = "当月计划";
            cell.Width = new Unit(80);
            cell.Font.Bold = true;
            cell.HorizontalAlign = HorizontalAlign.Center;
            row.Cells.Add(cell);

            cell = new TableCell();
            cell.Text = "当月实际";
            cell.Width = new Unit(80);
            cell.Font.Bold = true;
            cell.HorizontalAlign = HorizontalAlign.Center;
            row.Cells.Add(cell);

            cell = new TableCell();
            cell.Text = "比例";
            cell.Width = new Unit(80);
            cell.Font.Bold = true;
            cell.HorizontalAlign = HorizontalAlign.Center;
            row.Cells.Add(cell);


        }

        exl.Rows.Add(row);

    }


    private void Tableline(string strDept,decimal[] decLine,bool blflag,bool blRow,TableRow row)
    {
       
        TableCell cell;

        if (blflag)
        {
            cell = new TableCell();
            cell.Text = strDept;
            cell.Width = new Unit(120);
            cell.HorizontalAlign = HorizontalAlign.Center;
            row.Cells.Add(cell);

        }
       


        for (int i = 0; i < decLine.Length; i++)
        {
            cell = new TableCell();
            cell.Text = decLine[i].ToString();
            cell.Width = new Unit(80);
            cell.HorizontalAlign = HorizontalAlign.Center;
            row.Cells.Add(cell);
        }

        Array.Clear(decLine, 0, 7);

        if (blRow)
            exl.Rows.Add(row);

    }


    private void TableFooter(decimal[] decTotal)
    {
        TableRow row = new TableRow();
        row.HorizontalAlign = HorizontalAlign.Center;
        row.BorderWidth = new Unit(0);
        row.Font.Size = 10;


        TableCell cell = new TableCell();
        cell.Text = "总计";
        cell.Width = new Unit(120);
        cell.Font.Bold = true;
        cell.HorizontalAlign = HorizontalAlign.Center;
        row.Cells.Add(cell);

        for (int i = 0; i < decTotal.Length; i++)
        {
            cell = new TableCell();
            cell.Text = decTotal[i].ToString();
            cell.Width = new Unit(80);
            cell.HorizontalAlign = HorizontalAlign.Center;
            row.Cells.Add(cell);
        }

        Array.Clear(decTotal, 0, decTotal.Length);

        exl.Rows.Add(row);
    }


    private void complement(TableRow row,string strDept,bool blflag,bool blRow)
    {
        TableCell cell;
         if (blflag)
        {
            cell = new TableCell();
            cell.Text = strDept;
            cell.Width = new Unit(120);
            cell.HorizontalAlign = HorizontalAlign.Center;
            row.Cells.Add(cell);

        }

        for (int i = 0; i < 7; i++)
        {
            cell = new TableCell();
            cell.Text = "0";
            cell.Width = new Unit(80);
            cell.HorizontalAlign = HorizontalAlign.Center;
            row.Cells.Add(cell);
        }

        if (blRow)
            exl.Rows.Add(row);
    }

    private void EmptyLine(int intNum)
    {
        TableRow row = new TableRow();
        row.HorizontalAlign = HorizontalAlign.Center;
        row.BorderWidth = new Unit(0);
        row.Font.Size = 10;


        TableCell cell = new TableCell();
        cell.Text = "";
        cell.Width = new Unit(120);
        cell.HorizontalAlign = HorizontalAlign.Center;
        row.Cells.Add(cell);

        for (int i = 0; i <= intNum; i++)
        {
            cell = new TableCell();
            cell.Text = "";
            cell.Width = new Unit(80);
            cell.HorizontalAlign = HorizontalAlign.Center;
            row.Cells.Add(cell);
        }
        exl.Rows.Add(row);
    }
}
