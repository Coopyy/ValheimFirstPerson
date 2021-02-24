using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ValheimFP
{
    public static class Main
    {
        public static GameObject CO;
        public static void Load()
        {
            Debug.Log("Loaded");
            CO = new GameObject();
            UnityEngine.Object.DontDestroyOnLoad(CO);
            CO.AddComponent<Manager>();
        }
    }
}
