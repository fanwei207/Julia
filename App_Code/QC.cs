using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Microsoft.ApplicationBlocks.Data;
using System.Data.SqlClient;
using adamFuncs;
using System.IO;

namespace QCProgress
{
    /// <summary>
    /// QC 的摘要说明
    /// </summary>
    public class QC
    {
        adamClass adam = new adamClass();
        FuncErrCollection MyErrorCollection = new FuncErrCollection();

        DataSet _dataset;

        public QC()
        {
            _dataset = null;
        }

        #region 进料检验报告页面
        /// <summary>
        /// 根据收货单号获取相应的进料检验报告
        /// </summary>
        /// <returns></returns>
        public DataSet GetReport(string receiver, string order, string line, string part, string vend, string stddate, string enddate, int pagesize, int pageindex, int uID, int plantid)
        {
            try
            {  
                string strName = "sp_QC_GetReport";
                SqlParameter[] param = new SqlParameter[11];
                param[0] = new SqlParameter("@receiver", receiver);
                param[1] = new SqlParameter("@order", order);
                param[2] = new SqlParameter("@line", line);
                param[3] = new SqlParameter("@part", part);
                param[4] = new SqlParameter("@vend", vend);
                param[5] = new SqlParameter("@stddate", stddate);
                param[6] = new SqlParameter("@enddate", enddate);
                param[7] = new SqlParameter("@pagesize", pagesize);
                param[8] = new SqlParameter("@pageindex", pageindex);
                param[9] = new SqlParameter("@uID", uID);
                param[10] = new SqlParameter("@plantid", plantid);

                return _dataset = SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName, param);
            }
            catch
            {
                return null;
            }
            finally 
            {
                _dataset.Dispose();
            }
        }
        public DataSet GetReport(string receiver, string order, string line, string part, string vend, string stddate, string enddate, int pagesize, int pageindex, int uID, int plantid,int isProduct)
        {
            try
            {
                string strName = "sp_QC_GetReport";
                SqlParameter[] param = new SqlParameter[12];
                param[0] = new SqlParameter("@receiver", receiver);
                param[1] = new SqlParameter("@order", order);
                param[2] = new SqlParameter("@line", line);
                param[3] = new SqlParameter("@part", part);
                param[4] = new SqlParameter("@vend", vend);
                param[5] = new SqlParameter("@stddate", stddate);
                param[6] = new SqlParameter("@enddate", enddate);
                param[7] = new SqlParameter("@pagesize", pagesize);
                param[8] = new SqlParameter("@pageindex", pageindex);
                param[9] = new SqlParameter("@uID", uID);
                param[10] = new SqlParameter("@plantid", plantid);
                param[11] = new SqlParameter("@isProduct", isProduct);

                return _dataset = SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName, param);
            }
            catch
            {
                return null;
            }
            finally
            {
                _dataset.Dispose();
            }
        }
        public DataSet GetReportComplete(string receiver, string order, string line, string part, string vend, string stddate, string enddate,int state, int pagesize, int pageindex, int uID, int plantid)
        {
            try
            {
                string strName = "sp_QC_GetReportComplete";
                SqlParameter[] param = new SqlParameter[12];
                param[0] = new SqlParameter("@receiver", receiver);
                param[1] = new SqlParameter("@order", order);
                param[2] = new SqlParameter("@line", line);
                param[3] = new SqlParameter("@part", part);
                param[4] = new SqlParameter("@vend", vend);
                param[5] = new SqlParameter("@stddate", stddate);
                param[6] = new SqlParameter("@enddate", enddate);
                param[7] = new SqlParameter("@state", state);
                param[8] = new SqlParameter("@pagesize", pagesize);
                param[9] = new SqlParameter("@pageindex", pageindex);
                param[10] = new SqlParameter("@uID", uID);
                param[11] = new SqlParameter("@plantid", plantid);

                return _dataset = SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName, param);
            }
            catch
            {
                return null;
            }
            finally
            {
                _dataset.Dispose();
            }
        }


        public DataSet GetReportComplete(string receiver, string order, string line, string part, string vend, string stddate, string enddate, int state, int pagesize, int pageindex, int uID, int plantid,int isProduct)
        {
            try
            {
                string strName = "sp_QC_GetReportComplete";
                SqlParameter[] param = new SqlParameter[13];
                param[0] = new SqlParameter("@receiver", receiver);
                param[1] = new SqlParameter("@order", order);
                param[2] = new SqlParameter("@line", line);
                param[3] = new SqlParameter("@part", part);
                param[4] = new SqlParameter("@vend", vend);
                param[5] = new SqlParameter("@stddate", stddate);
                param[6] = new SqlParameter("@enddate", enddate);
                param[7] = new SqlParameter("@state", state);
                param[8] = new SqlParameter("@pagesize", pagesize);
                param[9] = new SqlParameter("@pageindex", pageindex);
                param[10] = new SqlParameter("@uID", uID);
                param[11] = new SqlParameter("@plantid", plantid);
                param[12] = new SqlParameter("@isProduct", isProduct);

                return _dataset = SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName, param);
            }
            catch
            {
                return null;
            }
            finally
            {
                _dataset.Dispose();
            }
        }

        

        public DataSet GetReportUndo(string receiver, string nbr, string line, string vent, string part, string stddate, string enddate, string uID, string plantid, int PageSize, int PageIndex)
        {
            try
            {
                string strName = "sp_QC_GetReportUndo";
                SqlParameter[] param = new SqlParameter[11];
                param[0] = new SqlParameter("@receiver", receiver);
                param[1] = new SqlParameter("@nbr", nbr);
                param[2] = new SqlParameter("@line", line);
                param[3] = new SqlParameter("@vent", vent);
                param[4] = new SqlParameter("@part", part);
                param[5] = new SqlParameter("@stddate", stddate);
                param[6] = new SqlParameter("@enddate", enddate);
                param[7] = new SqlParameter("@uID", uID);
                param[8] = new SqlParameter("@plantid", plantid);
                param[9] = new SqlParameter("@PageSize", PageSize);
                param[10] = new SqlParameter("@PageIndex", PageIndex);

                return _dataset = SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName, param);
            }
            catch
            {
                return null;
            }
            finally
            {
                _dataset.Dispose();
            }
        }

        public  DataTable GetReportSnapByGroup(string page,string group)
        {
            try
            {
                string strName = "sp_QC_GetReportSnapByGroup";
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@page", page);
                param[1] = new SqlParameter("@group", group);

                _dataset = SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName, param);

                return _dataset.Tables[0];
            }
            catch
            {
                return null;
            }
            finally
            {
                _dataset.Dispose();
            }
        }
        /// <summary>
        /// 获取与进料报告相关联的缺陷内容
        /// </summary>
        /// <param name="repID">报告ID</param>
        /// <returns></returns>
        public  DataTable GetReportItem(string repID)
        {
            try
            {
                string strName = "sp_QC_GetReportItem";
                SqlParameter parm = new SqlParameter("@repID", repID);
                _dataset = SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName, parm);

                return _dataset.Tables[0];
            }
            catch
            {
                return null;
            }
            finally
            {
                _dataset.Dispose();
            }
        }
        /// <summary>
        /// 获取检验项目和子项目排序的列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetProject(string project, int flag)
        {
            try
            {
                string strName = "sp_QC_GetProject";
                SqlParameter[] parm = new SqlParameter[2];
                parm[0] = new SqlParameter("@project", project);
                parm[1] = new SqlParameter("@flag", flag);

                _dataset = SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName, parm);

                return _dataset.Tables[0];
            }
            catch
            {
                return null;
            }
            finally
            {
                _dataset.Dispose();
            }
        }
        public  DataSet GetProjectItem(string proName,string recv)
        {
            try
            {
                string strName = "sp_QC_GetProjectItem";
                SqlParameter[] parm = new SqlParameter[2];
                parm[0] = new SqlParameter("@proName", proName);
                parm[1] = new SqlParameter("@recv", recv);

                _dataset = SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName, parm);

                return _dataset;
            }
            catch
            {
                return null;
            }
            finally
            {
                _dataset.Dispose();
            }
        }
        /// <summary>
        /// 获取检验报告的检验项目
        /// </summary>
        /// <param name="line">进料行号</param>
        /// <param name="reciever">收单号</param>
        /// <returns></returns>
        public DataTable GetReportProject(string prhID, string Identity)
        {
            try
            {
                string strName = "sp_QC_GetReportProject";
                SqlParameter[] parm = new SqlParameter[2];
                parm[0] = new SqlParameter("@prhID", prhID);
                parm[1] = new SqlParameter("@Identity", Identity);

                _dataset = SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName, parm);

                return _dataset.Tables[0];
            }
            catch
            {
                return null;
            }
            finally
            {
                _dataset.Dispose();
            }
        }

        public  DataTable GetReportHistory(string prhID,string uID)
        {
            try
            {
                string strName = "sp_QC_GetReportHistory";
                SqlParameter[] parm = new SqlParameter[2];
                parm[0] = new SqlParameter("@prhID", prhID);
                parm[1] = new SqlParameter("@uID", uID);

                _dataset = SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName, parm);

                return _dataset.Tables[0];
            }
            catch
            {
                return null;
            }
            finally
            {
                _dataset.Dispose();
            }
        }

        /// <summary>
        /// 获取检验项目列表
        /// </summary>
        /// <returns></returns>
        public  DataTable GetProjectList()
        {
            try
            {
                string strName = "sp_QC_GetProjectList";
                _dataset = SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName);

                return _dataset.Tables[0];
            }
            catch
            {
                return null;
            }
            finally
            {
                _dataset.Dispose();
            }
        }
        /// <summary>
        /// 在绑定项目时,根据项目ID绑定"缺陷内容"中的子项目
        /// </summary>
        /// <param name="proID">项目ID</param>
        /// <returns></returns>
        public  DataTable GetProjectItemByID(string proID)
        {
            try
            {
                string strName = "sp_QC_GetProjectItemByID";
                SqlParameter parm = new SqlParameter("@proID", proID);
                _dataset = SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName, parm);

                return _dataset.Tables[0];

                _dataset.Dispose();
            }
            catch
            {
                return null;
            }
            finally
            {
                _dataset.Dispose();
            }
        }
        /// <summary>
        /// 获取aqlMin,aqlMax,Ac,Re
        /// </summary>
        /// <param name="level"></param>
        /// <param name="aql"></param>
        /// <returns></returns>
        public bool IsGB(string tID,string proName, string level, string aql, string rcvd, ref int org)
        {
            bool returnVal = true;

            try
            {
                string strName = "sp_QC_GetGB";
                SqlParameter[] parm = new SqlParameter[5];
                parm[0] = new SqlParameter("@typeID", tID);
                parm[1] = new SqlParameter("@proName", proName);
                parm[2] = new SqlParameter("@level", level);
                parm[3] = new SqlParameter("@aql", aql);
                parm[4] = new SqlParameter("@rcvd", rcvd);

                _dataset = SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName, parm);

                returnVal = bool.Parse(_dataset.Tables[0].Rows[0][0].ToString());
                org = int.Parse(_dataset.Tables[0].Rows[0][1].ToString());
            }
            catch
            {
                returnVal = false;
            }
            finally
            {
                _dataset.Dispose();
            }

            return returnVal;
        }

        public string[] GetGB(int type,string proID,string level, string aql, int rcvd)
        {
            string[] returnVal = new string[] { "", "", ""};

            try
            {
                string strName = "sp_QC_GetGB";
                SqlParameter[] parm = new SqlParameter[5];
                parm[0] = new SqlParameter("@typeID", type);
                parm[1] = new SqlParameter("@proName", proID);
                parm[2] = new SqlParameter("@level", level);
                parm[3] = new SqlParameter("@aql", aql);
                parm[4] = new SqlParameter("@rcvd", rcvd);

                _dataset = SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName, parm);

                if (_dataset.Tables[0].Rows.Count == 1)
                {
                    returnVal[0] = _dataset.Tables[0].Rows[0][1].ToString();
                    returnVal[1] = _dataset.Tables[0].Rows[0][2].ToString();
                    returnVal[2] = _dataset.Tables[0].Rows[0][3].ToString();
                }
            }
            catch
            {
                ;
            }
            return returnVal;
        }

        /// <summary>
        /// 新增进料报告
        /// </summary>
        /// <param name="Line">进料号</param>
        /// <param name="Reciever">收单号</param>
        /// <param name="Person">增加人</param>
        public  void AddReport(string Line, string Reciever, int Person)
        {
            try
            {
                string strName = "sp_QC_AddReport";
                SqlParameter[] parm = new SqlParameter[3];
                parm[0] = new SqlParameter("@Line", Line);
                parm[1] = new SqlParameter("@Reciever", Reciever);
                parm[2] = new SqlParameter("@Person", Person);
                SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strName, parm);
            }
            catch
            {
                return;
            }
        }
        /// <summary>
        /// 保存进料报告的缺陷内容
        /// </summary>
        /// <param name="repID">报告ID</param>
        /// <param name="proID">项目ID</param>
        /// <param name="pItemID">缺陷内容ID</param>
        /// <param name="Num">数量</param>
        public  FuncErrType AddReportItem(string repID, string proID, string pItemID, string Num, ref string msg)
        {
            int returnVal;

            try
            {
                string strName = "sp_QC_AddReportItem";
                SqlParameter[] parm = new SqlParameter[5];
                parm[0] = new SqlParameter("@repID", repID);
                parm[1] = new SqlParameter("@proID", proID);
                parm[2] = new SqlParameter("@pItemID", pItemID);
                parm[3] = new SqlParameter("@repNum", Num);
                parm[4] = new SqlParameter("@returnVal", SqlDbType.Int);
                parm[4].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strName, parm);

                returnVal = int.Parse(parm[4].Value.ToString());
            }
            catch (Exception ee)
            {
                msg = "数据库操作失败!原因:" + ee.ToString();
                returnVal = -1;
            }
            return MyErrorCollection.GetProcedureErrInfo(returnVal);
        }
        /// <summary>
        /// 增加进料报告的检验项目
        /// </summary>
        /// <param name="Line">进料号</param>
        /// <param name="Reciever">收单号</param>
        /// <param name="proID">项目ID</param>
        /// <param name="Level">检验水平</param>
        /// <param name="Aql">接受质量限</param>
        /// <param name="Num">不良数</param>
        /// <param name="Person">增加人</param>
        /// <param name="msg">返回信息</param>
        public FuncErrType AddReportProject(string prhID, string proName, string Level, string Aql, string Num, string CheckNum, string group, string dItemID, string dItemNum, string Identity, int qty, int Person, string remarks)
        {
            int returnVal;

            try
            {
                string strName = "sp_QC_AddReportProject";
                SqlParameter[] parm = new SqlParameter[14];
                parm[0] = new SqlParameter("@prhID", @prhID);
                parm[1] = new SqlParameter("@proName", proName);
                parm[2] = new SqlParameter("@Level", Level);
                parm[3] = new SqlParameter("@Aql", Aql);
                parm[4] = new SqlParameter("@Num", Num);
                parm[5] = new SqlParameter("@CheckNum", CheckNum);
                parm[6] = new SqlParameter("@group", group);
                parm[7] = new SqlParameter("@dItemID", dItemID);
                parm[8] = new SqlParameter("@dItemNum", dItemNum);
                parm[9] = new SqlParameter("@Identity", Identity);
                parm[10] = new SqlParameter("@qty", qty);
                parm[11] = new SqlParameter("@Person", Person);
                parm[12] = new SqlParameter("@remarks", remarks);
                parm[13] = new SqlParameter("@returnVal", SqlDbType.Int);
                parm[13].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strName, parm);

                returnVal = int.Parse(parm[13].Value.ToString());
            }
            catch
            {
                returnVal = -1;
            }
            return MyErrorCollection.GetProcedureErrInfo(returnVal);
        }
        /// <summary>
        /// 新增检验项目
        /// </summary>
        /// <param name="project">项目名称</param>
        /// <param name="person">创建人ID</param>
        public  void AddProject(int person, ref string msg)
        {
            try
            {
                string strName = "sp_QC_AddProject";
                SqlParameter parm = new SqlParameter("@person", person);

                SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strName, parm);

                msg = "";
            }
            catch (Exception ee)
            {
                msg = "数据库操作失败!原因:" + ee.ToString();
                return;
            }
        }
        public void AddProject(string proname, int person, ref string msg)
        {
            try
            {
                string strName = "sp_QC_AddProject1";
                SqlParameter[] parm = new SqlParameter[2];
                parm[0] = new SqlParameter("@proname", proname);
                parm[1] = new SqlParameter("@person", person);

                SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strName, parm);

                msg = "";
            }
            catch (Exception ee)
            {
                msg = "数据库操作失败!原因:" + ee.ToString();
                return;
            }
        }

        /// <summary>
        /// 添加检验项目的子项目
        /// </summary>
        /// <param name="project">检验项目名称</param>
        /// <param name="item">子项目名称</param>
        public  void AddProjectItem(string proName, int person, ref string msg)
        {
            try
            {
                string strName = "sp_QC_AddProjectItem";
                SqlParameter[] parm = new SqlParameter[2];
                parm[0] = new SqlParameter("@proName", proName);
                parm[1] = new SqlParameter("@person", person);

                SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strName, parm);

                msg = "";
            }
            catch (Exception ee)
            {
                msg = "数据库操作失败!原因:" + ee.ToString();
                return;
            }
        }
        /// <summary>
        /// 获取某个检验项目
        /// </summary>
        /// <param name="project">检验项目名称</param>
        /// <returns></returns>
        public  DataTable GetProjectItemByName(string project)
        {
            try
            {
                string strName = "sp_QC_GetProjectItemByName";
                SqlParameter parm = new SqlParameter("@project", project);
                _dataset = SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName, parm);

                return _dataset.Tables[0];
            }
            catch
            {
                return null;
            }
            finally 
            {
                _dataset.Dispose();
            }
        }
        /// <summary>
        /// 删除一张检验报告
        /// </summary>
        /// <param name="repID">报告ID</param>
        public FuncErrType DeleteReport(string repID, string uID)
        {
            int returnVal;

            try
            {
                string strName = "sp_QC_DeleteReport";
                SqlParameter[] parm = new SqlParameter[3];
                parm[0] = new SqlParameter("@repID", repID);
                parm[1] = new SqlParameter("@uID", uID);
                parm[2] = new SqlParameter("@returVal", SqlDbType.Int);
                parm[2].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strName, parm);

                returnVal = int.Parse(parm[2].Value.ToString());
            }
            catch
            {
                returnVal = -1;
            }

            return MyErrorCollection.GetProcedureErrInfo(returnVal);
        }
        /// <summary>
        /// 删除一个检验项目
        /// </summary>
        /// <param name="proID">项目ID</param>
        public  void DeleteProject(int proID)
        {
            try
            {
                string strName = "sp_QC_DeleteProject";
                SqlParameter parm = new SqlParameter("@proID", proID);
                SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strName, parm);
            }
            catch
            {
                return;
            }
        }
        /// <summary>
        /// 删除项目的隶属的一个检验子项目
        /// </summary>
        /// <param name="pItemID">子项目ID</param>
        public  void DeleteProjectItem(int pItemID)
        {
            try
            {
                string strName = "sp_QC_DeleteProItem";
                SqlParameter parm = new SqlParameter("@pItemID", pItemID);
                SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strName, parm);
            }
            catch
            {
                return;
            }
        }
        /// <summary>
        /// 修改检验项目
        /// </summary>
        /// <param name="proID">项目ID</param>
        /// <param name="Name">项目名称</param>
        /// <param name="Person">修改人</param>
        /// <param name="msg">返回信息</param>
        public  FuncErrType ModifyProject(int proID, string Name, int min, int max, int num, int ac, int re, int Person,int tID)
        {
            int returnVal;

            try
            {
                string strName = "sp_QC_ModifyProject";
                SqlParameter[] parm = new SqlParameter[10];
                parm[0] = new SqlParameter("@proID", proID);
                parm[1] = new SqlParameter("@Name", Name);
                parm[2] = new SqlParameter("@min", min);
                parm[3] = new SqlParameter("@max", max);
                parm[4] = new SqlParameter("@num", num);
                parm[5] = new SqlParameter("@ac", ac);
                parm[6] = new SqlParameter("@re", re);
                parm[7] = new SqlParameter("@person", Person);
                parm[8] = new SqlParameter("@tID", tID);
                parm[9] = new SqlParameter("@returVal", SqlDbType.Int);
                parm[9].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strName, parm);

                returnVal = int.Parse(parm[9].Value.ToString());
            }
            catch
            {
                returnVal = -1;
            }
            return MyErrorCollection.GetProcedureErrInfo(returnVal);
        }
        /// <summary>
        /// 修改检验项目的子项目
        /// </summary>
        /// <param name="itemID">子项目ID</param>
        /// <param name="proName">隶属项目名称</param>
        /// <param name="ItemName">子项目名称</param>
        /// <param name="Person">修改人</param>
        /// <param name="msg">返回信息</param>
        public  void ModifyProjectItem(int itemID, string proName, string ItemName, int Person, ref string msg)
        {
            try
            {
                string strName = "sp_QC_ModifyProjectItem";
                SqlParameter[] parm = new SqlParameter[5];
                parm[0] = new SqlParameter("@itemID", itemID);
                parm[1] = new SqlParameter("@proName", proName);
                parm[2] = new SqlParameter("@ItemName", ItemName);
                parm[3] = new SqlParameter("@person", Person);
                parm[4] = new SqlParameter("@returnVal", SqlDbType.Int);
                parm[4].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strName, parm);

                if (int.Parse(parm[4].Value.ToString()) == 1)
                {
                    msg = "所添加的子项目已经存在!";
                }
                else
                {
                    msg = "";
                }
            }
            catch (Exception ee)
            {
                msg = "数据库操作失败!原因:" + ee.ToString();
                return;
            }
        }

        public  void SetReportComplete(string recieve,string order,string line,string group,int state, int uID) 
        {
            try
            {
                string strName = "sp_QC_SetReportComplete";
                SqlParameter[] parm = new SqlParameter[6];
                parm[0] = new SqlParameter("@recieve", recieve);
                parm[1] = new SqlParameter("@order", order);
                parm[2] = new SqlParameter("@line", line);
                parm[3] = new SqlParameter("@group", group);
                parm[4] = new SqlParameter("@state", state);
                parm[5] = new SqlParameter("@uID", uID);

                SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strName, parm);
            }
            catch (Exception ee)
            {
                return;
            }
        }

        public void RefuseReport(string group)
        {
            try
            {
                string strName = "sp_QC_RefuseReport";
                SqlParameter[] parm = new SqlParameter[1];
                parm[0] = new SqlParameter("@group", group);

                 SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strName, parm);
            }
            catch
            {
                return ;
            }
        }

        public int SetReportInGroup(string recieve, string order, string line, string group, string uID)
        {
            try
            {
                string strName = "sp_QC_SetReportInGroup";
                SqlParameter[] parm = new SqlParameter[5];
                parm[0] = new SqlParameter("@recieve", recieve);
                parm[1] = new SqlParameter("@order", order);
                parm[2] = new SqlParameter("@line", line);
                parm[3] = new SqlParameter("@group", group);
                parm[4] = new SqlParameter("@uID", uID);

                return SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strName, parm);
            }
            catch
            {
                return -1;
            }
        }

        #endregion

        #region GB/T 2828.1 - 2003中的抽样检验用表
        /// <summary>
        /// 动态创建GridView
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="gv"></param>
        public  void DynamicBindgvGbt2828(DataTable dt, GridView gv)
        {
            gv.Columns.Clear();

            for (int i = 0; i < dt.Columns.Count; i++)
            {
                TemplateField tf = new TemplateField();
                tf.HeaderText = dt.Columns[i].Caption.ToString().Trim();
                if (i < 2)
                {
                    tf.HeaderStyle.Width = Unit.Pixel(50);
                    tf.ItemTemplate = new GridViewTemplate("Label", dt.Columns[i].ColumnName.ToString(), DataControlRowType.DataRow);
                }
                else
                {
                    tf.HeaderStyle.Width = Unit.Pixel(50);
                    tf.ItemTemplate = new GridViewTemplate("Label", dt.Columns[i].ColumnName.ToString(), DataControlRowType.DataRow);
                    tf.EditItemTemplate = new GridViewTemplate("TextBox", dt.Columns[i].ColumnName.ToString(), DataControlRowType.DataRow);
                }
                tf.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                gv.Columns.Add(tf);
            }

            CommandField cfEdit = new CommandField();
            cfEdit.ButtonType = ButtonType.Link;
            cfEdit.ShowEditButton = true;
            cfEdit.EditText = "编辑";
            cfEdit.UpdateText = "更新";
            cfEdit.CancelText = "取消";
            cfEdit.CausesValidation = false;
            cfEdit.HeaderStyle.Width = Unit.Pixel(90);
            cfEdit.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
            gv.Columns.Add(cfEdit);

            gv.DataSource = dt;
            gv.DataBind();
        }
        /// <summary>
        /// 获取GB/T 2828.1 -2003中的抽样检验用表的内容
        /// </summary>
        /// <returns></returns>
        public  DataTable GetGbt2828()
        {
            try
            {
                string strName = "sp_QC_GetGbt2828";
                _dataset = SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName);

                return _dataset.Tables[0];
            }
            catch
            {
                return null;
            }
            finally
            {
                _dataset.Dispose();
            }
        }
        /// <summary>
        /// 获取批量数值列表
        /// </summary>
        /// <returns></returns>
        public  DataTable GetGbtPatch(string Condition)
        {
            try
            {
                string strName = "sp_QC_GetGbtPatch";
                SqlParameter parm = new SqlParameter("@Condition", Condition);

                _dataset = SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName, parm);

                return _dataset.Tables[0];
            }
            catch
            {
                return null;
            }
            finally
            {
                _dataset.Dispose();
            }
        }
        /// <summary>
        /// 获取检验水平列表
        /// </summary>
        /// <returns></returns>
        public  DataTable GetGbtLevel(string Condition)
        {
            try
            {
                string strName = "sp_QC_GetGbtLevel";
                SqlParameter parm = new SqlParameter("@Condition", Condition);

                _dataset = SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName, parm);

                return _dataset.Tables[0];
            }
            catch
            {
                return null;
            }
            finally
            {
                _dataset.Dispose();
            }
        }
        /// <summary>
        /// 新增抽样检验用表的一行
        /// </summary>
        public  void AddGbtPatch(int Person)
        {
            try
            {
                string strName = "sp_QC_AddGbtPatch";
                SqlParameter parm = new SqlParameter("@person", Person);

                SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strName, parm);
            }
            catch
            {
                return;
            }
        }
        /// <summary>
        /// 新增一个检验水平
        /// </summary>
        /// <param name="Level">水平名称</param>
        /// <param name="Person">创建人ID</param>
        public  void AddGbtLevel(int Person)
        {
            try
            {
                string strName = "sp_QC_AddGbtLevel";
                SqlParameter parm = new SqlParameter("@person", Person);

                SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strName, parm);
            }
            catch
            {
                return;
            }
        }
        /// <summary>
        /// 更新抽样检验用表
        /// </summary>
        /// <param name="Min">最小值</param>
        /// <param name="Max">最大值</param>
        /// <param name="Level">检验水平</param>
        /// <param name="Code">样本量子码</param>
        public  void ModifyGbt2828(int Min, int Max, string Level, string Code, int Person, ref string msg)
        {
            try
            {
                string strName = "sp_QC_ModifyGbt2828";
                SqlParameter[] parm = new SqlParameter[6];
                parm[0] = new SqlParameter("@min", Min);
                parm[1] = new SqlParameter("@max", Max);
                parm[2] = new SqlParameter("@level", Level);
                parm[3] = new SqlParameter("@code", Code);
                parm[4] = new SqlParameter("@person", Person);
                parm[5] = new SqlParameter("@returnVal", SqlDbType.Int);
                parm[5].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strName, parm);

                if (int.Parse(parm[5].Value.ToString()) == 1)
                {
                    msg = "所添加的字码尚不存在!";
                }
                else
                {
                    msg = "";
                }
            }
            catch (Exception ee)
            {
                msg = "数据库操作失败!原因:" + ee.ToString();
                return;
            }
        }
        /// <summary>
        /// 修改抽样检验用表中的一行
        /// </summary>
        /// <param name="nMin">新的最小值</param>
        /// <param name="nMax">新的最大值</param>
        /// <param name="oMin">旧的最小值</param>
        /// <param name="oMax">旧的最大值</param>
        public  void ModifyGbtPatch(int nMin, int nMax, int oMin, int oMax, int Person, ref string msg)
        {
            try
            {
                string strName = "sp_QC_ModifyGbtPatch";
                SqlParameter[] parm = new SqlParameter[6];
                parm[0] = new SqlParameter("@nMin", nMin);
                parm[1] = new SqlParameter("@nMax", nMax);
                parm[2] = new SqlParameter("@oMin", oMin);
                parm[3] = new SqlParameter("@oMax", oMax);
                parm[4] = new SqlParameter("@person", Person);
                parm[5] = new SqlParameter("@returnVal", SqlDbType.Int);
                parm[5].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strName, parm);

                if (int.Parse(parm[5].Value.ToString()) == 1)
                {
                    msg = "Min值必须不大于Max值!";
                }
                else if (int.Parse(parm[5].Value.ToString()) == 2)
                {
                    msg = "所添加的范围必须在现有范围之外!";
                }
                else
                {
                    msg = "";
                }
            }
            catch (Exception ee)
            {
                msg = "数据库操作失败!原因:" + ee.ToString();
                return;
            }
        }
        /// <summary>
        /// 修改检验水平
        /// </summary>
        /// <param name="nLevel">新水平名称</param>
        /// <param name="oLevel">旧水平名称</param>
        /// <param name="Person">修改人ID</param>
        public  void ModifyGbtLevel(string nLevel, string oLevel, int Person, ref string msg)
        {
            try
            {
                string strName = "sp_QC_ModifyGbtLevel";
                SqlParameter[] parm = new SqlParameter[4];
                parm[0] = new SqlParameter("@nLevel", nLevel);
                parm[1] = new SqlParameter("@oLevel", oLevel);
                parm[2] = new SqlParameter("@person", Person);
                parm[3] = new SqlParameter("@returnVal", SqlDbType.Int);
                parm[3].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strName, parm);

                if (int.Parse(parm[3].Value.ToString()) == 1)
                {
                    msg = "所添加的检验水平已经存在!";
                }
                else
                {
                    msg = "";
                }
            }
            catch (Exception ee)
            {
                msg = "数据库操作失败!原因:" + ee.ToString();
                return;
            }
        }
        /// <summary>
        /// 删除GB/T 2828.1-2003表中的一行
        /// </summary>
        /// <param name="Min">最小值</param>
        /// <param name="Max">最大值</param>
        public  void DeleteGbtPatch(int Min, int Max)
        {
            try
            {
                string strName = "sp_QC_DeleteGbtPatch";
                SqlParameter[] parmPatch = new SqlParameter[2];
                parmPatch[0] = new SqlParameter("@Min", Min);
                parmPatch[1] = new SqlParameter("@Max", Max);
                SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strName, parmPatch);
            }
            catch
            {
                return;
            }
        }
        /// <summary>
        /// 删除一个检验水平
        /// </summary>
        /// <param name="Level">水平名称</param>
        public  void DeleteGbtLevel(string Level)
        {
            try
            {
                string strName = "sp_QC_DeleteGbtLevel";
                SqlParameter parmGbtLevel = new SqlParameter("@level", Level);
                SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strName, parmGbtLevel);
            }
            catch
            {
                return;
            }
        }
        #endregion

        #region 一次抽样方案
        /// <summary>
        /// 动态创建GridView
        /// </summary>
        /// <param name="dt">数据源</param>
        /// <param name="gv">绑定的GridViw控件</param>
        public  void DynamicBindgvAql(DataTable dt, GridView gv)
        {
            gv.Columns.Clear();

            for (int i = 0; i < dt.Columns.Count; i++)
            {
                TemplateField tf = new TemplateField();
                tf.HeaderText = dt.Columns[i].Caption.ToString();
                if (i < 2)
                {
                    tf.ItemStyle.Width = Unit.Pixel(45);
                    tf.ItemTemplate = new GridViewTemplate("Label", dt.Columns[i].ColumnName.ToString(), DataControlRowType.DataRow);
                }
                else
                {
                    tf.ItemStyle.Width = Unit.Pixel(50);
                    tf.ItemTemplate = new GridViewTemplate("Label", dt.Columns[i].ColumnName.ToString(), DataControlRowType.DataRow);
                    tf.EditItemTemplate = new GridViewTemplate("TextBox", dt.Columns[i].ColumnName.ToString(), DataControlRowType.DataRow);
                }
                tf.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                gv.Columns.Add(tf);
            }

            gv.DataSource = dt;
            gv.DataBind();
        }
        /// <summary>
        /// 获取抽烟方案内容
        /// </summary>
        /// <returns></returns>
        public  DataTable GetAql()
        {
            try
            {
                string strName = "sp_QC_GetAql";
                _dataset = SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName);

                return _dataset.Tables[0];
            }
            catch
            {
                return null;
            }
            finally
            {
                _dataset.Dispose();
            }
        }
        /// <summary>
        /// 获取抽样方案的样本量字码
        /// </summary>
        /// <returns></returns>
        public  DataTable GetAqlCode(string Condition)
        {
            try
            {
                string strName = "sp_QC_GetAqlCode";
                SqlParameter parm = new SqlParameter("@Condition", Condition);

                _dataset = SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName, parm);

                return _dataset.Tables[0];
            }
            catch
            {
                return null;
            }
            finally
            {
                _dataset.Dispose();
            }
        }
        /// <summary>
        /// 获取所有的AQL值
        /// </summary>
        /// <returns></returns>
        public  DataTable GetAqlLevel(string Condition)
        {
            try
            {
                string strName = "sp_QC_GetAqlLevel";
                SqlParameter parm = new SqlParameter("@Condition", Condition);

                _dataset = SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName, parm);

                return _dataset.Tables[0];
            }
            catch
            {
                return null;
            }
            finally
            {
                _dataset.Dispose();
            }
        }
        /// <summary>
        /// 新增一个样本量字码
        /// </summary>
        /// <param name="Code">样本量字码</param>
        /// <param name="Num">检验数量</param>
        /// <param name="Person">创建人ID</param>
        public  void AddAqlCode(int Person)
        {
            try
            {
                string strName = "sp_QC_AddAqlCode";
                SqlParameter parm = new SqlParameter("@person", Person);
                SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strName, parm);
            }
            catch
            {
                return;
            }
        }
        /// <summary>
        /// 新增一个AQL值
        /// </summary>
        /// <param name="Aql">AQL值</param>
        /// <param name="Person">创建人ID</param>
        public  void AddAqlLevel(int Person)
        {
            try
            {
                string strName = "sp_QC_AddAqlLevel";
                SqlParameter parm = new SqlParameter("@person", Person);

                SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strName, parm);
            }
            catch
            {
                return;
            }
        }
        /// <summary>
        /// 修改抽样检验表
        /// </summary>
        /// <param name="Code">样本量字码</param>
        /// <param name="Level">AQL值</param>
        /// <param name="Ac">AC值</param>
        /// <param name="Re">RE值</param>
        /// <param name="Way">方向</param>
        /// <param name="Person">修改人ID</param>
        public  void ModifyAql(string Code, string Level, string Ac, string Re, string Way, int Person, ref string msg)
        {
            try
            {
                string strName = "sp_QC_ModifyAql";
                SqlParameter[] parm = new SqlParameter[7];
                parm[0] = new SqlParameter("@code", Code);
                parm[1] = new SqlParameter("@level", Level);
                parm[2] = new SqlParameter("@ac", Ac);
                parm[3] = new SqlParameter("@re", Re);
                parm[4] = new SqlParameter("@way", Way);
                parm[5] = new SqlParameter("@person", Person);
                parm[6] = new SqlParameter("@returnVal", SqlDbType.Int);
                parm[6].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strName, parm);

                if (int.Parse(parm[6].Value.ToString()) == 1)
                {
                    msg = "标准维护失败!";
                }
                else
                {
                    msg = "";
                }
            }
            catch (Exception ee)
            {
                msg = "数据库操作失败!原因:" + ee.ToString();
                return;
            }
        }
        /// <summary>
        /// 修改一个样本量字码
        /// </summary>
        /// <param name="nCode">新的字码</param>
        /// <param name="oCode">旧的字码</param>
        /// <param name="Orig">样本量</param>
        /// <param name="Person">修改人ID</param>
        public  void ModifyAqlCode(string nCode, string oCode, string Orig, int Person, ref string msg)
        {
            try
            {
                string strName = "sp_QC_ModifyAqlCode";
                SqlParameter[] parm = new SqlParameter[5];
                parm[0] = new SqlParameter("@nCode", nCode);
                parm[1] = new SqlParameter("@oCode", oCode);
                parm[2] = new SqlParameter("@orig", Orig);
                parm[3] = new SqlParameter("@person", Person);
                parm[4] = new SqlParameter("@returnVal", SqlDbType.Int);
                parm[4].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strName, parm);

                if (int.Parse(parm[4].Value.ToString()) == 1)
                {
                    msg = "所添加的样本量字码已经存在!";
                }
                else
                {
                    msg = "";
                }
            }
            catch (Exception ee)
            {
                msg = "数据库操作失败!原因:" + ee.ToString();
                return;
            }
        }
        /// <summary>
        /// 修改一个AQL值
        /// </summary>
        /// <param name="nLevel">新的AQL值</param>
        /// <param name="oLevel">旧的AQL值</param>
        /// <param name="Person">修改人ID</param>
        public  void ModifyAqlLevel(string nLevel, string oLevel, int Person, ref string msg)
        {
            try
            {
                string strName = "sp_QC_ModifyAqlLevel";
                SqlParameter[] parm = new SqlParameter[4];
                parm[0] = new SqlParameter("@nLevel", nLevel);
                parm[1] = new SqlParameter("@oLevel", oLevel);
                parm[2] = new SqlParameter("@person", Person);
                parm[3] = new SqlParameter("@returnVal", SqlDbType.Int);
                parm[3].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strName, parm);

                if (int.Parse(parm[3].Value.ToString()) == 1)
                {
                    msg = "所添加的质量接受限已经存在!";
                }
                else
                {
                    msg = "";
                }
            }
            catch (Exception ee)
            {
                msg = "数据库操作失败!原因:" + ee.ToString();
                return;
            }
        }
        /// <summary>
        /// 删除一个样本量字码
        /// </summary>
        /// <param name="Code">字码</param>
        public  void DeleteAqlCode(string Code)
        {
            try
            {
                string strName = "sp_QC_DeleteAqlCode";
                SqlParameter parmCode = new SqlParameter("@code", Code);
                SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strName, parmCode);
            }
            catch
            {
                return;
            }
        }
        /// <summary>
        /// 删除一个AQL值
        /// </summary>
        /// <param name="Level">AQL值</param>
        public  void DeleteAqlLevel(float Level)
        {
            try
            {
                string strName = "sp_QC_DeleteAqlLevel";
                SqlParameter parmLevel = new SqlParameter("@level", Level);
                SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strName, parmLevel);
            }
            catch
            {
                return;
            }
        }
        #endregion

        #region 光通量

        public DataTable GetLumFlax(string line, string recv)
        {
            try
            {
                string strName = "sp_QC_GetLumFlax";
                SqlParameter[] parm = new SqlParameter[2];
                parm[0] = new SqlParameter("@line", line);
                parm[1] = new SqlParameter("@recv", recv);

                _dataset = SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName, parm);
                return _dataset.Tables[0];
            }
            catch
            {
                return null;
            }
            finally
            {
                _dataset.Dispose();
            }
        }
        public DataTable GetProductLumFlax(string line, string recv, Boolean tcp)
        {
            try
            {
                string strName = "sp_QC_GetProductLumFlax";
                SqlParameter[] parm = new SqlParameter[3];
                parm[0] = new SqlParameter("@line", line);
                parm[1] = new SqlParameter("@recv", recv);
                parm[2] = new SqlParameter("@tcp", tcp);

                _dataset = SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName, parm);
                return _dataset.Tables[0];
            }
            catch
            {
                return null;
            }
            finally
            {
                _dataset.Dispose();
            }
        }
        public DataSet GetLumFlaxProduct(string date1, string date2, string nbr1, string nbr2, string lot1, string lot2, string part1, string part2)
        {
            try
            {
                string strName = "sp_QC_GetLumFlaxProduct";
                SqlParameter[] parm = new SqlParameter[8];
                parm[0] = new SqlParameter("@date1", date1);
                parm[1] = new SqlParameter("@date2", date2);
                parm[2] = new SqlParameter("@nbr1", nbr1);
                parm[3] = new SqlParameter("@nbr2", nbr2);
                parm[4] = new SqlParameter("@lot1", lot1);
                parm[5] = new SqlParameter("@lot2", lot2);
                parm[6] = new SqlParameter("@part1", part1);
                parm[7] = new SqlParameter("@part2", part2);

                _dataset = SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName, parm);
                return _dataset;
            }
            catch
            {
                return null;
            }
            finally
            {
                _dataset.Dispose();
            }
        }

        public  FuncErrType DeleteLumFlax(int id, ref string msg)
        {
            int returnVal;

            try
            {
                string strName = "sp_QC_DeleteLumFlax";
                SqlParameter[] parm = new SqlParameter[1];
                parm[0] = new SqlParameter("@id", id);

                SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strName, parm);
                returnVal = 0;
            }
            catch (Exception ee)
            {
                msg = "数据库操作失败!原因:" + ee.ToString();
                returnVal = -1;
            }
            return MyErrorCollection.GetProcedureErrInfo(returnVal);
        }
        public FuncErrType DeleteProdcutLumFlax(int id, ref string msg)
        {
            int returnVal;

            try
            {
                string strName = "sp_QC_DeleteProductLumFlax";
                SqlParameter[] parm = new SqlParameter[1];
                parm[0] = new SqlParameter("@id", id);

                SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strName, parm);
                returnVal = 0;
            }
            catch (Exception ee)
            {
                msg = "数据库操作失败!原因:" + ee.ToString();
                returnVal = -1;
            }
            return MyErrorCollection.GetProcedureErrInfo(returnVal);
        }
        #endregion

        #region 一次过程检验

        /// <summary>
        /// 获取一次过程检验单
        /// </summary>
        /// <returns></returns>
        public DataSet GetProcess_New(string order, string line, string wo_part, string vent, string lot, string part, string group
                                , string date1, string date2, string lineMgt, int PageSize, int PageIndex)
        {
            try
            {
                string strName = "sp_QC_GetProcess_New";
                SqlParameter[] parm = new SqlParameter[12];
                parm[0] = new SqlParameter("@order", order);
                parm[1] = new SqlParameter("@line", line);
                parm[2] = new SqlParameter("@wo_part", wo_part);
                parm[3] = new SqlParameter("@vent", vent);
                parm[4] = new SqlParameter("@lot", lot);
                parm[5] = new SqlParameter("@part", part);
                parm[6] = new SqlParameter("@group", group);
                parm[7] = new SqlParameter("@date1", date1);
                parm[8] = new SqlParameter("@date2", date2);
                parm[9] = new SqlParameter("@lineMgt", lineMgt);
                parm[10] = new SqlParameter("@PageSize", PageSize);
                parm[11] = new SqlParameter("@PageIndex", PageIndex);

                return _dataset = SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName, parm);
            }
            catch
            {
                return null;
            }
            finally
            {
                _dataset.Dispose();
            }
        }

        public FuncErrType GetProductSnatch_New(string line, string order, ref DataTable table)
        {
            int returnVal;

            try
            {
                string strName = "sp_QC_GetProductSnatch";
                SqlParameter[] parm = new SqlParameter[2];
                parm[0] = new SqlParameter("@line", line);
                parm[1] = new SqlParameter("@WorkOrder", order);

                _dataset = SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName, parm);

                table = _dataset.Tables[0];

                returnVal = int.Parse(_dataset.Tables[0].Rows[0][0].ToString());
            }
            catch
            {
                table = null;
                returnVal = -1;
            }
            return MyErrorCollection.GetProcedureErrInfo(returnVal);
        }

        /// <summary>
        /// insert a new record or process
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="workorder"></param>
        /// <param name="provider"></param>
        /// <param name="no"></param>
        /// <param name="process"></param>
        /// <param name="nIn"></param>
        /// <param name="nOut"></param>
        /// <param name="part"></param>
        /// <param name="person"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public FuncErrType AddProcess_New(string ID, string workorder, string provider, string no, int nIn, int nOut, string part
                                        , int person, string remarks, string workgroup, string lineMgt)
        {
            int returnVal;

            try
            {
                string strName = "sp_QC_AddProcess_New";
                SqlParameter[] parm = new SqlParameter[12];
                parm[0] = new SqlParameter("@ID", ID);
                parm[1] = new SqlParameter("@workorder", workorder);
                parm[2] = new SqlParameter("@provider", provider);
                parm[3] = new SqlParameter("@no", no);
                parm[4] = new SqlParameter("@nIn", nIn);
                parm[5] = new SqlParameter("@nOut", nOut);
                parm[6] = new SqlParameter("@part", part);
                parm[7] = new SqlParameter("@person", person);
                parm[8] = new SqlParameter("@remarks", remarks);
                parm[9] = new SqlParameter("@workgroup", workgroup);
                parm[10] = new SqlParameter("@lineMgt", lineMgt);
                parm[11] = new SqlParameter("@returnVal", SqlDbType.Int);
                parm[11].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strName, parm);

                returnVal = int.Parse(parm[11].Value.ToString());
            }
            catch
            {
                returnVal = -1;
            }
            return MyErrorCollection.GetProcedureErrInfo(returnVal);
        }


        public FuncErrType ModifyProcess_New(string prcID, string provider, string no, string nIn, string nOut, string part, int person, string remarks, string workgroup, string lineMgt)
        {
            int returnVal;

            try
            {
                string strName = "sp_QC_ModifyProcess_New";
                SqlParameter[] parm = new SqlParameter[11];
                parm[0] = new SqlParameter("@prcID", prcID);
                parm[1] = new SqlParameter("@provider", provider);
                parm[2] = new SqlParameter("@no", no);
                parm[3] = new SqlParameter("@nIn", nIn);
                parm[4] = new SqlParameter("@nOut", nOut);
                parm[5] = new SqlParameter("@part", part);
                parm[6] = new SqlParameter("@person", person);
                parm[7] = new SqlParameter("@remarks", remarks);
                parm[8] = new SqlParameter("@workgroup", workgroup);
                parm[9] = new SqlParameter("@lineMgt", lineMgt);
                parm[10] = new SqlParameter("@returnVal", SqlDbType.Int);
                parm[10].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strName, parm);

                returnVal = int.Parse(parm[10].Value.ToString());
            }
            catch
            {
                returnVal = -1;
            }

            return MyErrorCollection.GetProcedureErrInfo(returnVal);
        }        /// <summary>

        public FuncErrType DeleteProcess_New(int ID, int uID, ref string msg)
        {
            int returnVal;

            try
            {
                string strName = "sp_QC_DeleteProcess_New";
                SqlParameter[] parm = new SqlParameter[2];
                parm[0] = new SqlParameter("@ID", ID);
                parm[1] = new SqlParameter("@uID", uID);

                SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strName, parm);

                returnVal = 0;
            }
            catch (Exception ee)
            {
                msg = "数据库操作失败!原因:" + ee.ToString();
                returnVal = -1;
            }
            return MyErrorCollection.GetProcedureErrInfo(returnVal);
        }

        public String GetProcessLot_New(String WorkOrder, String part)
        {
            try
            {
                string strName = "sp_QC_GetProcessLot";
                SqlParameter[] parm = new SqlParameter[2];
                parm[0] = new SqlParameter("@WorkOrder", WorkOrder);
                parm[1] = new SqlParameter("@part", part);

                _dataset = SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName, parm);

                return _dataset.Tables[0].Rows[0][0].ToString().Trim();
            }
            catch
            {
                return String.Empty;
            }
            finally
            {
                _dataset.Dispose();
            }
        }

        public DataSet GetProcessUnfinished_New(string order, string line, string date1, string date2, int plantid)
        {
            try
            {
                string strName = "sp_QC_GetProcessUnfinished_New";
                SqlParameter[] parm = new SqlParameter[5];
                parm[0] = new SqlParameter("@order", order);
                parm[1] = new SqlParameter("@line", line);
                parm[2] = new SqlParameter("@date1", date1);
                parm[3] = new SqlParameter("@date2", date2);
                parm[4] = new SqlParameter("@plantid", plantid);

                return _dataset = SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName, parm);
            }
            catch
            {
                return null;
            }
            finally
            {
                _dataset.Dispose();
            }
        }

        public DataTable GetProcessByID_New(int prcID)
        {
            try
            {
                string strName = "sp_QC_GetProcessByID_New";
                SqlParameter[] parm = new SqlParameter[1];
                parm[0] = new SqlParameter("@prcID", prcID);

                _dataset = SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName, parm);

                return _dataset.Tables[0];
            }
            catch
            {
                return null;
            }
            finally
            {
                _dataset.Dispose();
            }
        }

        public DataTable GetProcessDefectType_New(String uID)
        {
            try
            {
                string strName = "sp_QC_GetProcessDefectType_New";
                SqlParameter[] parm = new SqlParameter[1];
                parm[0] = new SqlParameter("@uID", uID);

                _dataset = SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName, parm);

                return _dataset.Tables[0];
            }
            catch
            {
                return null;
            }
            finally
            {
                _dataset.Dispose();
            }
        }

        public int GetProcessDefectTypeById_New(string prcId)
        {
            string strName = "sp_QC_GetProcessDefectTypeById_New";
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@prcId", prcId);
            int typeId = (int)SqlHelper.ExecuteScalar(adam.dsnx(), CommandType.StoredProcedure, strName, parm);
            return typeId;
        }

        public DataTable GetProcessItem_New(int prcID, int tID)
        {
            try
            {
                string strName = "sp_QC_GetProcessItem_New";
                SqlParameter[] parm = new SqlParameter[2];
                parm[0] = new SqlParameter("@prcID", prcID);
                parm[1] = new SqlParameter("@tID", tID);

                _dataset = SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName, parm);

                return _dataset.Tables[0];
            }
            catch
            {
                return null;
            }
            finally
            {
                _dataset.Dispose();
            }
        }

        /// <summary>
        /// 获取过程检验项目
        /// </summary>
        /// <returns></returns>
        public DataSet GetProcessDefect_New(int prcdID, int prcdItemID)
        {
            try
            {
                string strName = "sp_QC_GetProcessDefect_New";
                SqlParameter[] parm = new SqlParameter[2];
                parm[0] = new SqlParameter("@prcdID", prcdID);
                parm[1] = new SqlParameter("@prcdItemID", prcdItemID);

                _dataset = SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName, parm);

                return _dataset;
            }
            catch
            {
                return null;
            }
            finally
            {
                _dataset.Dispose();
            }
        }

        /// <summary>
        /// Add a Process Item
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="workorder"></param>
        /// <param name="defID"></param>
        /// <param name="defNum"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool AddProcessItem_New(int prcID, int typeID, int prcdID, int total, string defID, string num, bool bPro, bool bDef)
        {
            try
            {
                string strName = "sp_QC_AddProcessItem_New";
                SqlParameter[] parm = new SqlParameter[8];
                parm[0] = new SqlParameter("@prcID", prcID);
                parm[1] = new SqlParameter("@typeID", typeID);
                parm[2] = new SqlParameter("@prcdID", prcdID);
                parm[3] = new SqlParameter("@total", total);
                parm[4] = new SqlParameter("@defID", defID);
                parm[5] = new SqlParameter("@num", num);
                parm[6] = new SqlParameter("@bPro", bPro);
                parm[7] = new SqlParameter("@bDef", bDef);

                int nRet = SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strName, parm);

                if (nRet == -1)
                    return false;
                else
                    return true;
            }
            catch (Exception ee)
            {
                return false;
            }
        }

        public int UpdateProcessByID_New(int prcID, int total, string inspector, string checkDate)
        {
            try
            {
                string strName = "sp_QC_UpdateProcessByID_New";
                SqlParameter[] parm = new SqlParameter[5];
                parm[0] = new SqlParameter("@prcID", prcID);
                parm[1] = new SqlParameter("@total", total);
                parm[2] = new SqlParameter("@inspector", inspector);
                parm[3] = new SqlParameter("@checkDate", checkDate);
                parm[4] = new SqlParameter("@returnVal", SqlDbType.Int);
                parm[4].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strName, parm);

                return int.Parse(parm[4].Value.ToString());
            }
            catch
            {
                return -1;
            }
        }



        #endregion

        #region 过程检验
        public void DynamicBindgvProcess(DataTable dt, GridView gv)
        {
            gv.Columns.Clear();

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Columns.Count - 1; i++)
                {
                    TemplateField tf = new TemplateField();
                    tf.HeaderText = dt.Columns[i].Caption.ToString().Trim();
                    if (i < 1)
                    {
                        tf.HeaderStyle.Width = Unit.Pixel(50);
                        tf.ItemTemplate = new GridViewTemplate("Label", dt.Columns[i].ColumnName.ToString(), DataControlRowType.DataRow);
                        tf.EditItemTemplate = new GridViewTemplate("TextBox", dt.Columns[i].ColumnName.ToString(), DataControlRowType.DataRow);
                    }
                    else
                    {
                        tf.ItemTemplate = new GridViewTemplate("CheckBox", dt.Columns[i].ColumnName.ToString(), DataControlRowType.DataRow);
                        //tf.EditItemTemplate = new GridViewTemplate("CheckBox", dt.Columns[i].ColumnName.ToString(), DataControlRowType.DataRow);
                        tf.EditItemTemplate = new GridViewTemplate("chk_txt", dt.Columns[i].ColumnName.ToString(), DataControlRowType.DataRow);
                    }
                    tf.ItemStyle.HorizontalAlign = HorizontalAlign.Center;

                    gv.Columns.Add(tf);
                }

                CommandField cfEdit = new CommandField();
                cfEdit.ButtonType = ButtonType.Link;
                cfEdit.ShowEditButton = true;
                cfEdit.EditText = "编辑";
                cfEdit.UpdateText = "更新";
                cfEdit.CancelText = "取消";
                cfEdit.CausesValidation = false;
                cfEdit.HeaderStyle.Width = Unit.Pixel(90);
                cfEdit.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                gv.Columns.Add(cfEdit);

                CommandField cfDel = new CommandField();
                cfDel.ButtonType = ButtonType.Link;
                cfDel.DeleteText = "<SPAN onclick=\"return confirm('你确定要删除吗?');\">删除</SPAN>";
                cfDel.ShowDeleteButton = true;
                cfDel.CausesValidation = false;
                cfDel.HeaderStyle.Width = Unit.Pixel(50);
                cfDel.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                gv.Columns.Add(cfDel);

                gv.DataSource = dt;
                gv.DataBind();
            }
            else 
            {
                gv.DataSource = null;
                gv.DataBind();
            }
        }
        /// <summary>
        /// 获取过程检验单
        /// </summary>
        /// <returns></returns>
        public DataSet GetProcess(string order, string line, string wo_part, string vent, string lot, string part, string group
                                , string date1, string date2, string lineMgt, int PageSize, int PageIndex)
        {
            try
            {
                string strName = "sp_QC_GetProcess";
                SqlParameter[] parm = new SqlParameter[12];
                parm[0] = new SqlParameter("@order", order);
                parm[1] = new SqlParameter("@line", line);
                parm[2] = new SqlParameter("@wo_part", wo_part);
                parm[3] = new SqlParameter("@vent", vent);
                parm[4] = new SqlParameter("@lot", lot);
                parm[5] = new SqlParameter("@part", part);
                parm[6] = new SqlParameter("@group", group);
                parm[7] = new SqlParameter("@date1", date1);
                parm[8] = new SqlParameter("@date2", date2);
                parm[9] = new SqlParameter("@lineMgt", lineMgt);
                parm[10] = new SqlParameter("@PageSize", PageSize);
                parm[11] = new SqlParameter("@PageIndex", PageIndex);

                return _dataset = SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName, parm);
            }
            catch
            {
                return null;
            }
            finally
            {
                _dataset.Dispose();
            }
        }

        public DataTable GetProcessDefectType(String uID)
        {
            try
            {
                string strName = "sp_QC_GetProcessDefectType";
                SqlParameter[] parm = new SqlParameter[1];
                parm[0] = new SqlParameter("@uID", uID);

                _dataset = SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName, parm);

                return _dataset.Tables[0];
            }
            catch
            {
                return null;
            }
            finally
            {
                _dataset.Dispose();
            }
        }

        public int GetProcessDefectTypeById(string prcId)
        {
            string strName = "sp_QC_GetProcessDefectTypeById";
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@prcId", prcId);
            int typeId = (int)SqlHelper.ExecuteScalar(adam.dsnx(), CommandType.StoredProcedure, strName, parm);
            return typeId;
        }

        public String GetProcessLot(String WorkOrder, String part)
        {
            try
            {
                string strName = "sp_QC_GetProcessLot";
                SqlParameter[] parm = new SqlParameter[2];
                parm[0] = new SqlParameter("@WorkOrder", WorkOrder);
                parm[1] = new SqlParameter("@part", part);

                _dataset = SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName, parm);

                return _dataset.Tables[0].Rows[0][0].ToString().Trim();
            }
            catch
            {
                return String.Empty;
            }
            finally
            {
                _dataset.Dispose();
            }
        }

        public DataTable GetProcessItem(int prcID, int tID)
        {
            try
            {
                string strName = "sp_QC_GetProcessItem";
                SqlParameter[] parm = new SqlParameter[2];
                parm[0] = new SqlParameter("@prcID", prcID);
                parm[1] = new SqlParameter("@tID", tID);

                _dataset = SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName, parm);

                return _dataset.Tables[0];
            }
            catch
            {
                return null;
            }
            finally
            {
                _dataset.Dispose();
            }
        }
        /// <summary>
        /// 获取过程检验的某些数据
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="WorkOrder"></param>
        /// <returns></returns>
        public  DataTable GetProcessSnatch(string Line, string WorkOrder,string domain)
        {
            try
            {
                string strName = "sp_QC_GetProcessSnatch";
                SqlParameter[] parm = new SqlParameter[3];
                parm[0] = new SqlParameter("@WorkOrder", WorkOrder);
                parm[1] = new SqlParameter("@Line", Line);
                parm[2] = new SqlParameter("@domain", domain);

                _dataset = SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName, parm);

                return _dataset.Tables[0];
            }
            catch
            {
                return null;
            }
            finally
            {
                _dataset.Dispose();
            }
        }

        public DataTable GetProcessByID(int prcID)
        {
            try
            {
                string strName = "sp_QC_GetProcessByID";
                SqlParameter[] parm = new SqlParameter[1];
                parm[0] = new SqlParameter("@prcID", prcID);

                _dataset = SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName, parm);

                return _dataset.Tables[0];
            }
            catch
            {
                return null;
            }
            finally
            {
                _dataset.Dispose();
            }
        }

        public int UpdateProcessByID(int prcID,int total,string inspector,string checkDate)
        {
            try
            {
                string strName = "sp_QC_UpdateProcessByID";
                SqlParameter[] parm = new SqlParameter[5];
                parm[0] = new SqlParameter("@prcID", prcID);
                parm[1] = new SqlParameter("@total", total);
                parm[2] = new SqlParameter("@inspector", inspector);
                parm[3] = new SqlParameter("@checkDate", checkDate);
                parm[4] = new SqlParameter("@returnVal",SqlDbType.Int);
                parm[4].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strName, parm);

                return int.Parse(parm[4].Value.ToString());
            }
            catch
            {
                return -1;
            }
        }

        /// <summary>
        /// 获取过程检验项目
        /// </summary>
        /// <returns></returns>
        public DataSet GetProcessDefect(int prcdID,int prcdItemID)
        {
            try
            {
                string strName = "sp_QC_GetProcessDefect";
                SqlParameter[] parm = new SqlParameter[2];
                parm[0] = new SqlParameter("@prcdID", prcdID);
                parm[1] = new SqlParameter("@prcdItemID", prcdItemID);

                _dataset = SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName, parm);

                return _dataset;
            }
            catch
            {
                return null;
            }
            finally
            {
                _dataset.Dispose();
            }
        }

        public DataTable GetProcedure(string name ,int tID)
        {
            try
            {
                string strName = "sp_QC_GetProcedure";
                SqlParameter[] parm = new SqlParameter[2];
                parm[0] = new SqlParameter("@name", name);
                parm[1] = new SqlParameter("@tID", tID);

                _dataset = SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName, parm);

                return _dataset.Tables[0];
            }
            catch
            {
                return null;
            }
            finally
            {
                _dataset.Dispose();
            }
        }

        /// <summary>
        /// insert a new record or process
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="workorder"></param>
        /// <param name="provider"></param>
        /// <param name="no"></param>
        /// <param name="process"></param>
        /// <param name="nIn"></param>
        /// <param name="nOut"></param>
        /// <param name="part"></param>
        /// <param name="person"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public FuncErrType AddProcess(string ID, string workorder, string provider, string no, int nIn, int nOut, string part
                                        , int person, string remarks, string workgroup, string lineMgt)
        {
            int returnVal;

            try
            {
                string strName = "sp_QC_AddProcess";
                SqlParameter[] parm = new SqlParameter[12];
                parm[0] = new SqlParameter("@ID", ID);
                parm[1] = new SqlParameter("@workorder", workorder);
                parm[2] = new SqlParameter("@provider", provider);
                parm[3] = new SqlParameter("@no", no);
                parm[4] = new SqlParameter("@nIn", nIn);
                parm[5] = new SqlParameter("@nOut", nOut);
                parm[6] = new SqlParameter("@part", part);
                parm[7] = new SqlParameter("@person", person);
                parm[8] = new SqlParameter("@remarks", remarks);
                parm[9] = new SqlParameter("@workgroup", workgroup);
                parm[10] = new SqlParameter("@lineMgt", lineMgt);
                parm[11] = new SqlParameter("@returnVal", SqlDbType.Int);
                parm[11].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strName, parm);

                returnVal = int.Parse(parm[11].Value.ToString());
            }
            catch
            {
                returnVal = -1;
            }
            return MyErrorCollection.GetProcedureErrInfo(returnVal);
        }
        /// <summary>
        /// when inserting processitems,complete the process infomation
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="workorder"></param>
        /// <param name="total"></param>
        /// <param name="self"></param>
        /// <param name="repair"></param>
        /// <param name="date"></param>
        /// <param name="inspector"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public  FuncErrType AddProcessSnatch(string ID, string workorder,string date,string inspector) 
        {
            int returnVal = -1;

            try
            {
                string strName = "sp_QC_AddProcessSnatch";
                SqlParameter[] parm = new SqlParameter[5];
                parm[0] = new SqlParameter("@ID", ID);
                parm[1] = new SqlParameter("@workorder", workorder);
                parm[2] = new SqlParameter("@date", date);
                parm[3] = new SqlParameter("@inspector", inspector);
                parm[4] = new SqlParameter("@returnVal", SqlDbType.Int);
                parm[4].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strName, parm);

                returnVal = int.Parse(parm[4].Value.ToString());
            }
            catch (Exception ee)
            {
                returnVal = -1;
            }
            return MyErrorCollection.GetProcedureErrInfo(returnVal);
        }
        /// <summary>
        /// Add a Process Item
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="workorder"></param>
        /// <param name="defID"></param>
        /// <param name="defNum"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool AddProcessItem(int prcID, int typeID, int prcdID, int total, string defID, string num, bool bPro,bool bDef) 
        {
            try
            {
                string strName = "sp_QC_AddProcessItem";
                SqlParameter[] parm = new SqlParameter[8];
                parm[0] = new SqlParameter("@prcID", prcID);
                parm[1] = new SqlParameter("@typeID", typeID);
                parm[2] = new SqlParameter("@prcdID", prcdID);
                parm[3] = new SqlParameter("@total", total);
                parm[4] = new SqlParameter("@defID", defID);
                parm[5] = new SqlParameter("@num", num);
                parm[6] = new SqlParameter("@bPro", bPro);
                parm[7] = new SqlParameter("@bDef", bDef);

                int nRet = SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strName, parm);

                if (nRet == -1)
                    return false;
                else
                    return true;
            }
            catch (Exception ee)
            {
                return false;
            }
        }
        /// <summary>
        /// Add a process defect
        /// </summary>
        /// <param name="person"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public FuncErrType AddProcessDefect(string prcID, string defID, string Identity) 
        {
            int returnVal;

            try
            {
                string strName = "sp_QC_AddProcessDefect";
                SqlParameter[] parm = new SqlParameter[3];
                parm[0] = new SqlParameter("@prcID", prcID);
                parm[1] = new SqlParameter("@defID", defID);
                parm[2] = new SqlParameter("@Identity", Identity);

                SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strName, parm);

                returnVal = 0;
            }
            catch
            {
                returnVal = -1;
            }
            return MyErrorCollection.GetProcedureErrInfo(returnVal);
        }
        /// <summary>
        /// 增加一个工序
        /// </summary>
        /// <param name="person"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public  FuncErrType AddProcedure(string name,int tID,int person) 
        {
            int returnVal;

            try
            {
                string strName = "sp_QC_AddProcedure";
                SqlParameter[] parm = new SqlParameter[4];
                parm[0] = new SqlParameter("@name", name);
                parm[1] = new SqlParameter("@tID", tID);
                parm[2] = new SqlParameter("@person", person);
                parm[3] = new SqlParameter("@returnVal", SqlDbType.Int);
                parm[3].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strName, parm);

                returnVal = int.Parse(parm[3].Value.ToString());
            }
            catch (Exception ee)
            {
                returnVal = -1;
            }
            return MyErrorCollection.GetProcedureErrInfo(returnVal);
        }
        public FuncErrType ModifyProcess(string prcID, string provider, string no, string nIn, string nOut, string part, int person, string remarks, string workgroup, string lineMgt)
        {
            int returnVal;

            try
            {
                string strName = "sp_QC_ModifyProcess";
                SqlParameter[] parm = new SqlParameter[11];
                parm[0] = new SqlParameter("@prcID", prcID);
                parm[1] = new SqlParameter("@provider", provider);
                parm[2] = new SqlParameter("@no", no);
                parm[3] = new SqlParameter("@nIn", nIn);
                parm[4] = new SqlParameter("@nOut", nOut);
                parm[5] = new SqlParameter("@part", part);
                parm[6] = new SqlParameter("@person", person);
                parm[7] = new SqlParameter("@remarks", remarks);
                parm[8] = new SqlParameter("@workgroup", workgroup);
                parm[9] = new SqlParameter("@lineMgt", lineMgt);
                parm[10] = new SqlParameter("@returnVal", SqlDbType.Int);
                parm[10].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strName, parm);

                returnVal = int.Parse(parm[10].Value.ToString());
            }
            catch
            {
                returnVal = -1;
            }

            return MyErrorCollection.GetProcedureErrInfo(returnVal);
        }        /// <summary>
        /// 更新工序
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="name"></param>
        /// <param name="person"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public  FuncErrType ModifyProcedure(int ID,int tID,string name,int person,int order)
        {
            int returnVal;

            try
            {
                string strName = "sp_QC_ModifyProcedure";
                SqlParameter[] parm = new SqlParameter[6];
                parm[0] = new SqlParameter("@ID", ID);
                parm[1] = new SqlParameter("@tID", tID);
                parm[2] = new SqlParameter("@name", name);
                parm[3] = new SqlParameter("@person", person);
                parm[4] = new SqlParameter("@order", order);
                parm[5] = new SqlParameter("@returnVal", SqlDbType.Int);
                parm[5].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strName, parm);

                returnVal = int.Parse(parm[5].Value.ToString());
            }
            catch (Exception ee)
            {
                returnVal = -1;
            }
            return MyErrorCollection.GetProcedureErrInfo(returnVal);
        }
        public FuncErrType ModifyProcessDefect(string prcID, string OldNum, string NewNum, string defID, string dName, string Identity)
        {
            int returnVal = 0;

            try
            {
                string strName = "sp_QC_ModifyProcessDefect";
                SqlParameter[] parm = new SqlParameter[6];
                parm[0] = new SqlParameter("@prcID", prcID);
                parm[1] = new SqlParameter("@OldNum", OldNum);
                parm[2] = new SqlParameter("@NewNum", NewNum);
                parm[3] = new SqlParameter("@defID", defID);
                parm[4] = new SqlParameter("@dName", dName);
                parm[5] = new SqlParameter("@Identity", Identity);

                SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strName, parm);
            }
            catch
            {
                returnVal = -1;
            }
            return MyErrorCollection.GetProcedureErrInfo(returnVal);
        }
        public  FuncErrType DeleteProcess(int ID, int uID, ref string msg)
        {
            int returnVal;

            try
            {
                string strName = "sp_QC_DeleteProcess";
                SqlParameter[] parm = new SqlParameter[2];
                parm[0] = new SqlParameter("@ID", ID);
                parm[1] = new SqlParameter("@uID", uID);

                SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strName, parm);

                returnVal = 0;
            }
            catch (Exception ee)
            {
                msg = "数据库操作失败!原因:" + ee.ToString();
                returnVal = -1;
            }
            return MyErrorCollection.GetProcedureErrInfo(returnVal);
        }
        /// <summary>
        /// 删除工序
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public  FuncErrType DeleteProcedure(int ID, ref string msg)
        {
            int returnVal;

            try
            {
                string strName = "sp_QC_DeleteProcedure";
                SqlParameter parm = new SqlParameter("@ID", ID);

                SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strName, parm);

                returnVal = 0;
            }
            catch (Exception ee)
            {
                msg = "数据库操作失败!原因:" + ee.ToString();
                returnVal = -1;
            }
            return MyErrorCollection.GetProcedureErrInfo(returnVal);
        }
        /// <summary>
        /// delete a process defect
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public  FuncErrType DeleteProcessDefect(string prcID, string num,string dName,string Identity)
        {
            int returnVal;

            try
            {
                string strName = "sp_QC_DeleteProcessDefect";
                SqlParameter[] parm = new SqlParameter[4];
                parm[0] = new SqlParameter("@prcID", prcID);
                parm[1] = new SqlParameter("@num", num);
                parm[2] = new SqlParameter("@dName", dName);
                parm[3] = new SqlParameter("@Identity", Identity);

                SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strName, parm);

                returnVal = 0;
            }
            catch (Exception ee)
            {
                returnVal = -1;
            }
            return MyErrorCollection.GetProcedureErrInfo(returnVal);
        }
        #endregion

        #region 成品检验

        public Boolean TcpLimited(String uID)
        {
            try
            {
                string strName = "sp_QC_SetTcpLimited";
                SqlParameter[] parm = new SqlParameter[1];
                parm[0] = new SqlParameter("@uID", uID);

                return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsnx(), CommandType.StoredProcedure, strName, parm));
            }
            catch
            {
                return false;
            }
        }

        public Boolean OutLimited(String uID)
        {
            try
            {
                string strName = "sp_QC_SetOutLimited";
                SqlParameter[] parm = new SqlParameter[1];
                parm[0] = new SqlParameter("@uID", uID);

                return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsnx(), CommandType.StoredProcedure, strName, parm));
            }
            catch
            {
                return false;
            }
        }

        public DataSet GetProduct(string order, string line, string board, string date, string insp, string period, string vent, 
                                  string examer, bool tcp, string date1, string date2, int PageSize, int PageIndex, bool outer,bool free)
        {
            try
            {
                string strName = "sp_QC_GetProduct";
                SqlParameter[] parm = new SqlParameter[15];
                parm[0] = new SqlParameter("@order", order);
                parm[1] = new SqlParameter("@line", line);
                parm[2] = new SqlParameter("@board", board);
                parm[3] = new SqlParameter("@date", date);
                parm[4] = new SqlParameter("@insp", insp);
                parm[5] = new SqlParameter("@period", period);
                parm[6] = new SqlParameter("@vent", vent);
                parm[7] = new SqlParameter("@examer", examer);
                parm[8] = new SqlParameter("@tcp", tcp);
                parm[9] = new SqlParameter("@date1", date1);
                parm[10] = new SqlParameter("@date2", date2);
                parm[11] = new SqlParameter("@PageSize", PageSize);
                parm[12] = new SqlParameter("@PageIndex", PageIndex);
                parm[13] = new SqlParameter("@out", outer);
                parm[14] = new SqlParameter("@isFree", free);

                return _dataset = SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName, parm);
            }
            catch
            {
                return null;
            }
            finally
            {
                _dataset.Dispose();
            }
        }

        public DataSet GetProductUnfinished(string order, string line, string date1, string date2, int plantid)
        {
            try
            {
                string strName = "sp_QC_GetProductUnfinished";
                SqlParameter[] parm = new SqlParameter[5];
                parm[0] = new SqlParameter("@order", order);
                parm[1] = new SqlParameter("@line", line);
                parm[2] = new SqlParameter("@date1", date1);
                parm[3] = new SqlParameter("@date2", date2);
                parm[4] = new SqlParameter("@plantid", plantid);

                return _dataset = SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName, parm);
            }
            catch
            {
                return null;
            }
            finally
            {
                _dataset.Dispose();
            }
        }

        public DataSet GetProcessUnfinished(string order, string line, string date1, string date2, int plantid)
        {
            try
            {
                string strName = "sp_QC_GetProcessUnfinished";
                SqlParameter[] parm = new SqlParameter[5];
                parm[0] = new SqlParameter("@order", order);
                parm[1] = new SqlParameter("@line", line);
                parm[2] = new SqlParameter("@date1", date1);
                parm[3] = new SqlParameter("@date2", date2);
                parm[4] = new SqlParameter("@plantid", plantid);

                return _dataset = SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName, parm);
            }
            catch
            {
                return null;
            }
            finally
            {
                _dataset.Dispose();
            }
        }

        public DataTable GetInspectModule(int type)
        {
            try
            {
                string strName = "sp_QC_GetInspectModule";
                SqlParameter[] parm = new SqlParameter[1];
                parm[0] = new SqlParameter("@type", type);

                _dataset = SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName, parm);

                return _dataset.Tables[0];
            }
            catch
            {
                return null;
            }
            finally
            {
                _dataset.Dispose();
            }
        }
        public FuncErrType GetProductSnatch(string line, string order, ref DataTable table)
        {
            int returnVal;

            try
            {
                string strName = "sp_QC_GetProductSnatch";
                SqlParameter[] parm = new SqlParameter[2];
                parm[0] = new SqlParameter("@line", line);
                parm[1] = new SqlParameter("@WorkOrder", order);

                _dataset = SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName, parm);

                table = _dataset.Tables[0];

                returnVal = int.Parse(_dataset.Tables[0].Rows[0][0].ToString());
            }
            catch
            {
                table = null;
                returnVal = -1;
            }
            return MyErrorCollection.GetProcedureErrInfo(returnVal); 
        }

        public DataTable GetProductDefectType(String uID)
        {
            try
            {
                string strName = "sp_QC_GetProductDefectType";
                SqlParameter[] parm = new SqlParameter[1];
                parm[0] = new SqlParameter("@uID", uID);

                _dataset = SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName, parm);

                return _dataset.Tables[0];
            }
            catch
            {
                return null;
            }
            finally
            {
                _dataset.Dispose();
            }
        }

        public int GetProductDefectTypeById(string prdId)
        {
            string strName = "sp_QC_GetProductDefectTypeById";
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@prdId", prdId);
            int typeId = (int)SqlHelper.ExecuteScalar(adam.dsnx(), CommandType.StoredProcedure, strName, parm);
            return typeId;
        }

        public DataTable GetProductItem(int prdID ,int typeID)
        {
            try
            {
                string strName = "sp_QC_GetProductItem";
                SqlParameter[] parm = new SqlParameter[2];
                parm[0] = new SqlParameter("@prdID", prdID);
                parm[1] = new SqlParameter("@typeID", typeID);

                _dataset = SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName, parm);

                return _dataset.Tables[0];
            }
            catch
            {
                return null;
            }
            finally
            {
                _dataset.Dispose();
            }
        }

        public void SetDefectItemTcpValue(int dItemID, Boolean isChecked)
        {
            try
            {
                string strName = "sp_QC_SetDefectItemTcpValue";
                SqlParameter[] parm = new SqlParameter[2];
                parm[0] = new SqlParameter("@dItemID", dItemID);
                parm[1] = new SqlParameter("@isChecked", isChecked);

                SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strName, parm);
            }
            catch
            {
                ;
            }
        }

        public DataTable GetDefectItem(int defID,int pID)
        {
            try
            {
                string strName = "sp_QC_GetDefectItem";
                SqlParameter[] parm = new SqlParameter[2];
                parm[0] = new SqlParameter("@defID", defID);
                parm[1] = new SqlParameter("@pID", pID);

                _dataset = SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName, parm);

                return _dataset.Tables[0];
            }
            catch
            {
                return null;
            }
            finally
            {
                _dataset.Dispose();
            }
        }
        public DataSet GetProductDefect(int defID, int prdItemID)
        {
            try
            {
                string strName = "sp_QC_GetProductDefect";
                SqlParameter[] parm = new SqlParameter[2];
                parm[0] = new SqlParameter("@defID", defID);
                parm[1] = new SqlParameter("@prdItemID", prdItemID);

                _dataset = SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName, parm);

                return _dataset;
            }
            catch
            {
                return null;
            }
            finally
            {
                _dataset.Dispose();
            }
        }
        public DataTable GetProductDefectItem(int defID)
        {
            try
            {
                string strName = "sp_QC_GetProductDefectItem";
                SqlParameter parm = new SqlParameter("@defID", defID);

                _dataset = SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName, parm);

                return _dataset.Tables[0];
            }
            catch
            {
                return null;
            }
            finally
            {
                _dataset.Dispose();
            }
        }
        public DataTable GetProductDefectItem(string itemID)
        {
            try
            {
                string strName = "sp_QC_GetProductDefectItem";
                SqlParameter parm = new SqlParameter("@itemID", itemID);

                _dataset = SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName, parm);

                return _dataset.Tables[0];
            }
            catch
            {
                return null;
            }
            finally
            {
                _dataset.Dispose();
            }
        }
        public FuncErrType AddProduct(string floor, string board, string date, string inspector,string Examiner, string qad, string id, string order
                                    , string period, int num, string guest, bool tcp, int person, string remarks, string lineMgt, string lineNo, bool outer
                                    , string ProcessMgt)
         {
            int returnVal;

            try
            {
                string strName = "sp_QC_AddProduct";
                SqlParameter[] parm = new SqlParameter[19];
                parm[0] = new SqlParameter("@floor", floor);
                parm[1] = new SqlParameter("@board", board);
                parm[2] = new SqlParameter("@date", date);
                parm[3] = new SqlParameter("@inspector", inspector);
                parm[4] = new SqlParameter("@Examiner", Examiner);
                parm[5] = new SqlParameter("@qad", qad);
                parm[6] = new SqlParameter("@id", id);
                parm[7] = new SqlParameter("@order", order);
                parm[8] = new SqlParameter("@period", period);
                parm[9] = new SqlParameter("@num", num);
                parm[10] = new SqlParameter("@guest", guest);
                parm[11] = new SqlParameter("@tcp", tcp);
                parm[12] = new SqlParameter("@remarks", remarks);
                parm[13] = new SqlParameter("@person", person);
                parm[14] = new SqlParameter("@lineMgt", lineMgt);
                parm[15] = new SqlParameter("@lineNo", lineNo);
                parm[16] = new SqlParameter("@out", outer);
                parm[17] = new SqlParameter("@processMgt", ProcessMgt);

                parm[18] = new SqlParameter("@returnVal", SqlDbType.Int);
                parm[18].Direction = ParameterDirection.Output;
               

                SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strName, parm);

                returnVal = int.Parse(parm[18].Value.ToString());
            }
            catch
            {
                
                returnVal = -1;
            }

            return MyErrorCollection.GetProcedureErrInfo(returnVal);
        }

        /// <summary>
        /// 重载AddProduct方法，添加isFree，是否免检
        /// </summary>
        /// <param name="floor"></param>
        /// <param name="board"></param>
        /// <param name="date"></param>
        /// <param name="inspector"></param>
        /// <param name="Examiner"></param>
        /// <param name="qad"></param>
        /// <param name="id"></param>
        /// <param name="order"></param>
        /// <param name="period"></param>
        /// <param name="num"></param>
        /// <param name="guest"></param>
        /// <param name="tcp"></param>
        /// <param name="person"></param>
        /// <param name="remarks"></param>
        /// <param name="lineMgt"></param>
        /// <param name="lineNo"></param>
        /// <param name="outer"></param>
        /// <param name="ProcessMgt"></param>
        /// <param name="isFree">是否免检</param>
        /// <returns></returns>
        public FuncErrType AddProduct(string floor, string board, string date, string inspector, string Examiner, string qad, string id, string order
                            , string period, int num, string guest, bool tcp, int person, string remarks, string lineMgt, string lineNo, bool outer
                            , string ProcessMgt ,bool isFree)
        {
            int returnVal;

            try
            {
                string strName = "sp_QC_AddProduct";
                SqlParameter[] parm = new SqlParameter[20];
                parm[0] = new SqlParameter("@floor", floor);
                parm[1] = new SqlParameter("@board", board);
                parm[2] = new SqlParameter("@date", date);
                parm[3] = new SqlParameter("@inspector", inspector);
                parm[4] = new SqlParameter("@Examiner", Examiner);
                parm[5] = new SqlParameter("@qad", qad);
                parm[6] = new SqlParameter("@id", id);
                parm[7] = new SqlParameter("@order", order);
                parm[8] = new SqlParameter("@period", period);
                parm[9] = new SqlParameter("@num", num);
                parm[10] = new SqlParameter("@guest", guest);
                parm[11] = new SqlParameter("@tcp", tcp);
                parm[12] = new SqlParameter("@remarks", remarks);
                parm[13] = new SqlParameter("@person", person);
                parm[14] = new SqlParameter("@lineMgt", lineMgt);
                parm[15] = new SqlParameter("@lineNo", lineNo);
                parm[16] = new SqlParameter("@out", outer);
                parm[17] = new SqlParameter("@processMgt", ProcessMgt);

                parm[18] = new SqlParameter("@returnVal", SqlDbType.Int);
                parm[18].Direction = ParameterDirection.Output;
                parm[19] = new SqlParameter("@isFree", isFree);

                SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strName, parm);

                returnVal = int.Parse(parm[18].Value.ToString());
            }
            catch
            {

                returnVal = -1;
            }

            return MyErrorCollection.GetProcedureErrInfo(returnVal);
        }


        public bool AddProductItem(int prdID, int typeID, int defID, string Level, decimal Aql, string defItemID, string num, bool bPro, bool bDef, int qty, int total) 
        {
            try
            {
                string strName = "sp_QC_AddProductItem";
                SqlParameter[] parm = new SqlParameter[11];
                parm[0] = new SqlParameter("@prdID", prdID);
                parm[1] = new SqlParameter("@typeID", typeID);
                parm[2] = new SqlParameter("@defID", defID);
                parm[3] = new SqlParameter("@Level", Level);
                parm[4] = new SqlParameter("@Aql", Aql);
                parm[5] = new SqlParameter("@defItemID", defItemID);
                parm[6] = new SqlParameter("@num", num);
                parm[7] = new SqlParameter("@bPro", bPro);
                parm[8] = new SqlParameter("@bDef", bDef);
                parm[9] = new SqlParameter("@qty", qty);
                parm[10] = new SqlParameter("@total", total);

                int nRet = SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strName, parm);

                if (nRet == -1)
                    return false;
                else
                    return true;
            }
            catch (Exception ee)
            {
                return false;
            }
        }

        public string GetCP100(string id)
        {
            try
            {
                string strName = "sp_QC_selectCP100";
                SqlParameter[] parm = new SqlParameter[1];
                parm[0] = new SqlParameter("@id", id);

                return SqlHelper.ExecuteScalar(adam.dsnx(), CommandType.StoredProcedure, strName, parm).ToString();
            }
            catch
            {
                return "0";
            }
        }

        public bool SaveCP100(string id, string nbr, string lot, string typeID, string seriers, int num, string uName, bool tcp)
        {
            try
            {
                string strName = "sp_QC_saveCP100";
                SqlParameter[] parm = new SqlParameter[8];
                parm[0] = new SqlParameter("@id", id);
                parm[1] = new SqlParameter("@nbr", nbr);
                parm[2] = new SqlParameter("@lot", lot);
                parm[3] = new SqlParameter("@typeID", typeID);
                parm[4] = new SqlParameter("@seriers", seriers);
                parm[5] = new SqlParameter("@num", num);
                parm[6] = new SqlParameter("@uName", uName);
                parm[7] = new SqlParameter("@tcp", tcp);

                SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strName, parm);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public FuncErrType AddProductItemDefect(string prdID, string dItemID, string num, ref string msg)
        {
            int returnVal;

            try
            {
                string strName = "sp_QC_AddProductItemDefect";
                SqlParameter[] parm = new SqlParameter[3];
                parm[0] = new SqlParameter("@prdID", prdID);
                parm[1] = new SqlParameter("@dItemID", dItemID);
                parm[2] = new SqlParameter("@num", num);

                SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strName, parm);

                returnVal = 0;
            }
            catch (Exception ee)
            {
                msg = "数据库操作失败!原因:" + ee.ToString();
                returnVal = -1;
            }
            return MyErrorCollection.GetProcedureErrInfo(returnVal);
        }
        public FuncErrType AddProductDefect(int person, ref string msg)
        {
            int returnVal;

            try
            {
                string strName = "sp_QC_AddProductDefect";
                SqlParameter parm = new SqlParameter("@person", person);

                SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strName, parm);

                returnVal = 0;
            }
            catch (Exception ee)
            {
                msg = "数据库操作失败!原因:" + ee.ToString();
                returnVal = -1;
            }
            return MyErrorCollection.GetProcedureErrInfo(returnVal);
        }
        public FuncErrType AddProductDefectItem(string defName, int person, ref string msg)
        {
            int returnVal;

            try
            {
                string strName = "sp_QC_AddProductDefectItem";
                SqlParameter[] parm = new SqlParameter[2];
                parm[0] = new SqlParameter("@defName", defName);
                parm[1] = new SqlParameter("@person", person);

                SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strName, parm);

                returnVal = 0;
            }
            catch (Exception ee)
            {
                msg = "数据库操作失败!原因:" + ee.ToString();
                returnVal = -1;
            }
            return MyErrorCollection.GetProcedureErrInfo(returnVal);
        }


        public DataTable GetDefect(int pID, int tID, string Name)
        {
            try
            {
                string strName = "sp_QC_GetDefect";
                SqlParameter[] parm = new SqlParameter[3];
                parm[0] = new SqlParameter("@pID", pID);
                parm[1] = new SqlParameter("@tID", tID);
                parm[2] = new SqlParameter("@Name", Name);

                _dataset = SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName, parm);

                return _dataset.Tables[0];
            }
            catch
            {
                return null;
            }
            finally
            {
                _dataset.Dispose();
            }
        }

        public DataTable GetDefectType(string tName, int module)
        {
            try
            {
                string strName = "sp_QC_GetDefectType";
                SqlParameter[] parm = new SqlParameter[2];
                parm[0] = new SqlParameter("@tName", tName);
                parm[1] = new SqlParameter("@module", module);

                _dataset = SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName, parm);

                return _dataset.Tables[0];
            }
            catch
            {
                return null;
            }
            finally
            {
                _dataset.Dispose();
            }
        }

        public FuncErrType AddDefect(string name, int tID,int person)
        {
            int returnVal;

            try
            {
                string strName = "sp_QC_AddDefect";
                SqlParameter[] parm = new SqlParameter[4];
                parm[0] = new SqlParameter("@name", name);
                parm[1] = new SqlParameter("@tID", tID);
                parm[2] = new SqlParameter("@person", person);
                parm[3] = new SqlParameter("@returnVal", SqlDbType.Int);
                parm[3].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strName, parm);

                returnVal = int.Parse(parm[3].Value.ToString());
            }
            catch (Exception ee)
            {
                returnVal = -1;
            }
            return MyErrorCollection.GetProcedureErrInfo(returnVal);
        }

        public FuncErrType AddDefectItem(string defID,int pID,string name, int person)
        {
            int returnVal;

            try
            {
                string strName = "sp_QC_AddDefectItem";
                SqlParameter[] parm = new SqlParameter[5];
                parm[0] = new SqlParameter("@defID", defID);
                parm[1] = new SqlParameter("@pID", pID);
                parm[2] = new SqlParameter("@name", name);
                parm[3] = new SqlParameter("@person", person);
                parm[4] = new SqlParameter("@returnVal", SqlDbType.Int);
                parm[4].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strName, parm);

                returnVal = int.Parse(parm[4].Value.ToString());
            }
            catch (Exception ee)
            {
                returnVal = -1;
            }
            return MyErrorCollection.GetProcedureErrInfo(returnVal);
        }

        public FuncErrType AddDefectType(string name,int pID,int person)
        {
            int returnVal;

            try
            {
                string strName = "sp_QC_AddDefectType";
                SqlParameter[] parm = new SqlParameter[4];
                parm[0] = new SqlParameter("name", name);
                parm[1] = new SqlParameter("@pID", pID);
                parm[2] = new SqlParameter("@person", person);
                parm[3] = new SqlParameter("@returnVal",SqlDbType.Int);
                parm[3].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strName, parm);

                returnVal = int.Parse(parm[3].Value.ToString());
            }
            catch
            {
                returnVal = -1;
            }
            return MyErrorCollection.GetProcedureErrInfo(returnVal);
        }

        public FuncErrType ModifyProduct(string prdID, string board, string date, string inspector, string examiner, string period, string num, string guest, int person, string remarks)
        {
            int returnVal;

            try
            {
                string strName = "sp_QC_ModifyProduct";
                SqlParameter[] parm = new SqlParameter[11];
                parm[0] = new SqlParameter("@prdID", prdID);
                parm[1] = new SqlParameter("@board", board);
                parm[2] = new SqlParameter("@date", date);
                parm[3] = new SqlParameter("@inspector", inspector);
                parm[4] = new SqlParameter("@examiner", examiner);
                parm[5] = new SqlParameter("@period", period);
                parm[6] = new SqlParameter("@num", num);
                parm[7] = new SqlParameter("@guest", guest);
                parm[8] = new SqlParameter("@person", person);
                parm[9] = new SqlParameter("@remarks", remarks);
                parm[10] = new SqlParameter("@returnVal", SqlDbType.Int);
                parm[10].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strName, parm);

                returnVal = int.Parse(parm[10].Value.ToString());
            }
            catch(Exception ex)
            {
                returnVal = -1;
            }
            return MyErrorCollection.GetProcedureErrInfo(returnVal);
        }
        public FuncErrType ModifyDefect(int ID, string pID,int tID, string name,int order, int person)
        {
            int returnVal;

            try
            {
                string strName = "sp_QC_ModifyDefect";
                SqlParameter[] parm = new SqlParameter[7];
                parm[0] = new SqlParameter("@ID", ID);
                parm[1] = new SqlParameter("@pID", pID);
                parm[2] = new SqlParameter("@tID", tID);
                parm[3] = new SqlParameter("@name", name);
                parm[4] = new SqlParameter("@order", order);
                parm[5] = new SqlParameter("@person", person);
                parm[6] = new SqlParameter("@returnVal", SqlDbType.Int);
                parm[6].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strName, parm);

                returnVal = int.Parse(parm[6].Value.ToString());
            }
            catch (Exception ee)
            {
                returnVal = -1;
            }
            return MyErrorCollection.GetProcedureErrInfo(returnVal);
        }

        public FuncErrType ModifyDefectItem(int ID, int defID,int pID, string name,int orderby, int person, string statetype)
        {
            int returnVal;

            try
            {
                string strName = "sp_QC_ModifyDefectItem";
                SqlParameter[] parm = new SqlParameter[8];
                parm[0] = new SqlParameter("@ID", ID);
                parm[1] = new SqlParameter("@defID", defID);
                parm[2] = new SqlParameter("@name", name);
                parm[3] = new SqlParameter("@pID", pID);
                parm[4] = new SqlParameter("@orderby", orderby);
                parm[5] = new SqlParameter("@person", person);
                parm[6] = new SqlParameter("@statetype", statetype);
                parm[7] = new SqlParameter("@returnVal", SqlDbType.Int);
                parm[7].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strName, parm);

                returnVal = int.Parse(parm[7].Value.ToString());
            }
            catch (Exception ee)
            {
                returnVal = -1;
            }
            return MyErrorCollection.GetProcedureErrInfo(returnVal);
        }

        public FuncErrType ModifyDefectType(int ID,string module, string name, int orderby,int person)
        {
            int returnVal;

            try
            {
                string strName = "sp_QC_ModifyDefectType";
                SqlParameter[] parm = new SqlParameter[6];
                parm[0] = new SqlParameter("@ID", ID);
                parm[1] = new SqlParameter("@module", module);
                parm[2] = new SqlParameter("@name", name);
                parm[3] = new SqlParameter("@orderby", orderby);
                parm[4] = new SqlParameter("@person", person);
                parm[5] = new SqlParameter("@returnVal", SqlDbType.Int);
                parm[5].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strName, parm);

                returnVal = int.Parse(parm[5].Value.ToString());
            }
            catch
            {
                returnVal = -1;
            }
            return MyErrorCollection.GetProcedureErrInfo(returnVal);
        }

        public FuncErrType DeleteProduct(int ID, int uID, ref string msg)
        {
            int returnVal;

            try
            {
                string strName = "sp_QC_DeleteProduct";
                SqlParameter[] parm = new SqlParameter[2];
                parm[0] = new SqlParameter("@ID", ID);
                parm[1] = new SqlParameter("@uID", uID);

                SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strName, parm);

                returnVal = 0;
            }
            catch (Exception ee)
            {
                msg = "数据库操作失败!原因:" + ee.ToString();
                returnVal = -1;
            }
            return MyErrorCollection.GetProcedureErrInfo(returnVal);
        }
        public FuncErrType DeleteProductItem(int ID, ref string msg)
        {
            int returnVal;

            try
            {
                string strName = "sp_QC_DeleteProductItem";
                SqlParameter parm = new SqlParameter("@ID", ID);

                SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strName, parm);

                returnVal = 0;
            }
            catch (Exception ee)
            {
                msg = "数据库操作失败!原因:" + ee.ToString();
                returnVal = -1;
            }
            return MyErrorCollection.GetProcedureErrInfo(returnVal);
        }
        public FuncErrType DeleteDefect(int ID, ref string msg)
        {
            int returnVal;

            try
            {
                string strName = "sp_QC_DeleteDefect";
                SqlParameter parm = new SqlParameter("@ID", ID);

                SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strName, parm);

                returnVal = 0;
            }
            catch (Exception ee)
            {
                msg = "数据库操作失败!原因:" + ee.ToString();
                returnVal = -1;
            }
            return MyErrorCollection.GetProcedureErrInfo(returnVal);
        }

        public FuncErrType DeleteDefectType(int ID)
        {
            int returnVal;

            try
            {
                string strName = "sp_QC_DeleteDefectType";
                SqlParameter parm = new SqlParameter("@ID", ID);

                SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strName, parm);

                returnVal = 0;
            }
            catch (Exception ee)
            {
                returnVal = -1;
            }
            return MyErrorCollection.GetProcedureErrInfo(returnVal);
        }

        public FuncErrType DeleteDefectItem(int ID, ref string msg)
        {
            int returnVal;

            try
            {
                string strName = "sp_QC_DeleteDefectItem";
                SqlParameter parm = new SqlParameter("@ID", ID);

                SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strName, parm);

                returnVal = 0;
            }
            catch (Exception ee)
            {
                msg = "数据库操作失败!原因:" + ee.ToString();
                returnVal = -1;
            }
            return MyErrorCollection.GetProcedureErrInfo(returnVal);
        }
        #endregion

        #region 样品等光电色
        public DataTable SelectFlux(string product, string qad, string oldpro, string oldqad, string po, string cust, string wo, string provider, string lamp, string series,
          string period, string version, string dept, string temp, string aging, string date, string date1, string tester, string remarks, string statu, string type)
        {
            try
            {
                string strName = "sp_QC_SelectFlux";
                SqlParameter[] parm = new SqlParameter[21];
                parm[0] = new SqlParameter("@product", product);
                parm[1] = new SqlParameter("@qad", qad);
                parm[2] = new SqlParameter("@oldpro", oldpro);
                parm[3] = new SqlParameter("@oldqad", oldqad);
                parm[4] = new SqlParameter("@po", po);
                parm[5] = new SqlParameter("@cust", cust);
                parm[6] = new SqlParameter("@wo", wo);
                parm[7] = new SqlParameter("@provider", provider);
                parm[8] = new SqlParameter("@lamp", lamp);
                parm[9] = new SqlParameter("@series", series);
                parm[10] = new SqlParameter("@period", period);
                parm[11] = new SqlParameter("@version", version);
                parm[12] = new SqlParameter("@dept", dept);
                parm[13] = new SqlParameter("@temp", temp);
                parm[14] = new SqlParameter("@aging", aging);
                parm[15] = new SqlParameter("@date", date);
                parm[16] = new SqlParameter("@tester", tester);
                parm[17] = new SqlParameter("@remarks", remarks);
                parm[18] = new SqlParameter("@statu", statu);
                parm[19] = new SqlParameter("@type", type);
                parm[20] = new SqlParameter("@date1", date1);

                _dataset = SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName, parm);

                return _dataset.Tables[0];
            }
            catch
            {
                return null;
            }
            finally
            {
                _dataset.Dispose();
            }
        }
        public FuncErrType InsertFlux(string product, string qad, string oldpro, string oldqad, string po, string cust, string wo, string provider, string lamp, string series,
            string period, string version, string dept, string temp, string aging, string date, string tester, string statu, string remarks, int uID, ref int retID, string type) 
        {
            int returnVal;

            try
            {
                string strName = "sp_QC_InsertFlux";
                SqlParameter[] parm = new SqlParameter[23];
                parm[0] = new SqlParameter("@product", product);
                parm[1] = new SqlParameter("@qad", qad);
                parm[2] = new SqlParameter("@oldpro", oldpro);
                parm[3] = new SqlParameter("@oldqad", oldqad);
                parm[4] = new SqlParameter("@po", po);
                parm[5] = new SqlParameter("@cust", cust);
                parm[6] = new SqlParameter("@wo", wo);
                parm[7] = new SqlParameter("@provider", provider);
                parm[8] = new SqlParameter("@lamp", lamp);
                parm[9] = new SqlParameter("@series", series);
                parm[10] = new SqlParameter("@period", period);
                parm[11] = new SqlParameter("@version", version);
                parm[12] = new SqlParameter("@dept", dept);
                parm[13] = new SqlParameter("@temp", temp);
                parm[14] = new SqlParameter("@aging", aging);
                parm[15] = new SqlParameter("@date", date);
                parm[16] = new SqlParameter("@tester", tester);
                parm[17] = new SqlParameter("@statu", statu);
                parm[18] = new SqlParameter("@remarks", remarks);
                parm[19] = new SqlParameter("@uID", uID);
                parm[20] = new SqlParameter("@returnVal", SqlDbType.Int);
                parm[20].Direction = ParameterDirection.Output;
                parm[21] = new SqlParameter("@retID", SqlDbType.Int);
                parm[21].Direction = ParameterDirection.Output;
                parm[22] = new SqlParameter("@type", type);
                SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strName, parm);

                returnVal = int.Parse(parm[20].Value.ToString());
                retID = int.Parse(parm[21].Value.ToString());
            }
            catch
            {
                returnVal = -1;
                retID = 0;
            }
            return MyErrorCollection.GetProcedureErrInfo(returnVal);
        }

        public FuncErrType UpdateFlux(int id, string product, string qad, string oldpro, string oldqad, string po, string cust, string wo, string provider, string lamp, string series,
            string period, string version, string dept, string temp, string aging, string date, string tester, string statu, string remarks, int uID,string type)
        {
            int returnVal;

            try
            {
                string strName = "sp_QC_UpdateFlux";
                SqlParameter[] parm = new SqlParameter[23];
                parm[0] = new SqlParameter("@id", id);
                parm[1] = new SqlParameter("@product", product);
                parm[2] = new SqlParameter("@qad", qad);
                parm[3] = new SqlParameter("@oldpro", oldpro);
                parm[4] = new SqlParameter("@oldqad", oldqad);
                parm[5] = new SqlParameter("@po", po);
                parm[6] = new SqlParameter("@cust", cust);
                parm[7] = new SqlParameter("@wo", wo);
                parm[8] = new SqlParameter("@provider", provider);
                parm[9] = new SqlParameter("@lamp", lamp);
                parm[10] = new SqlParameter("@series", series);
                parm[11] = new SqlParameter("@period", period);
                parm[12] = new SqlParameter("@version", version);
                parm[13] = new SqlParameter("@dept", dept);
                parm[14] = new SqlParameter("@temp", temp);
                parm[15] = new SqlParameter("@aging", aging);
                parm[16] = new SqlParameter("@date", date);
                parm[17] = new SqlParameter("@tester", tester);
                parm[18] = new SqlParameter("@statu", statu);
                parm[19] = new SqlParameter("@remarks", remarks);
                parm[20] = new SqlParameter("@uID", uID);
                parm[21] = new SqlParameter("@returnVal", SqlDbType.Int);
                parm[21].Direction = ParameterDirection.Output;
                parm[22] = new SqlParameter("@type", type);

                SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strName, parm);

                returnVal = int.Parse(parm[21].Value.ToString());
            }
            catch
            {
                returnVal = -1;
            }
            return MyErrorCollection.GetProcedureErrInfo(returnVal);
        }

        public bool DeleteFlux(int id) 
        {
            bool bRet;

            try
            {
                string strName = "sp_QC_DeleteFlux";
                SqlParameter[] parm = new SqlParameter[1];
                parm[0] = new SqlParameter("@id", id);

                int nRet = SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strName, parm);

                if (nRet > -1)
                    bRet = true;
                else
                    bRet = false;
            }
            catch
            {
                bRet = false;
            }

            return bRet;
        }

        public DataTable SelectFluxDetail(int id)
        {
            try
            {
                string strName = "sp_QC_SelectFluxDetail";
                SqlParameter[] parm = new SqlParameter[1];
                parm[0] = new SqlParameter("@id", id);

                _dataset = SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName, parm);

                return _dataset.Tables[0];
            }
            catch
            {
                return null;
            }
            finally
            {
                _dataset.Dispose();
            }
        }

        public bool DeleteFluxDetail(int id)
        {
            bool bRet;

            try
            {
                string strName = "sp_QC_DeleteFluxDetail";
                SqlParameter[] parm = new SqlParameter[1];
                parm[0] = new SqlParameter("@id", id);

                int nRet = SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strName, parm);

                if (nRet > -1)
                    bRet = true;
                else
                    bRet = false;
            }
            catch
            {
                bRet = false;
            }

            return bRet;
        }

        public bool CheckUser(int plantid,string userno) 
        {
            try
            {
                string strName = "sp_QC_CheckUser";
                SqlParameter[] parm = new SqlParameter[3];
                parm[0] = new SqlParameter("@plantid", plantid);
                parm[1] = new SqlParameter("@userno", userno);
                parm[2] = new SqlParameter("@returnVal", SqlDbType.Bit);
                parm[2].Direction = ParameterDirection.Output;

                int nRet = SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strName, parm);

                return bool.Parse(parm[2].Value.ToString());
            }
            catch
            {
                return false;
            }
        }

        #endregion

        /// <summary>
        /// 通过工号和域查找工号是否全部输入正确
        /// </summary>
        /// <param name="strUserNo">输入的工号，可能是以；分隔的多个工号</param>
        /// <param name="plantId">传入的公司地点的id</param>
        /// <returns>输入的工号是否正确</returns>
        /// cheakUserNo
        public bool checkUserNo(string strUserNo, int plantId)
        {

            try
            {
                string strName = "sp_QC_checkUserNo";
                SqlParameter[] parm = new SqlParameter[3];
                parm[0] = new SqlParameter("@strUserNo", strUserNo);
                parm[1] = new SqlParameter("@plantId", plantId);
                parm[2] = new SqlParameter("@retValue", DbType.Boolean);
                parm[2].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strName, parm);

                return Convert.ToBoolean(parm[2].Value);
            }
            catch
            {
                return false;
            }
            
        }

        /// <summary>
        /// 查找tcp检验（未检验）的列表方法
        /// </summary>
        /// <param name="p"></param>
        /// <param name="p_2"></param>
        /// <param name="p_3"></param>
        /// <param name="p_4"></param>
        /// <param name="p_5"></param>
        /// <returns></returns>
        public DataSet GetProductTCPUnfinished(string order, string line, string date1, string date2, int plantid)
        {
            try
            {
                string strName = "sp_QC_GetProductTCPUnfinished";
                SqlParameter[] parm = new SqlParameter[5];
                parm[0] = new SqlParameter("@order", order);
                parm[1] = new SqlParameter("@line", line);
                parm[2] = new SqlParameter("@date1", date1);
                parm[3] = new SqlParameter("@date2", date2);
                parm[4] = new SqlParameter("@plantid", plantid);

                return _dataset = SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName, parm);
            }
            catch
            {
                return null;
            }
            finally
            {
                _dataset.Dispose();
            }
        }
        /// <summary>
        /// 检查检测数量和收货数量是否相等
        /// </summary>
        /// <param name="receiver">收货单号</param>
        /// <param name="line">行号</param>
        /// <param name="order">订单号</param>
        /// <returns></returns>
        public bool CheckCheckNumEqualsPrhNum(string group)
        {
            string strsql = "sp_QC_CheckNumEqualsPrhNum";
            SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@group",group)
   
            };

             return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsnx(), CommandType.StoredProcedure, strsql, param));
        }

        public DataSet GetProductRework(string order, string line, string board, string date, string insp, string period, string vent,
                                  string examer, string date1, string date2, int PageSize, int PageIndex, bool free)
        {
             try
            {
                string strName = "sp_QC_GetProduct_rework";
                SqlParameter[] parm = new SqlParameter[13];
                parm[0] = new SqlParameter("@order", order);
                parm[1] = new SqlParameter("@line", line);
                parm[2] = new SqlParameter("@board", board);
                parm[3] = new SqlParameter("@date", date);
                parm[4] = new SqlParameter("@insp", insp);
                parm[5] = new SqlParameter("@period", period);
                parm[6] = new SqlParameter("@vent", vent);
                parm[7] = new SqlParameter("@examer", examer);
                parm[8] = new SqlParameter("@date1", date1);
                parm[9] = new SqlParameter("@date2", date2);
                parm[10] = new SqlParameter("@PageSize", PageSize);
                parm[11] = new SqlParameter("@PageIndex", PageIndex);
                parm[12] = new SqlParameter("@isFree", free);

                return _dataset = SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName, parm);
            }
            catch
            {
                return null;
            }
            finally
            {
                _dataset.Dispose();
            }
        }
        
    public FuncErrType AddProductRework(string floor, string board, string date, string inspector,string Examiner, string qad, string id, string order
                                        , string period, int num, string guest, int person, string remarks, string lineMgt, string lineNo
                                        , string ProcessMgt)
    {
 	      int returnVal;

                try
                {
                    string strName = "sp_QC_AddProduct_rework";
                    SqlParameter[] parm = new SqlParameter[17];
                    parm[0] = new SqlParameter("@floor", floor);
                    parm[1] = new SqlParameter("@board", board);
                    parm[2] = new SqlParameter("@date", date);
                    parm[3] = new SqlParameter("@inspector", inspector);
                    parm[4] = new SqlParameter("@Examiner", Examiner);
                    parm[5] = new SqlParameter("@qad", qad);
                    parm[6] = new SqlParameter("@id", id);
                    parm[7] = new SqlParameter("@order", order);
                    parm[8] = new SqlParameter("@period", period);
                    parm[9] = new SqlParameter("@num", num);
                    parm[10] = new SqlParameter("@guest", guest);
                    parm[11] = new SqlParameter("@remarks", remarks);
                    parm[12] = new SqlParameter("@person", person);
                    parm[13] = new SqlParameter("@lineMgt", lineMgt);
                    parm[14] = new SqlParameter("@lineNo", lineNo);
                    parm[15] = new SqlParameter("@processMgt", ProcessMgt);
                    parm[16] = new SqlParameter("@returnVal", SqlDbType.Int);
                    parm[16].Direction = ParameterDirection.Output;
               

                    SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strName, parm);

                    returnVal = int.Parse(parm[16].Value.ToString());
                }
                catch
                {
                
                    returnVal = -1;
                }

                return MyErrorCollection.GetProcedureErrInfo(returnVal);
    }

    public FuncErrType GetProductSnatchRework(string line, string order, ref DataTable table)
    {
        int returnVal;

        try
        {
            string strName = "sp_QC_GetProductSnatch_rework";
            SqlParameter[] parm = new SqlParameter[2];
            parm[0] = new SqlParameter("@line", line);
            parm[1] = new SqlParameter("@WorkOrder", order);

            _dataset = SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName, parm);

            table = _dataset.Tables[0];

            returnVal = int.Parse(_dataset.Tables[0].Rows[0][0].ToString());
        }
        catch
        {
            table = null;
            returnVal = -1;
        }
        return MyErrorCollection.GetProcedureErrInfo(returnVal); 
    }

    public FuncErrType DeleteProductRework(int ID, int uID, ref string msg)
    {
        int returnVal;

        try
        {
            string strName = "sp_QC_DeleteProduct_rework";
            SqlParameter[] parm = new SqlParameter[2];
            parm[0] = new SqlParameter("@ID", ID);
            parm[1] = new SqlParameter("@uID", uID);

            SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strName, parm);

            returnVal = 0;
        }
        catch (Exception ee)
        {
            msg = "数据库操作失败!原因:" + ee.ToString();
            returnVal = -1;
        }
        return MyErrorCollection.GetProcedureErrInfo(returnVal);
    }

    public FuncErrType ModifyProductRework(string prdID, string board, string date, string inspector, string examiner, string period, string num, string guest, int person, string remarks)
    {
        int returnVal;

        try
        {
            string strName = "sp_QC_ModifyProduct_rework";
            SqlParameter[] parm = new SqlParameter[11];
            parm[0] = new SqlParameter("@prdID", prdID);
            parm[1] = new SqlParameter("@board", board);
            parm[2] = new SqlParameter("@date", date);
            parm[3] = new SqlParameter("@inspector", inspector);
            parm[4] = new SqlParameter("@examiner", examiner);
            parm[5] = new SqlParameter("@period", period);
            parm[6] = new SqlParameter("@num", num);
            parm[7] = new SqlParameter("@guest", guest);
            parm[8] = new SqlParameter("@person", person);
            parm[9] = new SqlParameter("@remarks", remarks);
            parm[10] = new SqlParameter("@returnVal", SqlDbType.Int);
            parm[10].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strName, parm);

            returnVal = int.Parse(parm[10].Value.ToString());
        }
        catch (Exception ex)
        {
            returnVal = -1;
        }
        return MyErrorCollection.GetProcedureErrInfo(returnVal);
    }

    public object GetProductItemRework(int prdID, int typeID)
    {
        try
        {
            string strName = "sp_QC_GetProductItem_rework";
            SqlParameter[] parm = new SqlParameter[2];
            parm[0] = new SqlParameter("@prdID", prdID);
            parm[1] = new SqlParameter("@typeID", typeID);

            _dataset = SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName, parm);

            return _dataset.Tables[0];
        }
        catch
        {
            return null;
        }
        finally
        {
            _dataset.Dispose();
        }
    }

    public object GetProductDefectRework(int defID, int prdItemID)
    {
        try
        {
            string strName = "sp_QC_GetProductDefect_rework";
            SqlParameter[] parm = new SqlParameter[2];
            parm[0] = new SqlParameter("@defID", defID);
            parm[1] = new SqlParameter("@prdItemID", prdItemID);

            _dataset = SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName, parm);

            return _dataset;
        }
        catch
        {
            return null;
        }
        finally
        {
            _dataset.Dispose();
        }
    }

    public bool AddProductItemRework(int prdID, int typeID, int defID, string Level, decimal Aql, string defItemID, string num, bool bPro, bool bDef, int qty, int total)
    {
        try
        {
            string strName = "sp_QC_AddProductItem_rework";
            SqlParameter[] parm = new SqlParameter[11];
            parm[0] = new SqlParameter("@prdID", prdID);
            parm[1] = new SqlParameter("@typeID", typeID);
            parm[2] = new SqlParameter("@defID", defID);
            parm[3] = new SqlParameter("@Level", Level);
            parm[4] = new SqlParameter("@Aql", Aql);
            parm[5] = new SqlParameter("@defItemID", defItemID);
            parm[6] = new SqlParameter("@num", num);
            parm[7] = new SqlParameter("@bPro", bPro);
            parm[8] = new SqlParameter("@bDef", bDef);
            parm[9] = new SqlParameter("@qty", qty);
            parm[10] = new SqlParameter("@total", total);

            int nRet = SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strName, parm);

            if (nRet == -1)
                return false;
            else
                return true;
        }
        catch (Exception ee)
        {
            return false;
        }
    }

    public string GetCP100Rework(string id)
    {
        try
        {
            string strName = "sp_QC_selectCP100_rework";
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@id", id);

            return SqlHelper.ExecuteScalar(adam.dsnx(), CommandType.StoredProcedure, strName, parm).ToString();
        }
        catch
        {
            return "0";
        }
    }
        //------------------------
    public DataSet GetProductSecond(string order, string line, string board, string date, string insp, string period, string vent,
                             string examer, string date1, string date2, int PageSize, int PageIndex, bool free)
    {
        try
        {
            string strName = "sp_QC_GetProduct_second";
            SqlParameter[] parm = new SqlParameter[13];
            parm[0] = new SqlParameter("@order", order);
            parm[1] = new SqlParameter("@line", line);
            parm[2] = new SqlParameter("@board", board);
            parm[3] = new SqlParameter("@date", date);
            parm[4] = new SqlParameter("@insp", insp);
            parm[5] = new SqlParameter("@period", period);
            parm[6] = new SqlParameter("@vent", vent);
            parm[7] = new SqlParameter("@examer", examer);
            parm[8] = new SqlParameter("@date1", date1);
            parm[9] = new SqlParameter("@date2", date2);
            parm[10] = new SqlParameter("@PageSize", PageSize);
            parm[11] = new SqlParameter("@PageIndex", PageIndex);
            parm[12] = new SqlParameter("@isFree", free);

            return _dataset = SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName, parm);
        }
        catch
        {
            return null;
        }
        finally
        {
            _dataset.Dispose();
        }
    }

    public FuncErrType AddProductSecond(string floor, string board, string date, string inspector, string Examiner, string qad, string id, string order
                                        , string period, int num, string guest, int person, string remarks, string lineMgt, string lineNo
                                        , string ProcessMgt)
    {
        int returnVal;

        try
        {
            string strName = "sp_QC_AddProduct_second";
            SqlParameter[] parm = new SqlParameter[18];
            parm[0] = new SqlParameter("@floor", floor);
            parm[1] = new SqlParameter("@board", board);
            parm[2] = new SqlParameter("@date", date);
            parm[3] = new SqlParameter("@inspector", inspector);
            parm[4] = new SqlParameter("@Examiner", Examiner);
            parm[5] = new SqlParameter("@qad", qad);
            parm[6] = new SqlParameter("@id", id);
            parm[7] = new SqlParameter("@order", order);
            parm[8] = new SqlParameter("@period", period);
            parm[9] = new SqlParameter("@num", num);
            parm[10] = new SqlParameter("@guest", guest);
            parm[11] = new SqlParameter("@remarks", remarks);
            parm[12] = new SqlParameter("@person", person);
            parm[13] = new SqlParameter("@lineMgt", lineMgt);
            parm[14] = new SqlParameter("@lineNo", lineNo);
            parm[15] = new SqlParameter("@processMgt", ProcessMgt);
            parm[16] = new SqlParameter("@returnVal", SqlDbType.Int);
            parm[16].Direction = ParameterDirection.Output;


            SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strName, parm);

            returnVal = int.Parse(parm[16].Value.ToString());
        }
        catch
        {

            returnVal = -1;
        }

        return MyErrorCollection.GetProcedureErrInfo(returnVal);
    }

    public FuncErrType GetProductSnatchSecond(string line, string order, ref DataTable table)
    {
        int returnVal;

        try
        {
            string strName = "sp_QC_GetProductSnatch_second";
            SqlParameter[] parm = new SqlParameter[2];
            parm[0] = new SqlParameter("@line", line);
            parm[1] = new SqlParameter("@WorkOrder", order);

            _dataset = SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName, parm);

            table = _dataset.Tables[0];

            returnVal = int.Parse(_dataset.Tables[0].Rows[0][0].ToString());
        }
        catch
        {
            table = null;
            returnVal = -1;
        }
        return MyErrorCollection.GetProcedureErrInfo(returnVal);
    }

    public FuncErrType DeleteProductSecond(int ID, int uID, ref string msg)
    {
        int returnVal;

        try
        {
            string strName = "sp_QC_DeleteProduct_second";
            SqlParameter[] parm = new SqlParameter[2];
            parm[0] = new SqlParameter("@ID", ID);
            parm[1] = new SqlParameter("@uID", uID);

            SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strName, parm);

            returnVal = 0;
        }
        catch (Exception ee)
        {
            msg = "数据库操作失败!原因:" + ee.ToString();
            returnVal = -1;
        }
        return MyErrorCollection.GetProcedureErrInfo(returnVal);
    }

    public FuncErrType ModifyProductSecond(string prdID, string board, string date, string inspector, string examiner, string period, string num, string guest, int person, string remarks)
    {
        int returnVal;

        try
        {
            string strName = "sp_QC_ModifyProduct_second";
            SqlParameter[] parm = new SqlParameter[11];
            parm[0] = new SqlParameter("@prdID", prdID);
            parm[1] = new SqlParameter("@board", board);
            parm[2] = new SqlParameter("@date", date);
            parm[3] = new SqlParameter("@inspector", inspector);
            parm[4] = new SqlParameter("@examiner", examiner);
            parm[5] = new SqlParameter("@period", period);
            parm[6] = new SqlParameter("@num", num);
            parm[7] = new SqlParameter("@guest", guest);
            parm[8] = new SqlParameter("@person", person);
            parm[9] = new SqlParameter("@remarks", remarks);
            parm[10] = new SqlParameter("@returnVal", SqlDbType.Int);
            parm[10].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strName, parm);

            returnVal = int.Parse(parm[10].Value.ToString());
        }
        catch (Exception ex)
        {
            returnVal = -1;
        }
        return MyErrorCollection.GetProcedureErrInfo(returnVal);
    }

    public object GetProductItemSecond(int prdID, int typeID)
    {
        try
        {
            string strName = "sp_QC_GetProductItem_second";
            SqlParameter[] parm = new SqlParameter[2];
            parm[0] = new SqlParameter("@prdID", prdID);
            parm[1] = new SqlParameter("@typeID", typeID);

            _dataset = SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName, parm);

            return _dataset.Tables[0];
        }
        catch
        {
            return null;
        }
        finally
        {
            _dataset.Dispose();
        }
    }

    public object GetProductDefectSecond(int defID, int prdItemID)
    {
        try
        {
            string strName = "sp_QC_GetProductDefect_second";
            SqlParameter[] parm = new SqlParameter[2];
            parm[0] = new SqlParameter("@defID", defID);
            parm[1] = new SqlParameter("@prdItemID", prdItemID);

            _dataset = SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName, parm);

            return _dataset;
        }
        catch
        {
            return null;
        }
        finally
        {
            _dataset.Dispose();
        }
    }

    public bool AddProductItemSecond(int prdID, int typeID, int defID, string Level, decimal Aql, string defItemID, string num, bool bPro, bool bDef, int qty, int total)
    {
        try
        {
            string strName = "sp_QC_AddProductItem_second";
            SqlParameter[] parm = new SqlParameter[11];
            parm[0] = new SqlParameter("@prdID", prdID);
            parm[1] = new SqlParameter("@typeID", typeID);
            parm[2] = new SqlParameter("@defID", defID);
            parm[3] = new SqlParameter("@Level", Level);
            parm[4] = new SqlParameter("@Aql", Aql);
            parm[5] = new SqlParameter("@defItemID", defItemID);
            parm[6] = new SqlParameter("@num", num);
            parm[7] = new SqlParameter("@bPro", bPro);
            parm[8] = new SqlParameter("@bDef", bDef);
            parm[9] = new SqlParameter("@qty", qty);
            parm[10] = new SqlParameter("@total", total);

            int nRet = SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strName, parm);

            if (nRet == -1)
                return false;
            else
                return true;
        }
        catch (Exception ee)
        {
            return false;
        }
    }

    public string GetCP100Second(string id)
    {
        try
        {
            string strName = "sp_QC_selectCP100_second";
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@id", id);

            return SqlHelper.ExecuteScalar(adam.dsnx(), CommandType.StoredProcedure, strName, parm).ToString();
        }
        catch
        {
            return "0";
        }
    }





    public DataTable getInfoByPrdID(int prdID, int uID, out string avgLong, out string avgWide, out string avgHigh, out string avgVolume, out string avgQuality, out string error, string part)
    {
        try
        {
            string sqlstr = "sp_qc_selectANDNewSizeByPrdID";

            SqlParameter[] param = new SqlParameter[]{
                new SqlParameter("@prdID", prdID)
                , new SqlParameter("@uID" , uID)
                , new SqlParameter("@AvgLong", SqlDbType.VarChar,100)
                , new SqlParameter("@AvgWide", SqlDbType.VarChar,100)
                , new SqlParameter("@AvgHigh", SqlDbType.VarChar,100)
                , new SqlParameter("@AvgVolume", SqlDbType.VarChar,100)
                , new SqlParameter("@AvgQuality", SqlDbType.VarChar,100)
                , new SqlParameter("@error", SqlDbType.VarChar,100)
                , new SqlParameter("@part" , part)
            };
            param[2].Direction = ParameterDirection.Output;
            param[3].Direction = ParameterDirection.Output;
            param[4].Direction = ParameterDirection.Output;
            param[5].Direction = ParameterDirection.Output;
            param[6].Direction = ParameterDirection.Output;
            param[7].Direction = ParameterDirection.Output;

            DataTable dt = SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, sqlstr, param).Tables[0];

            avgLong = param[2].Value.ToString();
            avgWide = param[3].Value.ToString();
            avgHigh = param[4].Value.ToString();
            avgVolume = param[5].Value.ToString();
            avgQuality = param[6].Value.ToString();
            error = param[7].Value.ToString();

            return dt;

        }
        catch
        {
            avgLong = string.Empty;
            avgWide = string.Empty;
            avgHigh = string.Empty;
            avgVolume = string.Empty;
            avgQuality = string.Empty;
            error = string.Empty;
            return null;
        }

    }




    public bool saveProductSize(string uID, DataTable dt, string prdID , string error)
    {
        try
        {
            StringWriter writer = new StringWriter();
            dt.WriteXml(writer);
            string xmlDetail = writer.ToString();

            string sqlstr = "sp_qc_saveProductSize";

            SqlParameter[] param = new SqlParameter[]{
                new SqlParameter("@uID",uID)
                , new SqlParameter("@xml",xmlDetail)
                , new SqlParameter("@prdID",prdID)
                , new SqlParameter ("@error",error)
            };

            return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsnx(), CommandType.StoredProcedure, sqlstr, param));

        }
        catch
        {
            return false;
        
        }
    }

    public DataSet GetProductOutUnfinished(string Order, string line, string date1, string date2, int plantid)
    {
        try
        {
            string strName = "sp_QC_GetProductOutUnfinished";
            SqlParameter[] parm = new SqlParameter[5];
            parm[0] = new SqlParameter("@order", Order);
            parm[1] = new SqlParameter("@line", line);
            parm[2] = new SqlParameter("@date1", date1);
            parm[3] = new SqlParameter("@date2", date2);
            parm[4] = new SqlParameter("@plantid", plantid);

            return _dataset = SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName, parm);
        }
        catch
        {
            return null;
        }
        finally
        {
            _dataset.Dispose();
        }
    }

    public DataTable selectSuppBasisList(string order, string part)
    {
        try
        {
            string sqlstr = "sp_QC_selectSuppBasisList";

            SqlParameter[] param = new SqlParameter[]{
                new SqlParameter ("@prd_po_nbr",order)
                , new SqlParameter("@part" , part)
            
            };

            return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param).Tables[0];


        }
        catch
        {
            return null;
        }
    }

    public DataSet GetProductOut(string order, string line, string board, string date, string insp, string period, string vent,
                             string examer, bool tcp, string date1, string date2, int PageSize, int PageIndex, bool outer, bool free)
    {
        try
        {
            string strName = "sp_QC_GetProductOut";
            SqlParameter[] parm = new SqlParameter[15];
            parm[0] = new SqlParameter("@order", order);
            parm[1] = new SqlParameter("@line", line);
            parm[2] = new SqlParameter("@board", board);
            parm[3] = new SqlParameter("@date", date);
            parm[4] = new SqlParameter("@insp", insp);
            parm[5] = new SqlParameter("@period", period);
            parm[6] = new SqlParameter("@vent", vent);
            parm[7] = new SqlParameter("@examer", examer);
            parm[8] = new SqlParameter("@tcp", tcp);
            parm[9] = new SqlParameter("@date1", date1);
            parm[10] = new SqlParameter("@date2", date2);
            parm[11] = new SqlParameter("@PageSize", PageSize);
            parm[12] = new SqlParameter("@PageIndex", PageIndex);
            parm[13] = new SqlParameter("@out", outer);
            parm[14] = new SqlParameter("@isFree", free);

            return _dataset = SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName, parm);
        }
        catch
        {
            return null;
        }
        finally
        {
            _dataset.Dispose();
        }
    }





    public bool reCheckReport(string group, string uID, string uName)
    {
        string sqlstr = "sp_qc_reCheckReportByGroup";

        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@group",group)
            , new SqlParameter("@uID",uID)
            , new SqlParameter("@uName",uName)
        };

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsnx(), CommandType.StoredProcedure, sqlstr, param));



    }

    public DataTable selectProductReworkStreamline(string prdID)
    {

        string sqlstr = "sp_qc_selectProductReworkStreamline";

        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@prdID",prdID)
        };

        return SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, sqlstr, param).Tables[0];

    }




    public bool uploadProductReworkStreamlineFile(string prdID, string qprsID, string fileName, string filePate, string uID, string uName)
    {
        string sqlstr = "sp_qc_uploadProductReworkStreamlineFile";

        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@prdID",prdID)
            , new SqlParameter("@qprsID",qprsID)
            , new SqlParameter("@fileName",fileName)
            , new SqlParameter("@filePate",filePate)
            , new SqlParameter("@uID",uID)
            , new SqlParameter("@uName",uName)
        
        };

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsnx(),CommandType.StoredProcedure,sqlstr,param));
    }



    public bool uploadProductReworkStreamlineQC(string prdID, string qprsID, string fileName, string filePate, string uID, string uName, int ispass)
    {
        string sqlstr = "sp_qc_uploadProductReworkStreamlineQC";

        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@prdID",prdID)
            , new SqlParameter("@qprsID",qprsID)
            , new SqlParameter("@fileName",fileName)
            , new SqlParameter("@filePate",filePate)
            , new SqlParameter("@uID",uID)
            , new SqlParameter("@uName",uName)
            , new SqlParameter("@ispass",ispass)
        
        };

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsnx(), CommandType.StoredProcedure, sqlstr, param));
    }

    public DataTable selectProductReworkStreamlineList(string woNbr, string wolot, string type, string starDate, string endDate)
    {
        string sqlstr = "sp_qc_selectProductReworkStreamlineList";

        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@woNbr",woNbr)
            , new SqlParameter("@type",type)
            , new SqlParameter("@starDate",starDate)
            , new SqlParameter("@endDate",endDate)
            , new SqlParameter("@wolot",wolot)
        };

        return SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, sqlstr, param).Tables[0];

    }

    public bool deleteReworkStream(string qprsID, string uID, string uName)
    {

        string sqlstr = "sp_qc_deleteReworkStream";

        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@qprsID",qprsID)
            , new SqlParameter("@uID",uID)
            , new SqlParameter("@uName",uName)

        };

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsnx(), CommandType.StoredProcedure, sqlstr, param));

    }

    public string getProductCanUpdate(string id)
    {
        string sqlstr = "sp_qc_getProductCanUpdate";

        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@id",id)
         

        };

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsnx(), CommandType.StoredProcedure, sqlstr, param)).ToString();
    }

    public string getProductCanUpdate(string id, int days)
    {
        string sqlstr = "sp_qc_getProductCanUpdate";

        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@id",id)
            ,new SqlParameter("@days",days)

        };

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsnx(), CommandType.StoredProcedure, sqlstr, param)).ToString();
    }

    public DataSet GetReportUndo(string receiver, string nbr, string line, string vent, string part, string stddate, string enddate
            , string uID, string plantid, int PageSize, int PageIndex, int isProduct)
    {
        try
        {
            string strName = "sp_QC_GetReportUndo";
            SqlParameter[] param = new SqlParameter[12];
            param[0] = new SqlParameter("@receiver", receiver);
            param[1] = new SqlParameter("@nbr", nbr);
            param[2] = new SqlParameter("@line", line);
            param[3] = new SqlParameter("@vent", vent);
            param[4] = new SqlParameter("@part", part);
            param[5] = new SqlParameter("@stddate", stddate);
            param[6] = new SqlParameter("@enddate", enddate);
            param[7] = new SqlParameter("@uID", uID);
            param[8] = new SqlParameter("@plantid", plantid);
            param[9] = new SqlParameter("@PageSize", PageSize);
            param[10] = new SqlParameter("@PageIndex", PageIndex);
            param[11] = new SqlParameter("@isProduct", isProduct);

            return _dataset = SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName, param);
        }
        catch
        {
            return null;
        }
        finally
        {
            _dataset.Dispose();
        }
    }

    public DataTable selectSuppBasisListByReportGroup(string group,string plandCode, out string order
        , out string line, out string part, out string guest)
    {
        string sqlstr = "sp_QC_selectSuppBasisListByReportGroup";

        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@group",group)
            , new SqlParameter("@plandCode",plandCode)//数字
            , new SqlParameter("@order", SqlDbType.NVarChar,100)
            , new SqlParameter("@line", SqlDbType.NVarChar,100)
            , new SqlParameter("@part", SqlDbType.NVarChar,100)
            , new SqlParameter("@guest", SqlDbType.NVarChar,100)

        };

        param[2].Direction = ParameterDirection.Output;
        param[3].Direction = ParameterDirection.Output;
        param[4].Direction = ParameterDirection.Output;
        param[5].Direction = ParameterDirection.Output;
           

        DataTable dt = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param).Tables[0];


        order = param[2].Value.ToString();
        line = param[3].Value.ToString();
        part = param[4].Value.ToString();
        guest = param[5].Value.ToString();

        return dt;
    }

    public DataTable selectReportProductImportByGroup(string group, string plandCode, out string isPass)
    {
        string sqlstr = "sp_QC_selectReportProductImportByGroup";

        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@group",group)
            , new SqlParameter("@isPass",SqlDbType.NVarChar,10)

        };
        param[1].Direction = ParameterDirection.Output;

        DataTable dt = SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, sqlstr, param).Tables[0];


        isPass = param[1].Value.ToString();
       

        return dt;
    }

    public bool deleteReportProductImport(string importID, int uID, string uName)
    {
        string sqlstr = "sp_qc_deleteReportProductImport";

        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@importID",importID)
            ,new SqlParameter("@uID",uID)
             ,new SqlParameter("@uName",uName)

        };

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsnx(), CommandType.StoredProcedure, sqlstr, param));
    }

    public bool uploadReportProductBasis(string group, string fileName, string filePate, string uID, string uName)
    {
        string sqlstr = "sp_qc_uploadReportProductBasis";

        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@group",group)
            ,new SqlParameter("@fileName",fileName)
            ,new SqlParameter("@filePate",filePate)
            ,new SqlParameter("@uID",uID)
            ,new SqlParameter("@uName",uName)

        };

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsnx(), CommandType.StoredProcedure, sqlstr, param));
    }

    public bool checkTCPBasis(string group)
    {
        string sqlstr = "sp_qc_checkTCPBasis";

        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@group",group)
           

        };

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsnx(), CommandType.StoredProcedure, sqlstr, param));
    }

    public bool updateReportProductIsPass(string group, string isPass,string uID,string uName)
    {
        string sqlstr = "sp_qc_updateReportProductIsPass";

        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@group",group)
           ,new SqlParameter("@isPass",isPass)
            ,new SqlParameter("@uID",uID)
             ,new SqlParameter("@uName",uName)

        };

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsnx(), CommandType.StoredProcedure, sqlstr, param));
    }

    public DataTable ExportReportProductComplete(string receiver, string nbr, string line, string vend, string part,
        string stddate, string enddate, int state
            , string uID, string plantid)
    {
        string strName = "sp_QC_ExportReportProductComplete";
        SqlParameter[] param = new SqlParameter[10];
        param[0] = new SqlParameter("@receiver", receiver);
        param[1] = new SqlParameter("@order", nbr);
        param[2] = new SqlParameter("@line", line);
        param[3] = new SqlParameter("@vend", vend);
        param[4] = new SqlParameter("@part", part);
        param[5] = new SqlParameter("@stddate", stddate);
        param[6] = new SqlParameter("@enddate", enddate);
        param[7] = new SqlParameter("@state", state);//0全部 ，1免检，2完成，3特采，4退货
        param[8] = new SqlParameter("@uID", uID);
        param[9] = new SqlParameter("@plantid", plantid);


        return  SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName, param).Tables[0];
    }

    public bool importFluxBasisFile(string id, string uID, string uName, string fileName, string filePate)
    {
        string strName = "sp_QC_importFluxBasisFile";
        SqlParameter[] param = new SqlParameter[6];
        param[0] = new SqlParameter("@id", id);
        param[1] = new SqlParameter("@uID", uID);
        param[2] = new SqlParameter("@uName", uName);
        param[3] = new SqlParameter("@fileName", fileName);
        param[4] = new SqlParameter("@filePate", filePate);



        return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsnx(), CommandType.StoredProcedure, strName, param));
    }



    public DataTable selectQcFluxBasisList(string mid)
    {
        string strName = "sp_QC_selectQcFluxBasisList";
        SqlParameter[] param = new SqlParameter[5];
        param[0] = new SqlParameter("@mid", mid);

        return SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName, param).Tables[0];
    }

    public bool updateQCFluxBasis(string id, string uID, string uName)
    {
        string strName = "sp_QC_updateQCFluxBasis";
        SqlParameter[] param = new SqlParameter[5];
        param[0] = new SqlParameter("@id", id);
        param[1] = new SqlParameter("@uID", uID);
        param[2] = new SqlParameter("@uName", uName);

        return Convert.ToBoolean( SqlHelper.ExecuteScalar(adam.dsnx(), CommandType.StoredProcedure, strName, param));
    }

    public void updateQCFluxStatusByID(string id,string uID,string uName)
    {
        string strName = "sp_QC_updateQCFluxStatusByID";
        SqlParameter[] param = new SqlParameter[5];
        param[0] = new SqlParameter("@id", id);
        param[1] = new SqlParameter("@uID", uID);
        param[2] = new SqlParameter("@uName", uName);
     
        SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, strName, param);
    }

    public SqlDataReader SelectFlux(string id)
    {
        string strName = "sp_QC_SelectFluxByID";
        SqlParameter[] param = new SqlParameter[5];
        param[0] = new SqlParameter("@id", id);
      

        return  SqlHelper.ExecuteReader(adam.dsnx(), CommandType.StoredProcedure, strName, param);
    }
    }
}


