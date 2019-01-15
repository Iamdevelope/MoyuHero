using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using DreamFaction.GameCore;

public class DHTextManager
{
	private static DHTextManager m_Inst = null;

	static public DHTextManager GetInstance()
	{
		if (m_Inst == null)
		{
			m_Inst = new DHTextManager();
		}

		return m_Inst;
	}

	public string GetTextWithIndex(string index)
	{
        
		string str = String.Empty;
		try
		{
			ChsTextTemplate template = (ChsTextTemplate)DataTemplate.GetInstance().m_ChsTextTable.getTableData(index);
			//ChsTextTemplate template = (ChsTextTemplate)strData[index];
			str = template.languageMap[AppManager.Inst.GameLanguage];
		}
		catch
		{
			str = "请修改配置表，找不到" + index;
		}
		
		return str;
	}
}
