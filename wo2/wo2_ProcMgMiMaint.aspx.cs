using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;

public partial class wo2_MgMiMaint : BasePage
{
    adamClass adm = new adamClass();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        { }
    }

    protected bool DeleteTempData(string uID)
    {
        try
        {
            string strName = "sp_wo2_t_deleteProcInOutTemp";
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@uID", uID);
            param[1] = new SqlParameter("@retValue", SqlDbType.Bit);
            param[1].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(adm.dsn0(), CommandType.StoredProcedure, strName, param);

            return Convert.ToBoolean(param[1].Value);
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    protected DataTable SelectBatchTempData(string uID)
    {
        try
        {
            string strSql = "sp_wo2_t_batchProcInOutTemp";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@uID", uID);

            return SqlHelper.ExecuteDataset(adm.dsn0(), CommandType.StoredProcedure, strSql, param).Tables[0];
        }
        catch
        {
            return null;
        }
    }

    protected void btnExport1_Click(object sender, EventArgs e)
    {
        if (dropType.SelectedIndex == 0)
        {
            this.Alert("请选择一个类别！");
            return;
        }

        if (filename.PostedFile.FileName == string.Empty)
        {
            this.Alert("选择要导入的文件");
            return;
        }
        else
        {
            string strFileName = filename.PostedFile.FileName.Trim();
            string strExt = strFileName.Substring(strFileName.LastIndexOf('.') + 1);

            if (strExt.ToUpper() != "XLS")
            {
                this.Alert("输入文件的格式不正确，且是2003格式的Excel！");
                return;
            }
            else
            {
                if (filename.PostedFile.ContentLength <= 0 || filename.PostedFile.ContentLength >= 5 * 1024 * 1024)
                {
                    this.Alert("输入文件的大小在5M以内");
                    return;
                }
            }
        }

        string strServerFile = Server.MapPath("/import/") + DateTime.Now.ToFileTime().ToString() + ".xls";
        try
        {
            filename.PostedFile.SaveAs(strServerFile);
        }
        catch
        {
            this.Alert("文件上传失败");
            return;
        }

        DataTable table;

        try
        {
            table = this.GetExcelContents(strServerFile);
        }
        catch (Exception ee)
        {
            this.Alert("读取Excel文件失败");
            return;
        }

        if (table.Rows.Count <= 0)
        {
            this.Alert("没有可供读取的数据！");
            return;
        }
        else
        {
            if (table.Columns.Count < 18)
            {
                this.Alert("上传的文件列数至少应有18列！");
                return;
            }
            //日期	公司	车间	工单	ID	物料	原工序	工序	工段长	生产线	线长	投入量	产出量
            //消耗物料	消耗数量	缺陷原因	责任人工号	责任人姓名	供应商代码
            if (table.Columns[0].ColumnName.Replace(" ", "").Trim().ToLower() != "日期" ||
                table.Columns[1].ColumnName.Replace(" ", "").Trim().ToLower() != "公司" ||
                table.Columns[2].ColumnName.Replace(" ", "").Trim().ToLower() != "车间" ||
                table.Columns[3].ColumnName.Replace(" ", "").Trim().ToLower() != "工单" ||
                table.Columns[4].ColumnName.Replace(" ", "").Trim().ToUpper() != "ID" ||
                table.Columns[5].ColumnName.Replace(" ", "").Trim().ToLower() != "物料" ||
                table.Columns[6].ColumnName.Replace(" ", "").Trim().ToLower() != "工序" ||
                table.Columns[7].ColumnName.Replace(" ", "").Trim().ToLower() != "工段长" ||
                table.Columns[8].ColumnName.Replace(" ", "").Trim().ToLower() != "生产线" ||
                table.Columns[9].ColumnName.Replace(" ", "").Trim().ToLower() != "线长" ||
                table.Columns[10].ColumnName.Replace(" ", "").Trim().ToLower() != "投入量" ||
                table.Columns[11].ColumnName.Replace(" ", "").Trim().ToLower() != "产出量" ||
                table.Columns[12].ColumnName.Replace(" ", "").Trim().ToLower() != "消耗物料" ||
                table.Columns[13].ColumnName.Replace(" ", "").Trim().ToLower() != "消耗数量" ||
                table.Columns[14].ColumnName.Replace(" ", "").Trim().ToLower() != "缺陷原因" ||
                table.Columns[15].ColumnName.Replace(" ", "").Trim().ToLower() != "责任人工号" ||
                table.Columns[16].ColumnName.Replace(" ", "").Trim().ToLower() != "责任人姓名" ||
                table.Columns[17].ColumnName.Replace(" ", "").Trim().ToLower() != "供应商代码"
                )
            {
                this.Alert("上传的文件列名和模板不一致！");
                return;
            }
            else
            {
                table.Columns[0].ColumnName = "t_date";
                table.Columns[1].ColumnName = "t_domain";
                table.Columns[2].ColumnName = "t_workshop";
                table.Columns[3].ColumnName = "t_nbr";
                table.Columns[4].ColumnName = "t_lot";
                table.Columns[5].ColumnName = "t_part";
                table.Columns[6].ColumnName = "t_proc";
                table.Columns[7].ColumnName = "t_sect_man";
                table.Columns[8].ColumnName = "t_line";
                table.Columns[9].ColumnName = "t_line_man";
                table.Columns[10].ColumnName = "t_qty_in";
                table.Columns[11].ColumnName = "t_qty_out";

                table.Columns[12].ColumnName = "t_xh_part";
                table.Columns[13].ColumnName = "t_xh_qty";
                table.Columns[14].ColumnName = "t_quexian";
                table.Columns[15].ColumnName = "t_zr_userNo";
                table.Columns[16].ColumnName = "t_zr_userName";
                table.Columns[17].ColumnName = "t_supp";

                table.Columns.Add("t_type");
                table.Columns.Add("t_uID");
                table.Columns.Add("t_uName");
                table.Columns.Add("t_errMsg");
            }

            foreach (DataRow row in table.Rows)
            {
                row["t_type"] = dropType.SelectedValue;
                row["t_uID"] = Session["uID"].ToString();
                row["t_uName"] = Session["uName"].ToString();

                if (row["t_quexian"].ToString().Length > 50)
                {
                    row["t_errMsg"] = "缺陷字数不能超过50个字";
                }
            }

            #region 批量导入
            if (DeleteTempData(Session["uID"].ToString()))
            {
                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(adm.dsn0()))
                {
                    bulkCopy.DestinationTableName = "dbo.wo2_InOutTemp";

                    bulkCopy.ColumnMappings.Add("t_type", "t_type");
                    bulkCopy.ColumnMappings.Add("t_domain", "t_domain");
                    bulkCopy.ColumnMappings.Add("t_date", "t_date");
                    bulkCopy.ColumnMappings.Add("t_workshop", "t_workshop");
                    bulkCopy.ColumnMappings.Add("t_nbr", "t_nbr");
                    bulkCopy.ColumnMappings.Add("t_lot", "t_lot");
                    bulkCopy.ColumnMappings.Add("t_part", "t_part");
                    bulkCopy.ColumnMappings.Add("t_proc", "t_proc");
                    bulkCopy.ColumnMappings.Add("t_sect_man", "t_sect_man");
                    bulkCopy.ColumnMappings.Add("t_line", "t_line");
                    bulkCopy.ColumnMappings.Add("t_line_man", "t_line_man");
                    bulkCopy.ColumnMappings.Add("t_qty_in", "t_qty_in");
                    bulkCopy.ColumnMappings.Add("t_qty_out", "t_qty_out");
                    
                    bulkCopy.ColumnMappings.Add("t_xh_part", "t_xh_part");
                    bulkCopy.ColumnMappings.Add("t_xh_qty", "t_xh_qty");
                    bulkCopy.ColumnMappings.Add("t_quexian", "t_quexian");
                    bulkCopy.ColumnMappings.Add("t_zr_userNo", "t_zr_userNo");
                    bulkCopy.ColumnMappings.Add("t_zr_userName", "t_zr_userName");
                    bulkCopy.ColumnMappings.Add("t_supp", "t_supp");

                    bulkCopy.ColumnMappings.Add("t_uID", "t_uID");
                    bulkCopy.ColumnMappings.Add("t_uName", "t_uName");
                    bulkCopy.ColumnMappings.Add("t_errMsg", "t_errMsg");

                    try
                    {
                        bulkCopy.WriteToServer(table);
                        //系统认为，在上传Log的时候，表示更新任务的开始
                        table = SelectBatchTempData(Session["uID"].ToString());
                        if (table != null)
                        {
                            if (table.Rows.Count != 0)
                            {//日期	公司	车间	工单	ID	物料	原工序	工序	工段长	生产线	线长	投入量	产出量 消耗物料	消耗数量	缺陷原因	责任人工号	责任人姓名	供应商代码
                                string EXTitle = "<b>日期</b>~^<b>公司</b>~^<b>车间</b>~^<b>工单</b>~^<b>ID</b>~^150^<b>物料</b>~^<b>工序</b>~^<b>工段长</b>~^<b>生产线</b>~^<b>线长</b>~^<b>投入量</b>~^<b>产出量</b>~^<b>消耗物料</b>~^<b>消耗数量</b>~^200^<b>缺陷原因</b>~^<b>责任人工号</b>~^<b>责任人姓名</b>~^<b>供应商代码</b>~^200^<b>错误信息</b>~^";
                                this.ExportExcel(EXTitle, table, false);
                            }
                            else
                            {
                                this.Alert("导入成功！");
                            }

                            table.Dispose();
                        }
                        else
                        {
                            this.Alert("导入时出错，请联系系统管理员2！");
                        }
                    }
                    catch (Exception ex)
                    {
                        this.Alert("导入时出错，请联系系统管理员3！");
                    }
                }
            }
            else
            {
                this.Alert("删除临时表失败，请联系系统管理员2！");
            }
            #endregion
        }
    }
    protected void btnExport3_Click(object sender, EventArgs e)
    {
        if (dropType.SelectedIndex == 0)
        {
            this.Alert("请选择一个类别！");
            return;
        }

        if (dropDomain.SelectedIndex == 0)
        {
            this.Alert("请选择一个公司！");
            return;
        }

        if (!string.IsNullOrEmpty(txtDate.Text))
        {
            if (!this.IsDate(txtDate.Text))
            {
                this.Alert("日期格式不正确！");
                return;
            }
        }

        try
        {
            string strSql = "sp_wo2_t_deleteProcInOut";
            SqlParameter[] param = new SqlParameter[10];
            param[0] = new SqlParameter("@t_type", dropType.SelectedValue);
            param[1] = new SqlParameter("@t_domain", dropDomain.SelectedValue);
            param[2] = new SqlParameter("@t_date", txtDate.Text);
            param[3] = new SqlParameter("@t_workshop", txtWorkShop.Text);
            param[4] = new SqlParameter("@t_orig_proc", txtOrigProc.Text);
            param[5] = new SqlParameter("@t_proc", txtProc.Text);
            param[6] = new SqlParameter("@t_nbr", txtNbr.Text);
            param[7] = new SqlParameter("@t_lot", txtLot.Text);
            param[8] = new SqlParameter("@t_line", txtLine.Text);

            param[9] = new SqlParameter("@retValue", SqlDbType.Bit);
            param[9].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(adm.dsn0(), CommandType.StoredProcedure, strSql, param);

            if (Convert.ToBoolean(param[9].Value))
            {
                this.Alert("删除成功！");
            }
            else
            {
                this.Alert("删除失败！请联系管理员1！");
            }
        }
        catch
        {
            this.Alert("删除失败！请联系管理员2！");
        }
    }
}