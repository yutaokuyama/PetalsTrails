using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;


namespace LayeredScreen
{
    public class BindCurrentPlayerPositionToTrail : MonoBehaviour
    {

        public VisualEffect trailEffect;

        [SerializeField]
        private LayeredScreenManager manager;
        // Start is called before the first frame update
        void Start()
        {
            // trailEffect = this.GetComponent<VisualEffect>();
        }

        // Update is called once per frame
        void Update()
        {
            int rowId = trailEffect.GetInt("RowId") + manager.deviceId * 2;
            trailEffect.SetGradient("TrailGradient", manager.trailColorGradient);
            trailEffect.SetGradient("LeafGradient", manager.leafColorGradient) ;
            trailEffect.SetGradient("PetalGradient", manager.petalColorGradient);
            trailEffect.SetVector3("WalkerOffset", manager.viewerPositions[rowId]);
            trailEffect.SetVector3("SlowerWalkerOffset", manager.slowerViewerPositions[rowId]);
            trailEffect.SetFloat("TrailColorIntensity", manager.trailColorIntensity.Get());
            trailEffect.SetFloat("LeafColorIntensity", manager.leafColorIntensity.Get());
            trailEffect.SetFloat("PetalColorIntensity", manager.petalColorIntensity.Get());
            trailEffect.SetVector2("PetalSizeMinMax", manager.petalSizeMinMax.Get());
            trailEffect.SetVector2("LeafSizeMinMax", manager.leafSizeMinMax.Get());
            trailEffect.SetVector2("TrailWidthMinMax", manager.TrailWidthMinMax.Get());
            trailEffect.SetInt("TrailEmitRate", manager.trailEmitRate.Get());
            trailEffect.SetVector2("TrailLifeTimeMinMax", manager.TrailLifeTime.Get());
        }
    }
}
