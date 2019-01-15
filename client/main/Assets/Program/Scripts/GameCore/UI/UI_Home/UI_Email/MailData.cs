using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DreamFaction.GameCore;
using DreamFaction.GameNetWork;
using DreamFaction.GameNetWork.Data;
using GNET;
using DreamFaction.GameSceneEditor;
using DreamFaction.Utils;
using DreamFaction.LogSystem;
using DreamFaction.UI;
using DreamFaction.UI.Core;

public class MailItemData
{
    public int m_objectid; // 物品ID
    public int m_dropnum; // 数量
    public int m_dropparameter1; // 附加条件1
    public int m_dropparameter2; // 附加条件2

    public void Copy(GNET.MailItem data)
    {
        this.m_objectid = data.objectid;
        this.m_dropnum = data.dropnum;
        this.m_dropparameter1 = data.dropparameter1;
        this.m_dropparameter2 = data.dropparameter2;
    }
}
public class MailData
{
    public int m_key; // 邮件唯一ID
    public string m_sender; // 发送者
    public string m_title; // 邮件标题
    public string m_msg; // 消息内容
    public List<int> m_innerdropidlist = new List<int>(); // 掉落包ID
    public List<MailItemData> m_items = new List<MailItemData>(); // 掉落物品（非掉落包内容）
    public long m_endtime; // 开始时间
    public int m_isopen;      //个位为是否打开，十位为是否领取 0否，1是
    public List<string> m_strlist = new List<string>(); // 参数列表

    public void Copy(Mail _data)
    {
        this.m_key = _data.key;
        this.m_sender = _data.sender;
        this.m_title = _data.title;
        this.m_msg = _data.msg;
        this.m_endtime = _data.endtime;
        this.m_isopen = _data.isopen;

        foreach (int value in _data.innerdropidlist)
        {
            m_innerdropidlist.Add((int)value);
        }

        foreach (MailItem value in _data.items)
        {
            MailItemData _itemData = new MailItemData();
            _itemData.Copy(value);
            m_items.Add(_itemData);
        }

        foreach (string value in _data.strlist)
        {
            m_strlist.Add(value.ToString());
        }
    }
}

public class MailManager
{

    public List<MailData> m_MailList = new List<MailData>();
    public int mailallsize;//邮件总数
    public bool m_HaveNewMail = false;  //是否有新邮件

    public void CopyInfo(LinkedList<Mail> _mail)
    {
        m_MailList.Clear();
        
        foreach (var value in _mail)
        {
            MailData _data = new MailData();
            _data.Copy(value);
            m_MailList.Add(_data);
        }
    }

    public void GetMoreCopyInfo(LinkedList<Mail> _mail)
    {
        foreach (var value in _mail)
        {
            MailData _data = new MailData();
            _data.Copy(value);
            m_MailList.Add(_data);
        }
    }

    public void CopyOneMailDataInfo(Mail _mail)
    {
        MailData _data = new MailData();
        _data.Copy(_mail);

        for (int i = 0; i < m_MailList.Count; i++)
        {
            if (m_MailList[i].m_key == _data.m_key)
            {
                m_MailList[i] = _data;
            }
        }
    }


    public void RequestSeverListData(int key)
    {
        CGetMailList _CGetMailList = new CGetMailList();
        _CGetMailList.mailsize = key;
        IOControler.GetInstance().SendProtocol(_CGetMailList);
    }

    List<int> haveRead = new List<int>();
    public void CheckHaveRead()
    {
        int _isOpen;
        haveRead.Clear();

        for (int i = 0; i < m_MailList.Count; i++)
        {
            if (m_MailList[i].m_isopen.ToString().Length == 1)
            {
                _isOpen = int.Parse(m_MailList[i].m_isopen.ToString());
                if (_isOpen == 1)
                {
                    haveRead.Add(m_MailList[i].m_key);
                }
            }
            if (m_MailList[i].m_isopen.ToString().Length == 2)
            {
                _isOpen = int.Parse(m_MailList[i].m_isopen.ToString().Substring(1, 1));
                if (_isOpen == 1)
                {
                    haveRead.Add(m_MailList[i].m_key);
                }
            }
        }

        for (int i = 0; i < m_MailList.Count; i++)
        {
            if (m_MailList[i].m_isopen.ToString().Length == 1)
            {
                _isOpen = int.Parse(m_MailList[i].m_isopen.ToString());//是否打开过  0否  1是

                if (_isOpen == 1)
                {
                    haveRead.Add(m_MailList[i].m_key);
                    UI_RechargeBox box = UI_HomeControler.Inst.AddUI(UI_RechargeBox.UI_ResPath).GetComponent<UI_RechargeBox>();

                    if (box == null)
                    {
                        DreamFaction.LogSystem.LogManager.LogError("提示窗is null");
                        return;
                    }

                    box.SetDescription_text(GameUtils.getString("mail_content5"));
                    box.SetIsNeedDescription(false);
                    box.SetLeftBtn_text(GameUtils.getString("common_button_ok"));
                    box.SetLeftYesMid();
                    box.SetLeftClick(ONLeftBtnClick);

                    box.SetRightBtn_text(GameUtils.getString("common_button_close"));
                    return;
                }
            }
        }

        if (haveRead.Count == 0)
        {
            UI_EmailManager.Inst.NoMoreEmail();
            return;
        }

        ONLeftBtnClick();
    }

    public void ONLeftBtnClick()
    {
        DeleHaveRead();

        UI_HomeControler.Inst.ReMoveUI(UI_RechargeBox.UI_ResPath);
    }

    public void DeleHaveRead()
    {
        CRemoveMailList _CRemoveMailList = new CRemoveMailList();   
        IOControler.GetInstance().SendProtocol(_CRemoveMailList);
    }
}


