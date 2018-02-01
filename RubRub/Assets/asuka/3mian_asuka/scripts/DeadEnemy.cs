using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadEnemy : MonoBehaviour
{

    private enum LAST_KEY { _NONE, _ANIMATION, _WAIT, _DESTROY };//ゲームの状態
    private LAST_KEY DeadAnime;
    private bool FirstParticle;

    Animator _animator;
    AnimatorStateInfo animInfo;

    soundManager soundmanager;

    [SerializeField]
    [Header("アニメーションが終わった後の時間")]
    private float fWaitingTime;

    // Use this for initialization
    void Start()
    {
        soundmanager = GameObject.Find("SoundManager").GetComponent<soundManager>();
        _animator = GetComponent<Animator>();
        animInfo = _animator.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
    }

    private void Update()
    {
        switch (DeadAnime)
        {
            case LAST_KEY._NONE://生きてる間
               
                break;

            case LAST_KEY._ANIMATION://炎などに当たり、アニメーションに入る
                _animator.SetBool("Dead", true);
                if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Dead")) DeadAnime = LAST_KEY._WAIT;//アニメーションが終われば待ち時間に映る
                break;

            case LAST_KEY._WAIT://待つ
                if(!FirstParticle)
                {
                    GameObject Prefab = (GameObject)Resources.Load("Prefab/Fire_Explosion_01");
                    GameObject MakeSmoke = (GameObject)Instantiate(Prefab, new Vector3(this.transform.position.x, this.transform.position.y + 1
                        , this.transform.position.z), Quaternion.Euler(-90.0f, 0.0f, 0.0f));
                    //MakeSmoke.transform.parent = transform;
                    FirstParticle = true;
                }
                if (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime > fWaitingTime)DeadAnime = LAST_KEY._DESTROY;//待ち時間が終われば自分を消す
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
            soundmanager.PlaySound(6, false);//死んだ音
            DeadAnime = LAST_KEY._ANIMATION;
        }
        if (collision.gameObject.tag == "AttackSakura")
        {
            soundmanager.PlaySound(7,false);//アタック桜に当たったときの音
            DeadAnime = LAST_KEY._ANIMATION;
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "AttackSakura")
        {
            soundmanager.PlaySound(7, false);//アタック桜に当たったときの音
            DeadAnime = LAST_KEY._ANIMATION;
        }
    }
}