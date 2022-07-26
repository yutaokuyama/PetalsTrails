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

        private float elapsedTimeFromLastViewerAppearInFrontRow = 0.0f;
        private float elapsedTimeFromLastViewerAppearInBackRow = 0.0f;


        [SerializeField]
        private float sleepTimeInSec = 1.5f;


        // Start is called before the first frame update
        void Start()
        {
            EffectOfFrontRow.SendEvent("OnPlayerHide");
            EffectOfBackRow.SendEvent("OnPlayerHide");

        }

        // Update is called once per frame
        void Update()
        {
            if (isViewerInFronwRow(manager.currentFlaggedScreenId))
            {
                if (!isFrontRowEmitterEnabled)
                {
                    EffectOfFrontRow.SendEvent("OnPlayerVisible");
                    isFrontRowEmitterEnabled = true;
                }
                elapsedTimeFromLastViewerAppearInFrontRow = 0.0f;
            }
            else
            {
                elapsedTimeFromLastViewerAppearInFrontRow += 1 / 60.0f;
                if (elapsedTimeFromLastViewerAppearInFrontRow > sleepTimeInSec)
                {
                    EffectOfFrontRow.SendEvent("OnPlayerHide");
                    isFrontRowEmitterEnabled = false;
                }
            }


            if (isViewerInBackRow(manager.currentFlaggedScreenId))
            {
                if (!isBackRowEmitterEnabled)
                {
                    EffectOfBackRow.SendEvent("OnPlayerVisible");
                    isBackRowEmitterEnabled = true;
                }
                elapsedTimeFromLastViewerAppearInBackRow = 0.0f;
            }
            else
            {
                elapsedTimeFromLastViewerAppearInBackRow += 1 / 60.0f;
                if(elapsedTimeFromLastViewerAppearInBackRow > sleepTimeInSec)
                {
                    EffectOfBackRow.SendEvent("OnPlayerHide");
                    isBackRowEmitterEnabled = false;
                }
            }
        }

        bool shuoldHandleScreenIDOnThisDevice(int screenID)
        {
            const int NUM_COL = 12;
            int rowId = screenID / NUM_COL;
            return (manager.deviceId * 2 == rowId) || ((manager.deviceId * 2 + 1) == rowId);
        }

        bool isViewerInFronwRow(int screenId)
        {
            int rowId = convertScreenIDToRowId(screenId);
            return rowId == (manager.deviceId * 2);
        }

        bool isViewerInBackRow(int screenId)
        {
            int rowId = convertScreenIDToRowId(screenId);
            return rowId == ((manager.deviceId * 2) +1);
        }

        private int convertScreenIDToRowId(int screenId)
        {
            const int NUM_COL = 12;
            return screenId / NUM_COL;
        }
    }
}
