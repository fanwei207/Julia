using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using Microsoft.ApplicationBlocks.Data;
using CommClass;

/// <summary>
/// Summary description for DocTransfer
/// </summary>
namespace DocTransfer
{
    /// <summary>
    /// 转移前的文档信息
    /// </summary>
	public class BfDoc
	{
        /// <summary>
        /// 文档ID
        /// </summary>
        private int _id;
        public int Id
        {
            get
            {
                return this._id;
            }
            set
            {
                this._id = value;
            }
        }
        
        private string _nbr;
        /// <summary>
        /// 单号
        /// </summary>
        public string Nbr
        {
            get 
            { 
                return this._nbr; 
            }
            set
            {
                this._nbr = value;
            }
        }
        /// <summary>
        /// 行号
        /// </summary>
        private int _line;
        public int Line
        {
            get
            {
                return this._line;
            }
            set
            {
                this._line = value;
            }
        }
        private string _code;
        /// <summary>
        /// 部件号
        /// </summary>
        public string Code
        {
            get
            {
                return this._code;
            }
            set
            {
                this._code = value;
            }
        }

        private string _qad;
        /// <summary>
        /// QAD号
        /// </summary>
        public string QAD
        {
            get
            {
                return this._qad;
            }
            set
            {
                this._qad = value;
            }
        }
        private string _docDiskName;
        /// <summary>
        /// 文档的磁盘存储名
        /// </summary>
        public string DocDiskName
        {
            get
            {
                return this._docDiskName;
            }
            set
            {
                this._docDiskName = value;
            }
        }
       
        private string _vend;
        /// <summary>
        /// 供应商代码
        /// </summary>
        public string Vend
        {
            get
            {
                return this._vend;
            }
            set
            {
                this._vend = value;
            }
        }
       
        private string _vendName;
        /// <summary>
        /// 供应商名称
        /// </summary>
        public string VendName
        {
            get
            {
                return this._vendName;
            }
            set
            {
                this._vendName = value;
            }
        }
       
        private string _docName;
        /// <summary>
        /// 文档名
        /// </summary>
        public string DocName
        {
            get
            {
                return this._docName;
            }
            set
            {
                this._docName = value;
            }
        }
        
        private string _fileName;
        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName
        {
            get
            {
                return this._fileName;
            }
            set
            {
                this._fileName = value;
            }
        }
       
        private string _fileType;
        /// <summary>
        /// 文件类型
        /// </summary>
        public string FileType
        {
            get
            {
                return this._fileType;
            }
            set
            {
                this._fileType = value;
            }
        }
        
        
        private string _fileDesc;
        /// <summary>
        /// 文件描述
        /// </summary>
        public string FileDesc
        {
            get
            {
                return this._fileDesc;
            }
            set
            {
                this._fileDesc = value;
            }
        }
     
        private string _status;
        /// <summary>
        /// 转移状态--0未转移---1已转移---2全部
        /// </summary>
        public string Status
        {
            get
            {
                return this._status;
            }
            set
            {
                this._status = value;
            }
        }

        
        private int _transferBy;
        /// <summary>
        /// 转移人ID
        /// </summary>
        public int Transfer
        {
            get
            {
                return this._transferBy;
            }
            set
            {
                this._transferBy = value;
            }
        }
       
        private string _transferName;
        /// <summary>
        /// 转移人姓名
        /// </summary>
        public string TransferName
        {
            get
            {
                return this._transferName;
            }
            set
            {
                this._transferName = value;
            }
        }
       
        private string _transferDate;
        /// <summary>
        /// 转移日期
        /// </summary>
        public string TransferDate
        {
            get
            {
                return this._transferDate;
            }
            set
            {
                this._transferDate = value;
            }
        }
	}
    /// <summary>
    /// BfDoc操作类
    /// </summary>
    public class BfDocHelper
    {
        private static string Conn = admClass.getConnectString("SqlConn.TCPC_Supplier");

        /// <summary>
        /// 获取打样单文档的详细信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static BfDoc SelectBosDocInfo(string  id, string source)
        {
            BfDoc item = new BfDoc();
            try
            {
                string strSql = "sp_bos_SelectBosDocInfo";
                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter("@id", id);
                sqlParam[1] = new SqlParameter("@source", source);
                IDataReader reader = SqlHelper.ExecuteReader(Conn, CommandType.StoredProcedure, strSql, sqlParam);
                while (reader.Read())
                {
                    item.Id = Convert.ToInt32(reader["id"]);
                    item.Nbr = reader["nbr"].ToString();
                    item.Line = Convert.ToInt32(reader["line"]);
                    item.Code = reader["bos_det_Code"].ToString();
                    item.FileDesc = reader["bos_docfileDescs"].ToString();
                    item.QAD = reader["bos_det_Qad"].ToString();
                    item.DocDiskName = reader["bos_docId"].ToString();
                    item.Vend = reader["vend"].ToString();
                    item.VendName = reader["vendName"].ToString();
                    item.FileName = reader["bos_docfileName"].ToString();
                    int a = item.FileName.LastIndexOf(".");
                    int b = item.FileName.Length;
                    item.DocName = item.FileName.Substring(0,a);
                    item.FileType = item.FileName.Substring(a,b-a);
                    item.Status = reader["bos_transferStatus"].ToString();
                    item.TransferName = reader["bos_transferName"].ToString();
                    item.TransferDate = reader["bos_transferDate"].ToString();
      
                }

                reader.Close();
            }
            catch(Exception ex)
            { 
            
            }
            return item;
        
        }


