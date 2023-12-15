using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotSlot : ItemCtrl
{
    public override ItemKind itemKind => ItemKind.Slot;

    public ItemCtrl nowItemCtrl;
    public int index;

    public override bool checkUse(ItemCtrl nowItem)
    {
        return false;
    }

    public override void GrabToggle(I_RootCtrl rootCtrl, bool isGrab)
    {
    }

    public override void UseCall(I_RootCtrl rootCtrl, UseState useState)
    {
    }
}
