using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;
public class TextContoroller : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI DebugText;
    public GameObject DebugTextObject;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Camera camera = Camera.main;
        DebugText.text = "Width = " + Screen.width + "\nfov = " + camera.fieldOfView + "\naltitude = " + camera.transform.position.y;
        DebugTextObject.transform.localPosition = new Vector3((Screen.width / -2)  + 100, (Screen.height / 2) - 25, 0);
    }
}
