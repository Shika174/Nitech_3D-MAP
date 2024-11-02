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
        //画面の縦の大きさを取得
        float screenHeight = Screen.height;
        //縦の大きさを基準にボタンの大きさを変更
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(screenHeight / 20, screenHeight / 20);
    }
}
