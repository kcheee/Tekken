using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Test : MonoBehaviour
{
    public Animator ani;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Hand_L"))
        {
            if (collision.transform.position.y > 1.3f)
                ani.SetTrigger("hit_head_L");
            else if (collision.transform.position.y > 0.61f && collision.transform.position.y < 1.3f)
                ani.SetTrigger("hit_mid_L");
            else if (collision.transform.position.y < 0.6f)
                ani.SetTrigger("hit_low_L");
        }
        if (collision.collider.CompareTag("Hand_R"))
        {
            if (collision.transform.position.y > 1.3f)
                ani.SetTrigger("hit_head_R");
            else if (collision.transform.position.y > 0.61f && collision.transform.position.y < 1.3f)
                ani.SetTrigger("hit_mid_R");
            else if (collision.transform.position.y < 0.6f)
                ani.SetTrigger("hit_low_R");
        }
        if (collision.collider.CompareTag("Kick_L"))
        {
            if (collision.transform.position.y > 1.3f)
                ani.SetTrigger("hit_head_L");
            else if (collision.transform.position.y > 0.61f && collision.transform.position.y < 1.3f)
                ani.SetTrigger("hit_mid_L");
            else if (collision.transform.position.y < 0.6f)
                ani.SetTrigger("hit_low_L");
        }
        if (collision.collider.CompareTag("Kick_R"))
        {
            if (collision.transform.position.y > 1.3f)
                ani.SetTrigger("hit_head_R");
            else if (collision.transform.position.y > 0.61f && collision.transform.position.y < 1.3f)
                ani.SetTrigger("hit_mid_R");
            else if (collision.transform.position.y < 0.6f)
                ani.SetTrigger("hit_low_R");
        }

 
    }
}
