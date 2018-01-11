using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMobe3 : MonoBehaviour {

    //------------------------------
    [SerializeField]
    private float movement = 150f;
    [SerializeField]
    private float rotateSpeed = 5f;
    float moveX = 0f, moveZ = 0f;
    Rigidbody rb;
    //------------------------------

    //------------------------------
    Animator WalkAnimator;
    //------------------------------

    // Use this for initialization
    void Start()
    {

        //角度
        rb = this.GetComponent<Rigidbody>();

        //
        this.WalkAnimator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {

        //プレイヤー移動、角度調整
        if (Input.GetAxis("Horizontal") < 0)//左入力
        {
            MainManager2.LastKey = MainManager2.LAST_KEY._KEY_LEFT_;//最後のキー（左）入力を渡す

            moveX = Input.GetAxis("Horizontal") * Time.deltaTime * movement;
            WalkAnime(true);
        }
        else if (Input.GetAxis("Horizontal") > 0)//右入力
        {
            MainManager2.LastKey = MainManager2.LAST_KEY._KEY_RIGHT_;//最後のキー（右）入力を渡す

            moveX = Input.GetAxis("Horizontal") * Time.deltaTime * movement;
            WalkAnime(true);
        }
        else if (Input.GetAxis("Vertical") > 0)//上入力
        {
            MainManager2.LastKey = MainManager2.LAST_KEY._KEY_UP_;//最後のキー（上）入力を渡す

            moveZ = Input.GetAxis("Vertical") * Time.deltaTime * movement;
            WalkAnime(true);
        }
        else if (Input.GetAxis("Vertical") < 0)//下入力
        {
            MainManager2.LastKey = MainManager2.LAST_KEY._KEY_UP_;//最後のキー（上）入力を渡す

            moveZ = Input.GetAxis("Vertical") * Time.deltaTime * movement;
            WalkAnime(true);
        }
        else
        {
            WalkAnime(false);
        }



        Vector3 direction = new Vector3(moveX, 0, moveZ);
        if (direction.magnitude > 0.01f)
        {
            float step = rotateSpeed * Time.deltaTime;
            Quaternion myQ = Quaternion.LookRotation(direction);
            this.transform.rotation = Quaternion.Lerp(transform.rotation, myQ, step);
        }

        //アニメーション管理

    }

    void WalkAnime(bool bChange)
    {
        WalkAnimator.SetBool("Walk", bChange);
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector3(moveX, 0, moveZ);
    }

}
