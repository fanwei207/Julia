using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.IO;
using adamFuncs;
using System.Configuration;

namespace wo
{
    /// <summary>
    /// Summary description for Wo_ActualRelease
    /// </summary>
    public class Wo_ActualRelease
    {
        private adamClass chk = new adamClass();
        String strConn = ConfigurationSettings.AppSettings["SqlConn.BarCodeSys"];
        /// <summary>
        /// 清空临时表中上传者所有记录
        /// </summary>
        /// <param name="createdBy">上传者的ID号</param>
        private bool ClearTempTable(int createdBy)
        {
            try
            {
                SqlParameter param = new SqlParameter("@createdBy", createdBy);

                return Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, "sp_wo_deleteWoActRelTemp", param));
            }
            catch
            {
                return false;
            }
        }

        /// <summmary>
        /// 数据库端对临时表进行检查
        /// </summmary>
        private bool CheckTempTable(int createdBy)
        {
            try
            {
                SqlParameter param = new SqlParameter("@createdBy", createdBy);

                return Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, "sp_wo_checkWoActRelTemp", param));
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 判断临时表中该上传者能否通过
        /// </summary>
        /// <param name="createdBy">上传者的ID号</param>
        private bool JudgeTempTable(int createdBy)
        {
            try
            {
                SqlParameter param = new SqlParameter("@createdBy", createdBy);

                return Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, "sp_wo_judgeWoActRelTemp", param));
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 将大订单临时表更新到正式表里
        /// </summary>
        /// <param name="createdBy">上传者的ID号</param>
        private bool TransTempTable(int createdBy)
        {
            try
            {
                SqlParameter param = new SqlParameter("@createdBy", createdBy);

                return Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, "sp_wo_insertWoActRel", param));
            }
            catch
            {
                return false;
            }
        }

        public bool Import(DataTable DT, string uId, string plant,out string message)
        {
            message = "";
            DataTable dt = null;
            bool success = true;
            dt = DT;
            if (success)
            {
                try
                {
                    if (
                        dt.Columns[0].ColumnName != "加工单" ||
                        dt.Columns[1].ColumnName != "ID" ||
                        dt.Columns[2].ColumnName != "实际下达日期" ||
                        dt.Columns[3].ColumnName != "操作" ||
                        dt.Columns[4].ColumnName != "生产线" ||
                        dt.Columns[5].ColumnName != "成本中心"||
                        dt.Columns[6].ColumnName != "周期章"
                        )
                    {
                        dt.Reset();
                        message = "导入文件的模版不正确，请更新模板再导入!";
                        success = false;
                    }

                    DataTable TempTable = new DataTable("TempTable");
                    DataColumn TempColumn;
                    DataRow TempRow;

                    TempColumn = new DataColumn();
                    TempColumn.DataType = System.Type.GetType("System.String");
                    TempColumn.ColumnName = "wo_nbr";
                    TempTable.Columns.Add(TempColumn);

                    TempColumn = new DataColumn();
                    TempColumn.DataType = System.Type.GetType("System.String");
                    TempColumn.ColumnName = "wo_lot";
                    TempTable.Columns.Add(TempColumn);

                    TempColumn = new DataColumn();
                    TempColumn.DataType = System.Type.GetType("System.DateTime");
                    TempColumn.ColumnName = "wo_rel_date_act";
                    TempTable.Columns.Add(TempColumn);

                    TempColumn = new DataColumn();
                    TempColumn.DataType = System.Type.GetType("System.String");
                    TempColumn.ColumnName = "wo_mark";
                    TempTable.Columns.Add(TempColumn);

                    TempColumn = new DataColumn();
                    TempColumn.DataType = System.Type.GetType("System.String");
                    TempColumn.ColumnName = "wo_line";
                    TempTable.Columns.Add(TempColumn);

                    TempColumn = new DataColumn();
                    TempColumn.DataType = System.Type.GetType("System.String");
                    TempColumn.ColumnName = "wo_ctr";
                    TempTable.Columns.Add(TempColumn);

                    TempColumn = new DataColumn();
                    TempColumn.DataType = System.Type.GetType("System.String");
                    TempColumn.ColumnName = "wo_domain";
                    TempTable.Columns.Add(TempColumn);

                   

                    TempColumn = new DataColumn();
                    TempColumn.DataType = System.Type.GetType("System.String");
                    TempColumn.ColumnName = "wo_error";
                    TempTable.Columns.Add(TempColumn);

                   

                    TempColumn = new DataColumn();
                    TempColumn.DataType = System.Type.GetType("System.Int32");
                    TempColumn.ColumnName = "wo_createdby";
                    TempTable.Columns.Add(TempColumn);

                    TempColumn = new DataColumn();
                    TempColumn.DataType = System.Type.GetType("System.DateTime");
                    TempColumn.ColumnName = "wo_createdDate";

                    TempTable.Columns.Add(TempColumn);

                    TempColumn = new DataColumn();
                    TempColumn.DataType = System.Type.GetType("System.String");
                    TempColumn.ColumnName = "wo_zhouqizhang";
                    TempTable.Columns.Add(TempColumn);

                    if (dt.Rows.Count > 0)
                    {
                        string wo_nbr = string.Empty;
                        string wo_lot = string.Empty;
                        string wo_rel_date = string.Empty;
                        string wo_mark = string.Empty;
                        string wo_error = string.Empty;
                        string wo_createdBy = uId;
                        string wo_createdDate = DateTime.Now.ToString();
                        string wo_line = string.Empty;
                        string wo_ctr = string.Empty;
                        string wo_zhouqizhang = string.Empty;

                        DateTime dateFormat = DateTime.Now;

                        //先清空临时表中该上传员工的记录
                        if (ClearTempTable(Convert.ToInt32(uId)))
                        {
                            for (int i = 0; i <= dt.Rows.Count - 1; i++)
                            {
                                if (dt.Rows[i].IsNull(0)) wo_nbr = "";
                                else wo_nbr = dt.Rows[i].ItemArray[0].ToString().Trim();

                                if (dt.Rows[i].IsNull(1)) wo_lot = "";
                                else wo_lot = dt.Rows[i].ItemArray[1].ToString().Trim();

                                if (dt.Rows[i].IsNull(2)) wo_rel_date = "";
                                else wo_rel_date = dt.Rows[i].ItemArray[2].ToString().Trim();


                                if (dt.Rows[i].IsNull(3)) wo_mark = "";
                                else wo_mark = dt.Rows[i].ItemArray[3].ToString().Trim();

                                if (dt.Rows[i].IsNull(4)) wo_line = "";
                                else wo_line = dt.Rows[i].ItemArray[4].ToString().Trim();

                                if (dt.Rows[i].IsNull(5)) wo_ctr = "";
                                else wo_ctr = dt.Rows[i].ItemArray[5].ToString().Trim();

                                if (dt.Rows[i].IsNull(6)) wo_zhouqizhang = "";
                                else wo_zhouqizhang = dt.Rows[i].ItemArray[6].ToString().Trim();

                                wo_error = "";
                                TempRow = TempTable.NewRow();
                                if (wo_nbr.Length == 0 || wo_lot.Length == 0)
                                {
                                    wo_error += "加工单、ID不能为空;";
                                }

                                if ( wo_ctr.Length == 0)
                                {
                                    wo_error += "成本中心不能为空;";
                                }

                                if (wo_rel_date.Length == 0)
                                {
                                    wo_error += "下达日期不能为空;";
                                }
                                else
                                {
                                    try
                                    {
                                        DateTime dtFormat = Convert.ToDateTime(wo_rel_date);
                                        TempRow["wo_rel_date_act"] = dtFormat;
                                    }
                                    catch
                                    {
                                        wo_error += "下达日期格式不正确;";
                                    }
                                }

                                if (wo_mark.Length != 0 && wo_mark != "删除")
                                {
                                    wo_error += "操作只能留空或者删除;";
                                }

                                
                                TempRow["wo_nbr"] = wo_nbr;
                                TempRow["wo_lot"] = wo_lot;
                                TempRow["wo_mark"] = wo_mark;
                                TempRow["wo_domain"] = plant;
                                TempRow["wo_line"] = wo_line;
                                TempRow["wo_ctr"] = wo_ctr;
                                TempRow["wo_createdby"] = wo_createdBy;
                                TempRow["wo_createdDate"] = wo_createdDate;
                                TempRow["wo_error"] = wo_error;
                                TempRow["wo_zhouqizhang"] = wo_zhouqizhang;

                                TempTable.Rows.Add(TempRow);
                            }
                            //TempTable有数据的情况下批量复制到数据库里
                            if (TempTable != null && TempTable.Rows.Count > 0)
                            {
                                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(chk.dsn0(), SqlBulkCopyOptions.UseInternalTransaction))
                                {
                                    bulkCopy.DestinationTableName = "wo_actRel_Temp";

                                    bulkCopy.ColumnMappings.Clear();

                                    bulkCopy.ColumnMappings.Add("wo_nbr", "wo_nbr");
                                    bulkCopy.ColumnMappings.Add("wo_lot", "wo_lot");
                                    bulkCopy.ColumnMappings.Add("wo_rel_date_act", "wo_rel_date_act");
                                   
                                    bulkCopy.ColumnMappings.Add("wo_mark", "wo_mark");
                                    bulkCopy.ColumnMappings.Add("wo_line", "wo_line");
                                    bulkCopy.ColumnMappings.Add("wo_ctr", "wo_ctr");
                                    bulkCopy.ColumnMappings.Add("wo_domain", "wo_domain");
                                    bulkCopy.ColumnMappings.Add("wo_error", "wo_error");
                                    bulkCopy.ColumnMappings.Add("wo_createdby", "wo_createdby");
                                    bulkCopy.ColumnMappings.Add("wo_createdDate", "wo_createdDate");
                                    bulkCopy.ColumnMappings.Add("wo_zhouqizhang", "wo_zhouqizhang");

                                    try
                                    {
                                        bulkCopy.WriteToServer(TempTable);
                                    }
                                    catch (Exception ex)
                                    {
                                        message = "导入时出错，请联系系统管理员A！";
                                        success = false;
                                    }
                                    finally
                                    {
                                        TempTable.Dispose();
                                        bulkCopy.Close();
                                    }
                                }
                            }
                            dt.Reset();
                            if (success)
                            {
                                //数据库端验证
                                if (CheckTempTable(Convert.ToInt32(uId)))
                                {
                                    //判断上传内容能否通过验证
                                    if (JudgeTempTable(Convert.ToInt32(uId)))
                                    {
                                        if (TransTempTable(Convert.ToInt32(uId)))
                                        {
                                            message = "导入文件成功";
                                            success = true;
                                        }
                                        else
                                        {
                                            message = "导入时出错，请联系管理员C!";
                                            success = false;
                                        }
                                    }
                                    else
                                    {
                                        message = "导入文件结束，有错误!";
                                        success = false;
                                    }
                                }
                                else
                                {
                                    message = "导入时出错，请联系管理员B!";
                                    success = false;
                                }
                            }
                        }
                    }
                }
                catch
                {
                    message = "导入文件失败!";
                    success = false;
                }
                finally
                {
                    ;
                    //if (File.Exists(filePath))
                    //{
                    //    File.Delete(filePath);
                    //}
                }
            }
            return success;
        }

        public DataTable GetImportError(string createdBy)
        {
            try
            {
                SqlParameter param = new SqlParameter("@createdBy", createdBy);
                return SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.StoredProcedure, "sp_wo_selectWoActRelTempList", param).Tables[0];
            }
            catch
            {
                return null;
            }

        }

        public DataTable GetWoActRelList(string woNbr, string part, string relDateFrom, string relDateTo, string actDateFrom,string actDateTo,string domain)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[7];
                param[0] = new SqlParameter("@woNbr", woNbr);
                param[1] = new SqlParameter("@part", part);
                param[2] = new SqlParameter("@relDateFrom", relDateFrom);
                param[3] = new SqlParameter("@relDateTo", relDateTo);
                param[4] = new SqlParameter("@actDateFrom", actDateFrom);
                param[5] = new SqlParameter("@actDateTo", actDateTo);           
                param[6] = new SqlParameter("@domain", domain);
                return SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.StoredProcedure, "sp_wo_selectWoActRel", param).Tables[0];
            }
            catch
            {
                return null;
            }

        }
        public DataTable GetWoActRelListEX(string woNbr, string part, string relDateFrom, string relDateTo, string actDateFrom, string actDateTo, string domain, string line, string ctr, string get, string stuts, string online)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[12];
                param[0] = new SqlParameter("@woNbr", woNbr);
                param[1] = new SqlParameter("@part", part);
                param[2] = new SqlParameter("@relDateFrom", relDateFrom);
                param[3] = new SqlParameter("@relDateTo", relDateTo);
                param[4] = new SqlParameter("@actDateFrom", actDateFrom);
                param[5] = new SqlParameter("@actDateTo", actDateTo);
                param[6] = new SqlParameter("@domain", domain);
                param[7] = new SqlParameter("@line", line);
                param[8] = new SqlParameter("@ctr", ctr);
                param[9] = new SqlParameter("@get", get);
                param[10] = new SqlParameter("@stuts", stuts);
                param[11] = new SqlParameter("@online", online);
                return SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.StoredProcedure, "sp_wo_selectWoActRelEX", param).Tables[0];
            }
            catch
            {
                return null;
            }

        }
        public DataTable updateWoActRelEXget(string id)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@id", id);

                return SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.StoredProcedure, "sp_wo_updateWoActRelEXget", param).Tables[0];
            }
            catch
            {
                return null;
            }

        }
        public DataTable GetWoActRelListZq(string woNbr, string part, string relDateFrom, string relDateTo, string actDateFrom
            , string actDateTo, string domain, string zq)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[8];
                param[0] = new SqlParameter("@woNbr", woNbr);
                param[1] = new SqlParameter("@part", part);
                param[2] = new SqlParameter("@relDateFrom", relDateFrom);
                param[3] = new SqlParameter("@relDateTo", relDateTo);
                param[4] = new SqlParameter("@actDateFrom", actDateFrom);
                param[5] = new SqlParameter("@actDateTo", actDateTo);
                param[6] = new SqlParameter("@domain", domain);
                param[7] = new SqlParameter("@zq", zq);
                return SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.StoredProcedure, "sp_wo_selectWoActRelZq", param).Tables[0];
            }
            catch
            {
                return null;
            }

        }
        public string GetWoMstrULInfo(string domain, string nbr, string lot, string bom)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[5];
                param[0] = new SqlParameter("@domain", domain);
                param[1] = new SqlParameter("@nbr", nbr);
                param[2] = new SqlParameter("@lot", lot);
                param[3] = new SqlParameter("@bom", bom);
                param[4] = new SqlParameter("@retValue", SqlDbType.VarChar, 1000);
                param[4].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, "sp_wo_selectULInfo", param);

                return param[4].Value.ToString();
            }
            catch
            {
                return "";
            }
        }
        public DataTable GetWoActRelPrintList(string woNbr, string part, string relDateFrom, string relDateTo)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[4];
                param[0] = new SqlParameter("@woNbr", woNbr);
                param[1] = new SqlParameter("@part", part);
                param[2] = new SqlParameter("@relDateFrom", relDateFrom);
                param[3] = new SqlParameter("@relDateTo", relDateTo);
                return SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.StoredProcedure, "sp_wo_selectWoActRelPrintList", param).Tables[0];
            }
            catch
            {
                return null;
            }

        }
        public DataTable GetWoActRelPrint(string wolist, string woNbr, string part, string relDateFrom, string relDateTo)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[5];
                param[0] = new SqlParameter("@woNbr", woNbr);
                param[1] = new SqlParameter("@part", part);
                param[2] = new SqlParameter("@relDateFrom", relDateFrom);
                param[3] = new SqlParameter("@relDateTo", relDateTo);
                param[4] = new SqlParameter("@wolist", wolist);
                return SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.StoredProcedure, "sp_wo_selectWoActRelPrint", param).Tables[0];
            }
            catch
            {
                return null;
            }

        }

    }
}