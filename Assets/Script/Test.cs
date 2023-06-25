using System.Collections;
using System.Linq.Expressions;
using UnityEngine;

public class Test : MonoBehaviour
{
    public Animator ani;
    public GameObject hit_hand;
    public GameObject guard;
    public GameObject hit_kick;
    public Knockback G_knockback;
    public Knockback M_knockback;

    // test ���嵵 �߰��ؾ߉�
    IEnumerator specialAttackdelay()
    {
        yield return new WaitForSeconds(1f);
        Maskman_ani_Setting.M_S_T = Maskman_ani_Setting.special_state.idle;
    }


    // hit_possibe�� ���������� hit �ƴϸ� guard
    void Hit_Guard(Transform t, bool hand)
    {
        // ��
        if (hand)
        {
            // particle
            if (ani.GetBool("Hit_possible"))    // hit_possible�� �ƴϰ� guard�� �ٲ�� ��
                Instantiate(hit_hand, t.position, t.rotation);
            else
            {
                Instantiate(guard, t.position, t.rotation);
                // �˹� 
                //Debug.Log(gameObject.name);
                if (Maskman_ani_Setting.M_A_T == Maskman_ani_Setting.ani_state.backstep)
                {
                    // ���� special attack
                    if (Guard_ani_Setting.G_S_T == Guard_ani_Setting.special_state.specialattack)
                    {
                        M_knockback.ApplyKnockback(-transform.forward, 5);
                        // special_state = specialattack  - > special_state = idle �������ֱ�                       
                    }
                    else
                    {
                        M_knockback.ApplyKnockback(-transform.forward, 1);
                    }
                }
                if (Guard_ani_Setting.G_A_T == Guard_ani_Setting.ani_state.backstep)
                {
                    // ����ũ�� special attack
                    if (Maskman_ani_Setting.M_S_T == Maskman_ani_Setting.special_state.specialattack)
                    {
                        G_knockback.ApplyKnockback(-transform.forward, 5);
                        // special_state = specialattack  - > special_state = idle �������ֱ�                       
                    }
                    else
                    {
                       G_knockback.ApplyKnockback(-transform.forward, 1);
                    }
                }
            }

            // collider�� maskman���� Guard���� üũ
            if (gameObject.name.Contains("M_Wall_hit") && Maskman_ani_Setting.M_A_T != Maskman_ani_Setting.ani_state.backstep)
            {
                // �޺� ���߿� ���� �� �ִ� ��Ȳ �����ϱ� ���� �ڷ�ƾ �־�ߵǳ�
                Maskman_ani_Setting.M_A_T = Maskman_ani_Setting.ani_state.Hit_hand;

            }
            else if (gameObject.name.Contains("G_Wall_hit") && Guard_ani_Setting.G_A_T != Guard_ani_Setting.ani_state.backstep)
            {
                Guard_ani_Setting.G_A_T = Guard_ani_Setting.ani_state.Hit_hand;
            }
        }
        // ��
        if (!hand)
        {
            // particle
            if (ani.GetBool("Hit_possible"))
                Instantiate(hit_kick, t.position, t.rotation);
            else
            {
                Instantiate(guard, t.position, t.rotation);
                if (Maskman_ani_Setting.M_A_T == Maskman_ani_Setting.ani_state.backstep)
                {
                    // ���� special attack
                    if (Guard_ani_Setting.G_S_T == Guard_ani_Setting.special_state.specialattack)
                    {
                        M_knockback.ApplyKnockback(-transform.forward, 5);
                        // special_state = specialattack  - > special_state = idle �������ֱ�                       
                    }
                    else
                    {
                        M_knockback.ApplyKnockback(-transform.forward, 1.5f);
                    }
                }
                if (Guard_ani_Setting.G_A_T == Guard_ani_Setting.ani_state.backstep)
                {
                    // ����ũ�� special attack
                    if (Maskman_ani_Setting.M_S_T == Maskman_ani_Setting.special_state.specialattack)
                    {
                        G_knockback.ApplyKnockback(-transform.forward, 5);
                        // special_state = specialattack  - > special_state = idle �������ֱ�                       
                    }
                    else
                    {
                        G_knockback.ApplyKnockback(-transform.forward, 1);
                    }
                }
            }

            if (gameObject.name.Contains("M_Wall_hit") && Maskman_ani_Setting.M_A_T != Maskman_ani_Setting.ani_state.backstep)
                Maskman_ani_Setting.M_A_T = Maskman_ani_Setting.ani_state.Hit_kick;

            else if (gameObject.name.Contains("G_Wall_hit") && Guard_ani_Setting.G_A_T != Guard_ani_Setting.ani_state.backstep)
                Guard_ani_Setting.G_A_T = Guard_ani_Setting.ani_state.Hit_kick;
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        //Transform t = collision.transform;
        Transform t = other.transform;
        // �޼�
        if (other.CompareTag("Hand_L"))
        {
            if (t.position.y > 1.2f)
            {
                ani.SetTrigger("hit_head_L");
                Hit_Guard(t, true);
            }
            else if (t.position.y > 0.61f && t.position.y < 1.2f)
            {
                ani.SetTrigger("hit_mid_L");
                Hit_Guard(t, true);
            }

            else if (t.position.y < 0.6f)
            {
                ani.SetTrigger("hit_low_L");
                Hit_Guard(t, true);
            }
        }

        // ������
        if (other.CompareTag("Hand_R"))
        {
            if (t.position.y > 1.2f)
            {
                // ����ũ�ǵ� �߰��ؾ߉�
                if (Guard_ani_Setting.G_S_T == Guard_ani_Setting.special_state.specialattack)
                {
                    if (ani.GetBool("Hit_possible"))
                        ani.SetTrigger("special_hit");
                    else
                        ani.SetTrigger("hit_head_R");
                }
                else
                    ani.SetTrigger("hit_head_R");
                Hit_Guard(t, true);
            }
            else if (t.position.y > 0.61f && t.position.y < 1.2f)
            {
                if (Guard_ani_Setting.G_S_T == Guard_ani_Setting.special_state.specialattack)
                {
                    if (ani.GetBool("Hit_possible"))
                        ani.SetTrigger("special_hit");
                    else
                        ani.SetTrigger("hit_head_R");
                }
                else
                    ani.SetTrigger("hit_mid_R");

                Hit_Guard(t, true);
            }
            else if (t.position.y < 0.6f)
            {
                ani.SetTrigger("hit_low_R");
                Hit_Guard(t, true);
            }
        }

        //�޹�
        if (other.CompareTag("Kick_L"))
        {
            if (t.position.y > 1.2f)
            {
                if (Maskman_ani_Setting.M_S_T == Maskman_ani_Setting.special_state.specialattack)
                {
                    if (ani.GetBool("Hit_possible"))
                        ani.SetTrigger("special_hit");
                    else
                        ani.SetTrigger("hit_head_L");
                }
                else
                    ani.SetTrigger("hit_head_L");
                Hit_Guard(t, false);
            }
            else if (t.position.y > 0.0f && t.position.y < 1.2f)
            {
                if (Maskman_ani_Setting.M_S_T == Maskman_ani_Setting.special_state.specialattack)
                {
                    if (ani.GetBool("Hit_possible"))
                        ani.SetTrigger("special_hit");
                    else
                        ani.SetTrigger("hit_head_L");
                }
                else
                    ani.SetTrigger("hit_mid_L");
                Hit_Guard(t, false);
            }

        }

        // ������
        if (other.CompareTag("Kick_R"))
        {
            if (t.position.y > 1.2f)
            {
                if (Maskman_ani_Setting.M_S_T == Maskman_ani_Setting.special_state.specialattack)
                {
                    if (ani.GetBool("Hit_possible"))
                        ani.SetTrigger("special_hit");
                    else
                        ani.SetTrigger("hit_head_R");
                }
                else
                ani.SetTrigger("hit_head_R");
                Hit_Guard(t, false);
            }
            else if (t.position.y > 0f && t.position.y < 1.2f)
            {
                if (Maskman_ani_Setting.M_S_T == Maskman_ani_Setting.special_state.specialattack)
                {
                    if (ani.GetBool("Hit_possible"))
                        ani.SetTrigger("special_hit");
                    else
                        ani.SetTrigger("hit_head_R");
                }
                else
                    ani.SetTrigger("hit_mid_R");
                Hit_Guard(t, false);
            }

            // �ϴ� �Ͻ� �ߴ�
            //else if (t.position.y < 0.6f)
            //{
            //    ani.SetTrigger("hit_low_L");
            //    Hit_Guard(collision, false);
            //}
        }

        //    private void OnCollisionEnter(Collision collision)
        //{
        //    print(collision.transform.name + ", " + collision.collider.name);
        //    //Transform t = collision.transform;
        //    Transform t = collision.collider.transform;
        //    // �޼�
        //    if (collision.collider.CompareTag("Hand_L"))
        //    {
        //        if (t.position.y > 1.2f)
        //        {
        //            ani.SetTrigger("hit_head_L");
        //            Hit_Guard(collision, true);
        //        }
        //        else if (t.position.y > 0.61f && t.position.y < 1.2f)
        //        {
        //            ani.SetTrigger("hit_mid_L");
        //            Hit_Guard(collision, true);
        //        }

        //        else if (t.position.y < 0.6f)
        //        {
        //            ani.SetTrigger("hit_low_L");
        //            Hit_Guard(collision, true);
        //        }
        //    }

        //    // ������
        //    if (collision.collider.CompareTag("Hand_R"))
        //    {
        //        if (t.position.y > 1.2f)
        //        {
        //            ani.SetTrigger("hit_head_R");
        //            Hit_Guard(collision, true);
        //        }
        //        else if (t.position.y > 0.61f && t.position.y < 1.2f)
        //        {
        //            ani.SetTrigger("hit_mid_R");
        //            Hit_Guard(collision, true);
        //        }
        //        else if (t.position.y < 0.6f)
        //        {
        //            ani.SetTrigger("hit_low_R");
        //            Hit_Guard(collision, true);
        //        }
        //    }

        //    //�޹�
        //    if (collision.collider.CompareTag("Kick_L"))
        //    {
        //        if (t.position.y > 1.2f)
        //        {
        //            ani.SetTrigger("hit_head_L");
        //            Hit_Guard(collision, false);
        //        }
        //        else if (t.position.y > 0.0f && t.position.y < 1.2f)
        //        {
        //            ani.SetTrigger("hit_mid_L");
        //            Hit_Guard(collision, false);
        //        }
        //        // �ϴ� �Ͻ� �ߴ�
        //        //else if (t.position.y < 0.6f)
        //        //{
        //        //    ani.SetTrigger("hit_low_L");
        //        //    Hit_Guard(collision, false);
        //        //}
        //    }

        //    // ������
        //    if (collision.collider.CompareTag("Kick_R"))
        //    {
        //        if (t.position.y > 1.2f)
        //        {
        //            ani.SetTrigger("hit_head_L");
        //            Hit_Guard(collision, false);
        //        }
        //        else if (t.position.y > 0f && t.position.y < 1.2f)
        //        {
        //            ani.SetTrigger("hit_mid_L");
        //            Hit_Guard(collision, false);
        //        }

        //        // �ϴ� �Ͻ� �ߴ�
        //        //else if (t.position.y < 0.6f)
        //        //{
        //        //    ani.SetTrigger("hit_low_L");
        //        //    Hit_Guard(collision, false);
        //        //}
        //    }

    }
}
