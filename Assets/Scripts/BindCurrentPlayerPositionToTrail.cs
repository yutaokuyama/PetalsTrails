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
            int rowId = trailEffect.GetInt("RowId");
            
            trailEffect.SetVector3("WalkerOffset", manager.viewerPositions[rowId]);
            trailEffect.SetFloat("VIewerDirection", manager.viewerVelocityDirectionOfX[rowId]);
        }
    }
}
