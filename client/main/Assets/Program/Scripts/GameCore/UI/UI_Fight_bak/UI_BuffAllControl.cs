// 全体buff控制

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using DreamFaction.GameNetWork;
using DreamFaction.UI.Core;
using DreamFaction.SkillCore;
using DreamFaction.LogSystem;
using DreamFaction.Utils;

namespace DreamFaction.UI
{
    public class UI_BuffAllControl : MonoBehaviour
    {
        // 当前buff列表
        List<BuffIcon> mBuffList = new List<BuffIcon>();
        // debuff头位置
        private int iDebuffIdx = 0; 
        // 添加buff
        public void AddBuff(BuffTemplate info)
        {
            BuffIcon buffIcon = GetItemByBuffID(info.GetID());
            if (info.getMaxOverlayCount() > 1)
            {
                // 可叠加
                if (buffIcon!=null)
                {
                    // 更新数字
                    buffIcon.icon.onUpdateText(++buffIcon.iCount);
                }
                else
                {
                    // 创建icon
                    onCreateBuffIcon(info);
                }
            }
            else
            {
                // 不可叠加
                onCreateBuffIcon(info);
            }
        }
        // 移除buff
        public void RemoveBuff(BuffTemplate info)
        {
            BuffIcon buffIcon = GetItemByBuffID(info.GetID());

            if (buffIcon == null)
                return;
            if (info.getMaxOverlayCount() > 1)
            {
                // 可叠加
                if (buffIcon!=null)
                {
                    if (buffIcon.iCount < 2)
                    {
                        // 移除icon
                        if (buffIcon.icon)
                        {
                            //buffIcon.icon.transform.DetachChildren()
                            Destroy(buffIcon.icon.gameObject);
                            buffIcon.icon = null;
                        }
                        mBuffList.Remove(buffIcon);
                        buffIcon = null;
                    }
                    else
                    {
                        // 计数--
                        buffIcon.icon.onUpdateText(--buffIcon.iCount);
                    }
                }
            }
            else
            {
                // 不可叠加
                if (buffIcon.icon)
                {
                    Destroy(buffIcon.icon.gameObject);
                    buffIcon.icon = null;
                }
                mBuffList.Remove(buffIcon);
                buffIcon = null;
            }
        }

        // 创建bufficon结构
        private void onCreateBuffIcon(BuffTemplate info)
        {
            int buffid = info.GetID();
            GameObject obj = new GameObject("BuffIcon");
            obj.transform.SetParent(transform, false);

            UI_BuffIcon icon = obj.AddComponent<UI_BuffIcon>();

            //buff icon路径
            //string filepath = "";// info.m_buffIconBig;
            string filepath = info.getBuffIconSmall();//m_buffIconSmall;
            if (string.IsNullOrEmpty(filepath))
            {
                Debug.LogError("找不到资源：" + info.getDES());
                return;
            }            
            filepath = common.defaultPath + filepath;
            //string filepath = common.defaultPath + "Battle_Buff04";
            //LogManager.LogError("Should use table filepath ...");
            if (UIResourceMgr.LoadSprite(filepath) == null) return;
            Sprite sprite = Instantiate(UIResourceMgr.LoadSprite(filepath), Vector3.zero, Quaternion.identity) as Sprite;
            icon.onImageInit(sprite);

            BuffIcon buffIcon = new BuffIcon(icon,buffid);
            mBuffList.Add(buffIcon);

            // buff/debuff 排序
            if (info.getConduce() != 0)
            {
                // buff 排在debuff前
                obj.transform.SetSiblingIndex(iDebuffIdx);
                iDebuffIdx++;
            }
            else
            {
                // debuff 排在队尾
                // 所以不处理
            }
        }

        private BuffIcon GetItemByBuffID(int id)
        {
            BuffIcon ret = null;
            for (int idx = 0; idx < mBuffList.Count; idx++)
            {
                if (mBuffList[idx].iBuffID == id)
                {
                    ret = mBuffList[idx];
                    break;
                }
            }
            return ret;
        }
    }

    class BuffIcon
    {
        public int iBuffID; 
        public int iCount;  // 叠加数量
        public UI_BuffIcon icon;   // 对应icon

        public BuffIcon(UI_BuffIcon _icon,int buffid)
        {
            iBuffID = buffid;
            iCount = 1;
            icon = _icon;
        }

    }
}
