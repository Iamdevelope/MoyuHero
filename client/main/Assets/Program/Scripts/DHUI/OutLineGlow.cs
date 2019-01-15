using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
///<summary>
/// Glow UGUI Text,Use the alpha of the color panel to control opacity.
/// </summary>

public class OutLineGlow : Shadow
{

	int m_circleCount = 6;
	int m_firestSample = 4;
	int m_sampleIncrement = 4;
#if UNITY_EDITOR
	protected override void OnValidate()
	{
		base.OnValidate ();
		circleCount = m_circleCount;
		firstSample = m_firestSample;
		sampleIncrement = m_sampleIncrement;
	}
#endif
	public int circleCount
	{
		get
		{
			return m_circleCount;
		}

		set
		{
			m_circleCount = Mathf.Max(value, 1);
			if(graphic != null)
				graphic.SetVerticesDirty();
		}
	}

	public int firstSample
	{
		get
		{
			return m_firestSample;
		}
		set
		{
			m_firestSample = Mathf.Max(value, 2);
			if(graphic != null)
				graphic.SetVerticesDirty();
		}
	}

	public int sampleIncrement
	{
		get
		{
			return m_sampleIncrement;
		}
		set
		{
			m_sampleIncrement = Mathf.Max(value, 1);
			if(graphic != null)
				graphic.SetVerticesDirty();
		}
	}

	protected new void ApplyShadow(List<UIVertex> verts, Color32 color, int start, int end, float x, float y)
	{
		UIVertex vt;

		var neededCpacity = verts.Count + (end - start);

		if (verts.Capacity < neededCpacity) 
		{
			verts.Capacity = neededCpacity;
		}

		for (int i = start; i < end; ++i) 
		{
			vt = verts[i]; 
			verts.Add(vt);

			Vector3 v = vt.position;
			v.x += x;
			v.y += y;
			vt.position = v;

			var newColor = color;
			if(useGraphicAlpha)
				newColor.a = (byte)((newColor.a * verts[i].color.a) / 255);
			vt.color = newColor;
			verts[i] = vt;
		}
	}

	public override void ModifyVertices(List<UIVertex> verts)
	{
		if (!IsActive ())
			return;

		var total = (m_firestSample * 2 + m_sampleIncrement * (m_circleCount - 1)) * m_circleCount / 2;
		verts.Capacity = verts.Count * (total + 1);
		var original = verts.Count;

		var count = 0;
		var sampleCount = m_firestSample;
		var dx = effectDistance.x / circleCount;
		var dy = effectDistance.y / circleCount;

		for (int i = 1; i <= m_circleCount; i++) 
		{
			var rx = dx * i;
			var ry = dy * i;

			var radStep = 2 * Mathf.PI / sampleCount;
			var rad = (i%2) * radStep * 0.5f;

			for(int j = 0; j < sampleCount; j++)
			{
				var next = count + original;
				ApplyShadow(verts, effectColor, count, next, rx * Mathf.Cos(rad), ry * Mathf.Sin(rad));
				count = next;
				rad += radStep;
			}

			sampleCount += m_sampleIncrement;
		}
	}
}

