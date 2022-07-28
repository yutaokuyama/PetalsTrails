using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PrefsGUI;
using Mirror;
using OscJack;
using System.Linq;

namespace LayeredScreen
{

    
    public class LayeredScreenManager : NetworkBehaviour
    {

        // Start is called before the first frame update
        public int[] flaggedScreenIds = new int[256];
        private int receivedScreenIdNumInOneRenderLoop = 0;
        private int OSCPort = 6666;

        public PrefsColor trailMainColor = new PrefsColor("TrailMainColor");
        public PrefsColor trailSubColor0 = new PrefsColor("TrailSubColor0");
        public PrefsColor trailSubColor1 = new PrefsColor("TrailSubColor1");

        public Gradient trailColorGradient = new Gradient();
        private GradientColorKey[] trailColorKey;
        private GradientAlphaKey[] trailColorAlphaKey;

        public PrefsFloat trailColorIntensity = new PrefsFloat("TrailColorIntensity");

        public PrefsColor leafColor = new PrefsColor("LeafColor");
        public PrefsFloat leafColorIntensity = new PrefsFloat("LeafColorIntensity");

        public PrefsColor petalColor = new PrefsColor("PetalsColor");
        public PrefsFloat petalColorIntensity = new PrefsFloat("PetalColorIntensity");
        public PrefsInt deviceId = new PrefsInt("DeviceId");
        public PrefsString spoutSenderName = new PrefsString("SpoutSenderName", "preo");
        public PrefsBool delayMode = new PrefsBool("DelayMode", false);

        public PrefsVector2 TrailLifeTime = new PrefsVector2("TrailLifeTimeRange", new Vector2(0.5f, 4.0f));
        public PrefsFloat TrailWidth = new PrefsFloat("TrailWidth",0.03f);
        public PrefsInt NumTrail = new PrefsInt("NumTrail", 200);

        public PrefsFloat bloomIntensity = new PrefsFloat("BloomIntensity", 0.9f);
        public PrefsFloat bloomThresh = new PrefsFloat("BloomThresh", 0.9f);

        public PrefsFloat aquaOpacity = new PrefsFloat("AquaOpacity", 0.4f);

        
        
        
        OscServer _server;
        [SerializeField]
        private FlowDetector _flowDetector;

        public Vector3 currentPlayerPosition = new Vector3(0.0f, 0.0f, 0.0f);
        const int NUM_ROW = 8;
        const int NUM_COL = 12;

        public SyncList<Vector3> viewerPositions = new SyncList<Vector3>();
        public SyncList<Vector3> slowerViewerPositions = new SyncList<Vector3>();

        public SyncList<bool> isRowEmitterEnabled = new SyncList<bool>();
        private float[] elapsedTimesFromLastViewerAppeared = new float[NUM_ROW];
        public float[] viewerVelocityDirectionOfX = new float[NUM_ROW];

        private PrefsFloat sleepTimeInSec = new PrefsFloat("effectSleepTimeInSec", 1.5f);
         private void udpateTrailColorGradient()
        {
            // Populate the color keys at the relative time 0 and 1 (0 and 100%)
            trailColorKey = new GradientColorKey[5];
            trailColorKey[0].color = trailMainColor.Get();
            trailColorKey[0].time = 0.5f;
            trailColorKey[1].color = trailSubColor0.Get();
            trailColorKey[1].time = 0.0f;
            trailColorKey[2].color = trailSubColor1.Get();
            trailColorKey[2].time = 1.0f;
            trailColorKey[3].color = trailMainColor.Get();
            trailColorKey[3].time = 0.75f;
            trailColorKey[4].color = trailMainColor.Get();
            trailColorKey[4].time = 0.25f;
            // Populate the alpha  keys at relative time 0 and 1  (0 and 100%)
            trailColorAlphaKey = new GradientAlphaKey[3];
            trailColorAlphaKey[0].alpha = 1.0f;
            trailColorAlphaKey[0].time = 0.0f;
            trailColorAlphaKey[1].alpha = 1.0f;
            trailColorAlphaKey[1].time = 1.0f;
            trailColorAlphaKey[2].alpha = 1.0f;
            trailColorAlphaKey[2].time = 0.5f;

            trailColorGradient.SetKeys(trailColorKey, trailColorAlphaKey);

        }

        private void Awake()
        {
            for (int i = 0; i < NUM_ROW; i++)
            {
                viewerPositions.Add(new Vector3(0.0f, 0.0f, 0.0f));
                slowerViewerPositions.Add(new Vector3(0.0f, 0.0f, 0.0f));

                isRowEmitterEnabled.Add(false);
                elapsedTimesFromLastViewerAppeared[i] = 0.0f;
            }
            udpateTrailColorGradient();

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
            _server = new OscServer(OSCPort); // Port number
            Debug.LogFormat("Server listen on PORT {0}", OSCPort);
            _server.MessageDispatcher.AddCallback(
               "/point/piece", (string address, OscDataHandle data) =>
    {
        flaggedScreenIds[receivedScreenIdNumInOneRenderLoop] = data.GetElementAsInt(0);
        receivedScreenIdNumInOneRenderLoop = receivedScreenIdNumInOneRenderLoop + 1;
    }
    );
        }

        void updateMasterEffectState(int screenId)
        {
            int currentHandleRowId = convertScreenIDToRowId(screenId);
            if (delayMode)
            {
                updateMasterEffectStateInDelayMode(currentHandleRowId);
            }
            else
            {
                updateMasterEffectStateInEachRowMode(currentHandleRowId);
            }
        }


        void updateMasterEffectStateInDelayMode(int currentHandleRowId)
        {
            bool isCurrentHandleRowIdInFirstRow = currentHandleRowId == 0;

            if (isCurrentHandleRowIdInFirstRow)
            {
                if (!isRowEmitterEnabled[0])
                {
                    isRowEmitterEnabled[0] = true;
                }
                elapsedTimesFromLastViewerAppeared[0] = 0.0f;
            }
            else
            {
                elapsedTimesFromLastViewerAppeared[0] += 1.0f / 60.0f;
                if (elapsedTimesFromLastViewerAppeared[0] > sleepTimeInSec.Get())
                {
                    isRowEmitterEnabled[0] = false;
                }
            }

            for (int row = 1; row < NUM_ROW; row++)
            {
                isRowEmitterEnabled[row] = isRowEmitterEnabled[0];
            }
        }

        void updateMasterEffectStateInEachRowMode(int currentHandleRowId)
        {
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
                    if (elapsedTimesFromLastViewerAppeared[row] > sleepTimeInSec.Get())
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
                // var uniqueArray = flaggedScreenIds.Distinct();
                for(int i = 0;i<receivedScreenIdNumInOneRenderLoop;i++){
                    updateMasterEffectState(flaggedScreenIds[i]);
                    _flowDetector.updatePositions(flaggedScreenIds[i]);
                }
                receivedScreenIdNumInOneRenderLoop = 0;
            }
            //XXX:adhoc work around
            udpateTrailColorGradient();
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