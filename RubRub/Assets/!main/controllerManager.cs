//=================================================
// 操作を管理するスクリプト
// AsukaMekaru
// 2017/12/26
//=================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class controllerManager : MonoBehaviour
{
    public Button getControllerLeft;//取得する左ボタンなど
    public Button getControllerRight;//取得する右ボタンなど
    public Button getControllerUp;//取得する上ボタンなど
    public Button getControllerDown;//取得する下ボタンなど

    [SerializeField]
    [Header("ホームシーンならココにホームマネージャーを入れる")]
    private homeManager homemanager;
    [SerializeField]
    [Header("メインシーンならココにプレイヤーキャラを入れる")]
    private Yuko_Move yukomove;

    // Use this for initialization
    void Start()
    {
        getControllerLeft.onClick.AddListener(Left);
        getControllerRight.onClick.AddListener(Right);
        getControllerUp.onClick.AddListener(Up);
        getControllerDown.onClick.AddListener(Down);

        getControllerLeft.onClick.AddListener(Update);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Left()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            //ホームシーンなら
            case "HomeScene":
            case "home_test_asuka":
                homemanager.getControll("left");
                break;
            //ゲームメインシーンなら
            case "GameMainScene":
               // yukomove.getControll("left");
                break;
        }
    }

    private void Right()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            //ホームシーンなら
            case "HomeScene":
            case "home_test_asuka":
                homemanager.getControll("right");
                break;

            //ゲームメインシーンなら
            case "GameMainScene":
                //yukomove.getControll("right");
                break;
        }
    }

    private void Up()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            //ホームシーンなら
            case "HomeScene":
                //何もしない
                break;

            case "GameMainScene":
               // yukomove.getControll("up");
                break;
        }
    }

    private void Down()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            //ホームシーンなら
            case "HomeScene":
                //何もしない
                break;

            //ゲームメインシーンなら
            case "GameMainScene":
               // yukomove.getControll("down");
                break;
        }
    }
}
