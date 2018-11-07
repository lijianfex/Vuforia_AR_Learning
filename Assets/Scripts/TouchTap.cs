using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 手的交互功能
/// </summary>
public class TouchTap : MonoBehaviour
{
    private float touchTime;
    private bool isFirstTouch;

    void Update()
    {
        //双击销毁
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                if(IsDoubleTouch())
                {
                    Destroy(hitInfo.collider.gameObject);
                }

                if(IsLongTouch())
                {
                    Destroy(hitInfo.collider.gameObject);
                }

                
            }
        }

    }

    //双击
    public bool IsDoubleTouch()
    {
        
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if (Input.GetTouch(0).tapCount == 2)
            {
                return true;
            }
        }
        return false;
    }


    //长按
    public bool IsLongTouch()
    {
        
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                isFirstTouch = true;
                touchTime = Time.time;
            }
            else if (touch.phase == TouchPhase.Stationary)
            {
                if (isFirstTouch && Time.time - touchTime > 1f)
                {
                    isFirstTouch = false;
                    return true;
                }
            }
            else
            {
                isFirstTouch = false;
            }
        }
        return false;
    }
}
