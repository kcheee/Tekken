using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G_anistate_attack : StateMachineBehaviour
{
   
    public bool H_K_check;
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {     
        //if (H_K_check)
        //    Guard_ani_Setting.G_A_T = Guard_ani_Setting.ani_state.H_attack;
        //else
        //    Guard_ani_Setting.G_A_T = Guard_ani_Setting.ani_state.K_attack;
    }
}
