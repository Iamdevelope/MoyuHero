using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System;
using DreamFaction.GameEventSystem;
using DreamFaction.LogSystem;
using DreamFaction.UI;
using Platform;
using DreamFaction.GameCore;
using DreamFaction.GameAudio;
using GNET;

public class SettingData : BaseControler
{
    public int m_BattleMode;//战斗模式 0自动 1手动
    public int m_Music;//音乐 0关 1开
    public int m_Sound;//音效 0关 1开
    public int m_ImageQuality;// 图片质量 0底 1中 2高
    public int m_VitalityRemind;//活力回满 0关 1开
    public int m_FreeRecruit;//免费招募 0关 1开
    public int m_WorldBoss;//世界Boss 0关 1开
    public int m_ExplorationTask;//探险任务完成 0关 1开
    public int m_FreeVitality;//免费活力领取 0关 1开

    public List<int> m_innerdropidlist = new List<int>(); // 礼包兑换码掉落包ID



    protected  override void InitData()
    {
        base.InitData();

    }
    public void GetLocalSettingData()
    {
        m_BattleMode = int.Parse(ConfigsManager.Inst.GetClientConfig(ClientConfigs.BattleMode));
        m_Music = int.Parse(ConfigsManager.Inst.GetClientConfig(ClientConfigs.Music));
        m_Sound = int.Parse(ConfigsManager.Inst.GetClientConfig(ClientConfigs.Sound));
        m_ImageQuality = int.Parse(ConfigsManager.Inst.GetClientConfig(ClientConfigs.Quality));
        m_VitalityRemind = int.Parse(ConfigsManager.Inst.GetClientConfig(ClientConfigs.VitalityRemind));
        m_FreeRecruit = int.Parse(ConfigsManager.Inst.GetClientConfig(ClientConfigs.FreeRecruit));
        m_WorldBoss = int.Parse(ConfigsManager.Inst.GetClientConfig(ClientConfigs.WorldBoss));
        m_ExplorationTask = int.Parse(ConfigsManager.Inst.GetClientConfig(ClientConfigs.ExplorationTask));
        m_FreeVitality = int.Parse(ConfigsManager.Inst.GetClientConfig(ClientConfigs.FreeVitality));


    }

    public void SetLocalSettingData(String type,int value)
    {
        if (type == "BattleMode")
        {
            if (m_BattleMode == value)
                return;
            ConfigsManager.Inst.SetClientConfig(ClientConfigs.BattleMode, value.ToString());
            m_BattleMode = value;
        }
        if (type == "Music")
        {
            if (m_Music == value)
                return;
            ConfigsManager.Inst.SetClientConfig(ClientConfigs.Music, value.ToString());
            m_Music = value;
            if (value == 1)
            {
                AudioControler.Inst.StopMusic();
            }
            if (value == 0)
            {
                AudioControler.Inst.StartMusic();
            }
        }
        if (type == "Sound")
        {
            if (m_Sound == value)
                return;
            ConfigsManager.Inst.SetClientConfig(ClientConfigs.Sound, value.ToString());
            m_Sound = value;
            if (value == 1)
            {
                AudioControler.Inst.StopSound();
            }
            if (value == 0)
            {
                AudioControler.Inst.StartSound();
            }
        }
        if (type == "ImageQuality")
        {
            if (m_ImageQuality == value)
                return;
            ConfigsManager.Inst.SetClientConfig(ClientConfigs.Quality, value.ToString());
            m_ImageQuality = value;
            if (value == 7)
            {
                QualityManager.Inst.SetGameQuality(GameQuality.Hight);
            }
            if (value == 6)
            {
                QualityManager.Inst.SetGameQuality(GameQuality.Low);
            }
            
        }
        if (type == "VitalityRemind")
        {
            if (m_VitalityRemind == value)
                return;
            m_VitalityRemind = value;
            ConfigsManager.Inst.SetClientConfig(ClientConfigs.VitalityRemind, value.ToString());
        }
        if (type == "FreeRecruit")
        {
            if (m_FreeRecruit == value)
                return;
            ConfigsManager.Inst.SetClientConfig(ClientConfigs.FreeRecruit, value.ToString());
            m_FreeRecruit = value;
        }
        if (type == "WorldBoss")
        {
            if (m_WorldBoss == value)
                return;
            ConfigsManager.Inst.SetClientConfig(ClientConfigs.WorldBoss, value.ToString());
            m_WorldBoss = value;
        }
        if (type == "ExplorationTask")
        {
            if (m_ExplorationTask == value)
                return;
            ConfigsManager.Inst.SetClientConfig(ClientConfigs.ExplorationTask, value.ToString());
            m_ExplorationTask = value;
        }
        if (type == "FreeVitality")
        {
            if (m_FreeVitality == value)
                return;
            ConfigsManager.Inst.SetClientConfig(ClientConfigs.FreeVitality, value.ToString());
            m_FreeVitality = value;
        }    
    }

    //0 成功
    //1 用户名错误
    //2 用户名已存在
    public void AccountIsExists(int key)
    { 
        if (AccountBinding.instance != null)
        {
            AccountBinding.instance.IsAccountExist(key);
        }
    }
    //注册返回的信息
    //0 成功
    //1 用户名或密码错误
    //2 用户名已存在
    //3 创建用户错误
    public void RegisterIsSuccess(int key)
    {
        if (AccountBinding.instance != null)
        {
            AccountBinding.instance.RegisterMsg(key);
        }
    }

    /// <summary>
    /// 获取礼包兑换码中的数据
    /// </summary>
    /// <param name="_data"></param>
    public void DuihuanlbCopy(LinkedList<int> _data)
    {
        m_innerdropidlist.Clear();
        foreach (int value in _data)
        {
            m_innerdropidlist.Add((int)value);
        }

        if (m_innerdropidlist.Count != 0)
        {
            SpreeCode.Inst.OnOpenLbItemWindow();
        }
    }
    
}
