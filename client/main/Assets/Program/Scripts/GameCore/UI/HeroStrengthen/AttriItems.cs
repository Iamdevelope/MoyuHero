using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DreamFaction.UI.Core;

public class AttriItems : BaseUI
{
    protected Text title = null;
    protected Text number = null;

    private Transform mTrans = null;

    public AttriItems(Transform trans)
    {
        if (trans == null)
            return;

        mTrans = trans;

        title = mTrans.FindChild("Left_txt").GetComponent<Text>();
        number = mTrans.FindChild("Right_text").GetComponent<Text>();
    }

    public void SetInfo(Transform trans,string str1, string str2)
    {
        mTrans = trans;

        title = mTrans.FindChild("Left_txt").GetComponent<Text>();
        number = mTrans.FindChild("Right_text").GetComponent<Text>();
        Clean();
        title.text = str1;
        number.text = "+"+ str2;
    }

    public void Destroy()
    {
        GameObject.DestroyImmediate(mTrans.gameObject);
    }

    void Clean()
    {
        title.text = "";
        number.text = "";
    }

    public void SetActive(bool active)
    {
        mTrans.gameObject.SetActive(active);
    }
}

