using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{

    int status; //現在の撫でる動作の進行度

    float mouse_x_delta;    //マウス移動情報X
    float mouse_y_delta;    //マウス移動情報Y

    Vector2 mousePosition;
    float mouse_position_x;     //マウス座標
    float mouse_position_y;     //マウス座標
    float old_mouse_position_x; //古いマウス座標X
    float old_mouse_position_y; //古いマウス座標Y

    float MouseVector_Total_X;  //マウス移動量累積値.X
    float MouseVector_Total_Y;  //マウス移動量累積値.Y
    float MouseVector_Total;    //マウス移動量蓄積値X.Y

    public bool MouseX_UpFlg;      //マウスポジションXの累積値に加算処理があったか
    public bool MouseX_DownFlg;    //減算処理があったか
    public bool MouseY_UpFlg;      //マウスポジションYの累積値に加算処理があったか
    public bool MouseY_DownFlg;    //減算処理があったか

    bool RubRubFlg;

    int[] WallType = new int[] { 0, 0, 0 }; //0.通常壁　1.火吸収＆放出　2.水吸収＆放出

    void Start()
    {
        //初期化
        status = 0;
        RubRubFlg = false;
        MouseVector_Total = MouseVector_Total_X = MouseVector_Total_Y = 0.0f;
        MouseX_UpFlg = MouseX_DownFlg = MouseY_UpFlg = MouseY_DownFlg = false;
    }

    void Update()
    {
        Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        mouse_x_delta = Input.GetAxis("Mouse X");
        mouse_y_delta = Input.GetAxis("Mouse Y");

        switch (status)
        {
            case 0://ポジション計測へのトリガー
                if (RubRubFlg)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        status = 1;//撫に移動
                        mouse_position_x = mousePosition.x;
                        mouse_position_y = mousePosition.y;

                    }
                }
                break;

            case 1://マウスのポジションと移動量を計測
                //マウスベクトルの総量-----------------------------------------------------
                float MouseVector_X = (mouse_x_delta < 0) ? -mouse_x_delta : mouse_x_delta;
                float MouseVector_Y = (mouse_y_delta < 0) ? -mouse_y_delta : mouse_y_delta;
                MouseVector_Total_X += MouseVector_X;
                MouseVector_Total_Y += MouseVector_Y;
                MouseVector_Total = MouseVector_Total_X + MouseVector_Total_Y;

                //円の判定要素フラグ------------------------------------------------------
                old_mouse_position_x = mouse_position_x;
                old_mouse_position_y = mouse_position_y;
                mouse_position_x = mousePosition.x;
                mouse_position_y = mousePosition.y;

                if (mouse_position_x > old_mouse_position_x)
                {
                    MouseX_UpFlg = true;
                }
                else if (mouse_position_x < old_mouse_position_x)
                {
                    MouseX_DownFlg = true;
                }

                if (mouse_position_y > old_mouse_position_y)
                {
                    MouseY_UpFlg = true;
                }
                else if (mouse_position_y < old_mouse_position_y)
                {
                    MouseY_DownFlg = true;
                }
                //--------------------------------------------------------------------------
                if (Input.GetMouseButtonUp(0)) status = 2;//判定へ移動
                break;

            case 2://移動量を元に撫で方を判定

                //縦と横を割った数が0.3以上2.5以下なら　丸判定
                if (MouseVector_Total_X / MouseVector_Total_Y >= 0.3f &&
                    MouseVector_Total_X / MouseVector_Total_Y <= 2.5f &&
                    MouseX_UpFlg && MouseX_DownFlg && MouseY_UpFlg &&
                    MouseY_DownFlg && MouseVector_Total > 30.0f)
                {
                    Debug.Log("丸判定");
                    OMainManager.IFCreateCall();
                    WallType[0] = 1;
                }
                else if (MouseVector_Total_X > MouseVector_Total_Y && MouseVector_Total > 30.0f)
                {
                    Debug.Log("横判定");
                    OMainManager.IFCreateCall();
                    WallType[1] = 1;
                }
                else if (MouseVector_Total > 30.0f)
                {
                    Debug.Log("縦判定");
                    OMainManager.IFCreateCall();
                    WallType[2] = 1;
                }
                Start();
                break;
        }
    }

    public void MBottonDown()
    {
        RubRubFlg = true;
    }
}
