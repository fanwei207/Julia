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

public partial class Purchase_vm_mstrImport : BasePage
{
    adamClass chk = new adamClass();
    Mold mold = new Mold();
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }
    protected void btn_Import_Click(object sender, EventArgs e)
    {
        if(ImportExcelFile())
            ltlAlert.Text = "alert('导入成功！')";
    }
        public Boolean ImportExcelFile()
    {
        //DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        String strFileName = "";
        String strCatFolder = "";
        String strUserFileName = "";
        int intLastBackslash = 0;

        #region 上传文档例行处理
        strCatFolder = Server.MapPath("/import");

        if (!System.IO.Directory.Exists(strCatFolder))
        {
            try
            {
                System.IO.Directory.CreateDirectory(strCatFolder);
            }
            catch
            {
                ltlAlert.Text = "alert('上传文件失败.');";
                return false;
            }

        }

        strUserFileName = filename1.PostedFile.FileName;
        intLastBackslash = strUserFileName.LastIndexOf("\\");
        strFileName = strUserFileName.Substring(intLastBackslash + 1);
        if (strFileName.Trim().Length <= 0)
        {
            ltlAlert.Text = "alert('请选择导入文件.');";
            return false;
        }

        strUserFileName = strFileName;

        strFileName = strCatFolder + "\\" + DateTime.Now.ToString("yyyyMMddhhmmssfff") + strFileName;
        #endregion
        if (filename1.PostedFile != null)
        {
            if (filename1.PostedFile.ContentLength > 8388608)
            {
                ltlAlert.Text = "alert('上传的文件最大为 8 MB!');";
                return false;
            }

            try
            {
                filename1.PostedFile.SaveAs(strFileName);
            }
            catch
            {
                ltlAlert.Text = "alert('上传文件失败.');";
                return false;
            }

            if (File.Exists(strFileName))
            {
                try
                {
                    //ds = chk.getExcelContents(strFileName);
                    dt = this.GetExcelContents(strFileName);
                }
                catch (Exception e)
                {

                    if (File.Exists(strFileName))
                    {
                        File.Delete(strFileName);
                    }

                    ltlAlert.Text = "alert('导入文件必须是Excel格式!');";
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
                    
                    if (dt.Columns.Count != 9)
                    {
                        ////ds.Reset();
                        ltlAlert.Text = "alert('该文件必须有9列！');";
                        return false;
                    }

                    #region Excel的列名必须保持一致
                    for (int col = 0; col < dt.Columns.Count; col++)
                    {
                        if (col == 0 && dt.Columns[col].ColumnName.Trim() != "供应商代码")
                        {
                            //ds.Reset();
                            ltlAlert.Text = "alert('该文件的第" + col.ToString() + "列必须是 供应商代码！');";
                            return false;
                        }

                        if (col == 1 && dt.Columns[col].ColumnName.Trim() != "供应商名称")
                        {
                            //ds.Reset();
                            ltlAlert.Text = "alert('该文件的第" + col.ToString() + "列必须是 供应商名称！');";
                            return false;
                        }

                        if (col == 2 && dt.Columns[col].ColumnName.Trim() != "模具编号")
                        {
                            //ds.Reset();
                            ltlAlert.Text = "alert('该文件的第" + col.ToString() + "列必须是 模具编号！');";
                            return false;
                        }

                        if (col == 3 && dt.Columns[col].ColumnName.Trim() != "模具产量")
                        {
                            //ds.Reset();
                            ltlAlert.Text = "alert('该文件的第" + col.ToString() + "列必须是 模具产量！');";
                            return false;
                        }
                        if (col == 4 && dt.Columns[col].ColumnName.Trim() != "状态")
                        {
                            //ds.Reset();
                            ltlAlert.Text = "alert('该文件的第" + col.ToString() + "列必须是 状态！');";
                            return false;
                        }
                        if (col == 5 && dt.Columns[col].ColumnName.Trim() != "零件QAD号")
                        {
                            //ds.Reset();
                            ltlAlert.Text = "alert('该文件的第" + col.ToString() + "列必须是 零件QAD号！');";
                            return false;
                        }

                        if (col == 6 && dt.Columns[col].ColumnName.Trim() != "模具型腔")
                        {
                            //ds.Reset();
                            ltlAlert.Text = "alert('该文件的第" + col.ToString() + "列必须是 模具型腔！');";
                            return false;
                        }

                        if (col == 7 && dt.Columns[col].ColumnName.Trim() != "图纸图号")
                        {
                            //ds.Reset();
                            ltlAlert.Text = "alert('该文件的第" + col.ToString() + "列必须是 图纸图号');";
                            return false;
                        }
                        if (col == 8 && dt.Columns[col].ColumnName.Trim() != "备注")
                        {
                            //ds.Reset();
                            ltlAlert.Text = "alert('该文件的第" + col.ToString() + "列必须是 备注！');";
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
                    column.ColumnName = "vm_vendCode";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "vm_vendName";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "vm_moldCode";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.Int32");
                    column.ColumnName = "vm_moldQty";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.Int32");
                    column.ColumnName = "vm_status";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "vm_QAD";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "vm_Cavity";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "vm_drawingID";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "vm_remark";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.Int32");
                    column.ColumnName = "vm_createBy";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.DateTime");
                    column.ColumnName = "vm_createDate";
                    table.Columns.Add(column);


                    #endregion
                    #region Temp
                    int _uID = Convert.ToInt32(Session["uID"].ToString());
                    int intRow=0;
                    foreach (DataRow r in dt.Rows)
                    {
                        row = table.NewRow();
                        intRow=intRow+1;
                        #region 赋值、长度判定

                        //vm_vendCode的长度允许最长50个字符，否则截取
                        if (r[0].ToString().Length > 50)
                        {
                            row["vm_vendCode"] = r[0].ToString().Substring(0, 50);
                        }
                        else
                        {
                            row["vm_vendCode"] = r[0].ToString();
                        }

                        //vm_vendName的长度允许最长200个字符，否则截取
                        if (r[1].ToString().Length > 200)
                        {
                            row["vm_vendName"] = r[1].ToString().Substring(0, 200);
                        }
                        else
                        {
                            row["vm_vendName"] = r[1].ToString();
                        }

                        if (!mold.chkVendCode(row["vm_vendCode"].ToString(), row["vm_vendName"].ToString()))
                        {
                            ltlAlert.Text = "alert('该文件"+intRow+"行的供应商编号和名称不匹配！');";

                            return false;
                        }

                        //vm_moldCode的长度允许最长30个字符，否则截取
                        if (r[2].ToString().Length > 30)
                        {
                            row["vm_moldCode"] = r[2].ToString().Substring(0, 30);
                        }
                        else
                        {
                            row["vm_moldCode"] = r[2].ToString();
                        }

                        //vm_moldQty必须为数字
                        try
                        {
                            if (r[3].ToString().IndexOf(".") >= 0)
                            {
                                ltlAlert.Text = "alert('该文件" + intRow + "行的模具数量不是整数！');";
                                return false;
                            }

                            row["vm_moldQty"] =Convert.ToInt32(r[3]);

                            if (Convert.ToInt32(row["vm_moldQty"])<0)
                            {
                                ltlAlert.Text = "alert('该文件" + intRow + "行的模具数量是负数！');";
                                return false;
                            }

                            
                        }
                        catch
                        {
                            ltlAlert.Text = "alert('该文件" + intRow + "行的模具数量不是数字！');";
                            return false;
                        }
                        //vm_status只允许为"1"或"2"
                        if ( r[4].ToString()=="1" || r[4].ToString()=="2" )
                        {
                            try
                            { 
                                row["vm_status"] = Convert.ToInt32(r[4]); 
                            }
                            catch
                            {
                                ltlAlert.Text = "alert('该文件" + intRow + "行的状态不是数字！');";
                                return false;
                            }
                        }
                        else
                        {
                            ltlAlert.Text = "alert('该文件" + intRow + "行的状态不是数字1或2！');";
                            return false;
                        }

                        //vm_QAD的长度允许必须是14个字符，否则截取
                        if (r[5].ToString().Length != 14)
                        {
                            row["vm_QAD"] = r[5].ToString().Substring(0, 14);
                        }
                        else
                        {
                            row["vm_QAD"] = r[5].ToString();
                        }

                        //vm_Cavity的长度允许最长50个字符，否则截取
                        if (r[6].ToString().Length > 50)
                        {
                            row["vm_Cavity"] = r[6].ToString().Substring(0, 50);
                        }
                        else
                        {
                            row["vm_Cavity"] = r[6].ToString();
                        }

                        //vm_drawingID的长度允许最长50个字符，否则截取
                        if (r[7].ToString().Length > 50)
                        {
                            row["vm_drawingID"] = r[7].ToString().Substring(0, 50);
                        }
                        else
                        {
                            row["vm_drawingID"] = r[7].ToString();
                        }

                        //vm_remark的长度允许最长250个字符，否则截取
                        if (r[8].ToString().Length > 250)
                        {
                            row["vm_remark"] = r[8].ToString().Substring(0, 250);
                        }
                        else
                        {
                            row["vm_remark"] = r[8].ToString();
                        }

                        #endregion

                        row["vm_createBy"] = _uID;
                        row["vm_createDate"] = DateTime.Now;

                        table.Rows.Add(row);
                    }
                    #endregion
                    #region 导入数据
                    //table有数据的情况下
                    if (table != null && table.Rows.Count > 0)
                    {
                        using (SqlBulkCopy bulkCopy = new SqlBulkCopy(admClass.getConnectString("SqlConn.qadplan")))
                        {
                            bulkCopy.DestinationTableName = "dbo.vm_mstr";
                            bulkCopy.ColumnMappings.Add("vm_vendCode", "vm_vendCode");
                            bulkCopy.ColumnMappings.Add("vm_vendName", "vm_vendName");
                            bulkCopy.ColumnMappings.Add("vm_moldCode", "vm_moldCode");
                            bulkCopy.ColumnMappings.Add("vm_moldQty", "vm_moldQty");
                            bulkCopy.ColumnMappings.Add("vm_status", "vm_status");
                            bulkCopy.ColumnMappings.Add("vm_QAD", "vm_QAD");
                            bulkCopy.ColumnMappings.Add("vm_Cavity", "vm_Cavity");
                            bulkCopy.ColumnMappings.Add("vm_drawingID", "vm_drawingID");
                            bulkCopy.ColumnMappings.Add("vm_remark", "vm_remark");
                            bulkCopy.ColumnMappings.Add("vm_createBy", "vm_createBy");
                            bulkCopy.ColumnMappings.Add("vm_createDate", "vm_createDate");

                            try
                            {
                                bulkCopy.WriteToServer(table);
                            }
                            catch (Exception ex)
                            {
                                ltlAlert.Text = "alert('导入时出错，请联系系统管理员！');";

                                return false;
                            }
                            finally
                            {
                                table.Dispose();
                            }
                        }
                    }
                    #endregion    
                }

                //ds.Reset();

                if (File.Exists(strFileName))
                {
                    File.Delete(strFileName);
                }

            }
        }

        return true;
    }
    protected void btn_back_Click(object sender, EventArgs e)
    {
        Response.Redirect("vm_mstrList.aspx");
    }
    
}
