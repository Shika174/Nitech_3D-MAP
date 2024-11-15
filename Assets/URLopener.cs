using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class URLopener : MonoBehaviour
{
    public string url = "https://www.koudaisai.com/";//""の中には開きたいWebページのURLを入力します

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

    public void onClick()
    {
        Application.OpenURL(url);
    }
}
