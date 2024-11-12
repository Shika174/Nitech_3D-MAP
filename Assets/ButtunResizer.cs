using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtunResizer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //画面の横の大きさを取得
        float screenWidth = Screen.width;
        //横の大きさを基準にボタンの大きさを変更
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(screenWidth / 10, screenWidth / 10);
    }
}
