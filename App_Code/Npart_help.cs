using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System.Net.Mail;
using System.Configuration;


/// <summary>
/// Summary description for Npart_help
/// </summary>
public class Npart_help
{

    adamClass adam = new adamClass();

    //guid生成函数
    private string getGUID()
    {
        System.Guid guid = new Guid();
        guid = Guid.NewGuid();
        string str = guid.ToString();
        return str;
    }


    public Npart_help()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    /// <summary>
    /// 用于查询所有的零件模板，包括创建人和可否删改
    /// </summary>
    /// <returns></returns>
    public DataTable selectAllTypeMstr()
    {
        string sqlstr = "sp_Npart_selectAllTypeMstr";

        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, sqlstr).Tables[0];
    }

    public DataTable selectAllTypeMstr(string TypeName)
    {
        string sqlstr = "sp_Npart_selectAllTypeMstr";

        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@TypeName",TypeName)
            
            };

        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param).Tables[0];
    }

    public DataTable selectAllTypeDetByMstrID(string typeID)
    {
        string sqlstr = "sp_Npart_selectAllTypeDetByMstrID";

        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@typeID",typeID)
            
            };

        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, sqlstr,param).Tables[0];
    }

    public string deleteTypeMstrByID(string mstrID)
    {
        string sqlstr = "sp_Npart_deleteTypeMstrByID";

        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@mstrID",mstrID)
            
            };

        return SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param).ToString();
    }

    public int createNewType(string typeName, string applyType ,string uID, string uName)
    {
        string sqlstr = "sp_Npart_createNewType";

        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@typeName",typeName)
            ,new SqlParameter("@applyType",applyType)
            ,new SqlParameter("@uID",uID)   
            ,new SqlParameter("@uName",uName)
            };

        return Convert.ToInt32(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, sqlstr,param).ToString());
    }



    public DataTable selectTypeMstrByID(string mstrID)
    {
        string sqlstr = "sp_Npart_selectTypeMstrByID";

        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@mstrID",mstrID)
            
            };

        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param).Tables[0];
    }

    public string selectAllTypeDetByMstrIDReturnString(string typeID)
    {
        string sqlstr = "sp_Npart_selectAllTypeDetByMstrIDReturnString";

        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@typeID",typeID)
            
            };

        return SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param).ToString();
    }

    public string deleteTypeDetByID(string mstrID, string detID)
    {
        string sqlstr = "sp_Npart_deleteTypeDetByID";

        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@mstrID",mstrID)
            ,new SqlParameter("@detID",detID)
            };

        return SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param).ToString();
    }

    public string addTypeDet(string mstrID, string colname, string colEngilshName, string prefix, string srefix, string sort, int isnum, int isDate, int isEnum, int isSpace,string uID, string uName)
    {
        string sqlstr = "sp_Npart_addTypeDet";

        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@mstrID",mstrID)
            ,new SqlParameter("@colname",colname)
            ,new SqlParameter("@colEngilshName",colEngilshName)
            ,new SqlParameter("@prefix",prefix)
            ,new SqlParameter("@srefix",srefix)
            ,new SqlParameter("@sort",sort)
            ,new SqlParameter("@isnum",isnum)
            ,new SqlParameter("@isDate",isDate)
            ,new SqlParameter("@isSpace",isSpace)
            ,new SqlParameter("@isEnum",isEnum)
            ,new SqlParameter("@uID",uID)
             ,new SqlParameter("@uName",uName)
            };

        return SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param).ToString();
    }

    public string modifiyTypeDet(string mstrID, string detID, string colname, string colEngilshName, string prefix, string srefix, string sort, int isnum, int isDate, int isEnum, int isSpace,string uID, string uName)
    {
        string sqlstr = "sp_Npart_modifiyTypeDet";

        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@mstrID",mstrID)
            ,new SqlParameter("@detID",detID)
            ,new SqlParameter("@colname",colname)
            ,new SqlParameter("@colEngilshName",colEngilshName)
             ,new SqlParameter("@prefix",prefix)
            ,new SqlParameter("@srefix",srefix)
            ,new SqlParameter("@sort",sort)
            ,new SqlParameter("@isnum",isnum)
            ,new SqlParameter("@isDate",isDate)
            ,new SqlParameter("@isEnum",isEnum)
             ,new SqlParameter("@isSpace",isSpace)
            ,new SqlParameter("@uID",uID)
             ,new SqlParameter("@uName",uName)
            };

        return SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param).ToString();
    }

    public DataTable selectTypeQADByMstrID(string mstrID)
    {
        string sqlstr = "sp_Npart_selectTypeQADByMstrID";

        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@mstrID",mstrID)
            
            };

        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param).Tables[0];
    }

    public string deleteTypeForQADByID(string detID, string uID, string uName)
    {
        string sqlstr = "sp_Npart_deleteTypeForQADByID";

        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@detID",detID)
            ,new SqlParameter("@uID",uID)
            ,new SqlParameter("@uName",uName)
            };

        return SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param).ToString();
    }

    public string addTypeForQAD(string mstrID, string typeName, string typeRepName,string typeNameEnglish, string QADin, string uID, string uName)
    {
        string sqlstr = "sp_Npart_addTypeForQAD";

        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@mstrID",mstrID)
            ,new SqlParameter("@typeName",typeName)
            ,new SqlParameter("@typeRepName",typeRepName)
            ,new SqlParameter("@typeNameEnglish",typeNameEnglish)
            ,new SqlParameter("@QADin",QADin)
            ,new SqlParameter("@uID",uID)
             ,new SqlParameter("@uName",uName)
            };

        return SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param).ToString();
    }

    public DataTable selectTypeDetEnumByID(string mstrID, string detID)
    {
        string sqlstr = "sp_Npart_selectTypeDetEnumByID";

        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@mstrID",mstrID)
            ,new SqlParameter("@detID",detID)
            
            };

        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param).Tables[0];
    }

    public string deleteTypeDetEnumByID(string ID, string uID, string uName)
    {
        string sqlstr = "sp_Npart_deleteTypeDetEnumByID";

        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@ID",ID)
            ,new SqlParameter("@uID",uID)
            ,new SqlParameter("@uName",uName)
            };

        return SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, sqlstr,param).ToString();
    }

    public bool importPartForType(DataTable dt, string typeID, out string message, string uID, string uName, out DataTable errorDt)
    {
        message = "";
        errorDt = null;
        dt.TableName = "TempTable";
        bool success = true;

        DataTable dtDetHead = selectAllTypeDetByMstrID(typeID);

        if (dtDetHead.Rows.Count <= 0)
        {
            message = "模板没有创建好，请创建";
            success = false;
        }
        else
        {
            for (int i = 0; i < dtDetHead.Rows.Count; i++)
            {
                if (dt.Columns[i].ColumnName != dtDetHead.Rows[i]["colName"].ToString())
                {
                    message = "导入文件的模版不正确，请更新模板再导入!";
                    dt.Reset();
                    success = false;
                }
            }
        }

        if (success)
        {
            try
            {
                
                DataTable TempTable = new DataTable("TempTable");
                DataColumn TempColumn;
                DataRow TempRow;

                foreach (DataRow dr in dtDetHead.Rows)
                {
                    TempColumn = new DataColumn();
                    TempColumn.DataType = System.Type.GetType("System.String");
                    TempColumn.ColumnName = dr["colName"].ToString();
                    TempTable.Columns.Add(TempColumn);
                }

                TempColumn = new DataColumn();
                TempColumn.DataType = System.Type.GetType("System.String");
                TempColumn.ColumnName = "错误信息";
                TempTable.Columns.Add(TempColumn);


                if (dt.Rows.Count > 0)
                {
                   
                    foreach (DataRow dr in dt.Rows)
                    {

                        if (dr[0].ToString().Equals(string.Empty) && dr[1].ToString().Equals(string.Empty) && dr[2].ToString().Equals(string.Empty) && dr[3].ToString().Equals(string.Empty)
                            && dr[4].ToString().Equals(string.Empty) && dr[5].ToString().Equals(string.Empty) && dr[6].ToString().Equals(string.Empty))
                        {
                            continue;
                        }
                        StringBuilder err = new StringBuilder();
                        TempRow = TempTable.NewRow();//创建新的行
                        for (int i = 0; i <= TempTable.Columns.Count - 2; i++)//因为多了一列错误信息所以需要-2
                        {
                            string colName = TempTable.Columns[i].ColumnName;
                            DataRow rowDesign = dtDetHead.Select("colName='" + colName + "'")[0];
                            string isnumber = rowDesign["colIsNumber"].ToString();
                            string isDate = rowDesign["colIsDate"].ToString();
                            if (!colName.Equals(string.Empty))
                            {
                                if (isnumber == "True" && !dr[colName].ToString().Equals(string.Empty))
                                {
                                    float testfloat = 0;
                                    if (!float.TryParse(dr[colName].ToString(), out testfloat))
                                    {
                                        err.Append(colName).Append("的值不是数值");
                                        success = false;
                                        break;
                                    }
                                }
                                if (isDate == "True" && !dr[colName].ToString().Equals(string.Empty))
                                {
                                    DateTime datetime;

                                    if (!DateTime.TryParse(dr[colName].ToString(), out datetime))
                                    {
                                        success = false;
                                        err.Append(colName).Append("的值不是日期;");
                                        break;
                                    }
                                }
                                TempRow[colName] = dr[colName];
                            }


                            
                        }
                        TempRow["错误信息"] = err.ToString();
                        TempTable.Rows.Add(TempRow);
                    }
                   
                        
                    

                    StringWriter writer = new StringWriter();
                    TempTable.WriteXml(writer);
                    string xmlDetail = writer.ToString();

                    string sqlstr = "sp_npart_importToApplyByTypeAndUid";

                    SqlParameter[] param = new SqlParameter[]{
                             new SqlParameter("@detail", xmlDetail)
                             , new SqlParameter("@uID",uID)
                             , new SqlParameter("@uName",uName)
                             , new SqlParameter("@typeID",typeID)
                             , new SqlParameter("@countDT",TempTable.Rows.Count)
                                 };

                    errorDt = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param).Tables[0];
                    if (errorDt.Rows.Count > 0)
                    {

                        if (errorDt.Rows[0][0].ToString().Equals("1"))
                        {
                            success = true;
                            message = "导入文件成功!";

                            string sqlstr1 = "sp_npart_typeStationaryCol";

                             SqlParameter[] param1 = new SqlParameter[]{
                              new SqlParameter("@uID",uID)
                                 , new SqlParameter("@uName",uName)
                                 , new SqlParameter("@typeID",typeID)
                              };
                             SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, sqlstr1, param1);
                        }
                        else
                        {
                            message = "导入文件失败!";
                            success = false;
                        }
                    }
                    else
                    {
                        message = "导入文件失败!";
                        success = false;
                    }

                }
            }
            catch
            {
                message = "导入文件失败!";
                success = false;
                throw new Exception();
            }

        }
        return success;
    }


    public bool importPartListForTypeSkip(DataTable dt, string typeID, out string message, string uID, string uName, out DataTable errorDt)
    {
        message = "";
        errorDt = null;
        dt.TableName = "TempTable";
        bool success = true;

        DataTable dtDetHead = getGvColByImportPartList(typeID);

        if (dtDetHead.Rows.Count <= 0)
        {
            message = "模板没有创建好，请创建";
            success = false;
        }
        else
        {
            for (int i = 0; i < dtDetHead.Rows.Count; i++)
            {
                if (dt.Columns[i].ColumnName != dtDetHead.Rows[i]["colName"].ToString())
                {
                    message = "导入文件的模版不正确，请更新模板再导入!";
                    dt.Reset();
                    success = false;
                }
            }
        }

        if (success)
        {
            try
            {

                DataTable TempTable = new DataTable("TempTable");
                DataColumn TempColumn;
                DataRow TempRow;

                foreach (DataRow dr in dtDetHead.Rows)
                {
                    TempColumn = new DataColumn();
                    TempColumn.DataType = System.Type.GetType("System.String");
                    TempColumn.ColumnName = dr["colName"].ToString();
                    TempTable.Columns.Add(TempColumn);
                }

                TempColumn = new DataColumn();
                TempColumn.DataType = System.Type.GetType("System.String");
                TempColumn.ColumnName = "错误信息";
                TempTable.Columns.Add(TempColumn);


                if (dt.Rows.Count > 0)
                {

                    foreach (DataRow dr in dt.Rows)
                    {

                        if (dr[0].ToString().Equals(string.Empty) && dr[1].ToString().Equals(string.Empty) && dr[2].ToString().Equals(string.Empty) && dr[3].ToString().Equals(string.Empty)
                            && dr[4].ToString().Equals(string.Empty) && dr[5].ToString().Equals(string.Empty) && dr[6].ToString().Equals(string.Empty))
                        {
                            continue;
                        }
                        StringBuilder err = new StringBuilder();
                        TempRow = TempTable.NewRow();//创建新的行
                        for (int i = 0; i <= TempTable.Columns.Count - 2; i++)//因为多了一列错误信息所以需要-2
                        {
                            string colName = TempTable.Columns[i].ColumnName;
                            DataRow rowDesign = dtDetHead.Select("colName='" + colName + "'")[0];
                            string isnumber = rowDesign["colIsNumber"].ToString();
                            string isDate = rowDesign["colIsDate"].ToString();
                            if (!colName.Equals(string.Empty))
                            {
                                if (isnumber == "True" && !dr[colName].ToString().Equals(string.Empty))
                                {
                                    float testfloat = 0;
                                    if (!float.TryParse(dr[colName].ToString(), out testfloat))
                                    {
                                        err.Append(colName).Append("的值不是数值");
                                        success = false;
                                        break;
                                    }
                                }
                                if (isDate == "True" && !dr[colName].ToString().Equals(string.Empty))
                                {
                                    DateTime datetime;

                                    if (!DateTime.TryParse(dr[colName].ToString(), out datetime))
                                    {
                                        success = false;
                                        err.Append(colName).Append("的值不是日期;");
                                        break;
                                    }
                                }
                                TempRow[colName] = dr[colName];
                            }



                        }
                        TempRow["错误信息"] = err.ToString();
                        TempTable.Rows.Add(TempRow);
                    }




                    StringWriter writer = new StringWriter();
                    TempTable.WriteXml(writer);
                    string xmlDetail = writer.ToString();

                    string sqlstr = "sp_npart_importToPartListByTypeAndUid";

                    SqlParameter[] param = new SqlParameter[]{
                             new SqlParameter("@detail", xmlDetail)
                             , new SqlParameter("@uID",uID)
                             , new SqlParameter("@uName",uName)
                             , new SqlParameter("@typeID",typeID)
                             , new SqlParameter("@countDT",TempTable.Rows.Count)
                                 };

                    errorDt = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param).Tables[0];
                    if (errorDt.Rows.Count > 0)
                    {

                        if (errorDt.Rows[0][0].ToString().Equals("1"))
                        {
                            success = true;
                            message = "导入文件成功!";

                           
                        }
                        else
                        {
                            message = "导入文件失败!";
                            success = false;
                        }
                    }
                    else
                    {
                        message = "导入文件失败!";
                        success = false;
                    }

                }
            }
            catch
            {
                message = "导入文件失败!";
                success = false;
                throw new Exception();
            }

        }
        return success;
    }

    public string addTypeDetEnum(string mstrID, string detID, string enumValue,string repValue, string uID, string uName)
    {
        string sqlstr = "sp_Npart_addTypeDetEnum";

        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@mstrID",mstrID)
            ,new SqlParameter("@detID",detID)
            ,new SqlParameter("@enumValue",enumValue)
            ,new SqlParameter("@repValue",repValue)
            ,new SqlParameter("@uID",uID)
             ,new SqlParameter("@uName",uName)
            };

        return SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, sqlstr,param).ToString();
    }

    private class ItemLinkbuttonTemplate : ITemplate
    {
        public void InstantiateIn(Control container)
        {
            LinkButton linkbutton = new LinkButton();
            linkbutton.ID = "lkbDelete";
            linkbutton.CommandName = "lkbDelete";
            linkbutton.Text = "删除";
            linkbutton.Font.Underline = true;
            container.Controls.Add(linkbutton);
        }
    }

    private class HeaderCheckBoxTemplate : ITemplate
    {
        public void InstantiateIn(Control container)
        {
            CheckBox ck = new CheckBox();

            ck.ID = "chkAll";
            container.Controls.Add(ck);
        }

    }

    private class ItemCheckBoxTemplate : ITemplate
    {
        public void InstantiateIn(Control container)
        {
            CheckBox ck = new CheckBox();
            ck.ID = "chk";
            container.Controls.Add(ck);
        }
    }

    public void createGridView(GridView gvDet,DataTable dt, bool choice ,bool canDelete)
    {
        gvDet.Columns.Clear();

        if (choice)
        {
            TemplateField temp = new TemplateField();
            HeaderCheckBoxTemplate header = new HeaderCheckBoxTemplate();
            ItemCheckBoxTemplate item = new ItemCheckBoxTemplate();
            temp.HeaderTemplate = header;
            temp.ItemTemplate = item;
            temp.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
            temp.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
            temp.ItemStyle.Width = 30;
            gvDet.Columns.Add(temp);
        }

        List<string> dataKeys = new List<string>();
        dataKeys.Add("id");
        foreach (DataRow row in dt.Rows)
        {
            
            BoundField col = new BoundField();
            col.DataField = row["colEnglishName"].ToString();
            col.HeaderText = row["colName"].ToString();
            if (row["item"].ToString() == "0")
            {
                dataKeys.Add(row["colEnglishName"].ToString());
            }
            gvDet.Columns.Add(col);
        }


        if (canDelete)
        {
            TemplateField temp = new TemplateField();

            ItemLinkbuttonTemplate item = new ItemLinkbuttonTemplate();
            temp.HeaderText = "删除";
            temp.ItemTemplate = item;
            temp.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
            temp.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
            temp.ItemStyle.Width = 30;
            gvDet.Columns.Insert(1, temp);
        }
        gvDet.DataKeyNames = dataKeys.ToArray();
        gvDet.AllowPaging = true;
        gvDet.PageSize = 50;

        
       
    }

    public DataTable getGvColByTypeDet(string mstrID)
    {
        string sqlstr = "sp_Npart_getGvColByTypeDet";

        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@mstrID",mstrID)
            
            };

        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param).Tables[0];
    }

    public DataTable getGvColByTypeDetForSupplier(string mstrID)
    {
        string sqlstr = "sp_Npart_getGvColByTypeDetForSupplier";

        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@mstrID",mstrID)
            
            };

        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param).Tables[0];
    }

    public DataTable getGvDataByTypeIDAndUID(string mstrID, string uID,string status)
    {
        string sqlstr = "sp_Npart_getGvDataByTypeIDAndUID";

        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@mstrID",mstrID)
            ,new SqlParameter("@uID",uID)
            ,new SqlParameter("@status",status)
            };

        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param).Tables[0];
    }

    public string commitApplyByID(DataTable tableID, string uID, string uName)
    {
        StringWriter writer = new StringWriter();
        tableID.WriteXml(writer);
        string xmlDetail = writer.ToString();

        string sqlstr = "sp_npart_commitApplyByID";

        SqlParameter[] param = new SqlParameter[]{
                             new SqlParameter("@detail", xmlDetail)
                             , new SqlParameter("@uID",uID)
                             , new SqlParameter("@uName",uName)
                           
                                 };

        return  SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param).ToString();
    }

    public string deletePartApply(string ID, string uID, string uName)
    {
        Npartutity exc = new Npartutity();
        List<string> str = new List<string>();
        str.Add(ID);
        exc.DelTmpFile(str);

        string sqlstr = "sp_npart_deletePartApply";

        SqlParameter[] param = new SqlParameter[]{
                             new SqlParameter("@ID", ID)
                             , new SqlParameter("@uID",uID)
                             , new SqlParameter("@uName",uName)
                           
                                 };

        return SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param).ToString();

    }

    public DataTable getGvDataByTypeIDForSupplier(string mstrID, string nodeID)
    {
        string sqlstr = "sp_Npart_getGvDataByTypeIDForSupplier";

        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@mstrID",mstrID)
            ,new SqlParameter("@NodeID",nodeID)
            };

        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param).Tables[0];
    }

    public DataTable getGvDateBySupplierDown(string mstrID, string nodeID)
    {
        string sqlstr = "sp_Npart_getGvDateBySupplierDown";

        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@mstrID",mstrID)
            ,new SqlParameter("@NodeID",nodeID)
            };

        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param).Tables[0];
    }

    public bool importForSupplier(DataTable dt, out string message, string uID, string uName, out DataTable errorDt)
    {
        message = "";
        errorDt = null;
        dt.TableName = "TempTable";
        bool success = true;
        if (success)
        {
            try
            {
                if (
                    dt.Columns[0].ColumnName != "申请号" ||
                    dt.Columns[1].ColumnName != "物料类型" ||
                    dt.Columns[2].ColumnName != "详细描述" ||
                    dt.Columns[3].ColumnName != "MPQ" ||
                    dt.Columns[4].ColumnName != "MOQ" ||
                    dt.Columns[5].ColumnName != "vend" ||
                    dt.Columns[6].ColumnName != "leadtime" ||
                    dt.Columns[7].ColumnName != "原制造商1" ||
                    dt.Columns[8].ColumnName != "型号1" ||
                    dt.Columns[9].ColumnName != "原制造商2" ||
                    dt.Columns[10].ColumnName != "型号2" ||
                    dt.Columns[11].ColumnName != "原制造商3" ||
                    dt.Columns[12].ColumnName != "型号3" 

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
                TempColumn.ColumnName = "partApplyCode";
                TempTable.Columns.Add(TempColumn);

                TempColumn = new DataColumn();
                TempColumn.DataType = System.Type.GetType("System.String");
                TempColumn.ColumnName = "partTypeForQAD";
                TempTable.Columns.Add(TempColumn);

                TempColumn = new DataColumn();
                TempColumn.DataType = System.Type.GetType("System.String");
                TempColumn.ColumnName = "desc";
                TempTable.Columns.Add(TempColumn);

                TempColumn = new DataColumn();
                TempColumn.DataType = System.Type.GetType("System.String");
                TempColumn.ColumnName = "MPQ";
                TempTable.Columns.Add(TempColumn);

                TempColumn = new DataColumn();
                TempColumn.DataType = System.Type.GetType("System.String");
                TempColumn.ColumnName = "MOQ";
                TempTable.Columns.Add(TempColumn);

                TempColumn = new DataColumn();
                TempColumn.DataType = System.Type.GetType("System.String");
                TempColumn.ColumnName = "vend";
                TempTable.Columns.Add(TempColumn);

                TempColumn = new DataColumn();
                TempColumn.DataType = System.Type.GetType("System.String");
                TempColumn.ColumnName = "leadtime";
                TempTable.Columns.Add(TempColumn);

                TempColumn = new DataColumn();
                TempColumn.DataType = System.Type.GetType("System.String");
                TempColumn.ColumnName = "Manufacturer1";
                TempTable.Columns.Add(TempColumn);

                TempColumn = new DataColumn();
                TempColumn.DataType = System.Type.GetType("System.String");
                TempColumn.ColumnName = "Model1";
                TempTable.Columns.Add(TempColumn);

                TempColumn = new DataColumn();
                TempColumn.DataType = System.Type.GetType("System.String");
                TempColumn.ColumnName = "Manufacturer2";
                TempTable.Columns.Add(TempColumn);

                TempColumn = new DataColumn();
                TempColumn.DataType = System.Type.GetType("System.String");
                TempColumn.ColumnName = "Model2";
                TempTable.Columns.Add(TempColumn);

                TempColumn = new DataColumn();
                TempColumn.DataType = System.Type.GetType("System.String");
                TempColumn.ColumnName = "Manufacturer3";
                TempTable.Columns.Add(TempColumn);

                TempColumn = new DataColumn();
                TempColumn.DataType = System.Type.GetType("System.String");
                TempColumn.ColumnName = "Model3";
                TempTable.Columns.Add(TempColumn);

                //TempColumn = new DataColumn();
                //TempColumn.DataType = System.Type.GetType("System.String");
                //TempColumn.ColumnName = "suppliesType";
                //TempTable.Columns.Add(TempColumn);

                //TempColumn = new DataColumn();
                //TempColumn.DataType = System.Type.GetType("System.String");
                //TempColumn.ColumnName = "BroadHeadingType";
                //TempTable.Columns.Add(TempColumn);
                //TempColumn = new DataColumn();
                //TempColumn.DataType = System.Type.GetType("System.String");
                //TempColumn.ColumnName = "SubDivisionType";
                //TempTable.Columns.Add(TempColumn);
                //TempColumn = new DataColumn();
                //TempColumn.DataType = System.Type.GetType("System.String");
                //TempColumn.ColumnName = "SubMaterialType";
                //TempTable.Columns.Add(TempColumn);

                if (dt.Rows.Count > 0)
                {


                    for (int i = 0; i <= dt.Rows.Count - 1; i++)
                    {

                        //TempRow["cost"] 
                        TempRow = TempTable.NewRow();//创建新的行

                        TempRow["partApplyCode"] = dt.Rows[i].ItemArray[0].ToString().Trim();
                        TempRow["partTypeForQAD"] = dt.Rows[i].ItemArray[1].ToString().Trim();
                        TempRow["desc"] = dt.Rows[i].ItemArray[2].ToString().Trim();
                        TempRow["MPQ"] = dt.Rows[i].ItemArray[3].ToString().Trim();
                        TempRow["MOQ"] = dt.Rows[i].ItemArray[4].ToString().Trim();
                        TempRow["vend"] = dt.Rows[i].ItemArray[5].ToString().Trim();
                        TempRow["leadtime"] = dt.Rows[i].ItemArray[6].ToString().Trim();
                        TempRow["Manufacturer1"] = dt.Rows[i].ItemArray[7].ToString().Trim();
                        TempRow["Model1"] = dt.Rows[i].ItemArray[8].ToString().Trim();
                        TempRow["Manufacturer2"] = dt.Rows[i].ItemArray[9].ToString().Trim();
                        TempRow["Model2"] = dt.Rows[i].ItemArray[10].ToString().Trim();
                        TempRow["Manufacturer3"] = dt.Rows[i].ItemArray[11].ToString().Trim();
                        TempRow["Model3"] = dt.Rows[i].ItemArray[12].ToString().Trim();


                        TempTable.Rows.Add(TempRow);
                    }

                    StringWriter writer = new StringWriter();
                    TempTable.WriteXml(writer);
                    string xmlDetail = writer.ToString();

                    string sqlstr = "sp_naprt_importForSupplier";

                    SqlParameter[] param = new SqlParameter[]{
                             new SqlParameter("@detail", xmlDetail)
                             , new SqlParameter("@uID",uID)
                             , new SqlParameter("@uName",uName)
                                 };

                    errorDt = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param).Tables[0];
                    if (errorDt.Rows.Count > 0)
                    {

                        if (errorDt.Rows[0][0].ToString().Equals("1"))
                        {
                            success = true;
                            message = "导入文件成功!";
                        }
                        else
                        {
                            message = "导入文件失败!";
                            success = false;
                        }
                    }
                    else
                    {
                        message = "导入文件失败!";
                        success = false;
                    }

                }
            }
            catch
            {
                message = "导入文件失败!";
                success = false;
                throw new Exception();
            }

        }
        return success;
    }



    public object selectTypeMstrByEngineeringType(string flag)
    {

        string sqlstr = "sp_Npart_selectTypeMstrByEngineeringType";

        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@flag",flag)
        };

        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param).Tables[0];
    
    }

    public DataTable getGvDataByTypeIDForCommon(string mstrID, string nodeID)
    {
        string sqlstr = "sp_Npart_getGvDataByTypeIDForCommon";

        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@mstrID",mstrID)
            ,new SqlParameter("@nodeID",nodeID)
            };

        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param).Tables[0];
    }

    public void updateApplyFail(DataTable table, string reason, string uID, string uName)
    {
        StringWriter writer = new StringWriter();
        table.WriteXml(writer);
        string xmlDetail = writer.ToString();

        string sqlstr = "sp_npart_updateApplyFail";

        SqlParameter[] param = new SqlParameter[]{
                             new SqlParameter("@detail", xmlDetail)
                             , new SqlParameter("@uID",uID)
                             , new SqlParameter("@uName",uName)
                             , new SqlParameter("@reason",reason)   
                                 };

         SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param);
    }
    /// <summary>
    /// 查找邮件发送给的方法
    /// </summary>
    /// <param name="flag">标志</param>
    /// fail 失败时候的方法，转发给申请者
    /// pass 通过的方法，发送给创建料号的人
    /// <returns></returns>
    public string selectMailTo(DataTable table,string flag)
    {
        StringWriter writer = new StringWriter();
        table.WriteXml(writer);
        string xmlDetail = writer.ToString();

        string sqlstr = "sp_npart_selectMailTo";

        SqlParameter[] param = new SqlParameter[]{
                             new SqlParameter("@detail", xmlDetail)
                             , new SqlParameter("@flag",flag)
                           
                           
                                 };

        return SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param).ToString();
    }

    public DataTable exportFailPart(DataTable tableID,string mstrID)
    {
        StringWriter writer = new StringWriter();
        tableID.WriteXml(writer);
        string xmlDetail = writer.ToString();

        string sqlstr = "sp_npart_exportFailPart";

        SqlParameter[] param = new SqlParameter[]{
                             new SqlParameter("@detail", xmlDetail)
                             ,new SqlParameter("@mstrID",mstrID)
                                 };

        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param).Tables[0];
    }

    public DataTable getGvColByTypeDetForFail(string mstrID)
    {
        string sqlstr = "sp_Npart_getGvColByTypeDetForFail";

        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@mstrID",mstrID)
            
            };

        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param).Tables[0];
    }

    public DataTable getGvDataByTypeIDAndUIDFail(string mstrID, string uID, string status)
    {
        string sqlstr = "sp_Npart_getGvDataByTypeIDAndUIDFail";

        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@mstrID",mstrID)
            ,new SqlParameter("@uID",uID)
            ,new SqlParameter("@status",status)
            };

        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param).Tables[0];
    }

    public string selectAllTypeDetByMstrIDAddFailReturnString(string typeID)
    {
        string sqlstr = "sp_Npart_selectAllTypeDetByMstrIDAddFailReturnString";

        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@typeID",typeID)
            
            };

        return SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param).ToString();
    }

    public string checkModleEnumComplete(string mstrID)
    {
        string sqlstr = "sp_npart_checkModleEnumComplete";

        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@mstrID",mstrID)
            
            };

        return SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param).ToString();
    }

    public DataTable selectModleTypeForOpenNode(string nodeID)
    {
        string sqlstr = "sp_Npart_selectModleTypeForOpenNode";

        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@nodeID",nodeID)
            };

        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param).Tables[0];
    }

    public SqlDataReader selectAllEnumByTypeID(string mstrID)
    {
        string sqlstr = "sp_Npart_selectAllEnumByTypeID";

        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@mstrID",mstrID)
            };

        return SqlHelper.ExecuteReader(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param);
    }

    public DataTable getGvColByImportPartList(string mstrID)
    {
        string sqlstr = "sp_Npart_getGvColByImportPartList";

        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@mstrID",mstrID)
            };

        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param).Tables[0];
    }

    public SqlDataReader selectAllEnumByTypeIDByImport(string mstrID)
    {
        string sqlstr = "sp_Npart_selectAllEnumByTypeIDByImport";

        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@mstrID",mstrID)
            };

        return SqlHelper.ExecuteReader(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param);
    }
}