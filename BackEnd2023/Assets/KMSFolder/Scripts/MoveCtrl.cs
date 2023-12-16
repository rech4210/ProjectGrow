using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MoveCtrl : MonoBehaviour, InitiallizeInterface
{
    RootCtrl rootCtrl;
    float speed;
    float enemySpeed = 3;
    // 이동 처리, 플립 관리

    Vector3 pos;

    //스프라이트 반전 scale -1
    //SpriteRenderer playerRenderer;
    public Transform target = null;

    public void initiallize()
    {
        rootCtrl = gameObject.GetComponent<RootCtrl>();
        speed = rootCtrl.status.speed;
        //target = rootCtrl.inputCtrl.target; 
    }

    void Update()
    {
        //gettype 수정하기
        //플레이어 이동 관련
        if (rootCtrl.inputCtrl.GetType() == typeof(EnemyInput))
        {

            Vector3 direction = target.position - transform.position;
            direction.Normalize();

            transform.position += direction * (speed / enemySpeed) * Time.deltaTime;

            //Physics2D.OverlapCircle
            //transform.Translate(Vector2.MoveTowards(transform.position, target.position,10) * speed * Time.deltaTime);
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
                    target.localScale = new Vector3(1, 1, 1);
                }
                else if (rootCtrl.inputCtrl.horizontal < 0)
                {
                    target.localScale = new Vector3(-1, 1, 1);
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
