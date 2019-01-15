using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[ExecuteInEditMode]
public class UIHeroStar : UIBehaviour{

    private const int DEF_MAX = 5;

    [SerializeField]
    private GridLayoutGroup.Axis direction = GridLayoutGroup.Axis.Horizontal;

    [SerializeField, Range(0, 8)]
    private int max = DEF_MAX;

    [SerializeField, Range(0, DEF_MAX)]
    private int cur = 3;

    [SerializeField]
    private int size = 40;

    [SerializeField]
    private int space = 1;

    [SerializeField]
    private bool isGenerate = false;

    private string path = "Assets/Art/UI/UI_Bag/";
    private string startName = "Advanced_Star01.png";
    private string darkName = "Advanced_Star02.png";

    public Sprite starSprite;
    public Sprite dartSprite;


    protected override void Awake()
    {
        base.Awake();
    }

    private void getSprite()
    {
        if (starSprite == null)
            starSprite = Resources.LoadAssetAtPath("Assets/Art/UI/UI_Bag/Advanced_Star01.png", typeof(Sprite)) as Sprite;
        if (darkName == null)
            dartSprite = Resources.LoadAssetAtPath("Assets/Art/UI/UI_Bag/Advanced_Star02.png", typeof(Sprite)) as Sprite;
    }
    private void setBound()
    {
        RectTransform mTransform = transform as RectTransform;
        int feet = size * max + (max - 1) * space;
        mTransform.sizeDelta = direction == GridLayoutGroup.Axis.Horizontal ? new Vector2(feet, size) : new Vector2(size, feet);
    }

    public void Set(int cur, int max)
    {
        DestroyChildren(transform);

        getSprite();

        this.max = max;
        this.cur = cur;

        setBound();

        GridLayoutGroup grid = gameObject.GetComponent<GridLayoutGroup>();
        if (grid == null)
            grid = gameObject.AddComponent<GridLayoutGroup>();
        grid.cellSize = new Vector2(size, size);
        grid.spacing = new Vector2(space, space);
        grid.startAxis = direction;

        for (int i = 0; i < max; i++)
        {
            GameObject star = new GameObject();
            star.name = "star" + (i + 1);
            RectTransform startT = star.GetComponent<RectTransform>();
            if (startT == null) startT = star.AddComponent<RectTransform>();
            startT.SetParent(transform, false);
            startT.sizeDelta = new Vector2(size, size);

            GameObject dark = new GameObject();
            dark.name = "dark";
            startT = dark.GetComponent<RectTransform>();
            if (startT == null) startT = dark.AddComponent<RectTransform>();
            Image img = dark.GetComponent<Image>();
            if (img == null) img = dark.AddComponent<Image>();

            img.overrideSprite = dartSprite;
            startT.SetParent(star.transform, false);
            startT.sizeDelta = new Vector2(size, size);

            if (i >= cur) continue;

            GameObject lightStar = new GameObject();
            lightStar.name = "lstar";
            startT = lightStar.GetComponent<RectTransform>();
            if (startT == null) startT = lightStar.AddComponent<RectTransform>();
            img = lightStar.GetComponent<Image>();
            if (img == null) img = lightStar.AddComponent<Image>();
            img.overrideSprite = starSprite;
            startT.SetParent(star.transform, false);
            startT.sizeDelta = new Vector2(size, size);
        }
        
    }

    public void Set()
    {
        Set(this.cur, this.max);
    }
    private void createStar()
    {
        GameObject star = new GameObject();
        star.name = "star";

        RectTransform startT = star.AddComponent<RectTransform>();
     

        startT.SetParent(transform, false);
    }
    public void DestroyChildren(Transform transform)
    {
        if (transform != null)
        {
            for (int i = transform.childCount - 1; i >= 0; i--)
            {
                Transform t = transform.GetChild(i);
                if (t != null)
                {
                    if (t.gameObject != null)
                    {
                        GameObject.DestroyImmediate(t.gameObject);
                    }
                }
            }
        }
    }
#if UNITY_EDITOR
    void Update()
    {
        if (isGenerate)
        {
            isGenerate = false;
            Set();
            //todo...
        }
    }
#endif
}
