using UnityEngine;
using System.Collections;

public class RecruitScene : MonoBehaviour
{
    protected GameObject recryitBody;
    protected GameObject lightning_summon;
    protected GameObject lightning_falling;
    protected GameObject body;

    bool isPlaying = false;
    // Use this for initialization
    void Start()
    {
        recryitBody = transform.FindChild("Recruit_Prometheus02").gameObject;
        lightning_summon = transform.FindChild("Recruit_Effects/lightning_summon").gameObject;
        lightning_falling = transform.FindChild("Recruit_Effects/lightning_falling_surround").gameObject;
        body = transform.FindChild("Recruit_Prometheus02/Body").gameObject;

        recryitBody.SetActive(false);
        lightning_summon.SetActive(false);
        lightning_falling.SetActive(false);
    }

    public void PlayEffect()
    {
        recryitBody.SetActive(true);
        lightning_summon.SetActive(true);
        lightning_falling.SetActive(true);

        body.GetComponent<Dissolve>().isPlaying = true;
        isPlaying = true;
    }

    public void StopEffect()
    {
        recryitBody.SetActive(false);
        lightning_summon.SetActive(false);
        lightning_falling.SetActive(false);
        body.GetComponent<Dissolve>().isPlaying = false;
    }

    // TODO...
    void Update()
    {
        if (isPlaying)
        {

        }
        
    }
}

