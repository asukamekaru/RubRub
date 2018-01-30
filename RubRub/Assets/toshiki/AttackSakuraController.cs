using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSakuraController : MonoBehaviour
{
    private enum SakuraAngle { Nonw, Right, Left, Up, Down };
    private SakuraAngle sakuraAngle;
    public float MoveSpeed = 0.01f;
    soundManager soundmanager;

    // Update is called once per frame
    void Update()
    {
        switch (sakuraAngle)
        {
            case SakuraAngle.Nonw:
                //サクラのローテーションYを見て進むべき方向を設定する
                if (this.gameObject.transform.rotation.y == 0)
                {
                    sakuraAngle = SakuraAngle.Left;
                }
                else if (this.gameObject.transform.localEulerAngles.y == 90)
                {
                    sakuraAngle = SakuraAngle.Up;
                }
                else if (this.gameObject.transform.localEulerAngles.y == 180)
                {
                    sakuraAngle = SakuraAngle.Right;
                }
                else if (this.gameObject.transform.localEulerAngles.y == 270)
                {
                    sakuraAngle = SakuraAngle.Down;
                }
                break;

            case SakuraAngle.Right:
                Debug.Log("右");
                this.gameObject.transform.position += new Vector3(MoveSpeed, 0.0f,0.0f);
                break;

            case SakuraAngle.Left:
                Debug.Log("左");
                this.gameObject.transform.position -= new Vector3(MoveSpeed, 0.0f, 0.0f);
                break;

            case SakuraAngle.Up:
                Debug.Log("上");
                this.gameObject.transform.position += new Vector3(0.0f, 0.0f, MoveSpeed);
                break;

            case SakuraAngle.Down:
                Debug.Log("下");
                this.gameObject.transform.position -= new Vector3(0.0f, 0.0f, MoveSpeed);
                break;
        }
    }
    public void OnCollisionEnter(Collision collision)
    {
        Debug.Log("入った");
        if (collision.gameObject.tag == "Obstacle")
        {
            soundmanager = GameObject.Find("SoundManager").GetComponent<soundManager>();
            soundmanager.PlaySound(5, false);//吸う音
            Destroy(this.gameObject);
        }
    }
}