        public static IList<BfDoc> SelectBosDocList(BfDoc bf)
        { 
            IList<BfDoc> list = new List<BfDoc>();
            try
            {
                string strSql = "sp_bos_SelectBosDocAf";
                SqlParameter[] sqlParam = new SqlParameter[3];
                sqlParam[0] = new SqlParameter("@nbr", bf.Nbr);
                sqlParam[1] = new SqlParameter("@vend", bf.Vend);
                sqlParam[2] = new SqlParameter("@status", bf.Status);
                IDataReader reader = SqlHelper.ExecuteReader(Conn, CommandType.StoredProcedure, strSql, sqlParam);
                while (reader.Read())
                {
                    BfDoc item = new BfDoc();
                    item.Id = Convert.ToInt32(reader["id"]);
                    item.Nbr = reader["nbr"].ToString();
                    item.Line = Convert.ToInt32(reader["line"]);
                    item.Vend = reader["vend"].ToString();
                    item.VendName = reader["vendName"].ToString();
                    item.FileName = reader["bos_docfileName"].ToString();
                    item.Status = reader["bos_transferStatus"].ToString();
                    item.TransferName = reader["bos_transferName"].ToString();
                    item.TransferDate = reader["bos_transferDate"].ToString();
                    list.Add(item);

                }

                reader.Close();
            }
            catch(Exception ex)
            { 
                
            }

            return list;
        }
    
    }

    /// <summary>
    /// 转移后的文档信息
    /// </summary>
    public class AfDoc : BfDoc
    {
       
        private int _typeID;
        /// <summary>
        /// TypeID
        /// </summary>
        public int TypeID
        {
            get
            {
                return this._typeID;
            }
            set
            {
                this._typeID = value;
            }
        }
       
        private string _typeName;
        /// <summary>
        /// TypeName
        /// </summary>
        public string TypeName
        {
            get
            {
                return this._typeName;
            }
            set
            {
                this._typeName = value;
            }
        }

        
        private int _categoryID;
        /// <summary>
        /// CategoryID
        /// </summary>
        public int CategoryID
        {
            get
            {
                return this._categoryID;
            }
            set
            {
                this._categoryID = value;
            }
        }
       
        private string _categoryName;
        /// <summary>
        /// CategoryName
        /// </summary>
        public string CategoryName
        {
            get
            {
                return this._categoryName;
            }
            set
            {
                this._categoryName = value;
            }
        }

       
        private int _docVer;
        /// <summary>
        /// DocVer
        /// </summary>
        public int DocVer
        {
            get
            {
                return this._docVer;
            }
            set
            {
                this._docVer = value;
            }
        }

        
        private int _docLevel;
        /// <summary>
        /// DocLevel
        /// </summary>
        public int DocLevel
        {
            get
            {
                return this._docLevel;
            }
            set
            {
                this._docLevel = value;
            }
        }

        
        private bool _isPublic;
        /// <summary>
        /// 是否公开文档
        /// </summary>
        public bool IsPublic
        {
            get
            {
                return this._isPublic;
            }
            set
            {
                this._isPublic = value;
            }
        }

        private bool _isApprove;
        /// <summary>
        /// 是否需要审批
        /// </summary>
        public bool IsApprove
        {
            get
            {
                return this._isApprove;
            }
            set
            {
                this._isApprove = value;
            }
        }

        private bool _isVend;
        /// <summary>
        /// 是否需要关联供应商
        /// </summary>
        public bool IsVend
        {
            get
            {
                return this._isVend;
            }
            set
            {
                this._isVend = value;
            }
        }

        private bool _isAll;
        /// <summary>
        /// 是否关联所有零件
        /// </summary>
        public bool IsAll
        {
            get
            {
                return this._isAll;
            }
            set
            {
                this._isAll = value;
            }
        }
        
        private int _createdBy;
        /// <summary>
        /// 创建人
        /// </summary>
        public int CreatedBy
        {
            get
            {
                return this._createdBy;
            }
            set
            {
                this._createdBy = value;
            }
        }

