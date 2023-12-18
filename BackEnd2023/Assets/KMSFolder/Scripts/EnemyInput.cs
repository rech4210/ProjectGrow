using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyInput : InputCtrl
{
    bool isAttack = false;
    public float attackDelay = 2f;
    private float attackTemp;
    public List<PlantNameEnum> seedKindsList;
    private I_Pool targetPool;

    public void targetOff(I_Pool targetPool)
    {
        if (targetPool == this.targetPool)
        {
            rootCtrl.targetTran = null;
        }
    }
    private void Update()
    {
        if (attackTemp > 0f)
        {
            attackTemp -= Time.deltaTime;
        }
        //transform.position = targetPlayer.position;
        if (rootCtrl.targetTran == null)
        {
            rootCtrl.targetTran = GameManager.instance.ReturnClosesetObject(this.transform);
            rootCtrl.stateCtrl.IdleState();
            if (rootCtrl.targetTran != null)
            {
                targetPool = rootCtrl.targetTran.GetComponent<I_Pool>();
                targetPool.SetDisableOneEvent(targetOff);
            }
        }

        if (rootCtrl.targetTran != null && rootCtrl.stateCtrl.IsCanAction(rootCtrl.stateCtrl.stateEnum))
        {

            if (Vector2.Distance(rootCtrl.targetTran.position, transform.position) < 1.5f)
            {
                horizontal = 0;
                vertical = 0;
                if (!isAttack && attackTemp <= 0f)
                {
                    attackTemp = attackDelay;
                    isAttack = true;
                    StartCoroutine(DelayAttack());
                }
            }
            else
            {
                Vector3 direction = rootCtrl.targetTran.position - transform.position;
                direction.Normalize();

                if (rootCtrl.stateCtrl.IsCanAction(rootCtrl.stateCtrl.stateEnum))
                {
                    horizontal = direction.x;
                    vertical = direction.y;
                }
                else
                {
                    horizontal = 0;
                    vertical = 0;
                }
            }

        }
    }

    IEnumerator DelayAttack()
    {

        rootCtrl.stateCtrl.AttackState();

        yield return new WaitForSeconds(0.4f);
        Collider2D[] hits = Physics2D.OverlapCircleAll(this.transform.position + rootCtrl.moveCtrl.modelTran.forward, 1.5f, LayerManager.Instance.HitZone);

        if (hits != null && hits.Length > 0)
        {
            //판정해야함
            foreach (Collider2D hit in hits)
            {
                I_HitZone hitZone = hit.transform.GetComponentInParent<I_HitZone>();
                if (hitZone != null)
                {
                    if (rootCtrl.faction != hitZone.Faction && hitZone.CheckHitLock(rootCtrl) == false)
                    {
                        hitZone.SetDamaged(rootCtrl.scriptableMonster.Atk, rootCtrl);//데미지 작업해야함
                        yield return null;
                    }
                }
            }
        }
        rootCtrl.stateCtrl.StateChange(stateEnum.Idle);
        yield return null;
        isAttack = false;

    }

    public override void initiallize()
    {
        base.initiallize();
        //Hp Ctrl aggro reference use
        rootCtrl.aggroAction += (attacker) =>
        {
            rootCtrl.targetTran = attacker.myTransform;
            targetPool = rootCtrl.targetTran.GetComponent<I_Pool>();
            targetPool.SetDisableOneEvent(targetOff);
        };

        rootCtrl.deadAction += () =>
        {
            rootCtrl.stateCtrl.stateEnum = stateEnum.Dead;
            rootCtrl.stateCtrl.DeadState();
            StartCoroutine(ExcuteDeadAction());
        };
        rootCtrl.lifeAction += () =>
        {
            rootCtrl.stateCtrl.stateEnum = stateEnum.Idle;
            rootCtrl.stateCtrl.IdleState();
        };


    }

    IEnumerator ExcuteDeadAction()
    {
        yield return new WaitForSeconds(0.5f);

        if (Random.Range(0f, 1f) < 0.1f)
        {
            PlantNameEnum seed = GetRandomSeed();
            Item_Seed item = ItemCtrl.newItem(ItemKind.Seed, seed.ToString()) as Item_Seed;
            item.seedKind = ItemCtrl.ChangeSeed(seed);
            item.potOut();
            item.gameObject.SetActive(true);
            item.gameObject.transform.position = this.gameObject.transform.position;
        }
        else if (Random.Range(0f, 1f) < 0.4f)
        {
            PlantNameEnum seed = PlantNameEnum.Pot;
            Item_Seed item = ItemCtrl.newItem(ItemKind.Seed, seed.ToString()) as Item_Seed;
            item.seedKind = ItemCtrl.ChangeSeed(seed);
            item.potOut();
            item.gameObject.SetActive(true);
            item.gameObject.transform.position = this.gameObject.transform.position;
        }
        var obj = ScriptableManager.instance.getTable(ScriptableManager.ScriptableTag).getPrefab<Scriptable_Object.PrefabInfo>("Die Effect").Prefabs;
        FieldCtrl.Instance.pool(obj.transform).transform.SetPositionAndRotation(this.transform.position, Quaternion.identity);

        GameManager.instance.soundManager.PlaySoundclip("Snd_death");
        rootCtrl.disable();
    }

    private PlantNameEnum GetRandomSeed()
    {
        var rd = Random.Range(0, seedKindsList.Count - 1);
        return seedKindsList[rd];
    }
}
