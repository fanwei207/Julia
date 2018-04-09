using adamFuncs;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CNTManage
/// </summary>
public class CNTManage
{
	public CNTManage()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    adamClass chk = new adamClass();
    private string _plate_number;
    public string plate_number { get; set; }
    private string _cnt_id;
    public string cnt_id { get; set; }
    private string _cnt_entrydate;
    public string cnt_entrydate { get; set; }
    private string _cnt_leavedate;
    public string cnt_leavedate { get; set; }
    private string _driver_name;
    public string driver_name { get; set; }
    private string _driver_IDCard;
    public string driver_IDCard { get; set; }
    private string _temporary_ID;
    public string temporary_ID { get; set; }
    private string _seal_ID;
    public string seal_ID { get; set; }
    private string _cnt_checkdate;
    public string cnt_checkdate { get; set; }
    private string _seal_checkdate;
    public string seal_checkdate { get; set; }
    private string _tracking_date;
    public string tracking_date { get; set; }
    private string _driver_phone;
    public string driver_phone { get; set; }
    private string _motorcade_phone;
    public string motorcade_phone { get; set; }
    private string _registBy;
    public string registBy { get; set; }
    private string _registDate;
    public string registDate { get; set; }

    public IList<CNTManage> SelectCntELInfo(string entryTime1,string entryTime2,bool iscnt,bool isseal,bool isleave)
    {
        string sql = "sp_Cnt_SelectCntELInfo";

        SqlParameter[] param = new SqlParameter[5];
        param[0] = new SqlParameter("@cnt_entrydate1", Convert.ToDateTime(entryTime1));
        param[1] = new SqlParameter("@cnt_entrydate2", Convert.ToDateTime(entryTime2));
        param[2] = new SqlParameter("@isChk", iscnt);
        param[3] = new SqlParameter("@isSeal", isseal);
        param[4] = new SqlParameter("@isLeave", isleave);

        IList<CNTManage> CNTManage=new List<CNTManage>();
        IDataReader reader= SqlHelper.ExecuteReader(chk.dsn0(), CommandType.StoredProcedure, sql, param);

        while(reader.Read())
        {
            CNTManage cnt=new CNTManage();

            cnt.cnt_id=reader["cnt_id"].ToString();
            cnt.plate_number=reader["plate_number"].ToString();
            cnt.driver_name=reader["driver_name"].ToString();
            cnt.cnt_entrydate=reader["cnt_entrydate"].ToString();
            cnt.seal_ID=reader["seal_ID"].ToString();
            cnt.driver_phone = reader["driver_phone"].ToString();
            cnt.motorcade_phone = reader["motorcade_phone"].ToString();

            if (reader["cnt_leavedate"] == DBNull.Value)
            {
                cnt.cnt_leavedate="出厂";
            }
            else cnt.cnt_leavedate = reader["cnt_leavedate"].ToString();

            if (reader["cnt_checkdate"] == DBNull.Value)
            {
                cnt.cnt_checkdate="检查";
            }
            else cnt.cnt_checkdate = reader["cnt_checkdate"].ToString();

            if (reader["seal_checkdate"] == DBNull.Value)
            {
                cnt.seal_checkdate="检查";
            }
            else cnt.seal_checkdate = reader["seal_checkdate"].ToString();

            if (reader["tracking_date"] == DBNull.Value)
            {
                cnt.tracking_date="联系";
            }
            else cnt.tracking_date = reader["tracking_date"].ToString();
            
            CNTManage.Add(cnt);
        }
        reader.Close();
        return CNTManage;
    }

    public int CntLeave(string cntId,string entryDate,string leaveDate)
    {
        string sql = "sp_Cnt_leave";
        SqlParameter[] param = new SqlParameter[4];
        param[0] = new SqlParameter("@cnt_entrydate", Convert.ToDateTime(entryDate));
        param[1] = new SqlParameter("@cnt_leavedate", Convert.ToDateTime(leaveDate));
        param[2] = new SqlParameter("@cnt_id", cntId);
        param[3] = new SqlParameter("@reValue",SqlDbType.Int);
        param[3].Direction = ParameterDirection.Output;

        SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.StoredProcedure, sql, param);
        return Convert.ToInt32(param[3].Value);
    }

}