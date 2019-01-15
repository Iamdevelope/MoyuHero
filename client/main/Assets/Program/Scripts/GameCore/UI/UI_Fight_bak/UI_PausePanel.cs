using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;
using DreamFaction.UI.Core;
using DreamFaction.GameAudio;
using DreamFaction.GameCore;
using DreamFaction.GameNetWork;
using DreamFaction.Utils;
using GNET;

public enum MenuStatus
{ 
    Menu_Out,
    Menu_In,
    Menu_Hide,
    Menu_Show,
}

public class UI_PausePanel : BaseUI {

    public Color ActiveTextColor;
    public Color DisableTextColor;

    private Text m_TittleText;
    private Button m_ResetBtn;
    private Text m_ResetBtnText;
    private Text m_SubTittleMusicText;
    private Text m_SubTittleSoundText;
    private Text m_SubTittleQualityText;

    private Text m_MusicText;
    private Button m_MusicLeftBtn;
    private Button m_MusicRightBtn;
    private Text m_SoundText;
    private Button m_SoundLeftBtn;
    private Button m_SoundRightBtn;
    private Text m_QualityText;
    private Button m_QualityLeftBtn;
    private Button m_QualityRightBtn;
    private Text m_SettingTittleText;

    private Text m_SuperiorityText;
    private Text m_ExitBtnText;
    private Text m_VictoryText;
    private Text m_VictoryConditionText;

    private StageTemplate m_StageTemp;


    public Slider musicSlider = null;
    public Slider soundSlider = null;

    public GameObject musicFlag = null;
    public GameObject soundFlag = null;

    private Animator mAnim = null;
    public TimeScaleState mInitSpeed = TimeScaleState.TimeScale_Normal;

    private GameObject mQualityLow; // 低画质
    private GameObject mQualityHight; // 高画质
	public override void InitUIData()
    {
        LoadComponent();
        mInitSpeed = GameTimeControler.Inst.curTimeScaleState;
        GameTimeControler.Inst.SetState(TimeScaleState.TimeScale_Pause);
        mQualityLow = selfTransform.FindChild("left/Quality/low").gameObject;
        mQualityHight = selfTransform.FindChild("left/Quality/hight").gameObject;
        setQualityView();

        m_StageTemp = GetCurrentStage();
    }
    public override void InitUIView()
    {
        base.InitUIView();

        InitComponent();
    }


    private void LoadComponent()
    {
        m_TittleText = selfTransform.FindChild("title/TittleText").GetComponent<Text>();
        m_ResetBtn = selfTransform.FindChild("left/ResetBtn").GetComponent<Button>();
        m_ResetBtnText = selfTransform.FindChild("left/ResetBtn/ResetBtnText").GetComponent<Text>();
        m_SubTittleMusicText = selfTransform.FindChild("left/SubTittleMusic/SubTittleMusicText").GetComponent<Text>();
        m_SubTittleSoundText = selfTransform.FindChild("left/SubTittleSound/SubTittleSoundText").GetComponent<Text>();
        m_SubTittleQualityText = selfTransform.FindChild("left/SubTittleQuality/SubTittleQualityText").GetComponent<Text>();

        m_MusicText = selfTransform.FindChild("left/MusicText").GetComponent<Text>();
        m_MusicLeftBtn = selfTransform.FindChild("left/MusicText/MusicLeftBtn").GetComponent<Button>();
        m_MusicRightBtn = selfTransform.FindChild("left/MusicText/MusicRightBtn").GetComponent<Button>();
        m_SoundText = selfTransform.FindChild("left/SoundText").GetComponent<Text>();
        m_SoundLeftBtn = selfTransform.FindChild("left/SoundText/SoundLeftBtn").GetComponent<Button>();
        m_SoundRightBtn = selfTransform.FindChild("left/SoundText/SoundRightBtn").GetComponent<Button>();
        m_QualityText = selfTransform.FindChild("left/QualityText").GetComponent<Text>();
        m_SettingTittleText = selfTransform.FindChild("left/QualityText").GetComponent<Text>();
        m_QualityLeftBtn = selfTransform.FindChild("left/QualityText/QualityLeftBtn").GetComponent<Button>();
        m_QualityRightBtn = selfTransform.FindChild("left/QualityText/QualityRightBtn").GetComponent<Button>();

        m_SuperiorityText = selfTransform.FindChild("right/SuperiorityText").GetComponent<Text>();
        m_ExitBtnText = selfTransform.FindChild("right/exit/Text").GetComponent<Text>();
        m_VictoryText = selfTransform.FindChild("right/VictoryText").GetComponent<Text>();
        m_VictoryConditionText = selfTransform.FindChild("right/VictoryConditionText").GetComponent<Text>();
    }

