using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;
using PrefsGUI;

using RosettaUI;
using PrefsGUI.RosettaUI;


namespace LayeredScreen
{
    public class MasterControl : MonoBehaviour
        ,IElementCreator
    {
        public LayeredScreenManager manager; 

        void Update()
        {
            
        }

        public Element CreateElement(LabelElement _)
        {
            return UI.Column(
                manager.masterClock.CreateElement(),
                manager.OSCPort.CreateElement(),
                manager.offsetOfPC1.CreateElement(),
                UI.Label(() => $"file path: {PrefsGUI.Kvs.PrefsKvsPathSelector.path}"),
                UI.Button(nameof(Prefs.Save), Prefs.Save),
                UI.Button(nameof(Prefs.DeleteAll), Prefs.DeleteAll)
            );
        }
    }
}