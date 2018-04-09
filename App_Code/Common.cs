using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Net;
using System.IO;
using System.Text;

namespace Portal.Common
{
    /// <summary>
    /// JavaScript
    /// </summary>
    public class Script
    {
        private string _object;
        /// <summary>
        /// 脚本的类名
        /// </summary>
        public string Object
        {
            get
            {
                return this._object;
            }
            set
            {
                this._object = value;
            }
        }

        private string _namespace;
        /// <summary>
        /// 脚本的Namespace
        /// </summary>
        public string Namespace
        {
            get
            {
                return this._namespace;
            }
            set
            {
                this._namespace = value;
            }
        }

        public Script()
        {
            this._object = string.Empty;
            this._namespace = string.Empty;
        }
    }
    public class ScriptHelper
    {
        private string _tab;
        /// <summary>
        /// 要分解的标签
        /// </summary>
        public string Tab
        {
            get
            {
                return this._tab;
            }
            set
            {
                this._tab = value;
            }
        }

        private string _source;
        /// <summary>
        /// 要分解的字符
        /// </summary>
        public string Source
        {
            get
            {
                return this._source;
            }
            set
            {
                this._source = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source">分解源</param>
        public ScriptHelper(string source)
        {
            this._tab = "<script";
            this._source = source;

        }
        /// <summary>
        /// 分解
        /// </summary>
        /// <returns></returns>
        public IList<Script> Analyze()
        {
            IList<Script> list = new List<Script>();

            string[] split = this._source.Split(new string[] { this._tab }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string str in split)
            {
                if (str.Contains("src"))
                {
                    Script item = new Script();
                    item.Namespace = "";
                    item.Object = "";


                    list.Add(item);
                }
            }

            return list;
        }
    }

    public class Ftp
    {
        /// <summary>
        /// FTP服务器的IP地址
        /// </summary>
        private string ftpServerIP;

        /// <summary>
        /// 验证账户
        /// </summary>
        private string ftpUserID;

        /// <summary>
        /// 验证密码
        /// </summary>
        private string ftpPassword;

        private FtpWebRequest reqFTP;

        private void Connect(String path)//连接ftp
        {
            // 根据uri创建FtpWebRequest对象
            reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(path));
            // 指定数据传输类型
            reqFTP.UseBinary = true;
            // ftp用户名和密码
            reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword, string.Empty);
        }
        /// <summary>
        /// 必须。生成FTP链接对象。
        /// </summary>
        /// <param name="ftpServerIP"></param>
        /// <param name="ftpUserID"></param>
        /// <param name="ftpPassword"></param>
        public Ftp(string ftpServerIP, string ftpUserID, string ftpPassword)
        {
            this.ftpServerIP = ftpServerIP;
            this.ftpUserID = ftpUserID;
            this.ftpPassword = ftpPassword;
        }

