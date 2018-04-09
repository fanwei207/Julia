using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Mold
/// </summary>
public class Mold
{
    private String strConn = ConfigurationSettings.AppSettings["SqlConn.qadplan"];
    
    private string _vendCode;
    public string vendCode
    {
        get;
        set;
    }

    private string _vendName;
    public string vendName
    {
        get;
        set;
    }

    private string _moldCode;
    public string moldCode
    {
        get;
        set;
    }

    private int _moldQty;
    public int moldQty
    {
        get;
        set;
    }

    private string _Status;
    public string Status
    {
        get;
        set;
    }

    private int _intStatus;
    public int intStatus
    {
        get;
        set;
    }

    private string _QAD;
    public string QAD
    {
        get;
        set;
    }

    private string _Cavity;
    public string Cavity
    {
        get;
        set;
    }

    private string _drawingID;
    public string drawingID
    {
        get;
        set;
    }

    private string _Remark;
    public string Remark
    {
        get;
        set;
    }

    private string _createBy;
    public string createBy
    {
        get;
        set;
    }

    private string _createDate;
    public string createDate
    {
        get;
        set;
    }

    private int _ID;
    public int ID
    {
        get;
        set;
    }
	public Mold()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public DataSet BindVend()
    {
        string sql = "sp_vc_selectvender";
        return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, sql);
    }

    public bool AddVendMold(string vendName, string vendCode, string moldID, int Qty, int status, string QAD, string Cavity,string drawID,string remark,int createBy)
    {
        string sql = "sp_vm_insertMold";
        SqlParameter[] param = new SqlParameter[11];
        param[0] = new SqlParameter("@vm_vendName", vendName);
        param[1] = new SqlParameter("@vm_vendCode", vendCode);
        param[2] = new SqlParameter("@vm_moldCode", moldID);
        param[3] = new SqlParameter("@vm_moldQty", Qty);
        param[4] = new SqlParameter("@vm_status", status);
        param[5] = new SqlParameter("@vm_QAD", QAD);
        param[6] = new SqlParameter("@vm_Cavity", Cavity);
        param[7] = new SqlParameter("@vm_drawingID", drawID);
        param[8] = new SqlParameter("@vm_remark", remark);
        param[9] = new SqlParameter("@vm_createBy",createBy);
        
        param[10] = new SqlParameter("@reValue", SqlDbType.Int);
        param[10].Direction = ParameterDirection.Output;

        SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, sql, param);

        if (Convert.ToInt32(param[10].Value) == 1) return true;
        else return false;
    }
    public bool ModifyVendMold(string vendName, string vendCode, string moldID, int Qty, int status, string QAD, string Cavity, string drawID, string remark, int createBy,int vm_id)
    {
        string sql = "sp_vm_ModifyMold";
        SqlParameter[] param = new SqlParameter[12];
        param[0] = new SqlParameter("@vm_vendName", vendName);
        param[1] = new SqlParameter("@vm_vendCode", vendCode);
        param[2] = new SqlParameter("@vm_moldCode", moldID);
        param[3] = new SqlParameter("@vm_moldQty", Qty);
        param[4] = new SqlParameter("@vm_status", status);
        param[5] = new SqlParameter("@vm_QAD", QAD);
        param[6] = new SqlParameter("@vm_Cavity", Cavity);
        param[7] = new SqlParameter("@vm_drawingID", drawID);
        param[8] = new SqlParameter("@vm_remark", remark);
        param[9] = new SqlParameter("@vm_createBy", createBy);
        param[10] = new SqlParameter("@vm_id", vm_id);
        param[11] = new SqlParameter("@reValue", SqlDbType.Int);
        param[11].Direction = ParameterDirection.Output;

        SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, sql, param);

        if (Convert.ToInt32(param[11].Value) == 1) return true;
        else return false;
    }
    public IList<Mold> SelectMold(string vendCode,string moldCode,string QAD,int status)
    {
        string sql = "sp_vm_SelectMold";
        SqlParameter[] param = new SqlParameter[4];
        param[0] = new SqlParameter("@vm_vendCode", vendCode);
        param[1] = new SqlParameter("@vm_moldCode", moldCode);
        param[2] = new SqlParameter("@vm_QAD", QAD);
        param[3] = new SqlParameter("@vm_status", status);

        IList<Mold> Molds = new List<Mold>();

        IDataReader reader = SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, sql, param);

        while (reader.Read())
        {
            Mold item = new Mold();

            item.ID = Convert.ToInt32(reader["vm_id"]);
            item.vendCode = reader["vm_vendCode"].ToString();
            item.vendName = reader["vm_vendName"].ToString();
            item.moldCode = reader["vm_moldCode"].ToString();
            item.moldQty = Convert.ToInt32(reader["vm_moldQty"]);

            if (Convert.ToInt32(reader["vm_status"]) == 1) item.Status = "可用";
            else item.Status = "不可用";
            item.intStatus = Convert.ToInt32(reader["vm_status"]);
            item.QAD = reader["vm_QAD"].ToString();
            item.Cavity = reader["vm_Cavity"].ToString();
            item.drawingID = reader["vm_drawingID"].ToString();
            item.Remark = reader["vm_remark"].ToString();

            Molds.Add(item);
        }
        reader.Close();
        return Molds;
    }

    public Mold SelectMoldByID(string vm_id)
    {
        Mold mold = new Mold();
        string sql = "sp_vm_SelectMoldByID";
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@vm_id", vm_id);

        IDataReader reader = SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, sql, param);

        while (reader.Read())
        {
            mold.ID = Convert.ToInt32(reader["vm_id"]);
            mold.vendCode = reader["vm_vendCode"].ToString();
            mold.vendName = reader["vm_vendName"].ToString();
            mold.moldCode = reader["vm_moldCode"].ToString();
            mold.moldQty = Convert.ToInt32(reader["vm_moldQty"]);

            if (Convert.ToInt32(reader["vm_status"]) == 1) mold.Status = "可用";
            else mold.Status = "不可用";
            mold.intStatus = Convert.ToInt32(reader["vm_status"]);
            mold.QAD = reader["vm_QAD"].ToString();
            mold.Cavity = reader["vm_Cavity"].ToString();
            mold.drawingID = reader["vm_drawingID"].ToString();
            mold.Remark = reader["vm_remark"].ToString();
        }
        reader.Close();

        return mold;
    }
    public bool DeleteMold(int vm_id,int HistBy)
    {
        string sql = "sp_vm_deletemold";
        SqlParameter[] param = new SqlParameter[4];
        param[0] = new SqlParameter("@vm_id", vm_id);
        param[1] = new SqlParameter("@reValue", SqlDbType.Bit);
        param[1].Direction = ParameterDirection.Output;
        param[2] = new SqlParameter("@HistBy", HistBy);
        param[3] = new SqlParameter("@HistDate", DateTime.Now.ToString());

        SqlHelper.ExecuteNonQuery(strConn,CommandType.StoredProcedure,sql,param);

        return Convert.ToBoolean(param[1].Value);
    }

    public bool chkVendCode(string code, string name)
    {
        string sql = "sp_vm_chkVendCode";
        SqlParameter[] param = new SqlParameter[12];
        param[0] = new SqlParameter("@vm_vendName", name);
        param[1] = new SqlParameter("@vm_vendCode", code);

        if (SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, sql, param) == null) return false;
        else return true;
    }
}