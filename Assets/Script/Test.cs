using UnityEngine;

public class Test : MonoBehaviour
{
    public Animator ani;
    public GameObject hit_hand;
    public GameObject guard;
    public GameObject hit_kick;

    // hit_possibe�� ���������� hit �ƴϸ� guard
    void Hit_Guard(Collision collision, bool hand)
    {
        // ��
        if (hand)
        {
            // particle
            if (ani.GetBool("Hit_possible"))    // hit_possible�� �ƴϰ� guard�� �ٲ�� ��
                Instantiate(hit_hand, collision.transform.position, collision.transform.rotation);
            else
                Instantiate(guard, collision.transform.position, collision.transform.rotation);

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
        // �޼�
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

        // ������
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

        //�޹�
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

        // ������
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
