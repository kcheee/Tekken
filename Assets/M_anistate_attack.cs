using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_anistate_attack : StateMachineBehaviour
{

    public bool H_K_check;
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (H_K_check)
            Maskman_ani_Setting.M_A_T = Maskman_ani_Setting.ani_state.H_attack;
        else
            Maskman_ani_Setting.M_A_T = Maskman_ani_Setting.ani_state.K_attack;
    }
}
