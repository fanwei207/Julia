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
public class PCM_price
{
    BasePage bp = new BasePage();
    public PCM_price()
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
        string sqlstr = "sp_pcm_selectItemCode";
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
    //    string strsql = "sp_pcm_AddApplyMstr";
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
    public int addVender(string formate, string vender, int uID, string PQID, string qad, string UM , string type,string applyPrice,string curr)
    {
        string strsql = "sp_pcm_addApplyVender";
        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@formate",formate)
            ,new SqlParameter("@vender",vender)
            ,new SqlParameter("@uID",uID)
            ,new SqlParameter("@PQID",PQID)
            ,new SqlParameter("@qad",qad)
            ,new SqlParameter("@UM",UM)
            ,new SqlParameter("@type",type)
            ,new SqlParameter("@applyPrice",applyPrice)
            ,new SqlParameter("@curr",curr)
        };
        return Convert.ToInt32(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strsql, param));
    }

    public bool changeReason(string PQID, int uID, string reason)
    {
        string sqlstr = "sp_pcm_changeReason";
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
        string sqlstr = "sp_pcm_selectVender";
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
        string sqlstr = "sp_pcm_deleteApplyVender";
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
        string sqlstr = "sp_pcm_selectApplyMstrList";
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

        string sqlstr = "sp_pcm_addNewPQ";
        SqlParameter[] param = new SqlParameter[]{
        new SqlParameter("@uID",uID )
        ,new SqlParameter("@PQNO",SqlDbType.Char,9)
        ,new SqlParameter("@uName",SqlDbType.NVarChar,20)
        , new SqlParameter("@isOut",isout)
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
        string sqlstr = "sp_pcm_deletePQ";
        SqlParameter[] param = new SqlParameter[]{
        new SqlParameter("@PQID",PQID)
        };
        return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param));
    }

    public DataTable selectApplyQADList(string QAD, string PQID)
    {
        string sqlstr = "sp_pcm_selectApplyQadList";
        SqlParameter[] param = new SqlParameter[]{
        new SqlParameter("@qad","")
        ,new SqlParameter("@PQID",PQID)
        };

        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param).Tables[0];





    }

    public bool checkQADNo(string QAD)
    {
        string sqlstr = "sp_pcm_checkQadNo";
        SqlParameter[] param = new SqlParameter[]{
        new SqlParameter("@QAD",QAD)
       
        };
        return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param));

    }

    public void selectVenderOnlyGv(string QADNO, GridView gvVender, string PQID)
    {
        string sqlstr = "sp_pcm_selectVenderOnlyGv";
        SqlParameter[] param = new SqlParameter[]{
        new SqlParameter("@qad",QADNO)
        ,new SqlParameter("@PQID",PQID)
        };

        DataTable dt = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param).Tables[0];

        this.GridViewDataBind(gvVender, dt);
    }

    public DataTable selectVenderCheckList(string venderNo, string venderName, int isfinish)
    {
        string sqlstr = "sp_pcm_selectQuotationAndCalculationList";
        SqlParameter[] param = new SqlParameter[]{
        new SqlParameter("@venderNo",venderNo )
        ,new SqlParameter("@venderName" ,venderName)
        ,new SqlParameter("@IsFinish" ,isfinish)
        };

        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param).Tables[0];

    }

    //public DataTable selectChangeList(string mstrID, string QAD, int isFinish)
    //{
    //    string sqlstr = "sp_pcm_selectChangeList";
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
    public int PQSubmit(string PQID, int uID, string applyType, out string QAD, out string vender, int SubmitCount)
    {
        string sqlstr = "sp_pcm_updatePQState";
        SqlParameter[] param = new SqlParameter[]{
        new SqlParameter("@PQID",PQID)
        ,new SqlParameter("@uID",uID)
        ,new SqlParameter("@QAD",SqlDbType.VarChar,20)
       ,new SqlParameter("@vender",SqlDbType.VarChar,10)
       ,new SqlParameter("@applyType",applyType)
       , new SqlParameter("@submitCount",SubmitCount)
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
        string sqlstr = "sp_pcm_selectQuotationList";
        SqlParameter[] param = new SqlParameter[]{
        new SqlParameter("@venderID" ,venderId)
        ,new SqlParameter("@isAll",isAll?1:0)
        ,new SqlParameter("@uID",uID)
        };
        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param).Tables[0];
    }

    public bool deleteApplyMstr(string qad, string PQID)
    {
        string sqlstr = "sp_pcm_deleteApplyMstr";
        SqlParameter[] param = new SqlParameter[]{
        new SqlParameter("@QAD" ,qad)
        ,new SqlParameter("@PQID",PQID)
        };

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param));
    }



    public bool toQuotation(int DetID, decimal price1, string price2, string curr, string fileAllName, int uID)
    {
        string sqlstr = "sp_pcm_updateApplyDetAboutQuotation";
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
        string sqlstr = "sp_pcm_selectCalculationList";
        SqlParameter[] param = new SqlParameter[]{
         new SqlParameter("@venderID" ,venderId)
        ,new SqlParameter("@isAll",isAll?1:0)
        ,new SqlParameter("@uID",uID)
        };

        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param).Tables[0];
    }

    public bool toCalculation(int DetID, decimal checkPrice, string fileAllName, int uID)
    {
        string sqlstr = "sp_pcm_updateApplyDetAboutCalculation";

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
        string sqlstr = "sp_pcm_selectChangePriceVenderForQAD";
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
        string sqlstr = "sp_pcm_selectApplyDetList";
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
        string sqlstr = "sp_pcm_selectNotInquiryFromApplyDet";
        SqlParameter[] param = new SqlParameter[]{
        new SqlParameter("@QAD",QAD)
        ,new SqlParameter("@vender",vender)
        ,new SqlParameter("@venderName",venderName)
        };

        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param).Tables[0];
    }

    public DataTable selectNotInquiryByVender(string vender)
    {
        string sqlstr = "sp_pcm_selectNotInquiryByVender";
        SqlParameter[] param = new SqlParameter[]{
        new SqlParameter("@vender",vender)
        };

        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param).Tables[0];
    }

    public bool insertTOInquiry(List<int> ilist, string vender, string venderName, int uID, out string IMID)
    {
        int flag = 0;
        string sqlstr = "sp_pcm_insertInquiryMstr";
        SqlParameter[] param = new SqlParameter[]{
        new SqlParameter("@vender",vender)
        ,new SqlParameter("@uID",uID)
        ,new SqlParameter("@venderName",venderName)
        ,new SqlParameter("@IMID",SqlDbType.Char,9)
        };
        param[3].Direction = ParameterDirection.Output;
        if (!Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param)))
        {
            flag = flag + 1;
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
                bulkCopy.DestinationTableName = "PCM_InquiryDet";

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
                    string sqlstr2 = "sp_pcm_deleteNullInquiryMstr";
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
            return false;
        }
        else
        {
            string sqlstr3 = "sp_pcm_updateApplyDetStatusInCreateIMID";
            SqlParameter[] param3 = new SqlParameter[]{
                        new SqlParameter("@IMID",IMID)
                        ,new SqlParameter("@uID",uID)
                        };
            SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, sqlstr3, param3);
            return true;
        }
    }

    public DataTable selectInquiryList(string IMID, string vender, string venderName, string QAD, string status,  string Code)
    {
        string sqlstr = "sp_pcm_selectInquiryList";
        SqlParameter[] param = new SqlParameter[]{
        new SqlParameter("@QAD",QAD)
        ,new SqlParameter("@vender",vender)
        ,new SqlParameter("@venderName",venderName)
        ,new SqlParameter("@IMID",IMID)
        ,new SqlParameter("@status",status)
        ,new SqlParameter("@code",Code)
        };

        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param).Tables[0];
    }

    public DataTable selectInquiryDet(string IMID, out string vender, out string verderName, out int statue, out string create, out string createDate, out string venderPhone, out string venderEmail)
    {
        string sqlstr = "sp_pcm_selectInquiryDet";
        SqlParameter[] param = new SqlParameter[]{
        new SqlParameter("@IMID",IMID)
        ,new SqlParameter("@vender",SqlDbType.VarChar,10)
        ,new SqlParameter("@venderName",SqlDbType.NVarChar,50)
       ,new SqlParameter("@statue",SqlDbType.Int)
      // ,new SqlParameter("@curr",SqlDbType.VarChar,5)
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
        //param[8].Direction = ParameterDirection.Output;

        DataTable dt = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param).Tables[0];
        vender = param[1].Value.ToString();
        verderName = param[2].Value.ToString();
        if (!int.TryParse(param[3].Value.ToString(), out statue))
        {
            statue = 0;
        }
       // curr = param[4].Value.ToString();
        create = param[4].Value.ToString();
        createDate = param[5].Value.ToString();
        venderPhone = param[6].Value.ToString();
        venderEmail = param[7].Value.ToString();
        return dt;
    }

    public bool insertBasis(string IMID, int uID, string flieName, string filePate, int type)
    {
        string sqlstr = "sp_pcm_insertBasis";
        SqlParameter[] param = new SqlParameter[]{
        new SqlParameter("@IMID",IMID)
        , new SqlParameter("@uID",uID)
        , new SqlParameter("@flieName",flieName)
        , new SqlParameter("@filePate",filePate)
        , new SqlParameter("@type",type)
        };

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param));
    }



    public int updateQuotation(DataTable dt, int uID, string IMID)
    {
        StringWriter writer = new StringWriter();
        dt.WriteXml(writer);
        string xmlDetail = writer.ToString();


        string sqlstr = "sp_pcm_updateQuotation";
        SqlParameter[] param = new SqlParameter[]{
        new SqlParameter("@uID",uID)
        , new SqlParameter("@gvTable",xmlDetail)
        ,new SqlParameter("@IMID",IMID)
        };

        return Convert.ToInt32(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param));
    }

    public int updateCheck(DataTable dt, int uID,  string IMID)
    {
        StringWriter writer = new StringWriter();
        dt.WriteXml(writer);
        string xmlDetail = writer.ToString();


        string sqlstr = "sp_pcm_updateCheck";
        SqlParameter[] param = new SqlParameter[]{
        new SqlParameter("@uID",uID)
       // , new SqlParameter("@curr",curr)
        , new SqlParameter("@gvTable",xmlDetail)
        ,new SqlParameter("@IMID",IMID)
        };

        return Convert.ToInt32(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param));
    }

    public DataTable selectBasis(string PQDetID, out string IMID)
    {
        string sqlstr = "sp_pcm_selectBasis";
        SqlParameter[] param = new SqlParameter[]{
        new SqlParameter("@PQDetID",PQDetID)
        ,new SqlParameter("@IMID",SqlDbType.VarChar,10)
        };

        param[1].Direction = ParameterDirection.Output;

        DataTable dt = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param).Tables[0];

        IMID = param[1].Value.ToString();

        return dt;
    }



    public bool updateBasis(int ID, int uID)
    {
        string sqlstr = "sp_pcm_updateBasis";
        SqlParameter[] param = new SqlParameter[]{
        new SqlParameter("@ID",ID)
        , new SqlParameter("@uID",uID)
        };

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param));
    }






    #region 申请导入相关
    public bool  applyImport(string filePath, string uId, string PQID, out string message, string InfoFrom,string type,DataTable dt,bool isOut)
    {
        
        message = "";
        bool isExcel = false;

            try
            {
                if (
                    dt.Columns[0].ColumnName != "QAD" ||
                    dt.Columns[1].ColumnName != "供应商" ||
                    dt.Columns[2].ColumnName != "需求规格" ||
                    dt.Columns[3].ColumnName != "采购单位" ||
                    dt.Columns[4].ColumnName != "基本单位" ||
                    dt.Columns[5].ColumnName != "转换因子" ||
                    dt.Columns[6].ColumnName != "币种" ||
                    dt.Columns[7].ColumnName != "当前最低价格" ||
                    dt.Columns[8].ColumnName != "当前价格" ||
                    dt.Columns[9].ColumnName != "当前最高价格" ||
                    dt.Columns[10].ColumnName != "当前是否可抵扣" ||
                    dt.Columns[11].ColumnName != "当前税率" ||
                    dt.Columns[12].ColumnName != "当前价格起始时间" ||
                    dt.Columns[13].ColumnName != "当前价格终止时间" ||
                    dt.Columns[14].ColumnName != "期望价格" ||
                    dt.Columns[15].ColumnName != "期望是否可抵扣" ||
                    dt.Columns[16].ColumnName != "期望税率" ||
                    dt.Columns[17].ColumnName != "期望价格起始时间" ||
                    dt.Columns[18].ColumnName != "期望价格终止时间"||
                    dt.Columns[19].ColumnName != "供应商名" ||
                    dt.Columns[20].ColumnName != "备注" ||
                    dt.Columns[21].ColumnName != "验证ID(请勿修改)" 
                    )
                {
                    dt.Reset();
                    message = "导入文件的模版不正确，请更新模板再导入!";
                    isExcel = false;
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
                TempColumn.ColumnName = "ptum";
                TempTable.Columns.Add(TempColumn);

                TempColumn = new DataColumn();
                TempColumn.DataType = System.Type.GetType("System.String");
                TempColumn.ColumnName = "changeFor";
                TempTable.Columns.Add(TempColumn);

                TempColumn = new DataColumn();
                TempColumn.DataType = System.Type.GetType("System.String");
                TempColumn.ColumnName = "curr";
                TempTable.Columns.Add(TempColumn);


                TempColumn = new DataColumn();
                TempColumn.DataType = System.Type.GetType("System.String");
                TempColumn.ColumnName = "oldPrice";
                TempTable.Columns.Add(TempColumn);

                TempColumn = new DataColumn();
                TempColumn.DataType = System.Type.GetType("System.String");
                TempColumn.ColumnName = "isDeductible";
                TempTable.Columns.Add(TempColumn);

                TempColumn = new DataColumn();
                TempColumn.DataType = System.Type.GetType("System.String");
                TempColumn.ColumnName = "taxes";
                TempTable.Columns.Add(TempColumn);

                TempColumn = new DataColumn();
                TempColumn.DataType = System.Type.GetType("System.String");
                TempColumn.ColumnName = "oldPriceStarDate";
                TempTable.Columns.Add(TempColumn);


                TempColumn = new DataColumn();
                TempColumn.DataType = System.Type.GetType("System.String");
                TempColumn.ColumnName = "oldPriceEndDate";
                TempTable.Columns.Add(TempColumn);

                TempColumn = new DataColumn();
                TempColumn.DataType = System.Type.GetType("System.String");
                TempColumn.ColumnName = "applyPrice";
                TempTable.Columns.Add(TempColumn);

                TempColumn = new DataColumn();
                TempColumn.DataType = System.Type.GetType("System.String");
                TempColumn.ColumnName = "applyIsDeductible";
                TempTable.Columns.Add(TempColumn);

                TempColumn = new DataColumn();
                TempColumn.DataType = System.Type.GetType("System.String");
                TempColumn.ColumnName = "applyTaxes";
                TempTable.Columns.Add(TempColumn);

                TempColumn = new DataColumn();
                TempColumn.DataType = System.Type.GetType("System.String");
                TempColumn.ColumnName = "applyPriceStarDate";
                TempTable.Columns.Add(TempColumn);

                TempColumn = new DataColumn();
                TempColumn.DataType = System.Type.GetType("System.String");
                TempColumn.ColumnName = "applyPriceEndDate";
                TempTable.Columns.Add(TempColumn);

 

                TempColumn = new DataColumn();
                TempColumn.DataType = System.Type.GetType("System.String");
                TempColumn.ColumnName = "venderName";
                TempTable.Columns.Add(TempColumn);


                TempColumn = new DataColumn();
                TempColumn.DataType = System.Type.GetType("System.String");
                TempColumn.ColumnName = "remark";
                TempTable.Columns.Add(TempColumn);


                TempColumn = new DataColumn();
                TempColumn.DataType = System.Type.GetType("System.String");
                TempColumn.ColumnName = "GUID";
                TempTable.Columns.Add(TempColumn);

                if (dt.Rows.Count > 0)
                {
                    

                    for (int i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        if (dt.Rows[i].IsNull(0) && dt.Rows[i].IsNull(1))
                        {
                            continue;
                        }

                        TempRow = TempTable.NewRow();

                        TempRow["PQID"] = PQID;
                        TempRow["QAD"] = dt.Rows[i].ItemArray[0].ToString().Trim();
                        TempRow["vender"] = dt.Rows[i].ItemArray[1].ToString().Trim();
                        TempRow["formate"] = dt.Rows[i].ItemArray[2].ToString().Trim();
                        TempRow["um"] = dt.Rows[i].ItemArray[3].ToString().Trim().ToUpper();
                        TempRow["ptum"] = dt.Rows[i].ItemArray[4].ToString().Trim().ToUpper();
                        TempRow["changeFor"] = dt.Rows[i].ItemArray[5].ToString().Trim().ToUpper();
                        TempRow["curr"] = dt.Rows[i].ItemArray[6].ToString().Trim().ToUpper();
                        TempRow["oldPrice"] = dt.Rows[i].ItemArray[8].ToString().Trim();
                        TempRow["isDeductible"] = dt.Rows[i].ItemArray[10].ToString().Trim();
                        TempRow["taxes"] = dt.Rows[i].ItemArray[11].ToString().Trim();
                        TempRow["oldPriceStarDate"] = dt.Rows[i].ItemArray[12].ToString().Trim();
                        TempRow["oldPriceEndDate"] = dt.Rows[i].ItemArray[13].ToString().Trim();
                        TempRow["applyPrice"] = dt.Rows[i].ItemArray[14].ToString().Trim();
                        TempRow["applyIsDeductible"] = dt.Rows[i].ItemArray[15].ToString().Trim();
                        TempRow["applyTaxes"] = dt.Rows[i].ItemArray[16].ToString().Trim();
                        TempRow["applyPriceStarDate"] = dt.Rows[i].ItemArray[17].ToString().Trim();
                        TempRow["applyPriceEndDate"] = dt.Rows[i].ItemArray[18].ToString().Trim();
                        TempRow["venderName"] = dt.Rows[i].ItemArray[19].ToString().Trim();
                        TempRow["remark"] = dt.Rows[i].ItemArray[20].ToString().Trim();
                        
                        TempRow["GUID"] = dt.Rows[i].ItemArray[21].ToString().Trim();
                     

                        TempTable.Rows.Add(TempRow);
                    }
                    //第几次导入
                    int importOrder = this.checkApplyImportOrder(Convert.ToInt32(uId), TempTable);
                    if (importOrder == 1)
                    {
                        if (!this.ClearTempTable(Convert.ToInt32(uId)))
                        {
                            message = "导入出错，清除数据出错请联系管理员";
                            isExcel = false;
                        }
                        DataTable dtOut = null;
                        try
                        {
                             dtOut= this.GetVenderOrQADInfo(Convert.ToInt32(uId), TempTable, isOut);
                        }
                        catch 
                        {
                            message = "第一次导入导出中有问题，请联系管理员";
                            isExcel = false;
                            return isExcel;

                        }
                        try
                        {
                            isExcel = this.JudgmentImportReturn(dtOut, out message);
                        }
                        catch (Exception e)
                        {
                            message = e.Message + ";第一次导入导出获取Excel，请联系管理员;" + dtOut != null ?  dtOut.Rows.Count.ToString(): "AA";
                            isExcel = false;
                            return isExcel;
                        }
                    }
                    else if (importOrder == 2)
                    {

                        DataTable dtOut = null;
                        try
                        {
                            dtOut = this.importVenderOrQADToTemp(Convert.ToInt32(uId), TempTable, isOut);
                        }
                        catch
                        {
                            message = "第二次导入导出中有问题，请联系管理员";

                        }
                        try
                        {
                            isExcel = this.JudgmentImportReturn(dtOut, out message);
                        }
                        catch (Exception e)
                        {
                            message = e.Message + ";第二次导入导出获取Excel，请联系管理员";
                        }


                        if (dtOut.Rows[0][0].ToString().Equals("1"))
                        {
                            message = "导入文件成功";
                           
                            if (!this.ClearTempTable(Convert.ToInt32(uId)))
                            {
                                message = "导入出错，清除数据出错请联系管理员";
                                isExcel = false;
                            }
                         


                        }
                    }
                    else if (importOrder == 0)
                    {
                        message = "导入出错，清除数据出错请联系管理员A";
                        isExcel = false;
                    }


                }
            }
            catch
            {
                message = "导入文件失败!";
                isExcel = false;
            }
            finally
            {
                 dt.Reset();
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }
        
        return isExcel;

    }

    private DataTable importVenderOrQADToTemp(int uID, DataTable TempTable,bool isOut)
    {
        StringWriter writer = new StringWriter();
        TempTable.WriteXml(writer);
        string xmlDetail = writer.ToString();

        string sqlstr = "sp_pcm_checkimportVenderOrQADToTemp";
        SqlParameter[] param = new SqlParameter[]{
        new SqlParameter("@gvTable",xmlDetail)
        ,new SqlParameter("@uID",uID)
        ,new SqlParameter("@isOut",isOut)
    
        };
        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param).Tables[0];
    }


    private bool JudgmentImportReturn(DataTable dt, out string message)
    {
        bool isExcel = false;
        if (dt.Rows.Count == 0)
        {
            message = "您填写的数据无法查到相关数据或者是相关数据全在流程中，请查清后再导入！";
            isExcel = false;
        }
        else if (dt.Rows[0][0].ToString().Equals("0"))
        {
            message = "导入文件失败，请联系管理员";
            isExcel = false;
        }
        else if (dt.Columns.Count == 22)//没有error
        {

            string stroutFile = "ModifyApplyDetImport" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
            string strFile = stroutFile;
            string filePathex = System.Web.HttpContext.Current.Server.MapPath("../Excel/" + strFile);
            MemoryStream ms = null;
            FileStream fs = null;
            try
            {
                ms = RenderDataTableToExcelOutImportError(dt, filePathex) as MemoryStream;
                
            }
            catch (Exception e)
            {
                message = e.Message + "错误A";
                isExcel = false;
            }
            try
            {
                fs = new FileStream(filePathex, FileMode.Create, FileAccess.Write);
            }
            catch (Exception e)
            {
                message = e.Message + "错误B";
                isExcel = false;
            }
            byte[] data = ms.ToArray();
            fs.Write(data, 0, data.Length);
            fs.Flush();
            fs.Close();
            data = null;
            ms = null;
            fs = null;

           
           
         
            message = stroutFile;
            isExcel = true;
        }
        else if (dt.Columns.Count == 23)//有问题的情况
        {
            //输出excel文件
            string stroutFile = "ModifyApplyDetImportError" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
            string strFile = stroutFile;
            string filePathex = System.Web.HttpContext.Current.Server.MapPath("../Excel/" + strFile);
            MemoryStream ms = RenderDataTableToExcelOutImportError(dt, filePathex) as MemoryStream;
            FileStream fs = new FileStream(filePathex, FileMode.Create, FileAccess.Write);
            byte[] data = ms.ToArray();
            fs.Write(data, 0, data.Length);
            fs.Flush();
            fs.Close();
            data = null;
            ms = null;
            fs = null;
            message = stroutFile;
            isExcel = true;

        }
        else
        {
            message = "";
            isExcel = false;
        }
        
        return isExcel;
    
    }


    private int checkApplyImportOrder(int uID, DataTable TempTable)
    {
        StringWriter writer = new StringWriter();
        TempTable.WriteXml(writer);
        string xmlDetail = writer.ToString();

        string sqlstr = "sp_pcm_selectApplyImportOrder";
        SqlParameter[] param = new SqlParameter[]{
        new SqlParameter("@gvTable",xmlDetail)
        ,new SqlParameter("@uID",uID)
    
        };
        return Convert.ToInt32(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param));
    }

    public Stream RenderDataTableToExcelOutImportError(System.Data.DataTable SourceTable, string stroutFile)
    {
        MemoryStream ms = new MemoryStream();
        FileStream file = null;
        if (SourceTable.Columns.Count == 22)
        {
             file = new System.IO.FileStream(System.Web.HttpContext.Current.Server.MapPath("../docs/ModifyApplyDetImport.xls"), FileMode.Open, FileAccess.Read);
        }
        else
        {
             file = new System.IO.FileStream(System.Web.HttpContext.Current.Server.MapPath("../docs/ModifyApplyDetImportError.xls"), FileMode.Open, FileAccess.Read);
        }
        NPOI.HSSF.UserModel.HSSFWorkbook hssfworkbook = new NPOI.HSSF.UserModel.HSSFWorkbook(file);
        NPOI.SS.UserModel.ISheet sheet = hssfworkbook.GetSheet("Sheet1");//workbook.CreateSheet();
        NPOI.SS.UserModel.IRow headerRow = sheet.GetRow(80);//sheet.GetRow(0);
        file.Close();



        //输出明细


        //设置10列宽为100
        sheet.SetColumnWidth(23, 250);
        //sheet.SetColumnWidth(12, 100);
        //加标题
        //sheet.GetRow(0).GetCell(0).SetCellValue("QAD");
        //sheet.GetRow(0).GetCell(1).SetCellValue("供应商号");
        //sheet.GetRow(0).GetCell(2).SetCellValue("需求规格");
        //sheet.GetRow(0).GetCell(3).SetCellValue("单位");
        //sheet.GetRow(0).GetCell(4).SetCellValue("币种");
        //sheet.GetRow(0).GetCell(5).SetCellValue("当前价格");
        //sheet.GetRow(0).GetCell(6).SetCellValue("当前价格起始时间");
        //sheet.GetRow(0).GetCell(7).SetCellValue("当前价格终止时间");
        //sheet.GetRow(0).GetCell(8).SetCellValue("期望价格");
        //sheet.GetRow(0).GetCell(9).SetCellValue("期望价格起始时间");
        //sheet.GetRow(0).GetCell(10).SetCellValue("期望价格终止时间");
        //sheet.GetRow(0).GetCell(11).SetCellValue("供应商名");
        //sheet.GetRow(0).GetCell(12).SetCellValue("GUID");
        //if (SourceTable.Columns.Count == 15)
        //{
        //    sheet.GetRow(0).GetCell(13).SetCellValue("ERROR");
        //}
        //明细起始行
        int rowIndex = 1;

        //ICellStyle style1 = hssfworkbook.CreateCellStyle();
        //style1.WrapText = true;
        //ICellStyle style2 = hssfworkbook.CreateCellStyle();
        //style2.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
        //style2.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
        //style2.BorderTop = NPOI.SS.UserModel.BorderStyle.Thick;

        //ICellStyle style = hssfworkbook.CreateCellStyle();
        //style.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
        //style.TopBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
        //style.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
        //style.RightBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
        //style.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
        //style.BottomBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
        //style.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
        //style.LeftBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
        int countCell = SourceTable.Columns.Count;

        try
        {
            foreach (DataRow row in SourceTable.Rows)
            {

                NPOI.SS.UserModel.IRow dataRow = sheet.CreateRow(rowIndex);

                int i = 0;
                while (i < countCell)
                {
                    ICell cell = dataRow.CreateCell(i);
                    cell.SetCellValue(row[i].ToString());
                    i++;
                }



                //ICell cell = dataRow.CreateCell(0);
                //cell.SetCellValue(row["QAD"].ToString());
                //cell.CellStyle = style;

                //cell = dataRow.CreateCell(1);
                //cell.SetCellValue(row["vender"].ToString());
                //cell.CellStyle = style;

                //cell = dataRow.CreateCell(2);
                //cell.SetCellValue(row["formate"].ToString());
                //cell.CellStyle = style;

                //cell = dataRow.CreateCell(3);
                //cell.SetCellValue(row["um"].ToString());
                //cell.CellStyle = style;

                //cell = dataRow.CreateCell(4);
                //cell.SetCellValue(row["curr"].ToString());
                //cell.CellStyle = style;
                //cell = dataRow.CreateCell(5);
                //cell.SetCellValue(row["oldPrice"].ToString());
                //cell.CellStyle = style;

                //cell = dataRow.CreateCell(6);
                //cell.SetCellValue(row["oldPriceStarDate"].ToString());
                //cell.CellStyle = style;
                //cell = dataRow.CreateCell(7);
                //cell.SetCellValue(row["oldPriceEndDate"].ToString());
                //cell.CellStyle = style;

                //cell = dataRow.CreateCell(8);
                //cell.SetCellValue(row["applyPrice"].ToString());
                //cell.CellStyle = style;
                //cell = dataRow.CreateCell(9);
                //cell.SetCellValue(row["applyPriceStarDate"].ToString());
                //cell.CellStyle = style;

                //cell = dataRow.CreateCell(10);
                //cell.SetCellValue(row["applyPriceEndDate"].ToString());
                //cell.CellStyle = style;

                //cell = dataRow.CreateCell(11);
                //cell.SetCellValue(row["venderName"].ToString());
                //cell.CellStyle = style;
                //cell = dataRow.CreateCell(12);
                //cell.SetCellValue(row["GUID"].ToString());
                //cell.CellStyle = style;

                //if (SourceTable.Columns.Count == 15)
                //{

                //    cell = dataRow.CreateCell(13);
                //    cell.SetCellValue(row["error"].ToString());
                //    cell.CellStyle = style;
                //}

                rowIndex++;



            }
        }
        catch (Exception ex)
        { 
        
        }

        for (int i = sheet.LastRowNum; i > SourceTable.Rows.Count + 1; i--)
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

    private DataTable GetVenderOrQADInfo(int uID,DataTable dt,bool isOut)
    {
        StringWriter writer = new StringWriter();
        dt.WriteXml(writer);
        string xmlDetail = writer.ToString();

        string sqlstr = "sp_pcm_selectVenderOrQADInfoList";
        SqlParameter[] param = new SqlParameter[]{
        new SqlParameter("@gvTable",xmlDetail)
        ,new SqlParameter("@uID",uID)
        ,new SqlParameter("@isOut",isOut)
    
        };
	//SqlHelper.Timeout = 600;
        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param).Tables[0];
    }

    private bool TransTempTable(int uID, string PQID, string InfoFrom, string type,bool isOut)
    {
        string sqlstr = "sp_pcm_insertApplyImportTempToApplyDet";
        SqlParameter[] param = new SqlParameter[]{
        new SqlParameter("@uID",uID)
        ,new SqlParameter("@PQID",PQID)
         ,new SqlParameter("@InfoFrom",InfoFrom)
         ,new SqlParameter("@type",type)
         ,new SqlParameter("@isOut", isOut)
        };
        return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param));
    }

    private bool JudgeTempTable(int uID)
    {
        string sqlstr = "sp_pcm_selectApplyImportTempByID";
        SqlParameter[] param = new SqlParameter[]{
        new SqlParameter("@uID",uID)

       
        };
        return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param));
    }

    private bool CheckTempTable(int uID, string PQID, string InfoFrom)
    {
        string sqlstr = "sp_pcm_checkApplyImportTempByID";
        SqlParameter[] param = new SqlParameter[]{
        new SqlParameter("@uID",uID)
        ,new SqlParameter("@PQID",PQID)
        ,new SqlParameter("@InfoFrom",InfoFrom)
        };
        return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param));
    }

    private bool ClearTempTable(int uID)
    {
        string sqlstr = "sp_pcm_deleteApplyImportTempByID";
        SqlParameter[] param = new SqlParameter[]{
        new SqlParameter("@uID",uID)
        };
        return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param));
    }

    public DataTable GetApplyImportError(string strUID)
    {
        string sqlstr = "sp_pcm_selectApplyImportTempError";
        SqlParameter[] param = new SqlParameter[]{
        new SqlParameter("@uID",strUID)
        };

        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param).Tables[0];
    }
   



    public bool updateReject(string PQID, int uID, string rejectReason, string DetIDList)
    {
        string sqlstr = "sp_pcm_updateReject";
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
        string sqlstr = "sp_pcm_selectApplyPQInfo";
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

    public DataTable generationInquiry(string IMID, out string company, out string createdDate, out string createdByName, int uID)
    {
        string sqlstr = "sp_pcm_generationInquiry";
        SqlParameter[] param = new SqlParameter[]{
        new SqlParameter("@IMID",IMID)
        ,new SqlParameter("@company",SqlDbType.NVarChar,100)
        ,new SqlParameter("@createdDate" ,SqlDbType.VarChar,20)
        ,new SqlParameter("@createdByName" ,SqlDbType.NVarChar,24)
        , new SqlParameter("@uID",uID)
     
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
    public void createinquiry(string stroutFile, string IMID, string company, string vendor, string createdDate, string createByName, int uid)
    {
        System.Data.DataTable dt = generationInquiry(IMID, out company, out createdDate, out createByName, uid);
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
        FileStream file = new System.IO.FileStream(System.Web.HttpContext.Current.Server.MapPath("../docs/pcm_excelinquiry.xls"), FileMode.Open, FileAccess.Read);
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
        
        
        //设置10列宽为100
        sheet.SetColumnWidth(10, 250);
        //sheet.SetColumnWidth(12, 100);
        //加标题
        sheet.GetRow(11).GetCell(1).SetCellValue("QAD");
        sheet.GetRow(11).GetCell(2).SetCellValue("部件号");
        sheet.GetRow(11).GetCell(3).SetCellValue("单位");
        sheet.GetRow(11).GetCell(4).SetCellValue("需求规格");
        sheet.GetRow(11).GetCell(5).SetCellValue("详细描述");
        //sheet.GetRow(11).GetCell(6).SetCellValue("描述1");
        //sheet.GetRow(11).GetCell(7).SetCellValue("描述2");
        sheet.GetRow(11).GetCell(6).SetCellValue("原价格");
        sheet.GetRow(11).GetCell(7).SetCellValue("期望修改价");
        sheet.GetRow(11).GetCell(8).SetCellValue("报价");
        sheet.GetRow(11).GetCell(9).SetCellValue("币种");
        
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
            cell.SetCellValue(row["oldPrice"].ToString());
            cell.CellStyle = style;
            cell = dataRow.CreateCell(7);
            cell.SetCellValue(row["applyPrice"].ToString());
            cell.CellStyle = style;

            cell = dataRow.CreateCell(8);
            cell.CellStyle = style;
            cell = dataRow.CreateCell(9);
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

    public DataTable selectPCMstr(string QAD, string vender, string venderName, string domain)
    {

        string sqlstr = "sp_pcm_selectPCMstr";
        SqlParameter[] param = new SqlParameter[]{
        new SqlParameter("@vender",vender )
        ,new SqlParameter("@venderName" ,venderName)
        ,new SqlParameter("@QAD" ,QAD)
        ,new SqlParameter("@domain" ,domain)
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
    public bool uploadPrice(string strFileName, string strUID, string IMID, out string message,DataTable dt)
    {
        message = "";
        bool success = true;
            try
            {
                #region excel创建datatable
                if (
                    dt.Columns[0].ColumnName != "询价单号" ||
                    dt.Columns[1].ColumnName != "申请id" ||
                    dt.Columns[2].ColumnName != "QAD" ||
                    dt.Columns[3].ColumnName != "供应商" ||
                    dt.Columns[4].ColumnName != "期望价格" ||
                    dt.Columns[5].ColumnName != "折扣方式" ||
                    dt.Columns[6].ColumnName != "核价" ||
                    dt.Columns[7].ColumnName != "核价备注" 
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
                TempColumn.ColumnName = "IMID";
                TempTable.Columns.Add(TempColumn);

                TempColumn = new DataColumn();
                TempColumn.DataType = System.Type.GetType("System.String");
                TempColumn.ColumnName = "DetID";
                TempTable.Columns.Add(TempColumn);

                TempColumn = new DataColumn();
                TempColumn.DataType = System.Type.GetType("System.String");
                TempColumn.ColumnName = "QAD";
                TempTable.Columns.Add(TempColumn);

                TempColumn = new DataColumn();
                TempColumn.DataType = System.Type.GetType("System.String");
                TempColumn.ColumnName = "Vender";
                TempTable.Columns.Add(TempColumn);

                TempColumn = new DataColumn();
                TempColumn.DataType = System.Type.GetType("System.String");
                TempColumn.ColumnName = "CheckPriceBasis";
                TempTable.Columns.Add(TempColumn);

                TempColumn = new DataColumn();
                TempColumn.DataType = System.Type.GetType("System.String");
                TempColumn.ColumnName = "priceDiscount";
                TempTable.Columns.Add(TempColumn);

                TempColumn = new DataColumn();
                TempColumn.DataType = System.Type.GetType("System.String");
                TempColumn.ColumnName = "checkPrice";
                TempTable.Columns.Add(TempColumn);

                TempColumn = new DataColumn();
                TempColumn.DataType = System.Type.GetType("System.String");
                TempColumn.ColumnName = "uID";
                TempTable.Columns.Add(TempColumn);
                #endregion
               
                if (dt.Rows.Count > 0)
                {

                    //先清空临时表中该上传员工的记录
                    if (ClearInquiryTempTable(Convert.ToInt32(strUID)))
                    {
                        #region 前台导入数据
                        for (int i = 0; i <= dt.Rows.Count - 1; i++)
                        {

                            TempRow = TempTable.NewRow();//创建新的行
                            if (dt.Rows[i].IsNull(0) && dt.Rows[i].IsNull(1) && dt.Rows[i].IsNull(2) && dt.Rows[i].IsNull(3) && dt.Rows[i].IsNull(4) && dt.Rows[i].IsNull(5) && dt.Rows[i].IsNull(6) && dt.Rows[i].IsNull(7))
                            {
                                continue;
                            }

                            TempRow["IMID"]  = dt.Rows[i].ItemArray[0].ToString().Trim();
                            TempRow["DetID"] = dt.Rows[i].ItemArray[1].ToString().Trim();
                            TempRow["QAD"]  = dt.Rows[i].ItemArray[2].ToString().Trim();
                            TempRow["Vender"] = dt.Rows[i].ItemArray[3].ToString().Trim();
                            TempRow["CheckPriceBasis"]  = dt.Rows[i].ItemArray[7].ToString().Trim();
                            TempRow["priceDiscount"]  = dt.Rows[i].ItemArray[5].ToString().Trim();
                            TempRow["checkPrice"]  = dt.Rows[i].ItemArray[6].ToString().Trim();
                            TempRow["uID"] = strUID;
                                 
                            TempTable.Rows.Add(TempRow);
                        }
                        #endregion
                        //TempTable有数据的情况下批量复制到数据库里
                        if (TempTable != null && TempTable.Rows.Count > 0)
                        {
                            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(adam.dsn0(), SqlBulkCopyOptions.UseInternalTransaction))
                            {
                                bulkCopy.DestinationTableName = "PCM_inquiryImportTemp";

                                bulkCopy.ColumnMappings.Clear();

                                bulkCopy.ColumnMappings.Add("IMID", "IMID");
                                bulkCopy.ColumnMappings.Add("DetID", "DetID");
                                bulkCopy.ColumnMappings.Add("QAD", "QAD");
                                bulkCopy.ColumnMappings.Add("Vender", "Vender");
                                bulkCopy.ColumnMappings.Add("CheckPriceBasis", "CheckPriceBasis");
                                bulkCopy.ColumnMappings.Add("priceDiscount", "priceDiscount");
                                bulkCopy.ColumnMappings.Add("checkPrice", "checkPrice");
                                bulkCopy.ColumnMappings.Add("uID", "createdBy");
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
                            if (CheckInquiryTempTable(Convert.ToInt32(strUID),IMID))
                            {
                                //判断上传内容能否通过验证
                                if (JudgeInquiryTempTable(Convert.ToInt32(strUID)))
                                {
                                    int flag = (TransInquiryTempTable(Convert.ToInt32(strUID), IMID));
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
        
        return success;

    }
    /// <summary>
    /// 转插入
    /// </summary>
    /// <param name="uID"></param>
    /// <param name="IMID"></param>
    /// <param name="status"></param>
    /// <returns></returns>
    private int TransInquiryTempTable(int uID, string IMID)
    {
        string sqlstr = "sp_pcm_insertInquiryImportTempToDet";
        SqlParameter[] param = new SqlParameter[]{
        new SqlParameter("@uID",uID)
        ,new SqlParameter("@IMID",IMID)
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
        string sqlstr = "sp_pcm_deleteInquiryImportTempByID";
        SqlParameter[] param = new SqlParameter[]{
        new SqlParameter("@uID",uID)
       
        };
        return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param));
    }

    private bool JudgeInquiryTempTable(int uID)
    {
        string sqlstr = "sp_pcm_selectInquiryImportTempByID";
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
    private bool CheckInquiryTempTable(int uID, string IMID)
    {
        string sqlstr = "sp_pcm_checkInquiryImportTempByID";
        SqlParameter[] param = new SqlParameter[]{
        new SqlParameter("@uID",uID)
        ,new SqlParameter("@IMID",IMID)
        };
        return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param));
    }

    public DataTable GetInquiryPriceImportError(string strUID)
    {
        string sqlstr = "sp_pcm_selectInquiryPriceImportError";
        SqlParameter[] param = new SqlParameter[]{
        new SqlParameter("@uID",strUID)
        };

        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param).Tables[0];
    }

    #endregion




    public bool updatePassApply(string PQID, int uID)
    {
        string sqlstr = "sp_pcm_updatePassApply";

        SqlParameter[] param = new SqlParameter[]{
        new SqlParameter("@PQID",PQID)
        ,new SqlParameter("@uID",uID)

        };

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param));
    }



    public DataTable selectApplyQADStatus(int uID, string QAD, string vender, string venderName, string status)
    {
        string sqlstr = "sp_pcm_selectApplyQADStatus";
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
        string sqlstr = "sp_pcm_selectApplyPerson";

        return (SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, sqlstr).Tables[0]);
    }



    public bool addApplyPerson(string addUserID, int uID)
    {
        string sqlstr = "sp_pcm_addApplyPerson";

        SqlParameter[] param = new SqlParameter[]{
        new SqlParameter("@addUserID",addUserID)
        ,new SqlParameter("@uID",uID)

        };

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param));
    }

    public bool deleteApplyPerson(int userID)
    {
        string sqlstr = "sp_pcm_deleteApplyPerson";

        SqlParameter[] param = new SqlParameter[]{
        new SqlParameter("@userID",userID)
     

        };

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param));
    }

    public bool cancelInquiryDet(string applyDetId, int uID)
    {
        string sqlstr = "sp_pcm_cancelInquiryDet";

        SqlParameter[] param = new SqlParameter[]{
        new SqlParameter("@applyDetId",applyDetId)
        ,new SqlParameter("@uID",uID)
        };

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param));
    }

    //public DataTable select100AndQADPCMSTR(string QAD, string vender, string venderName, int isCheck)
    //{
    //    string sqlstr = "sp_pcm_select100AndQADPCMSTR";
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
        string sqlstr = "sp_pcm_sendMailForReject";
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
        string sqlstr = "sp_pcm_selectApplyDetExport";
        SqlParameter[] param = new SqlParameter[]{
        new SqlParameter("@PQID",PQID)
        };

        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param).Tables[0];
    }

    public object select100AndQADPCMSTR(string QAD, string vender, string venderName, int check, string domain,int diff)
    {
        string sqlstr = "sp_pcm_selectPcMstrCheck";
        SqlParameter[] param = new SqlParameter[]{
        new SqlParameter("@vender",vender)
        ,new SqlParameter("@QAD",QAD)
        ,new SqlParameter("@venderName",venderName)
        ,new SqlParameter("@domain",domain)
        ,new SqlParameter("@check",check)
          ,new SqlParameter("@diff",diff)
        };

        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param).Tables[0];
    }

    public bool checkPricePass(string id)
    {
        string sqlstr = "sp_pcm_checkPricePass";

        SqlParameter[] param = new SqlParameter[]{
        new SqlParameter("@id",id)
        };
        return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param));
    }

    public int updateDesc(string detid,string PQID, int uID)
    {
        string sqlstr = "sp_pcm_updateDesc";
        SqlParameter[] param = new SqlParameter[]{
        new SqlParameter("@detid",detid)
        ,new SqlParameter("@PQID",PQID)
        ,new SqlParameter("@uID",uID)
        ,new SqlParameter("@GUID",getGUID())
        };
        return Convert.ToInt32(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param));
    }

    public DataTable selectOldVenderList(string qad ,string PQID)
    {
        string sqlstr = "sp_pcm_selectOldVenderList";
        SqlParameter[] param = new SqlParameter[]{
        new SqlParameter("@QAD",qad),
        new SqlParameter("@PQID",PQID)
        };

        DataTable dt=   SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param).Tables[0];

        return dt;
    }

    public bool addVenderMore(DataTable dt, int uID, string PQID, string QAD,string formate)
    {
        StringWriter writer = new StringWriter();
        dt.WriteXml(writer);
        string xmlDetail = writer.ToString();

        string sqlstr = "sp_pcm_addVenderMore";
        SqlParameter[] param = new SqlParameter[]{
        new SqlParameter("@gvTable",xmlDetail)
        ,new SqlParameter("@uID",uID)
        ,new SqlParameter("@PQID",PQID)
        ,new SqlParameter("@QAD",QAD)
        ,new SqlParameter("@formate",formate)
        };
        return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param));

    }


    public string selectFormateByQAD(string qad)
    {
        string sqlstr = "sp_pcm_selectFormateByQAD";
        SqlParameter[] param = new SqlParameter[]{
        new SqlParameter("@QAD",qad)

        };
        return SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param).ToString();
    }



    /// <summary>
    /// 通过QAD和供应商编号查找所有价格历史的方法，目前没有任何使用到，估计要在一定的时候会使用到，现在予以保留
    /// </summary>
    /// <param name="QAD"></param>
    /// <param name="vender"></param>
    /// <returns></returns>
    public object selectHistoryPriceByQADAndVender(string QAD, string vender)
    {
        string sqlstr = "sp_pcm_selectHistoryPriceByQADAndVender";
        SqlParameter[] param = new SqlParameter[]{
        new SqlParameter("@QAD",QAD)
        ,new SqlParameter("@vender",vender)
        };
        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param).Tables[0];
    }
    
    /// <summary>
    /// 通过询价单号
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
    public DataTable selectInquiryToExport(string IMID)
    {
        try
        {
            string sqlstr = "sp_pcm_selectInquiryToExport";
            SqlParameter[] param = new SqlParameter[]{
                new SqlParameter("@IMID",IMID)
                };
            return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param).Tables[0];

        }
        catch
        {

            return null;
        }
    }

    public DataTable viewBasis(string IMID,out string flag )
    {
        string sqlstr = "sp_pcm_selectBasisByIMID";

        SqlParameter[] param = new SqlParameter[]{
        
             new SqlParameter("@IMID",IMID)
            ,new SqlParameter("@flag",SqlDbType.NVarChar,20)

        };

        param[1].Direction = ParameterDirection.Output;

        DataTable dt = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param).Tables[0];

        flag = param[1].ToString();

        return dt;
    }

    public DataTable selectApplyForPriceUp(string qad, string vender, string venderName)
    {
        string sqlstr = "sp_pcm_selectApplyForPriceUp";


        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@qad",qad)
            , new SqlParameter("@vender",vender)
            , new SqlParameter("@venderName",venderName)
        
        };

        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param).Tables[0];

    }

    public int confirmPrice(DataTable TempTable, string uID, string uName)
    {
        StringWriter writer = new StringWriter();
        TempTable.WriteXml(writer);
        string xmlDetail = writer.ToString();

        string sqlstr = "sp_pcm_confirmPrice";

        SqlParameter[] param = new SqlParameter[]{
                  new SqlParameter("@gvTable", xmlDetail)
                  ,new SqlParameter("@uID",uID)
                  ,new SqlParameter("@uName",uName)
            };

        return Convert.ToInt32( SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param));
    }

    public int updateRejectFromSupplier(DataTable TempTable, string uID, string uName, string Reason)
    {

        StringWriter writer = new StringWriter();
        TempTable.WriteXml(writer);
        string xmlDetail = writer.ToString();

        string sqlstr = "sp_pcm_updateRejectFromSupplier";

        SqlParameter[] param = new SqlParameter[]{
                  new SqlParameter("@gvTable", xmlDetail)
                  ,new SqlParameter("@uID",uID)
                  ,new SqlParameter("@uName",uName)
                  ,new SqlParameter("@reason",Reason)
            };

        return Convert.ToInt32(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param));
    }
}


    
