using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class CamContoroller : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }



    // Update is called once per frame
    void Update()
    {
        Camera camera = Camera.main;

        float sensitiveWheel = 15.0f;
        float sensitivePinch = 1.0f;

        //カメラ倍率によって感度を低くする
        float sensitiveRotate = 2.0f * (camera.fieldOfView / 80f);

        //回転移動
        //PC
        if (Input.GetMouseButton(1))
        {
            float rotateX = -1 * Input.GetAxis("Mouse X") * sensitiveRotate;
            float rotateY = Input.GetAxis("Mouse Y") * sensitiveRotate;
            camera.transform.Rotate(rotateY, rotateX, 0.0f);
            //カメラのロール方向を0に設定
            camera.transform.eulerAngles = new Vector3(camera.transform.localEulerAngles.x, camera.transform.localEulerAngles.y, 0.0f);
        }

        //スマホ
        if (Input.touchCount >= 2)
        {
            float rotateX = -1 * Input.touches[0].deltaPosition.x * sensitiveRotate * 0.02f;
            float rotateY = Input.touches[0].deltaPosition.y * sensitiveRotate * 0.02f;
            camera.transform.Rotate(rotateY, rotateX, 0.0f);
            //カメラのロール方向を0に設定
            camera.transform.eulerAngles = new Vector3(camera.transform.localEulerAngles.x, camera.transform.localEulerAngles.y, 0.0f);
        }

        //拡大・縮小
        //PC
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        float view = camera.fieldOfView - (scroll * sensitiveWheel);

        //スマホ
        if (Input.touchCount == 2)
        {
            float BaseDistance = 0;
            float ChangedDistance = 0;

            if (Input.GetTouch(1).phase == TouchPhase.Began)
            {
                BaseDistance = Vector2.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);
            }

            if (Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(1).phase == TouchPhase.Moved)
            {
                ChangedDistance = Vector2.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);
            }
            
            float pinch = ChangedDistance - BaseDistance;
            view = camera.fieldOfView - (pinch * sensitivePinch);
        }
        camera.fieldOfView = Mathf.Clamp(value : view, min : 10f, max : 70f);
    }
}
