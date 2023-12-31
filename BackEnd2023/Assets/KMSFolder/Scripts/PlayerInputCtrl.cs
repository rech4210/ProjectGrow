using System.Collections;
using UnityEngine;

public class PlayerInputCtrl : InputCtrl
{
    private float reviveTime = 3f;
    private void Update()
    {

        if (rootCtrl.stateCtrl.IsCanAction(rootCtrl.stateCtrl.stateEnum))
        {
            Vector2 moveDic = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            horizontal = moveDic.x;
            vertical = moveDic.y;
        }
        else
        {
            horizontal = 0;
            vertical = 0;
        }

        //Add 4 keys


        //이거 바꾸면 stack overflow 뜸
        if (Input.GetMouseButtonDown(0))
        {
            attackState = BtnState.Down;
        }
        else if (Input.GetMouseButton(0))
        {
            attackState = BtnState.Stay;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            attackState = BtnState.Up;
        }
        else
        {
            attackState = BtnState.None;
        }

        //상호작용키
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (rootCtrl.stateCtrl.IsCanAction(rootCtrl.stateCtrl.stateEnum))
            {
                rootCtrl.interaction.interactionGrap();
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            rootCtrl.interaction.interactionThrow();
        }
    }

    public override void initiallize()
    {
        base.initiallize();
        rootCtrl.deadAction += () =>
        {
            if (rootCtrl.stateCtrl.IsCanAction(rootCtrl.stateCtrl.stateEnum) && Time.realtimeSinceStartup > 5f)
            {
                rootCtrl.stateCtrl.stateEnum = stateEnum.Stunned;
                rootCtrl.stateCtrl.StunState();
                StartCoroutine(ExecuteLifeAction());
            }
        };

    }
    public IEnumerator ExecuteLifeAction()
    {
        yield return new WaitForSeconds(reviveTime);
        rootCtrl.lifeAction.Invoke();
    }
}