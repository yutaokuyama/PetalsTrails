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
                manager.NumTrail.CreateElement(),
                manager.TrailLifeTime.CreateElement(),
                manager.TrailWidth.CreateElement(),
                manager.trailColor.CreateElement(),
                manager.trailColorIntensity.CreateElement(),
                manager.petalColor.CreateElement(),
                manager.petalColorIntensity.CreateElement(),
                manager.leafColor.CreateElement(),
                manager.leafColorIntensity.CreateElement(),
                manager.deviceId.CreateElement(),
                manager.spoutSenderName.CreateElement(),
                manager.delayMode.CreateElement(),
                UI.Label(() => $"file path: {PrefsGUI.Kvs.PrefsKvsPathSelector.path}"),
                UI.Button(nameof(Prefs.Save), Prefs.Save),
                UI.Button(nameof(Prefs.DeleteAll), Prefs.DeleteAll)
            );
        }
    }
}