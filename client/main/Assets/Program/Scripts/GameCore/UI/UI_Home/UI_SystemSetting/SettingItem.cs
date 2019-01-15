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
public class SettingItem : BaseUI
{
        /// <summary>
    /// 系统设置界面，继承自BaseUI
    /// </summary>
        private Text m_TypeText;

        private Text m_LeftButText;
        private Text m_MiddleButText;
        private Text m_RightButText;

        private Outline m_LeftOutline;
        private Outline m_MiddkeOutline;
        private Outline m_RightOutline;

        private GameObject m_LeftButImage;
        private GameObject m_MiddleButImage;
        private GameObject m_RightButImage;

        private Button m_LeftBtn;
        private Button m_MiddleBtn;
        private Button m_RightBtn;

        public string SettingType;
        
        public override void InitUIData()
        {
            base.InitUIData();
            m_TypeText = selfTransform.FindChild("Button/Text").GetComponent<Text>();

            m_LeftButText = selfTransform.FindChild("LeftButton/Text").GetComponent<Text>();
            m_MiddleButText = selfTransform.FindChild("MiddleButton/Text").GetComponent<Text>();
            m_RightButText = selfTransform.FindChild("RightButton/Text").GetComponent<Text>();

            m_LeftOutline = selfTransform.FindChild("LeftButton/Text").GetComponent<Outline>();
            m_MiddkeOutline  = selfTransform.FindChild("MiddleButton/Text").GetComponent<Outline>();
            m_RightOutline = selfTransform.FindChild("RightButton/Text").GetComponent<Outline>();

            m_LeftButImage = selfTransform.FindChild("LeftButton/Image").gameObject;
            m_MiddleButImage = selfTransform.FindChild("MiddleButton/Image").gameObject;
            m_RightButImage = selfTransform.FindChild("RightButton/Image").gameObject;

            m_LeftBtn = selfTransform.FindChild("LeftButton").GetComponent<Button>();
            m_LeftBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickLeftBtn));
            m_MiddleBtn = selfTransform.FindChild("MiddleButton").GetComponent<Button>();
            m_MiddleBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickMiddleBtn));
            m_RightBtn = selfTransform.FindChild("RightButton").GetComponent<Button>();
            m_RightBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickRightBtn));


        }

        // 2：初始化UI显示内容
        public override void InitUIView()
        {
            base.InitUIView();

            switch (SettingType)
            {
                case "BattleMode":
                    BattleModeShow();
                    break;
                case "Music":
                    MusicShow();
                    break;
                case "Sound":
                    SoundShow();
                    break;
                case "ImageQuality":
                    ImageQualityShow();
                    break;
            }
        }
     
    private void BattleModeShow()
    {
        m_TypeText.text = GameUtils.getString("System_setting_content3");
        m_LeftButText.text = GameUtils.getString("System_setting_content9");
        m_MiddleButText.text = GameUtils.getString("");
        m_RightButText.text = GameUtils.getString("System_setting_content10");
        if (ObjectSelf.GetInstance().GetSettingData().m_BattleMode == 0)
        {
            LeftSelect();
        }
        else
        {
            RightSelect();
        }
    }

    private void MusicShow()
    {
        m_TypeText.text = GameUtils.getString("System_setting_content4");
        m_LeftButText.text = GameUtils.getString("System_setting_content18");
        m_MiddleButText.text = GameUtils.getString("");
        m_RightButText.text = GameUtils.getString("System_setting_content19");
        if (ObjectSelf.GetInstance().GetSettingData().m_Music == 0)
        {
            LeftSelect();
        }
        else
        {
            RightSelect();
        }
     }

    private void SoundShow()
    {
        m_TypeText.text = GameUtils.getString("System_setting_content5");
        m_LeftButText.text = GameUtils.getString("System_setting_content18");
        m_MiddleButText.text = GameUtils.getString("");
        m_RightButText.text = GameUtils.getString("System_setting_content19");
        if (ObjectSelf.GetInstance().GetSettingData().m_Sound == 0)
        {
            LeftSelect();
        }
        else
        {
            RightSelect();
        }
    }

    private void ImageQualityShow()
    {
        m_TypeText.text = GameUtils.getString("System_setting_content6");
        m_LeftButText.text = GameUtils.getString("System_setting_content11");
        m_MiddleButText.text = GameUtils.getString("System_setting_content12");
        m_RightButText.text = GameUtils.getString("System_setting_content13");
        if (ObjectSelf.GetInstance().GetSettingData().m_ImageQuality == 6)
        {
            LeftSelect();
            MiddleNoSelect();
        }
        if (ObjectSelf.GetInstance().GetSettingData().m_ImageQuality == 10000)
        {
            MiddleSelect();
        }
        if (ObjectSelf.GetInstance().GetSettingData().m_ImageQuality == 7)
        {
            RightSelect();
            MiddleNoSelect();
        }
    }

    private void LeftSelect()
    {
        m_LeftOutline.enabled = true;
        m_MiddkeOutline.enabled = false;
        m_RightOutline.enabled = false;
        m_LeftButImage.SetActive(true);
        m_MiddleButImage.SetActive(false);
        m_RightButImage.SetActive(false);
        m_LeftButText.color = new Color(1f, 1f, 1f);
        m_RightButText.color = new Color(0.44f, 0.42f, 0.42f);
    }

    private void MiddleSelect()
    {
        m_LeftOutline.enabled = false;
        m_MiddkeOutline.enabled = true;
        m_RightOutline.enabled = false;
        m_LeftButImage.SetActive(false);
        m_MiddleButImage.SetActive(true);
        m_RightButImage.SetActive(false);
        m_MiddleButText.color = new Color(1f, 1f, 1f);
        m_LeftButText.color = new Color(0.44f, 0.42f, 0.42f);
        m_RightButText.color = new Color(0.44f, 0.42f, 0.42f);
    }
    private void MiddleNoSelect()
    {
        m_MiddkeOutline.enabled = false;
        m_MiddleButImage.SetActive(false);
        m_MiddleButText.color = new Color(0.44f, 0.42f, 0.42f);
    }

    private void RightSelect()
    {
        m_LeftOutline.enabled = false;
        m_MiddkeOutline.enabled = false;
        m_RightOutline.enabled = true;
        m_LeftButImage.SetActive(false);
        m_MiddleButImage.SetActive(false);
        m_RightButImage.SetActive(true);
        m_LeftButText.color = new Color(0.44f, 0.42f, 0.42f);
        m_RightButText.color = new Color(1f, 1f, 1f);
    }
 
    private void OnClickLeftBtn()
    {
        switch (SettingType)
        {
            case "BattleMode":
                BattleModeLeftBtn();
                break;
            case "Music":
                MusicLeftBtn();
                break;
            case "Sound":
                SoundLeftBtn();
                break;
            case "ImageQuality":
                ImageQualityLeftBtn();
                break;
        }
    }
    private void BattleModeLeftBtn()
    {
        LeftSelect();
        ObjectSelf.GetInstance().GetSettingData().SetLocalSettingData(SettingType, 0);
    }
    
    private void MusicLeftBtn()
    {
        LeftSelect();
        ObjectSelf.GetInstance().GetSettingData().SetLocalSettingData(SettingType, 0);
    }

    private void SoundLeftBtn()
    {
        LeftSelect();
        ObjectSelf.GetInstance().GetSettingData().SetLocalSettingData(SettingType, 0);
    }
 
     private void ImageQualityLeftBtn()
     {
         LeftSelect();
         MiddleNoSelect();
        ObjectSelf.GetInstance().GetSettingData().SetLocalSettingData(SettingType, 6);
     }
            

    private void OnClickMiddleBtn()
    {
        if (SettingType == "ImageQuality")
        {
            MiddleSelect();
            ObjectSelf.GetInstance().GetSettingData().SetLocalSettingData(SettingType, 1);
        }

    }

    private void OnClickRightBtn()
    {
        switch (SettingType)
        {
            case "BattleMode":
                BattleModeRightBtn();
                break;
            case "Music":
                MusicRightBtn();
                break;
            case "Sound":
                SoundRightBtn();
                break;
            case "ImageQuality":
                ImageQualityRightBtn();
                break;
        }
        }
    private void BattleModeRightBtn()
    {
        RightSelect();
        ObjectSelf.GetInstance().GetSettingData().SetLocalSettingData(SettingType, 1);
    }

    private void MusicRightBtn()
    {
        RightSelect();
        ObjectSelf.GetInstance().GetSettingData().SetLocalSettingData(SettingType, 1);  
    }
    
    private void SoundRightBtn()
    {
        RightSelect();
        ObjectSelf.GetInstance().GetSettingData().SetLocalSettingData(SettingType, 1);
    }

    private void ImageQualityRightBtn()
    {
        RightSelect();
        MiddleNoSelect();
        ObjectSelf.GetInstance().GetSettingData().SetLocalSettingData(SettingType, 7);
    }
}
