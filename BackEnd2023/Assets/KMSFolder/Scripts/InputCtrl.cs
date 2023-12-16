using UnityEngine;


public enum BtnState
{ 
    None = 0,
    Down = 1,
    Stay = 2,
    Up = 3
}


public class InputCtrl : MonoBehaviour, InitiallizeInterface
{
    RootCtrl rootCtrl;
    public Status status;

    public BtnState attackState;

    public float horizontal;
    public float vertical;
    
    private void Update()
    {

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        //Add 4 keys

        if (Input.GetMouseButton(0))
        {
            attackState = BtnState.Stay;
        }
        else if(Input.GetMouseButtonDown(0))
        {
            attackState = BtnState.Down;
        }
        else if(Input.GetMouseButtonUp(0)) 
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
            if(rootCtrl.stateCtrl.IsCanAction(rootCtrl.stateCtrl.stateEnum))
            {
                rootCtrl.interaction.interactionGrap();
            }
        }
    }

    // 입력관리, 상태머신, 조작, 다른, Ctrl에 작업요청 or AI 처리
    public void initiallize()
    {
        rootCtrl = gameObject.GetComponent<RootCtrl>();
        status = rootCtrl.status;
    }

    // OverLapsCircle
    //private void OnCollisionEnter(Collision collision)
    //{
    //    if(collision.gameObject.CompareTag("아군"))
    //    {
    //        //collision.gameObject.GetComponent<Player>().enabled = false;
            
    //        //collision.gameObject.transform.SetParent(transform, false);
    //        //아군 터렛의 경우 옮기기와 내려놓기를 호출
    //    }
    //    else if(collision.gameObject.CompareTag("적"))
    //    {
    //        //collision.gameObject.GetComponent<Enemy>().enabled = false;
    //    }
    //}
}
