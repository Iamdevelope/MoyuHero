using DreamFaction.UI.Core;
using UnityEngine.UI;
using System.Collections.Generic;
using DreamFaction.GameNetWork;
using DreamFaction.LogSystem;
using GNET;
using DreamFaction.Utils;
using DreamFaction.UI;
using UnityEngine.Events;
public class UI_RuneTypeSelect : BaseUI
{
    public static UI_RuneTypeSelect _instance;
    private Text runeType;
    private Button BlueBtn;
    private Button PurpleBtn;
    private Button GreenBtn;
    private Button RedBtn;
    private Button SpectialBtn;
    private Button AllBtn;
    public UI_SlideBtn MainBtn;
    public int itemTypeNum;
    private Button mainButton;
    public override void InitUIData()
    {
        base.InitUIData();
        _instance = this;
        itemTypeNum = 1;
        runeType = selfTransform.FindChild("SortObj/MainBagBtn/Text").GetComponent<Text>();
        BlueBtn = selfTransform.FindChild("SortObj/BlueBtn").GetComponent<Button>();
        BlueBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnBule));
        PurpleBtn = selfTransform.FindChild("SortObj/PurpleBtn").GetComponent<Button>();
        PurpleBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnPurple));
        GreenBtn = selfTransform.FindChild("SortObj/GreenBtn").GetComponent<Button>();
        GreenBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnGreen));
        RedBtn = selfTransform.FindChild("SortObj/RedBtn").GetComponent<Button>();
        RedBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnRed));
        SpectialBtn = selfTransform.FindChild("SortObj/SpectialBtn").GetComponent<Button>();
        SpectialBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnSpectial));
        AllBtn = selfTransform.FindChild("SortObj/AllBtn").GetComponent<Button>();
        AllBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(OnAll));
        MainBtn = selfTransform.FindChild("SortObj/MainBagBtn").GetComponent<UI_SlideBtn>();
        
    }


    public override void InitUIView()
    {
        base.InitUIView();
        UpdateRuneType(itemTypeNum);
    }

    public void UpdateRuneType(int itemTypeID)
    {
        switch (itemTypeID)
        {
            case 1:
                runeType.text = GameUtils.getString("hero_rune_content14");
                UI_RuneMange._instance.SelectRune(EM_SORT_RUNE_ITEM.EM_SORT_RUNE_ITEM_ALL);
                break;
            case 2:
                runeType.text = GameUtils.getString("hero_rune_content15");
                UI_RuneMange._instance.SelectRune(EM_SORT_RUNE_ITEM.EM_SORT_RUNE_ITEM_SPECIAL);
                break;
            case 3:
                runeType.text = GameUtils.getString("hero_rune_content16");
                UI_RuneMange._instance.SelectRune(EM_SORT_RUNE_ITEM.EM_SORT_RUNE_ITEM_RED);
                break;
            case 4:
                runeType.text = GameUtils.getString("hero_rune_content17");
                UI_RuneMange._instance.SelectRune(EM_SORT_RUNE_ITEM.EM_SORT_RUNE_ITEM_GREEN);
                break;
            case 5:
                runeType.text = GameUtils.getString("hero_rune_content18");
                UI_RuneMange._instance.SelectRune(EM_SORT_RUNE_ITEM.EM_SORT_RUNE_ITEM_PURPLE);
                break;
            case 6:
                runeType.text = GameUtils.getString("hero_rune_content19");
                UI_RuneMange._instance.SelectRune(EM_SORT_RUNE_ITEM.EM_SORT_RUNE_ITEM_BLUE);
                break;

            default:
                break;
        }

    }  


   public void OnBule()
    {
        itemTypeNum = 6;
        UpdateRuneType(itemTypeNum);
        MainBtn.OnClose();
    }
    public void OnPurple()
   {
       itemTypeNum = 5;
       UpdateRuneType(itemTypeNum);
       MainBtn.OnClose();
   }
    public void OnGreen()
    {
        itemTypeNum = 4;
        UpdateRuneType(itemTypeNum);
        MainBtn.OnClose();
    }

    public void OnRed()
    {
        itemTypeNum = 3;
        UpdateRuneType(itemTypeNum);
        MainBtn.OnClose();
    }
    public void OnSpectial()
    {
        itemTypeNum = 2;
        UpdateRuneType(itemTypeNum);
        MainBtn.OnClose();
    }
    public void OnAll()
    {
        itemTypeNum = 1;
        UpdateRuneType(itemTypeNum);
        MainBtn.OnClose();
    }
}
