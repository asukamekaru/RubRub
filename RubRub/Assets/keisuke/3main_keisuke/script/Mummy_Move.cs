using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mummy_Move : MonoBehaviour
{

    public Transform target;        // ターゲットの位置情報

    Rigidbody rb;
    Animator anim;
    NavMeshAgent nav;

    public float visibleDistance;   // 可視距離
    float targetDistance;           // ターゲットとの距離

    public float sightAngle;        // 視野角

    Transform lineOfSight1;         // 目の位置
    Ray gazeRay1;                   // 目とターゲットを結ぶRay

    public LayerMask visibleLayer;  // 見る対象のLayer（ターゲットと障害物が含まれる）

    public float walkSpeed;         // さまよっているときの速度
    public float runSpeed;          // おいかけているときの速度

    public float targetLostLimitTime;   // ターゲットを見失うまでの時間
    public float targetFindDistance;    // ターゲットを見つける距離（至近距離に近寄ると視野に関係なく見つける）
    float _lostTime = 0f;

    public float idleMaxTime;       // たちどまっている時間
    float _idleTime = 0f;

    enum eState                     // 状態
    {
        Idle,       // 立ち止まっている

        Chase,      // 追っている


    }
    eState _state = eState.Idle;

    // --- 初期化 ----------------------------------------------------------
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();

        lineOfSight1 = GameObject.Find("LineOfSight1").transform;
    }

    // --- 更新処理 ----------------------------------------------------------
    private void FixedUpdate()
    {
        switch (_state)
        {
            case eState.Idle:
                Idle();
                break;

            case eState.Chase:
                Chase();
                break;
        }

    }

    // --- 立ち止まっているときの処理 ----------------------------------------------------------
    void Idle()
    {
        Search(_state);

        _idleTime += Time.deltaTime;
        if (_idleTime > idleMaxTime)                        // 一定時間立ち止まったら、さまよう
        {
            _idleTime = 0;
        }
    }

    // --- ターゲットを探す処理 ----------------------------------------------------------
    void Search(eState state)
    {
        float _angle = Vector3.Angle(target.position - transform.position, lineOfSight1.forward);

        if (_angle <= sightAngle)
        {
            Debug.Log("Target In SightAngle");

            gazeRay1.origin = lineOfSight1.position;
            gazeRay1.direction = target.position - lineOfSight1.position;
            RaycastHit hit;

            if (Physics.Raycast(gazeRay1, out hit, visibleDistance, visibleLayer))
            {
                Debug.DrawRay(gazeRay1.origin, gazeRay1.direction * hit.distance, Color.red);

                if (hit.collider.gameObject.tag != "Obstacle")    // ターゲットとの間に障害物がない
                {
                    if (state == eState.Idle)
                    {
                        TargetFound();
                    }
                    else if (state == eState.Chase)
                    {
                        TargetInSight();
                    }
                    return;
                }
            }

            Debug.DrawRay(gazeRay1.origin, gazeRay1.direction * visibleDistance, Color.gray);
        }

        targetDistance = (transform.position - target.position).magnitude;

        if (targetDistance < targetFindDistance)            // 距離でターゲット発見
        {
            if (state == eState.Idle)
            {
                TargetFound();
            }
            else if (state == eState.Chase)
            {
                TargetInSight();
            }
            return;
        }
    }

    // --- ターゲットを発見したときの処理 ----------------------------------------------------------
    void TargetFound()
    {
        Debug.Log("Target Found");
        anim.SetTrigger("run");
        nav.Resume();
        nav.SetDestination(target.position);
        _state = eState.Chase;
        nav.speed = runSpeed;
        _idleTime = 0f;

    }

    // --- ターゲットを追っているときの処理 ----------------------------------------------------------
    void Chase()                                // 
    {
        nav.SetDestination(target.position);
        Search(_state);

        _lostTime += Time.deltaTime;
        // Debug.Log ("LostTime: " + _lostTime);

        if (_lostTime > targetLostLimitTime)                 // 一定時間視界の外なら、見失う
        {
            Debug.Log("Target Lost");                       // ターゲットロスト
            _state = eState.Idle;
            nav.Stop();
            anim.SetTrigger("idle");
            nav.speed = 0f;
            _lostTime = 0f;
        }
    }

    // --- ターゲットが視野に入っているときの処理 ----------------------------------------------------------
    void TargetInSight()
    {
        Debug.Log("Target In Sight");
        _lostTime = 0f;
    }

}
   