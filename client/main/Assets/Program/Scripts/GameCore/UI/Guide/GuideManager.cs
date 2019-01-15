using UnityEngine;
using System.Collections;
using DreamFaction.UI.Core;
using UnityEngine.UI;
using System.Collections.Generic;
using DreamFaction.GameCore;
using DreamFaction.GameEventSystem;
using DreamFaction.GameNetWork;
using GNET;

using UnityEngine.Events;

public class GuideManager
{
    private static GuideManager _inst = null;

    List<int> m_GuideIDList = new List<int>();

    NewbieguideTemplate m_NewTemp;
    private int m_InterruptID;

    public int interruptID
    {
        get
        {
            return m_InterruptID;
        }
        set
        {
            m_InterruptID = value;
        }
    }


    /// <summary>
    /// 是否引导取名
    /// </summary>
    public bool isIntitle
    {
        get
        {
            if (guideIDList.Count <= 0)
                return true;
            return false;
        }
    }

    /// <summary>
    /// 是否开启强制引导
    /// </summary>
    public bool isGuideUser
    {
        get
        {
            if (guideIDList.Count <= 0)
                return true;
            else
            {
                if (guideIDList[guideIDList.Count - 1] < 100501)
                {
                    return true;
                }
            }

            return false;
        }
    }

    public static GuideManager GetInstance()
    {
        if (_inst == null)
        {
            _inst = new GuideManager();
            // 屏蔽新手引导
            if(_inst.guideIDList.Count <= 0)
                _inst.ShieldGuide();
        }

        return _inst;
    }

    public List<int> guideIDList
    {
        get
        {
            return m_GuideIDList;
        }
    }

    // 屏蔽新手引导
    public void ShieldGuide()
    {
        CSendCommand command = new CSendCommand();
        command.cmd = "gmpassnew";
        IOControler.GetInstance().SendProtocol(command);

        for (int i = 100100; i < 300605; ++i)
            GuideManager.GetInstance().guideIDList.Add(i);

        try
        {
            UI_HomeControler.Inst.ReMoveUI(UI_Intitle.UI_ResPath);
            UI_HomeControler.Inst.ReMoveUI(UI_Guide.UI_ResPath);
        }
        catch
        {

        }
    }

    /// <summary>
    /// 
    /// </summary>
    public int CheckWhetherNeedGuide()
    {
        string[] _stageArray = new string[] { "1-2", "1-4", "1-5", "1-6" };//,"2-1","2-5","2-7","3-1","3-3","3-6","3-8","4-1"};
        int[] _stageIdArray = new int[] { 200101, 200201, 200301, 200401 };
        for (int i = 0; i < _stageArray.Length; i++)
        {
            string _str = _stageArray[i];
            string[] _strArray = _str.Split('-');
            //通过了关卡_stageArray[i]
            if (CheckThroughCheckPoint(int.Parse(_strArray[0]), int.Parse(_strArray[1])))
            {
                if (IsContentGuideID(_stageIdArray[i]) == false)
                {
                    return _stageIdArray[i];
                }
            }
        }

        return -1;
    }


    /// <summary>
    /// id是否包含在 服务器返回的需要引导的id列表中
    /// </summary>
    /// <returns></returns>
    public bool IsContentGuideID(int id)
    {
        for (int i = 0; i < m_GuideIDList.Count; i++)
        {
            if (m_GuideIDList.Contains(id))
            {
                return true;
            }
        }
        return false;
    }
    /// <summary>
    /// 返回新手引导列表最后一个如果等于传进来ID
    /// </summary>
    /// <param name="value">新手引导ID</param>
    /// <returns>是否相等</returns>
    public bool GetBackCount(int value)
    {
        if (guideIDList.Count <= 0)
            return false;

        if (guideIDList[guideIDList.Count - 1] == value)
        {
            return true;
        }
        return false;
    }

    public int GetLastID()
    {
        if (guideIDList.Count > 0)
        {
            return guideIDList[guideIDList.Count - 1];
        }

        return -1;
    }

    public void RemoveGuideID(int id)
    {
        if (guideIDList.Contains(id))
        {
            guideIDList.Remove(id);
        }
    }

