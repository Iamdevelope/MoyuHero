using System;
using DreamFaction.Interface;

namespace DreamFaction.GameNetWork
{
    /// <summary>
    /// X_GUID 作为客户端和服务器通信的唯一序列号！物品，英雄，技能等都使用序列号方式标注唯一性！
    /// </summary>
    public class X_GUID : IEquatable<X_GUID>, IValid
    {
        /// <summary>
        /// 唯一序列号
        /// </summary>
        private long m_lGuid;
        /// <summary>
        /// 构造函数，执行清理操作CleanUp();
        /// </summary>
        public X_GUID()
        {
            CleanUp();
        }

        public void CreateMonsterGUID()
        {
            byte[] buffer = System.Guid.NewGuid().ToByteArray();
            this.m_lGuid = BitConverter.ToInt64(buffer,0);
        }
        /// <summary>
        /// 比较两个X_GUID是否相等
        /// </summary>
        /// <param name="obj">要比较的X_GUID</param>
        /// <returns>是否相等</returns>
        public bool Equals(X_GUID obj)
        {
            // If this and obj do not refer to the same type, then they are not equal.
            //if (obj.GetType() != this.GetType()) return false;
            return (this.m_lGuid == obj.m_lGuid);
        }

        /// <summary>
        /// 判断X_GUID是否有效<br/>
        /// if ((m_lGuid == ulong.MaxValue) || (m_lGuid == ulong.MinValue))<br/>
        /// </summary>
        /// <returns>是否有效</returns>
        public bool IsValid()
        {
            if (((m_lGuid == long.MaxValue) || (m_lGuid == long.MinValue)))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 获取GUID的值！
        /// </summary>
        /// <returns>GUID的值</returns>
        public long GUID_value
        {
            get { return m_lGuid; }
            set { m_lGuid = value; }
        }

        /// <summary>
        /// 清理操作：m_lGuid = ulong.MaxValue;
        /// </summary>
        public void CleanUp()
        {
            m_lGuid = long.MinValue;
        }

        /// <summary>
        /// 复制操作，将参数的GUID复制到当前GUID中！
        /// </summary>
        /// <param name="guid">要赋值的GUID源</param>
        public void Copy(X_GUID guid)
        {
            this.m_lGuid = guid.m_lGuid;
        }
    }
}