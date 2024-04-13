using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelContoroller : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //カメラの倍率によって感度を変更
        Camera camera = Camera.main;
        float Move_sensitive = 10.0f * (camera.fieldOfView / 80f);

        //pc操作
        if (Input.GetMouseButton(0) && Input.touchCount == 0)
        {
            //カメラ上方向と右方向のベクトルを正規化して取得
            //画面左下が原点
            Vector3 cam_up = Vector3.Scale(camera.transform.up.normalized, new Vector3(1.0f, 0.0f, 1.0f));
            Vector3 cam_right = Vector3.Scale(camera.transform.right.normalized, new Vector3(1.0f, 0.0f, 1.0f));

            //移動量を設定
            float Move_X = Input.GetAxis("Mouse X") * Move_sensitive;
            float Move_Y = Input.GetAxis("Mouse Y") * Move_sensitive;
            Vector3 Move = (cam_right * Move_X) + (cam_up * Move_Y);

            //移動
            transform.position += Move;
        }

        //スマホ操作
        if (Input.touchCount >= 2)
        {
            //カメラ上方向と右方向のベクトルを正規化して取得
            //画面左下が原点
            Vector3 cam_up = -1 * Vector3.Scale(camera.transform.up.normalized, new Vector3(1.0f, 0.0f, 1.0f));
            Vector3 cam_right = -1 * Vector3.Scale(camera.transform.right.normalized, new Vector3(1.0f, 0.0f, 1.0f));

            //移動量を設定
            if (Input.touches[1].phase == TouchPhase.Moved)
            {
                float Move_X = (Input.touches[0].deltaPosition.x + Input.touches[1].deltaPosition.x) / Input.touches[0].deltaTime * Time.deltaTime * Move_sensitive * 0.05f;
                float Move_Y = (Input.touches[0].deltaPosition.y + Input.touches[1].deltaPosition.y) / Input.touches[0].deltaTime * Time.deltaTime * Move_sensitive * 0.05f;
                Vector3 Move = (cam_right * Move_X) + (cam_up * Move_Y);
                //移動
                transform.position += Move;
            }
        }
    }
}
