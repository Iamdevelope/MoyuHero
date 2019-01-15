using UnityEngine;
using System.Collections;
using System;

namespace DreamFaction.GameNetWork.Data
{
    public class MysticData
    {

        private int m_MysticType;		//秘术的类型id
        private int m_MysticLevel;      //秘术等级
        private int m_MysticExp;      //秘术等级

        public MsTemplate m_CurMsDataT;		//当前秘术的表数据


        public MysticData()
        {
            CleanUp();
        }
        public MysticData(int type,string levelAndExp)
        {
            m_MysticType = type;
            string[] typeLevel = levelAndExp.Split(new string[] { "|" }, StringSplitOptions.None); 
            m_MysticLevel = int.Parse(typeLevel[0]);
            m_MysticExp = int.Parse(typeLevel[1]);
            //查找对应类型的表数据
            foreach (var item in DataTemplate.GetInstance().m_MsTable.getData())
            {
                if (item.Value == null)
                {
                    continue;
                }

                MsTemplate MsDataT = item.Value as MsTemplate;
                if (MsDataT.getId() == type)
                {
                    m_CurMsDataT = MsDataT;
                }
            }
        }

        public int GetMysticType
        {
            get { return m_MysticType; }
            //set { m_MysticType = value; }
        }

        public int GetMysticLevel
        {
            get { return m_MysticLevel; }
            //set { m_MysticLevel = value; }
        }
        public int GetMysticExp
        {
            get { return m_MysticExp; }
            //set { m_MysticLevel = value; }
        }

        /// <summary>
        /// 返回秘术的加成值
        /// </summary>
        /// <returns></returns>
        public int GetMysticAddValue()
        {
            for (int i = 0; i < m_CurMsDataT.getLevel().Length;i++ )
            {
                if (m_CurMsDataT.getLevel()[i] == m_MysticLevel)
                {
                    return m_CurMsDataT.getValue()[i];
                }
            }
            return -1;
        }

        public void Copy(int type, int level)
        {
            m_MysticType = type;
            m_MysticLevel = level;

            //查找对应类型的表数据
            foreach (var item in DataTemplate.GetInstance().m_MsTable.getData())
            {
                if (item.Value == null)
                {
                    continue;
                }

                MsTemplate MsDataT = item.Value as MsTemplate;
                if (MsDataT.getId() == type)
                {
                    m_CurMsDataT = MsDataT;
                }
            }
        }

        public void CleanUp()
        {
            m_MysticType = -1;
            m_MysticLevel = -1;
            m_CurMsDataT = null;
        }
    }
}
