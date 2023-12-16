using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotSlot : ItemCtrl
{
    public override ItemKind itemKind => ItemKind.Slot;

    public ItemCtrl nowPot;
    public int index;

    public override bool checkUse(ItemCtrl nowItem)
    {
        return false;
    }

    public override ItemCtrl GrabToggle(RootCtrl rootCtrl, bool isGrab)
    {
        return null;
    }

    public override void UseCall(RootCtrl rootCtrl, UseState useState)
    {
    }
}
