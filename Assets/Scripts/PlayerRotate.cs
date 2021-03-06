﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家角色旋转
/// </summary>
public class PlayerRotate : MonoBehaviour {

    private float xSpeed = 150f;//旋转速度	
	
	
	void Update ()
    {
		if(Input.GetMouseButton(0))
        {
            if(Input.touchCount==1)
            {
                if(Input.GetTouch(0).phase==TouchPhase.Moved)
                {
                    transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * -xSpeed * Time.deltaTime);
                }
            }
        }
	}
}
