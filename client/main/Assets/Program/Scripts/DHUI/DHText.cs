using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine;
using UnityEngine.UI;
using DreamFaction.GameCore;


/// <summary>
/// 关于完美的宽和高有一点疑问
/// </summary>
[AddComponentMenu("DHUI/DHText", 10)]
public class DHText : Text
{
	[TextArea(3, 10)]
	[SerializeField]
	public string m_TextIndex = string.Empty;

	protected DHText()
	{ }

	public string textIndex
	{
		get
		{
			return m_TextIndex;
		}
		set
		{
			m_TextIndex = value;
			text = DHTextManager.GetInstance().GetTextWithIndex(textIndex);
		}
	}

	// Use this for initialization
	protected override void Start()
	{
		if (textIndex.CompareTo(String.Empty) != 0 && text.CompareTo("New Text") != 0)
		{
			text = DHTextManager.GetInstance().GetTextWithIndex(textIndex);
			// DEBUG...
			//horizontalOverflow = HorizontalWrapMode.Overflow;
			//verticalOverflow = VerticalWrapMode.Overflow;
		}
	}
}
