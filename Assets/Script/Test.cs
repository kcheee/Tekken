using UnityEngine;

public class Test : MonoBehaviour
{
    public Animator ani;
    public GameObject hit_hand;
    public GameObject guard;
    public GameObject hit_kick;

    // hit_possibe이 켜져있으면 hit 아니면 guard
    void Hit_Guard(Collision collision, bool hand)
    {
        // 손
        if (hand)
        {
            // particle
            if (ani.GetBool("Hit_possible"))    // hit_possible이 아니고 guard로 바꿔야 함
                Instantiate(hit_hand, collision.transform.position, collision.transform.rotation);
            else
                Instantiate(guard, collision.transform.position, collision.transform.rotation);

            // collider가 maskman인지 Guard인지 체크
            if (gameObject.name.Contains("M_Wall_hit") && Maskman_ani_Setting.M_A_T != Maskman_ani_Setting.ani_state.backstep)
            {
                // 콤보 도중에 막을 수 있는 상황 방지하기 위해 코루틴 넣어야되나
                Maskman_ani_Setting.M_A_T = Maskman_ani_Setting.ani_state.Hit_hand;
                
            }
            else if (gameObject.name.Contains("G_Wall_hit") && Guard_ani_Setting.G_A_T != Guard_ani_Setting.ani_state.backstep)
            {
                Guard_ani_Setting.G_A_T = Guard_ani_Setting.ani_state.Hit_hand;
               
            }
        }
        // 발
        if (!hand)
        {
            // particle
            if (ani.GetBool("Hit_possible"))
                Instantiate(hit_kick, collision.transform.position, collision.transform.rotation);
            else
                Instantiate(guard, collision.transform.position, collision.transform.rotation);

            if (gameObject.name.Contains("M_Wall_hit") && Maskman_ani_Setting.M_A_T != Maskman_ani_Setting.ani_state.backstep)
                Maskman_ani_Setting.M_A_T = Maskman_ani_Setting.ani_state.Hit_kick;

            else if (gameObject.name.Contains("G_Wall_hit") && Guard_ani_Setting.G_A_T != Guard_ani_Setting.ani_state.backstep)
                Guard_ani_Setting.G_A_T = Guard_ani_Setting.ani_state.Hit_kick;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        // 왼손
        if (collision.collider.CompareTag("Hand_L"))
        {
            if (collision.transform.position.y > 1.2f)
            {
                ani.SetTrigger("hit_head_L");
                Hit_Guard(collision, true);
            }
            else if (collision.transform.position.y > 0.61f && collision.transform.position.y < 1.2f)
            {
                ani.SetTrigger("hit_mid_L");
                Hit_Guard(collision, true);
            }

            else if (collision.transform.position.y < 0.6f)
            {
                ani.SetTrigger("hit_low_L");
                Hit_Guard(collision, true);
            }
        }

        // 오른손
        if (collision.collider.CompareTag("Hand_R"))
        {
            if (collision.transform.position.y > 1.2f)
            {
                ani.SetTrigger("hit_head_R");
                Hit_Guard(collision, true);
            }
            else if (collision.transform.position.y > 0.61f && collision.transform.position.y < 1.2f)
            {
                ani.SetTrigger("hit_mid_R");
                Hit_Guard(collision, true);
            }
            else if (collision.transform.position.y < 0.6f)
            {
                ani.SetTrigger("hit_low_R");
                Hit_Guard(collision, true);
            }
        }

        //왼발
        if (collision.collider.CompareTag("Kick_L"))
        {
            if (collision.transform.position.y > 1.2f)
            {
                ani.SetTrigger("hit_head_L");
                Hit_Guard(collision, false);
            }
            else if (collision.transform.position.y > 0.61f && collision.transform.position.y < 1.2f)
            {
                ani.SetTrigger("hit_mid_L");
                Hit_Guard(collision, false);
            }
            else if (collision.transform.position.y < 0.6f)
            {
                ani.SetTrigger("hit_low_L");
                Hit_Guard(collision, false);
            }
        }

        // 오른발
        if (collision.collider.CompareTag("Kick_R"))
        {
            if (collision.transform.position.y > 1.2f)
            {
                ani.SetTrigger("hit_head_L");
                Hit_Guard(collision, false);
            }
            else if (collision.transform.position.y > 0.61f && collision.transform.position.y < 1.2f)
            {
                ani.SetTrigger("hit_mid_L");
                Hit_Guard(collision, false);
            }
            else if (collision.transform.position.y < 0.6f)
            {
                ani.SetTrigger("hit_low_L");
                Hit_Guard(collision, false);
            }
        }

    }
}
