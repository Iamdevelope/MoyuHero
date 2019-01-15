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

public class UI_PlayerInfoMes : BaseUI 
{
    public static UI_PlayerInfoMes _inst;
    public static string UI_ResPath = "UI_Home/UI_PlayerInfoMes_1_2";

    private Text m_UidStr;               //玩家ID文本
    private Text m_UidTxt;               //玩家ID
    private Text _ServerTxt;             //服务器名称
    private Text _HeroCounumTxt;         //英雄的个数
    private Text _ItemCounumTxt;         //道具的个数
    private Text _SeverTimeTxt;          //服务器时间
    private Text _MaxHeroNumTxt;         //最大英雄个数     
    private Text _MaxItemNumTxt;         //最大道具个数
    private Text _PlayerLevelTxt;        //玩家等级
    private Text _ExpTxt;                //最大经验
    private Slider _ExpBar;              //经验条
    private Button _BtnOK;               //确定按钮

    private long severTime;
    public override void InitUIData()
    {
        base.InitUIData();
        _inst = this;
        severTime = ObjectSelf.GetInstance().ServerTime + (int)Time.time;
        m_UidStr = selfTransform.FindChild("MesBox/UidTxt").GetComponent<Text>();
        m_UidTxt = selfTransform.FindChild("MesBox/UidValueTxt").GetComponent<Text>();
        _ServerTxt = selfTransform.FindChild("MesBox/ServerTxt").GetComponent<Text>();
        _HeroCounumTxt = selfTransform.FindChild("MesBox/HeroConnumTXt").GetComponent<Text>();
        _ItemCounumTxt = selfTransform.FindChild("MesBox/ItemConnumTxt").GetComponent<Text>();
        _SeverTimeTxt = selfTransform.FindChild("MesBox/ServerTimeValueTxt").GetComponent<Text>();
        _MaxHeroNumTxt = selfTransform.FindChild("MesBox/MaxHeroNumTxt").GetComponent<Text>();
        _MaxItemNumTxt = selfTransform.FindChild("MesBox/MaxItemNumTxt").GetComponent<Text>();
        _PlayerLevelTxt = selfTransform.FindChild("MesBox/LevelTxt").GetComponent<Text>();
        _ExpTxt = selfTransform.FindChild("MesBox/ExpTxt").GetComponent<Text>();
        _ExpBar = selfTransform.FindChild("MesBox/Exbar").GetComponent<Slider>();
        _BtnOK = selfTransform.FindChild("MesBox/Btn_Ok").GetComponent<Button>();

        _BtnOK.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickBtnOK));

        //GameEventDispatcher.Inst.addEventListener(GameEventID.KE_BagItemSizeShow);
    }

    public override void InitUIView()
    {
        base.InitUIView();

        string ServerName = UI_LoginWin.curServerName;
        if (ServerName != string.Empty)
        {
            string[] _str = ServerName.Split('#');
            if (_str.Length >= 2)
            {
                _ServerTxt.text = "  " + _str[0] + "   " + _str[1];
            }
        }
        m_UidTxt.text = ObjectSelf.GetInstance().Guid.GUID_value.ToString("X");
        m_UidStr.text = GameUtils.getString("UID");
        InvokeRepeating("GetServerTime", 0, 1);
        ShowHeroCounum();
        ShowItemConnum();
        ShoweExbar();        
    }

    

    //显示经验值  经验条
    private void ShoweExbar()
    {
        PlayerTemplate pRow = (PlayerTemplate)DataTemplate.GetInstance().m_PlayerExpTable.getTableData(ObjectSelf.GetInstance().Level);
        _ExpBar.value = (float)ObjectSelf.GetInstance().Exp / (float)pRow.getExp();
        _PlayerLevelTxt.text = ObjectSelf.GetInstance().Level.ToString();
        
        StringBuilder str = new StringBuilder();
        str.Append("<color=yellow>");
        str.Append(ObjectSelf.GetInstance().Exp.ToString());
        str.Append("</color>");
        str.Append("/");
        str.Append(pRow.getExp());
        _ExpTxt.text = str.ToString();
    }


    //显示道具个数
    void ShowItemConnum()
    {
        int ItemCounum = ObjectSelf.GetInstance().CommonItemContainer.GetBagItemSum();
        _ItemCounumTxt.text = ItemCounum.ToString();
        StringBuilder str = new StringBuilder();
        str.Append("/");
        str.Append(ObjectSelf.GetInstance().CommonItemContainer.GetBagItemSizeMax());
        _MaxItemNumTxt.text = str.ToString();
        if (ItemCounum >= ObjectSelf.GetInstance().CommonItemContainer.GetBagItemSizeMax())
        {
            _ItemCounumTxt.color = Color.red;
        }
        else
        {
            _ItemCounumTxt.color = Color.green;
        }
    }

    //显示英雄个数
    void ShowHeroCounum()
    {
        int HeroCount = ObjectSelf.GetInstance().HeroContainerBag.GetHeroList().Count;
        _HeroCounumTxt.text = HeroCount.ToString();
        StringBuilder str = new StringBuilder();
        str.Append("/");
        str.Append(ObjectSelf.GetInstance().HeroContainerBag.GetHeroBagSizeMax());
        _MaxHeroNumTxt.text = str.ToString();
        if (HeroCount >= ObjectSelf.GetInstance().HeroContainerBag.GetHeroBagSizeMax())
        {
            _HeroCounumTxt.color = Color.red;
        }
        else
        {
            _HeroCounumTxt.color = Color.green;
        }
    
    }


    //获取当前的服务器时间
    void GetServerTime()
    {
        //Debug.Log(severTime);
        _SeverTimeTxt.text = ObjectSelf.GetInstance().ServerDateTime.ToString("HH:mm");
    }


    private void OnClickBtnOK()
    {
        UI_HomeControler.Inst.ReMoveUI(gameObject);
    }
}
