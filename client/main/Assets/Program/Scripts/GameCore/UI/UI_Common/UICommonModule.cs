using UnityEngine;
using System.Collections;
using DreamFaction.LogSystem;
using DreamFaction.UI.Core;
using DreamFaction.GameNetWork;
using DreamFaction.Utils;
using DreamFaction.UI;

public class UICommonModule
{
#region 物品获得

    public static PropsaccessTemplate GetPropsacessTemplate(int id)
    {

        PropsaccessTemplate propsT = DataTemplate.GetInstance().m_PropsacessTable.getTableData(id) as PropsaccessTemplate;

        return propsT;
    }

    /// <summary>
    /// 根据物品id获得PropsacessTemplate;
    /// </summary>
    /// <param name="id">item 表id</param>
    /// <returns></returns>
    public static PropsaccessTemplate GetPropsacessTemplateByItemId(int itemId)
    {
        for (int i = 0; i < DataTemplate.GetInstance().m_PropsacessTable.getDataCount(); i++ )
        {
            int key = DataTemplate.GetInstance().m_PropsacessTable.GetDataKeys()[i];

            PropsaccessTemplate tem = GetPropsacessTemplate(key);
            if (tem == null) continue;

            if (tem.getPropsid() == itemId)
            {
                return tem;
            }
        }

        Debug.LogError("PropsaccessTemplate中不存在物品id=" + itemId + "的数据");
        return null;
    }

    public static void PropsacessHandler(int type, int data)
    {
        switch (type)
        {
            case 1:             //副本;
                UI_HomeControler.Inst.RemoveAllUIButThis(new string[] { UI_MainHome.UI_ResPath });

                UI_SelectLevelMgrNew.InitLevelId = data;
                UI_HomeControler.Inst.AddUI(UI_SelectLevelMgrNew.UI_ResPath);
                UI_HomeControler.Inst.AddUI(UI_Stage.UI_Res);
                StageTemplate stageT = StageModule.GetStageTemplateById(data);
                UI_Stage.Inst.Show(stageT);
                break;
            case 2:             //66表--界面跳转表id;
                PropsjumpuiTemplate jumpUiT = DataTemplate.GetInstance().m_PropsacessTable.getTableData(data) as PropsjumpuiTemplate;
                if (jumpUiT != null)
                {
                    UI_HomeControler.Inst.AddUI(jumpUiT.getJumpUIpath());
                }
                break;
            default:
                LogManager.LogError("不支持的获得途径类型" + type);
                break;
        }
    }

    /// <summary>
    /// 物品获得途径是否可以前往;
    /// </summary>
    /// <param name="type"></param>
    /// <param name="data"></param>
    /// <param name="errorStr"></param>
    /// <returns></returns>
    public static bool PropsacessChecker(int type, int data, out string errorStr)
    {
        errorStr = string.Empty;
        switch (type)
        {
            case 1:                 //关卡--是否开启;
                bool isOpen = ObjectSelf.GetInstance().BattleStageData.IsStageOpen(data);
                
                if (!isOpen)
                {
                    errorStr = GameUtils.getString("65propsaccess1name");
                }
                
                return isOpen;
            case 2:                 //打开UI;-- 等级限制;
                int uiOpenLv = 50;
                bool res = ObjectSelf.GetInstance().Level >= uiOpenLv;
                
                if (!res)
                {
                    errorStr = string.Format(GameUtils.getString("65propsaccess2name"), uiOpenLv); 
                }

                return res;
            default:
                LogManager.LogError("不支持的获得途径类型" + type);
                return false;
        }
    }
#endregion
}
