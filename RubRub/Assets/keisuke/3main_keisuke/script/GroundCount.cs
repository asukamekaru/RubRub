using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GroundC
{
    public class GroundCount : MonoBehaviour
    {
        //static public bool GCFlg;
        static public int Ground = 0;
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            /*if (GCFlg) 
            {
                GC();
                GCFlg = !GCFlg;
            }*/

            Ground = this.transform.childCount;

           /* if (Ground <= 3)       //上がってる壁が3つ以下ならいいんやで。
            {
                Debug.Log("まだだ・・・まだ終わらんよ！！");
               
            }
            else
            {                           //3つ以上でてるのは許さない・・・。
                Debug.Log("お前は最後に殺してやると言ったな？あれは嘘だ。");
            }*/
        }

        //壁が出ている数を数える
        /*void GC()
        {
            Ground = this.transform.childCount;

            if (Ground <= 3)       //上がってる壁が3つ以下ならいいんやで。
            {
                Debug.Log("まだだ・・・まだ終わらんよ！！");
            }
            else
            {                           //3つ以上でてるのは許さない・・・。
                Debug.Log("お前は最後に殺してやると言ったな？あれは嘘だ。");
            }

        }*/
    }
}