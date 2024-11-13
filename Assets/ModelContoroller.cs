using Unity.VisualScripting;
using UnityEngine;

public class ModelController : MonoBehaviour
{
    private float lastTapTime = 1.0f;
    private float lastClickTime = 1.0f;
    private float doubleTapDelay = 0.3f; // ダブルタップと認識する時間間隔
    private float doubleClickDelay = 0.3f; // ダブルクリックと判定する時間間隔
    private Vector3 targetPosition;
    private bool isMoving = false;
    private float moveSpeed = 5f; // 移動速度
    private float leap = 300f; // 移動量

    void Update()
    {
        Camera camera = Camera.main;
        float Move_sensitive = -10.0f * ((float)Screen.width / (float)Screen.height);

        // PC操作
        if (Input.GetMouseButton(0) && Input.touchCount == 0)
        {
            Vector3 cam_up = Vector3.Scale(camera.transform.up.normalized, new Vector3(1.0f, 0.0f, 1.0f));
            Vector3 cam_right = Vector3.Scale(camera.transform.right.normalized, new Vector3(1.0f, 0.0f, 1.0f));

            float Move_X = Input.GetAxis("Mouse X") * Move_sensitive;
            float Move_Y = Input.GetAxis("Mouse Y") * Move_sensitive;
            Vector3 Move = (cam_right * Move_X) + (cam_up * Move_Y);

            // 範囲内なら移動
            if (transform.position.x + Move.x > -300 && transform.position.x + Move.x < 800 && transform.position.z + Move.z > -600 && transform.position.z + Move.z < 400) 
            {
                transform.position += Move;
            }
            else
            {
                if (transform.position.x + Move.x < -300)
                {
                    transform.position = new Vector3(-300, transform.position.y, transform.position.z);
                }
                if (transform.position.x + Move.x > 800)
                {
                    transform.position = new Vector3(800, transform.position.y, transform.position.z);
                }
                if (transform.position.z + Move.z < -600)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y, -600);
                }
                if (transform.position.z + Move.z > 400)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y, 400);
                }
            }
            
        }

        //PC操作
        if (Input.GetMouseButtonDown(0) && Input.touchCount == 0)
        {
            if (Time.time - lastClickTime < doubleClickDelay)
            {
                Vector3 cam_up = Vector3.Scale(camera.transform.up.normalized, new Vector3(1.0f, 0.0f, 1.0f));
                Debug.Log(cam_up * camera.transform.position.y * (1/6));
                Debug.Log(camera.transform.position.y);
                // 範囲内なら前方に移動
                if (transform.position.z > -600 && transform.position.z < 400 && transform.position.x > -300 && transform.position.x < 800)
                {
                    targetPosition = transform.position + cam_up * leap;
                    isMoving = true;
                }
                else
                {
                    if (transform.position.z < -600)
                    {
                        transform.position = new Vector3(transform.position.x, transform.position.y, -600);
                    }
                    if (transform.position.z > 400)
                    {
                        transform.position = new Vector3(transform.position.x, transform.position.y, 400);
                    }
                    if (transform.position.x < -300)
                    {
                        transform.position = new Vector3(-300, transform.position.y, transform.position.z);
                    }
                    if (transform.position.x > 800)
                    {
                        transform.position = new Vector3(800, transform.position.y, transform.position.z);
                    }
                targetPosition = transform.position + cam_up * leap;
                isMoving = true;
                }
            }
            lastClickTime = Time.time;
        }
            
        // スマホ操作
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                if (Time.time - lastTapTime < doubleTapDelay)
                {
                    Vector3 cam_up = Vector3.Scale(camera.transform.up.normalized, new Vector3(1.0f, 0.0f, 1.0f));
                    
                    // 範囲内なら前方に移動
                if (transform.position.z > -600 && transform.position.z < 400 && transform.position.x > -300 && transform.position.x < 800)
                {
                    targetPosition = transform.position + cam_up * leap;
                    isMoving = true;
                }
                else
                {
                    if (transform.position.z < -600)
                    {
                        transform.position = new Vector3(transform.position.x, transform.position.y, -600);
                    }
                    if (transform.position.z > 400)
                    {
                        transform.position = new Vector3(transform.position.x, transform.position.y, 400);
                    }
                    if (transform.position.x < -300)
                    {
                        transform.position = new Vector3(-300, transform.position.y, transform.position.z);
                    }
                    if (transform.position.x > 800)
                    {
                        transform.position = new Vector3(800, transform.position.y, transform.position.z);
                    }
                    targetPosition = transform.position + cam_up * leap;
                    isMoving = true;
                    }
                }
                lastTapTime = Time.time;
            }
        }

        // 線形補間移動
        if (isMoving)
        {
            float step = moveSpeed * Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, targetPosition, step);

            // 目標位置に到達したかどうかを確認
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                transform.position = targetPosition;
                isMoving = false;
                Debug.Log("目標位置に到達しました: " + targetPosition);
            }
        }
    }
}