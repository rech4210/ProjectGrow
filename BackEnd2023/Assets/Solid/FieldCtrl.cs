using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldCtrl : MonoBehaviour
{
    private static FieldCtrl instance;
    public static FieldCtrl Instance { get { return instance; } }
    //화분을 관리함
    public Vector2Int grid;
    public Vector2 gridSize;
    public Vector2 offset;//위치 오프셋

    public PotSlot potSlotPrefab;
    public PotSlot[] potArr;

    private void Awake()
    {
        instance = this;
        potSlotPrefab.gameObject.SetActive(false);
    }
    private void Start()
    {
        potArr = new PotSlot[grid.x * grid.y];
        for (int i = 0; i < potArr.Length; i++)
        {
            potArr[i] = Instantiate(potSlotPrefab, this.transform);
            potArr[i].gameObject.SetActive(true);
            potArr[i].index = i;
            potArr[i].transform.localPosition = new Vector3(gridSize.x * (i % grid.x) - offset.x, gridSize.y * (i / grid.y) - offset.y, 0f);
        }
    }

    public void setSlot(Item_Pot potCtrl, PotSlot slot)
    {
        slot.nowPot = potCtrl;
        potCtrl.isGrabLock = true;
        potCtrl.Awake();
        potCtrl.transform.SetParent(slot.transform);
        potCtrl.hpCtrl.lifeOn();
        potCtrl.transform.localPosition = Vector3.zero;
    }
    public void removePot(Item_Pot pot)
    {
        for (int i = 0; i < potArr.Length; i++)
        {
            if (potArr[i].nowPot == pot)
            {
                potArr[i].nowPot = null;
                return;
            }
        }
    }





}
