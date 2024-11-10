using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class PositionTracer : MonoBehaviour
{
    //名工大の緯度経度
    float nit_lat_min = 35.1551456f;  // 自治会館跡地南西端
    float nit_lat_max = 35.1595103f;  // 55号館北西の敷地の端
    float nit_lon_min = 136.9231147f; // 正門入口
    float nit_lon_max = 136.9271865f; // 東門出口

    // マップデータの座標情報
    float map_z_min = -37.1f;   // 正門入口
    float map_z_max = 518.94f;  // 東門出口
    float map_x_min = -448.02f; // 55号館北西の敷地の端
    float map_x_max = 275.9f;   // 自治会館跡地南西端

    // オブジェクトの位置情報
    float pos_x;
    float pos_z;

    [DllImport("__Internal")]
    private static extern void GetCurrentPosition();

    // Start is called before the first frame update
    void Start()
    {
        // JavaScriptのGeolocation APIを呼び出す
        GetCurrentPosition();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // JavaScriptから位置情報を受け取るメソッド
    public void ReceiveLocation(string location)
    {
        string[] coords = location.Split(',');
        float latitude = float.Parse(coords[0]); // 緯度
        float longitude = float.Parse(coords[1]); //経度

        //座標変換を行い、オブジェクトを移動
        pos_x = map_x_max - ( (map_x_max - map_x_min) / (nit_lat_max - nit_lat_min) ) * (latitude - nit_lat_min);
        pos_z = map_z_min + ( (map_z_max - map_z_min) / (nit_lon_max - nit_lon_min) ) * (longitude - nit_lon_min);
        transform.position = new Vector3(pos_x, transform.position.y, pos_z);

        // 位置情報を使ってオブジェクトを動かす
        // 校外の場合初期位置に固定
        /*
        if (latitude < nit_lat_min || latitude > nit_lat_max || longitude < nit_lon_min || longitude > nit_lon_max)
        {
            Debug.Log("Out of range");
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            return;
        }
        else
        {
            Debug.Log("In range");
            //座標変換を行い、オブジェクトを移動
            pos_x = map_x_max - ( (map_x_max - map_x_min) / (nit_lat_max - nit_lat_min) ) * (latitude - nit_lat_min);
            pos_z = map_z_min + ( (map_z_max - map_z_min) / (nit_lon_max - nit_lon_min) ) * (longitude - nit_lon_min);
            transform.position = new Vector3(pos_x, transform.position.y, pos_z);
        }
        */
    }
}