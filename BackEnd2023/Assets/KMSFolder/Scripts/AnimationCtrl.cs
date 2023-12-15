using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationCtrl : MonoBehaviour, InitiallizeInterface
{
    RootCtrl rootCtrl;
    Animator animator;

    // 상태머신에서 요청한 애니메이션 처리
    public void initiallize()
    {
        rootCtrl = gameObject.GetComponent<RootCtrl>();
        animator = gameObject.GetComponent<Animator>();
    }

    #region 상태이상

    public void IdleAniamtion()
    {
        animator.SetTrigger("Idle");
    }
    public void StunningAnimation()
    {
        animator.SetTrigger("Stunned");
    }

    public void MoveAniamtion()
    {
        animator.SetTrigger("Move");
    }

    public void DeathAnimation()
    {
        animator.SetTrigger("Death");
    }
    public void GrapAniamtion()
    {
        animator.SetTrigger("Grap");
    }
    public void UseAniamtion()
    {
        animator.SetTrigger("Use");
    }
    public void ThrowAniamtion()
    {
        animator.SetTrigger("Throw");
    }
    #endregion
}
