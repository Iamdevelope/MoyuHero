using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

[ExecuteInEditMode]
[AddComponentMenu("Layout/DHGridLayout", 152)]
public class DHGridLayout : GridLayoutGroup
{
    public delegate GameObject LoadCellEvent(int index);
    public delegate void LoadMoreEvent();
    public DHGridLayout()
    { }

    public LoadCellEvent _loadCell;
    public LoadMoreEvent _loadMore;

    [SerializeField]
    private Vector2 _loadLength;
    [SerializeField]
    private Vector2 _offsetLength;

    [SerializeField]
    private int _index;
    [SerializeField]
    public int cellCount;

    public bool isAutoLoad = true;

    RectTransform _selfTransform;
    private RectTransform selfTransform
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

    RectTransform _parnetRectTransform;
    private RectTransform parentRectTransform
    {
        get
        {
            if (_parnetRectTransform == null)
            {
                _parnetRectTransform = selfTransform.parent.GetComponent<RectTransform>();
            }

            return _parnetRectTransform;
        }
    }

    public LoadCellEvent loadCell
    {
        get
        {
            return _loadCell;
        }
        set
        {
            _loadCell = value;
        }
    }

    public LoadMoreEvent loadMore
    {
        get
        {
            return _loadMore;
        }
        set
        {
            _loadMore = value;
        }
    }

    void Start()
    {
        selfTransform.anchorMin = new Vector2(0.0f, 1.0f);
        selfTransform.anchorMax = new Vector2(0.0f, 1.0f);
        selfTransform.pivot = new Vector2(0.0f, 1.0f);
    }

    void LateUpdate()
    {
        if (!isAutoLoad)
            return;

        while ( _index < cellCount )
        {
            if (_index < cellCount)
            {

                LoadCellWithIndex(_index);
            }
        }

        _offsetLength = CalculateOffsetLength();
        if (startAxis == Axis.Horizontal)
        {
            if (_loadLength.y < _offsetLength.y)
            {
            }
        }
        else
        {
            if (_loadLength.x < _offsetLength.x)
            {
                if (_index < cellCount)
                {
                    LoadCellWithIndex(_index);
                }
                else
                {
                    LoadMoreCell();
                }
            }
        }
    }

    /// <summary>
    /// 添加一个 cell 到布局中
    /// </summary>
    /// <param name="cell"> 添加的 cell</param>
    /// <param name="index">需要添加到的位置</param>
    public bool AddCell(GameObject cell, int index)
    {
        _index++;
        try
        {
            cell.gameObject.SetActive(true);
            cell.transform.SetParent(selfTransform, false);
            cell.transform.SetSiblingIndex(index);

            if (startAxis == Axis.Horizontal)
            {
                _loadLength.y = (_index / constraintCount) * cellSize.y;
                _loadLength.y += (_index / constraintCount) * spacing.y;
            }
            else
            {
                _loadLength.x = (_index / constraintCount) * cellSize.x;
                _loadLength.x += (_index / constraintCount) * spacing.x;
            }
            return true;
        }
        catch
        {
        }

        return false;
    }

    public bool AddCellToFirst(GameObject cell)
    {
        return AddCell(cell, 0);
    }

    public bool AddCellToLast(GameObject cell)
    {
        return AddCell(cell, selfTransform.childCount);
    }

    // 移除第 index 个 gameObject
    public bool RemoveCell(int index)
    {
        if (index < 0 || index >= selfTransform.childCount)
        {
            return false;
        }

        try
        {
            Destroy(selfTransform.GetChild(index));

            _index--;
            if (startAxis == Axis.Horizontal)
            {
                _loadLength.y = (_index / constraintCount) * cellSize.y;
                _loadLength.y += (_index / constraintCount - 1) * spacing.y;
            }
            else
            {
                _loadLength.x = (_index / constraintCount) * cellSize.x;
                _loadLength.x += (_index / constraintCount) * spacing.x;
            }
            return true;
        }
        catch
        {

        }

        return false;
    }

    public bool RemoveCellWithFirst()
    {
        return RemoveCell(0);
    }

    public bool RemoveCellWithLast()
    {
        return RemoveCell(selfTransform.childCount - 1);
    }

    Vector2 CalculateOffsetLength()
    {
        Vector2 offset = new Vector2();
        if (startAxis == Axis.Horizontal)
        {
            offset.y = -(-selfTransform.anchoredPosition.y - parentRectTransform.sizeDelta.y);
            offset.y += (_index / constraintCount - 1) * spacing.y;
        }
        else
        {
            offset.x = -(selfTransform.anchoredPosition.x - parentRectTransform.sizeDelta.x);
            offset.x += (_index / constraintCount) * spacing.x;
        }

        return offset;
    }

    public void Reload()
    {
        selfTransform.anchoredPosition = new UnityEngine.Vector2(0, 0);
        for (int i = 0; i < selfTransform.childCount; ++i)
        {
            Destroy(selfTransform.GetChild(i).gameObject);
        }

        _loadLength = new Vector2(0, 0);
        _offsetLength = new Vector2(0, 0);

        _index = 0;
        cellCount = 0;
    }

    void LoadCellWithIndex(int index)
    {
        GameObject cell;
        try
        {
            cell = loadCell(index);

            AddCell(cell, index);
        }
        catch
        {
            Debug.Log(index.ToString() + " cell is null");
        }
    }

    void LoadMoreCell()
    {
        try
        {
            loadMore();
        }
        catch
        {

        }
    }
}
