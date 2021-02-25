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
        public static bool Prefix(GameCamera __instance, ref Camera ___m_camera, ref float ___m_distance)
        {
            if (Player.m_localPlayer == null)
                return true;
            if (!Manager.FirstPerson)
            {
                __instance.m_fov = Manager.S.NormFov;
                return true;
            }
            if (___m_camera == null)
                return true;

            __instance.m_fov = Manager.S.FPFov;
            ___m_camera.fieldOfView = __instance.m_fov;
            __instance.m_skyCamera.fieldOfView = __instance.m_fov;
            ___m_distance = 0;
            ___m_camera.transform.position = Player.m_localPlayer.m_eye.transform.position;
            ___m_camera.transform.rotation = Player.m_localPlayer.m_eye.transform.rotation;

            return false;
        }
    }
}
