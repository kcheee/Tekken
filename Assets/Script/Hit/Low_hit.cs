using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Low_hit : MonoBehaviour
{
    public Animator ani;
    void Start()
    {

    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject);
        // Hit_possible -> 플레이어 상태가 bwd 일때
        //if (ani.GetBool("Hit_possible"))

        if (collision.collider.CompareTag("Hand_L"))
        {
            ani.SetTrigger("hit_low_L");
        }
        if (collision.collider.CompareTag("Hand_R"))
        {
            ani.SetTrigger("hit_low_R");
        }
        if (collision.collider.CompareTag("Kick_L"))
        {
            ani.SetTrigger("hit_low_L");
        }
        if (collision.collider.CompareTag("Kick_R"))
        {
            ani.SetTrigger("hit_low_R");
        }

    }
}
