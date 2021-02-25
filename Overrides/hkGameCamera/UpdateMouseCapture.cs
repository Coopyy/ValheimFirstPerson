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
    [HarmonyPatch("UpdateMouseCapture")]
    public class UpdateMouseCapture
	{
        public static bool Prefix(GameCamera __instance, ref bool ___m_mouseCapture)
        {
			if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.F1))
			{
				___m_mouseCapture = !___m_mouseCapture;
			}
			if (___m_mouseCapture && !InventoryGui.IsVisible() && !Menu.IsVisible() && !Minimap.IsOpen() && !StoreGui.IsVisible() && !Hud.IsPieceSelectionVisible() && !Manager.MenuOpen)
			{
				Cursor.lockState = CursorLockMode.Locked;
				Cursor.visible = false;
				return false;
			}
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = ZInput.IsMouseActive();
			return false;
		}
    }
}
