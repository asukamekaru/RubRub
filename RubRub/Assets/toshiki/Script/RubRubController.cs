using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubRubController : MonoBehaviour {

    private float OldMousePositionX;
    private float OldMousePositionY;

    private float OldMousePositionX;
    private float OldMousePositionY;

    // Use this for initialization
    void Start () {
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y,
          Input.mousePosition.z);
        OldMousePositionX = mousePosition.x;
        OldMousePositionY = mousePosition.y;
    }
	
	// Update is called once per frame
	void Update () {
        //マウスポジション取得
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y,
            Input.mousePosition.z);


	}
}
