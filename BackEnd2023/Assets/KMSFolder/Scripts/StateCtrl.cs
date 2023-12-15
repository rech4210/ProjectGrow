using UnityEngine;

public enum stateEnum
{
    Idle,
    Stunned,
    Move,
    Dead,
    Grap,
    Use,
    Throw
}

public class StateCtrl : MonoBehaviour, InitiallizeInterface
{

    public stateEnum stateEnum = stateEnum.Idle;
    RootCtrl rootCtrl;


    //상태머신 (대기, 걷기, 달리기,공격,기절,포박,상효작용,이송) 애니메이션 요청
    public void initiallize()
    {
        rootCtrl = gameObject.GetComponent<RootCtrl>();
    }

    public void StateChange(stateEnum state)
    {
        switch (stateEnum)
        {
            case stateEnum.Idle:
                stateEnum = stateEnum.Idle;
                break;
            case stateEnum.Stunned:
                stateEnum = stateEnum.Stunned;
                break;
            case stateEnum.Move:
                stateEnum = stateEnum.Move;
                break;
            case stateEnum.Dead:
                stateEnum = stateEnum.Dead;
                break;
            case stateEnum.Grap:
                stateEnum = stateEnum.Grap;
                break;
            case stateEnum.Use:
                stateEnum = stateEnum.Use;
                break;
            case stateEnum.Throw:
                stateEnum = stateEnum.Throw;
                break;
        }
    }

    #region 상태이상 변경
    public void StunState()
    {
        if (IsCanAction(stateEnum))
        {
            StateChange(stateEnum.Stunned);
            rootCtrl.AnimationCtrl.StunningAnimation();
        }
    }

    public void DeathState()
    {
        if (stateEnum != stateEnum.Dead)
        {
            StateChange(stateEnum.Dead);
            rootCtrl.AnimationCtrl.DeathAnimation();
        }
    }
    public void IdleState()
    {
        if (IsCanAction(stateEnum))
        {
            stateEnum = stateEnum.Idle;
            rootCtrl.AnimationCtrl.IdleAniamtion();
        }
    }
    public void WalkState()
    {
        if (IsCanAction(stateEnum))
        {
            stateEnum = stateEnum.Move;
            rootCtrl.AnimationCtrl.MoveAniamtion();
        }
    }
    public void GrapState()
    {
        if (IsCanAction(stateEnum))
        {
            stateEnum = stateEnum.Grap;
            rootCtrl.AnimationCtrl.GrapAniamtion();
        }
    }

    public void UseState()
    {
        if (IsCanAction(stateEnum))
        {
            stateEnum = stateEnum.Use;
            rootCtrl.AnimationCtrl.UseAniamtion();
        }
    }
    public void ThrowState()
    {
        if (IsCanAction(stateEnum))
        {
            stateEnum = stateEnum.Throw;
            rootCtrl.AnimationCtrl.ThrowAniamtion();
        }
    }
    #endregion

    public bool IsCanAction(stateEnum state)
    {
        if(state != stateEnum.Stunned || state != stateEnum.Dead)
        {
            return true;
        }
        return false;
    }
}
