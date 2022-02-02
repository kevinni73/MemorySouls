using UnityEngine;

public class AttackOnSwing : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.GetComponent<Knight>().Attack(stateInfo.length / 2f);
    }
}
