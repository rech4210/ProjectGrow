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

    public BtnState attackState;


    protected RootCtrl rootCtrl;
    public Status status;

    public float horizontal;
    public float vertical;

    // 입력관리, 상태머신, 조작, 다른, Ctrl에 작업요청 or AI 처리
    public virtual void initiallize()
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
