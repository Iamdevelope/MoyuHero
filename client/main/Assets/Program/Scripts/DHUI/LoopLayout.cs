using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.Collections.Generic;

[ExecuteInEditMode]
[RequireComponent ( typeof ( RectTransform ) )]
public class LoopLayout : UIBehaviour
{
    public delegate void UpdateCellEvent ( int index, RectTransform cell );
    public delegate void LoadFinishEvent ();
    public delegate void LoadMoreEvent ();

    public event UpdateCellEvent m_UpdateCellEvent;
    public event LoadFinishEvent m_LoadFinishEvent;
    public event LoadMoreEvent m_LoadMoreEvent;

    [SerializeField]
    protected RectOffset m_Padding = new RectOffset ();

    [SerializeField]
    private ScrollRect m_ScrollRect = null;

    /// <summary>
    /// m_ScrollRect 的 RectTransform 组件
    /// </summary>
    protected RectTransform m_ViewRect = null;

    /// <summary>
    /// 需要创建的对象
    /// </summary>
    [SerializeField]
    RectTransform m_Cell = null;

    [SerializeField]
    RectTransform m_EmptyCell = null;

	/// <summary>
	/// 分割符
	/// </summary>
	[SerializeField]
	RectTransform m_Separator = null;

    /// <summary>
    /// 每一个 Cell 的大小
    /// </summary>
    [SerializeField]
    protected Vector2 m_CellSize = new Vector2 ( 100, 100 );

	[SerializeField]
	protected Vector2 m_SeparatorSize = new Vector2 (0, 0);

    /// <summary>
    /// 间隔 or 空隙
    /// </summary>
    [SerializeField]
    Vector2 m_Spacing = new Vector2 ( 0, 0 );

    /// <summary>
    /// 方向 水品 Or 竖直
    /// </summary>
    [SerializeField]
    Direction m_Direction = Direction.Vertical;

    /// <summary>
    /// self
    /// </summary>
    [NonSerialized]
    private RectTransform m_RectTransform;

    /// <summary>
    /// 非空的 Cell 数量
    /// </summary>
    [ReadOnly]
    [SerializeField]
    protected int m_CellCount = 0;

    /// <summary>
    /// 空的 Cell 数量
    /// </summary>
    [ReadOnly]
    [SerializeField]
    protected int m_EmptyCellCount = 0;


	/// </summary>
	/// 分割符的索引
	/// </summary>
	[ReadOnly]
	[SerializeField]
	protected int m_SeparatorIndex = -1;

    /// <summary>
    /// 行或者列的个数
    /// </summary>
    [SerializeField]
    protected int m_Columns = 1;

    [SerializeField]
    [ReadOnly]
    bool m_IsLoad = false;

    /// <summary>
    /// 起点索引
    /// </summary>
    [SerializeField]
    [ReadOnly]
    private int m_OriginIndex = 0;

    /// <summary>
    /// 结束的点的索引
    /// </summary>
    [SerializeField]
    [ReadOnly]
    private int m_EndIndex = 0;

    [SerializeField]
    private bool m_IsLoadMore = false;

    // 所有 Cell 的位置
    List<Vector2> m_CellPos = new List<Vector2> ();

    /// <summary>
    /// 
    /// </summary>
    [ReadOnly]
    [SerializeField]
    List<RectTransform> m_CellList = new List<RectTransform> ();

    [ReadOnly]
    [SerializeField]
    List<RectTransform> m_EmptyCellList = new List<RectTransform> ();

    public UpdateCellEvent updateCellEvent
    {
        get
        {
            return m_UpdateCellEvent;
        }
        set
        {
            m_UpdateCellEvent = value;
        }
    }

    public LoadFinishEvent loadFinishEvent
    {
        get
        {
            return m_LoadFinishEvent;
        }
        set
        {
            m_LoadFinishEvent = value;
        }
    }

    public LoadMoreEvent loadMoreEvent
    {
        get
        {
            return m_LoadMoreEvent;
        }
        set
        {
            m_LoadMoreEvent = value;
        }
    }

