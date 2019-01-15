using UnityEngine;
using UnityEngine.UI;
using DreamFaction.UI.Core;
using System;
using System.Text;
using DreamFaction.GameNetWork;
using DreamFaction.GameEventSystem;
using DreamFaction.UI;
using System.Collections.Generic;
using GNET;
using DreamFaction.Utils;
using DreamFaction.GameCore;
using System.Text.RegularExpressions;

public class UI_Intitle : UI_IntitleBase
{
    public static UI_Intitle inst;
    public static string UI_ResPath = "Guide/UI_Intitle_1_2";

    private InputField m_InputName;
    private Text m_InputField_Text;

    private string m_NickName = string.Empty;
    private int CharNum = 0;

    public override void InitUIData()
    {
        base.InitUIData();
        m_InputName = selfTransform.FindChild("Center/InputField").GetComponent<InputField>();
        m_InputName.onValueChange.AddListener(OnChangeAccountEdit);
        m_InputField_Text = selfTransform.FindChild("Center/InputField/Text").GetComponent<Text>();

        GameEventDispatcher.Inst.addEventListener(GameEventID.UI_ChangeName, OnNameCHange);
    }

    public override void InitUIView()
    {
        base.InitUIView();
        m_TipsText.text = GameUtils.getString("nickname_word9");
        m_Placeholder.text = GameUtils.getString("nickname_word7"); 
    }

    public void OnChangeAccountEdit(string username)
    {
        //Regex _cn = new Regex("[\u4e00-\u9fa5]+");//正则表达式 表示汉字范围  
        Regex _cn = new Regex("[^\x00-\xff]");// 双字节字符
        CharNum = 0;

        char[] _char_2 = username.ToCharArray();
        foreach (char c in _char_2)
        {
            if (_cn.IsMatch(c.ToString()))
            {
                CharNum += 2;
            }
            else
            {
                CharNum += 1;
            }
        }

        if (CharNum <= 14)
        {
            m_NickName = username;
            m_InputName.text = username;
        }
        else
        {
            m_InputName.text = m_NickName;
        }

        m_InputField_Text.enabled = false;
        m_InputField_Text.enabled = true;
    }

    protected override void OnClickRandBtn()
    {
        string name = GetRandName();
        if (name != string.Empty)
        {
            m_InputName.text = name;
            m_NickName = name;
        }
        else
        {
            GetRandName();
        }
    }

    // 得到一个随机的名字
    private string GetRandName()
    {
        List<string> _name_1 = new List<string>();
        List<string> _name_2 = new List<string>();

        Dictionary<int, IExcelBean> _data = DataTemplate.GetInstance().m_RandnameTemplate.getData();
        foreach (var item in _data)
        {
            RandnameTemplate _randnameTemplate = (RandnameTemplate)DataTemplate.GetInstance().m_RandnameTemplate.getTableData(item.Key);
            if (_randnameTemplate.getType() == 1)
            {
                _name_1.Add(_randnameTemplate.getName());
            }
            else
            {
                _name_2.Add(_randnameTemplate.getName());
            }
        }

        string _name = _name_1[UnityEngine.Random.Range(0, _name_1.Count)] + _name_2[UnityEngine.Random.Range(0, _name_2.Count)];
        return _name;
    }

    protected override void OnClickCloseBtn()
    {

        if (m_NickName == string.Empty)
        {
            InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("nickname_word4"), this.transform);
            m_InputName.text = string.Empty;
            return;
        }
        else
        {
            if (GameUtils.IsShieldCharacter(m_NickName, "name"))
            {
                InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("nickname_word1"), this.transform);
                m_InputName.text = string.Empty;
                return;
            }
        }
        CChangeName _CChangeName = new CChangeName();
        _CChangeName.newname = m_NickName;
        IOControler.GetInstance().SendProtocol(_CChangeName);
    }
    
    /// <summary>
    /// OK = 1; // 成功
    /// ERROR = 2; // 失败
    /// INVALID = 3; // 名称不合法
    /// DUPLICATED = 4; // 重名
    /// NO_ITEM = 5; // 没有道具
    /// OVERLEN = 6; // 角色名过长
    /// SHORTLEN = 7; // 角色名过短
    /// ERRORCHAR = 8; // 特殊符号
    /// HAVESPACE = 9; // 有空格
    /// </summary>
    private void OnNameCHange(GameEvent ge)
    {
        byte error = (byte)ge.data;
        switch(error)
        {
            case 1:
                InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("nickname_word5"), m_InputName.gameObject.transform);
                ObjectSelf.GetInstance().Name = m_NickName;
                GameEventDispatcher.Inst.dispatchEvent(GameEventID.Net_MCHumanDetailAttribute_Name);

                // 新手引导
                UI_MainHome.GetInst().ShowGuideRecruit();

                UI_HomeControler.Inst.ReMoveUI(gameObject);
                
                break;
            case 2:
                InterfaceControler.GetInst().AddMsgBox("修改昵称失败", this.transform);
                m_InputName.text = string.Empty;
                break;
            case 3:
                InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("nickname_word1"), this.transform);
                m_InputName.text = string.Empty;
                break;
            case 8:
                InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("nickname_word2"), this.transform);
                m_InputName.text = string.Empty;
                break;
            case 9:
                InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("nickname_word6"), this.transform);
                m_InputName.text = string.Empty;
                break;
            case 4:
                InterfaceControler.GetInst().AddMsgBox(GameUtils.getString("nickname_word3"), this.transform);
                m_InputName.text = string.Empty;
                break;
            case 6:
                InterfaceControler.GetInst().AddMsgBox("角色名过长，请重新输入", this.transform);
                m_InputName.text = string.Empty;
                break;
            case 7:
                InterfaceControler.GetInst().AddMsgBox("角色名过短，请重新输入", this.transform);
                m_InputName.text = string.Empty;
                break;
            default:
                break;
        }
    }

    void OnDestroy()
    {
        GameEventDispatcher.Inst.removeEventListener(GameEventID.UI_ChangeName, OnNameCHange);
    }
}