    private void InitComponent()
    {
        m_TittleText.text = GameUtils.getString("System_setting_content36");
        m_SubTittleMusicText.text = GameUtils.getString("System_setting_content4");
        m_SubTittleSoundText.text = GameUtils.getString("System_setting_content5");
        m_SubTittleQualityText.text = GameUtils.getString("System_setting_content6");
        m_ResetBtnText.text = GameUtils.getString("System_setting_content33");
        m_ExitBtnText.text = GameUtils.getString("System_setting_content31");

        m_VictoryText.text = GameUtils.getString("System_setting_content35");
        m_SuperiorityText.text = GameUtils.getString("System_setting_content34");

        switch (m_StageTemp.m_winCondition)
        {
            case 1:
                m_VictoryConditionText.text = GameUtils.getString("System_setting_content28");
                break;
            case 2:
                m_VictoryConditionText.text = string.Format(GameUtils.getString("System_setting_content29"), GetBossName(m_StageTemp));
                break;
        }

        m_ResetBtn.onClick.AddListener(OnClickResetBtn);
        m_MusicLeftBtn.onClick.AddListener(OnClickMusicBtn);
        m_MusicRightBtn.onClick.AddListener(OnClickMusicBtn);
        m_SoundLeftBtn.onClick.AddListener(OnClickSoundBtn);
        m_SoundRightBtn.onClick.AddListener(OnClickSoundBtn);
        m_QualityLeftBtn.onClick.AddListener(OnClickQualityBtn);
        m_QualityRightBtn.onClick.AddListener(OnClickQualityBtn);

        RefreshMusicText();
        RefreshSoundText();
        RefreshQualityText();
    }
    private StageTemplate GetCurrentStage()
    {
        int _stageID;
        if (ObjectSelf.GetInstance().GetIsPrompt())
        {
            _stageID = ObjectSelf.GetInstance().GetPromptCurCampaignID();
        }
        else
        {
            _stageID = ObjectSelf.GetInstance().GetCurCampaignID();
        }
        return (StageTemplate)DataTemplate.GetInstance().m_StageTable.getTableData(_stageID);
    }

    private string GetBossName(StageTemplate stageData)
    {
        MonstergroupTemplate _monsterGroup = null;
        MonsterTemplate _bossTemp = null;
        for (int i = stageData.m_monstergroup.Length - 1; i >= 0;i--)
        {
            _monsterGroup = null;
            _bossTemp = null;
            if (stageData.m_monstergroup[i] <= 0)
                continue;
            _monsterGroup = DataTemplate.GetInstance().m_MonsterGroupTable.getTableData(stageData.m_monstergroup[i]) as MonstergroupTemplate;
            if (_monsterGroup == null || _monsterGroup.getGrouptype() != 2 || GameUtils.GetObjectClassById(_monsterGroup.getMonsterid()[0]) != EM_OBJECT_CLASS.EM_OBJECT_CLASS_MONSTER)
                continue;

            _bossTemp = DataTemplate.GetInstance().m_MonsterTable.getTableData(_monsterGroup.getMonsterid()[0]) as MonsterTemplate;
            if (_bossTemp != null)
                break;
        }
        if (_bossTemp == null)
            return null;
        else
            return GameUtils.getString(_bossTemp.getMonstername());
    }
    private void RefreshMusicText()
    {
        if (AudioControler.Inst.musicVolume > 0.1f)
        {
            m_MusicText.text = GameUtils.getString("fight_bosbox_button2");
            m_MusicText.color = ActiveTextColor;
        }
        else
        {
            m_MusicText.text = GameUtils.getString("System_setting_content30");
            m_MusicText.color = DisableTextColor;
        }
    }
    private void RefreshSoundText()
    {
        if (AudioControler.Inst.soundVolume > 0.1f)
        {
            m_SoundText.text = GameUtils.getString("fight_bosbox_button2");
            m_SoundText.color = ActiveTextColor;
        }
        else
        {
            m_SoundText.text = GameUtils.getString("System_setting_content30");
            m_SoundText.color = DisableTextColor;
        }
    }
    private void RefreshQualityText()
    {
        if (QualityManager.Inst.GetGameQuality() == GameQuality.Low)
        {
            m_QualityText.text = GameUtils.getString("System_setting_content11");
        }
        else
        {
            m_QualityText.text = GameUtils.getString("System_setting_content13");
        }
    }
    private void OnClickMusicBtn()
    {
        if (AudioControler.Inst.musicVolume > 0.1f)
        {
            AudioControler.Inst.musicVolume = 0;
        }
        else
        {
            AudioControler.Inst.musicVolume = 1;
        }
        RefreshMusicText();
    }
    private void OnClickSoundBtn()
    {
        if (AudioControler.Inst.soundVolume > 0.1f)
        {
            AudioControler.Inst.soundVolume = 0;
        }
        else
        {
            AudioControler.Inst.soundVolume = 1;
        }
        RefreshSoundText();
    }

