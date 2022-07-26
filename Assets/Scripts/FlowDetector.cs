using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LayeredScreen
{
    public class FlowDetector : MonoBehaviour
    {
        // Start is called before the first frame update

        public LayeredScreenManager manager;

        private int NUM_ROW = 8;
        private int NUM_COL = 12;
        private const float speed = 24.0f;


        void Start()
        {
            Debug.LogFormat("PlayerPosition:{0}", manager.currentPlayerPosition);
        }
        private void updateViewerPositionInEachRow(int rowId)
        {
            if (manager.isServer)
            {
                manager.viewerPositions[screenIdToRowId(rowId)] += (screenIdToPosition(rowId) - manager.viewerPositions[screenIdToRowId(rowId)]) / speed;
            }
        }

        private void updateViewersPositionInDelayMode(int rowId)
        {
            if (manager.isServer)
            {
                manager.viewerPositions[screenIdToRowId(rowId)] += (screenIdToPosition(rowId) - manager.viewerPositions[screenIdToRowId(rowId)]) / speed;
                for (int i = 1; i < manager.viewerPositions.Count; i++)
                {
                    manager.viewerPositions[i] += ((manager.viewerPositions[0] + calculateOffsetByRowId(i) - manager.viewerPositions[i]) / (i * 16));
                }
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void updatePositions(int screenId)
        {
            if (manager.delayMode)
            {
                updateViewersPositionInDelayMode(screenId);
                return;
            }
            updateViewerPositionInEachRow(screenId);
        }

        private int screenIdToRowId(int screenId)
        {
            return screenId / NUM_COL;
        }

        Vector3 screenIdToPosition(int screenId)
        {
            Debug.Assert(screenId >= 0 && screenId <= 95);

            int rowId = screenIdToRowId(screenId);
            int colId = screenId % NUM_COL;

            float INTERBAL_COL = 8.0f / NUM_COL;

            bool isBackRow = (rowId % 2 == 1);
            Vector3 rowOffset = new Vector3(0.0f, 0.0f, 0.0f);
            rowOffset.x = isBackRow ? 10.0f : 0.0f;

            float offsetX = INTERBAL_COL * 0.5f;
            return new Vector3(colId * INTERBAL_COL - 8.0f / 2.0f + offsetX, 0.0f, 0.0f) + calculateOffsetByRowId(rowId);
        }

        Vector3 calculateOffsetByRowId(int rowId)
        {
            bool isBackRow = (rowId % 2 == 1);
            Vector3 rowOffset = new Vector3(0.0f, 0.0f, 0.0f);
            rowOffset.x = isBackRow ? 10.0f : 0.0f;
            return rowOffset;
        }
    }
}