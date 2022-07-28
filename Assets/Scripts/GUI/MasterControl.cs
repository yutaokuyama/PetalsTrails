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
                manager.trailEmitRate.CreateElement(),
                manager.TrailLifeTime.CreateElement(),
                manager.TrailWidthMinMax.CreateElement(),
                manager.trailMainColor.CreateElement(),
                manager.trailSubColor0.CreateElement(),
                manager.trailSubColor1.CreateElement(),
                manager.trailColorIntensity.CreateElement(),
                manager.petalMainColor.CreateElement(),
                manager.petalSubColor0.CreateElement(),
                manager.petalSubColor1.CreateElement(),
                manager.petalColorIntensity.CreateElement(),
                manager.petalSizeMinMax.CreateElement(),
                manager.leafMainColor.CreateElement(),
                manager.leafSubColor0.CreateElement(),
                manager.leafSubColor1.CreateElement(),
                manager.leafColorIntensity.CreateElement(),
                manager.leafSizeMinMax.CreateElement(),
                manager.deviceId.CreateElement(),
                manager.spoutSenderName.CreateElement(),
                manager.delayMode.CreateElement(),
                manager.aquaOpacity.CreateElement(),
                manager.bloomIntensity.CreateElement(),
                manager.bloomThresh.CreateElement(),
                manager.edgeColor.CreateElement(),
                manager.edgeThresh.CreateElement(),
                manager.edgeContrast.CreateElement(),
                manager.OSCPort.CreateElement(),
                UI.Label(() => $"file path: {PrefsGUI.Kvs.PrefsKvsPathSelector.path}"),
                UI.Button(nameof(Prefs.Save), Prefs.Save),
                UI.Button(nameof(Prefs.DeleteAll), Prefs.DeleteAll)
            );
        }
    }
}