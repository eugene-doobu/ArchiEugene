using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArchiEugene.AnimatorMachine
{
    public class NpcStateMachine : StateMachineBehaviour
    {
        // States
        private readonly int HASH_IDLE = Animator.StringToHash("IdleE");
        
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            ResetAnimPoseParameter(animator, stateInfo);
        }

        private void ResetAnimPoseParameter(Animator animator, AnimatorStateInfo stateInfo)
        {
            if (stateInfo.shortNameHash == HASH_IDLE) return;
            animator.SetInteger(Define.HASH_NPC_ANIMATION, 0);
        }
    }
}
