using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MoveCtrl : MonoBehaviour, InitiallizeInterface
{
    RootCtrl rootCtrl;
    float speed;
    // 捞悼 贸府, 敲赋 包府

    Vector3 pos;

    SpriteRenderer playerRenderer;


    public void initiallize()
    {
        rootCtrl = gameObject.GetComponent<RootCtrl>();
        speed = rootCtrl.status.speed;
        playerRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        
        pos = Vector3.zero;

        if ((rootCtrl.stateCtrl.stateEnum != stateEnum.Stunned) || Input.GetAxis("Horizontal") !=0) 
        {
            pos.x = rootCtrl.inputCtrl.horizontal * Time.deltaTime * speed;
            if(rootCtrl.inputCtrl.horizontal > 0)
            {
                playerRenderer.flipX = false;
                rootCtrl.stateCtrl.WalkState();
            }
            else
            {
                playerRenderer.flipX = true;
                rootCtrl.stateCtrl.WalkState();
            }
        }

        if ((rootCtrl.stateCtrl.stateEnum != stateEnum.Stunned) || Input.GetAxis("Vertical") != 0)
        {
            pos.y = rootCtrl.inputCtrl.vertical * Time.deltaTime * speed;
            rootCtrl.stateCtrl.WalkState();
        }

        transform.position += pos;
        rootCtrl.stateCtrl.IdleState();
    }
}
