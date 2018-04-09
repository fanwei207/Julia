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
        /// �ű�������
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
        /// �ű���Namespace
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
        /// Ҫ�ֽ�ı�ǩ
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
        /// Ҫ�ֽ���ַ�
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
        /// <param name="source">�ֽ�Դ</param>
        public ScriptHelper(string source)
        {
            this._tab = "<script";
            this._source = source;

        }
        /// <summary>
        /// �ֽ�
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
        /// FTP��������IP��ַ
        /// </summary>
        private string ftpServerIP;

        /// <summary>
        /// ��֤�˻�
        /// </summary>
        private string ftpUserID;

        /// <summary>
        /// ��֤����
        /// </summary>
        private string ftpPassword;

        private FtpWebRequest reqFTP;

        private void Connect(String path)//����ftp
        {
            // ����uri����FtpWebRequest����
            reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(path));
            // ָ�����ݴ�������
            reqFTP.UseBinary = true;
            // ftp�û���������
            reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword, string.Empty);
        }
        /// <summary>
        /// ���롣����FTP���Ӷ���
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

        //���������
        private string[] GetFileList(string path, string WRMethods)
        {
            StringBuilder result = new StringBuilder();

            try
            {
                Connect(path);

                reqFTP.Method = WRMethods;

                WebResponse response = reqFTP.GetResponse();

                StreamReader reader = new StreamReader(response.GetResponseStream(), System.Text.Encoding.Default);//�����ļ���

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
        /// ��ȡFTP������ָ���ļ��е��ļ��б�
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string[] GetFileList(string path)
        {
            return GetFileList("ftp://" + ftpServerIP + "/" + path, WebRequestMethods.Ftp.ListDirectory);
        }

        /// <summary>
        /// ��ȡFTP�������ļ��б�
        /// </summary>
        /// <returns></returns>
        public string[] GetFileList()
        {
            return GetFileList("ftp://" + ftpServerIP + "/", WebRequestMethods.Ftp.ListDirectory);
        }

        /// <summary>
        /// �ϴ��ĵ�
        /// </summary>
        /// <param name="filename">�����ļ�������·��</param>
        public Boolean Upload(string filename, string serverPath)
        {
            FileInfo fileInf = new FileInfo(filename);

            string uri = "ftp://" + ftpServerIP + "/" + serverPath + "/" + fileInf.Name;

            Connect(uri);//����          

            // Ĭ��Ϊtrue�����Ӳ��ᱻ�ر�

            // ��һ������֮��ִ��

            reqFTP.KeepAlive = false;

            // ָ��ִ��ʲô����

            reqFTP.Method = WebRequestMethods.Ftp.UploadFile;

            // �ϴ��ļ�ʱ֪ͨ�������ļ��Ĵ�С

            reqFTP.ContentLength = fileInf.Length;

            // �����С����Ϊkb 

            int buffLength = 2048;

            byte[] buff = new byte[buffLength];

            int contentLen;

            // ��һ���ļ���(System.IO.FileStream) ȥ���ϴ����ļ�

            FileStream fs = fileInf.OpenRead();

            try
            {
                // ���ϴ����ļ�д����

                Stream strm = reqFTP.GetRequestStream();

                // ÿ�ζ��ļ�����kb 

                contentLen = fs.Read(buff, 0, buffLength);

                // ������û�н���

                while (contentLen != 0)
                {
                    // �����ݴ�file stream д��upload stream 

                    strm.Write(buff, 0, contentLen);

                    contentLen = fs.Read(buff, 0, buffLength);
                }

                // �ر�������

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
        /// �����ĵ�
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

                    errorinfo = string.Format("�����ļ�{0}�Ѵ���,�޷�����", newFileName);

                    return false;
                }

                string url = "ftp://" + ftpServerIP + "/" + fileName;

                Connect(url);//����  

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
                errorinfo = string.Format("��{0},�޷�����", ex.Message);

                return false;
            }

        }

        /// <summary>
        /// ɾ���ļ�
        /// </summary>
        /// <param name="fileName">�������������·�����ļ���</param>
        public bool DeleteFileName(string fileName)
        {
            try
            {
                string uri = "ftp://" + ftpServerIP + "/" + fileName;

                Connect(uri);//����          

                // Ĭ��Ϊtrue�����Ӳ��ᱻ�ر�

                // ��һ������֮��ִ��

                reqFTP.KeepAlive = false;

                // ָ��ִ��ʲô����

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
        /// ����Ŀ¼
        /// </summary>
        /// <param name="dirName"></param>
        public bool MakeDir(string dirName)
        {
            try
            {
                string uri = "ftp://" + ftpServerIP + "/" + dirName;

                Connect(uri);//����       

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
        /// ɾ��Ŀ¼
        /// </summary>
        /// <param name="dirName"></param>
        public void delDir(string dirName)
        {
            try
            {
                string uri = "ftp://" + ftpServerIP + "/" + dirName;

                Connect(uri);//����       

                reqFTP.Method = WebRequestMethods.Ftp.RemoveDirectory;

                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();

                response.Close();
            }

            catch (Exception ex)
            {
                throw new Exception("ɾ��Ŀ¼" + ex.Message);
            }
        }

        /// <summary>
        /// ����ļ���С
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

                Connect(uri);//����       

                reqFTP.Method = WebRequestMethods.Ftp.GetFileSize;

                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();

                fileSize = response.ContentLength;

                response.Close();
            }

            catch (Exception ex)
            {
                throw new Exception("����ļ���С" + ex.Message);
            }

            return fileSize;
        }

        /// <summary>
        /// �ļ�����
        /// </summary>
        /// <param name="currentFilename"></param>
        /// <param name="newFilename"></param>
        public void Rename(string currentFilename, string newFilename)
        {
            try
            {
                FileInfo fileInf = new FileInfo(currentFilename);

                string uri = "ftp://" + ftpServerIP + "/" + fileInf.Name;

                Connect(uri);//����

                reqFTP.Method = WebRequestMethods.Ftp.Rename;

                reqFTP.RenameTo = newFilename;

                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();

                response.Close();
            }

            catch (Exception ex)
            {
                throw new Exception("�ļ�����" + ex.Message);
            }
        }

        /// <summary>
        /// ����ļ�����
        /// </summary>
        /// <returns></returns>
        public string[] GetFilesDetailList()
        {
            return GetFileList("ftp://" + ftpServerIP + "/", WebRequestMethods.Ftp.ListDirectoryDetails);
        }

        /// <summary>
        /// ����ļ�����
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string[] GetFilesDetailList(string path)
        {
            return GetFileList("ftp://" + ftpServerIP + "/" + path, WebRequestMethods.Ftp.ListDirectoryDetails);
        }
    }
}