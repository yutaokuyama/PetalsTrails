using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Klak.Spout;


namespace LayeredScreen{
public class SpoutSenderWrapper : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private SpoutSender _spoutSender;



    [SerializeField]
    private LayeredScreenManager manager;

    void Start()
    {
        _spoutSender.spoutName = manager.spoutSenderName;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
}