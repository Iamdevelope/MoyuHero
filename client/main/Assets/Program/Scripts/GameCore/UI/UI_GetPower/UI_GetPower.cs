using UnityEngine;
using System.Collections;
using DreamFaction.UI.Core;
using UnityEngine.UI;
using DreamFaction.GameNetWork;
using System;
using DreamFaction.GameEventSystem;
using DreamFaction.GameCore;
using DreamFaction.UI;
using System.Text;
using DreamFaction.Utils;
using GNET;
public class UI_GetPower : UI_GetPowerManage
{

    public static string UI_ResPath = "UI_Home/UI_GetPower_2_2";
    public static UI_GetPower _instance;
    private long m_severTime;
    TimeInfoHM m_TimeNoonMin;                       //中午时段开始时间
    TimeInfoHM m_TimeNoonMax;                       //中午时段关闭时间
    TimeInfoHM m_TimeNightMin;                      //晚上时段开始时间
    TimeInfoHM m_TimeNightMax;                      //晚上时段关闭时间
    TimeInfoHM m_TimeMadrugadaMin;                  //每天开始时间
    TimeInfoHM m_TimeMadrugadaMax;                  //每天结束时间
    TimeInfoHM m_TimeNow;                           //当前时间
    private int m_NoonNum;                          //中午是否领取 1为领取 0为未领取
    private int m_NightNum;                         //晚上是否领取 1为领取 0为未领取
    public override void InitUIData()
    {
        base.InitUIData();
        _instance = this;
        m_TimeNow = new TimeInfoHM();
        m_severTime = ObjectSelf.GetInstance().ServerTime + (int)Time.time;
        m_TimeNoonMin = new TimeInfoHM();
        m_TimeNoonMin.hour = 12;
        m_TimeNoonMin.minute = 0;
        m_TimeNoonMax = new TimeInfoHM();
        m_TimeNoonMax.hour = 13;
        m_TimeNoonMax.minute = 0;
        m_TimeNightMin = new TimeInfoHM();
        m_TimeNightMin.hour = 18;
        m_TimeNightMin.minute = 0;
        m_TimeNightMax = new TimeInfoHM();
        m_TimeNightMax.hour = 19;
        m_TimeNightMax.minute = 0;
        m_TimeMadrugadaMin = new TimeInfoHM();
        m_TimeMadrugadaMin.hour = 0;
        m_TimeMadrugadaMin.minute = 0;
        m_TimeMadrugadaMax = new TimeInfoHM();
        m_TimeMadrugadaMax.hour = 23;
        m_TimeMadrugadaMax.minute = 59;
        string[] _Time = DataTemplate.GetInstance().m_GameConfig.getAp_get_time();
        m_NoontimeDes.text = _Time[0];
        m_NightDes.text = _Time[1];
        InvokeRepeating("GetServerTime", 0, 1);
        InvokeRepeating("RenewalUIShow", 0, 1);

        UI_CaptionManager cap = UI_CaptionManager.GetInstance();
        if (cap != null)
            cap.AwakeUp(M_CapPos);

        m_Name.text = GameUtils.getString("muse_music_title");
        m_GetPowerText.text = GameUtils.getString("muse_music_content5");

        GameEventDispatcher.Inst.addEventListener(GameEventID.UI_GetPower, GetPowerTip);
    }

    protected void OnDestroy()
    {
        UI_CaptionManager cap = UI_CaptionManager.GetInstance();
        if (cap != null)
            cap.Release(M_CapPos);
        GameEventDispatcher.Inst.removeEventListener(GameEventID.UI_GetPower, GetPowerTip);
    }

