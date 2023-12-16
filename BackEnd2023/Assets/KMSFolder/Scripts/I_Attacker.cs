using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public interface I_Attacker
{
    Transform myTransform => null;
    Transform TargetTran => null;
    public Faction Faction => Faction.None;

    void DeadEvent(I_Faction i_Faction) { }
}