    public RectOffset padding
    {
        get
        {
            return m_Padding;
        }
        set
        {
            m_Padding = value;
        }
    }

    public int cellCount
    {
        get
        {
            return m_CellCount;
        }
        set
        {
            m_CellCount = value;
        }
    }

    public int emptyCellCount
    {
        get
        {
            return m_EmptyCellCount;
        }
        set
        {
            m_EmptyCellCount = value;
        }
    }

	public int SeparatorIndex
	{
		get 
		{
			return m_SeparatorIndex;				
		}
		set 
		{
			if(value % m_Columns == 0)
			{
				m_SeparatorIndex = value;
			}
			else
			{
				m_SeparatorIndex = (value / m_Columns + 1) * m_Columns;
			}
		}
	}

    public int allCellCount
    {
        get
        {
            return m_CellCount + m_EmptyCellCount;
        }
    }

    public int columns
    {
        get
        {
            return m_Columns;
        }
        set
        {
            m_Columns = value;
        }
    }

    public bool isLoadMore
    {
        get
        {
            return m_IsLoadMore;
        }
        set
        {
            m_IsLoadMore = value;
        }
    }

    Direction direction
    {
        get
        {
            if ( m_ScrollRect != null )
            {
                if ( m_ScrollRect.horizontal )
                {
                    m_Direction = Direction.Horizontal;
                }
                else
                {
                    m_Direction = Direction.Vertical;
                }
            }
            return m_Direction;
        }
        set
        {
            m_Direction = value;
            if ( m_Direction == Direction.Horizontal )
            {
                m_ScrollRect.horizontal = true;
                m_ScrollRect.vertical = false;
            }
            else
            {
                m_ScrollRect.horizontal = false;
                m_ScrollRect.vertical = true;
            }
        }
    }

    public Vector2 cellSize
    {
        get
        {
            return m_CellSize + m_Spacing;
        }
        set
        {
            m_CellSize = value;
        }
    }

    public Vector2 viewRect
    {
        get
        {
            if ( m_ViewRect == null )
                m_ViewRect = m_ScrollRect.GetComponent<RectTransform> ();
            return m_ViewRect.sizeDelta;
        }
    }

	public Vector2 SeparatorSize
	{
		get
		{
			return m_SeparatorSize;
		}
		set
		{
			m_SeparatorSize = value;
		}
	}

    public int originIndex
    {
        get
        {
            return m_OriginIndex;
        }
    }

    public int endIndex
    {
        get
        {
            return m_EndIndex;
        }
    }

    public RectTransform rectTransform
    {
        get
        {
            return m_RectTransform ?? ( m_RectTransform = GetComponent<RectTransform> () );
        }
    }

    public List<RectTransform> cellList
    {
        get
        {
            return m_CellList;
        }
    }

    public List<RectTransform> emptyCellList
    {
        get
        {
            return m_EmptyCellList;
        }
    }

    void Awake ()
    {
        if ( m_ScrollRect == null )
        {
            m_ScrollRect = GetComponentInParent<ScrollRect> ();
            if ( m_ScrollRect == null )
            {
                Debug.LogError ( "ScrollRect is null" );
            }
        }

        if ( m_ScrollRect != null )
        {
            if ( m_ScrollRect.horizontal && m_ScrollRect.vertical )
            {
                Debug.LogError ( "ScrollRect 的方向只能够为水平或竖直" );
            }
        }
    }

