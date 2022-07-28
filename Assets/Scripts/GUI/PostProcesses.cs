using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PrefsGUI;

using RosettaUI;
using PrefsGUI.RosettaUI;
namespace LayeredScreen
{
    public class PostProcesses : MonoBehaviour, IElementCreator
    {
        [SerializeField]
        private LayeredScreenManager manager;


        public Element CreateElement(LabelElement _)
        {
            return UI.Column(
                manager.aquaOpacity.CreateElement(),
                manager.bloomIntensity.CreateElement(),
                manager.bloomThresh.CreateElement(),
                manager.edgeColor.CreateElement(),
                manager.edgeThresh.CreateElement(),
                manager.edgeContrast.CreateElement()
            );
        }
    }
}