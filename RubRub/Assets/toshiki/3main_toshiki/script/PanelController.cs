using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MouseController;

public class PanelController : MonoBehaviour
{
    private enum PANEL_STATUS { _PANEL_STAY_, _GAME_SCALEUP_, _GAME_SCALEDOWN_ };//ゲームの状態
    private PANEL_STATUS PanelStatus;
    //画像
    public Sprite rubrub1;
    public Sprite rubrub2;
    private Image image;
    //現在の処理進行度
    public GameObject Panel;        //panelとの関係付け
    public bool RubRubFlg;          //ボタンが押されたときのフラグ
    float PanelSizeX, PanelSizeY;   //変更するサイズのX,Y
    float Color_Alpha;              //Color(R,G,B,A)-
    float red, green, blue;         //---------------
    //定数---------------------------
    const float VectolSize = 0.05f;     //サイズの拡大、縮小率
    const float PanelFulSizeX = 1.0f;   //サイズのX最大値
    const float PanelFulSizeY = 1.0f;   //サイズのY最大値
    const float Color_Alpha_Max = 225.0f;    //Alphaの最大値
    const float Color_Variable = Color_Alpha_Max / (PanelFulSizeX / VectolSize);//Alphaの可変率
    public static float MouseMoveTotal;
    void Start()
    {
        image = Panel.GetComponent<Image>();
        image.sprite = rubrub1;
        MouseMoveTotal = 0.0f;
        PanelSizeX = 0.0f;
        PanelSizeY = 0.0f;
        //Color(R,G,B.A)の初期化----------------
        Color_Alpha = 0.0f;
        red = GetComponent<Image>().color.r;
        green = GetComponent<Image>().color.g;
        blue = GetComponent<Image>().color.b;
        //--------------------------------------
        GetComponent<Image>().color = new Color(red, green, blue, Color_Alpha / 255.0f);
        GetComponent<RectTransform>().localScale = new Vector3(PanelSizeX, PanelSizeY, 1);
        PanelStatus = 0;
    }

    // Update is called once per frame
    void Update()
    {
        switch (PanelStatus)
        {
            case PANEL_STATUS._PANEL_STAY_:
                //サイズ拡大トリガー
                if (RubRubFlg)
                {
                    PanelStatus = PANEL_STATUS._GAME_SCALEUP_;

                    MainManager.ChangeStatus(MainManager.STATUS._GAME_RUB_);//ゲームモードを撫でるパネルに変える
                }
                break;

            case PANEL_STATUS._GAME_SCALEUP_:
                //サイズ拡大-----------------------------
                if (PanelSizeX < PanelFulSizeX)
                {
                    PanelSizeX += VectolSize;
                }
                else
                {
                    PanelSizeX = PanelFulSizeX;
                }
                if (PanelSizeY < PanelFulSizeX)
                {
                    PanelSizeY += VectolSize;
                }
                else
                {
                    PanelSizeY = PanelFulSizeX;
                }
                //透明度変更-----------------------------
                if (Color_Alpha < Color_Alpha_Max)
                {
                    Color_Alpha += Color_Variable;
                }
                else
                {
                    Color_Alpha = (float)Color_Alpha_Max;
                }
                //---------------------------------------
                GetComponent<RectTransform>().localScale = new Vector3(PanelSizeX, PanelSizeY, 1);
                GetComponent<Image>().color = new Color(red, green, blue, Color_Alpha / 255.0f);
                //サイズ縮小トリガ
                if (MouseMoveTotal >= 10.0f)
                {
                    image.sprite = rubrub2;
                }
                if (MouseMoveTotal >= 30.0f)
                {
                    RubRubFlg = false;
                    PanelStatus = PANEL_STATUS._GAME_SCALEDOWN_;

                    MainManager.ChangeStatus(MainManager.STATUS._GAME_PLAY_);//ゲームモードをプレイに変える
                }
                if (Input.GetMouseButtonUp(0))
                {
                    RubRubFlg = false;
                    PanelStatus = PANEL_STATUS._GAME_SCALEDOWN_;

                    MainManager.ChangeStatus(MainManager.STATUS._GAME_PLAY_);//ゲームモードをプレイに変える
                }
                break;

            case PANEL_STATUS._GAME_SCALEDOWN_:
                //サイズ縮小------------------------------
                if (PanelSizeX > 0.0f)
                {
                    PanelSizeX -= VectolSize;
                }
                if (PanelSizeY > 0.0f)
                {
                    PanelSizeY -= VectolSize;
                }
                //透明度変更-----------------------------
                if (Color_Alpha > 0.0f)
                {
                    Color_Alpha -= Color_Variable;
                }
                //---------------------------------------
                GetComponent<RectTransform>().localScale = new Vector3(PanelSizeX, PanelSizeY, 1);
                GetComponent<Image>().color = new Color(red, green, blue, Color_Alpha / 255.0f);
                //処理の終了
                if (PanelSizeY < 0.0f && PanelSizeY < 0.0f)
                {
                    Start();
                }
                break;
        }
    }

    public void PBottonDown()   //ボタンを押されたときに呼び出す関数
    {
        RubRubFlg = true;
    }
}
