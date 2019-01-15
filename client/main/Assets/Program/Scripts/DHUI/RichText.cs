using UnityEngine;
using UnityEngine.UI;
using System.Xml.Linq;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.Serialization;
using DreamFaction.Utils;
using DreamFaction.UI;
/// <summary>
/// 按照一定格式自动解析字符串生成富文本
/// 警告，不要在这个对象下有其他子节点，会被自动移除
/// 需要配置的字符串格式
/// 恭喜您获得了;<Image res='UI_Buzhen_11' height='16' width='16' />;X1000！
/// </summary>

[RequireComponent(typeof(RectTransform))]
public class RichText : Text
{
    public float _width;   // 子节点的宽的和
    public float _height;  // 子节点的高的和
    public int _lastIndex; // 先前的计数索引
    public int _index;  // 计数索引
    public float _allWidth;  // 总的宽
    public float _space; // 上下两行间隔的距离

    [TextArea(3, 10)]
    [SerializeField]
    public string _textCache;   // 文本索引

    [SerializeField]
    [FormerlySerializedAs("ImageAlignment")] 
    private ImageAlignment _imageAlignment;  // 对其方式

    [SerializeField]
    [FormerlySerializedAs("ChildAlignment")]
    private ChildAlignment _childAlignment = ChildAlignment.MiddleCenter;  // 对其方式

    private RectTransform _selfTransform;
    RectTransform selfTransform
    {
        get
        {
            if (_selfTransform == null)
            {
                _selfTransform = GetComponent<RectTransform>();
            }

            return _selfTransform;
        }
    }

    //public string text
    //{
    //    set
    //    {
    //        base.text = "";
    //        _textCache = value;
    //    }
    //}

    public void DestroyAllChild()
    {
        for (int i = selfTransform.childCount - 1; i >= 0; --i)
        {
            rectTransform.GetChild(i).gameObject.SetActive(false);
            DestroyImmediate(rectTransform.GetChild(i).gameObject);
        }
    }

    public void ShowRichText(string richText)
    {
        DestroyAllChild();
        _textCache = richText;
        if (_textCache == "")
            return;

        _height = selfTransform.sizeDelta.y / 2;
        _width = -selfTransform.sizeDelta.x / 2;
        _lastIndex = 0;
        _index = 0;
        _allWidth = 0;

        string[] textList = richText.Split(';');

        for (int i = 0; i < textList.Length; ++i)
        {
            try
            {
                XDocument doc2 = XDocument.Parse(textList[i]);
                if (doc2 != null)
                {
                    XElement Root = doc2.Root;
                    if (Root != null && (Root.Name == "Image" || Root.Name == "image"))
                    {
                        GameObject child = new GameObject("Image");
                        child.transform.SetParent(selfTransform, false);
                        child.SetActive(true);
                        RectTransform rectTransform = child.AddComponent<RectTransform>();
                        Image image = child.AddComponent<Image>();

                        // res 
                        XAttribute attres = Root.Attribute("res");
                        image.overrideSprite = UIResourceMgr.LoadSprite(common.uiPath + attres.Value);

                        // width
                        XAttribute attwidth = Root.Attribute("width");
                        int width = int.Parse(attwidth.Value);

                        // height
                        XAttribute attheight = Root.Attribute("height");
                        int height = int.Parse(attheight.Value);

                        if (_allWidth + width > selfTransform.sizeDelta.x)
                        {
                            // 这里减 1 是因为 Image 需要放在下一行
                            CalculateLayout(selfTransform.childCount - 1);
                        }

                        switch (_imageAlignment)
                        {
                            case ImageAlignment.Upper:
                                {
                                    rectTransform.anchoredPosition = new Vector2(_width + width / 2, _height - height / 2);
                                    rectTransform.sizeDelta = new Vector2(width, height);
                                }
                                break;
                            case ImageAlignment.Middle:
                                {
                                    rectTransform.anchoredPosition = new Vector2(_width + width / 2, _height - (fontSize + 4) / 2);
                                    rectTransform.sizeDelta = new Vector2(width, height);
                                }
                                break;
                            case ImageAlignment.Lower:
                                {
                                    rectTransform.anchoredPosition = new Vector2(_width + width / 2, _height - (fontSize + 4) + height / 2);
                                    rectTransform.sizeDelta = new Vector2(width, height);
                                }
                                break;
                            default:
                                break;
                        }
                        _width += rectTransform.sizeDelta.x;
                        _allWidth += width;
                    }
                }
            }
            catch (System.Exception ex)
            {
                ShowText(textList[i]);
            }
        }

        CalculateLayout(selfTransform.childCount);
    }