    void Update ()
    {
        if ( m_IsLoad == false )
            return;

        // 非空
        if ( cellCount > 0 )
        {
            m_OriginIndex = GetOriginIndex ();
            m_EndIndex = GetEndIndex ();
            m_OriginIndex = Mathf.Min ( m_OriginIndex, m_OriginIndex );
            m_EndIndex = Mathf.Min ( m_EndIndex, m_CellCount );

            // 非空
            for ( int i = m_OriginIndex; i < m_EndIndex; ++i )
            {
                // 向下移动
                if ( m_OriginIndex > m_CellList [ 0 ].GetComponent<CellItem> ().index )
                {
                    MoveItemCell ( 0, m_CellList [ m_CellList.Count - 1 ].GetComponent<CellItem> ().index + 1 );
                    //RectTransform cell = m_CellList[0];
                    //m_CellList.RemoveAt(0);
                    //m_CellList.Add(cell);
                }  // 向上移动
                else if ( m_OriginIndex < m_CellList [ 0 ].GetComponent<CellItem> ().index )
                {
                    MoveItemCell ( m_CellList.Count - 1, m_CellList [ 0 ].GetComponent<CellItem> ().index - 1 );
                    //RectTransform cell = m_CellList[m_CellList.Count - 1];
                    //m_CellList.RemoveAt(m_CellList.Count - 1);
                    //m_CellList.Insert(0, cell);
                }
                else
                {
                    break;
                }
            }

            // 非空填补可视范围内
            if ( m_EndIndex > m_CellList [ m_CellList.Count - 1 ].GetComponent<CellItem> ().index + 1 )
            {
                int delta = m_EndIndex - m_CellList [ m_CellList.Count - 1 ].GetComponent<CellItem> ().index - 1;
                for ( int i = 0; i < delta; ++i )
                {
                    int index = m_CellList [ m_CellList.Count - 1 ].GetComponent<CellItem> ().index + 1;
                    RectTransform cell = InstantiateCell ( index );

                    if ( cell.GetComponent<CellItem> () != null )
                        cell.GetComponent<CellItem> ().UpdateItem ( index, cell );
                    if ( updateCellEvent != null )
                        updateCellEvent ( index, cell );
                }
            }
        }

        // 空
        if ( m_EmptyCellCount > 0 )
        {
            m_OriginIndex = GetOriginIndex ();
            m_EndIndex = GetEndIndex ();
            // 空
            int offset = m_EndIndex - m_CellCount;
            if ( offset >= 0 )
            {
                if ( m_EmptyCellList.Count < ( offset ) )
                {
                    for ( int j = 0; j < ( ( offset ) - m_EmptyCellList.Count ); ++j )
                    {
                        InstantiateEmptyCell ( m_CellCount );
                    }
                }

                // 对空的 cell 重新排列位置
                for ( int j = 0; j < m_EmptyCellList.Count; ++j )
                {
                    m_EmptyCellList [ j ].gameObject.SetActive ( true );
                    m_EmptyCellList [ j ].anchoredPosition = GetPosWithIndex ( m_CellCount + j );
                }
            }
            else
            {
                // 对空的 cell 重新排列位置
                for ( int j = 0; j < m_EmptyCellList.Count; ++j )
                {
                    m_EmptyCellList [ j ].gameObject.SetActive ( false );
                }
            }
        }
    }

    /// <summary>
    /// 初始化所有数据之后，再调用这个方法加载所有的 UI
    /// TODO...
    /// </summary>
    public void Reload ()
    {
        m_IsLoad = false;
        m_EmptyCellList.Clear ();
        m_CellList.Clear ();

        // TODO... 有待思考 这样做有限定
        InitCellData ( m_Cell );
        InitCellData ( m_EmptyCell );

        InitTransfromSize ();
        //InitCellPos ();

        Clear ();

        if (m_Separator != null && m_SeparatorIndex > 0)
		{				
			m_Separator.anchorMax = new Vector2 ( 0, 1 );
			m_Separator.anchorMin = new Vector2 ( 0, 1 );
			m_Separator.pivot = new Vector2 ( 0, 1 );

			var Separator = GameObject.Instantiate ( m_Separator ) as RectTransform;
			Separator.SetParent ( transform, false );
			Separator.name = m_Separator.name;

			if(m_Direction == Direction.Horizontal)
			{
				Separator.anchoredPosition = new Vector2((m_SeparatorIndex / m_Columns * cellSize.x + padding.left), -padding.top);
			}
			else
			{
				Separator.anchoredPosition = new Vector2(padding.left, -(m_SeparatorIndex / m_Columns * cellSize.y + padding.top));
			}
		}

        m_OriginIndex = GetOriginIndex ();
        m_EndIndex = GetEndIndex ();

        m_OriginIndex = Mathf.Min ( m_OriginIndex, cellCount );
        m_EndIndex = Mathf.Min ( m_EndIndex, cellCount );

        for ( int i = m_OriginIndex; i < m_EndIndex; ++i )
        {
            InstantiateCell ( i );
        }

        for ( int i = 0; i < m_CellList.Count; ++i )
        {
            if ( m_CellList [ i ].GetComponent<CellItem> () != null )
                m_CellList [ i ].GetComponent<CellItem> ().UpdateItem ( i, m_CellList [ i ] );
            if ( updateCellEvent != null )
                updateCellEvent ( i, m_CellList [ i ] );
        }

        if ( m_CellList.Count > 0 && loadFinishEvent != null )
        {
            loadFinishEvent ();
        }

        m_IsLoad = true;
    }

