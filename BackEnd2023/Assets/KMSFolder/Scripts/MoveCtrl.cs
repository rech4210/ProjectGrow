using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MoveCtrl : MonoBehaviour, InitiallizeInterface
{
    RootCtrl rootCtrl;
    float speed;
    // 이동 처리, 플립 관리

    Vector3 pos;

    //스프라이트 반전 scale -1
    //SpriteRenderer playerRenderer;
   [SerializeField] Transform Target;

    public void initiallize()
    {
        rootCtrl = gameObject.GetComponent<RootCtrl>();
        speed = rootCtrl.status.speed;
        //Target = rootCtrl.inputCtrl.target;
    }

    void Update()
    {

        //gettype 수정하기
        //플레이어 이동 관련
        if (rootCtrl.inputCtrl.GetType() == typeof(EnemyInput))
        {
            //Vector3 pos = Vector3.MoveTowards(transform.position, Target.position, speed * Time.deltaTime);

            //transform.position += pos;
        }


        //적 이동 관련
        else if (rootCtrl.inputCtrl.GetType() == typeof(PlayerInputCtrl))
        {
            pos = Vector3.zero;

            if ((rootCtrl.stateCtrl.IsCanAction(rootCtrl.stateCtrl.stateEnum)) || Input.GetAxis("Horizontal") != 0)
            {
                pos.x = rootCtrl.inputCtrl.horizontal * Time.deltaTime * speed;
                if (rootCtrl.inputCtrl.horizontal > 0)
                {
                    Target.localScale = new Vector3(1, 1, 1);
                }
                else if (rootCtrl.inputCtrl.horizontal < 0)
                {
                    Target.localScale = new Vector3(-1, 1, 1);
                }
                rootCtrl.stateCtrl.WalkState(rootCtrl.inputCtrl.horizontal, rootCtrl.inputCtrl.vertical);
            }
            else
            {
                rootCtrl.stateCtrl.IdleState();
            }

            if ((rootCtrl.stateCtrl.IsCanAction(rootCtrl.stateCtrl.stateEnum)) || Input.GetAxis("Vertical") != 0)
            {
                pos.y = rootCtrl.inputCtrl.vertical * Time.deltaTime * speed;
                rootCtrl.stateCtrl.WalkState(rootCtrl.inputCtrl.horizontal, rootCtrl.inputCtrl.vertical);
            }
            else
            {
                rootCtrl.stateCtrl.IdleState();
            }
            transform.position += pos;
        }
    }
}
