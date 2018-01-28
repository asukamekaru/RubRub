using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player_Wall_Ray : MonoBehaviour {
    public float RayLength = 1.0f;  //Rayの長さ
    public LayerMask visibleLayer;
    public GameObject wall;
    public GameObject Point;
    private CubeControl2 cube;

    public Ray ray;

    void Update()
    {

        // Rayを飛ばす（第1引数がRayの発射座標、第2引数がRayの向き）
        ray = new Ray(new Vector3(transform.position.x,0.55f,transform.position.z), transform.forward);

        // シーンビューにRayを可視化するデバッグ（必要がなければ消してOK）
        Debug.DrawRay(ray.origin, ray.direction * RayLength, Color.blue);

        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            // Rayが衝突したかどうか
            if (Physics.Raycast(ray, out hit, RayLength))
            {
                //衝突してそれがすでに生成された壁なら
                if (Physics.Raycast(ray, out hit, RayLength, visibleLayer))
                {
                    //Destroy(hit.collider.gameObject);
                   cube = hit.collider.GetComponent<CubeControl2>();
                   cube.enabled = true;

                }

            }
            else
            {
                //Instantiate( 生成するオブジェクト,  場所, 回転 );  回転はそのままなら↓
                if (MainManager.LastKey == MainManager.LAST_KEY._KEY_UP_ || MainManager.LastKey == MainManager.LAST_KEY._KEY_DOWN_)
                {
                    Instantiate(wall,
                                new Vector3(Mathf.RoundToInt(Point.transform.position.x),
                                            this.transform.position.y - 2,
                                            Point.transform.position.z),
                                Quaternion.identity);
                }

                if (MainManager.LastKey == MainManager.LAST_KEY._KEY_LEFT_ || MainManager.LastKey == MainManager.LAST_KEY._KEY_RIGHT_)
                {
                    Instantiate(wall,
                                new Vector3(Point.transform.position.x,
                                            this.transform.position.y -2,
                                            Mathf.RoundToInt(Point.transform.position.z)),
                                Quaternion.identity);
                }
            }
        }*/
    }
}