    /// <summary>
    /// 加载更多
    /// </summary>
    public void ReLoadMore ( int num )
    {
        m_IsLoad = false;
        m_ScrollRect.movementType = ScrollRect.MovementType.Unrestricted;

        float offset = 0.0f;
        //InitCellPos();
        if ( m_Direction == Direction.Horizontal )
        {
            offset = rectTransform.anchoredPosition.x + rectTransform.sizeDelta.x;
            rectTransform.sizeDelta = new Vector2 ( GetRow () * cellSize.x, rectTransform.sizeDelta.y );
            rectTransform.anchoredPosition = new Vector2 ( -offset, rectTransform.anchoredPosition.y );
        }
        else
        {
            offset = rectTransform.anchoredPosition.y + rectTransform.sizeDelta.y;
            rectTransform.sizeDelta = new Vector2 ( rectTransform.sizeDelta.x, GetRow () * cellSize.y );
            //rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, -offset);
            //rectTransform.anchoredPosition = new Vector2 ( rectTransform.anchoredPosition.x, -4269f );// [yao]
            rectTransform.anchoredPosition = new Vector2 ( rectTransform.anchoredPosition.x, -cellSize.y * num + rectTransform.anchoredPosition.y );
        }


        m_ScrollRect.movementType = ScrollRect.MovementType.Elastic;

        m_IsLoad = true;
    }


    public void Clear ()
    {
        if ( rectTransform.childCount != 0 )
        {
            for ( int i = rectTransform.childCount - 1; i >= 0; --i )
            {
                GameObject go = rectTransform.GetChild ( i ).gameObject;
                DestroyImmediate ( go );
            }
        }

        m_CellList.Clear();
    }

    public void ClearSeparator()
    {
        m_SeparatorIndex = -1;

        m_SeparatorSize = Vector2.zero;
    }


    public void UpdateCell ()
    {
        for ( int i = 0; i < m_CellList.Count; ++i )
        {
            if ( m_CellList [ i ].GetComponent<CellItem> () != null )
            {
                m_CellList [ i ].GetComponent<CellItem> ().UpdateItem ( m_CellList [ i ].GetComponent<CellItem> ().index, m_CellList [ i ] );
                m_CellList [ i ].anchoredPosition = GetPosWithIndex ( m_CellList [ i ].GetComponent<CellItem> ().index );
            }
            if ( updateCellEvent != null )
            {
                updateCellEvent ( m_CellList [ i ].GetComponent<CellItem> ().index, m_CellList [ i ] );
            }
        }
    }

    /// <summary>
    /// 移动 Cell，并且更新 Cell
    /// </summary>
    /// <param name="cell"> 移动的 cell 对应的 index</param>
    /// <param name="index">移动到目标的 index </param>
    void MoveItemCell ( int cellIndex, int index )
    {
        if ( index >= cellCount )
            return;
        m_CellList [ cellIndex ].anchoredPosition = GetPosWithIndex ( index );

        if ( m_CellList [ cellIndex ].GetComponent<CellItem> () != null )
            m_CellList [ cellIndex ].GetComponent<CellItem> ().UpdateItem ( index, m_CellList [ cellIndex ] );
        if ( updateCellEvent != null )
            updateCellEvent ( index, m_CellList [ cellIndex ] );

        // TODO... 可优化
        SortItemCell ();
    }

