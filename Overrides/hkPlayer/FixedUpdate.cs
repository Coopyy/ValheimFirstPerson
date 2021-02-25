using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace ValheimFP.Overrides.hkPlayer
{
    [HarmonyPatch(typeof(Player))]
    [HarmonyPatch("FixedUpdate")]
    public class FixedUpdate
    {
        public static void Postfix(ref LODGroup ___m_lodGroup, Vector3 ___m_originalLocalRef)
        {
            if (Manager.FirstPerson && !Manager.S.HidePlayer)
                ___m_lodGroup.localReferencePoint = ___m_originalLocalRef;
        }
    }
}
