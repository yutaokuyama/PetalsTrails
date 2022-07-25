using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LayeredScreen {
    public class FlowDetector : MonoBehaviour
    {
        // Start is called before the first frame update

        public LayeredScreenManager manager;

        private int NUM_ROW = 8;
        private int NUM_COL = 12;


        
        
        void Start()
        {
                Debug.LogFormat("PlayerPosition:{0}", manager.currentPlayerPosition);
        }

        // Update is called once per frame
        void Update()
        {
            const float speed = 24.0f;
          manager.viewerPositions[screenIdToRowId(manager.currentFlaggedScreenId)] += (screenIdToPosition(manager.currentFlaggedScreenId) - manager.viewerPositions[screenIdToRowId(manager.currentFlaggedScreenId)]) / speed;
            manager.viewerVelocityDirectionOfX[screenIdToRowId(manager.currentFlaggedScreenId)]  = -(screenIdToPosition(manager.currentFlaggedScreenId) - manager.viewerPositions[screenIdToRowId(manager.currentFlaggedScreenId)]).x / speed;
        }

        private int screenIdToRowId(int screenId)
        {
            return screenId / NUM_COL;
        }

        Vector3 screenIdToPosition(int screenId)
        {
            Debug.Assert(screenId >= 0 && screenId <= 95);

            int rowId = screenId / NUM_COL;
            int colId = screenId % NUM_COL;

            const int ROW_WIDTH = 8;
            float INTERBAL_COL = 8.0f/ NUM_COL;

            bool isBackRow = (rowId % 2 == 1);
            Vector3 rowOffset = new Vector3(0.0f,0.0f,0.0f);
            rowOffset.x = isBackRow?10.0f:0.0f;


            return new Vector3(colId * INTERBAL_COL - 8.0f/2.0f, 0.0f, 0.0f) + rowOffset;

        }
    } 
}