    /// <summary>
    /// 对 CellList 进行排序
    /// </summary>
    void SortItemCell ()
    {
        for ( int i = 0; i < m_CellList.Count - 1; ++i )
        {
            RectTransform item = m_CellList [ i ];
            for ( int j = i; j < m_CellList.Count; ++j )
            {
                RectTransform one = m_CellList [ i ];
                RectTransform two = m_CellList [ j ];
                int oneIndex = m_CellList [ i ].GetComponent<CellItem> ().index;
                int twoIndex = m_CellList [ j ].GetComponent<CellItem> ().index;

                if ( oneIndex > twoIndex )
                {
                    item = m_CellList [ j ];
                    m_CellList [ j ] = m_CellList [ i ];
                    m_CellList [ i ] = item;
                }
            }
        }
    }

    /// <summary>
    /// 定位到一个指定索引
    /// </summary>
    /// <param name="index">定位到的索引</param>
    public void LookAtIndex ( int index )
    {
        index = Mathf.Max ( 0, index );
        index = Mathf.Min ( index, allCellCount );
        int row = index / m_Columns;

        if ( direction == Direction.Horizontal )
        {
            int viewX = ( int ) ( viewRect.x / ( m_CellSize.x + m_Spacing.x ) );
            if ( row > GetRow () - viewX )
            {
                row = row - viewX + 1;
            }

            float width = row * ( m_CellSize.x + m_Spacing.x );
            rectTransform.anchoredPosition = new Vector2 ( -width, rectTransform.anchoredPosition.y );
        }
        else
        {
            int viewY = ( int ) ( viewRect.y / ( m_CellSize.y + m_Spacing.y ) );
            if ( row > GetRow () - viewY )
            {
                row = row - viewY + 1;
            }
            float height = row * ( m_CellSize.y + m_Spacing.y );
            rectTransform.anchoredPosition = new Vector2 ( rectTransform.anchoredPosition.x, -( rectTransform.sizeDelta.y - height ) );
        }
    }


    /// <summary>
    /// 获取一共有多少行 Or 列
    /// </summary>
    /// <returns></returns>
    public int GetRow ()
    {
        int row = 0;
        if ( allCellCount % m_Columns == 0 )
        {
            row = allCellCount / m_Columns;
        }
        else
        {
            row = allCellCount / m_Columns + 1;
        }

        return row;
    }

    public int GetRow ( int count )
    {
        int row = 0;
        if ( count % m_Columns == 0 )
        {
            row = count / m_Columns;
        }
        else
        {
            row = count / m_Columns + 1;
        }

        return row;
    }

    /// <summary>
    /// 实例化非空 Cell
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    RectTransform InstantiateCell ( int index )
    {
        /// 默认空的补全，在最后
        if ( index < m_CellCount )    // 非空
        {
            var cell = GameObject.Instantiate ( m_Cell ) as RectTransform;
            cell.SetParent ( transform, false );
            cell.name = m_Cell.name + index.ToString ();
            cell.anchoredPosition = GetPosWithIndex ( index );
            m_CellList.Add ( cell );
            cell.gameObject.SetActive ( true );
            return cell;
        }
        Debug.LogError ( "LoopLaout InstantiateCell is null" );
        return null;
    }

    /// <summary>
    /// 实例化 empty cell
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    RectTransform InstantiateEmptyCell ( int index )
    {
        if ( index >= m_CellCount && ( index - m_CellCount ) < m_EmptyCellCount )
        {
            var cell = GameObject.Instantiate ( m_EmptyCell ) as RectTransform;
            cell.SetParent ( transform, false );
            cell.name = m_EmptyCell.name + index.ToString ();
            cell.anchoredPosition = GetPosWithIndex ( index );
            m_EmptyCellList.Add ( cell );
            cell.gameObject.SetActive ( true );
            return cell;
        }
        Debug.LogError ( "LoopLaout InstantiateEmptyCell is null" );
        return null;
    }

