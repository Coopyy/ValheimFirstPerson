using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.IO;
using HarmonyLib;
using System.Runtime.Serialization;
using System.Collections;

namespace ValheimFP
{
    public class Manager : MonoBehaviour
    {
        public static Harmony hInstance = null;
        public static bool FirstPerson = true;
        public static bool MenuOpen = true;
        public static Vector3 BackupHelm = Vector3.zero; 
        public static bool NeedsToSet = true;
        public Rect r = new Rect(20, 20, 200, 220);
        public static Settings S;
        void Start()
        {
            S = new Settings();
            if (File.Exists(Application.dataPath + "/FPValheim.config"))
                S = XML.ReadFromXmlFile<Settings>(Application.dataPath + "/FPValheim.config");

            Debug.Log("Starting Hooks");
            hInstance = new Harmony("hooks");
            hInstance.PatchAll();
            Debug.Log("Hooked");
        }

        void Update()
        {
            if (Input.GetKeyDown(S.Bind))
            {
                FirstPerson = !FirstPerson;
                NeedsToSet = true;
            }
            if (Input.GetKeyDown(KeyCode.F1))
                MenuOpen = !MenuOpen;

            if (S.Bind == KeyCode.None)
            {
                Event e = Event.current;
                if (e.keyCode != KeyCode.None)
                    S.Bind = e.keyCode;
            }

            if (Player.m_localPlayer)
            {
                Transform _head = FindTransform(Player.m_localPlayer.transform, "Visual", "Armature", "Hips", "Spine", "Spine1", "Spine2", "Neck", "Head");
                Transform _leg1 = FindTransform(Player.m_localPlayer.transform, "Visual", "Armature", "Hips", "LeftUpLeg");
                Transform _leg2 = FindTransform(Player.m_localPlayer.transform, "Visual", "Armature", "Hips", "RightUpLeg");
                Transform _helm = FindTransform(_head, "Helmet_attach");
                if (BackupHelm == Vector3.zero)
                    BackupHelm = _helm.localScale;
                Transform _jaw = FindTransform(_head, "Jaw");

                if (NeedsToSet && BackupHelm != Vector3.zero)
                    if (FirstPerson)
                    {
                        _head.localScale = new Vector3(0.0001f, 0.0001f, 0.0001f);
                        _helm.localScale = new Vector3(0.0001f, 0.0001f, 0.0001f);
                        _leg1.localScale = new Vector3(0.0001f, 0.0001f, 0.0001f);
                        _leg2.localScale = new Vector3(0.0001f, 0.0001f, 0.0001f);
                        _jaw.localScale = new Vector3(0.0001f, 0.0001f, 0.0001f);
                        NeedsToSet = false;
                    }
                    else
                    {
                        _head.localScale = new Vector3(1, 1, 1);
                        _helm.localScale = BackupHelm;
                        _leg1.localScale = new Vector3(1, 1, 1);
                        _leg2.localScale = new Vector3(1, 1, 1);
                        _jaw.localScale = new Vector3(1, 1, 1);
                        NeedsToSet = false;
                    }

            }
        }
        void OnGUI()
        {
            if (MenuOpen)
                r = GUI.Window(0, r, Window, "Valheim First Person");
        }
        private static Transform FindTransform(Transform root, params string[] path)
        {

            Transform output = root;
            for (int i = 0; i < path.Length; i++)
            {
                output = output.Find(path[i]);

                if (output == null) break;
            }

            return output;
        }
        void Window(int windowID)
        {
            GUILayout.Space(5);
            GUILayout.Box("F1 To Toggle Menu");
            if (GUILayout.Button("Cam Toggle Key: " + S.Bind.ToString()))
                S.Bind = KeyCode.None;
            GUILayout.Box("First Person FOV: " + S.FPFov);
            S.FPFov = (int)GUILayout.HorizontalSlider(S.FPFov, 40, 120);
            GUILayout.Box("Third Person FOV: " + S.NormFov);
            S.NormFov = (int)GUILayout.HorizontalSlider(S.NormFov, 40, 120);
            if (GUILayout.Button("Hide Player: " + S.HidePlayer))
                S.HidePlayer = !S.HidePlayer;
            if (GUILayout.Button("Save Settings"))
                XML.WriteToXmlFile(Application.dataPath + "/FPValheim.config", S);
            GUI.DragWindow();
        }
    }
}
