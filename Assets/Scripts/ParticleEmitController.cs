using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

namespace LayeredScreen
{
    public class ParticleEmitController : MonoBehaviour
    {

        [SerializeField]
        private LayeredScreenManager manager;

        [SerializeField]
        private VisualEffect EffectOfFrontRow;

        [SerializeField]
        private VisualEffect EffectOfBackRow;

        private bool isFrontRowEmitterEnabled = false;
        private bool isBackRowEmitterEnabled = false;
        const int NUM_ROW = 8;

        // Start is called before the first frame update
        void Start()
        {
            EffectOfFrontRow.SendEvent("OnPlayerHide");
            EffectOfBackRow.SendEvent("OnPlayerHide");
        }

        // Update is called once per frame
        void Update()
        {
            updateLocalStateOfFrontRow();
            updateLocalStateOfBackRow();
        }

        private void updateLocalStateOfFrontRow()
        {
            bool isGlobalStateChanged = isFrontRowEmitterEnabled != manager.isRowEmitterEnabled[manager.deviceId * 2];
            if (!isGlobalStateChanged)
            {
                return;
            }

            isFrontRowEmitterEnabled = manager.isRowEmitterEnabled[manager.deviceId * 2];
            EffectOfFrontRow.SendEvent(getEffectEmitterEventName(isFrontRowEmitterEnabled));
        }

        private void updateLocalStateOfBackRow()
        {
            bool isGlobalStateChanged = isBackRowEmitterEnabled != manager.isRowEmitterEnabled[manager.deviceId * 2 + 1];
            if (!isGlobalStateChanged)
            {
                return;
            }

            isBackRowEmitterEnabled = manager.isRowEmitterEnabled[manager.deviceId * 2 + 1];
            EffectOfBackRow.SendEvent(getEffectEmitterEventName(isBackRowEmitterEnabled));
        }

        private string getEffectEmitterEventName(bool isEnabled)
        {
            return isEnabled ? "OnPlayerVisible" : "OnPlayerHide";
        }
    }
}
