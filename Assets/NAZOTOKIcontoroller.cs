using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NAZOTOKIcontoroller : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 謎解きに使うオブジェクトのクリック判定
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                // クリックされたオブジェクトの名前を取得
                string objectName = hit.collider.gameObject.name;
                Debug.Log(objectName);
                // クリックされたオブジェクトが正解の場合
                if (objectName == "correct")
                {
                    Debug.Log("Correct!");
                    // 正解のオブジェクトを非表示にする
                    hit.collider.gameObject.SetActive(false);
                }
            }
        }
    }
}