        //都调用这个
        private string[] GetFileList(string path, string WRMethods)
        {
            StringBuilder result = new StringBuilder();

            try
            {
                Connect(path);

                reqFTP.Method = WRMethods;

                WebResponse response = reqFTP.GetResponse();

                StreamReader reader = new StreamReader(response.GetResponseStream(), System.Text.Encoding.Default);//中文文件名

                string line = reader.ReadLine();

                while (line != null)
                {

                    result.Append(line);

                    result.Append(" ");

                    line = reader.ReadLine();

                }

                result.Remove(result.ToString().LastIndexOf(' '), 1);

                reader.Close();

                response.Close();

                return result.ToString().Split(' ');
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 获取FTP服务器指定文件夹的文件列表
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string[] GetFileList(string path)
        {
            return GetFileList("ftp://" + ftpServerIP + "/" + path, WebRequestMethods.Ftp.ListDirectory);
        }

        /// <summary>
        /// 获取FTP服务器文件列表
        /// </summary>
        /// <returns></returns>
        public string[] GetFileList()
        {
            return GetFileList("ftp://" + ftpServerIP + "/", WebRequestMethods.Ftp.ListDirectory);
        }

        /// <summary>
        /// 上传文档
        /// </summary>
        /// <param name="filename">本地文件的完整路径</param>
        public Boolean Upload(string filename, string serverPath)
        {
            FileInfo fileInf = new FileInfo(filename);

            string uri = "ftp://" + ftpServerIP + "/" + serverPath + "/" + fileInf.Name;

            Connect(uri);//连接          

            // 默认为true，连接不会被关闭

            // 在一个命令之后被执行

            reqFTP.KeepAlive = false;

            // 指定执行什么命令

            reqFTP.Method = WebRequestMethods.Ftp.UploadFile;

            // 上传文件时通知服务器文件的大小

            reqFTP.ContentLength = fileInf.Length;

            // 缓冲大小设置为kb 

            int buffLength = 2048;

            byte[] buff = new byte[buffLength];

            int contentLen;

            // 打开一个文件流(System.IO.FileStream) 去读上传的文件

            FileStream fs = fileInf.OpenRead();

            try
            {
                // 把上传的文件写入流

                Stream strm = reqFTP.GetRequestStream();

                // 每次读文件流的kb 

                contentLen = fs.Read(buff, 0, buffLength);

                // 流内容没有结束

                while (contentLen != 0)
                {
                    // 把内容从file stream 写入upload stream 

                    strm.Write(buff, 0, contentLen);

                    contentLen = fs.Read(buff, 0, buffLength);
                }

                // 关闭两个流

                strm.Close();

                fs.Close();

                return true;
            }

            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 下载文档
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="fileName"></param>
        /// <param name="errorinfo"></param>
        /// <returns></returns>
        public bool Download(string filePath, string fileName, out string errorinfo)
        {
            try
            {
                String onlyFileName = Path.GetFileName(fileName);

                string newFileName = filePath + "/" + onlyFileName;

                if (File.Exists(newFileName))
                {

                    errorinfo = string.Format("本地文件{0}已存在,无法下载", newFileName);

                    return false;
                }

                string url = "ftp://" + ftpServerIP + "/" + fileName;

                Connect(url);//连接  

                reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);

                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();

                Stream ftpStream = response.GetResponseStream();

                long cl = response.ContentLength;

                int bufferSize = 2048;

                int readCount;

                byte[] buffer = new byte[bufferSize];

                readCount = ftpStream.Read(buffer, 0, bufferSize);

                FileStream outputStream = new FileStream(newFileName, FileMode.Create);

                while (readCount > 0)
                {
                    outputStream.Write(buffer, 0, readCount);

                    readCount = ftpStream.Read(buffer, 0, bufferSize);
                }

                ftpStream.Close();

                outputStream.Close();

              response.Close();

                errorinfo = "";

                return true;
            }
            catch (Exception ex)
            {
                errorinfo = string.Format("因{0},无法下载", ex.Message);

                return false;
            }

        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="fileName">包含服务器相对路径的文件名</param>
        public bool DeleteFileName(string fileName)
        {
            try
            {
                string uri = "ftp://" + ftpServerIP + "/" + fileName;

                Connect(uri);//连接          

                // 默认为true，连接不会被关闭

                // 在一个命令之后被执行

                reqFTP.KeepAlive = false;

                // 指定执行什么命令

                reqFTP.Method = WebRequestMethods.Ftp.DeleteFile;

                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();

                response.Close();

                return true;
            }

            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 创建目录
        /// </summary>
        /// <param name="dirName"></param>
        public bool MakeDir(string dirName)
        {
            try
            {
                string uri = "ftp://" + ftpServerIP + "/" + dirName;

                Connect(uri);//连接       

                reqFTP.Method = WebRequestMethods.Ftp.MakeDirectory;

                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();

                response.Close();

                return true;
            }

            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 删除目录
        /// </summary>
        /// <param name="dirName"></param>
        public void delDir(string dirName)
        {
            try
            {
                string uri = "ftp://" + ftpServerIP + "/" + dirName;

                Connect(uri);//连接       

                reqFTP.Method = WebRequestMethods.Ftp.RemoveDirectory;

                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();

                response.Close();
            }

            catch (Exception ex)
            {
                throw new Exception("删除目录" + ex.Message);
            }
        }

        /// <summary>
        /// 获得文件大小
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public long GetFileSize(string filename)
        {
            long fileSize = 0;

            try
            {
                FileInfo fileInf = new FileInfo(filename);

                string uri = "ftp://" + ftpServerIP + "/" + fileInf.Name;

                Connect(uri);//连接       

                reqFTP.Method = WebRequestMethods.Ftp.GetFileSize;

                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();

                fileSize = response.ContentLength;

                response.Close();
            }

            catch (Exception ex)
            {
                throw new Exception("获得文件大小" + ex.Message);
            }

            return fileSize;
        }

        /// <summary>
        /// 文件改名
        /// </summary>
        /// <param name="currentFilename"></param>
        /// <param name="newFilename"></param>
        public void Rename(string currentFilename, string newFilename)
        {
            try
            {
                FileInfo fileInf = new FileInfo(currentFilename);

                string uri = "ftp://" + ftpServerIP + "/" + fileInf.Name;

                Connect(uri);//连接

                reqFTP.Method = WebRequestMethods.Ftp.Rename;

                reqFTP.RenameTo = newFilename;

                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();

                response.Close();
            }

            catch (Exception ex)
            {
                throw new Exception("文件改名" + ex.Message);
            }
        }

        /// <summary>
        /// 获得文件明晰
        /// </summary>
        /// <returns></returns>
        public string[] GetFilesDetailList()
        {
            return GetFileList("ftp://" + ftpServerIP + "/", WebRequestMethods.Ftp.ListDirectoryDetails);
        }

        /// <summary>
        /// 获得文件明晰
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string[] GetFilesDetailList(string path)
        {
            return GetFileList("ftp://" + ftpServerIP + "/" + path, WebRequestMethods.Ftp.ListDirectoryDetails);
        }
    }
}