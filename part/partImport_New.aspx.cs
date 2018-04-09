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
using Microsoft.ApplicationBlocks.Data;
using System.IO;
using System.Data.OleDb;
using System.Data.SqlClient;
using CommClass;


public partial class ManualPoImport : BasePage
{
    adamClass chk = new adamClass();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
        }
    }

    private bool InsertBatchTemp(string uID, string uName, string plantCode)
    {
        try
        {
            SqlParameter[] sqlParam = new SqlParameter[4];
            sqlParam[0] = new SqlParameter("@uID", uID);
            sqlParam[1] = new SqlParameter("@uName", uName);
            sqlParam[2] = new SqlParameter("@plantCode", plantCode);
            sqlParam[3] = new SqlParameter("@retValue", DbType.Boolean);
            sqlParam[3].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.StoredProcedure, "sp_pro_insertBatchPro", sqlParam);

            return Convert.ToBoolean(sqlParam[3].Value);
        }
        catch {
            return false;
        }

    }

    protected bool CheckValidity(string uID)
    {
        try
        {
            string strSql = "sp_pro_checkproItem";

            SqlParameter[] sqlParam = new SqlParameter[2];
            sqlParam[0] = new SqlParameter("@uID", uID);
            sqlParam[1] = new SqlParameter("@retValue", DbType.Boolean);
            sqlParam[1].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.StoredProcedure, strSql, sqlParam);

            return Convert.ToBoolean(sqlParam[1].Value);
        }
        catch
        {
            return false;
        }
    }

    private bool ClearTemp(int uID)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@uID", uID);

            SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.StoredProcedure, "sp_pro_clearProTemp", param);

            return true;
        }
        catch
        {
            return false;
        }
    }

    public Boolean ImportExcelFile()
    {
        DataTable dt ;
        String strFileName = "";
        String strCatFolder = "";
        String strUserFileName = "";
        int intLastBackslash = 0;

        #region 上传文档例行处理
        strCatFolder = Server.MapPath("/import");

        if (!Directory.Exists(strCatFolder))
        {
            try
            {
                Directory.CreateDirectory(strCatFolder);
            }
            catch
            {
                ltlAlert.Text = "alert('Fail to upload file.');";
                return false;
            }

        }

        strUserFileName = filename1.PostedFile.FileName;
        intLastBackslash = strUserFileName.LastIndexOf("\\");
        strFileName = strUserFileName.Substring(intLastBackslash + 1);
        if (strFileName.Trim().Length <= 0)
        {
            ltlAlert.Text = "alert('Please select a file.');";
            return false;
        }

        strUserFileName = strFileName;

        strFileName = strCatFolder + "\\" + DateTime.Now.ToString("yyyyMMddhhmmssfff") + strFileName;
        #endregion

        if (filename1.PostedFile != null)
        {
            string error = "";
            if (filename1.PostedFile.ContentLength > 8388608)
            {
                ltlAlert.Text = "alert('The maximum upload file is 8 MB.');";
                return false;
            }

            try
            {
                filename1.PostedFile.SaveAs(strFileName);
            }
            catch
            {
                ltlAlert.Text = "alert('Failed to upload file.');";
                return false;
            }

            if (File.Exists(strFileName))
            {
                try
                {
                    dt = this.GetExcelContents(strFileName); //chk.getExcelContents(strFileName);
                }
                catch (Exception e)
                {

                    if (File.Exists(strFileName))
                    {
                        File.Delete(strFileName);
                    }

                    ltlAlert.Text = "alert('Import file must be in Excel format.');";
                    return false;
                }
                finally
                {
                    if (File.Exists(strFileName))
                    {
                        File.Delete(strFileName);
                    }
                }

                if (dt.Rows.Count > 0)
                {
                    /*
                     *  导入的Excel文件必须满足：
                     *      1、至少应该有五列
                     *      2、从第五列开始即视为工序
                     *      3、工序名称必须在wo2_mop中存在
                    */

                    if (dt.Columns.Count != 29)

                    {
                        dt.Reset();

                        ltlAlert.Text = "alert('The file must have 29 columns！');";

                        return false;
                    }

                    #region Excel的列名必须保持一致
                    for (int col = 0; col < dt.Columns.Count; col++)
                    {
                        if (col == 0 && dt.Columns[col].ColumnName.Trim() != "部件号")
                        {
                            dt.Reset(); 
                            ltlAlert.Text = "alert('The " + col.ToString() + "th column name should be 部件号！');";
                            return false;
                        }

                        if (col == 1 && dt.Columns[col].ColumnName.Trim() != "部件描述")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('The " + col.ToString() + "th column name should be 部件描述！');";
                            return false;
                        }

                        if (col == 2 && dt.Columns[col].ColumnName.Trim() != "部件分类")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('The " + col.ToString() + "th column name should be 部件分类 ！');";
                            return false;
                        }

                        if (col == 3 && dt.Columns[col].ColumnName.Trim() != "状态(0:可用 1:试用 2:停用)")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('The " + col.ToString() + "th column name should be 状态(0:可用 1:试用 2:停用)！');";
                            return false;
                        }

                        if (col == 4 && dt.Columns[col].ColumnName.Trim() != "最小库存量")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('The " + col.ToString() + "th column name should be 最小库存量！');";
                            return false;
                        }

                        if (col == 5 && dt.Columns[col].ColumnName.Trim() != "单位(可空)")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('The " + col.ToString() + "th column name should be 单位(可空)！');";
                            return false;
                        }

                        if (col == 6 && dt.Columns[col].ColumnName.Trim() != "转换前单位(可空)")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('The " + col.ToString() + "th column name should be 转换前单位(可空)！');";
                            return false;
                        }
                        if (col == 7 && dt.Columns[col].ColumnName.Trim() != "转换系数(可空)")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('The " + col.ToString() + "th column name should be 转换系数(可空) ！');";
                            return false;
                        }

                        if (col == 8 && dt.Columns[col].ColumnName.Trim() != "QAD零件号")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('The " + col.ToString() + "th column name should be QAD零件号！');";
                            return false;
                        }

                        if (col == 9 && dt.Columns[col].ColumnName.Trim() != "QAD描述1")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('The " + col.ToString() + "th column name should be QAD描述1！');";
                            return false;
                        }

                        if (col == 10 && dt.Columns[col].ColumnName.Trim() != "QAD描述2")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('The " + col.ToString() + "th column name should be QAD描述2！');";
                            return false;
                        }

                        if (col == 11 && dt.Columns[col].ColumnName.Trim() != "单位-um")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('The " + col.ToString() + "th column name should be 单位-um！');";
                            return false;
                        }

                        if (col == 12 && dt.Columns[col].ColumnName.Trim() != "设计组-dsgn")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('The " + col.ToString() + "th column name should be 设计组-dsgn！');";
                            return false;
                        }

                        if (col == 13 && dt.Columns[col].ColumnName.Trim() != "推销组-promo")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('The " + col.ToString() + "th column name should be 推销组-promo！');";
                            return false;
                        }
                        //同名的，自动加1
                        if (col == 14 && dt.Columns[col].ColumnName.Trim() != "产品类型-parttype")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('The " + col.ToString() + "th column name should be 产品类型-parttype！');";
                            return false;
                        }

                        if (col == 15 && dt.Columns[col].ColumnName.Trim() != "状态-QADstatus")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('The " + col.ToString() + "th column name should be 状态-QADstatus！');";
                            return false;
                        }

                        if (col == 16 && dt.Columns[col].ColumnName.Trim() != "组-group")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('The " + col.ToString() + "th column name should be 组-group！');";
                            return false;
                        }
                        if (col == 17 && dt.Columns[col].ColumnName.Trim() != "图纸-draw")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('The " + col.ToString() + "th column name should be 图纸-draw！');";
                            return false;
                        }


                        if (col == 18 && dt.Columns[col].ColumnName.Trim() != "版本-rev")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('The " + col.ToString() + "th column name should be 版本-rev！');";
                            return false;
                        }
                        if (col == 19 && dt.Columns[col].ColumnName.Trim() != "图纸位置-drwg")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('The " + col.ToString() + "th column name should be 图纸位置-drwg！');";
                            return false;
                        }

                        if (col == 20 && dt.Columns[col].ColumnName.Trim() != "库位-loc")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('The " + col.ToString() + "th column name should be 库位-loc！');";
                            return false;
                        }

                        if (col == 21 && dt.Columns[col].ColumnName.Trim() != "发放原则-isspol")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('The " + col.ToString() + "th column name should be 发放原则-isspol！');";
                            return false;
                        }
                        if (col == 22 && dt.Columns[col].ColumnName.Trim() != "采购员-buyer")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('The " + col.ToString() + "th column name should be 采购员-buyer！');";
                            return false;
                        }


                        if (col == 23 && dt.Columns[col].ColumnName.Trim() != "供应商-vend")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('The " + col.ToString() + "th column name should be 供应商-vend！');";
                            return false;
                        }
                        if (col == 24 && dt.Columns[col].ColumnName.Trim() != "采制代码-pmcode")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('The " + col.ToString() + "th column name should be 采制代码-pmcode！');";
                            return false;
                        }
                        if (col == 25 && dt.Columns[col].ColumnName.Trim() != "制造提前期-mfglead")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('The " + col.ToString() + "th column name should be 制造提前期-mfglead！');";
                            return false;
                        }
                        if (col == 26 && dt.Columns[col].ColumnName.Trim() != "采购提前期-purlead")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('The " + col.ToString() + "th column name should be 采购提前期-purlead！');";
                            return false;
                        }


                        if (col == 27 && dt.Columns[col].ColumnName.Trim() != "虚实件-phantom")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('The " + col.ToString() + "th column name should be 虚实件-phantom！');";
                            return false;
                        }
                        if (col == 28 && dt.Columns[col].ColumnName.Trim() != "域-domain")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('The " + col.ToString() + "th column name should be 域-domain！');";
                            return false;
                        }
                    }																
                    #endregion

                    //转换成模板格式
                    DataTable table = new DataTable("temp");
                    DataColumn column;
                    DataRow row;

                    #region 定义表列
                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "partNumber";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "partdesc";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "category";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "status";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.Int32");
                    column.ColumnName = "min_inv";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "unit";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "tranunit";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "tranrate";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "xxptmt_part";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "xxptmt_um";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "xxptmt_desc1";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "xxptmt_desc2";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "xxptmt_prodline";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "xxptmt_dsgn";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "xxptmt_promo";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "xxptmt_parttype";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "xxptmt_status";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "xxptmt_group";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "xxptmt_draw";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "xxptmt_rev";
                    table.Columns.Add(column);


                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "xxptmt_drwg";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "xxptmt_abc";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "xxptmt_loc";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "xxptmt_article";
                    table.Columns.Add(column);

                    column = new DataColumn();//null
                    column.DataType = System.Type.GetType("System.Int32");
                    column.ColumnName = "xxptmt_ordper";
                    table.Columns.Add(column);

                    column = new DataColumn();//null
                    column.DataType = System.Type.GetType("System.Int32");
                    column.ColumnName = "xxptmt_sftystk";
                    table.Columns.Add(column);

                    column = new DataColumn();//null
                    column.DataType = System.Type.GetType("System.Int32");
                    column.ColumnName = "xxptmt_sftytime";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "xxptmt_isspol";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "xxptmt_buyer";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "xxptmt_vend";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "xxptmt_pmcode";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.Int32");
                    column.ColumnName = "xxptmt_mfglead";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.Int32");
                    column.ColumnName = "xxptmt_purlead";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "xxptmt_phantom";
                    table.Columns.Add(column);

                    column = new DataColumn();//null
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "xxptmt_yield";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "xxptmt_domain";
                    table.Columns.Add(column);


                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.Int32");
                    column.ColumnName = "createBy";
                    table.Columns.Add(column);


                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "createname";
                    table.Columns.Add(column);


                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "errMsg";
                    table.Columns.Add(column);

                    #endregion

                    int _uID = Convert.ToInt32(Session["uID"]);

                    if (ClearTemp(_uID))
                    {
                        foreach (DataRow r in dt.Rows)
                        {
                            if (r[0].ToString().Trim() != string.Empty)
                            {
                                row = table.NewRow();

                                #region 赋值、长度判定

                                if (r[0].ToString().Trim() == string.Empty)
                                {
                                    error += "部件号不能为空；";
                                }
                                else
                                {
                                    row["partNumber"] = r[0].ToString().Trim();
                                }
                                //custPo的长度允许最长20个字符，否则截取
                                if (r[1].ToString().Trim().Length == 0)
                                {
                                    error += "部件描述不能为空；";
                                }
                                else
                                {
                                    row["partdesc"] = r[1].ToString().Trim();
                                }

                                //hrdReqDate的长度允许最长10个字符，否则截取
                                if (r[2].ToString().Trim().Length ==0 )
                                {
                                    error += "部件分类不能为空；";
                                }
                                else
                                {
                                    if (r[2].ToString().Trim().Length > 10)
                                    {
                                        row["category"] = r[2].ToString().Trim().Substring(0, 10);
                                    }
                                    else
                                    {
                                        row["category"] = r[2].ToString().Trim();
                                    }
                                }

                                //hrdDueDate的长度允许最长10个字符，否则截取
                                if (r[3].ToString().Trim().Length ==0)
                                {
                                    error += "状态(0:可用 1:试用 2:停用)不能为空；";
                                }
                                else if (r[3].ToString().Trim() != "0" && r[3].ToString().Trim() != "1" &&  r[3].ToString().Trim() != "2")
                                {
                                    error += "状态(0:可用 1:试用 2:停用)必须为0，1，2；";
                                }
                                else
                                {

                                    row["status"] = r[3].ToString().Trim();
                                    
                                }

                                //shipTo的长度允许最长20个字符，否则截取
                                if (r[4].ToString().Trim().Length == 0)
                                {
                                    row["min_inv"] = 0;
                                }
                                else
                                {
                                    if (!IsNumber(r[4].ToString().Trim()))
                                    {
                                        error += "最小库存量必须是数字；";
                                    }
                                    else
                                    {
                                        row["min_inv"] = r[4].ToString().Trim();
                                    }
                                }
                                //shipTo的长度允许最长20个字符，否则截取
                                if (r[5].ToString().Trim().Length > 5)
                                {
                                    row["unit"] = r[5].ToString().Trim().Substring(0, 5);
                                }
                                else
                                {
                                    row["unit"] = r[5].ToString().Trim();
                                }

                                //channel的长度允许最长8个字符，否则截取
                                if (r[6].ToString().Trim().Length > 5)
                                {
                                    row["tranunit"] = r[6].ToString().Trim().Substring(0, 5);
                                }
                                else
                                {
                                    row["tranunit"] = r[6].ToString().Trim();
                                }
                                //hrdRmks的长度允许最长50个字符，否则截取
                                if (r[7].ToString().Trim().Length == 0)
                                {
                                    row["tranrate"] = 0;
                                }

                                else if (!IsNumber(r[7].ToString().Trim()))
                                {
                                    error += "转换系数必须是数字；";
                                }
                                else if (Convert.ToDouble(r[7].ToString().Trim())<0)
                                {
                                    error += "转换系数不能小于零；";
                                }
                                else if (r[7].ToString().Trim().Length > 50)
                                {
                                    row["tranrate"] = r[7].ToString().Trim().Substring(0, 50);
                                }
                                else
                                {
                                    row["tranrate"] = r[7].ToString().Trim();
                                }

                                //line的长度允许最长4个字符，否则截取
                                if (r[8].ToString().Trim().Length == 0)
                                {
                                    error += "QAD零件号不能为空；";
                                }
                                else
                                {
                                    if (r[8].ToString().Trim().Length > 14)
                                    {
                                        row["xxptmt_part"] = r[8].ToString().Trim().Substring(0,14);
                                    }
                                    else
                                    {
                                       row["xxptmt_part"] = r[8].ToString().Trim();
                                    }
                                   
                                }

                                //custPart的长度允许最长20个字符，否则截取
                                if (r[9].ToString().Trim().Length > 24)
                                {
                                     row["xxptmt_desc1"] = r[9].ToString().Trim().Substring(0,24);
                                }
                                else
                                {
                                     row["xxptmt_desc1"] = r[9].ToString().Trim();
                                }
                               
                               

                                //Qad的长度允许最长15个字符，否则截取
                                if (r[10].ToString().Trim().Length > 24)
                                {
                                     row["xxptmt_desc2"] = r[10].ToString().Trim().Substring(0, 24);
                                }
                                else
                                {
                                    row["xxptmt_desc2"] = r[10].ToString().Trim();
                                }
                               
                               

                                //ordQty的长度允许最长15个字符，否则截取
                                if (r[11].ToString().Trim().Length == 0)
                                {
                                    error += "单位-um不能为空；";
                                }
                                else
                                {
                                    if (r[11].ToString().Trim().Length > 2)
                                        {
                                              row["xxptmt_um"] = r[11].ToString().Trim().Substring(0,2);
                                        }
                                        else
                                        {
                                               row["xxptmt_um"] = r[11].ToString().Trim();
                                        }
                                 
                                }

                                //um的长度允许最长5个字符，否则截取
                                //if (r[12].ToString().Trim().Length ==0 )
                                //{
                                //    error += "设计组-dsgn不能为空；";
                                //}
                                //else
                                //{
                                    if (r[12].ToString().Trim().Length > 8)
                                {
                                     row["xxptmt_dsgn"] = r[12].ToString().Trim().Substring(0, 8);
                                }
                                else
                                {
                                    row["xxptmt_dsgn"] = r[12].ToString().Trim();
                                }
                                    
                                //}

                                //price的长度允许最长15个字符，否则截取
                                //if (r[13].ToString().Trim().Length ==0 )
                                //{   
                                //    error += "推销组-promo不能为空；";
                                //}
                                //else
                                //{
                                    if (r[13].ToString().Trim().Length > 10)
                                {
                                     row["xxptmt_promo"] = r[13].ToString().Trim().Substring(0, 10);
                                }
                                else
                                {
                                     row["xxptmt_promo"] = r[13].ToString().Trim();
                                }
                                  
                                //}

                                //detReqDate的长度允许最长10个字符，否则截取
                                if (r[14].ToString().Trim().Length == 0 )
                                {
                                    error += "产品类型-parttype不能为空；";
                                }
                                else
                                {
                                    if (r[14].ToString().Trim().Length > 8)
                                {
                                    row["xxptmt_parttype"] = r[14].ToString().Trim().Substring(0, 8);
                                }
                                else
                                {
                                   row["xxptmt_parttype"] = r[14].ToString().Trim();
                                }
                                    
                                   
                                }

                                //detDueDate的长度允许最长10个字符，否则截取
                                if (r[15].ToString().Trim().Length==0)
                                {
                                    error += "状态-QADstatus不能为空；";
                                }
                                else
                                {
                                    if (r[15].ToString().Trim().Length > 8)
                                {
                                    row["xxptmt_status"] = r[15].ToString().Trim().Substring(0, 8);
                                }
                                else
                                {
                                     row["xxptmt_status"] = r[15].ToString().Trim();
                                }
                                   
                                    
                                }

                                //detRmks的长度允许最长50个字符，否则截取
                                if (r[16].ToString().Trim().Length ==0)
                                {
                                    error += "组-group不能为空；";
                                }
                                else
                                {
                                    
                                    if (r[16].ToString().Trim().Length > 8)
                                        {
                                            row["xxptmt_group"] = r[16].ToString().Trim().Substring(0, 8);
                                        }
                                        else
                                        {
                                            row["xxptmt_group"] = r[16].ToString().Trim();
                                        }
                                   
                                 }

                                //if (r[17].ToString().Trim().Length ==0)
                                //{
                                //    //error += "图纸-draw不能为空；";
                                //}
                                //else
                                //{   
                                    if (r[17].ToString().Trim().Length > 18)
                                        {
                                             row["xxptmt_draw"] = r[17].ToString().Trim().Substring(0, 18);
                                        }
                                        else
                                        {
                                            row["xxptmt_draw"] = r[17].ToString().Trim();
                                        }
                                   
                                   
                                //}

                                //if (r[18].ToString().Trim().Length == 0)
                                //{
                                //    //error += "版本-rev不能为空；";
                                //}
                                //else
                                //{   
                                    if (r[18].ToString().Trim().Length > 4)
                                        {
                                             row["xxptmt_rev"] = r[18].ToString().Trim().Substring(0, 4);
                                        }
                                        else
                                        {
                                             row["xxptmt_rev"] = r[18].ToString().Trim();
                                        }
                                   
                                   
                                //}

                                //if (r[19].ToString().Trim().Length == 0)
                                //{
                                //    //error += "图纸位置-drwg不能为空；";
                                //}
                                //else
                                //{   
                                    if (r[19].ToString().Trim().Length > 18)
                                        {
                                             row["xxptmt_drwg"] = r[19].ToString().Trim().Substring(0, 18);
                                        }
                                        else
                                        {
                                            row["xxptmt_drwg"] = r[19].ToString().Trim();
                                        }
                                   
                                   
                               // }
                                if (r[20].ToString().Trim().Length == 0)
                                {
                                    error += "库位-loc不能为空；";
                                }
                                else
                                {   
                                    if (r[20].ToString().Trim().Length > 8)
                                        {
                                             row["xxptmt_loc"] = r[20].ToString().Trim().Substring(0, 8);
                                        }
                                        else
                                        {
                                            row["xxptmt_loc"] = r[20].ToString().Trim();
                                        }
                                   
                                   
                                }
                                if (r[21].ToString().Trim().Length == 0)
                                {
                                    error += "发放原则-isspol不能为空；";
                                }
                                else if (r[21].ToString().Trim().ToLower() != "y" && r[21].ToString().Trim().ToLower() != "n")
                                {
                                    error += "发放原则-isspol只能为Y,N；Y为发料，N为无需发料；";
                                }
                                else if (r[21].ToString().Trim().ToLower() == "y")
                                {
                                    row["xxptmt_isspol"] = "True";
                                }
                                else//---bit
                                {
                                    row["xxptmt_isspol"] = "False";
                                }


                                //if (r[22].ToString().Trim().Length == 0)
                                //{
                                //    error += "采购员-buyer不能为空；";
                                //}
                                //else
                                //{   
                                    if (r[22].ToString().Trim().Length > 8)
                                        {
                                              row["xxptmt_buyer"] = r[22].ToString().Trim().Substring(0, 8);
                                        }
                                        else
                                        {
                                            row["xxptmt_buyer"] = r[22].ToString().Trim();
                                        }
                                   
                                  
                               // }

                                if (r[23].ToString().Trim().Length == 0)
                                {
                                    error += "供应商-vend不能为空；";
                                }
                                else
                                {   
                                    if (r[23].ToString().Trim().Length > 8)
                                        {
                                             row["xxptmt_vend"] = r[23].ToString().Trim().Substring(0, 8);
                                        }
                                        else
                                        {
                                            row["xxptmt_vend"] = r[23].ToString().Trim();
                                        }
                                   
                                   
                                }
                                if (r[24].ToString().Trim().Length == 0)
                                {
                                    error += "采制代码-pmcode不能为空；";
                                }
                                else
                                {   
                                    if (r[24].ToString().Trim().Length > 1)
                                        {
                                            row["xxptmt_pmcode"] = r[24].ToString().Trim().Substring(0, 1);
                                        }
                                        else
                                        {
                                           row["xxptmt_pmcode"] = r[24].ToString().Trim();
                                        }
                                   
                                    
                                }
                                if (r[25].ToString().Trim().Length == 0)
                                {
                                    error += "制造提前期-mfglead不能为空；";
                                }
                                else if (!IsNumber(r[25].ToString().Trim()))
                                {
                                    error += "制造提前期-mfglead必须是数字；";
                                }
                                else
                                {   
                                    if (r[25].ToString().Trim().Length > 8)
                                        {
                                             row["xxptmt_mfglead"] = r[25].ToString().Trim().Substring(0, 3);//int
                                        }
                                        else
                                        {
                                             row["xxptmt_mfglead"] = r[25].ToString().Trim();//int
                                        }
                                   
                                   
                                }
                                if (r[26].ToString().Trim().Length == 0)
                                {
                                    error += "采购提前期-purlead不能为空；";
                                }
                                else if (!IsNumber(r[26].ToString().Trim()))
                                {
                                    error += "采购提前期-purlead必须是数字；";
                                }
                                else
                                {   
                                    if (r[26].ToString().Trim().Length > 14)
                                        {
                                             row["xxptmt_purlead"] = r[26].ToString().Trim().Substring(0, 14);//int
                                        }
                                        else
                                        {
                                            row["xxptmt_purlead"] = r[26].ToString().Trim();//int
                                        }
                                   
                                   
                                }
                                if (r[27].ToString().Trim().Length == 0)
                                {
                                    error += "虚实件-phantom不能为空；";
                                }
                                else if (r[27].ToString().Trim().ToLower() != "y" && r[27].ToString().Trim().ToLower() != "n")
                                {
                                    error += "虚实件-phantom只能为Y,N,或空，Y为实件，N为虚件；";
                                }
                                else if (r[27].ToString().Trim().ToLower() == "y")
                                {
                                    row["xxptmt_phantom"] = "True";
                                }
                                else//---bit
                                {
                                    row["xxptmt_phantom"] = "False";
                                }
                              
                                if (r[28].ToString().Trim().Length == 0)
                                {
                                    error += "域-domain不能为空；";
                                }
                                else
                                {   
                                    if (r[28].ToString().Trim().Length > 40)
                                        {
                                            row["xxptmt_domain"] = r[28].ToString().Trim().Substring(0, 40);
                                        }
                                        else
                                        {
                                           row["xxptmt_domain"] = r[28].ToString().Trim();
                                        }
                                   
                                    
                                }
                               


                                
                                #endregion

                                row["createBy"] = _uID;
                                if (error == "")
                                {
                                    row["errMsg"] = string.Empty;
                                }
                                else
                                {
                                    row["errMsg"] = error;
                                }

                                table.Rows.Add(row);
                            }
                        }

                        //table有数据的情况下
                        if (table != null && table.Rows.Count > 0)
                        {
                            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(chk.dsn0()))
                            {
                                bulkCopy.DestinationTableName = "dbo.part_temp";
                                bulkCopy.ColumnMappings.Add("partNumber", "partNumber");
                                bulkCopy.ColumnMappings.Add("partdesc", "partdesc");
                                bulkCopy.ColumnMappings.Add("category", "category");
                                bulkCopy.ColumnMappings.Add("status", "status");
                                bulkCopy.ColumnMappings.Add("min_inv", "min_inv");
                                bulkCopy.ColumnMappings.Add("unit", "unit");
                                bulkCopy.ColumnMappings.Add("tranunit", "tranunit");
                                bulkCopy.ColumnMappings.Add("tranrate", "tranrate");
                                bulkCopy.ColumnMappings.Add("xxptmt_part", "xxptmt_part");
                                bulkCopy.ColumnMappings.Add("xxptmt_um", "xxptmt_um");
                                bulkCopy.ColumnMappings.Add("xxptmt_desc1", "xxptmt_desc1");
                                bulkCopy.ColumnMappings.Add("xxptmt_desc2", "xxptmt_desc2");
                                bulkCopy.ColumnMappings.Add("xxptmt_prodline", "xxptmt_prodline");
                                bulkCopy.ColumnMappings.Add("xxptmt_dsgn", "xxptmt_dsgn");
                                bulkCopy.ColumnMappings.Add("xxptmt_promo", "xxptmt_promo");
                                bulkCopy.ColumnMappings.Add("xxptmt_parttype", "xxptmt_parttype");
                                bulkCopy.ColumnMappings.Add("xxptmt_status", "xxptmt_status");
                                bulkCopy.ColumnMappings.Add("xxptmt_group", "xxptmt_group");
                                bulkCopy.ColumnMappings.Add("xxptmt_draw", "xxptmt_draw");
                                bulkCopy.ColumnMappings.Add("xxptmt_rev", "xxptmt_rev");

                                bulkCopy.ColumnMappings.Add("xxptmt_drwg", "xxptmt_drwg");
                                bulkCopy.ColumnMappings.Add("xxptmt_abc", "xxptmt_abc");
                                bulkCopy.ColumnMappings.Add("xxptmt_loc", "xxptmt_loc");
                                bulkCopy.ColumnMappings.Add("xxptmt_article", "xxptmt_article");
                                bulkCopy.ColumnMappings.Add("xxptmt_ordper", "xxptmt_ordper");

                                bulkCopy.ColumnMappings.Add("xxptmt_sftystk", "xxptmt_sftystk");
                                bulkCopy.ColumnMappings.Add("xxptmt_sftytime", "xxptmt_sftytime");
                                bulkCopy.ColumnMappings.Add("xxptmt_isspol", "xxptmt_isspol");
                                bulkCopy.ColumnMappings.Add("xxptmt_buyer", "xxptmt_buyer");
                                bulkCopy.ColumnMappings.Add("xxptmt_vend", "xxptmt_vend");
                                bulkCopy.ColumnMappings.Add("xxptmt_pmcode", "xxptmt_pmcode");
                                bulkCopy.ColumnMappings.Add("xxptmt_mfglead", "xxptmt_mfglead");
                                bulkCopy.ColumnMappings.Add("xxptmt_purlead", "xxptmt_purlead");
                                bulkCopy.ColumnMappings.Add("xxptmt_phantom", "xxptmt_phantom");
                                bulkCopy.ColumnMappings.Add("xxptmt_yield", "xxptmt_yield");
                                bulkCopy.ColumnMappings.Add("xxptmt_domain", "xxptmt_domain");

                                bulkCopy.ColumnMappings.Add("createBy", "createBy");
                                bulkCopy.ColumnMappings.Add("errMsg", "errMsg");



                                try
                                {
                                    bulkCopy.WriteToServer(table);
                                }
                                catch (Exception ex)
                                {
                                    ltlAlert.Text = "alert('Operation fails!Please try again!');";

                                    return false;
                                }
                                finally
                                {
                                    table.Dispose();
                                }
                            }
                        }
                    }
                }

                dt.Reset();

                if (File.Exists(strFileName))
                {
                    File.Delete(strFileName);
                }

            }
        }

        return true;
    }
    protected void btnRouting_Click(object sender, EventArgs e)
    {
        if (Session["uID"] == null)
        {
            return;
        }

        if (ImportExcelFile())
        {
            if (!CheckValidity(Session["uID"].ToString()))
            {
                if (InsertBatchTemp(Session["uID"].ToString(), Session["uName"].ToString(), Session["plantCode"].ToString()))
                {
                    QadService.WebService1SoapClient client = new QadService.WebService1SoapClient();
                    client.Product_Add_Submit();
                    ltlAlert.Text = "alert('success!');";
                }
                else
                {
                    ltlAlert.Text = "alert('fail!');";
                }
            }
            else
            {
                string title = "100^<b>部件号</b>~^100^<b>部件描述</b>~^100^<b>部件分类 </b>~^100^<b>状态(0:可用 1:试用 2:停用)</b>~^100^<b>最小库存量</b>~^100^<b>单位(可空)</b>~^100^<b>转换前单位(可空)</b>~^100^<b>QAD零件号 </b>~^100^<b>QAD描述1 </b>~^100^<b>QAD描述2 </b>~^100^<b>单位-um </b>~^100^<b>设计组-dsgn </b>~^100^<b>推销组-promo </b>~^100^<b>产品类型-parttype </b>~^100^<b>状态-QADstatus </b>~^100^<b>组-group </b>~^100^<b>图纸-draw</b>~^100^<b>版本-rev </b>~^100^<b>图纸位置-drwg </b>~^100^<b>库位-loc </b>~^100^<b>发放原则-isspol </b>~^100^<b>采购员-buyer </b>~^100^<b>供应商-vend </b>~^100^<b>采制代码-pmcode </b>~^100^<b>制造提前期-mfglead</b>~^100^<b>采购提前期-purlead </b>~^100^<b>虚实件-phantom </b>~^100^<b>域-domain </b>~^100^<b>错误信息</b>~^";

                string sql = " select partNumber,[partdesc],[category],[status],[min_inv],[unit],[tranunit],[tranrate],  xxptmt_part,xxptmt_desc1,xxptmt_desc2,xxptmt_um,xxptmt_dsgn,xxptmt_promo,xxptmt_parttype,xxptmt_status,xxptmt_group,xxptmt_draw,xxptmt_rev,xxptmt_drwg,xxptmt_loc,xxptmt_isspol,xxptmt_buyer,xxptmt_vend,xxptmt_pmcode,xxptmt_mfglead,xxptmt_purlead,xxptmt_phantom,xxptmt_domain,errMsg from part_temp  where createBy =  " + Session["uID"].ToString();

                DataTable dt = SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.Text, sql).Tables[0];
                //ltlAlert.Text = "alert('导入失败!');";
                ExportExcel(title, dt, false);
               
            }
        }
    }
}
