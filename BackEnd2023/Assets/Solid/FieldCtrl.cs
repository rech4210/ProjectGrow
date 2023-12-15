using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldCtrl : MonoBehaviour
{
    private static FieldCtrl instance;
    public static FieldCtrl Instance { get { return instance; } }
    //화분을 관리함
    public Vector2Int grid;
    public Vector2 offset;//위치 오프셋

    public PotSlot potSlotPrefab;
    public PotSlot[] potArr;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        potArr = new PotSlot[grid.x * grid.y];
        for (int i = 0; i < potArr.Length; i++)
        {
            potArr[i] = Instantiate(potSlotPrefab, this.transform);
            potArr[i].index = i;
        }
    }

    public void setSlot(ItemCtrl potCtrl, PotSlot slot)
    {
        slot.nowItemCtrl = potCtrl;
    }





}
