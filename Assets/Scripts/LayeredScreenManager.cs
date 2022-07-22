using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PrefsGUI;
using Mirror;

namespace LayeredScreen
{
    public class LayeredScreenManager : NetworkBehaviour
    {
        // Start is called before the first frame update
        [SyncVar]
        public float masterClock = 0.0f;
        public PrefsFloat offsetOfPC1 = new PrefsFloat("OffsetOfPC1");
        public PrefsInt OSCPort = new PrefsInt("OSCPort");
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            if (this.isServer)
            {
                masterClock += 1.0f;
            }
        }
    }
}