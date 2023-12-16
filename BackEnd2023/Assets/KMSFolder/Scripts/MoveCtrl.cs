using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
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

        if ((rootCtrl.stateCtrl.IsCanAction(rootCtrl.stateCtrl.stateEnum)) || Input.GetAxis("Horizontal") !=0) 
        {
            pos.x = rootCtrl.inputCtrl.horizontal * Time.deltaTime * speed;
            if(rootCtrl.inputCtrl.horizontal > 0)
            {
                playerRenderer.flipX = false;
                rootCtrl.stateCtrl.WalkState(rootCtrl.inputCtrl.horizontal, rootCtrl.inputCtrl.vertical);

            }
            else
            {
                playerRenderer.flipX = true;
                rootCtrl.stateCtrl.WalkState(rootCtrl.inputCtrl.horizontal, rootCtrl.inputCtrl.vertical);

            }
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
