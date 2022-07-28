using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

using Kino.PostProcessing;

namespace LayeredScreen
{
    public class PostProcessController : MonoBehaviour
    {

        [SerializeField]
        private VolumeProfile postProsecces;
        [SerializeField]
        private LayeredScreenManager manager;

        private Bloom bloom;
        private Aqua aqua;
        private Recolor recolor;
        
        // Start is called before the first frame update
        void Start()
        {
            if (bloom == null) postProsecces.TryGet<Bloom>(out bloom);
            if (aqua == null) postProsecces.TryGet<Aqua>(out aqua);
            if (recolor == null) postProsecces.TryGet<Recolor>(out recolor);
        }

        // Update is called once per frame
        void Update()
        {
            bloom.threshold.value = manager.bloomThresh.Get();
            bloom.intensity.value = manager.bloomIntensity.Get();

            aqua.opacity.value = manager.aquaOpacity.Get();

            recolor.edgeThreshold.value = manager.edgeThresh.Get();
            recolor.edgeContrast.value = manager.edgeContrast.Get();
            recolor.edgeColor.value = manager.edgeColor.Get();


        }
    }
}