    public void AddGuideID(int id)
    {
        guideIDList.Add(id);
    }
    /// <summary>
    /// 是否通过了指定关卡
    /// </summary>
    /// <param name="_pieceNum"></param>
    /// <param name="stageId"></param>
    /// <returns></returns>
    private bool CheckThroughCheckPoint(int _pieceNum, int stageId)
    {
        int _num;
        ChapterinfoTemplate chapterinfo = (ChapterinfoTemplate)DataTemplate.GetInstance().m_ChapterTable.getTableData(_pieceNum);
        int[] levelID = chapterinfo.getStageID();

        if (ObjectSelf.GetInstance().BattleStageData.IsCopyScenePass(_pieceNum, levelID[stageId - 1], out _num))
        {
            return true;
        }
        return false;
    }

    // 下一步指引的 ID
    public int GetNextGuideID()
    {
        if (m_NewTemp != null)
        {
            return m_NewTemp.getSkip_to();
        }

        return -1;
    }

    // 通过要引导的 index，显示引导
    public void ShowGuideWithIndex(int index, UnityAction e = null)
    {
        return ;

        // 1 读表数据 得到表
        m_NewTemp = (NewbieguideTemplate)DataTemplate.GetInstance().m_NewbieguideTable.getTableData(index);
        // 2 判断是否应该引导

        if (m_NewTemp == null)
        {
            Debug.LogError("无效的指引 ID ");
            return;
        }

        if (UI_Guide.inst == null)
        {
            if (SceneManager.Inst.CurScene == SceneEntry.Home.ToString())
            {
                UI_HomeControler.Inst.AddUI(UI_Guide.UI_ResPath);
            }
            else if (SceneManager.Inst.CurScene == SceneEntry.Fight.ToString())
            {
                UI_FightControler.Inst.AddUI(UI_Guide.UI_ResPath);
            }
        }

        UI_Guide.inst.GuideWithInfo(m_NewTemp);

        if (e != null)
        {
            UI_Guide.inst.m_LeaveBtn.onClick.AddListener(e);
        }
        SendMessage(index);
    }




    public bool CheckInterruptGuide(int index)
    {

        return true;
    }

    /// <summary>
    /// 给服务器发送指定引导的 ID
    /// </summary>
    /// <param name="index"></param>
    public void SendMessage(int index)
    {
        guideIDList.Add(index);
        CNewyindao proto = new CNewyindao();
        proto.num = index;
        IOControler.GetInstance().SendProtocol(proto);
    }

    // 显示指引


    // 下一步引导
    public void ShowNextGuide()
    {
        return;
        // 小组引导结束
        if (m_NewTemp.getStop_type() == 1)
        {
            StopGuide();
            return;
        }

        if (m_NewTemp.getSkip_to() != -1)
        {
            m_NewTemp = (NewbieguideTemplate)DataTemplate.GetInstance().m_NewbieguideTable.getTableData(m_NewTemp.getSkip_to());
            if (UI_Guide.inst == null)
            {
                if (SceneManager.Inst.CurScene == SceneEntry.Home.ToString())
                {
                    UI_HomeControler.Inst.AddUI(UI_Guide.UI_ResPath);
                }
                else if (SceneManager.Inst.CurScene == SceneEntry.Fight.ToString())
                {
                    UI_FightControler.Inst.AddUI(UI_Guide.UI_ResPath);
                }
            }

            UI_Guide.inst.GuideWithInfo(m_NewTemp);
        }
        else
        {
            StopGuide();
        }
    }

    // 停止新手引导
    public void StopGuide()
    {
        if (UI_Guide.inst != null)
        {
            if (SceneManager.Inst.CurScene == SceneEntry.Home.ToString())
            {
                UI_HomeControler.Inst.ReMoveUI(UI_Guide.UI_ResPath);
            }
            else if (SceneManager.Inst.CurScene == SceneEntry.Fight.ToString())
            {
                UI_FightControler.Inst.ReMoveUI(UI_Guide.UI_ResPath);
            }
        }

        if (m_NewTemp != null)
            GameEventDispatcher.Inst.dispatchEvent(GameEventID.G_Guide_Stop_Type, m_NewTemp.GetID());
    }
}
