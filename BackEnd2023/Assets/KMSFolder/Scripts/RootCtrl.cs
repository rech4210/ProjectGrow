using System;
using UnityEngine;

public struct Status
{
    public float speed;
    public Status(float speed)
    {
        this.speed = speed;
    }
}

public class RootCtrl : MonoBehaviour, I_Attacker, I_Pool, I_Faction
{
    public MonsterKind kind;
    public ScriptableMonsterInfo.PrefabInfo scriptableMonster;

    public Action lifeAction;
    public Action deadAction;
    public Action<I_Attacker> aggroAction;

    //??? 프로퍼티 축약.
    public Transform myTransform => this.transform;
    public bool IsTarget => stateCtrl.stateEnum != stateEnum.Dead;//사망이 아닐때
    public Status status;
    public Transform TargetTran => targetTran;
    public Faction Faction => faction;
    public Faction faction;
    public Transform targetTran;

    public InputCtrl inputCtrl;
    public MoveCtrl moveCtrl;
    public StateCtrl stateCtrl;
    public HpCtrl hpCtrl;
    public WeaponCtrl WeaponCtrl;
    public AnimationCtrl AnimationCtrl;
    //public InteracionCtrl InteracionCtrl;

    public I_Interaction interaction;


    private void Awake()
    {

        scriptableMonster = ScriptableManager.instance.getTable(ScriptableManager.MobTableTag)
               .getPrefab<ScriptableMonsterInfo.PrefabInfo>(kind.ToString());

        status = new Status(3f);
        inputCtrl = gameObject.GetComponent<InputCtrl>();
        inputCtrl.initiallize();

        moveCtrl = gameObject.GetComponent<MoveCtrl>();
        moveCtrl.initiallize();

        stateCtrl = gameObject.GetComponent<StateCtrl>();
        stateCtrl.initiallize();

        hpCtrl = gameObject.GetComponent<HpCtrl>();
        hpCtrl.initiallize();

        WeaponCtrl = gameObject.GetComponent<WeaponCtrl>();
        WeaponCtrl.initiallize();

        AnimationCtrl = gameObject.GetComponent<AnimationCtrl>();
        AnimationCtrl.initiallize();

        // 이부분은 구현된 개체를 자동으로 가져온다.
        interaction = gameObject.GetComponent<I_Interaction>();
        interaction.initiallize();
    }


    /// <RootCtrl 플레이어용>
    protected Action<I_Pool> disableAction;
    public void SetPoolEvent(Action<I_Pool> poolevent)
    {
        disableAction += poolevent;
    }
    public virtual void disable()
    {
        //풀링 회수 
        disableAction?.Invoke(this);
    }
    /// </summary>
    /// 

    // enemy 어그로 재조정 함수
    public void DeadEvent(I_Faction i_Faction)
    {
        if (targetTran == i_Faction.myTransform)
        {
            targetTran = null;
        }
    }
    // 입력관리, 상태머신, 조작, 컨트롤간 제어


    //#region input

    //#endregion


    //#region move

    //#endregion

    //#region state // 애니메이션 상태머신 요청

    //#endregion

    //#region hp

    //#endregion

    //#region weapon

    //#endregion

    //#region animation

    //#endregion

}
