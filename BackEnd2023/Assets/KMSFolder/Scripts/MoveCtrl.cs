using UnityEngine;

public class MoveCtrl : MonoBehaviour, InitiallizeInterface
{
    RootCtrl rootCtrl;
    float speed;
    float enemySpeed = 3;
    // 이동 처리, 플립 관리
    Vector3 pos;

    //스프라이트 반전 scale -1
    //SpriteRenderer playerRenderer;
    public Transform modelTran = null;

    public void initiallize()
    {

        rootCtrl = gameObject.GetComponent<RootCtrl>();
        speed = rootCtrl.status.speed;
        //target = rootCtrl.inputCtrl.target; 
    }

    void Update()
    {
        pos = Vector3.zero;

        if ((rootCtrl.stateCtrl.IsCanAction(rootCtrl.stateCtrl.stateEnum)))
        {
            pos.x = rootCtrl.inputCtrl.horizontal * Time.deltaTime * rootCtrl.scriptableMonster.MoveSpeed;
            transform.position += pos;
            pos.y = rootCtrl.inputCtrl.vertical * Time.deltaTime * rootCtrl.scriptableMonster.MoveSpeed;
            transform.position += pos;
            if (rootCtrl.targetTran != null)
            {

                if ((rootCtrl.targetTran.position.x - transform.position.x) > 0f)
                {
                    modelTran.localScale = new Vector3(-1, 1, 1);
                }
                else if (rootCtrl.targetTran.position.x - transform.position.x < 0f)
                {
                    modelTran.localScale = new Vector3(1, 1, 1);
                }
            }
            else
            {

                if ((rootCtrl.inputCtrl.horizontal) > 0f)
                {
                    modelTran.localScale = new Vector3(-1, 1, 1);
                }
                else if (rootCtrl.inputCtrl.horizontal < 0f)
                {
                    modelTran.localScale = new Vector3(1, 1, 1);
                }
            }

            rootCtrl.stateCtrl.WalkState(rootCtrl.inputCtrl.horizontal * (modelTran.localScale.x), rootCtrl.inputCtrl.vertical);
        }
        else
        {
            rootCtrl.stateCtrl.IdleState();
        }


    }
}
