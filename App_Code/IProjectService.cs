using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;
using System.Runtime.InteropServices;

namespace Contracts
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IProjectService
    {
        /// <summary>
        /// 返回项目头栏信息
        /// </summary>
        /// <param name="name">项目名称</param>
        /// <param name="code">项目编号</param>
        /// <returns>返回的值依次是项目描述，项目起始日期，项目结束日期，以及ID</returns>
        [OperationContract]
        Tuple<string, string, string, string> GetHeader(string name, string code);

        /// <summary>
        /// 根据项目名称和编号获取项目步骤
        /// </summary>
        /// <param name="project">项目名称</param>
        /// <param name="prodcode">项目编号</param>
        /// <returns>项目下的步骤</returns>
        [OperationContract]
        DataTable GetSteps(string project, string prodcode);

        /// <summary>
        /// 根据项目步骤ID获取步骤信息
        /// </summary>
        /// <param name="id">项目步骤ID</param>
        /// <returns>步骤信息</returns>
        [OperationContract]
        DataSet GetStepById(string id);

        /// <summary>
        /// 根据步骤ID获取步骤的文档
        /// </summary>
        /// <param name="strDID">步骤ID</param>
        /// <returns>文档</returns>
        [OperationContract]
        DataSet SelectRDWDetailDocs(string strDID, string strUid);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="uID"></param>
        /// <param name="uName"></param>
        /// <param name="stepID"></param>
        /// <returns></returns>
        [OperationContract]
        bool UploadFile(string fileName, string newFileName,string uID, string uName, string stepID);

        [OperationContract]
        bool DeleteFile(string strDocID);

        [OperationContract]
        bool CheckFinishRDWDetail(string strDID, string strUID);
    }

    [ServiceContract]
    public interface ILoginService
    {
        [OperationContract]
        string EncryptPWD(string strInput, [Optional, DefaultParameterValue("www.fengxinsoftware.com")] string sKey);
        [OperationContract]
        DataSet Login(string loginName, string userPwd, string domain = "szx");
        [OperationContract]
        DataTable GetCompanies();
        // TODO: Add your service operations here
    }

    [ServiceContract]
    public interface IBaseInfo
    {
        [OperationContract]
        string GetFtpDocumentServerAddress();
        [OperationContract]
        string GetFtpDocumentServerFtpUserName();
        [OperationContract]
        string GetFtpDocumentServerPassword();
    }

}