        private string _createdName;
        /// <summary>
        /// 创建人姓名
        /// </summary>
        public string CreatedName
        {
            get
            {
                return this._createdName;
            }
            set
            {
                this._createdName = value;
            }
        }
    }
    /// <summary>
    /// AfDoc操作类
    /// </summary>
    public class AfDocHelper
    {
        private static string Conn = admClass.getConnectString("SqlConn.TCPC_Supplier");
        public static DataSet SelectDocType(string uRole, string uID)
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@uRole", uRole);
            param[1] = new SqlParameter("@uID", uID);
            DataSet ds = SqlHelper.ExecuteDataset(Conn, CommandType.StoredProcedure, "sp_bos_selectDcoType", param);
            return ds;
        }

        public static DataSet SelectDocCategory(string typeID)
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@typeID", typeID);
            DataSet ds = SqlHelper.ExecuteDataset(Conn, CommandType.StoredProcedure, "sp_bos_selectDcoCategory", param);
            return ds;
        }

        public static int TransferBosDoc(AfDoc af)
        {
            try
            {
                string strSql = "sp_bos_TransferBosDoc";
                SqlParameter[] sqlParam = new SqlParameter[20];
                sqlParam[0] = new SqlParameter("@isApprove", af.IsApprove);
                sqlParam[1] = new SqlParameter("@isVend", af.IsVend);
                sqlParam[2] = new SqlParameter("@typeID", af.TypeID);
                sqlParam[3] = new SqlParameter("@typeName", af.TypeName);
                sqlParam[4] = new SqlParameter("@categroyID", af.CategoryID);
                sqlParam[5] = new SqlParameter("@categoryName", af.CategoryName);
                sqlParam[6] = new SqlParameter("@docName", af.DocName);
                sqlParam[7] = new SqlParameter("@fileName", af.FileName);
                sqlParam[8] = new SqlParameter("@docVer", af.DocVer);
                sqlParam[9] = new SqlParameter("@docLevel", af.DocLevel);
                sqlParam[10] = new SqlParameter("@isPublic", af.IsPublic);
                sqlParam[11] = new SqlParameter("@docDiskName", af.DocDiskName);
                sqlParam[12] = new SqlParameter("@vend", af.Vend);
                sqlParam[13] = new SqlParameter("@vendName", af.VendName);
                sqlParam[14] = new SqlParameter("@uID", af.CreatedBy);
                sqlParam[15] = new SqlParameter("@uName", af.CreatedName);
                sqlParam[16] = new SqlParameter("@docDesc", af.FileDesc);
                sqlParam[17] = new SqlParameter("@fileType", af.FileType);
                sqlParam[18] = new SqlParameter("@id", af.Id);
                sqlParam[19] = new SqlParameter("@retValue", SqlDbType.Int);
                sqlParam[19].Direction = ParameterDirection.Output;
                SqlHelper.ExecuteNonQuery(Conn, CommandType.StoredProcedure, strSql, sqlParam);
                return Convert.ToInt32(sqlParam[19].Value);
            }
            catch (Exception ex)
            {
                return -5;
            }

        }

        /// <summary>
        /// 获取打样单文档转移后的详细信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static AfDoc SelectBosDocTransferInfo(string id, string source)
        {
            AfDoc item = new AfDoc();
            try
            {
                string strSql = "sp_bos_SelectBosDocAf";
                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter("@id", id);
                sqlParam[1] = new SqlParameter("@source", source);
                IDataReader reader = SqlHelper.ExecuteReader(Conn, CommandType.StoredProcedure, strSql, sqlParam);
                while (reader.Read())
                {
                    item.Id = Convert.ToInt32(reader["id"]);
                    item.Nbr = reader["nbr"].ToString();
                    item.Line = Convert.ToInt32(reader["line"]);
                    item.Code = reader["bos_det_Code"].ToString();
                    item.FileDesc = reader["description"].ToString();
                    item.QAD = reader["bos_det_Qad"].ToString();
                    item.TypeID = Convert.ToInt32(reader["typeid"]);
                    item.CategoryID = Convert.ToInt32(reader["cateid"]);
                    item.DocName = reader["name"].ToString();
                    item.FileName = reader["filename"].ToString();
                    item.DocVer = Convert.ToInt32(reader["version"]);
                    item.DocLevel = Convert.ToInt32(reader["docLevel"]);
                    item.IsApprove = Convert.ToBoolean(reader["isApprove"]);
                    item.IsPublic = Convert.ToBoolean(reader["isPublic"]);
                    item.Vend = reader["documentVend"].ToString();
                    item.VendName = reader["documentVendName"].ToString();
                    item.IsAll = Convert.ToBoolean(reader ["isall"]);
                }

                reader.Close();
            }
            catch (Exception ex)
            {

            }
            return item;

        }
    }
}