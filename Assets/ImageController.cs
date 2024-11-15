using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ImageController : MonoBehaviour
{
    public float marginRate = 0.8f; // 余白の割合を設定
    public Button closeButton; // バツボタンを参照するための変数
    public Button URLButton;
    private Image imageComponent; // Imageコンポーネントを参照するための変数
    public string url = "https://www.koudaisai.com/";//""の中には開きたいWebページのURLを入力します

    // Start is called before the first frame update
    void Start()
    {
        // Imageコンポーネントを取得
        imageComponent = GetComponent<Image>();

        // バツボタンのクリックイベントにメソッドを登録
        closeButton.onClick.AddListener(HideImage);

        // URLボタンのクリックイベントにメソッドを登録
        URLButton.onClick.AddListener(OpenURL);

        // バツボタンのRectTransformを取得
        RectTransform closeButtonRect = closeButton.GetComponent<RectTransform>();

        // アンカーを右上に設定
        closeButtonRect.anchorMin = new Vector2(1, 1);
        closeButtonRect.anchorMax = new Vector2(1, 1);

        // ピボットを右上に設定
        closeButtonRect.pivot = new Vector2(1, 1);

        // 位置を右上に調整
        closeButtonRect.anchoredPosition = new Vector2(10, 10); // 右上からのオフセットを設定

        // ボタンのサイズを設定
        //closeButtonRect.sizeDelta = new Vector2(50, 50); // 幅と高さを設定

        // 画像のRectTransformを取得
        RectTransform imageRect = imageComponent.GetComponent<RectTransform>();

        // アンカーとピボットを中央に設定
        imageRect.anchorMin = new Vector2(0.5f, 0.5f);
        imageRect.anchorMax = new Vector2(0.5f, 0.5f);
        imageRect.pivot = new Vector2(0.5f, 0.5f);

        // 位置を中央に調整
        imageRect.anchoredPosition = Vector2.zero; // 中央に配置
    }

    void Update()
    {
        //画面比率を取得
        float screenRate = (float)Screen.width / (float)Screen.height;

        //画像の縦横比を取得
        float imageRate = GetImageAspectRatio();
        Debug.Log("imageRate: " + imageRate);

        //画像サイズはA4サイズしかないと仮定して作成しました
        //画像の縦横比がA4縦の場合
        if (imageRate < 1 / 1)
        {
            //画面比率が210:297(A4縦)より横長の場合
            if (screenRate > 210f / 297f)
            {
               //画像の高さを画面の高さ+余白に合わせる
               GetComponent<RectTransform>().sizeDelta = new Vector2((210f / 297f) * marginRate * Screen.height, marginRate * Screen.height);
            }
            //画面比率が210:297(A4縦)より縦長の場合
            else
            {
                //画像の幅を画面の幅+余白に合わせる
                GetComponent<RectTransform>().sizeDelta = new Vector2(marginRate * Screen.width, (297f / 210f) * marginRate * Screen.width);
            }
        }
        //画像の縦横比がA4横の場合
        else
        {
            //画面比率が297:210(A4横)より横長の場合
            if (screenRate > 297f / 210f)
            {
                //画像の高さを画面の高さ+余白に合わせる
                GetComponent<RectTransform>().sizeDelta = new Vector2((297f / 210f) * marginRate * Screen.height, marginRate * Screen.height);
            }
            //画面比率が297:210(A4横)より縦長の場合
            else
            {
                //画像の幅を画面の幅+余白に合わせる
                GetComponent<RectTransform>().sizeDelta = new Vector2(marginRate * Screen.width, (210f / 297f) * marginRate * Screen.width);
            }
        }
    }

    // 画像を非表示にするメソッド
    void HideImage()
    {
        gameObject.SetActive(false);
    }


    // 画像の縦横比を取得するメソッド
    float GetImageAspectRatio()
    {
        if (imageComponent != null && imageComponent.sprite != null)
        {
            float width = imageComponent.sprite.rect.width;
            float height = imageComponent.sprite.rect.height;
            return width / height;
        }
        return 1.0f; // デフォルトの縦横比
    }

    void OpenURL()
    {
        if (!string.IsNullOrEmpty(url))
        {
            Application.OpenURL(url);
        }
        else
        {
            Debug.LogError("URLが設定されていません");
        }
    }
}