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
        public PrefsString spoutSenderName = new PrefsString("SpoutSenderName", "preo_pc0");
        public PrefsBool delayMode = new PrefsBool("DelayMode", false);
        OscServer _server;
        FlowDetector _flowDetector;

        public Vector3 currentPlayerPosition = new Vector3(0.0f, 0.0f, 0.0f);
        const int NUM_ROW = 8;
        const int NUM_COL = 12;

        //public Vector3[] viewerPositions = new Vector3[NUM_ROW];
        public SyncList<Vector3> viewerPositions = new SyncList<Vector3>();
        public SyncList<bool> isRowEmitterEnabled = new SyncList<bool>();
        private float[] elapsedTimesFromLastViewerAppeared = new float[NUM_ROW];
        public float[] viewerVelocityDirectionOfX = new float[NUM_ROW];

        [SerializeField]
        private float sleepTimeInSec = 1.5f;
        private void Awake()
        {
            for (int i = 0; i < NUM_ROW; i++)
            {
                viewerPositions.Add(new Vector3(0.0f, 0.0f, 0.0f));
                isRowEmitterEnabled.Add(false);
                elapsedTimesFromLastViewerAppeared[i] = 0.0f;
            }
        }

        void Start()
        {
            if (this.isServer)
            {
                startOSCServer();
            }
            Debug.Assert(deviceId < 4);

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

        void updateMasterEffectState()
        {
            int currentHandleRowId = convertScreenIDToRowId(
                currentFlaggedScreenId);
            for (int row = 0; row < NUM_ROW; row++)
            {
                if (currentHandleRowId == row)
                {
                    if (!isRowEmitterEnabled[row])
                    {
                        isRowEmitterEnabled[row] = true;
                    }
                    elapsedTimesFromLastViewerAppeared[row] = 0.0f;
                }
                else
                {
                    elapsedTimesFromLastViewerAppeared[row] += 1.0f / 60.0f;
                    if (elapsedTimesFromLastViewerAppeared[row] > sleepTimeInSec)
                    {
                        isRowEmitterEnabled[row] = false;
                    }
                }
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (this.isServer)
            {
                updateMasterEffectState();
            }
        }

        void OnDisable()
        {
            if (_server != null)
            {
                _server.Dispose();
                _server = null;
            }
        }

        private int convertScreenIDToRowId(int screenId)
        {
            const int NUM_COL = 12;
            return screenId / NUM_COL;
        }
    }
}