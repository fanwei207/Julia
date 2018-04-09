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
/// Summary description for PC_price
/// </summary>
public class PC_price
{
    public PC_price()
    {

    }
    adamClass adam = new adamClass();

    //当Gridview数据为空时显示的信息
    private string EmptyText = "没有记录";

    //guid生成函数
    private string getGUID()
    {
        System.Guid guid = new Guid();
        guid = Guid.NewGuid();
        string str = guid.ToString();
        return str;
    }

    ///<summary>
    ///绑定数据到GridView，当表格数据为空时显示表头
    ///</summary>
    ///<param name="gridview"></param>
    ///<param name="table"></param>
    public void GridViewDataBind(GridView gridview, DataTable table)
    {
        //记录为空重新构造Gridview
        if (table.Rows.Count == 0)
        {
            table = table.Clone();
            table.Rows.Add(table.NewRow());
            gridview.DataSource = table;
            gridview.DataBind();
            int columnCount = gridview.Rows[0].Cells.Count;
            gridview.Rows[0].Cells.Clear();
            gridview.Rows[0].Cells.Add(new TableCell());
            gridview.Rows[0].Cells[0].ColumnSpan = columnCount;
            gridview.Rows[0].Cells[0].Text = EmptyText;
            gridview.Rows[0].Cells[0].Style.Add("text-align", "center");
        }
        else
        {
            //数据不为空直接绑定
            gridview.DataSource = table;
            gridview.DataBind();
        }

        //重新绑定取消选择
        gridview.SelectedIndex = -1;
        //释放表，是放表所占的空间
        table.Dispose();
    }


    public void selectItemCode(string txtQAD, out string code, out string desc1, out string desc2, GridView gvVender, string PQID)
    {
        code = string.Empty;
        desc1 = string.Empty;
        desc2 = string.Empty;
        string sqlstr = "sp_pc_selectItemCode";
        SqlParameter[] param = new SqlParameter[5]{
        new SqlParameter("@qad",txtQAD)
        ,new SqlParameter("@code",SqlDbType.NVarChar,20)
        ,new SqlParameter("@desc1",SqlDbType.NVarChar,200)
        ,new SqlParameter("@desc2",SqlDbType.NVarChar,200)
        ,new SqlParameter("@PQID",PQID)
        };
        param[1].Direction = ParameterDirection.Output;
        param[2].Direction = ParameterDirection.Output;
        param[3].Direction = ParameterDirection.Output;

        DataTable dt = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param).Tables[0];

        code = param[1].Value.ToString().Trim();
        desc1 = param[2].Value.ToString().Trim();
        desc2 = param[3].Value.ToString().Trim();


