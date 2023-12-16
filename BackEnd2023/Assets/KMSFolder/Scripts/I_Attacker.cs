using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public interface I_Attacker
{
    Transform myTransform => null;

    void DeadEvent(I_Faction i_Faction) { }
}
