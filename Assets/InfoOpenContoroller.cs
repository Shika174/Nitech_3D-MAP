using UnityEngine;
using UnityEngine.UI;

public class InfoOpenContoroller : MonoBehaviour
{
    public Button infoButton; // ボタンの参照
    public GameObject infoImage; // 表示する画像の参照

    // Start is called before the first frame update
    void Start()
    {
        //ボタンの位置を画面左上に、すべてがおさまるように設定
        RectTransform infoButtonRect = infoButton.GetComponent<RectTransform>();
        infoButtonRect.anchorMin = new Vector2(0, 1);
        infoButtonRect.anchorMax = new Vector2(0, 1);
        infoButtonRect.pivot = new Vector2(0, 1);
        infoButtonRect.anchoredPosition = new Vector2(30, -10); // 左上からのオフセットを設定

        // ボタンのクリックイベントにメソッドを登録
        infoButton.onClick.AddListener(ShowImage);

        //初期状態では画像を表示する
        if (infoImage != null)
        {
            infoImage.SetActive(true); // 画像を表示
        }
        else
        {
            Debug.LogError("infoImageが設定されていません");
        }
    }

    // 画像を表示するメソッド
    void ShowImage()
    {
        if (infoImage != null)
        {
            infoImage.SetActive(true); // 画像を表示
        }
        else
        {
            Debug.LogError("infoImageが設定されていません");
        }
    }
}