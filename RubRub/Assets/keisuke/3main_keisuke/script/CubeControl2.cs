using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GroundC;


public class CubeControl2 : WallBase
{

    [SerializeField, Range(0, 10)]
    float time = 1; //移動時間

    [SerializeField]
    float posY;

    private Vector3 endPosition;

    //[SerializeField]
    //AnimationCurve curve;

    private float startTime;
    private Vector3 startPosition;

    private bool mode = true;   //trueなら壁を上げてfalseなら下げる

    Rigidbody rigidBody;
    public Vector3 force = new Vector3(0, 10, 0);
    public ForceMode forceMode = ForceMode.VelocityChange;

    //翁長君作成変数
    [HideInInspector]
    public bool MoveEnd;

   // public GameObject up;
   // public GameObject down;


    // Use this for initialization
    void Start()
    {
        rigidBody = gameObject.GetComponent<Rigidbody>();
        endPosition = new Vector3(this.transform.position.x, posY, this.transform.position.z);

    }
    //このスクリプトがtrueになったとき1度のみ行う処理
    void OnEnable()
    {

        if (mode)
        {
            posY = 1;
           // this.transform.parent = GameObject.Find("up").transform;   //upの子オブジェクトに登録
        }
        else
        {
            posY = -2.1f;
            //this.transform.parent = GameObject.Find("down").transform;   //downの子オブジェクトに登録
        }

        endPosition = new Vector3(this.transform.position.x, posY, this.transform.position.z);

        //timeが0より小さい場合
        //ポジションを一気にエンドポジションまで持っていき
        //このスクリプトをfalseにする。
        if (time <= 0)
        {
            transform.position = endPosition;
            //GroundC.GroundCount.GCFlg = true;
            mode = !mode;//modeのflagを反転
            //this.transform.parent = up.transform;   //upの子オブジェクトに登録
            enabled = false;    //このスクリプトをfalseに

            return;
        }

        startTime = Time.timeSinceLevelLoad;
        startPosition = transform.position;
    }

    void Update()
    {
        if (this.gameObject.transform.position.y < -2)
        {
            Destroy(this.gameObject);
        }
        var diff = Time.timeSinceLevelLoad - startTime;
        if (diff > time)
        {
            transform.position = endPosition;
            mode = !mode;
            //GroundC.GroundCount.GCFlg = true;
            //this.transform.parent = up.transform;   //upの子オブジェクトに登録
            enabled = false;    //このスクリプトをfalseに
            
        }

        var rate = diff / time;
        //var pos = curve.Evaluate(rate);

        transform.position = Vector3.Lerp(startPosition, endPosition, rate);
        //transform.position = Vector3.Lerp (startPosition, endPosition, pos);
        //翁長君が触った所-------------------------------------------------------------------
        if (MoveEnd)
        {
            if (WallType != -1)
            {
                WallTagChange(WallType, this.gameObject);
                if (WallType > 0)
                {
                    if (NearObjectRetrieval(this.gameObject, TagName[WallType - 1]))//TagNameの要素が２つしかないため　WallType - 1
                    {
                        NearTriggerObject = WallType;
                    }
                    ObjectCreate(this.gameObject, WallName[WallType - 1], NearTriggerObject);
                }

                WallType = -1;
            }
            enabled = false;
        }
    }

    //ギズモ(移動ポインタ)の描画
    void OnDrawGizmosSelected()
    {
#if UNITY_EDITOR

        if (!UnityEditor.EditorApplication.isPlaying || enabled == false)
        {
            startPosition = transform.position;
        }

        UnityEditor.Handles.Label(endPosition, endPosition.ToString());
        UnityEditor.Handles.Label(startPosition, startPosition.ToString());
#endif
        Gizmos.DrawSphere(endPosition, 0.1f);
        Gizmos.DrawSphere(startPosition, 0.1f);

        Gizmos.DrawLine(startPosition, endPosition);
    }

}
