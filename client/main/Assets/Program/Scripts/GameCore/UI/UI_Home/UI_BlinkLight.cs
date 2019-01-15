using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UI_BlinkLight : MonoBehaviour {
    public bool PlayOnce
    {
        set 
        {
            _play = value;
            m_Pos = -1.0f;
        }
    }
    private bool _play;
    public bool isOnce;
    public float Speed = 1.0f;
    public float Width = 5.0f;
    public float Angle = 68.3f;
    private float m_Tiem;
    public Color color = new Color(1,1,1,1);
    private Material   m_Mat;
    private Image m_Img;
    private Texture2D  m_Tex;
    private float m_Pos;
    void Awake() 
    {
        if (isOnce) return;
        m_Img = GetComponent<Image>();
        m_Tex = m_Img.sprite.texture;
        m_Mat = new Material(Shader.Find("DreamFaction/Effects/ImageFlash"));
        m_Mat.SetTexture("_image", m_Tex);
        m_Img.material = m_Mat;
        m_Mat.SetFloat("_percent",-1.0f);
        m_Mat.SetColor("_flashColor", color);
        m_Mat.SetFloat("_widthRatio", Width);
        m_Mat.SetFloat("_angle", Angle);
    }

    public void Set(Image img) 
    {
        m_Img = img;
        m_Tex = m_Img.sprite.texture;
        m_Mat = new Material(Shader.Find("DreamFaction/Effects/ImageFlash"));
        m_Mat.SetTexture("_image", m_Tex);
        m_Img.material = m_Mat;
        m_Mat.SetFloat("_percent", -1.0f);
        m_Mat.SetColor("_flashColor", color);
        m_Mat.SetFloat("_widthRatio", Width);
        m_Mat.SetFloat("_angle", Angle);
        PlayOnce = true;
    }
	// Update is called once per frame
	void Update () {
        if (_play)
        {
            Once();
        }
        else if (!isOnce)
        {
            Loop();
        }
	
	}

    void Once() 
    {
        if (m_Pos >= 1.95f) 
        {
            m_Mat.SetFloat("_percent", -1.0f);
            return;
        }
        m_Pos = m_Mat.GetFloat("_percent");
        m_Pos += Time.deltaTime * Speed;
        m_Mat.SetFloat("_percent", m_Pos);
        m_Mat.SetColor("_flashColor", color);
        m_Mat.SetFloat("_widthRatio", Width);
        m_Mat.SetFloat("_angle", Angle);
    }

    void Loop() 
    {
        m_Pos = m_Mat.GetFloat("_percent");
        m_Pos += Time.deltaTime * Speed;
        m_Mat.SetFloat("_percent", m_Pos);
        if (m_Pos >= 1.95f) m_Mat.SetFloat("_percent", -1.0f);
        m_Mat.SetColor("_flashColor", color);
        m_Mat.SetFloat("_widthRatio", Width);
        m_Mat.SetFloat("_angle", Angle);
    }
}
