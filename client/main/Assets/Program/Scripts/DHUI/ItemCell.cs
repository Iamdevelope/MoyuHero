using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DreamFaction.GameCore;
using DreamFaction.UI.Core;

public class ItemCell : BaseUI
{
	public int _index;

	public int GetIndex()
	{
		return _index;
	}

	public void SetIndex(int index)
	{
		_index = index;
	}
}
