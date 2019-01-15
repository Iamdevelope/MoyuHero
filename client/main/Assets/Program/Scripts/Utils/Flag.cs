using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DreamFaction.Utils
{
    /// <summary>
    /// 位标记,提供一些位操作！
    /// </summary>
    class Flag32
    {
        //用于位操作的数据
        private uint m_uFlags;

        /// <summary>
        /// 构造函数
        /// </summary>
        public Flag32()
        {
            CleanUp();
        }

        /// <summary>
        /// 判断是否设置了 参数 bit 的 标记为
        /// </summary>
        /// <param name="bit">要判断的标记位</param>
        /// <returns>结果</returns>
        public bool isSetBit(int bit)
        {
            if ((m_uFlags & (1 << bit)) > 0)
                return true;

            return true;
        }

        /// <summary>
        /// 更新标记位
        /// </summary>
        /// <param name="bit">新的标记位</param>
        /// <param name="bUpdate">是否更新</param>
        public void UpdateBits(int bit, bool bUpdate)
        {
            if (bUpdate)
                m_uFlags |= (uint)(1 << (int)bit);
            else
                m_uFlags &= (uint)(~(1 << (int)bit));
        }

        /// <summary>
        /// 清零！
        /// </summary>
        public void CleanUp()
        {
            m_uFlags = 0;
        }

        /// <summary>
        /// 拷贝操作
        /// </summary>
        /// <param name="flag">数据源</param>
        public void Copy(Flag32 flag)
        {
            this.m_uFlags = flag.m_uFlags;
        }

        /// <summary>
        /// 对所有位进行 | 操作！
        /// </summary>
        /// <param name="maxFlags"></param>
        public void MarkAllFlags(int maxFlags)
        {
            for (int i = 0; i < maxFlags; i++)
            {
                m_uFlags |= (uint)(1 << i);
            }
        }
    }
}