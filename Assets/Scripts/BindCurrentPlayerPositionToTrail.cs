using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;


namespace LayeredScreen
{
    public class BindCurrentPlayerPositionToTrail : MonoBehaviour
    {

        VisualEffect trailEffect;

        [SerializeField]
        private LayeredScreenManager manager;
        // Start is called before the first frame update
        void Start()
        {
            trailEffect = this.GetComponent<VisualEffect>();
        }

        // Update is called once per frame
        void Update()
        {
            trailEffect.SetVector3("ParticleOrigin", manager.currentPlayerPosition);
        }
    }
}
