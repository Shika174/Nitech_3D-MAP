using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.InteropServices;

using TMPro;
public class TextContoroller : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI DebugText;
    public GameObject DebugTextObject;

    [DllImport("__Internal")]
    private static extern void GetCurrentPosition();
    
    // フレームカウント
    int frameCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        GetCurrentPosition();
    }

    // Update is called once per frame
    void Update()
    {
        // 600フレームごとに現在地を取得する
        if (frameCount == 600)
        {
            // JavaScriptの呼び出し
            GetCurrentPosition(); 
            frameCount = 0;
        }
        frameCount++;
        
    }
    public void ReceiveLocation(string location)
    {
        string[] coords = location.Split(',');
        float latitude = float.Parse(coords[0]); // 緯度
        float longitude = float.Parse(coords[1]); //経度

        Text text = DebugTextObject.GetComponent<Text>();
        text.text = "緯度: " + latitude + "\n経度: " + longitude;
    }

}
