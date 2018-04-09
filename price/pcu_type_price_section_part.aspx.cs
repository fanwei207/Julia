using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System.Text;

public partial class price_pcu_type_price_section_part : BasePage
{

    protected override void OnInit(EventArgs e)
    {
        if (!IsPostBack)
        {
            this.Security.Register("123000900", "物料供应商价格对照表财务确认权限");
            
        }

        base.OnInit(e);
    }
    ////空间.Visible = this.Security["121000030"].isValid;



    adamClass chk = new adamClass();
    string guid = string.Empty;
    int indexRed = 0;
    

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ddlTypeBind();
            BindGridView();
            btnExport.Enabled = this.Security["123000900"].isValid;
        }
    }

    /// <summary>
    /// 获取数据
    /// </summary>
    private DataTable GetData()
    {
        try
        {
            #region 查找生成gridview数据
            string strC = "sp_pcu_selectTableView";
            SqlParameter[] paramC = new SqlParameter[]{
            new SqlParameter ("@type",ddlType.SelectedItem.Value)
            };

            DataSet dsC = SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.StoredProcedure, strC, paramC);


         
            #endregion

            #region gridview绑定数据
            string str = "sp_pcu_selectSectionPrice";//获取数据是存储过程的名字

            SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@type" , ddlType.SelectedItem.Value)
            , new SqlParameter("@vend" , txtVend.Text.Trim().ToString())
            , new SqlParameter("@QAD" , txtQAD.Text.Trim().ToString())
            , new SqlParameter("@calender" , ddlcalendar.SelectedItem.Value)
            , new SqlParameter ("@section" , SqlDbType.NVarChar,200)
            };
            param[4].Direction = ParameterDirection.Output;
            DataTable ds = SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.StoredProcedure, str, param).Tables[0];
            guid = param[4].Value.ToString().Trim();
            #endregion

            int count = -1;

            #region gridview表格生成
            foreach (DataRow row in dsC.Tables[0].Rows)
            {
                count++;

                if (row["VALUE"].ToString().Equals(guid))
                {
                    indexRed =dsC.Tables[0].Rows.Count-count-1;//得到在哪一行
                }
                
                BoundField boundF = new BoundField();
                boundF.HeaderText = row["NAME"].ToString();
                boundF.DataField = row["NAME"].ToString();

                switch (row["NAME"].ToString())
                {
                    case "类型":
                        boundF.HeaderStyle.Width = 100;
                        boundF.ItemStyle.Width = 100;
                        boundF.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                        break;
                    case "QAD":
                        boundF.HeaderStyle.Width = 100;
                        boundF.ItemStyle.Width = 100;
                        boundF.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                        break;
                    case "供应商":
                        boundF.HeaderStyle.Width = 100;
                        boundF.ItemStyle.Width = 100;
                        boundF.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                        break;
                    case "价格差异":
                        boundF.HeaderStyle.Width = 100;
                        boundF.ItemStyle.Width = 100;
                        boundF.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                        break;
                    case "供应商名":
                        boundF.HeaderStyle.Width = 300;
                        boundF.ItemStyle.Width = 300;
                        boundF.ItemStyle.HorizontalAlign = HorizontalAlign.Left;
                        break;
                    case "描述":
                        boundF.HeaderStyle.Width = 300;
                        boundF.ItemStyle.Width = 300;
                        boundF.ItemStyle.HorizontalAlign = HorizontalAlign.Left;
                        break;

                    default:
                        boundF.HeaderStyle.Width = 80;
                        boundF.ItemStyle.Width = 80;
                        boundF.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                        break;
                }

           

                gvInfo.Columns.Insert(0, boundF);
            }
            #endregion

            return ds; 
        }
        catch (Exception e)
        {
            return null;
        }
        
    }
    protected new void BindGridView()
    {
        ddlCalendarBind();
        gvInfo.Columns.Clear();
        gvInfo.DataSource = this.GetData();
        gvInfo.DataBind();
    }
    private void ddlCalendarBind()
    {
        try
        {
            string sqlstr = "sp_pcu_selectCalender";
            SqlParameter[] param = new SqlParameter[]{
                new SqlParameter("@type" , ddlType.SelectedItem.Value)
            };

            DataTable ds = SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.StoredProcedure, sqlstr, param).Tables[0];

            ddlcalendar.DataSource = ds;
            ddlcalendar.DataBind();

        }
        catch (Exception e)
        {
            return;
        }
    }

    private void ddlTypeBind()
    {
        try
        {
            string sqlStr = "sp_pcu_selectType";
            DataTable ds = SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.StoredProcedure, sqlStr).Tables[0];
            ddlType.DataSource = ds;
            ddlType.DataBind();
            selectlbMsg();
        }
        catch (Exception e)
        {
            return;
        }
    }
    protected void btnSelect_Click(object sender, EventArgs e)
    {
        BindGridView();
    }
    /// <summary>
    /// 导出cimload
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnExport_Click(object sender, EventArgs e)
    {
       int returnVel = 0;
       DataSet ds = SetPriceToPcMstr(out  returnVel);

       if (returnVel == 0)
       {
           ltlAlertTS.Text = "alert('处理数据出错，请联系管理员！');";
           return ;
       }
       else if (returnVel == 2)
       {
           ltlAlertTS.Text = "alert('数据中有结束时间不对的项目，请联系管理员！');";
           return;
       }
       else if (returnVel == 3)
       {
           ltlAlertTS.Text = "alert('正在导出，已经导出过的数据！请注意价格起止时间');";
       }

        string tempFile = Server.MapPath("/docs/QadPrice.xls");
        string outputFile = "QadPrice_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
        string outputFilein =Server.MapPath("../Excel/") + outputFile;

        FileStream templetFile = new FileStream(tempFile, FileMode.Open, FileAccess.Read);
        IWorkbook workbook = new HSSFWorkbook(templetFile);
       // DataTable dt1 = GetPriceToCimload();


        for (int i = 1; i <= 5; i++)
        {
            DataTable dt = ds.Tables[i - 1];
            ISheet workSheet = workbook.GetSheetAt(i);
            int nRows = 4;
            foreach (DataRow row in dt.Rows)
            {
                IRow iRow = workSheet.CreateRow(nRows);
                iRow.CreateCell(0).SetCellValue(row["pc_list"]);
                iRow.CreateCell(1).SetCellValue(row["pc_curr"]);
                iRow.CreateCell(2).SetCellValue(row["pc_empty1"]);
                iRow.CreateCell(3).SetCellValue(row["pc_part"]);
                iRow.CreateCell(4).SetCellValue(row["pc_um"]);
                iRow.CreateCell(5).SetCellValue(row["pc_start"], false, "MM/dd/yy");
                iRow.CreateCell(6).SetCellValue(row["pc_expire"], false, "MM/dd/yy");
                iRow.CreateCell(7).SetCellValue(row["pc_empty2"]);
                iRow.CreateCell(8).SetCellValue(row["pc_empty3"]);
                iRow.CreateCell(9).SetCellValue(row["pc_price"]);
                //iRow.CreateCell(10).SetCellValue(row["pc_price1"]);
                nRows++;
            }
        }
        // 输出Excel文件并退出 
        try
        {
            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);

                Stream localFile = new FileStream(outputFilein, FileMode.OpenOrCreate);

                localFile.Write(ms.ToArray(), 0, (int)ms.Length);
                localFile.Dispose();

                ms.Flush();
                ms.Position = 0;

                workbook = null;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

        ltlAlert.Text = "window.open('/Excel/" + outputFile + "','Excel','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";
        selectlbMsg();
        BindGridView();
    }


    private DataSet SetPriceToPcMstr(out int returnVal)
    {
        try
        {
            string strsql = "sp_pcu_insertPriceToPcMstr";

            SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@type" , ddlType.SelectedItem.Value)
             , new SqlParameter("@uID" , Convert.ToInt32(Session["uID"]))
             , new SqlParameter("@calendarID" , ddlcalendar.SelectedItem.Value )
             , new SqlParameter("@returnVal" , SqlDbType.Int)
            };
            param[3].Direction = ParameterDirection.Output;

            DataSet dt = SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.StoredProcedure, strsql, param);

             if (!int.TryParse(param[3].Value.ToString(), out returnVal))
             {
                 returnVal = 0;
             }


             return dt;
        }
        catch
        {
            returnVal = 0;
            return null;
        }

    }

    private DataTable GetPriceToCimload()
    {
        try
        {
            string strsql = "sp_pcu_selectPriceToCimload";

            SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@type" , ddlType.SelectedItem.Value)
            , new SqlParameter("@uID" , Convert.ToInt32(Session["uID"]))
            , new SqlParameter("@calendarID" , ddlcalendar.SelectedItem.Value )
            };

            return SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.StoredProcedure, strsql, param).Tables[0];
        }
        catch
        {
            return null;
        }
    }
    protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    {
        selectlbMsg();
        BindGridView();
    }

    private void selectlbMsg()
    {
        try
        {
            bool flag = false;
            string strsql = "sp_pcu_selectNotCimloadByType";

            SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@type",ddlType.SelectedItem.Value)
            };

            flag = Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, strsql, param));
            lbMsg.Visible = flag;
        }
        catch
        { 
        
        }
    }
    protected void ddlcalendar_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvInfo.Columns.Clear();
        gvInfo.DataSource = this.GetData();
        gvInfo.DataBind();
    }
    protected void gvInfo_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells.Count > indexRed && indexRed>=4)
            {
                e.Row.Cells[indexRed].BackColor = System.Drawing.Color.Yellow;
            }
        }
    }
    protected void gvInfo_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvInfo.PageIndex = e.NewPageIndex;
        gvInfo.DataSource = this.GetData();
        gvInfo.DataBind();
    }
}