using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Player_Wall_Ray : MonoBehaviour {

    public float RayLength = 1.0f;  //Rayの長さ
   
    public GameObject wall;
    public GameObject Point;

    void Update()
    {

        // Rayを飛ばす（第1引数がRayの発射座標、第2引数がRayの向き）
        Ray ray = new Ray(transform.position, transform.forward);
       
        // シーンビューにRayを可視化するデバッグ（必要がなければ消してOK）
        Debug.DrawRay(ray.origin, ray.direction * RayLength, Color.blue);

        // Rayが衝突したコライダーの情報を得る
    RaycastHit hit;
    // Rayが衝突したかどうか
    if (!Physics.Raycast(ray, out hit, RayLength))
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Instantiate( 生成するオブジェクト,  場所, 回転 );  回転はそのままなら↓
            Instantiate(wall,
                        new Vector3(Point.transform.position.x,
                                    this.transform.position.y - 2,
                                    Point.transform.position.z),
                        Quaternion.identity);
        }
    }
        
    }

}
