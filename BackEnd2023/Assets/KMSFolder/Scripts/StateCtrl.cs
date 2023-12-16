using UnityEngine;

public enum stateEnum
{
    Idle,
    Stunned,
    Move,
    Dead,
    Grap,
    Use,
    Throw,
    hited,
    Attack
}

public class StateCtrl : MonoBehaviour, InitiallizeInterface
{
    public float time = 0;
    float stunnedTime = 0;
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

    private void Update()
    {
        time += Time.deltaTime;
        if(time- stunnedTime > 3f && stateEnum == stateEnum.Stunned)
        {
            IdleState();
        }
    }

    public void HitedState()
    {
        if (IsCanAction(stateEnum))
        {
            StateChange(stateEnum.hited);
            rootCtrl.AnimationCtrl.HitedAnimation();
            stunnedTime = time;
        }
    }

    #region 상태이상 변경
    public void StunState()
    {
        if (IsCanAction(stateEnum))
        {
            StateChange(stateEnum.Stunned);
            rootCtrl.AnimationCtrl.StunningAnimation();
            stunnedTime = time;
        }
        //스턴 제어 (시간 추가)
    }

    public void DeadState()
    {
        if (stateEnum != stateEnum.Dead)
        {
            StateChange(stateEnum.Dead);
            rootCtrl.AnimationCtrl.DeadAnimation();
        }
    }
    public void IdleState()
    {
        if (IsCanAction(stateEnum))
        {
            stateEnum = stateEnum.Idle;
            rootCtrl.AnimationCtrl.IdleToMoveAniamtion(0);
        }
    }
    public void WalkState(float pointX,float pointY)
    {
        if(pointX <0)
        {
            pointX = -pointX;
        }
        if(pointY <0)
        {
            pointY = -pointY;
        }
        if (IsCanAction(stateEnum))
        {
            stateEnum = stateEnum.Move;
            rootCtrl.AnimationCtrl.IdleToMoveAniamtion(pointX+pointY);
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


    public void AttackState()
    {
        if (IsCanAction(stateEnum))
        {
            stateEnum = stateEnum.Attack;
            rootCtrl.AnimationCtrl.ThrowAniamtion();
        }
    }
}
