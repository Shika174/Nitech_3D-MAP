using UnityEngine;
using UnityEngine.UI;

public class NAZOTOKIcontoroller : MonoBehaviour
{
    public GameObject targetObjectA1; // 52号館(1)
    public GameObject targetObjectA2; // 52号館(2)
    public GameObject targetObjectA3; // 12号館
    public GameObject targetObjectA4; // 体育館
    public GameObject targetObjectB1; // 4号館
    public GameObject targetObjectB2; // 19号館
    public GameObject targetObjectB3; // 23号館(1)
    public GameObject targetObjectB4; // 23号館(2)

    private Collider targetColliderA1;
    private Collider targetColliderA2;
    private Collider targetColliderA3;
    private Collider targetColliderA4;
    private Collider targetColliderB1;
    private Collider targetColliderB2;
    private Collider targetColliderB3;
    private Collider targetColliderB4;

    public Image repeatMessageImage; // もう一回！
    public Image clearMessageImageA; // 次の謎の画像の表示(A)
    public Image clearMessageImageB; // 次の謎の画像の表示(B)

    // 順番のカウント用
    private int countA = 0;
    private int countB = 0;
    private int repeatCountA = 0;
    private int repeatCountB = 0;

    void Start()
    {
        // 初期状態では画像を非表示にする
        repeatMessageImage.gameObject.SetActive(false);
        clearMessageImageA.gameObject.SetActive(false);
        clearMessageImageB.gameObject.SetActive(false);

        // ターゲットオブジェクトのコライダーを取得
        if (targetObjectA1 != null)
        {
            targetColliderA1 = targetObjectA1.GetComponent<Collider>();
        }
        else
        {
            Debug.LogError("targetObjectA1が設定されていません");
        }

        if (targetObjectA2 != null)
        {
            targetColliderA2 = targetObjectA2.GetComponent<Collider>();
        }
        else
        {
            Debug.LogError("targetObjectA2が設定されていません");
        }

        if (targetObjectA3 != null)
        {
            targetColliderA3 = targetObjectA3.GetComponent<Collider>();
        }
        else
        {
            Debug.LogError("targetObjectA3が設定されていません");
        }

        if (targetObjectA4 != null)
        {
            targetColliderA4 = targetObjectA4.GetComponent<Collider>();
        }
        else
        {
            Debug.LogError("targetObjectA4が設定されていません");
        }

        if (targetObjectB1 != null)
        {
            targetColliderB1 = targetObjectB1.GetComponent<Collider>();
        }
        else
        {
            Debug.LogError("targetObjectB1が設定されていません");
        }

        if (targetObjectB2 != null)
        {
            targetColliderB2 = targetObjectB2.GetComponent<Collider>();
        }
        else
        {
            Debug.LogError("targetObjectB2が設定されていません");
        }

        if (targetObjectB3 != null)
        {
            targetColliderB3 = targetObjectB3.GetComponent<Collider>();
        }
        else
        {
            Debug.LogError("targetObjectB3が設定されていません");
        }

        if (targetObjectB4 != null)
        {
            targetColliderB4 = targetObjectB4.GetComponent<Collider>();
        }
        else
        {
            Debug.LogError("targetObjectB4が設定されていません");
        }
    }

    void Update()
    {
        // 3回連続でクリックされたらカウントアップ(1回目)
        if (countA == 3 && repeatCountA == 0)
        {
            repeatCountA++;
            countA = 0;
            repeatMessageImage.gameObject.SetActive(true); // もう一回！の画像を表示
        }
        if (countB == 3 && repeatCountB == 0)
        {
            repeatCountB++;
            countB = 0;
            repeatMessageImage.gameObject.SetActive(true); // もう一回！の画像を表示
        }

        // 3回連続でクリックされたらカウントアップ(2回目)
        if (countA == 3 && repeatCountA == 1)
        {
            repeatCountA = 0;
            countA = 0;
            clearMessageImageA.gameObject.SetActive(true); // 次の謎の画像の表示(A)
        }
        if (countB == 3 && repeatCountB == 1)
        {
            repeatCountB = 0;
            countB = 0;
            clearMessageImageB.gameObject.SetActive(true); // 次の謎の画像の表示(B)
        }


        // 特定の建物のクリック判定
            // PCのマウスクリックを検出
        if (Input.GetMouseButtonDown(0) && Input.touchCount == 0)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            ProcessRaycast(ray);
        }

        // スマホのタップを検出
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                ProcessRaycast(ray);
            }
        }
    }

    void ProcessRaycast(Ray ray)
    {
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            // クリックされた建物が特定の建物か判定
            // 順番の判定も行う
            if (hit.collider == targetColliderA1 || hit.collider == targetColliderA2)
            {
                countA = 1;
            }
            else if (hit.collider == targetColliderA3 && countA == 1)
            {
                countA++;
            }
            else if (hit.collider == targetColliderA4 && countA == 2)
            {
                countA++;
            }
            else
            {
                countA = 0;
                repeatCountA = 0;
            }

            if (hit.collider == targetColliderB1)
            {
                countB = 1;
            }
            else if (hit.collider == targetColliderB2 && countB == 1)
            {
                countB++;
            }
            else if (hit.collider == targetColliderB3 || hit.collider == targetColliderB4 && countB == 2)
            {
                countB++;
            }
            else
            {
                countB = 0;
                repeatCountB = 0;
            }

            // クリックされた建物の名前を表示
            if (hit.collider == targetColliderA1)
            {
                Debug.Log("52号館(1)");
            }
            else if (hit.collider == targetColliderA2)
            {
                Debug.Log("52号館(2)");
            }
            else if (hit.collider == targetColliderA3)
            {
                Debug.Log("12号館");
            }
            else if (hit.collider == targetColliderA4)
            {
                Debug.Log("体育館");
            }
            else if (hit.collider == targetColliderB1)
            {
                Debug.Log("4号館");
            }
            else if (hit.collider == targetColliderB2)
            {
                Debug.Log("19号館");
            }
            else if (hit.collider == targetColliderB3)
            {
                Debug.Log("23号館(1)");
            }
            else if (hit.collider == targetColliderB4)
            {
                Debug.Log("23号館(2)");
            }
        }
    }
}