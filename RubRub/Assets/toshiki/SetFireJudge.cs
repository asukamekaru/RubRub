using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetFireJudge : MonoBehaviour
{
    bool OnePlayflg;
    public void OnCollisionStay(Collision collision)
    {
        Debug.Log("入った");
        CubeControl2 cubeControl2;
        GameObject DeleteObject = null;
        if (!OnePlayflg) {
            if (collision.gameObject.tag == "sakura" || collision.gameObject.tag == "Enemy")
            {
                foreach (GameObject obs in GameObject.FindGameObjectsWithTag("FireWall"))
                {
                    cubeControl2 = obs.GetComponent<CubeControl2>();
                    if (cubeControl2.NearTriggerObject != -1)
                    {
                        if (cubeControl2.SetFire != null)
                        {
                            DeleteObject = cubeControl2.SetFire;
                            cubeControl2.NearTriggerObject = -1;
                        }
                    }
                }

                if (DeleteObject != null)
                {
                    DeleteObject.GetComponent<SetFireDelete>().DeleteFlg = true;
                    //SetFireDelete.DeleteFlg = true;
                    //Destroy(DeleteObject);
                }
            }
        }
    }
}

