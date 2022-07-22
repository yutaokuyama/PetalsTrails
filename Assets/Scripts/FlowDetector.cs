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

            int row_id = screenId / NUM_COL;
            int col_id = screenId % NUM_COL;

            const int ROW_WIDTH = 5;
            const int INTERBAL_COL = 5;

            return new Vector3(col_id * INTERBAL_COL,0.0f,row_id * ROW_WIDTH);

        }
    } 
}