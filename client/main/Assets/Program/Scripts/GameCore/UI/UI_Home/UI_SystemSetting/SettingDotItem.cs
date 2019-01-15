using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using DreamFaction.UI.Core;
using DreamFaction.GameNetWork;
using DG.Tweening;
using DreamFaction.GameEventSystem;
using DreamFaction.Utils;
using GNET;
using System;

public class SettingDotItem : BaseUI
{
    private Text m_HuoLiButText;
    private Text m_ZhaoMuButText;
    private Text m_BossButText;
    private Text m_TanXianButText;
    private Text m_FreeHuoLiButText;

    private GameObject m_HuoLiGouImage;
    private GameObject m_ZhaoMuGouImage;
    private GameObject m_BossGouImage;
    private GameObject m_TanXianGouImage;
    private GameObject m_FreeHuoLiGouImage;

    private Button m_HuoLiBtn;
    private Button m_ZhaoMuBtn;
    private Button m_BossBtn;
    private Button m_TanXianBtn;
    private Button m_FreeHuoLiBtn;


    public override void InitUIData()
    {
        base.InitUIData();

        m_HuoLiButText = selfTransform.FindChild("TuiSong_1/HuoLiButton/Text").GetComponent<Text>();
        m_ZhaoMuButText = selfTransform.FindChild("TuiSong_1/ZhaoMuButton/Text").GetComponent<Text>();
        m_BossButText = selfTransform.FindChild("TuiSong_2/BossButton/Text").GetComponent<Text>();
        m_TanXianButText = selfTransform.FindChild("TuiSong_2/TanXianButton/Text").GetComponent<Text>();
        m_FreeHuoLiButText = selfTransform.FindChild("TuiSong_3/FreeHuoLiButton/Text").GetComponent<Text>();

        m_HuoLiGouImage = selfTransform.FindChild("TuiSong_1/HuoLiButton/GouImage").gameObject;
        m_ZhaoMuGouImage = selfTransform.FindChild("TuiSong_1/ZhaoMuButton/GouImage").gameObject;
        m_BossGouImage = selfTransform.FindChild("TuiSong_2/BossButton/GouImage").gameObject;
        m_TanXianGouImage = selfTransform.FindChild("TuiSong_2/TanXianButton/GouImage").gameObject;
        m_FreeHuoLiGouImage = selfTransform.FindChild("TuiSong_3/FreeHuoLiButton/GouImage").gameObject;

        m_HuoLiBtn = selfTransform.FindChild("TuiSong_1/HuoLiButton").GetComponent<Button>();
        m_HuoLiBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickHuoLiBtn));

        m_ZhaoMuBtn = selfTransform.FindChild("TuiSong_1/ZhaoMuButton").GetComponent<Button>();
        m_ZhaoMuBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickZhaoMuBtn));

        m_BossBtn = selfTransform.FindChild("TuiSong_2/BossButton").GetComponent<Button>();
        m_BossBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickBossBtn));

        m_TanXianBtn = selfTransform.FindChild("TuiSong_2/TanXianButton").GetComponent<Button>();
        m_TanXianBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickTanXianBtn));

        m_FreeHuoLiBtn = selfTransform.FindChild("TuiSong_3/FreeHuoLiButton").GetComponent<Button>();
        m_FreeHuoLiBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickFreeHuoLiBtn));


    }

    // 2：初始化UI显示内容
    public override void InitUIView()
    {
        base.InitUIView();

        m_HuoLiButText.text = GameUtils.getString("System_setting_content7");
        m_ZhaoMuButText.text = GameUtils.getString("System_setting_content8");
        m_BossButText.text = GameUtils.getString("System_setting_content20");
        m_TanXianButText.text = GameUtils.getString("System_setting_content22");
        m_FreeHuoLiButText.text = GameUtils.getString("System_setting_content23");

        if (ObjectSelf.GetInstance().GetSettingData().m_VitalityRemind == 0)
        {
            m_HuoLiGouImage.SetActive(false);
        }
        else
        {
            m_HuoLiGouImage.SetActive(true);
        }
        if (ObjectSelf.GetInstance().GetSettingData().m_FreeRecruit == 0)
        {
            m_ZhaoMuGouImage.SetActive(false);
        }
        else
        {
            m_ZhaoMuGouImage.SetActive(true);
        }
        if (ObjectSelf.GetInstance().GetSettingData().m_WorldBoss == 0)
        {
            m_BossGouImage.SetActive(false);
        }
        else
        {
            m_BossGouImage.SetActive(true);
        }
        if (ObjectSelf.GetInstance().GetSettingData().m_ExplorationTask == 0)
        {
            m_TanXianGouImage.SetActive(false);
        }
        else
        {
            m_TanXianGouImage.SetActive(true);
        }
        if (ObjectSelf.GetInstance().GetSettingData().m_FreeVitality == 0)
        {
            m_FreeHuoLiGouImage.SetActive(false);
        }
        else
        {
            m_FreeHuoLiGouImage.SetActive(true);
        }

    }
    private void OnClickHuoLiBtn()
    {
        if (ObjectSelf.GetInstance().GetSettingData().m_VitalityRemind == 0)
        {
            ObjectSelf.GetInstance().GetSettingData().SetLocalSettingData("VitalityRemind", 1);
            m_HuoLiGouImage.SetActive(true);
        }
        else
        {
            ObjectSelf.GetInstance().GetSettingData().SetLocalSettingData("VitalityRemind", 0);
            m_HuoLiGouImage.SetActive(false);
        }

    }

    private void OnClickZhaoMuBtn()
    {
        if (ObjectSelf.GetInstance().GetSettingData().m_FreeRecruit == 0)
        {
            ObjectSelf.GetInstance().GetSettingData().SetLocalSettingData("FreeRecruit", 1);
            m_ZhaoMuGouImage.SetActive(true);
        }
        else
        {
            ObjectSelf.GetInstance().GetSettingData().SetLocalSettingData("FreeRecruit", 0);
            m_ZhaoMuGouImage.SetActive(false);
        }
    }

    private void OnClickBossBtn()
    {
        if (ObjectSelf.GetInstance().GetSettingData().m_WorldBoss == 0)
        {
            ObjectSelf.GetInstance().GetSettingData().SetLocalSettingData("WorldBoss", 1);
            m_BossGouImage.SetActive(true);
        }
        else
        {
            ObjectSelf.GetInstance().GetSettingData().SetLocalSettingData("WorldBoss", 0);
            m_BossGouImage.SetActive(false);
        }
    }

    private void OnClickTanXianBtn()
    {
        if (ObjectSelf.GetInstance().GetSettingData().m_ExplorationTask == 0)
        {
            ObjectSelf.GetInstance().GetSettingData().SetLocalSettingData("ExplorationTask", 1);
            m_TanXianGouImage.SetActive(true);
        }
        else
        {
            ObjectSelf.GetInstance().GetSettingData().SetLocalSettingData("ExplorationTask", 0);
            m_TanXianGouImage.SetActive(false);
        }
    }

    private void OnClickFreeHuoLiBtn()
    {
        if (ObjectSelf.GetInstance().GetSettingData().m_FreeVitality == 0)
        {
            ObjectSelf.GetInstance().GetSettingData().SetLocalSettingData("FreeVitality", 1);
            m_FreeHuoLiGouImage.SetActive(true);
        }
        else
        {
            ObjectSelf.GetInstance().GetSettingData().SetLocalSettingData("FreeVitality", 0);
            m_FreeHuoLiGouImage.SetActive(false);
        }
    }

}
