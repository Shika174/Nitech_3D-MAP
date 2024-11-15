using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class URLopener : MonoBehaviour
{
    

    // Start is called before the first frame update
    void Start()
    {
        // 位置を中央下部に調整
        RectTransform rect = GetComponent<RectTransform>();
        rect.anchorMin = new Vector2(0.5f, 0);
        rect.anchorMax = new Vector2(0.5f, 0);
        rect.pivot = new Vector2(0.5f, 0);
        rect.anchoredPosition = new Vector2(0, 50);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