    private void OnClickQualityBtn()
    {
        if (QualityManager.Inst.GetGameQuality() == GameQuality.Low)
        {
            QualityManager.Inst.SetGameQuality(GameQuality.Hight);
        }
        else
        {
            QualityManager.Inst.SetGameQuality(GameQuality.Low);
        }
        RefreshQualityText();
    }
    private void OnClickResetBtn()
    {
        AudioControler.Inst.musicVolume = 1;
        AudioControler.Inst.soundVolume = 1;
        QualityManager.Inst.SetGameQuality(GameQuality.Hight);
        RefreshMusicText();
        RefreshSoundText();
        RefreshQualityText();
    }

    //public override void OnPlayingEnterAnimation()
    //{
    //    base.OnPlayingEnterAnimation();
    //    if (mAnim != null)
    //    {
    //        mAnim.SetBool("IsOpen", true);
    //    }

    //    SetMusicSetting();
    //    SetSoundSetting();
    //}


    public void OnBackGame()
    {
        GameTimeControler.Inst.SetState(mInitSpeed);
        //UIFightManager.Inst.curFightSpeed = UIFightManager.Inst.preTimeScale;
        if (mAnim != null)
        {
            mAnim.SetBool("IsOpen", false);
        }
        if (!IsInvoking("OnClose"))
        {
            CancelInvoke("OnClose");
        }
        Invoke("OnClose", 0.15f);

    }

    public void OnClose()
    {
        Destroy(gameObject);
    }

    public void OnQuitGame()
    {

        GameTimeControler.Inst.SetState(TimeScaleState.TimeScale_Normal);
        if (ObjectSelf.GetInstance().LimitFightMgr.m_bStartEnter)
        {
            CEndlessEnd msg = new CEndlessEnd();
            IOControler.GetInstance().SendProtocol(msg);
        }
        //bool isStageScene = !(ObjectSelf.GetInstance().IsLimitFight
        //    || ObjectSelf.GetInstance().IsInWorldBoss);
        //ObjectSelf.GetInstance().SetChangeLevel(isStageScene);
        SceneManager.Inst.StartChangeScene(SceneEntry.Home.ToString());

        Destroy(gameObject);
    }

    public void OnSwitchMusic(GameObject sender)
    {
        if (musicSlider == null) return;
        AudioControler.Inst.musicVolume = musicSlider.value;
        if (musicSlider.value == 0)
        {
            musicFlag.SetActive(true);
        }
        else
        {
            musicFlag.SetActive(false);
        }
    }

    public void OnSwitchSound(GameObject sender)
    {
        if (soundSlider == null) return;
        AudioControler.Inst.soundVolume = soundSlider.value;
        if (soundSlider.value == 0)
        {
            soundFlag.SetActive(true);
        }
        else
        {
            soundFlag.SetActive(false);
        }
    }

    public void OnSwitchEffect(GameObject sender)
    {
       
    }

    public void OnSwitchHp(GameObject sender)
    {
       
    }

    public void OnSwitchBettery(GameObject sender)
    {
        
    }

    private void SetMusicSetting()
    {
        musicSlider.value = AudioControler.Inst.musicVolume;
    }

    private void SetSoundSetting()
    {
        soundSlider.value = AudioControler.Inst.soundVolume;
    }

    private void SetEffectSetting()
    {
        
    }

    private void SetHpSetting()
    {
        
    }

    private void SetBetterySetting()
    {

    }

    public void switchQuality()
    {
        switch (QualityManager.Inst.GetGameQuality())
        {
            case GameQuality.Low:
                {
                    QualityManager.Inst.SetGameQuality(GameQuality.Hight);
                }   
                break;
            case GameQuality.Hight:
                {
                    QualityManager.Inst.SetGameQuality(GameQuality.Low);
                }
                break;
        }
        setQualityView();
    }

    private void setQualityView()
    {
        switch (QualityManager.Inst.GetGameQuality())
        {
            case GameQuality.Low:
                {
                    mQualityLow.gameObject.SetActive(true);
                    mQualityHight.gameObject.SetActive(false); 
                }   
                break;
            case GameQuality.Hight:
                {
                    mQualityLow.gameObject.SetActive(false);
                    mQualityHight.gameObject.SetActive(true); 
                }
                break;
        }
    }

	void Update () 
    {
	
	}

    void OnDisable()
    { 
        
    }


}
