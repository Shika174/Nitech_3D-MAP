using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class URLopener : MonoBehaviour
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
        rectTransform.sizeDelta = new Vector2(screenWidth / 3f, (115f / 470f) * screenWidth / 3f);
        // 位置を中央下部に調整
        RectTransform rect = GetComponent<RectTransform>();
        rect.anchorMin = new Vector2(0.5f, 0);
        rect.anchorMax = new Vector2(0.5f, 0);
        rect.pivot = new Vector2(0.5f, 0);
        rect.anchoredPosition = new Vector2(0, 50);
    }

    
}
