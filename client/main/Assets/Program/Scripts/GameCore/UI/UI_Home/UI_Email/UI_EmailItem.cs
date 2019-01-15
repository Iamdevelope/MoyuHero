using UnityEngine;
using UnityEngine.UI;
using DreamFaction.UI.Core;
using System.Collections;
using System.Collections.Generic;
using GNET;
using DreamFaction.Utils;
using DreamFaction.UI.Core;
using System;
using DreamFaction.GameNetWork;
using DreamFaction.GameEventSystem;

public class UI_EmailItem : CellItem 
{
    private Button m_Btn;//自身
    private GameObject LightImage;//亮边
    private GameObject BgImage;

    private GameObject CloseImage;//未打开图标
    private GameObject OpenImage;//已打开图标

    private Text SenderText;//发送者
    private Text TitleText;//标题头
    private Text ReceiveText;//是否可领取
    private Text TimeText;//时间的显示

    private MailData m_Mail = new MailData();
    private int m_ListNum;//列表中的第几个Item

    private int _isOpen;
    private int _isReceive;

    public delegate void m_ClickOnItemHandle(MailData _MailData);
    public event m_ClickOnItemHandle m_ClickOnItem = null;
   
    public override void InitUIData()
    {
        base.InitUIData();
        m_Btn = selfTransform.GetComponent<Button>();
        m_Btn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickBtn));

        LightImage = selfTransform.FindChild("LightImage").gameObject;
        BgImage = selfTransform.FindChild("BgImage").gameObject;

        CloseImage = selfTransform.FindChild("CloseImage").gameObject;
        OpenImage = selfTransform.FindChild("OpenImage").gameObject;

        SenderText = selfTransform.FindChild("SenderText").GetComponent<Text>();
        TitleText = selfTransform.FindChild("TitleText").GetComponent<Text>();
        ReceiveText = selfTransform.FindChild("ReceiveText").GetComponent<Text>();
        TimeText = selfTransform.FindChild("TimeText").GetComponent<Text>();


    }

    // 2：初始化UI显示内容
    public override void InitUIView()
    {
        base.InitUIView();
        ReceiveText.text = GameUtils.getString("mail_content1");
    }

    public void SetImageLight(bool isLight)
    {
        LightImage.SetActive(isLight);
    }

    public void SetOnClickHandle(m_ClickOnItemHandle _ClickOnItemHandlehandle)
    {
        m_ClickOnItem = _ClickOnItemHandlehandle;
    }

    public void SetEmailDate(MailData mail)
    {
        m_Mail = mail;

        if (mail.m_isopen.ToString().Length == 1 )
        {
            _isOpen = int.Parse(mail.m_isopen.ToString());

            if (_isOpen == 0)//没有被查看
            {
                ReceiveText.gameObject.SetActive(true);
                CloseImage.SetActive(true);
                OpenImage.SetActive(false);
            }
            else
            {
                CloseImage.SetActive(false);
                OpenImage.SetActive(true);
                ReceiveText.gameObject.SetActive(true);
            }
        }

        if (mail.m_isopen.ToString().Length >= 2)
        {
            _isOpen = int.Parse(mail.m_isopen.ToString().Substring(1, 1));//是否打开过  0否  1是
            _isReceive = int.Parse(mail.m_isopen.ToString().Substring(0, 1));//是否领取  0否  1是

            if (_isOpen == 0)//没有被查看
            {
                CloseImage.SetActive(true);
                OpenImage.SetActive(false);
            }
            else
            {
                CloseImage.SetActive(false);
                OpenImage.SetActive(true);
            }

            if (_isReceive == 1)
            {
                ReceiveText.gameObject.SetActive(false);
            }
            else
            {
                ReceiveText.gameObject.SetActive(true);
            }
        }

        
        if (mail.m_title.Length > 12)
        {
            TitleText.text = GameUtils.getString(mail.m_title).Substring(0, 6) + "......";
        }
        else
        {
            TitleText.text = GameUtils.getString(mail.m_title).ToString();
        }
        if (mail.m_sender != null)
        { 
            SenderText.text = GameUtils.getString(mail.m_sender);
        }
        

        DateTime dateTime = TimeUtils.ConverMillionSecToDateTime(mail.m_endtime, ObjectSelf.GetInstance().ServerTimeZone);
        TimeSpan ts = TimeUtils.GetTimeSpan(ObjectSelf.GetInstance().ServerDateTime, dateTime);

        if (ts.TotalSeconds < (double)60)
        {
            TimeText.text = GameUtils.getString("mail_content21").Replace("{0}", "");
        }
        else if (ts.TotalSeconds > (double)60 && ts.TotalSeconds < (double)3600)
        {
            TimeText.text = Mathf.FloorToInt((float)ts.TotalSeconds / 60).ToString() + GameUtils.getString("mail_content20").Replace("{0}", "");
        }
        else if (ts.TotalSeconds > (double)3600 && ts.TotalSeconds < (double)86400)
        {
            TimeText.text = Mathf.FloorToInt((float)ts.TotalSeconds / 3600).ToString() + GameUtils.getString("mail_content19").Replace("{0}", "");
        }
        else if (ts.TotalSeconds > (double)86400 && ts.TotalSeconds < (double)604800)
        {
            TimeText.text = Mathf.FloorToInt((float)ts.TotalSeconds / 86400).ToString() + GameUtils.getString("mail_content18").Replace("{0}", "");
        }
    }

    public void OnClickBtn()
    {
       if (_isOpen == 0)
       {
        CReceiveMail _CReceiveMail = new CReceiveMail();
        _CReceiveMail.mailkey = m_Mail.m_key;
        _CReceiveMail.isget = 0;
        IOControler.GetInstance().SendProtocol(_CReceiveMail);
       }

       if (m_ClickOnItem != null)
        {
            m_ClickOnItem(m_Mail);
        }
    }
}
