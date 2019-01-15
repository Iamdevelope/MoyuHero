using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
public class EventTriggerListener : MonoBehaviour
   , IPointerClickHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler, IBeginDragHandler, IDragHandler, IEndDragHandler,ISelectHandler
{

    public delegate void VoidDelegate(GameObject go);

    public delegate void EventDataDelegate(GameObject go, PointerEventData data = null);
    public delegate void BoolDelegate(GameObject go, bool data);

    public VoidDelegate onClick;

    public VoidDelegate onDown;

    public VoidDelegate onEnter;

    public VoidDelegate onExit;

    public VoidDelegate onUp;

    public EventDataDelegate onBeginDrag;

    public EventDataDelegate onDrag;

    public EventDataDelegate onEndDrag;

    public VoidDelegate onSelect;

    public VoidDelegate onPress;

    bool isPress = false;

    public float pressInterval = 0.5f;
    private float m_InitPressInterval = 0.5f;

    public float InitPressInterval
    {
        get
        {
            return m_InitPressInterval;
        }
        set
        {
            pressInterval = m_InitPressInterval = value;
        }
    }

    /// <summary>
    /// 是否需要重置press时间间隔;
    /// </summary>
    public bool needResetInterval = false;

    private float mDelta = 0f;

    public object param = null;

    public static EventTriggerListener Get (GameObject go)
    { 

    EventTriggerListener listener = go.GetComponent<EventTriggerListener>(); 

    if (listener == null) listener = go.AddComponent<EventTriggerListener>(); 

        return listener; 

    } 

    public void OnPointerClick(PointerEventData eventData)
    { 

        if(onClick != null) onClick(gameObject); 

    } 

    public void OnPointerDown (PointerEventData eventData)
    { 
        if(onDown != null) onDown(gameObject);

        //if (onPress != null) onPress(gameObject, eventData.pointerPress == gameObject);
        isPress = true;
    } 

    public void OnPointerEnter (PointerEventData eventData)
    { 

        if(onEnter != null) onEnter(gameObject); 

    } 

    public void OnPointerExit (PointerEventData eventData)
    { 

        if(onExit != null) onExit(gameObject); 

    } 

    public void OnPointerUp (PointerEventData eventData)
    { 

        if(onUp != null) onUp(gameObject);

        //if (onPress != null) onPress(gameObject, false);
        isPress = false;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (onBeginDrag != null) onBeginDrag(gameObject, eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (onDrag != null) onDrag(gameObject, eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (onEndDrag != null) onEndDrag(gameObject, eventData);
    }

    public void OnSelect(BaseEventData eventData)
    {
        if(onSelect != null) onSelect(gameObject);
    }

    public void RemoveAllListener()
    {
        Destroy(this);
    }

    void ResetPressInterval()
    {
        pressInterval = InitPressInterval;
    }

    void ResetDelta()
    {
        mDelta = 0f;
    }

    void Update()
    {
        if (onPress != null)
        {
            if(isPress)
            {
                mDelta += Time.deltaTime;

                if (mDelta >= pressInterval)
                {
                    mDelta = 0f;
                    onPress(gameObject);
                }
            }
            
            else
            {
                ResetDelta();
                if(needResetInterval)
                    ResetPressInterval();
            }
        }
    }
} 
