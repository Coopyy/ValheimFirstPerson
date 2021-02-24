using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.IO;
using HarmonyLib;
using System.Reflection;
using System.Collections;

namespace ValheimFP
{
    public class Manager : MonoBehaviour
    {
        public static Harmony hInstance = null;
        public static bool FirstPerson = true;
        void Start()
        {
            Debug.Log("Starting Hooks");
            hInstance = new Harmony("hooks");
            hInstance.PatchAll();
            Debug.Log("Hooked");
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.F))
                FirstPerson = !FirstPerson;
        }


        /// <summary>
        /// Uses reflection to get the field value from an object.
        /// </summary>
        ///
        /// <param name="type">The instance type.</param>
        /// <param name="instance">The instance object.</param>
        /// <param name="fieldName">The field's name which is to be fetched.</param>
        ///
        /// <returns>The field value from the object.</returns>
        internal static object GetInstanceField(Type type, object instance, string fieldName)
        {
            BindingFlags bindFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic
                | BindingFlags.Static;
            FieldInfo field = type.GetField(fieldName, bindFlags);
            return field.GetValue(instance);
        }
    }
}
