using UnityEngine;
using System.IO;
using System.Xml;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.LogSystem;
using System.Net;
namespace DreamFaction.Utils
{
    /// <summary>
    /// XML文档IO操作的辅助类，提供加载，创建，判断等操作！PC,IOS,Android 都已通过测试！可用
    /// </summary>
    public class XMLHelper
    {
        /// <summary>
        /// 删除可读写路径下文件，例如：file:////storage/sdcard0/Android/data/com.DreamFactionGame.DreamHeros/files/Data/Config.xml
        /// 注意这里的路径只是可读写路径，并不是只读路径！
        /// </summary>
        /// <param name="rw_path">可读写路径+文件名+后缀名</param>
        /// <returns>文件是否存在</returns>
        public static bool RemoveFile_RW(string rw_path)
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                if (!rw_path.Contains("apk!/assets/"))
                {
                    rw_path = "file:///" + rw_path; // Android下可读写路径(SD卡)也要加file:///
                }
            }

            if (File.Exists(rw_path))
            {
                //Debug.Log("删除文件：" + rw_path);
                File.Delete(rw_path);
            }

            return true;
        }

        /// <summary>
        /// 检查可读写路径下文件和目录是否存在，例如：file:////storage/sdcard0/Android/data/com.DreamFactionGame.DreamHeros/files/Data/Config.xml
        /// 注意这里的路径只是可读写路径，并不是只读路径！
        /// </summary>
        /// <param name="rw_path">可读写路径+文件名+后缀名</param>
        /// <returns>文件是否存在</returns>
        public static bool CheckFileAndDirExists_RW(string rw_path)
        {
            FileInfo fileInfo = new FileInfo(rw_path);
            // 1：先检查路径是否存在，如果不存在则创建！
            if (!fileInfo.Directory.Exists)
            {
                Directory.CreateDirectory(fileInfo.Directory.FullName);
                LogManager.LogToFile("创建新目录：" + fileInfo.Directory);
                return false;
            }
            // 2：路径OK以后检查文件是否存在
            if (fileInfo.Exists)
            {
                return true;
            }
            else
            {
                LogManager.LogToFile("文件不存在: " + rw_path);
                return false;
            }
        }


        /// <summary>
        /// 在可读写路径下生成xml文件，提供xml文本内容,这里因为使用的是rw可读写路径所以可以使用File类<br/>
        /// 需要测试: Android下SD卡被锁或者无sd卡的手机是否能正常创建！
        /// </summary>
        /// <param name="rw_Path">可读写路径+文件名+后缀名</param>
        /// <param name="xmlDoc">要创建的xml文档</param>
        /// <returns>是否创建成功</returns>
        public static bool CreateXML(string rw_Path, XmlDocument xmlDoc)
        {
            StreamWriter sw;
            FileInfo t = new FileInfo(rw_Path);
            if (!t.Exists)
            {
                //如果此文件不存在则创建 
                sw = t.CreateText();
                sw.WriteLine(xmlDoc.InnerXml);
                //关闭流
                sw.Close();
                //销毁流
                sw.Dispose();
                LogManager.LogToFile("XML 创建完毕! " + rw_Path);
                return true;
            }
            return false;
        }

        /// <summary>
        /// 从指定路径下读取xml文件 ,安卓下只能用www加载Streaming下的资源，官方说明：http://docs.unity3d.com/Manual/StreamingAssets.html
        /// </summary>
        /// <param name="strPath">文档路径+文件名+后缀名，可以是只读路径也可以使可读写路径！</param>
        /// <param name="xmlDoc">返回加载后的文档引用</param>
        /// <remarks>
        /// 安卓下使用www只在读取只读路径的时候不需要添加file:///,在SD卡中使用www时，也需要添加file:///<br/>   
        /// 在jar包里的 路径：jar:file:///data/app/com.DreamFactionGame.DreamHeros-2.apk!/assets/Data/Config.xml 这个是只读路径<br/>   
        /// 在SD卡上的  路径：file:////storage/sdcard0/Android/data/com.DreamFactionGame.DreamHeros/files/Data/Config.xml <br/>   
        /// 安卓下单个文件加载时间超过5秒则自动终止加载流程！
        /// </remarks>
        public static void LoadXML(string strPath, ref XmlDocument xmlDoc)
        {
            float safeTime = 5.0f; // 安全时间，用来退出循环使用！

            try
            {
                if (Application.platform == RuntimePlatform.Android)
                {
                    if (!strPath.Contains("apk!/assets/"))
                    {
                        strPath = "file:///" + strPath; // Android下可读写路径(SD卡)也要加file:///
                    }
                    LogManager.LogToFile("XML 开始加载： " + strPath);
                    WWW www = new WWW(strPath);
                    float t_time = Time.realtimeSinceStartup;
                    while (!www.isDone)
                    {
                        // 没加载完继续等待，直至加载完毕再继续！这里没有用IEnumerator 是因为需要有返回参数！协程无法返回参数
                        if (Time.realtimeSinceStartup - t_time > safeTime)
                        {
                            LogManager.LogError("XML 加载超时，强制退出 ： " + strPath);
                        }
                    }

                    if (www.error != null)
                    {
                        LogManager.LogError(www.error);
                    }
                    if (www.isDone)
                    {
                        LogManager.LogToFile("XML 加载完毕! " + strPath);
                        xmlDoc.LoadXml(www.text);
                    }
                }
                else
                {
                    // 苹果，pc系统用以下操作
                    //if (!File.Exists(strPath))
                    //{
                    //    LogManager.LogError("XML 加载错误，文件不存在 ：" + strPath);
                    //}
                    //else
                    //{
                      //  Debug.Log("yao------------" + strPath);
                    LogManager.LogToFile("XML 开始加载： " + strPath);
                    xmlDoc.Load(strPath);

                    LogManager.LogToFile("XML 加载完毕 ： " + strPath);
                  
                }
            }
            catch (System.Exception ex)
            {
                Debug.LogError(ex.Message);
                LogManager.LogToFile(ex.ToString());
            }
           
        }

        private static bool RemoteFileExists(string fileURL)
        {
            HttpWebRequest re = null;
            HttpWebResponse res = null;

            try
            {
                re = (HttpWebRequest)WebRequest.Create(fileURL);
                res = (HttpWebResponse)re.GetResponse();
                if (res.ContentLength != 0)
                {
                    //文件存在
                    return true;
                }
            }
            catch (System.Exception)
            {
                return false;
            }
            finally
            {
                if (re != null)
                {
                    re.Abort();//销毁关闭连接
                }
                if (res != null)
                {
                    res.Close();//销毁关闭响应
                }
            }
            return false;
        }
    }
}