    /// <summary>
    /// 初始化当前 rectTransfrom 的大小
    /// </summary>
    void InitTransfromSize ()
    {
        if ( m_Direction == Direction.Horizontal )
        {
            rectTransform.sizeDelta = new Vector2 ( GetRow () * cellSize.x, rectTransform.sizeDelta.y );
            rectTransform.sizeDelta += new Vector2 ( padding.left + padding.right, padding.top + padding.bottom );
			rectTransform.sizeDelta += new Vector2 (m_SeparatorSize.x, 0);

            if ( rectTransform.sizeDelta.x < viewRect.x )
            {
                rectTransform.sizeDelta = new Vector2 ( viewRect.x, rectTransform.sizeDelta.y );
            }
        }
        else
        {
            rectTransform.sizeDelta = new Vector2 ( rectTransform.sizeDelta.x, GetRow () * cellSize.y );
            rectTransform.sizeDelta += new Vector2 ( padding.left + padding.right, padding.top + padding.bottom );
			rectTransform.sizeDelta += new Vector2 (0, m_SeparatorSize.y);

            if ( rectTransform.sizeDelta.y < viewRect.y )
            {
                rectTransform.sizeDelta = new Vector2 ( rectTransform.sizeDelta.x, viewRect.y );
            }
        }

        rectTransform.pivot = new Vector2 ( 0, 0 );
        rectTransform.anchorMax = new Vector2 ( 0, 1 );
        rectTransform.anchorMin = new Vector2 ( 0, 1 );
        rectTransform.anchoredPosition = new Vector2 ( 0, -rectTransform.sizeDelta.y );
    }

    /// <summary>
    /// 初始化所有 Cell 的位置
    /// </summary>
    void InitCellPos ()
    {
        m_CellPos.Clear ();
        int row = GetRow ();
        if ( m_Direction == Direction.Horizontal )
        {
            for ( int i = 0; i < row; ++i )
            {
                for ( int j = 0; j < m_Columns; ++j )
                {
                    Vector2 pos = new Vector2 ( cellSize.x * i + padding.left, -cellSize.y * j - padding.top );
                    m_CellPos.Add ( pos );
                }
            }
        }
        else
        {
            for ( int i = 0; i < row; ++i )
            {
                for ( int j = 0; j < m_Columns; ++j )
                {
                    Vector2 pos = new Vector2 ( cellSize.x * j + padding.left, -cellSize.y * i - padding.top );
                    m_CellPos.Add ( pos );
                }
            }
        }
    }

    void InitCellPos ( int count )
    {
        m_CellPos.Clear ();
        int row = GetRow ( count );
        if ( m_Direction == Direction.Horizontal )
        {
            for ( int i = 0; i < row; ++i )
            {
                for ( int j = 0; j < m_Columns; ++j )
                {
                    Vector2 pos = new Vector2 ( cellSize.x * i + padding.left, -cellSize.y * j - padding.top );
                    m_CellPos.Add ( pos );
                }
            }
        }
        else
        {
            for ( int i = 0; i < row; ++i )
            {
                for ( int j = 0; j < m_Columns; ++j )
                {
                    Vector2 pos = new Vector2 ( cellSize.x * j + padding.left, -cellSize.y * i - padding.top );
                    m_CellPos.Add ( pos );
                }
            }
        }
    }

    /// <summary>
    /// 用于无限循环时，获取位置
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    Vector2 GetPosWithIndex ( int index )
    {
        if ( m_Direction == Direction.Horizontal )
        {
            float x = cellSize.x * ( index / m_Columns ) + padding.left;
            float y = cellSize.y * ( index % m_Columns ) + padding.top;
			if(index >= m_SeparatorIndex)
			{
				x += m_SeparatorSize.x;	
			}

            return new Vector2 ( x, -y );
        }
        else
        {

            float x = cellSize.x * ( index % m_Columns ) + padding.left;
            float y = cellSize.y * ( index / m_Columns ) + padding.top;
			if(index >= m_SeparatorIndex)
			{
				y += m_SeparatorSize.y;	
			}

            return new Vector2 ( x, -y );
        }
    }

    void InitCellData ( RectTransform cell )
    {
        if ( cell != null )
        {
            cell.anchorMax = new Vector2 ( 0, 1 );
            cell.anchorMin = new Vector2 ( 0, 1 );
            cell.pivot = new Vector2 ( 0, 1 );
            cell.sizeDelta = m_CellSize;
        }
    }


