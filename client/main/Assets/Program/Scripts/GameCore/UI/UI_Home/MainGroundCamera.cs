using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using DreamFaction.UI;

public class MainGroundCamera : MonoBehaviour
{

    float xSpeed = 0.1f;

    float curXMax = 0.0f;
    float curXMin = 0.0f;

    float x = 0.0f;
    float y = 0.0f;

    float temp_x = 0f;

    [Range(-3f, 3f)]
    public float angle = 0;

    float curTime = 0.0f;
    float allTime = 0.5f;
    bool isRotation = false;

    void Start()
    {
        var angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;

        angle = x;

        curXMax = x + 3f;
        curXMin = x - 3f;
    }

    void Update()
    {
#if UNITY_STANDALONE_WIN
        if (Input.GetMouseButton(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            temp_x = Input.GetAxis("Mouse X") * -1;
            if (temp_x > 0)//限制摄像机在X轴的旋转
            {
                if (x + temp_x * xSpeed < curXMax)
                {
                    x += temp_x * xSpeed;
                }
                else
                {
                    x = curXMax;
                }
            }
            else
            {
                if (x + temp_x * xSpeed > curXMin)
                {
                    x += temp_x * xSpeed;
                }
                else
                {
                    x = curXMin;
                }
            }

            if (UI_MainHome.GetInst() != null)
            {
                UI_MainHome.GetInst().UpdateMainHeroInfo();
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            isRotation = true;
        }
        Quaternion rotation = Quaternion.Euler(0, x, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 2);
#endif

#if UNITY_IPHONE || UNITY_ANDROID
        if (Input.touchCount == 1)
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                temp_x = Input.GetAxis("Mouse X") * -1;
                if (temp_x > 0)//限制摄像机在X轴的旋转
                {
                    if (x + temp_x * xSpeed < curXMax)
                    {
                        x += temp_x * xSpeed;
                    }
                    else
                    {
                        x = curXMax;
                    }
                }
                else
                {
                    if (x + temp_x * xSpeed > curXMin)
                    {
                        x += temp_x * xSpeed;
                    }
                    else
                    {
                        x = curXMin;
                    }
                }

                if ( UI_MainHome.GetInst () != null )
                {
                    UI_MainHome.GetInst ().UpdateMainHeroInfo ();
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            isRotation = true;
        }

        Quaternion rotation = Quaternion.Euler(0, x, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 2);
#endif
    }

    void LateUpdate()
    {
        //Quaternion rotation = Quaternion.Euler(0, x, 0);
        //transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 2);
        //transform.rotation = rotation;

        if (isRotation)
        {
            curTime += Time.deltaTime;
            if (curTime >= allTime)
            {
                curTime = 0.0f;
                isRotation = false;
            }

            if (UI_MainHome.GetInst() != null)
            {
                UI_MainHome.GetInst().UpdateMainHeroInfo();
            }

        }
    }

}
