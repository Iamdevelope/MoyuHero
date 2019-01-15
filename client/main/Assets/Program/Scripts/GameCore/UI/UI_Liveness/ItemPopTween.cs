using UnityEngine;
using System.Collections;
using DG.Tweening;
using DreamFaction.GameCore;

public class ItemPopTween : BaseControler
{
    //protected override void InitData()
    //{
    //    gameObject.SetActive(false);
    //    Sequence mySequence = DOTween.Sequence();
    //    //mySequence.Append(this.gameObject.transform.DOScale(new Vector3(0.2f, 0.2f, 0.2f), 0.04f));
    //    mySequence.Append(this.gameObject.transform.DOScale(new Vector3(0.1f, 0.1f, 0.1f), 0.02f));
    //    mySequence.AppendCallback(MiddleShow);
    //    mySequence.SetUpdate(true);
    //}


    //private void MiddleShow()
    //{
    //    gameObject.SetActive(true);
    //    Sequence mySequence = DOTween.Sequence();
    //    //mySequence.Append(mSkillImage.transform.DOScaleY(1, 0.5f).SetEase(Ease.OutElastic));
    //    //mySequence.AppendInterval(0.1f);
    //    mySequence.Append(this.gameObject.transform.DOScale(new Vector3(1.2f, 1.2f, 1), 0.18f).SetEase(Ease.OutQuad));
    //    mySequence.AppendCallback(onShowNameEnd);
    //    mySequence.SetUpdate(true);
    //}

    //private void onShowNameEnd()
    //{
    //    this.gameObject.transform.DOScale(new Vector3(1.0f, 1.0f, 1), 0.05f);
    //}
    //} 
    protected override void InitData()
    {
        gameObject.SetActive(false);
        this.gameObject.transform.DOScale(new Vector3(0.1f, 0.1f, 0.1f), 0.02f).SetEase(Ease.OutQuad).SetUpdate(true).OnComplete(MiddleShow);
    }


    private void MiddleShow()
    {
        gameObject.SetActive(true);
        this.gameObject.transform.DOScale(new Vector3(1.2f, 1.2f, 1), 0.23f).SetEase(Ease.OutQuad).SetUpdate(true).OnComplete(onShowNameEnd);
    }

    private void onShowNameEnd()
    {
        this.gameObject.transform.DOScale(new Vector3(1.0f, 1.0f, 1), 0.05f);
    }
}
