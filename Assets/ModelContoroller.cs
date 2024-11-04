using UnityEngine;

public class ModelController : MonoBehaviour
{
    private float lastTapTime = 0f;
    private float doubleTapDelay = 0.3f; // ダブルタップと認識する時間間隔
    private Vector3 targetPosition;

    void Update()
    {
        Camera camera = Camera.main;
        float Move_sensitive = 10.0f * ((float)Screen.width / (float)Screen.height);

        // PC操作
        if (Input.GetMouseButton(0) && Input.touchCount == 0)
        {
            Vector3 cam_up = Vector3.Scale(camera.transform.up.normalized, new Vector3(1.0f, 0.0f, 1.0f));
            Vector3 cam_right = Vector3.Scale(camera.transform.right.normalized, new Vector3(1.0f, 0.0f, 1.0f));

            float Move_X = Input.GetAxis("Mouse X") * Move_sensitive;
            float Move_Y = Input.GetAxis("Mouse Y") * Move_sensitive;
            Vector3 Move = (cam_right * Move_X) + (cam_up * Move_Y);

            transform.position += Move;
        }

        // スマホ操作
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                if (Time.time - lastTapTime < doubleTapDelay)
                {
                    // ダブルタップを検出
                    Ray ray = camera.ScreenPointToRay(touch.position);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit))
                    {
                        targetPosition = hit.point;
                        transform.position = targetPosition;
                    }
                }
                lastTapTime = Time.time;
            }
        }
    }
}