using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getmosue : MonoBehaviour
{

    int status;

    float mouse_x_delta;
    float mouse_y_delta;

    float x;
    float y;

    // Use this for initialization
    void Start()
    {
        status = 0;
        x = y = 0;

    }

    // Update is called once per frame
    void Update()
    {
        mouse_x_delta = Input.GetAxis("Mouse X");
        mouse_y_delta = Input.GetAxis("Mouse Y");

        switch (status)
        {
            case 0://何もない
                if (Input.GetMouseButtonDown(0))
                {
                    status = 1;//撫に移動
                    Debug.Log("Click");
                }
                break;

            case 1://撫

                float xx = (mouse_x_delta < 0) ? -mouse_x_delta : mouse_x_delta;
                float yy = (mouse_y_delta < 0) ? -mouse_y_delta : mouse_y_delta;

                x += xx;
                y += yy;

                Debug.Log("x" + x + "y" + y);
                if (Input.GetMouseButtonUp(0)) status = 2;
                break;

            case 2://判定

                if (x / y >= 0.3 && x / y <= 2.5)//縦と横を割った数が0.3以上2.5以下なら　丸判定
                {
                    Debug.Log("丸判定");

                }
                else if (x > y)
                {
                    Debug.Log("横判定");
                }
                else
                {
                    Debug.Log("縦判定");
                }

                if (Input.GetKey(KeyCode.Space))
                {

                    status = 0;
                    Start();
                }
                break;
        }
    }
}