    void ShowText(string str)
    {
        GameObject child = new GameObject("Text");
        child.transform.SetParent(selfTransform);
        child.SetActive(true);
        RectTransform rectTransform = child.AddComponent<RectTransform>();
        rectTransform.localPosition = new Vector3(0, 0, 0);
        rectTransform.localScale = new Vector3(1, 1, 1);
        Text lbl = child.AddComponent<Text>();
        lbl.text = str;
        lbl.font = this.font;
        lbl.fontStyle = this.fontStyle;
        lbl.fontSize = this.fontSize;
        lbl.lineSpacing = this.lineSpacing;
        lbl.supportRichText = this.supportRichText;
        lbl.alignment = this.alignment;
        lbl.horizontalOverflow = this.horizontalOverflow;
        lbl.verticalOverflow = this.verticalOverflow;
        lbl.resizeTextForBestFit = this.resizeTextForBestFit;
        lbl.color = this.color;
        lbl.material = this.material;

        if (_allWidth + lbl.preferredWidth <= selfTransform.sizeDelta.x)
        {
            lbl.rectTransform.anchoredPosition = new Vector2(_width + lbl.preferredWidth / 2, _height - lbl.preferredHeight / 2);
            lbl.rectTransform.sizeDelta = new Vector2(lbl.preferredWidth, lbl.preferredHeight + 4);

            rectTransform.sizeDelta = new Vector2(lbl.preferredWidth, lbl.preferredHeight + 4);
            rectTransform.anchoredPosition = new Vector2(_width + lbl.preferredWidth / 2, _height - lbl.preferredHeight / 2);
            _width += lbl.preferredWidth;
            _allWidth += lbl.preferredWidth;
        }
        else
        {
            for (int j = str.Length - 1; j >= 0; --j)
            {
                string temp = str.Substring(0, j);
                lbl.text = temp;
                if (_allWidth + lbl.preferredWidth <= selfTransform.sizeDelta.x)
                {
                    lbl.rectTransform.anchoredPosition = new Vector2(_width + lbl.preferredWidth / 2, _height - lbl.preferredHeight / 2);
                    lbl.rectTransform.sizeDelta = new Vector2(lbl.preferredWidth, lbl.preferredHeight + 4);

                    rectTransform.sizeDelta = new Vector2(lbl.preferredWidth, lbl.preferredHeight + 4);
                    rectTransform.anchoredPosition = new Vector2(_width + lbl.preferredWidth / 2, _height - lbl.preferredHeight / 2);
                    _width += lbl.preferredWidth;
                    _allWidth += lbl.preferredWidth;

                    //
                    CalculateLayout(selfTransform.childCount);

                    // 
                    if (j < (str.Length - 1))
                        ShowText(str.Substring(j, str.Length - j));
                    break;
                }
            }
        }
    }