    public void RenewalUIShow()
    {
        int _typeNum = ObjectSelf.GetInstance().IsGetPower;
        m_NoonNum = _typeNum % 10;
        m_NightNum = _typeNum / 10;
        
        if (TimeUtils.IsInHourTimeDuration(m_TimeNow,m_TimeNoonMin,m_TimeNoonMax))
        {
           
            m_NoontimeDesText.color = Color.green;
            
            if (m_NoonNum==1)
            {
                m_NoontimeDesText.color = new Color(0.69f,1f,0.36f);//绿色
               // GameUtils.SetBtnSpriteGrayState(m_GetPowerBtn, true);  
                m_NoontimeDesText.text = GameUtils.getString("muse_music_content2");
            }
            else
            {
                m_NoontimeDesText.color = new Color(0.69f, 1f, 0.36f);
               // GameUtils.SetBtnSpriteGrayState(m_GetPowerBtn, false);
                m_NoontimeDesText.text = GameUtils.getString("muse_music_content3");
            }
        }
        else
        {
            m_NoontimeDes.color = Color.white;
            if (TimeUtils.IsInHourTimeDuration(m_TimeNow,m_TimeMadrugadaMin,m_TimeNoonMin))
            {
                m_NoontimeDesText.color = Color.white;
                m_NoontimeDesText.text = GameUtils.getString("muse_music_content3");
            }
            else
            {
                if (m_NoonNum == 1)
                {
                    m_NoontimeDesText.color = new Color(0.69f, 1f, 0.36f);
                    m_NoontimeDesText.text = GameUtils.getString("muse_music_content2");
                }
                else
                {
                    m_NoontimeDesText.color = new Color(0.98f, 0.32f, 0.41f);
                    m_NoontimeDesText.text = GameUtils.getString("muse_music_content4");
                }
            }
        }

        if (TimeUtils.IsInHourTimeDuration(m_TimeNow,m_TimeNightMin,m_TimeNightMax))
        {
            m_NightDes.color = Color.green;
           
            if (m_NightNum== 1)
            {
                m_NightDesText.color = new Color(0.69f, 1f, 0.36f);
               // GameUtils.SetBtnSpriteGrayState(m_GetPowerBtn, true);
                m_NightDesText.text = GameUtils.getString("muse_music_content2");
            }
            else
            {
                m_NightDesText.color = new Color(0.69f, 1f, 0.36f);
                //GameUtils.SetBtnSpriteGrayState(m_GetPowerBtn, false);
                m_NightDesText.text = GameUtils.getString("muse_music_content3");
            }
        }
        else
        {
            m_NightDes.color = Color.white;
            if (TimeUtils.IsInHourTimeDuration(m_TimeNow, m_TimeMadrugadaMin, m_TimeNightMin))
            {
                m_NightDesText.color = Color.white;
                m_NightDesText.text = GameUtils.getString("muse_music_content3");
            }
            else
            {
                if (m_NightNum == 1)
                {
                    m_NightDesText.color = new Color(0.69f, 1f, 0.36f);
                    m_NightDesText.text = GameUtils.getString("muse_music_content2");
                }
                else
                {
                    m_NightDesText.color = new Color(0.98f, 0.32f, 0.41f);
                    m_NightDesText.text = GameUtils.getString("muse_music_content4");
                }
            }
        }
        if (TimeUtils.IsInHourTimeDuration(m_TimeNow, m_TimeNoonMin, m_TimeNoonMax) && m_NoonNum==0)
        {
            GameUtils.SetBtnSpriteGrayState(m_GetPowerBtn, false);
        }
        else if (TimeUtils.IsInHourTimeDuration(m_TimeNow, m_TimeNightMin, m_TimeNightMax) && m_NightNum==0)
        {
            GameUtils.SetBtnSpriteGrayState(m_GetPowerBtn, false);
        }
        else
        {
            GameUtils.SetBtnSpriteGrayState(m_GetPowerBtn, true);
        }


    }
    void GetServerTime()
    {
        //Debug.Log(m_severTime);
        DateTime dt = ObjectSelf.GetInstance().ServerDateTime;
        m_TimeText.text = dt.ToString("HH:mm");
        m_TimeNow.hour = dt.Hour;
        m_TimeNow.minute = dt.Minute;
    }

    protected override void OnClickGetPowerBtn()
    {
        base.OnClickGetPowerBtn();

        

            if (TimeUtils.IsInHourTimeDuration(m_TimeNow, m_TimeNoonMin, m_TimeNoonMax))
            {
                if (m_NoonNum==1)
                {

                    InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("muse_music_tip3"), MsgBoxGroup);
                }
                else
                {
                    if (ObjectSelf.GetInstance().ActionPoint >= ObjectSelf.GetInstance().ActionPointMax)
                    {
                        InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("muse_music_tip7"), MsgBoxGroup);
                    }
                    else
                    {
                        CGetMSZQ cMSZQ = new CGetMSZQ();
                        IOControler.GetInstance().SendProtocol(cMSZQ);
                    }
                }
            }
            else if (TimeUtils.IsInHourTimeDuration(m_TimeNow, m_TimeNightMin, m_TimeNightMax))
            {
                if (m_NightNum == 1)
                {
                    InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("muse_music_tip2"), MsgBoxGroup);
                }
                else
                {
                    if (ObjectSelf.GetInstance().ActionPoint >= ObjectSelf.GetInstance().ActionPointMax)
                    {
                        InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("muse_music_tip7"), MsgBoxGroup);
                    }
                    else
                    {
                        CGetMSZQ cMSZQ = new CGetMSZQ();
                        IOControler.GetInstance().SendProtocol(cMSZQ);
                    }
                }
            }
            else if (TimeUtils.IsInHourTimeDuration(m_TimeNow,m_TimeMadrugadaMin,m_TimeNoonMin))
            {
                InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("muse_music_tip4"), MsgBoxGroup);
            }
            else if (TimeUtils.IsInHourTimeDuration(m_TimeNow,m_TimeNoonMax, m_TimeNightMin))
            {
                InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("muse_music_tip5"), MsgBoxGroup);
            }
            else if (TimeUtils.IsInHourTimeDuration(m_TimeNow, m_TimeNightMax, m_TimeMadrugadaMax))
            {
                InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("muse_music_tip6"), MsgBoxGroup);
            }
            
    }

    public void GetPowerTip()
    {
        InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("muse_music_tip1"), MsgBoxGroup);
        RenewalUIShow();
    }

    //关闭界面
    protected override void OnClickBackBtn()
    {
        base.OnClickBackBtn();
        UI_HomeControler.Inst.ReMoveUI(gameObject);
    }

}
