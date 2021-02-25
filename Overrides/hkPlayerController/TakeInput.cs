using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace ValheimFP.Overrides.hkInventoryGui
{
    [HarmonyPatch(typeof(PlayerController))]
    [HarmonyPatch("TakeInput")]
    public class TakeInput
    {
        public static bool Prefix(bool __result)
        {
            if (Manager.MenuOpen)
            {
                __result = false;
                return false;
            }
            return true;
        }
    }
}
