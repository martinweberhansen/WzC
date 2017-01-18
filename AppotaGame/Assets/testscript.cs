using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections.Generic;

public class testscript : MonoBehaviour {

    WebCamTexture mCamera;
    WebCamDevice[] devices;
    bool takePhoto;
    public GameObject plane;
    public Renderer display;
    public List<GameObject> objectsToHide;

	// Use this for initialization
	void Start ()
    {
        devices = WebCamTexture.devices;
        mCamera = new WebCamTexture(Screen.width, Screen.height);
        mCamera.deviceName = devices[0].name;
        plane.GetComponent<Image>().material.mainTexture = mCamera;
        mCamera.Play();
        Camera.onPostRender += MyPostRender;
    }

    // Update is called once per frame
    public void ChangeCameraButton()
    {
        if (devices.Length > 1)
        {
            mCamera.Stop();
            mCamera.deviceName = (mCamera.deviceName == devices[0].name) ? devices[1].name : devices[0].name;
            mCamera.Play();
        }
    }
    public void TakePhoto()
    {
        takePhoto = true;
        foreach (GameObject item in objectsToHide)
        {
            item.SetActive(false);
        }
    }

    public IEnumerator SaveScreenshotRoutine(Action<Texture2D> result)
    {
        yield return new WaitForEndOfFrame();
        int width = 500;
        int height = 500;
        var tex = new Texture2D(width, height, TextureFormat.RGB24, false);
        tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        tex.Apply();
        result(tex);
    }


    public void MyPostRender(Camera cam)
    {
               
        if (takePhoto)
        {
            //Texture2D capturedPhoto = new Texture2D(2000, 2000);
            //capturedPhoto.ReadPixels(new Rect(0, 0, 2000, 2000), 0, 0, false);
            //capturedPhoto.Apply();
            //display.material.mainTexture = capturedPhoto;
            //byte[] bytes;
            //bytes = capturedPhoto.EncodeToPNG();
            //System.IO.File.WriteAllBytes(Application.persistentDataPath + "/p.png", bytes);
            takePhoto = false;
            Application.CaptureScreenshot("C:/Users/MarkVisbech/AppData/LocalLow/DefaultCompany/AppotaGame/s.png");

            int tid = 0;
            int tidmax = 50;
            while (tid > tidmax)
            {
                tid++;
            }

            byte[] imageString = File.ReadAllBytes(Application.persistentDataPath + "/s.png");
            Texture2D tex = new Texture2D(4, 4, TextureFormat.RGB24, false);
            tex.LoadImage(imageString);
            display.material.SetTexture("_MainTex", tex);

            foreach (GameObject item in objectsToHide)
            {
                item.SetActive(true);
            }
        }
    }
}