        this.GridViewDataBind(gvVender, dt);


    }

    //public int addApplyMstr(string QAD, string reason,int uID)
    //{
    //    string strsql = "sp_pc_AddApplyMstr";
    //    SqlParameter[] param = new SqlParameter[]{
    //    new SqlParameter("@QAD",QAD)
    //    ,new SqlParameter("@reason",reason)
    //    ,new SqlParameter("@uID",uID)
    //    ,new SqlParameter("@error",SqlDbType.Bit)
    //    };
    //    param[3].Direction = ParameterDirection.Output;

    //    int ApplyMstrID= Convert.ToInt32( SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strsql, param));

    //    if(Convert.ToBoolean(param[3].Value))
    //    {
    //        return ApplyMstrID;

    //    }
    //    else
    //    {
    //        throw new Exception();
    //    }




    //}
    /// <summary>
    /// 添加供应商
    /// </summary>
    /// <param name="ApplyMstrID"></param>
    /// <returns>0失败</returns>
    /// <returns>1成功</returns>
    /// <returns>2供应商不存在</returns>
    /// <returns>3供应商已存在</returns>
    public int addVender(string formate, string vender, int uID, string PQID, string qad, string UM, string type, bool isout, string TechnicalPrice)
    {
        string strsql = "sp_pc_addApplyVender";
        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@formate",formate)
            ,new SqlParameter("@vender",vender)
            ,new SqlParameter("@uID",uID)
            ,new SqlParameter("@PQID",PQID)
            ,new SqlParameter("@qad",qad)
            ,new SqlParameter("@UM",UM)
            ,new SqlParameter("@type",type)
            ,new SqlParameter("@isout",isout)
            ,new SqlParameter("@technicalPrice",TechnicalPrice)
        };
        return Convert.ToInt32(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strsql, param));
    }

    public bool changeReason(string PQID, int uID, string reason)
    {
        string sqlstr = "sp_pc_changeReason";
        SqlParameter[] param = new SqlParameter[]{
        new SqlParameter("@PQID",PQID)
        ,new SqlParameter("@uID",uID)
        ,new SqlParameter("@reason",reason)
        };
        return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param));
    }

    public string selectVender(string QADNO, GridView gvVender, out int mstrID)
    {
        mstrID = 0;
        string sqlstr = "sp_pc_selectVender";
        SqlParameter[] param = new SqlParameter[]{
        new SqlParameter("@QADNO",QADNO)
         ,new SqlParameter("@mstrID",SqlDbType.Int)
        };
        param[1].Direction = ParameterDirection.Output;


        DataTable dt = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param).Tables[0];

        if (!(int.TryParse(param[1].Value.ToString(), out mstrID)))
        {
            if (mstrID == 0)
            {
                this.GridViewDataBind(gvVender, dt);
                return "alert('QAD号有问题请重新书写再处理');";
            }
        }

        this.GridViewDataBind(gvVender, dt);
        return string.Empty;

    }


    /// <summary>
    /// 删除供应商
    /// </summary>
    /// <param name="DetID"></param>
    /// <returns></returns>
    public bool deleteVender(int DetID)
    {   
        string sqlstr = "sp_pc_deleteApplyVender";
        SqlParameter[] param = new SqlParameter[]{
        new SqlParameter("@DetID",DetID)
        };
        return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param));
    }
    /// <summary>
    /// 查PQ列表
    /// </summary>
    /// <param name="QAD"></param>
    /// <param name="applyBy"></param>
    /// <param name="starDate"></param>
    /// <param name="endDate"></param>
    /// <param name="status"></param>
    /// <returns></returns>
    public DataTable selectApplyMstr(string QAD, string applyBy, string starDate, string endDate, int status ,string typeValue)
    {
        string sqlstr = "sp_pc_selectApplyMstrList";
        SqlParameter[] param = new SqlParameter[]{
        new SqlParameter("@QAD",QAD)
        ,new SqlParameter("@applyBy",applyBy)
        ,new SqlParameter("@starDate",starDate)
        ,new SqlParameter("@endDate",endDate)
        ,new SqlParameter("@status",status )
        ,new SqlParameter("@typeValue",typeValue)
        };

        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param).Tables[0];




    }
    /// <summary>
    /// 新建pq
    /// </summary>
    /// <param name="uID"></param>
    /// <param name="PQID"></param>
    /// <param name="uName"></param>
    /// <returns></returns>
    public bool addNewPQ(int uID, out string PQID, out string uName,bool isout)
    {

        string sqlstr = "sp_pc_addNewPQ";
        SqlParameter[] param = new SqlParameter[]{
        new SqlParameter("@uID",uID )
        ,new SqlParameter("@PQNO",SqlDbType.Char,9)
        ,new SqlParameter("@uName",SqlDbType.NVarChar,20)
        ,new SqlParameter("@isout",isout)
        };
        param[1].Direction = ParameterDirection.Output;
        param[2].Direction = ParameterDirection.Output;
        bool flag = Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param));
        PQID = param[1].Value.ToString();
        uName = param[2].Value.ToString();
        return flag;
    }
    /// <summary>
    /// 删除PQ
    /// </summary>
    /// <param name="PQID"></param>
    /// <returns></returns>
    public bool deletePQ(string PQID)
    {
        string sqlstr = "sp_pc_deletePQ";
        SqlParameter[] param = new SqlParameter[]{
        new SqlParameter("@PQID",PQID)
        };
        return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param));
    }

    public DataTable selectApplyQADList(string QAD, string PQID)
    {
        string sqlstr = "sp_pc_selectApplyQadList";
        SqlParameter[] param = new SqlParameter[]{
        new SqlParameter("@qad","")
        ,new SqlParameter("@PQID",PQID)
        };

        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param).Tables[0];





    }

    public bool checkQADNo(string QAD)
    {
        string sqlstr = "sp_pc_checkQadNo";
        SqlParameter[] param = new SqlParameter[]{
        new SqlParameter("@QAD",QAD)
       
        };
        return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param));

    }

    public void selectVenderOnlyGv(string QADNO, GridView gvVender, string PQID)
    {
        string sqlstr = "sp_pc_selectVenderOnlyGv";
        SqlParameter[] param = new SqlParameter[]{
        new SqlParameter("@qad",QADNO)
        ,new SqlParameter("@PQID",PQID)
        };

        DataTable dt = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param).Tables[0];

        this.GridViewDataBind(gvVender, dt);
    }

    public DataTable selectVenderCheckList(string venderNo, string venderName, int isfinish)
    {
        string sqlstr = "sp_pc_selectQuotationAndCalculationList";
        SqlParameter[] param = new SqlParameter[]{
        new SqlParameter("@venderNo",venderNo )
        ,new SqlParameter("@venderName" ,venderName)
        ,new SqlParameter("@IsFinish" ,isfinish)
        };

        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param).Tables[0];

    }

    //public DataTable selectChangeList(string mstrID, string QAD, int isFinish)
    //{
    //    string sqlstr = "sp_pc_selectChangeList";
    //    SqlParameter[] param = new SqlParameter[]{
    //    new SqlParameter("@mstrID",mstrID)
    //    ,new SqlParameter("@QAD",QAD)
    //    ,new SqlParameter("@isFinish",isFinish)
    //    };

    //    return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param).Tables[0];
    //}
    /// <summary>
    /// 修改pq状态
    /// </summary>
    /// <param name="PQID"></param>
    /// <param name="uID"></param>
    /// <param name="QAD"></param>
    /// <param name="vender"></param>
    /// <returns></returns>
    public int PQSubmit(string PQID, int uID,string applyType, out string QAD, out string vender)
    {
        string sqlstr = "sp_pc_updatePQState";
        SqlParameter[] param = new SqlParameter[]{
        new SqlParameter("@PQID",PQID)
        ,new SqlParameter("@uID",uID)
        ,new SqlParameter("@QAD",SqlDbType.VarChar,20)
       ,new SqlParameter("@vender",SqlDbType.VarChar,10)
       ,new SqlParameter("@applyType",applyType)
        };
        param[2].Direction = ParameterDirection.Output;
        param[3].Direction = ParameterDirection.Output;

        int flag = Convert.ToInt32(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param));
        QAD = param[2].Value.ToString();
        vender = param[3].Value.ToString();
        return flag;
    }

    public DataTable selectQuotationList(int venderId, bool isAll, int uID)
    {
        string sqlstr = "sp_pc_selectQuotationList";
        SqlParameter[] param = new SqlParameter[]{
        new SqlParameter("@venderID" ,venderId)
        ,new SqlParameter("@isAll",isAll?1:0)
        ,new SqlParameter("@uID",uID)
        };
        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param).Tables[0];
    }

    public bool deleteApplyMstr(string qad, string PQID)
    {
        string sqlstr = "sp_pc_deleteApplyMstr";
        SqlParameter[] param = new SqlParameter[]{
        new SqlParameter("@QAD" ,qad)
        ,new SqlParameter("@PQID",PQID)
        };

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param));
    }



    public bool toQuotation(int DetID, decimal price1, string price2, string curr, string fileAllName, int uID)
    {
        string sqlstr = "sp_pc_updateApplyDetAboutQuotation";
        SqlParameter[] param = new SqlParameter[]{
        new SqlParameter("@DetID",DetID)
        ,new SqlParameter("@price1",price1)
        ,new SqlParameter("@price2",price2)
        ,new SqlParameter("@basis",fileAllName)
        ,new SqlParameter("@uID",uID)
        ,new SqlParameter("@curr",curr)
        };

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param));
    }

    public DataTable selectCalculationList(int venderId, bool isAll, int uID)
    {
        string sqlstr = "sp_pc_selectCalculationList";
        SqlParameter[] param = new SqlParameter[]{
         new SqlParameter("@venderID" ,venderId)
        ,new SqlParameter("@isAll",isAll?1:0)
        ,new SqlParameter("@uID",uID)
        };

        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param).Tables[0];
    }

    public bool toCalculation(int DetID, decimal checkPrice, string fileAllName, int uID)
    {
        string sqlstr = "sp_pc_updateApplyDetAboutCalculation";

        SqlParameter[] param = new SqlParameter[]{
        new SqlParameter("@DetID",DetID)
        ,new SqlParameter("@basis",fileAllName)
        ,new SqlParameter("@uID",uID)
        ,new SqlParameter("@checkPrice",checkPrice)
        };

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param));

    }


    public DataTable selectChangePriceVenderForQAD(string QADNO)
    {
        string sqlstr = "sp_pc_selectChangePriceVenderForQAD";
        SqlParameter[] param = new SqlParameter[]{
        new SqlParameter("@QAD",QADNO)
        };

        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param).Tables[0];
    }

    /// <summary>
    /// 查看详单列表
    /// </summary>
    /// <param name="PQID"></param>
    /// <param name="rejectReason"></param>
    /// <param name="flag"></param>
    /// <returns></returns>
    public DataTable selectApplyDetList(string PQID, out string rejectReason, out string ddlItemValue)
    {
        string sqlstr = "sp_pc_selectApplyDetList";
        SqlParameter[] param = new SqlParameter[]{
        new SqlParameter("@PQID",PQID)
        ,new SqlParameter("@rejectReason",SqlDbType.NVarChar,500)
        ,new SqlParameter("@ddlItemValue",SqlDbType.VarChar,5)
        };

        param[1].Direction = ParameterDirection.Output;
        param[2].Direction = ParameterDirection.Output;
        DataTable dt = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param).Tables[0];

        rejectReason = param[1].Value.ToString();
        ddlItemValue = param[2].Value.ToString();
        return dt;
    }


    //-----------------------------------------------------------------------------------------

    public DataTable selectNotInquiryFromApplyDet(string QAD, string vender, string venderName)
    {
        string sqlstr = "sp_pc_selectNotInquiryFromApplyDet";
        SqlParameter[] param = new SqlParameter[]{
        new SqlParameter("@QAD",QAD)
        ,new SqlParameter("@vender",vender)
        ,new SqlParameter("@venderName",venderName)
        };

        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param).Tables[0];
    }

    public DataTable selectNotInquiryByVender(string vender)
    {
        string sqlstr = "sp_pc_selectNotInquiryByVender";
        SqlParameter[] param = new SqlParameter[]{
        new SqlParameter("@vender",vender)
        };

        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param).Tables[0];
    }

    public string insertTOInquiry(List<int> ilist, string vender, string venderName, int uID, out string IMID)
    {
        int flag = 0;//出错数
        string sqlstr = "sp_pc_insertInquiryMstr";
        SqlParameter[] param = new SqlParameter[]{
        new SqlParameter("@vender",vender)
        ,new SqlParameter("@uID",uID)
        ,new SqlParameter("@venderName",venderName)
        ,new SqlParameter("@IMID",SqlDbType.Char,9)
        };
        param[3].Direction = ParameterDirection.Output;

        /*
         isCanDo 0 = 数据库异常
         * 1 = 正常
         * 2 = 币种没有跳出
         */
        string isCanDo = SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param).ToString();

        if (isCanDo.Equals("0"))
        {
            flag = flag + 1;
        }
        else if (isCanDo.Equals("2"))
        {
            IMID = param[3].Value.ToString();
            return "2";
        }
        IMID = param[3].Value.ToString();

        DataTable TempTable = new DataTable("PC_InquiryDet");
        DataColumn TempColumn;
        DataRow TempRow = null;

        TempColumn = new DataColumn();
        TempColumn.DataType = System.Type.GetType("System.String");
        TempColumn.ColumnName = "IMID";
        TempTable.Columns.Add(TempColumn);

        TempColumn = new DataColumn();
        TempColumn.DataType = System.Type.GetType("System.Int32");
        TempColumn.ColumnName = "applyDetID";
        TempTable.Columns.Add(TempColumn);

        TempColumn = new DataColumn();
        TempColumn.DataType = System.Type.GetType("System.Int32");
        TempColumn.ColumnName = "createdBy";
        TempTable.Columns.Add(TempColumn);

        TempColumn = new DataColumn();
        TempColumn.DataType = System.Type.GetType("System.DateTime");
        TempColumn.ColumnName = "createdDate";
        TempTable.Columns.Add(TempColumn);



        foreach (int detID in ilist)
        {
            TempRow = TempTable.NewRow();
            TempRow["IMID"] = IMID;
            TempRow["applyDetID"] = detID;
            TempRow["createdBy"] = uID;
            TempRow["createdDate"] = DateTime.Now;

            TempTable.Rows.Add(TempRow);

        }
        if (TempTable != null && TempTable.Rows.Count > 0)
        {
            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(adam.dsn0(), SqlBulkCopyOptions.UseInternalTransaction))
            {
                bulkCopy.DestinationTableName = "PC_InquiryDet";

                bulkCopy.ColumnMappings.Clear();

                bulkCopy.ColumnMappings.Add("IMID", "IMID");
                bulkCopy.ColumnMappings.Add("applyDetID", "applyDetID");
                bulkCopy.ColumnMappings.Add("createdBy", "createdBy");
                bulkCopy.ColumnMappings.Add("createdDate", "createdDate");
                try
                {
                    bulkCopy.WriteToServer(TempTable);

                }
                catch (Exception ex)
                {
                    flag = flag + 1;
                    string sqlstr2 = "sp_pc_deleteNullInquiryMstr";
                    SqlParameter[] param2 = new SqlParameter[]{
                        new SqlParameter("@IMID",IMID)
                        };
                    SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, sqlstr2, param2);
                }
                finally
                {
                    TempTable.Dispose();
                    bulkCopy.Close();
                }
            }
        }

        if (flag > 0)
        {
            return "0";
        }
        else
        {
            string sqlstr3 = "sp_pc_updateApplyDetStatusInCreateIMID";
            SqlParameter[] param3 = new SqlParameter[]{
                        new SqlParameter("@IMID",IMID)
                        ,new SqlParameter("@uID",uID)
                        };
            SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, sqlstr3, param3);
            return "1";
        }
    }

    public DataTable selectInquiryList(string IMID, string vender, string venderName, string QAD, string status, string isSelf, string Code)
    {
        string sqlstr = "sp_pc_selectInquiryList";
        SqlParameter[] param = new SqlParameter[]{
        new SqlParameter("@QAD",QAD)
        ,new SqlParameter("@vender",vender)
        ,new SqlParameter("@venderName",venderName)
        ,new SqlParameter("@IMID",IMID)
        ,new SqlParameter("@status",status)
        ,new SqlParameter("@isSelf",isSelf)
        ,new SqlParameter("@code",Code)
        };

        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param).Tables[0];
    }

    public DataTable selectInquiryDet(string IMID, out string vender, out string verderName, out int statue, out string curr, out string create, out string createDate, out string venderPhone, out string venderEmail)
    {
        string sqlstr = "sp_pc_selectInquiryDet";
        SqlParameter[] param = new SqlParameter[]{
        new SqlParameter("@IMID",IMID)
        ,new SqlParameter("@vender",SqlDbType.VarChar,10)
        ,new SqlParameter("@venderName",SqlDbType.NVarChar,50)
           ,new SqlParameter("@statue",SqlDbType.Int)
           ,new SqlParameter("@curr",SqlDbType.VarChar,5)
           ,new SqlParameter("@create",SqlDbType.NVarChar,20)
           ,new SqlParameter("@createDate",SqlDbType.VarChar,20)
           ,new SqlParameter("@venderPhone",SqlDbType.VarChar,30)
           ,new SqlParameter("@venderEmail",SqlDbType.NVarChar,100)
        };
        param[1].Direction = ParameterDirection.Output;
        param[2].Direction = ParameterDirection.Output;
        param[3].Direction = ParameterDirection.Output;
        param[4].Direction = ParameterDirection.Output;
        param[5].Direction = ParameterDirection.Output;
        param[6].Direction = ParameterDirection.Output;
        param[7].Direction = ParameterDirection.Output;
        param[8].Direction = ParameterDirection.Output;

        DataTable dt = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param).Tables[0];
        vender = param[1].Value.ToString();
        verderName = param[2].Value.ToString();
        if (!int.TryParse(param[3].Value.ToString(), out statue))
        {
            statue = 0;
        }
        curr = param[4].Value.ToString();
        create = param[5].Value.ToString();
        createDate = param[6].Value.ToString();
        venderPhone = param[7].Value.ToString();
        venderEmail = param[8].Value.ToString();
        return dt;
    }

    public bool insertBasis(string IMID, int uID, string flieName, string filePate, int type)
    {
        string sqlstr = "sp_pc_insertBasis";
        SqlParameter[] param = new SqlParameter[]{
        new SqlParameter("@IMID",IMID)
        , new SqlParameter("@uID",uID)
        , new SqlParameter("@flieName",flieName)
        , new SqlParameter("@filePate",filePate)
        , new SqlParameter("@type",type)
        };

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param));
    }



    public int updateQuotation(DataTable dt, int uID, string curr, string IMID)
    {
        StringWriter writer = new StringWriter();
        dt.WriteXml(writer);
        string xmlDetail = writer.ToString();


        string sqlstr = "sp_pc_updateQuotation";
        SqlParameter[] param = new SqlParameter[]{
        new SqlParameter("@uID",uID)
        , new SqlParameter("@curr",curr)
        , new SqlParameter("@gvTable",xmlDetail)
        ,new SqlParameter("@IMID",IMID)
        };

        return Convert.ToInt32(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param));
    }

    public int updateCheck(DataTable dt, int uID, string curr, string IMID)
    {
        StringWriter writer = new StringWriter();
        dt.WriteXml(writer);
        string xmlDetail = writer.ToString();


        string sqlstr = "sp_pc_updateCheck";
        SqlParameter[] param = new SqlParameter[]{
        new SqlParameter("@uID",uID)
        , new SqlParameter("@curr",curr)
        , new SqlParameter("@gvTable",xmlDetail)
        ,new SqlParameter("@IMID",IMID)
        };
        

        return Convert.ToInt32(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param));
    }

    public DataTable selectBasis(string PQDetID ,out string  IMID)
    {
        string sqlstr = "sp_pc_selectBasis";
        SqlParameter[] param = new SqlParameter[]{
        new SqlParameter("@PQDetID",PQDetID)
        ,new SqlParameter("@IMID",SqlDbType.VarChar,10)
        };

        param[1].Direction = ParameterDirection.Output;

        DataTable dt =   SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param).Tables[0];

        IMID = param[1].Value.ToString();

        return dt;
    }



    public bool updateBasis(int ID, int uID)
    {
        string sqlstr = "sp_pc_updateBasis";
        SqlParameter[] param = new SqlParameter[]{
        new SqlParameter("@ID",ID)
        , new SqlParameter("@uID",uID)
        };

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param));
    }






    #region 申请导入相关
    public bool applyImport(string filePath, string uId, string PQID, out string message, string InfoFrom,string type,bool isout)
    {
        message = "";
        DataTable dt = null;
        bool success = true;
        try
        {
            //dt = adam.getExcelContents(filePath).Tables[0];
            NPOIHelper helper = new NPOIHelper();
            dt = helper.GetExcelContents(filePath);
        }
        catch (Exception ex)
        {
            message = "导入文件必须是Excel格式";
            success = false;
        }
        finally
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
        if (success)
        {
            try
            {
                if (
                    dt.Columns[0].ColumnName != "QAD" ||
                    dt.Columns[1].ColumnName != "vendor" ||
                    dt.Columns[2].ColumnName != "um" ||
                    dt.Columns[3].ColumnName != "specifications" ||
                    dt.Columns[4].ColumnName != "technicalPrice" 
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
                TempColumn.ColumnName = "PQID";
                TempTable.Columns.Add(TempColumn);

                TempColumn = new DataColumn();
                TempColumn.DataType = System.Type.GetType("System.String");
                TempColumn.ColumnName = "QAD";
                TempTable.Columns.Add(TempColumn);

                TempColumn = new DataColumn();
                TempColumn.DataType = System.Type.GetType("System.String");
                TempColumn.ColumnName = "vender";
                TempTable.Columns.Add(TempColumn);

                TempColumn = new DataColumn();
                TempColumn.DataType = System.Type.GetType("System.String");
                TempColumn.ColumnName = "formate";
                TempTable.Columns.Add(TempColumn);

                TempColumn = new DataColumn();
                TempColumn.DataType = System.Type.GetType("System.String");
                TempColumn.ColumnName = "um";
                TempTable.Columns.Add(TempColumn);

                TempColumn = new DataColumn();
                TempColumn.DataType = System.Type.GetType("System.String");
                TempColumn.ColumnName = "technicalPrice";
                TempTable.Columns.Add(TempColumn);

                //TempColumn = new DataColumn();
                //TempColumn.DataType = System.Type.GetType("System.Decimal");
                //TempColumn.ColumnName = "cost";
                //TempTable.Columns.Add(TempColumn);

                TempColumn = new DataColumn();
                TempColumn.DataType = System.Type.GetType("System.String");
                TempColumn.ColumnName = "error";
                TempTable.Columns.Add(TempColumn);

                TempColumn = new DataColumn();
                TempColumn.DataType = System.Type.GetType("System.Int32");
                TempColumn.ColumnName = "createdby";
                TempTable.Columns.Add(TempColumn);

                TempColumn = new DataColumn();
                TempColumn.DataType = System.Type.GetType("System.String");
                TempColumn.ColumnName = "createdDate";
                TempTable.Columns.Add(TempColumn);

                if (dt.Rows.Count > 0)
                {


                    //decimal cost = -1;
                    string createdBy = uId;
                    string createdDate = DateTime.Now.ToString();

                    DateTime dateFormat = DateTime.Now;

                    //先清空临时表中该上传员工的记录
                    if (ClearTempTable(Convert.ToInt32(uId)))
                    {

                        for (int i = 0; i <= dt.Rows.Count - 1; i++)
                        {
                            long qadNo = 0;
                            string QAD = string.Empty;
                            string vender = string.Empty;
                            string formate = string.Empty;
                            string errorMsg = string.Empty;
                            string technicalPrice = string.Empty;
                            decimal technicalPriceTest = 0;



                            if ((dt.Rows[i].IsNull(0) || string.Empty.Equals(dt.Rows[i].ItemArray[0].ToString().Trim())) && (dt.Rows[i].IsNull(1) || string.Empty.Equals(dt.Rows[i].ItemArray[0].ToString().Trim())) &&( dt.Rows[i].IsNull(2) || string.Empty.Equals(dt.Rows[i].ItemArray[2].ToString().Trim())) && (dt.Rows[i].IsNull(3)|| string.Empty.Equals(dt.Rows[i].ItemArray[3].ToString().Trim())))
                            {
                                continue;
                            }

                            TempRow = TempTable.NewRow();//创建新的行
                            if (dt.Rows[i].IsNull(0))
                            {
                                errorMsg += "QAD不能为空;";
                                
                            }
                            else if (!long.TryParse(dt.Rows[i].ItemArray[0].ToString().Trim(), out qadNo))
                            {
                                errorMsg += "QAD只能是数字;";
                                 

                            }
                            else if (dt.Rows[i].ItemArray[0].ToString().Trim().Length != 14)
                            {
                                errorMsg += "QAD只能是14位数字;";
                                
                            }
                            else
                            {
                                QAD = dt.Rows[i].ItemArray[0].ToString().Trim();
                            }

                            if (dt.Rows[i].IsNull(1))
                            {

                                errorMsg += "供应商号不能为空;";
                            }
                            else if (dt.Rows[i].ItemArray[1].ToString().Trim().Length != 8)
                            {
                                errorMsg += "供应商号只能是8位;";
                            }
                            else
                            {
                                vender = dt.Rows[i].ItemArray[1].ToString().Trim();

                            }

                            if (dt.Rows[i].IsNull(2))
                            {

                                errorMsg += "单位不能为空;";
                            }
                            string um = dt.Rows[i].ItemArray[2].ToString().Trim().ToUpper();
                            if ("BO".Equals(um) && "EA".Equals(um) && "G".Equals(um) && "KG".Equals(um) && "M".Equals(um) && "PC".Equals(um) && "RL".Equals(um) && "L".Equals(um))
                            {
                                errorMsg += "单位只能是 BO，EA，G，KG，M，PC，RL;";
                            }
                            if (um.Length > 2)
                            {
                                errorMsg += "单位长度不会超过2，单位只能是 BO，EA，G，KG，M，PC，RL，L;";
                            }

                            formate = dt.Rows[i].ItemArray[3].ToString().Trim();

                            technicalPrice = dt.Rows[i].ItemArray[4].ToString().Trim();

                            if (!string.Empty.Equals(technicalPrice) && !decimal.TryParse(technicalPrice, out technicalPriceTest)  )
                            {
                                errorMsg += "技术参考价不是数字！";
                            }

                            //TempRow["cost"] 
                            TempRow["PQID"] = PQID;
                            TempRow["QAD"] = QAD;
                            TempRow["vender"] = vender;
                            TempRow["um"] = um;
                            TempRow["formate"] = formate;
                            TempRow["technicalPrice"] = technicalPrice;
                            TempRow["error"] = errorMsg.Trim();
                            TempRow["createdBy"] = Convert.ToInt32(createdBy);
                            TempRow["createdDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                            TempTable.Rows.Add(TempRow);
                        }
                        //TempTable有数据的情况下批量复制到数据库里
                        if (TempTable != null && TempTable.Rows.Count > 0)
                        {
                            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(adam.dsn0(), SqlBulkCopyOptions.UseInternalTransaction))
                            {
                                bulkCopy.DestinationTableName = "PC_applyImportTemp";

                                bulkCopy.ColumnMappings.Clear();

                                bulkCopy.ColumnMappings.Add("PQID", "PQID");
                                bulkCopy.ColumnMappings.Add("QAD", "QAD");
                                bulkCopy.ColumnMappings.Add("formate", "formate");
                                bulkCopy.ColumnMappings.Add("vender", "vender");
                                bulkCopy.ColumnMappings.Add("um", "um");
                                bulkCopy.ColumnMappings.Add("technicalPrice", "priceFromTechnical");
                                bulkCopy.ColumnMappings.Add("error", "error");
                                bulkCopy.ColumnMappings.Add("createdBy", "createdBy");
                                bulkCopy.ColumnMappings.Add("createdDate", "createdDate");
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
                            if (CheckTempTable(Convert.ToInt32(uId), PQID, InfoFrom, isout))
                            {
                                //判断上传内容能否通过验证
                                if (JudgeTempTable(Convert.ToInt32(uId)))
                                {
                                    if (TransTempTable(Convert.ToInt32(uId), PQID, InfoFrom, type , isout))
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
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }
        }
        return success;

    }

    private bool TransTempTable(int uID, string PQID, string InfoFrom, string type , bool isout)
    {
        string sqlstr = "sp_pc_insertApplyImportTempToApplyDet";
        SqlParameter[] param = new SqlParameter[]{
        new SqlParameter("@uID",uID)
        , new SqlParameter("@PQID",PQID)
        , new SqlParameter("@InfoFrom",InfoFrom)
        , new SqlParameter("@type",type)
        , new SqlParameter("@isout",isout)
        };

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param));
    }

    private bool JudgeTempTable(int uID)
    {
        string sqlstr = "sp_pc_selectApplyImportTempByID";
        SqlParameter[] param = new SqlParameter[]{
        new SqlParameter("@uID",uID)

       
        };
        return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param));
    }

    private bool CheckTempTable(int uID, string PQID, string InfoFrom,bool isout)
    {
        string sqlstr = "sp_pc_checkApplyImportTempByID";
        SqlParameter[] param = new SqlParameter[]{
        new SqlParameter("@uID",uID)
        , new SqlParameter("@PQID",PQID)
        , new SqlParameter("@InfoFrom",InfoFrom)
        , new SqlParameter("@isout",isout)
        };
        return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param));
    }

    private bool ClearTempTable(int uID)
    {
        string sqlstr = "sp_pc_deleteApplyImportTempByID";
        SqlParameter[] param = new SqlParameter[]{
        new SqlParameter("@uID",uID)
        };
        return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param));
    }

    public DataTable GetApplyImportError(string strUID)
    {
        string sqlstr = "sp_pc_selectApplyImportTempError";
        SqlParameter[] param = new SqlParameter[]{
        new SqlParameter("@uID",strUID)
        };

        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param).Tables[0];
    }
    #endregion



    public bool updateReject(string PQID, int uID, string rejectReason, string DetIDList)
    {
        string sqlstr = "sp_pc_updateReject";
        SqlParameter[] param = new SqlParameter[]{
        new SqlParameter("@PQID",PQID)
        ,new SqlParameter("@uID",uID)
        ,new SqlParameter("@rejectReason",rejectReason)
        ,new SqlParameter("@DetIDList",DetIDList)
        };
        return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param));
    }
    /// <summary>
    /// 查看pq信息
    /// </summary>
    /// <param name="PQID"></param>
    /// <param name="status"></param>
    /// <param name="applyByName"></param>
    /// <param name="applyBy"></param>
    /// <param name="applyDate"></param>
    public void selectApplyPQInfo(string PQID, out string status, out string applyByName, out int applyBy, out string applyDate)
    {
        string sqlstr = "sp_pc_selectApplyPQInfo";
        SqlParameter[] param = new SqlParameter[]{
        new SqlParameter("@PQID",PQID)
        ,new SqlParameter("@status",SqlDbType.Int)
        ,new SqlParameter("@applyByName" ,SqlDbType.NVarChar,24)
        ,new SqlParameter("@applyBy" ,SqlDbType.Int)
        ,new SqlParameter("@applyDate" ,SqlDbType.VarChar,24)
        };

        param[1].Direction = ParameterDirection.Output;
        param[2].Direction = ParameterDirection.Output;
        param[3].Direction = ParameterDirection.Output;
        param[4].Direction = ParameterDirection.Output;
        SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param);

        status = param[1].Value.ToString();
        applyByName = param[2].Value.ToString();
        applyBy = Convert.ToInt32(param[3].Value);
        applyDate = param[4].Value.ToString();

    }

    public DataTable generationInquiry(string IMID, out string company, out string createdDate, out string createdByName, int uID,string curr)
    {
        string sqlstr = "sp_pc_generationInquiry";
        SqlParameter[] param = new SqlParameter[]{
        new SqlParameter("@IMID",IMID)
        ,new SqlParameter("@company",SqlDbType.NVarChar,100)
        ,new SqlParameter("@createdDate" ,SqlDbType.VarChar,20)
        ,new SqlParameter("@createdByName" ,SqlDbType.NVarChar,24)
        , new SqlParameter("@uID",uID)
        ,new SqlParameter("@curr",curr)
        };

        param[1].Direction = ParameterDirection.Output;
        param[2].Direction = ParameterDirection.Output;
        param[3].Direction = ParameterDirection.Output;
        DataTable dt = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param).Tables[0];

        company = param[1].Value.ToString();
        createdDate = param[2].Value.ToString();
        createdByName = param[3].Value.ToString();
        return dt;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sheetPrefixName"></param>
    /// <param name="strShipNo"></param>
    /// <param name="isPkgs"></param>
    public void createinquiry(string stroutFile, string IMID, string company, string vendor, string createdDate, string createByName, int uid,string curr)
    {
        System.Data.DataTable dt = generationInquiry(IMID, out company, out createdDate, out createByName, uid, curr);
        string excelname = System.DateTime.Now.ToString().Replace(":", "").Replace("-", "").Replace(" ", "");
        string strFile = stroutFile;
        string filePath = System.Web.HttpContext.Current.Server.MapPath("../Excel/" + strFile);
        MemoryStream ms = RenderDataTableToExcel(dt, filePath, uid, IMID, company, vendor, createdDate, createByName) as MemoryStream;
        FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write);
        byte[] data = ms.ToArray();
        fs.Write(data, 0, data.Length);
        fs.Flush();
        fs.Close();
        data = null;
        ms = null;
        fs = null;
    }

    public Stream RenderDataTableToExcel(System.Data.DataTable SourceTable, string stroutFile, int uid, string IMID, string company, string vendor, string createdDate, string createByName)
    {
        MemoryStream ms = new MemoryStream();
        FileStream file = new System.IO.FileStream(System.Web.HttpContext.Current.Server.MapPath("../docs/pc_excelinquiry.xls"), FileMode.Open, FileAccess.Read);
        NPOI.HSSF.UserModel.HSSFWorkbook hssfworkbook = new NPOI.HSSF.UserModel.HSSFWorkbook(file);
        NPOI.SS.UserModel.ISheet sheet = hssfworkbook.GetSheet("Sheet1");//workbook.CreateSheet();
        NPOI.SS.UserModel.IRow headerRow = sheet.GetRow(17);//sheet.GetRow(0);
        file.Close();
       

        //输出头部信息
        #region 输出头部信息
        sheet.GetRow(2).GetCell(1).SetCellValue("询价单号：" + IMID);
        sheet.GetRow(3).GetCell(1).SetCellValue("公司：");
        sheet.GetRow(3).GetCell(2).SetCellValue(company);
        sheet.GetRow(3).GetCell(5).SetCellValue("询价日期：");
        sheet.GetRow(3).GetCell(6).SetCellValue(createdDate);
        sheet.GetRow(5).GetCell(2).SetCellValue(vendor);
        sheet.GetRow(7).GetCell(1).SetCellValue("联系人：");
        sheet.GetRow(7).GetCell(5).SetCellValue("联系电话：");
        sheet.GetRow(9).GetCell(1).SetCellValue("备注：");

        //输出明细
        
        
        //设置7列宽为100
        sheet.SetColumnWidth(8, 250);
        //sheet.SetColumnWidth(12, 100);
        //加标题
        sheet.GetRow(11).GetCell(1).SetCellValue("QAD");
        sheet.GetRow(11).GetCell(2).SetCellValue("部件号");
        sheet.GetRow(11).GetCell(3).SetCellValue("单位");
        sheet.GetRow(11).GetCell(4).SetCellValue("需求规格");
        sheet.GetRow(11).GetCell(5).SetCellValue("详细描述");
        //sheet.GetRow(11).GetCell(6).SetCellValue("描述1");
        //sheet.GetRow(11).GetCell(7).SetCellValue("描述2");
        sheet.GetRow(11).GetCell(6).SetCellValue("价格");
        sheet.GetRow(11).GetCell(7).SetCellValue("币种");
        
        //明细起始行
        int rowIndex = 12;

        //ICellStyle style1 = hssfworkbook.CreateCellStyle();
        //style1.WrapText = true;
        ICellStyle style2 = hssfworkbook.CreateCellStyle();
        style2.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
        style2.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
        style2.BorderTop = NPOI.SS.UserModel.BorderStyle.Thick;
        


        foreach (DataRow row in SourceTable.Rows)
        {
            
           
//            Sub changeRows()
//'
//' changeRows 宏
//'

//'
//    Range("E12:H100").Select
//    With Selection
//        .HorizontalAlignment = xlGeneral
//        .VerticalAlignment = xlBottom
//        .WrapText = True
//        .Orientation = 0
//        .AddIndent = False
//        .IndentLevel = 0
//        .ShrinkToFit = False
//        .ReadingOrder = xlContext
//        .MergeCells = False
//    End With
//End Sub




            NPOI.SS.UserModel.IRow dataRow = sheet.CreateRow(rowIndex);
            ICellStyle style = hssfworkbook.CreateCellStyle();
            style.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            style.TopBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
            style.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            style.RightBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
            style.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            style.BottomBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
            style.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            style.LeftBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;


            ICell cell = dataRow.CreateCell(1);
            cell.SetCellValue(row["QAD"].ToString());
            cell.CellStyle = style;

            cell = dataRow.CreateCell(2);
            cell.SetCellValue(row["ItemCode"].ToString());
            cell.CellStyle = style;

            cell = dataRow.CreateCell(3);
            cell.SetCellValue(row["UM"].ToString());
            cell.CellStyle = style;

            cell = dataRow.CreateCell(4);
            cell.SetCellValue(row["Formate"].ToString());
            cell.CellStyle = style;

            cell = dataRow.CreateCell(5);
            cell.SetCellValue(row["ItemDescription"].ToString());
            cell.CellStyle = style;

            //cell = dataRow.CreateCell(6);
            //cell.SetCellValue(row["ItemDesc1"].ToString());
            //cell.CellStyle = style;
            //cell = dataRow.CreateCell(7);
            //cell.SetCellValue(row["ItemDesc2"].ToString());
            //cell.CellStyle = style;

            cell = dataRow.CreateCell(6);
            cell.CellStyle = style;
            cell = dataRow.CreateCell(7);
            cell.SetCellValue(row["Curr"].ToString());
            cell.CellStyle = style;


            rowIndex++;
                        


        }

        for (int i = sheet.LastRowNum; i > SourceTable.Rows.Count + 12; i--)
        {
            sheet.ShiftRows(i, i + 1, -1);
            
        }

        #endregion

        hssfworkbook.Write(ms);
        //workbook.Write(ms);
        ms.Flush();
        ms.Position = 0;
        sheet = null;
        headerRow = null;
        //workbook = null;
        hssfworkbook = null;
        return ms;
    }

      public DataTable selectPCMstr(string QAD, string vender, string venderName, string domain, bool isNotEnd, string curr, string ddlUmType, bool IsNotEffect)
    {

        string sqlstr = "sp_pc_selectPCMstr";
        SqlParameter[] param = new SqlParameter[]{
        new SqlParameter("@vender",vender )
        ,new SqlParameter("@venderName" ,venderName)
        ,new SqlParameter("@QAD" ,QAD)
        ,new SqlParameter("@domain" ,domain)
        ,new SqlParameter("@isNotEnd",isNotEnd)
        ,new SqlParameter("@curr",curr)
        ,new SqlParameter("@ddlUmType",ddlUmType)
        ,new SqlParameter("@IsNotEffect",IsNotEffect)
        };

        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param).Tables[0];

    }




    #region 报价核价导入相关
    /// <summary>
    /// 
    /// </summary>
    /// <param name="strFileName"></param>
    /// <param name="strUID"></param>
    /// <param name="IMID"></param>
    /// <param name="message"></param>
    /// <param name="status">0是报价，1是核价</param>
    /// <returns></returns>
    public bool uploadPrice(string strFileName, string strUID, string IMID, out string message, int status)
    {
        message = "";
        DataTable dt = null;
        bool success = true;
        try
        {
            NPOIHelper helper = new NPOIHelper();
            dt = helper.GetExcelContents(strFileName);
            //dt = adam.getExcelContents(strFileName).Tables[0];
        }
        catch (Exception ex)
        {
            message = "导入文件必须是Excel格式'" + ex.Message.ToString() + "'.";
            success = false;
        }
        finally
        {
            if (File.Exists(strFileName))
            {
                File.Delete(strFileName);
            }
        }
        if (success)
        {
            try
            {
                if (
                    dt.Columns[0].ColumnName != "inquiry" ||
                    dt.Columns[1].ColumnName != "QAD" ||
                    dt.Columns[2].ColumnName != "price" ||
                    dt.Columns[3].ColumnName != "priceSelf"||
                    dt.Columns[4].ColumnName != "priceDiscount" ||
                    dt.Columns[5].ColumnName != "checkPrice" ||
                    dt.Columns[6].ColumnName != "isDelete"
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
                TempColumn.ColumnName = "inquiry";
                TempTable.Columns.Add(TempColumn);

                TempColumn = new DataColumn();
                TempColumn.DataType = System.Type.GetType("System.String");
                TempColumn.ColumnName = "QAD";
                TempTable.Columns.Add(TempColumn);

                TempColumn = new DataColumn();
                TempColumn.DataType = System.Type.GetType("System.Decimal");
                TempColumn.ColumnName = "price";
                TempTable.Columns.Add(TempColumn);

                TempColumn = new DataColumn();
                TempColumn.DataType = System.Type.GetType("System.Decimal");
                TempColumn.ColumnName = "priceSelf";
                TempTable.Columns.Add(TempColumn);

                TempColumn = new DataColumn();
                TempColumn.DataType = System.Type.GetType("System.String");
                TempColumn.ColumnName = "priceDiscount";
                TempTable.Columns.Add(TempColumn);

                TempColumn = new DataColumn();
                TempColumn.DataType = System.Type.GetType("System.Decimal");
                TempColumn.ColumnName = "checkPrice";
                TempTable.Columns.Add(TempColumn);

                TempColumn = new DataColumn();
                TempColumn.DataType = System.Type.GetType("System.String");
                TempColumn.ColumnName = "isDelete";
                TempTable.Columns.Add(TempColumn);

                TempColumn = new DataColumn();
                TempColumn.DataType = System.Type.GetType("System.String");
                TempColumn.ColumnName = "error";
                TempTable.Columns.Add(TempColumn);

                TempColumn = new DataColumn();
                TempColumn.DataType = System.Type.GetType("System.Int32");
                TempColumn.ColumnName = "createdby";
                TempTable.Columns.Add(TempColumn);

                TempColumn = new DataColumn();
                TempColumn.DataType = System.Type.GetType("System.String");
                TempColumn.ColumnName = "createdDate";
                TempTable.Columns.Add(TempColumn);

                

                if (dt.Rows.Count > 0)
                {


                    //decimal cost = -1;
                    string createdBy = strUID;
                    string createdDate = DateTime.Now.ToString();

                    DateTime dateFormat = DateTime.Now;

                    //先清空临时表中该上传员工的记录
                    if (ClearInquiryTempTable(Convert.ToInt32(strUID)))
                    {

                        for (int i = 0; i <= dt.Rows.Count - 1; i++)
                        {
                            long qadNo = 0;
                            string inquiry = string.Empty;
                            string QAD = string.Empty;
                            decimal priceT = -1;
                            string price = string.Empty;
                            decimal priceSelfT = -1;
                            string priceSelf = string.Empty;
                            string priceDiscount = string.Empty;
                            decimal checkPriceT = -1;
                            string checkPrice = string.Empty;
                            string errorMsg = string.Empty;
                            string isdelete = string.Empty;
                            TempRow = TempTable.NewRow();//创建新的行

                         

                            if (dt.Rows[i].IsNull(0))
                            {
                                errorMsg += "询价单号不能为空;";
                            }
                            else if (dt.Rows[i].ItemArray[0].ToString().Trim().Length != 9)
                            {
                                errorMsg += "询价单号只可能是9位;";
                            }
                            else if (!IMID.Equals(dt.Rows[i].ItemArray[0].ToString().Trim()))
                            {
                                errorMsg += "询价单号与本单不符;";
                            }
                            else
                            {
                                inquiry = dt.Rows[i].ItemArray[0].ToString().Trim();
                            }


                            if (dt.Rows[i].IsNull(1))
                            {
                                errorMsg += "QAD不能为空;";
                            }
                            else if (!long.TryParse(dt.Rows[i].ItemArray[1].ToString().Trim(), out qadNo))
                            {
                                errorMsg += "QAD只能是数字;";

                            }
                            else if (dt.Rows[i].ItemArray[1].ToString().Trim().Length != 14)
                            {
                                errorMsg += "QAD只能是14位数字;";
                            }
                            else
                            {
                                QAD = dt.Rows[i].ItemArray[1].ToString().Trim();
                            }

                            if (!string.Empty.Equals(dt.Rows[i].ItemArray[2].ToString().Trim()))
                            {
                                if (!decimal.TryParse(dt.Rows[i].ItemArray[2].ToString().Trim(), out priceT))
                                {
                                    errorMsg += "报价必须是数字;";
                                }
                                else if (priceT == 0)
                                {
                                    errorMsg += "报价不能为零;";
                                }
                                else
                                {
                                    price = dt.Rows[i].ItemArray[2].ToString().Trim();
                                }

                            }
                          
                            if (!string.Empty.Equals(dt.Rows[i].ItemArray[3].ToString().Trim()))
                            {
                                if (!decimal.TryParse(dt.Rows[i].ItemArray[3].ToString().Trim(), out priceSelfT))
                                {
                                    errorMsg += "自报价必须是数字;";
                                }
                                else if (priceSelfT == 0)
                                {
                                    errorMsg += "自报价不能为零;";
                                }
                                else
                                {
                                    priceSelf = dt.Rows[i].ItemArray[3].ToString().Trim();
                                }
                            }
                           


                            if (!string.Empty.Equals(dt.Rows[i].ItemArray[5].ToString().Trim()))
                            {
                                if (!decimal.TryParse(dt.Rows[i].ItemArray[5].ToString().Trim(), out checkPriceT))
                                {
                                    errorMsg += "核价必须是数字;";
                                }
                                else if (checkPriceT == 0)
                                {
                                    errorMsg += "价格不能为零;";
                                }
                                else
                                {
                                    checkPrice = dt.Rows[i].ItemArray[5].ToString().Trim();
                                }

                            }
                           

                            priceDiscount = dt.Rows[i].ItemArray[4].ToString().Trim();


                            if (!dt.Rows[i].IsNull(6) && dt.Rows[i].ItemArray[6].ToString() != "delete")
                            {
                                errorMsg += "删除行必须是写“delete”或者为空;";
                            }
                            else
                            {
                                isdelete = dt.Rows[i].ItemArray[6].ToString();
                            }

                            if (dt.Rows[i].IsNull(0) && dt.Rows[i].IsNull(1) && dt.Rows[i].IsNull(2) && dt.Rows[i].IsNull(3) && dt.Rows[i].IsNull(4) && dt.Rows[i].IsNull(5))
                            {
                                continue;
                            }

                            //TempRow["cost"] 
                            TempRow["inquiry"] = inquiry;
                            TempRow["QAD"] = QAD;
                            if (string.Empty.Equals(price))
                            {
                                TempRow["price"] = DBNull.Value;
                            }
                            else
                            {
                                TempRow["price"] = price;
                            }
                            if (string.Empty.Equals(priceSelf))
                            {
                                TempRow["priceSelf"] = DBNull.Value;
                            }
                            else
                            {
                                TempRow["priceSelf"] = priceSelf;
                            }
                          
                            if (status==0)
                            {
                                if (string.Empty.Equals(price) && string.Empty.Equals(priceSelf))
                                {
                                    errorMsg += "报价时，报价和自报价必须有一个";
                                }
                                if (!string.Empty.Equals(checkPrice))
                                {
                                    errorMsg += "报价时，不可以上传核价";
                                }
                            }
                            if (string.Empty.Equals(checkPrice))
                            {
                                TempRow["checkPrice"] = DBNull.Value;
                            }
                            else
                            {
                                TempRow["checkPrice"] = checkPrice;
                            }
                            TempRow["priceDiscount"] = priceDiscount;
                            TempRow["isDelete"] = isdelete;
                            TempRow["error"] = errorMsg.Trim();
                            TempRow["createdBy"] = Convert.ToInt32(createdBy);
                            TempRow["createdDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                            TempTable.Rows.Add(TempRow);
                        }
                        //TempTable有数据的情况下批量复制到数据库里
                        if (TempTable != null && TempTable.Rows.Count > 0)
                        {
                            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(adam.dsn0(), SqlBulkCopyOptions.UseInternalTransaction))
                            {
                                bulkCopy.DestinationTableName = "PC_inquiryImportTemp";

                                bulkCopy.ColumnMappings.Clear();

                                bulkCopy.ColumnMappings.Add("inquiry", "IMID");
                                bulkCopy.ColumnMappings.Add("QAD", "QAD");
                                bulkCopy.ColumnMappings.Add("price", "price");
                                bulkCopy.ColumnMappings.Add("priceSelf", "priceSelf");
                                bulkCopy.ColumnMappings.Add("priceDiscount", "priceDiscount");
                                bulkCopy.ColumnMappings.Add("checkPrice", "checkPrice");
                                bulkCopy.ColumnMappings.Add("isDelete", "isDelete");
                                bulkCopy.ColumnMappings.Add("error", "error");
                                bulkCopy.ColumnMappings.Add("createdBy", "createdBy");
                                bulkCopy.ColumnMappings.Add("createdDate", "createdDate");
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
                            if (CheckInquiryTempTable(Convert.ToInt32(strUID),IMID,status==0?"0":"1" ))//0是报价，1是核价
                            {
                                //判断上传内容能否通过验证
                                if (JudgeInquiryTempTable(Convert.ToInt32(strUID)))
                                {
                                    int flag = (TransInquiryTempTable(Convert.ToInt32(strUID), IMID, status == 0 ? "0" : "1"));
                                    if(flag==1)
                                    {
                                        message = "导入文件并保存成功";
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
                if (File.Exists(strFileName))
                {
                    File.Delete(strFileName);
                }
            }
        }
        return success;

    }
    /// <summary>
    /// 转插入
    /// </summary>
    /// <param name="uID"></param>
    /// <param name="IMID"></param>
    /// <param name="status"></param>
    /// <returns></returns>
    private int TransInquiryTempTable(int uID, string IMID, string status)
    {
        string sqlstr = "sp_pc_insertInquiryImportTempToDet";
        SqlParameter[] param = new SqlParameter[]{
        new SqlParameter("@uID",uID)
        ,new SqlParameter("@IMID",IMID)
        ,new SqlParameter("@status",status)
        };
        return Convert.ToInt32(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param));
    }


    /// <summary>
    /// 清空上传表
    /// </summary>
    /// <param name="uID"></param>
    /// <returns></returns>
    private bool ClearInquiryTempTable(int uID)
    {
        string sqlstr = "sp_pc_deleteInquiryImportTempByID";
        SqlParameter[] param = new SqlParameter[]{
        new SqlParameter("@uID",uID)
       
        };
        return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param));
    }

    private bool JudgeInquiryTempTable(int uID)
    {
        string sqlstr = "sp_pc_selectInquiryImportTempByID";
        SqlParameter[] param = new SqlParameter[]{
        new SqlParameter("@uID",uID)
       
        };
        return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param));
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="uID"></param>
    /// <param name="IMID"></param>
    /// <param name="status">0报价1核价</param>
    /// <returns></returns>
    private bool CheckInquiryTempTable(int uID, string IMID, string status)
    {
        string sqlstr = "sp_pc_checkInquiryImportTempByID";
        SqlParameter[] param = new SqlParameter[]{
        new SqlParameter("@uID",uID)
        ,new SqlParameter("@IMID",IMID)
        ,new SqlParameter("@status",status)
        };
        return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param));
    }

    public DataTable GetInquiryPriceImportError(string strUID)
    {
        string sqlstr = "sp_pc_selectInquiryPriceImportError";
        SqlParameter[] param = new SqlParameter[]{
        new SqlParameter("@uID",strUID)
        };

        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param).Tables[0];
    }

    #endregion




    public bool updatePassApply(string PQID, int uID)
    {
        string sqlstr = "sp_pc_updatePassApply";

        SqlParameter[] param = new SqlParameter[]{
        new SqlParameter("@PQID",PQID)
        ,new SqlParameter("@uID",uID)

        };

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param));
    }



    public DataTable selectApplyQADStatus(int uID, string QAD, string vender, string venderName, string status)
    {
        string sqlstr = "sp_pc_selectApplyQADStatus";
        SqlParameter[] param = new SqlParameter[]{
        new SqlParameter("@uID",uID)
        ,new SqlParameter("@QAD",QAD)
        ,new SqlParameter("@vender",vender)
        ,new SqlParameter("@venderName",venderName)
        ,new SqlParameter("@status",status)
        };

        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param).Tables[0];
    }

    public object selectApplyPerson()
    {
        string sqlstr = "sp_pc_selectApplyPerson";

        return (SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, sqlstr).Tables[0]);
    }



    public bool addApplyPerson(string addUserID, int uID)
    {
        string sqlstr = "sp_pc_addApplyPerson";

        SqlParameter[] param = new SqlParameter[]{
        new SqlParameter("@addUserID",addUserID)
        ,new SqlParameter("@uID",uID)

        };

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param));
    }

    public bool deleteApplyPerson(int userID)
    {
        string sqlstr = "sp_pc_deleteApplyPerson";

        SqlParameter[] param = new SqlParameter[]{
        new SqlParameter("@userID",userID)
     

        };

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param));
    }

    public bool cancelInquiryDet(string applyDetId, int uID)
    {
        string sqlstr = "sp_pc_cancelInquiryDet";

        SqlParameter[] param = new SqlParameter[]{
        new SqlParameter("@applyDetId",applyDetId)
        ,new SqlParameter("@uID",uID)
        };

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param));
    }

    //public DataTable select100AndQADPCMSTR(string QAD, string vender, string venderName, int isCheck)
    //{
    //    string sqlstr = "sp_pc_select100AndQADPCMSTR";
    //    SqlParameter[] param = new SqlParameter[]{
    //    new SqlParameter("@QAD",QAD)
    //    ,new SqlParameter("@vender",vender)
    //    ,new SqlParameter("@venderName",venderName)
    //    ,new SqlParameter("@isCheck",isCheck)
    //    };

    //    return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param).Tables[0];
    //}

    public void sendMailForReject(string PQID, int uID)
    {
        string sqlstr = "sp_pc_sendMailForReject";
        string createdName = string.Empty;
        string createdMail = string.Empty;
        string rejectDate = string.Empty;
        string rejectReason = string.Empty;
        string rejectName = string.Empty;
        SqlParameter[] param = new SqlParameter[]{
        new SqlParameter("@uID",uID)
        ,new SqlParameter("@PQID",PQID)
        ,new SqlParameter("@createdName",SqlDbType.NVarChar,24)
        ,new SqlParameter("@createdMail",SqlDbType.NVarChar,50)
        ,new SqlParameter("@rejectDate",SqlDbType.VarChar,30)
        ,new SqlParameter("@rejectReason",SqlDbType.NVarChar,400)
        ,new SqlParameter("@rejectName",SqlDbType.NVarChar,24)
        };

        param[2].Direction = ParameterDirection.Output;
        param[3].Direction = ParameterDirection.Output;
        param[4].Direction = ParameterDirection.Output;
        param[5].Direction = ParameterDirection.Output;
        param[6].Direction = ParameterDirection.Output;
        DataTable dt =  SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param).Tables[0];
        createdName = param[2].Value.ToString();
        createdMail = param[3].Value.ToString();
        rejectDate = param[4].Value.ToString();
        rejectReason = param[5].Value.ToString();
        rejectName = param[6].Value.ToString();



        StringBuilder sb = new StringBuilder();
        sb.Append("<html>");
        sb.Append("<body>");
        sb.Append("<form>");
        sb.Append(" Dear  " + createdName + "  <br>");
        sb.Append("     您的报价申请" + PQID + " 中<br>");
        sb.Append("     您申请的零件：<br>");
        foreach (DataRow row in dt.Rows)
        {
            sb.Append("     QAD:" + row["Part"].ToString() + "供应商:" + row["Vender"].ToString() + "供应商名:" + row["VenderName"].ToString() + "<br>");
        }
        sb.Append("    被" + rejectName + " 驳回<br>");
        sb.Append("    请尽快处理 ");
        sb.Append("</form>");
        sb.Append("</html>");
        SendMail(createdMail, createdName, sb.ToString());
    }


    private bool SendMail(string toEmailAddress, string toEmailName, string mailBody)
    {

        MailAddress from = new MailAddress(ConfigurationManager.AppSettings["AdminEmail"].ToString());
        MailAddress to;
        MailMessage mail = new MailMessage();
        mail.From = from;
        Boolean isSuccess = false;
        try
        {
            to = new MailAddress(toEmailAddress, toEmailName);
            mail.To.Add(to);
        }
        catch
        {
            return false;
        }

        try
        {
            mail.Subject = "[Notify]零件驳回";
            mail.Body = mailBody;
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.Normal;
            mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;

            SmtpClient client = new SmtpClient();
            client.Host = ConfigurationManager.AppSettings["mailServer"].ToString();
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["AdminEmail"].ToString(), ConfigurationManager.AppSettings["AdminEmailPwd"].ToString());
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Send(mail);
            isSuccess = true;
        }
        catch (Exception ex)
        {
            isSuccess = false;
        }

        return isSuccess;


    }

    public DataTable selectApplyDetExport(string PQID)
    {
        string sqlstr = "sp_pc_selectApplyDetExport";
        SqlParameter[] param = new SqlParameter[]{
        new SqlParameter("@PQID",PQID)
        };

        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param).Tables[0];
    }

    public DataTable select100AndQADPCMSTR(string QAD, string vender, string venderName, int check, string domain,int diff,int is100,string type)
    {
        string sqlstr = "sp_pc_selectPcMstrCheck";
        SqlParameter[] param = new SqlParameter[]{
        new SqlParameter("@vender",vender)
        ,new SqlParameter("@QAD",QAD)
        ,new SqlParameter("@venderName",venderName)
        ,new SqlParameter("@domain",domain)
        ,new SqlParameter("@check",check)
        ,new SqlParameter("@diff",diff)
        ,new SqlParameter ("@is100",is100)
        ,new SqlParameter("@type",type)
        };

        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param).Tables[0];
    }

    public bool checkPricePass(string id)
    {
        string sqlstr = "sp_pc_checkPricePass";

        SqlParameter[] param = new SqlParameter[]{
        new SqlParameter("@id",id)
        };
        return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param));
    }

    public int updateDesc(string detid,string PQID, int uID)
    {
        string sqlstr = "sp_pc_updateDesc";
        SqlParameter[] param = new SqlParameter[]{
        new SqlParameter("@detid",detid)
        ,new SqlParameter("@PQID",PQID)
        ,new SqlParameter("@uID",uID)
        ,new SqlParameter("@GUID",getGUID())
        };
        return Convert.ToInt32(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param));
    }

    public SqlDataReader SelectType()
    {
        try
        {
            string sqlstr = "sp_pc_selectTypeForCheckPcMstr";

            return SqlHelper.ExecuteReader(adam.dsn0(), CommandType.StoredProcedure, sqlstr);

        }
        catch
        {
            return null;
        }
    
    }

    public DataTable PriceCheckBetweenQadAnd100(string part, string vender, string domain, string type, string diff, string venderName)
    {
        try
        {
            string sqlstr = "sp_pc_contrastPC_MSTR";

            SqlParameter[] param = new SqlParameter[]{
                new SqlParameter("@QAD",part)
                , new SqlParameter("@vender",vender)
                , new SqlParameter("@venderName",venderName)
                , new SqlParameter("@type" , type)
                , new SqlParameter("@domain" , domain)
                , new SqlParameter("@diff" , diff)                
            };

            return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param).Tables[0];
            

        }
        catch
        {
            return null;
            
        }
    }

    public DataTable selectAddDomain(string QAD, string vender, string venderName, string code, bool isAddDomain)
    {
        try
        {
            string sqlstr = "sp_pc_selectAddDomain";

            SqlParameter[] param = new SqlParameter[]{
                new SqlParameter("@QAD" , QAD)
                , new SqlParameter("@vender" , vender)
                , new SqlParameter("@venderName" , venderName)
                , new SqlParameter("@code" , code)
                , new SqlParameter("@isAddDomain" , isAddDomain)
              
            };

            return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param).Tables[0];

        }
        catch
        {
            return null;
        }
    }


    public void ExportExcel(string tempFile, string outputFile, DataTable detail,bool isAdd,string uID)
    {
        FileStream templetFile = new FileStream(tempFile, FileMode.Open, FileAccess.Read);
        IWorkbook workbook = new HSSFWorkbook(templetFile);
        DataSet ds = GetCimloadData(detail, isAdd ,uID);

        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count >= 1)
            {
                if (ds.Tables[0].Rows[0][0].ToString().Equals("0"))
                {
                    throw new Exception("处理出错请联系管理员");

                }
            }
            for (int i = 1; i <= 6; i++)
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
        }
        // 输出Excel文件并退出 
        try
        {
            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);

                Stream localFile = new FileStream(outputFile, FileMode.OpenOrCreate);

                localFile.Write(ms.ToArray(), 0, (int)ms.Length);
                localFile.Dispose();

                ms.Flush();
                ms.Position = 0;

                workbook = null;
            }
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    private DataSet GetCimloadData(DataTable detail, bool isAdd , string uID)
    {
        try
        {
            StringWriter writer = new StringWriter();
            detail.WriteXml(writer);
            string xmlDetail = writer.ToString();

            string sqlstr = "sp_pc_getCimloadToAddDomain";

            SqlParameter[] param = new SqlParameter[]{
                  new SqlParameter("@detail", xmlDetail)
                  ,new SqlParameter("@isAdd",isAdd)
                  ,new SqlParameter("@uID",uID)
            };

            return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param);
        }
        catch
        {
             return null;
        }
       

    }

    public bool addDomainToPcMstr(DataTable dt, string uID)
    {
         StringWriter writer = new StringWriter();
            dt.WriteXml(writer);
            string xmlDetail = writer.ToString();
        try
        {
            string sqlstr = "sp_pc_addDomainToPcMstr";

            SqlParameter[] param = new SqlParameter[]{
                    new SqlParameter("@detail", xmlDetail)
                    , new SqlParameter("@uID",uID)
         
            };

            return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param));
        }
        catch { return false; }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dt"></param>
    /// <param name="uID"></param>
    /// <param name="PQID"></param>
    /// <param name="uName"></param>
    /// <param name="isout"></param>
    /// <param name="typeValue">1,元器件
    ///                         2，结构件
    ///                         3，包装</param>
    /// <returns></returns>
    public bool AddNewPQFromNPart(DataTable dt, string uID, out string PQID, out string uName, bool isout,string typeValue)
    {
        StringWriter writer = new StringWriter();
        dt.WriteXml(writer);
        string xmlDetail = writer.ToString();


        string sqlstr = "sp_pc_AddNewPQFromNPart";

            SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@uID",uID )
            ,new SqlParameter("@PQNO",SqlDbType.Char,9)
            ,new SqlParameter("@uName",SqlDbType.NVarChar,20)
            ,new SqlParameter("@isout",isout)
            ,new SqlParameter("@xml",xmlDetail)
            ,new SqlParameter("@typeValue",typeValue)
            };
            param[1].Direction = ParameterDirection.Output;
            param[2].Direction = ParameterDirection.Output;
            bool flag = Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param));
            PQID = param[1].Value.ToString();
            uName = param[2].Value.ToString();
            return flag;

           
      
    }

    public bool importPartVendorToAppv(DataTable dt, out string message, string uID , out DataTable errorDt)
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
                    dt.Columns[0].ColumnName != "QAD" ||
                    dt.Columns[1].ColumnName != "物料号" ||
                    dt.Columns[2].ColumnName != "供应商" ||
                    dt.Columns[3].ColumnName != "单位" ||
                    dt.Columns[4].ColumnName != "技术参考价"||
                    dt.Columns[5].ColumnName != "需求规格" ||
                    dt.Columns[6].ColumnName != "备注" /*||
                    dt.Columns[6].ColumnName != "小类区分" ||
                    dt.Columns[7].ColumnName != "子物料" */
               
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
                TempColumn.ColumnName = "QAD";
                TempTable.Columns.Add(TempColumn);

                TempColumn = new DataColumn();
                TempColumn.DataType = System.Type.GetType("System.String");
                TempColumn.ColumnName = "itemCode";
                TempTable.Columns.Add(TempColumn);

                TempColumn = new DataColumn();
                TempColumn.DataType = System.Type.GetType("System.String");
                TempColumn.ColumnName = "vendor";
                TempTable.Columns.Add(TempColumn);

                TempColumn = new DataColumn();
                TempColumn.DataType = System.Type.GetType("System.String");
                TempColumn.ColumnName = "formate";
                TempTable.Columns.Add(TempColumn);

                TempColumn = new DataColumn();
                TempColumn.DataType = System.Type.GetType("System.String");
                TempColumn.ColumnName = "um";
                TempTable.Columns.Add(TempColumn);

                TempColumn = new DataColumn();
                TempColumn.DataType = System.Type.GetType("System.String");
                TempColumn.ColumnName = "technicalPrice";
                TempTable.Columns.Add(TempColumn);

                TempColumn = new DataColumn();
                TempColumn.DataType = System.Type.GetType("System.String");
                TempColumn.ColumnName = "remark";
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

                            TempRow["QAD"] = dt.Rows[i].ItemArray[0].ToString().Trim();
                            TempRow["itemCode"] = dt.Rows[i].ItemArray[1].ToString().Trim();
                            TempRow["vendor"] = dt.Rows[i].ItemArray[2].ToString().Trim();
                            TempRow["um"] = dt.Rows[i].ItemArray[3].ToString().Trim();
                            TempRow["technicalPrice"] = dt.Rows[i].ItemArray[4].ToString().Trim();
                            TempRow["formate"] = dt.Rows[i].ItemArray[5].ToString().Trim();
                            TempRow["remark"] = dt.Rows[i].ItemArray[6].ToString().Trim();
                               
                            

                            TempTable.Rows.Add(TempRow);
                        }
                        
                        StringWriter writer = new StringWriter();
                        TempTable.WriteXml(writer);
                        string xmlDetail = writer.ToString();
                   
                            string sqlstr = "sp_pc_importPartVendorToAppv";

                            SqlParameter[] param = new SqlParameter[]{
                             new SqlParameter("@detail", xmlDetail)
                             , new SqlParameter("@uID",uID)
                        
                                 };

                          errorDt =  SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param).Tables[0];
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

    public object selectDocByPartAndVendor(string QAD, string vendor)
    {
        string sqlstr = "sp_pc_selectDocByPartAndVendor";

        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@QAD",QAD)
            , new SqlParameter("@vendor",vendor)
        
        };

        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param).Tables[0];
    }

    public DataTable selectQADDocHistByDocID(string code, string typeid, string cateid)
    {
        string sqlstr = "sp_pc_selectQADDocHistByDocID";

        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@code",code)
            , new SqlParameter("@typeid",typeid)
            , new SqlParameter("@cateid",cateid)
        };

        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param).Tables[0];
    }

    public object selectApplyPassList(string QAD, string code, string vendor, string vendorName, string uID,string sou)
    {
        string sqlstr = "sp_NPart_selectApplyPassList";

        SqlParameter[] param = new SqlParameter[]{
        
             new SqlParameter("@QAD",QAD)
            , new SqlParameter("@code",code)
            , new SqlParameter("@vendor",vendor)
            , new SqlParameter("@vendorName",vendorName)
            , new SqlParameter("@uID",uID)
            , new SqlParameter("@sou",sou)

        };

        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param).Tables[0];
    }







    public DataTable selectUpdateFormateTemp(string QAD, string code, string vendor, string vendorName, string uID, string sou)
    {
        string sqlstr = "sp_NPart_selectUpdateFormateTemp";

        SqlParameter[] param = new SqlParameter[]{
        
             new SqlParameter("@QAD",QAD)
            , new SqlParameter("@code",code)
            , new SqlParameter("@vendor",vendor)
            , new SqlParameter("@vendorName",vendorName)
            , new SqlParameter("@uID",uID)
            , new SqlParameter("@sou",sou)
        };

        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param).Tables[0];
    }

    public bool importNPartPassFormate(DataTable dt, out string message, string uID)
    {
        message = "";
        dt.TableName = "TempTable";
        bool success = true;

        if (success)
        {
            try
            {
                if (
                    dt.Columns[0].ColumnName != "QAD" ||
                    dt.Columns[1].ColumnName != "供应商" ||
                    dt.Columns[2].ColumnName != "单位" ||
                    dt.Columns[3].ColumnName != "需求规格" 
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
                TempColumn.ColumnName = "QAD";
                TempTable.Columns.Add(TempColumn);

                TempColumn = new DataColumn();
                TempColumn.DataType = System.Type.GetType("System.String");
                TempColumn.ColumnName = "vendor";
                TempTable.Columns.Add(TempColumn);

                TempColumn = new DataColumn();
                TempColumn.DataType = System.Type.GetType("System.String");
                TempColumn.ColumnName = "um";
                TempTable.Columns.Add(TempColumn);

                TempColumn = new DataColumn();
                TempColumn.DataType = System.Type.GetType("System.String");
                TempColumn.ColumnName = "formate";
                TempTable.Columns.Add(TempColumn);

                if (dt.Rows.Count > 0)
                {


                    for (int i = 0; i <= dt.Rows.Count - 1; i++)
                    {

                        if (dt.Rows[i].ItemArray[0].ToString().Trim().Equals(string.Empty))
                        {
                            continue;
                        }
                        //TempRow["cost"] 
                        TempRow = TempTable.NewRow();//创建新的行

                        TempRow["QAD"] = dt.Rows[i].ItemArray[0].ToString().Trim();
                        TempRow["vendor"] = dt.Rows[i].ItemArray[1].ToString().Trim();

                        string um = dt.Rows[i].ItemArray[2].ToString().Trim().ToUpper();



                        if (um.Equals("EA") || um.Equals("BO") || um.Equals("G") || um.Equals("KG") || um.Equals("M") || um.Equals("PC") || um.Equals("RL") || um.Equals("L"))
                        {
                            TempRow["um"] = um;
                        }
                        else
                        {
                            message = "您导入的文件中存在单位符，重新修改在导入!";
                            return false;
                        }

                        
                        TempRow["formate"] = dt.Rows[i].ItemArray[3].ToString().Trim();

                        TempTable.Rows.Add(TempRow);
                    }

                    StringWriter writer = new StringWriter();
                    TempTable.WriteXml(writer);
                    string xmlDetail = writer.ToString();

                    string sqlstr = "sp_pc_updateFormate";

                    SqlParameter[] param = new SqlParameter[]{
                             new SqlParameter("@detail", xmlDetail)
                             , new SqlParameter("@uID",uID)
                        
                                 };

                    success = Convert.ToBoolean( SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param));


                    if (success)
                    {
                      
                       message = "导入文件成功!";
                    }
                    else
                    {
                       message = "导入文件失败!";
                       
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

    public bool deleteNPartFromAddApplyPage(string id, string uID , string uName)
    {
        string sqlstr = "sp_pc_deleteNPartFromAddApplyPage";

        SqlParameter[] param = new SqlParameter[]{
                    new SqlParameter("@id", id)
                    , new SqlParameter("@uID",uID)
                    , new SqlParameter("@uName",uName)
            };

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param));
    }

    public DataTable viewBasis(string IMID)
    {
        string sqlstr = "sp_pc_selectBasisByIMID";

        SqlParameter[] param = new SqlParameter[]{
        
             new SqlParameter("@IMID",IMID)
           

        };

        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param).Tables[0];
    }

    public bool importNPartPassUM(DataTable dt, out string message, string uID, string uName, out DataTable errorDt)
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
                    dt.Columns[0].ColumnName != "QAD" ||
                    dt.Columns[1].ColumnName != "供应商" ||
                    dt.Columns[2].ColumnName != "单位" ||
                    dt.Columns[3].ColumnName != "技术参考价" ||
                    dt.Columns[4].ColumnName != "需求规格" ||
                    dt.Columns[5].ColumnName != "备注" 

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
                TempColumn.ColumnName = "QAD";
                TempTable.Columns.Add(TempColumn);

                TempColumn = new DataColumn();
                TempColumn.DataType = System.Type.GetType("System.String");
                TempColumn.ColumnName = "vendor";
                TempTable.Columns.Add(TempColumn);

                TempColumn = new DataColumn();
                TempColumn.DataType = System.Type.GetType("System.String");
                TempColumn.ColumnName = "formate";
                TempTable.Columns.Add(TempColumn);

                TempColumn = new DataColumn();
                TempColumn.DataType = System.Type.GetType("System.String");
                TempColumn.ColumnName = "um";
                TempTable.Columns.Add(TempColumn);

                TempColumn = new DataColumn();
                TempColumn.DataType = System.Type.GetType("System.String");
                TempColumn.ColumnName = "technicalPrice";
                TempTable.Columns.Add(TempColumn);

                TempColumn = new DataColumn();
                TempColumn.DataType = System.Type.GetType("System.String");
                TempColumn.ColumnName = "remark";
                TempTable.Columns.Add(TempColumn);

                

                if (dt.Rows.Count > 0)
                {


                    for (int i = 0; i <= dt.Rows.Count - 1; i++)
                    {

                        //TempRow["cost"] 
                        TempRow = TempTable.NewRow();//创建新的行

                        TempRow["QAD"] = dt.Rows[i].ItemArray[0].ToString().Trim();
                        TempRow["vendor"] = dt.Rows[i].ItemArray[1].ToString().Trim();
                        TempRow["um"] = dt.Rows[i].ItemArray[2].ToString().Trim();
                        TempRow["technicalPrice"] = dt.Rows[i].ItemArray[3].ToString().Trim();
                        TempRow["formate"] = dt.Rows[i].ItemArray[4].ToString().Trim();
                        TempRow["remark"] = dt.Rows[i].ItemArray[5].ToString().Trim();

                        TempTable.Rows.Add(TempRow);
                    }

                    StringWriter writer = new StringWriter();
                    TempTable.WriteXml(writer);
                    string xmlDetail = writer.ToString();

                    string sqlstr = "sp_pc_importPartVendorForDiffUm";

                    SqlParameter[] param = new SqlParameter[]{
                             new SqlParameter("@detail", xmlDetail)
                             , new SqlParameter("@uID",uID)
                        
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
}


    
