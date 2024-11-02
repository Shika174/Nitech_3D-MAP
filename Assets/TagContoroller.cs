using UnityEngine;
using UnityEngine.UI; // Imageコンポーネントを使用するために必要

public class TagContoroller : MonoBehaviour
{
    private Vector3 initTagPos;
    private Vector3 initModelPos;
    public Image messageImage; // UI Imageオブジェクトを参照するための変数

    void Start()
    {
        // タグの初期位置を取得
        initTagPos = transform.position;

        // モデルの初期位置を取得
        initModelPos = GameObject.Find("Model").transform.position;

        // 初期状態では画像を非表示にする
        messageImage.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // タグをモデルに追従させる
        // モデルの相対移動量を取得
        Vector3 modelMove = GameObject.Find("Model").transform.position - initModelPos;

        // タグの位置をモデルの移動量に合わせて移動
        transform.position = initTagPos + modelMove;

        // タグの向きをカメラに向ける
        transform.LookAt(Camera.main.transform);
        // 向きを反転
        transform.Rotate(0, 180, 0);
    }

    void OnMouseDown()
    {
        // オブジェクトがクリックされたときにUI Imageを表示
        messageImage.gameObject.SetActive(true);
    }
}