using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class CamContoroller : MonoBehaviour
{
    //視野角調整の変数宣言
    float fov_A = 80f; float fov_B = 130f;
    float aspect_A = 9f / 16f; float aspect_B = 21f / 9f;
    float y_intercept = 0f; float coef = 0f;
    float BaseAspect = 0f; float ChangedAspect = 0f;

    //変数宣言
    float BaseDistance = 0;
    float ChangedDistance = 0;
    float pinch = 0;
    float view_base = 60f;
    float view_max = 110f;
    float view_delta = 0f;
    float view_diff = 0f;

    // Start is called before the first frame update
    void Start()
    {
        //起動時の画面比率を取得
        BaseAspect = (float)Screen.height / (float)Screen.width;
        y_intercept = ((fov_A * aspect_A) - (fov_B * aspect_B)) / (aspect_A - aspect_B);
        coef = aspect_A * (fov_A - y_intercept);
        view_base = (coef / BaseAspect) + y_intercept;
        view_max = view_base + 20f;
    }

    // Update is called once per frame
    void Update()
    {
        Camera camera = Camera.main;

        float sensitiveWheel = 15.0f;
        float sensitivePinch = 0.2f;

        //カメラ倍率によって感度を低くする
        float sensitiveRotate = 2.0f * (camera.fieldOfView / 80f);

        //回転移動
        //PC
        if (Input.touchCount == 0 && Input.GetMouseButton(1))
        {
            float rotateX = Input.GetAxis("Mouse X") * sensitiveRotate;
            float rotateY = Input.GetAxis("Mouse Y") * sensitiveRotate;
            camera.transform.Rotate(rotateY, rotateX, 0.0f);
            //カメラのロール方向を0に設定
            camera.transform.eulerAngles = new Vector3(camera.transform.localEulerAngles.x, camera.transform.localEulerAngles.y, 0.0f);
        }

        //スマホ
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            float rotateX = Input.touches[0].deltaPosition.x / Input.touches[0].deltaTime * Time.deltaTime * sensitiveRotate * 0.05f;
            float rotateY = -1 * Input.touches[0].deltaPosition.y / Input.touches[0].deltaTime * Time.deltaTime * sensitiveRotate * 0.05f;
            camera.transform.Rotate(rotateY, rotateX, 0.0f);
            //カメラのロール方向を0に設定
            camera.transform.eulerAngles = new Vector3(camera.transform.localEulerAngles.x, camera.transform.localEulerAngles.y, 0.0f);
        }

        //拡大・縮小
        //画面比率が変更されたとき、視野角調整
        ChangedAspect = (float)Screen.height / (float)Screen.width;
        if (BaseAspect != ChangedAspect)
        {
            view_diff = camera.fieldOfView - view_base;
            y_intercept = ((fov_A * aspect_A) - (fov_B * aspect_B)) / (aspect_A - aspect_B);
            coef = aspect_A * (fov_A - y_intercept);
            view_base = (coef / ChangedAspect) + y_intercept;
            view_max = view_base + 20f;
            BaseAspect = ChangedAspect;
        }
        
        //PC
        if (Input.touchCount == 0)
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            view_delta += -1f * scroll * sensitiveWheel;
            if (view_base + view_delta >= view_max || view_base + view_delta <= 10f)
            {
                view_delta -= -1f * scroll * sensitiveWheel;
            }
        }
        
        //スマホ
        if (Input.touchCount == 2)
        {
            if (Input.GetTouch(1).phase == TouchPhase.Began)
            {
                BaseDistance = Vector2.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);
            }

            if (Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(1).phase == TouchPhase.Moved)
            {
                ChangedDistance = Vector2.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);
                //差分を撮ってBaseDistanceを更新
                pinch = ChangedDistance - BaseDistance;
                BaseDistance = ChangedDistance;
            }
            view_delta += -1f * pinch * sensitivePinch;
        }

        //視野角設定
        camera.fieldOfView = Mathf.Clamp(value : view_base + view_delta, min : 10f, max : view_max);

        //debugゾーン
        Debug.Log("y_intercept = " + y_intercept);
        Debug.Log("coef = " + coef);
        Debug.Log("view_base = " + view_base);
        Debug.Log("view_delta = " + view_delta);
        Debug.Log("aspect = " + ((float)Screen.height / (float)Screen.width));
    }
}