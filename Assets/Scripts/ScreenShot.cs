using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

/// <summary>
/// 截屏功能
/// </summary>
public class ScreenShot : MonoBehaviour {

    private Camera renderCamera;

    public string CameraName = "ARCamera";

    public bool isUI = true;
	
	void Start ()
    {
        renderCamera = GameObject.Find(CameraName).GetComponent<Camera>();//获取相机组件
	}
	
	void Update ()
    {
		
	}

    public void OnScrenShotClick()
    {
        string fileName = null;
        System.DateTime dateTime = System.DateTime.Now;

        fileName = "AR" + dateTime.ToString().Trim().Replace("/", "-") + ".png";

        if(Application.platform==RuntimePlatform.Android)
        {
            RenderTexture rt = new RenderTexture(Screen.width, Screen.height, (int)renderCamera.depth);
            if (!isUI)
            {
                renderCamera.targetTexture = rt;
                renderCamera.Render();
                RenderTexture.active = rt;
            }

            Texture2D texture = new Texture2D(Screen.width, Screen.height);
            texture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
            texture.Apply();

            renderCamera.targetTexture = null;
            RenderTexture.active = null;
            Destroy(rt);

            byte[] bytes= texture.EncodeToPNG();

            string destination = "/sdcard/DCIM/Screenshots";
            if(!Directory.Exists(destination))
            {
                Directory.CreateDirectory(destination);
            }

            string savePath = destination + "/" + fileName;

            File.WriteAllBytes(savePath, bytes);

        }

    }


}
