using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRotation : MonoBehaviour {
    //0.下、90.左、180.上、270.右、
    bool OnePleyFlg;
	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        if (!OnePleyFlg)
        {
            Rotation();
        }
    }
    void Rotation()
    {
        Debug.Log("くるくる");
        GameObject Player = GameObject.FindGameObjectWithTag("Player");
        float DifferenceX;
        float DifferenceZ;
        //X軸、Z軸のプレイヤーと壁の座標の差を取得
        //差のより大きい方を優先とし向きを設定
        DifferenceX = Player.transform.position.x - this.transform.position.x;
        DifferenceZ = Player.transform.position.z - this.transform.position.z;
        //負の数を正の数へ変更
        if (DifferenceX < 0)
        {
            DifferenceX *= -1;
        }
        if (DifferenceZ < 0)
        {
            DifferenceZ *= -1;
        }

        if (DifferenceX < DifferenceZ)
        {
            if (Player.transform.position.z > this.transform.position.z)
            {
                //180
                //this.transform.rotation = new Vector3(0.0f,0.0f,180.0f);
                this.transform.Rotate(new Vector3(0f, 180f, 0f));
            }
        }
        else if (DifferenceX > DifferenceZ)
        {
            if (Player.transform.position.x > this.transform.position.x)
            {
                //270
                //this.transform.rotation = new Vector3(0.0f, 0.0f, 270.0f);
                this.transform.Rotate(new Vector3(0f, 270f,0));
            }
            else
            {
                //90
                //this.transform.rotation = new Vector3(0.0f, 0.0f, 90.0f);
                this.transform.Rotate(new Vector3(0f, 90f, 0f));
            }
        }
        else
        {
            Debug.Log("差が同じにより初期値０となります。");
        }
        OnePleyFlg = true;
    }
}
