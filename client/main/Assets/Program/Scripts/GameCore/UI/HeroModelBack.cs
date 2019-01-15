using UnityEngine;
using System.Collections;

public class HeroModelBack : MonoBehaviour
{
    public static HeroModelBack Inst;
    // Use this for initialization
    void Start()
    {
        Inst = this;
    }

    public void ClearBg()
    {
        for (int i = 0; i < transform.childCount; ++i)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    public void ChangePanel(string name)
    {
        ClearBg();

        transform.FindChild(name).gameObject.SetActive(true);
    }
}