    // 重新计算布局
    void CalculateLayout(int childCount)
    {
        // 清理数据
        _width = -selfTransform.sizeDelta.x / 2;
        _allWidth = 0;

        float maxHeight = 0;
        for (int i = _index; i < childCount; ++i)
        {
            RectTransform rectTransform = selfTransform.GetChild(i).GetComponent<RectTransform>();
            if (rectTransform.sizeDelta.y > maxHeight)
            {
                maxHeight = rectTransform.sizeDelta.y;
            }
        }

        _lastIndex = _index;
        _index = childCount;

        _height -= maxHeight;
        if (_lastIndex != 0)
            _height -= _space;

        float width = -selfTransform.sizeDelta.x / 2;
        switch (_imageAlignment)
        {
            case ImageAlignment.Upper:
                {
                    for (int i = _lastIndex; i < _index; ++i)
                    {
                        RectTransform rectTransform = selfTransform.GetChild(i).GetComponent<RectTransform>();
                        rectTransform.anchoredPosition = new Vector2(width + rectTransform.sizeDelta.x / 2, _height + maxHeight - rectTransform.sizeDelta.y / 2);
                        width += rectTransform.sizeDelta.x;
                    }
                }
                break;
            case ImageAlignment.Middle:
                {
                    // 默认中对齐
                }
                break;
            case ImageAlignment.Lower:
                {
                    for (int i = _lastIndex; i < _index; ++i)
                    {
                        RectTransform rectTransform = selfTransform.GetChild(i).GetComponent<RectTransform>();
                        rectTransform.anchoredPosition = new Vector2(width + rectTransform.sizeDelta.x / 2, _height + rectTransform.sizeDelta.y / 2);
                        width += rectTransform.sizeDelta.x;
                    }
                }
                break;
            default:
                break;
        }

        switch (_childAlignment)
        {
            case ChildAlignment.UpperLeft:
                {

                }
                break;
            case ChildAlignment.UpperCenter:
                {
                    width = 0;
                    float allWidth = 0;
                    for (int i = _lastIndex; i < _index; ++i)
                    {
                        allWidth += selfTransform.GetChild(i).GetComponent<RectTransform>().sizeDelta.x;
                    }

                    width = allWidth / -2;
                    for (int i = _lastIndex; i < _index; ++i)
                    {
                        RectTransform rectTransform = selfTransform.GetChild(i).GetComponent<RectTransform>();
                        rectTransform.anchoredPosition = new Vector2(width + rectTransform.sizeDelta.x / 2, rectTransform.anchoredPosition.y);
                        width += rectTransform.sizeDelta.x;
                    }
                }
                break;
            case ChildAlignment.MiddleCenter:
                {
                    width = 0;
                    float allWidth = 0;
                    for (int i = _lastIndex; i < _index; ++i)
                    {
                        allWidth += selfTransform.GetChild(i).GetComponent<RectTransform>().sizeDelta.x;
                    }

                    width = allWidth / -2;
                    for (int i = _lastIndex; i < _index; ++i)
                    {
                        RectTransform rectTransform = selfTransform.GetChild(i).GetComponent<RectTransform>();
                        rectTransform.anchoredPosition = new Vector2(width + rectTransform.sizeDelta.x / 2, rectTransform.anchoredPosition.y);
                        width += rectTransform.sizeDelta.x;
                    }
                }
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// image 的对齐方式
    /// </summary>
    enum ImageAlignment
    {
        Upper,
        Middle,
        Lower,
    }

    enum ChildAlignment
    {
        UpperLeft,
        UpperCenter,
        UpperRight,
        MiddleLeft,
        MiddleCenter,
        MiddleRight,
        LowerLeft,
        LowerCenter,
        LowerRight,
    }

    public static RichText GetRichText(string str)
    {
        GameObject obj = Instantiate(Resources.Load("UI/Prefabs/UI_Home/RichText")) as GameObject;
        RichText richText = obj.GetComponent<RichText>();
        richText.ShowRichText(str);
        return richText;
    }

    //protected override void OnFillVBO(List<UIVertex> vbo)
    //{
    //	Debug.Log(cachedTextGenerator.verts.Count.ToString());
    //	base.OnFillVBO(vbo);
    //	//cachedTextGenerator.verts.Count;
    //	Debug.Log(cachedTextGenerator.verts.Count);
    //}

#if UNITY_EDITOR
    void OnValidate()
    {
        //for (int i = 0; i < selfTransform.childCount; ++i)
        //{
        //    UnityEditor.EditorApplication.delayCall += () =>
        //    {
        //        if (selfTransform.childCount > 0)
        //        {
        //            if (Application.isPlaying)
        //                Destroy(selfTransform.GetChild(0).gameObject);
        //            else
        //                DestroyImmediate(selfTransform.GetChild(0).gameObject);
        //        }
        //    };
        //}

        //if (!Application.isPlaying)
        //{
        //    UnityEditor.EditorApplication.delayCall += () =>
        //    {
        //        ShowRichText(_textCache);
        //    };
        //}
    }
#endif
}