    /// <summary>
    /// 获取起点的索引值
    /// </summary>sdfa
    /// <returns></returns>
    int GetOriginIndex ()
    {
        if ( m_Direction == Direction.Horizontal )
        {
			int index = ( int ) ( ( -rectTransform.anchoredPosition.x - m_SeparatorSize.y ) / cellSize.x );
            index = index * m_Columns;
            index = Mathf.Min ( index, allCellCount - 1 );
            index = Mathf.Max ( index, 0 );

            return index;
        }
        else
        {
			int row = m_SeparatorIndex / m_Columns;
			int index = ( int ) ( ( rectTransform.anchoredPosition.y + rectTransform.sizeDelta.y - m_SeparatorSize.y) / cellSize.y );
            index = index * m_Columns;
            index = Mathf.Min ( index, allCellCount - 1 );
            index = Mathf.Max ( index, 0 );
            return index;
        }
    }

    /// <summary>
    /// 获取结束的点的索引值
    /// </summary>
    /// <returns></returns>
    int GetEndIndex ()
    {
        if ( m_Direction == Direction.Horizontal )
        {
            int index = ( int ) ( ( -rectTransform.anchoredPosition.x + viewRect.x ) / cellSize.x );
            index = index * m_Columns;
            index += m_Columns;

            index = Mathf.Max ( index, 0 );
            index = Mathf.Min ( index, allCellCount );

            return index;
        }
        else
        {
			int row = m_SeparatorIndex / m_Columns;
			int index = ( int ) ( ( rectTransform.anchoredPosition.y + rectTransform.sizeDelta.y + viewRect.y) / cellSize.y );
            index = index * m_Columns;
            index += m_Columns;
            index = Mathf.Max ( index, 0 );
            index = Mathf.Min ( index, allCellCount );
            return index;
        }
    }

    /// <summary>
    /// 用于编辑器中预览功能
    /// </summary>
    public void PreviewWithEditor ()
    {
        //InitCellPos ( rectTransform.childCount );

        for ( int i = 0; i < rectTransform.childCount; ++i )
        {
            RectTransform cell = rectTransform.GetChild ( i ) as RectTransform;
            InitCellData ( cell );
            Vector2 pos = GetPosWithIndex ( i );
            cell.SetInsetAndSizeFromParentEdge ( RectTransform.Edge.Left, pos.x, m_CellSize.x );
            cell.SetInsetAndSizeFromParentEdge ( RectTransform.Edge.Top, -pos.y, m_CellSize.y );
        }
    }

    public void PreviewWithPlaying ()
    {
        //InitCellPos ();

        try
        {
            for ( int i = 0; i < cellList.Count; ++i )
            {
                RectTransform cell = cellList [ i ];
                CellItem item = cell.GetComponent<CellItem> ();
                if ( item != null )
                {
                    Vector2 pos = GetPosWithIndex ( cell.GetComponent<CellItem> ().index );
                    //cell.anchoredPosition = GetPosWithIndex(cell.GetComponent<CellItem>().index);

                    cell.SetInsetAndSizeFromParentEdge ( RectTransform.Edge.Left, pos.x, m_CellSize.x );
                    cell.SetInsetAndSizeFromParentEdge ( RectTransform.Edge.Top, -pos.y, m_CellSize.y );
                }
            }
        }
        catch ( System.Exception ex )
        {

        }
    }

    enum Direction
    {
        Horizontal,
        Vertical
    }

    void ResetLayout ()
    {
        if ( m_Columns <= 0 )
            return;

        if ( Application.isPlaying )
        {
            PreviewWithPlaying ();
        }
        else
        {
            PreviewWithEditor ();
        }
    }

#if UNITY_EDITOR
    protected void OnTransformChildrenChanged ()
    {
		if ( ! Application.isPlaying )
		{
			PreviewWithEditor ();
		}
    }
    
    protected override void OnValidate ()
    {
        ResetLayout ();
    }
#endif
}


