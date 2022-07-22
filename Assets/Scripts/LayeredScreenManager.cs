using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PrefsGUI;

namespace LayeredScreen{
public class LayeredScreenManager : MonoBehaviour
{
    // Start is called before the first frame update

    public PrefsFloat masterClock = new PrefsFloat("MasterClock");
    public PrefsFloat offsetOfPC1 = new PrefsFloat("OffsetOfPC1");
    public PrefsInt OSCPort = new PrefsInt("OSCPort");
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         masterClock.Set(masterClock + 0.01f);
   }
}
}