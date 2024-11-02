using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class CamContoroller : MonoBehaviour
{
    //視野角調整の変数宣言
    float fov_A = 60f; float fov_B = 100f;  //視野角の最小値と最大値。16:9と9:21の比率で設定
    float aspect_A = 9f / 16f; float aspect_B = 21f / 9f;   //画面比率の設定。16:9と9:21の比率で設定
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
    float groundAltitude = 90f;

    // Start is called before the first frame update
    void Start()
    {
        //起動時の視野角を設定
        //過去の俺がどういう処理を組んだのかわからない
        //画面比率と初期視野角を比例した関係と仮定して計算していると思われる
        Camera camera = Camera.main;
        BaseAspect = (float)Screen.height / (float)Screen.width;    //画面比率の取得
        y_intercept = ((fov_A * aspect_A) - (fov_B * aspect_B)) / (aspect_A - aspect_B);    //y切片の計算
        coef = aspect_A * (fov_A - y_intercept);    //係数の計算
        view_base = (coef / BaseAspect) + y_intercept;  //視野角の計算
        view_max = view_base + 20f; //視野角の最大値の設定
        //視野角の変更
        camera.fieldOfView = Mathf.Clamp(value : view_base, min : 10f, max : view_max);
    }

    // Update is called once per frame
    void Update()
    {
        Camera camera = Camera.main;

        float sensitiveWheel = 30.0f;
        float sensitivePinch = 0.4f;
        float sensitiveRotate = 2.0f;

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
            //視野角の変更
            camera.fieldOfView = Mathf.Clamp(value : view_base, min : 10f, max : view_max);
        }
        
        //拡大・縮小
        //スクロールもしくはピンチでカメラの前後移動を行う
        //PC
        if (Input.touchCount == 0)
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            //カメラの前後移動
            //地面にめり込まないように移動を制限
            if (camera.transform.position.y + camera.transform.forward.y * scroll * sensitiveWheel > groundAltitude)
            {
                camera.transform.position += camera.transform.forward * scroll * sensitiveWheel;
            }
            else
            {
                camera.transform.position = new Vector3(camera.transform.position.x, groundAltitude, camera.transform.position.z);
            }
        }

        //スマホ
        if (Input.touchCount == 2)
        {
            //ピンチの処理
            if (Input.GetTouch(1).phase == TouchPhase.Began)    //タッチが始まったとき
            {
                BaseDistance = Vector2.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);
            }

            if (Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(1).phase == TouchPhase.Moved)   //タッチが動いたとき
            {
                ChangedDistance = Vector2.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);
                //差分をとってBaseDistanceを更新
                pinch = ChangedDistance - BaseDistance;
                BaseDistance = ChangedDistance;
            }
            //カメラの前後移動
            //地面にめり込まないように移動を制限
            if (camera.transform.position.y + camera.transform.forward.y * pinch * sensitivePinch > groundAltitude)
            {
                camera.transform.position += camera.transform.forward * pinch * sensitivePinch;
            }
            else
            {
                camera.transform.position = new Vector3(camera.transform.position.x, groundAltitude, camera.transform.position.z);
            }
        }

        /*古の拡大・縮小処理
        //その昔、拡大縮小は視野角を変更することで行われていた...
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
        */

        //debugゾーン
        Debug.Log("y_intercept = " + y_intercept);
        Debug.Log("coef = " + coef);
        Debug.Log("view_base = " + view_base);
        Debug.Log("view_delta = " + view_delta);
        Debug.Log("aspect = " + ((float)Screen.height / (float)Screen.width));
    }
}