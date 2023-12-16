
using UnityEngine;
using System.Collections;

public class LayerManager : MonoBehaviour
{
    public static LayerMask HitZone;
    public static LayerMask ItemInterObj;

    public void Awake()
    {
        HitZone = 1 + (1 << LayerMask.NameToLayer("ItemInterObj"));
        ItemInterObj = 1 + (1 << LayerMask.NameToLayer("HitZone"));
    }
}
