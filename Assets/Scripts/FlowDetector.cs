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

        }

        // Update is called once per frame
        void Update()
        {
                manager.currentPlayerPosition +=    (screenIdToPosition(manager.currentFlaggedScreenId) -manager.currentPlayerPosition)/24.0f;
                Debug.LogFormat("PlayerPosition:{0}", manager.currentPlayerPosition);
        }


        Vector3 screenIdToPosition(int screenId)
        {
            Debug.Assert(screenId >= 0 && screenId <= 95);

            int rowId = screenId / NUM_COL;
            int colId = screenId % NUM_COL;

            const int ROW_WIDTH = 8;
            float INTERBAL_COL = 8.0f/ NUM_COL;

            Debug.LogFormat("Col_id:{0}", colId);
            Debug.LogFormat("x:{0}", colId);


            return new Vector3(colId * INTERBAL_COL - 8.0f/2.0f, 0.0f, rowId * ROW_WIDTH);

        }
    } 
}