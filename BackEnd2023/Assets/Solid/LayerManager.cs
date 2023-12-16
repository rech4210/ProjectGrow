
using UnityEngine;
using System.Collections;

public class LayerManager : MonoBehaviour
{
    private static LayerManager instance;
    public static LayerManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<LayerManager>();
            }
            if (instance == null)
            {
                instance = new GameObject().AddComponent<LayerManager>();

            }
            return instance;
        }
    }
    public LayerMask HitZone;
    public LayerMask ItemInterObj;

    public void Awake()
    {
        HitZone = 1 + (1 << LayerMask.NameToLayer("ItemInterObj"));
        ItemInterObj = 1 + (1 << LayerMask.NameToLayer("HitZone"));
    }
}
