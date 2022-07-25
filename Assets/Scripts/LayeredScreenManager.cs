using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PrefsGUI;
using Mirror;
using OscJack;


namespace LayeredScreen
{
    public class LayeredScreenManager : NetworkBehaviour
    {
        
        // Start is called before the first frame update
        [SyncVar]
        public float masterClock = 0.0f;
        public int currentFlaggedScreenId = 0;

        public PrefsFloat offsetOfPC1 = new PrefsFloat("OffsetOfPC1");
        public PrefsInt OSCPort = new PrefsInt("OSCPort");

        public PrefsColor trailColor = new PrefsColor("TrailColor");
        public PrefsFloat trailColorIntensity = new PrefsFloat("TrailColorIntensity");

        public PrefsColor leafColor = new PrefsColor("LeafColor");
        public PrefsFloat leafColorIntensity = new PrefsFloat("LeafColorIntensity");

        public PrefsColor petalColor = new PrefsColor("PetalsColor");
        public PrefsFloat petalColorIntensity = new PrefsFloat("PetalColorIntensity");
        public PrefsInt deviceId = new PrefsInt("DeviceId");
        public PrefsString spoutSenderName = new PrefsString("SpoutSenderName","preo_pc0");
        OscServer _server;
        FlowDetector _flowDetector;

        public Vector3 currentPlayerPosition = new Vector3(0.0f, 0.0f, 0.0f);
        const int NUM_ROW = 8;
        const int NUM_COL = 12;

        public Vector3[] viewerPositions = new Vector3[NUM_ROW];
        public float[] viewerVelocityDirectionOfX = new float[NUM_ROW];


        void Start()
        {
            startOSCServer();
            Debug.Assert(deviceId <4);

        }

        void startOSCServer()
        {
            _server = new OscServer(OSCPort.Get()); // Port number
            Debug.LogFormat("Server listen on PORT {0}", OSCPort.Get());
            _server.MessageDispatcher.AddCallback(
               "/point/piece", (string address, OscDataHandle data) =>
    {
        currentFlaggedScreenId = data.GetElementAsInt(0);
        
        Debug.LogFormat("currentFlaggedScreenId:{0}", currentFlaggedScreenId);
    }
    );
        }

        // Update is called once per frame
        void Update()
        {
            if (this.isServer)
            {
                masterClock += 1.0f;
            }
        }

        void OnDisable()
        {
            _server.Dispose();
            _server = null;
        }
    }
}