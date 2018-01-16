using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadEnemy : MonoBehaviour
{

    private enum LAST_KEY { _NONE, _ANIMATION, _WAIT, _DESTROY };//ゲームの状態
    private LAST_KEY DeadAnime;

    Animator _animator;
    AnimatorStateInfo animInfo;

    [SerializeField]
    [Header("アニメーションが終わった後の時間")]
    private float fWaitingTime;

    // Use this for initialization
    void Start()
    {
        _animator = GetComponent<Animator>();
        animInfo = _animator.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
    }

    private void Update()
    {
        Debug.Log(DeadAnime);
        switch (DeadAnime)
        {
            case LAST_KEY._NONE://生きてる間
               
                break;

            case LAST_KEY._ANIMATION://炎などに当たり、アニメーションに入る
                _animator.SetBool("Dead", true);
                if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Dead")) DeadAnime = LAST_KEY._WAIT;//アニメーションが終われば待ち時間に映る
                break;

            case LAST_KEY._WAIT://待つ
                if (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime > fWaitingTime) DeadAnime = LAST_KEY._DESTROY;//待ち時間が終われば自分を消す
                break;

            case LAST_KEY._DESTROY://消す
                Destroy(gameObject);
                break;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Fire")
        {
            
            Debug.Log("HIT");
            DeadAnime = LAST_KEY._ANIMATION;
        }
    }
}