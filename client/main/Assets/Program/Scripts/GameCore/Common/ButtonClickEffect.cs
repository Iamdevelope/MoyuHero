using UnityEngine;
using System.Collections;
using  UnityEngine.UI;
using UnityEditor;

public class ButtonClickEffect : MonoBehaviour {

    private Button m_Button;
    private Animator m_Animator;
    private RuntimeAnimatorController _Controller;
	// Use this for initialization
    void Awake()
    {
      
    }
	void Start () 
    {
        m_Button= GetComponent<Button>();
        m_Animator = GetComponent<Animator>();
        if (m_Button == null)
        {
            Debug.LogError("找不到组件Button!");
            return;
        }
        if (m_Animator == null)
        {
            Debug.LogError("找不到组件Animator!");
            //Animator _annimator= gameObject.AddComponent<Animator>();
            //_annimator.runtimeAnimatorController = _Controller;
            //m_Animator = _annimator;
            return;
        }
        m_Button.onClick.AddListener(OnButtonClick);
	}
    void OnDestroy()
    {
        if (m_Button == null || m_Animator == null) return;
        m_Button.onClick.RemoveListener(OnButtonClick);
    }
	
	// Update is called once per frame
	void Update () 
    {
	
	}
    void OnButtonClick()
    {
        if (m_Button == null || m_Animator == null) return;

       m_Animator.Play("ui_bt_onclick");
       //StartCoroutine(OnClickButton());
       
    }
    IEnumerator OnClickButton()
    {
        yield return null;
        m_Animator.Play("ui_bt_onclick");
    }

}
