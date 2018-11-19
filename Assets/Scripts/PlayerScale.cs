using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家角色缩放
/// </summary>
public class PlayerScale : MonoBehaviour {

    Vector2 oldPos1;
    Vector2 oldPos2;
	
	
	
	void Update ()
    {
        //检测双手指滑动
        if(Input.touchCount==2)
        {
            if (Input.GetTouch(0).phase==TouchPhase.Moved||Input.GetTouch(1).phase==TouchPhase.Moved)
            {
                Vector2 tempPos1 = Input.GetTouch(0).position;
                Vector2 tempPos2 = Input.GetTouch(1).position;

                Vector3 oldSacle = transform.localScale;
                if (IsEnlarge(oldPos1,oldPos2,tempPos1,tempPos2))
                {                    
                    Vector3 TargetSacle = oldSacle * 1.025f;
                    transform.localScale = TargetSacle;
                }
                else
                {
                    Vector3 TargetSacle = oldSacle /1.025f;
                    transform.localScale = TargetSacle;
                }

                oldPos1 = tempPos1;
                oldPos2 = tempPos2;
            }
        }
		
	}

    //判断是放大缩小
    private bool IsEnlarge(Vector2 oldPos1,Vector2 oldPos2,Vector2 newPos1,Vector2 newPos2)
    {
        float oldLength = (oldPos1 - oldPos2).magnitude;
        float newLength = (newPos1 - newPos2).magnitude;
        return oldLength < newLength ? true : false;
    }
}
