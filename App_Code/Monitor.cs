using adamFuncs;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Site
/// </summary>
public class Monitor
{
    adamClass adam = new adamClass();
    //#region 属性
    //private string _MonitID;
    //public string MonitID
    //{
    //    get;
    //    set;
    //}
    //private string _Resolution;
    //public string Resolution
    //{
    //    get;
    //    set;
    //}
    //private int _ID;
    //public int ID
    //{
    //    get;
    //    set;
    //}

    //private string _PlantID;
    //public string PlantID
    //{
    //    get;
    //    set;
    //}

    //private string _PlantName;
    //public int PlantName
    //{
    //    get;
    //    set;
    //}

    //private string _Site;
    //public string Site
    //{
    //    get;
    //    set;
    //}

    //private int _CreateBy;
    //public int CreateBy
    //{
    //    get;
    //    set;
    //}
    //private string _CreateName;
    //public string CreateName
    //{
    //    get;
    //    set;
    //}

    //private DateTime _CreateDate;
    //public DateTime CreateDate
    //{
    //    get;
    //    set;
    //}
    //private string _Remark;
    //public string Remark
    //{
    //    get;
    //    set;
    //}
    //#endregion
    public Monitor()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    //添加摄像头记录
    public bool AddMonitor(string mID,string PlantID,string PalntName,string Resolution,string Beltline,string AreaID,string Area,string Remark,string uID)
    {
        string sql = "sp_Monit_insertMonitor";
        SqlParameter[] param = new SqlParameter[10];
        param[0] = new SqlParameter("@Monit_mID", mID);
        param[1] = new SqlParameter("@PlantID", Convert.ToInt32(PlantID));
        param[2] = new SqlParameter("@Monit_Resolution", Resolution);
        param[3] = new SqlParameter("@Monit_Beltline", Beltline);
        param[4] = new SqlParameter("@Monit_Remark", Remark);
        param[5] = new SqlParameter("@uID", Convert.ToInt32(uID));
        param[6] = new SqlParameter("@reValue", SqlDbType.Int);
        param[6].Direction = ParameterDirection.Output;
        param[7] = new SqlParameter("@PlantName", PalntName);
        param[8] = new SqlParameter("@Monit_AreaID", Convert.ToInt32(AreaID));
        param[9] = new SqlParameter("@Monit_Area", Area);

        SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, sql, param);

