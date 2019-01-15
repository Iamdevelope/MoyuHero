using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using DreamFaction.UI.Core;
using DreamFaction.UI;

public class LeftPanelDragMove : BaseUI, IDragHandler
{

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log(eventData.pointerDrag.name);
        if (eventData.delta.x > 0.0f)
        {
            //print("right");
            UI_MainHome.GetInst().OnShowTool();

        }
        if (eventData.delta.x < 0.0f)
        {
            //print("left");
            UI_MainHome.GetInst().OnBackTool();
        }
    }
}

