using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;


using RosettaUI;
using PrefsGUI.RosettaUI;

namespace PrefsGUI.Example
{
    public class PrefsGUIExample_Part1 : MonoBehaviour
        ,IElementCreator
    {
        #region Type Define

        public enum EnumSample
        {
            One,
            Two,
            Three
        }

        [Serializable]
        public class CustomClass
        {
            public string name;
            public int intValue;

            public CustomClass()
            {
            }

            public CustomClass(CustomClass other)
            {
                name = other.name;
                intValue = other.intValue;
            }
        }

        #endregion

        // define PrefsParams with key.
        public PrefsBool prefsBool = new PrefsBool("PrefsBool");
        public PrefsInt prefsInt = new PrefsInt("PrefsInt");
        public PrefsFloat prefsFloat = new PrefsFloat("PrefsFloat");
        public PrefsString prefsString = new PrefsString("PrefsString");
        public PrefsParam<EnumSample> prefsEnum = new PrefsParam<EnumSample>("PrefsEnum");
        public PrefsColor prefsColor = new PrefsColor("PrefsColor");
        public PrefsVector2 prefsVector2 = new PrefsVector2("PrefsVector2");
        public PrefsVector3 prefsVector3 = new PrefsVector3("PrefsVector3");
        public PrefsVector4 prefsVector4 = new PrefsVector4("PrefsVector4");
        public PrefsAny<CustomClass> prefsClass = new PrefsAny<CustomClass>("PrefsClass");
        public PrefsList<CustomClass> prefsList = new PrefsList<CustomClass>("PrefsList");

        
        void Update()
        {
            TestImplicitCast();
        }

        protected void TestImplicitCast()
        {
            bool b = prefsBool;
            int i = prefsInt;
            float f = prefsFloat;
            string s = prefsString;
            EnumSample e = prefsEnum;
            Color c = prefsColor;
            Vector2 v2 = prefsVector2;
            Vector3 v3 = prefsVector2;
            Vector4 v4 = prefsVector2;
            v2 = prefsVector3;
            v3 = prefsVector3;
            v4 = prefsVector3;
            v2 = prefsVector4;
            v3 = prefsVector4;
            v4 = prefsVector4;

            CustomClass customClass = prefsClass;
            List<CustomClass> list = prefsList;
        }
        

        public Element CreateElement(LabelElement _)
        {
            return UI.Column(
                prefsBool.CreateElement(),
                prefsInt.CreateElement().RegisterValueChangeCallback(() => Debug.Log("CreateElement: Changed. " + prefsInt.Get())),
                prefsFloat.CreateElement(),
                prefsFloat.CreateSlider(),
                prefsString.CreateElement(),
                prefsEnum.CreateElement(),
                prefsColor.CreateElement(),
                prefsVector2.CreateElement(),
                prefsVector2.CreateSlider(),
                prefsVector3.CreateElement(),
                prefsVector3.CreateSlider(),
                prefsVector4.CreateElement(),
                prefsVector4.CreateSlider(),
                prefsClass.CreateElement(),
                prefsList.CreateElement()
            );
        }
    }
}