        if (Convert.ToInt32(param[6].Value) == 1) return true;
        else return false;
    }

    //修改摄像头记录
    public bool ModifyMonitor(string ID, string mID, string PlantID, string PalntName, string Resolution, string Beltline, string AreaID, string Area, string Remark, string uID)
    {
        string sql = "sp_Monit_modifyMonitor";
        SqlParameter[] param = new SqlParameter[11];
        param[0] = new SqlParameter("@Monit_mID", mID);
        param[1] = new SqlParameter("@PlantID", Convert.ToInt32(PlantID));
        param[2] = new SqlParameter("@Monit_Resolution", Resolution);
        param[3] = new SqlParameter("@Monit_Beltline", Beltline);
        param[4] = new SqlParameter("@Monit_Remark", Remark);
        param[5] = new SqlParameter("@uID", Convert.ToInt32(uID));
        param[6] = new SqlParameter("@reValue", SqlDbType.Int);
        param[6].Direction = ParameterDirection.Output;
        param[7] = new SqlParameter("@PlantName", PalntName);
        param[8] = new SqlParameter("@ID", ID);
        param[9] = new SqlParameter("@Monit_AreaID", Convert.ToInt32(AreaID));
        param[10] = new SqlParameter("@Monit_Area", Area);

        SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, sql, param);

        if (Convert.ToInt32(param[6].Value) == 1) return true;
        else return false;
    }
    //删除摄像头记录
    public bool DeleteMonitor(string mID, string DeleteBy)
    {
        string sql = "sp_Monit_deleteMonitor";
        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@Monit_mID", mID);
        param[1] = new SqlParameter("@reValue", SqlDbType.Int);
        param[1].Direction = ParameterDirection.Output;
        param[2] = new SqlParameter("@DeleteBy", Convert.ToInt32(DeleteBy));

        SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, sql, param);

        if (Convert.ToInt32(param[1].Value) == 1) return true;
        else return false;
    }
    //通过Monit_mID寻找摄像头记录
    public DataTable SelectMonitorByID(string mID)
    {
        string sql = "sp_Monit_SelectMonitorByID";

        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@Monit_mID", mID);

        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, sql, param).Tables[0];
    }
    //绑定Monitor的信息（Gridview）
    public DataSet SelectMonitor(string mID,string PlantID)
    {
        string sql = "sp_Monit_SelectMonitor";

        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@Monit_mID", mID);
        param[1] = new SqlParameter("@PlantID", Convert.ToInt32(PlantID));

        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, sql, param);
    }
    //获得某工厂的摄像头编号
    public DataSet GetMIDByPlantID(string PlantID)
    {
        string sql = "sp_Monit_GetMIDByPlantID";

        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@PlantID", Convert.ToInt32(PlantID));

        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, sql, param);
    }
    //新增监控日志
    public int AddLog(string Plant,string mID,string Content,string ActualDate,string Flag,string Area,string Beltline,string CreateBy)
    {
        string sql = "sp_Monit_InsertLog";

        SqlParameter[] param = new SqlParameter[9];
        param[0] = new SqlParameter("@Monit_mID", mID);
        param[1] = new SqlParameter("@Plant", Plant);
        param[2] = new SqlParameter("@Content", Content);;
        param[3] = new SqlParameter("@CreateBy", Convert.ToInt32(CreateBy));
        param[4] = new SqlParameter("@reValue", SqlDbType.Int);
        param[4].Direction = ParameterDirection.Output;
        param[5] = new SqlParameter("@Monit_ActualDate", Convert.ToDateTime(ActualDate));
        param[6] = new SqlParameter("@Monit_Flag", Convert.ToInt32(Flag));
        param[7] = new SqlParameter("@Monit_Area", Area);
        param[8] = new SqlParameter("@Monit_Beltline", Beltline);

        SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, sql, param);

        return Convert.ToInt32(param[4].Value);
    }
    //修改监控日志
    public bool ModifyLog(string id, string mID, string Plant, string Content, string ActualDate, string Flag, string Area, string Beltline, string uID)
    {
        string sql = "sp_Monit_modifyLog";
        SqlParameter[] param = new SqlParameter[10];
        param[0] = new SqlParameter("@Monit_mID", mID);
        param[1] = new SqlParameter("@reValue", SqlDbType.Int);
        param[1].Direction = ParameterDirection.Output;
        param[2] = new SqlParameter("@uID", Convert.ToInt32(uID));
        param[3] = new SqlParameter("@id", Convert.ToInt32(id));
        param[4] = new SqlParameter("@Plant", Plant);
        param[5] = new SqlParameter("@Monit_Content", Content);
        param[6] = new SqlParameter("@Monit_ActualDate", Convert.ToDateTime(ActualDate));
        param[7] = new SqlParameter("@Monit_Flag", Convert.ToInt32(Flag));
        param[8] = new SqlParameter("@Monit_Area", Area);
        param[9] = new SqlParameter("@Monit_Beltline", Beltline);

        SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, sql, param);

        if (Convert.ToInt32(param[1].Value) == 1) return true;
        else return false;
    }
    //删除监控日志
    public bool DeleteLog(string LogID)
    {
        string sql = "sp_Monit_DeleteLog";
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@Monit_logID", Convert.ToInt32(LogID));
        param[1] = new SqlParameter("@reValue", SqlDbType.Int);
        param[1].Direction = ParameterDirection.Output;

        SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, sql, param);

        if (Convert.ToInt32(param[1].Value) == 1) return true;
        else return false;

        

    }
    //查询监控日志
    public DataSet SelectLog(string Plant,string Date1,string Date2,string mID,string flag,string PIC)
    {
        string sql = "sp_Monit_SelectLog";

        SqlParameter[] param = new SqlParameter[6];
        param[0] = new SqlParameter("@Monit_mID", mID);
        param[1] = new SqlParameter("@Plant", Plant);
        param[2] = new SqlParameter("@Date1", Convert.ToDateTime(Date1));
        param[3] = new SqlParameter("@Date2", Convert.ToDateTime(Date2));
        param[4] = new SqlParameter("@Monit_Flag", Convert.ToInt32(flag));
        param[5] = new SqlParameter("@PIC", PIC);

        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, sql, param);
    }
    //查询监控日志BY ID
    public DataTable SelectLogByID(string id)
    {
        string sql = "sp_Monit_SelectLogByID";

        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@Monit_ID", id);

        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, sql, param).Tables[0];
    }
    //查询监控照片（gridview的绑定）
    public DataSet SelectPicByID(string LogID, out int count)
    {
        string sql = "sp_Monit_SelectPicByID";
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@Monit_logID", Convert.ToInt32(LogID));
        param[1] = new SqlParameter("@count", SqlDbType.Int);
        param[1].Direction = ParameterDirection.Output;
        DataSet ds=SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, sql, param);
        count=Convert.ToInt32(param[1].Value);
        return ds;
    }
    //得到文件物理路径
    public string SelectPicAllPath(string id)
    {
        string sql = "select Monit_AllPath from Monit_pic where Monit_id=" + id;
        string AllPath = Convert.ToString(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.Text, sql));
        return AllPath;
    }
    //删除Monit_Pic的部分信息
    public void DeletePic(string id,string DeleteBy)
    {
        string sql = "sp_Monit_deletePic" ;
        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@Monit_ID", Convert.ToInt32(id));
        param[1] = new SqlParameter("@reValue", SqlDbType.Int);
        param[1].Direction = ParameterDirection.Output;
        param[2] = new SqlParameter("@DeleteBy", Convert.ToInt32(DeleteBy));
        SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, sql,param);
    }
    //上传图片文件
    public bool InsertMonitPic(int logID,string picName,string AllPath,string CreateBy)
    {
        string sql = "sp_Monit_insertPic";
        SqlParameter[] param = new SqlParameter[5];
        param[0] = new SqlParameter("@Monit_LogID", logID);
        param[1] = new SqlParameter("@Monit_PicName", picName);
        param[2] = new SqlParameter("@Monit_AllPath", AllPath);
        param[3] = new SqlParameter("@Monit_CreateBy", Convert.ToInt32(CreateBy));
        param[4] = new SqlParameter("@reValue", SqlDbType.Int);
        param[4].Direction = ParameterDirection.Output;

        SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, sql, param);

        if (Convert.ToInt32(param[4].Value) == 1) return true;
        else return false;

    }
    public DataSet SelectReviewers(int plant, int id, int type, int department, string userName, bool isleave)
    {
        try
        {
            string strName = "sp_RDW_SelectReviewers";
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@plant", plant);
            param[1] = new SqlParameter("@id", id);
            param[2] = new SqlParameter("@type", type);
            param[3] = new SqlParameter("@department", department);
            param[4] = new SqlParameter("@userName", userName);
            param[5] = new SqlParameter("@isleave", isleave);
            return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strName, param);
        }
        catch
        {
            return null;
        }
    }
    public void AddPIC(int id,string PICName,string PICID)
    {
        string sql = "sp_Monit_AddPIC";
        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@id", id);
        param[1] = new SqlParameter("@Monit_PIC", PICName);
        param[2] = new SqlParameter("@Monit_PICID", PICID);

        SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, sql, param);
    }
    public DataSet BindArea(string domain)
    {
        string sql = "sp_Monit_SelectArea";
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@domain", domain);

        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, sql, param);
    }
}