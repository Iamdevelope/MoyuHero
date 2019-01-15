using UnityEngine;
using System.Collections;
using DreamFaction.UI.Core;
using DreamFaction.GameNetWork;
using DreamFaction.UI;
using UnityEngine.UI;
using System.Collections.Generic;
using GNET;
using DreamFaction.Utils;
using DreamFaction.GameCore;

public class RuneItem : CellItem
{
    public int tableID;
    public X_GUID guid;
    public int money;     // 金币
    public int smelt;    // 熔炼点基础量

    private bool _isSelect = false;
    private ItemTemplate _rune;
    private ItemEquip _data;

    //private Image _icon;       // 图标
    private RuneIconItem iconItem;
    private Text _name;          // 名称
    private Text _level;        // 等级
    private GameObject _equip;  // 是否已经装备
    private Button _selfBtn;    // 自身的按钮
    private Button _selectBtn;   // 选择按钮
    private Image _selectImage;   // 选择按钮的图片
    private Text _smeltText;         // 熔炼点基础量
    private GameObject _starLevel;    // 星级
    private GameObject _border;        // 边界标志被选中状态

    public override void InitUIData()
    {
        _selfBtn = selfTransform.GetComponent<Button>();
        _selfBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickSelf));
        //_icon = selfTransform.FindChild("Parent/Icon").GetComponent<Image>();
        iconItem = new RuneIconItem(selfTransform.FindChild("Parent/RuneIconItem"));
        _level = selfTransform.FindChild("Parent/Level/Text").GetComponent<Text>();
        _equip = selfTransform.FindChild("Parent/Equip").gameObject;
        _name = selfTransform.FindChild("Parent/Name").GetComponent<Text>();
        _selectBtn = selfTransform.FindChild("Parent/Select_Btn").GetComponent<Button>();
        _selectBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnClickSelect));
        _selectImage = selfTransform.FindChild("Parent/Select_Btn").GetComponent<Image>();
        _smeltText = selfTransform.FindChild("Parent/Image/Smelt").GetComponent<Text>();
        _starLevel = selfTransform.FindChild("Parent/StarLevel").gameObject;
        _border = selfTransform.FindChild("Parent/Border").gameObject;
    }

    public override void InitUIView()
    {
        base.InitUIView();
    }

    public void ShowInfo()
    {
        // 获得数据
        _rune = (ItemTemplate)DataTemplate.GetInstance().m_ItemTable.getTableData(tableID);
        _data = (ItemEquip)ObjectSelf.GetInstance().CommonItemContainer.FindItem(EM_BAG_HASHTABLE_TYPE.EM_BAG_HASHTABLE_TYPE_EQUIP, guid);

        //_icon.sprite = UIResourceMgr.LoadSprite(common.defaultPath + _rune.getIcon());
        //_icon.SetNativeSize();

        iconItem.SetIcon(common.defaultPath + _rune.getIcon());
        iconItem.SetRuneType(_rune.getRune_type());
        //iconItem.SetIsSpecial(_rune.getRune_type() == 5 || _rune.getRune_type() == 6);
        iconItem.SetIsSpecial(RuneModule.IsSpecialRune(_rune));

        // 是否已装备
        if (_data.IsEquip())
        {
            _equip.SetActive(true);
        }
        else
        {
            _equip.SetActive(false);
        }

        // 名称
        _name.text = GameUtils.getString(_rune.getName());

        // 符文等级
        _level.text = "+" + _data.GetStrenghLevel().ToString();

        // 符文基础值
        smelt = _rune.getRune_smelt();
        //_smeltText.text = smelt.ToString();

        // 符文星级
        int level = _rune.getRune_quality();
        for (int i = 5; i < 10; i++)
        {
            _starLevel.transform.GetChild(i).gameObject.SetActive(i < level + 5);
        }

        // 是否被选中
        _border.SetActive(false);

        // 金币 返回的物品
        int strengid = _rune.getRune_strengthenId();
        int strenglevel = _data.GetStrenghLevel();
        int unqelID = 0;
        money = 0;
        smelt = 0;
        if (strenglevel == 0)
        {
            money = 0;
            smelt = _rune.getRune_smelt();
            _smeltText.text = smelt.ToString();
        }

        if (strenglevel != 0)
        {
            smelt = _rune.getRune_smelt();
            unqelID = strengid * 100;
            unqelID += strenglevel;

            //  
            RunecostTemplate runecost = (RunecostTemplate)DataTemplate.GetInstance().m_RunecostTable.getTableData(unqelID);
            if (runecost.getReturnType1() == 1400000002)
            {
                money += runecost.getReturnValue1();
            }
            else if (runecost.getReturnType1() == 1400000004)
            {
                smelt += runecost.getReturnValue1();
            }

            if (runecost.getReturnType2() == 1400000002)
            {
                money += runecost.getReturnValue2();
            }
            else if (runecost.getReturnType2() == 1400000004)
            {
                smelt += runecost.getReturnValue2();
            }
            _smeltText.text = smelt.ToString();
        }
    }

    // 更新选择按钮，是否为取消或者无效
    public void updateSelectBtn(bool isGrey)
    {
        if (isGrey)
        {
            //if (!_isSelect)
            {
                GameUtils.SetBtnSpriteGrayState(_selectBtn, true);
            }
        }
        else
        {
            //if (!_isSelect)
            {
                GameUtils.SetBtnSpriteGrayState(_selectBtn, false);
            }
        }
    }

    public void SetSelectBtnState(bool isSelect)
    {
        _isSelect = isSelect;
        if (_isSelect)
        {
            _selectBtn.transform.FindChild("Text").GetComponent<Text>().text = GameUtils.getString("heromelt_button6");
            _selectBtn.GetComponent<Image>().sprite = UIResourceMgr.LoadSprite(common.defaultPath + "Ui_quxiao");
            _border.SetActive(true);
        }
        else
        {
            _selectBtn.transform.FindChild("Text").GetComponent<Text>().text = GameUtils.getString("heromelt_button5");
            _selectBtn.GetComponent<Image>().sprite = UIResourceMgr.LoadSprite(common.defaultPath + "Ui_xuanze");

            _border.SetActive(false);            
        }
    }

    /////////////// 按钮回调  /////////////////////
    void OnClickSelect()
    {
        if (_isSelect)
        {
            UI_RuneExp.inst.RemoveSelectRune(index, tableID, smelt, money, guid);
        }
        else
        {
            if (UI_RuneExp.inst.AddSelectRune(index, tableID, smelt, money, guid))
            {
            }

        }
    }

    // 点击自己的回调
    void OnClickSelf()
    {

        UI_HomeControler.Inst.AddUI(UI_RuneInfo.UI_ResPath);
        UI_RuneInfo.SetShowRuneGUID(guid);
    }
}
