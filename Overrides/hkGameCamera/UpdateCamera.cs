using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace ValheimFP.Overrides.hkGameCamera
{
    [HarmonyPatch(typeof(GameCamera))]
    [HarmonyPatch("UpdateCamera")]
    public class UpdateCamera
    {
        public static bool Prefix(GameCamera __instance)
        {
            if (!Manager.FirstPerson)
                return true;

            Camera c = Manager.GetInstanceField(typeof(GameCamera), __instance, "m_camera") as Camera;
            if (c == null)
                return true;

            c.fieldOfView = __instance.m_fov;
            __instance.m_skyCamera.fieldOfView = __instance.m_fov;
            typeof(GameCamera).GetField("m_distance", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(__instance, 0);

            c.transform.position = Player.m_localPlayer.m_eye.transform.position;
            c.transform.rotation = Player.m_localPlayer.m_eye.transform.rotation;

            return false;
        }
    }
}
