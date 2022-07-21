using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

using OscJack;

namespace LayeredScreen
{
    public class UpdateParticleOrigin : MonoBehaviour
    {
        OscServer _server;
        VisualEffect _particleEffect;
        Vector3 _position;
        public LayeredScreenManager  manager;

        // Start is called before the first frame update
        void Start()
        {
            _server = new OscServer(manager.OSCPort.Get()); // Port number
            _particleEffect = this.GetComponent<VisualEffect>();
            Debug.LogFormat("Server listen on PORT {0}", manager.OSCPort.Get());
            _position = new Vector3(0.0f,0.0f,0.0f);
            _server.MessageDispatcher.AddCallback(
                "/test", // OSC address
                (string address, OscDataHandle data) =>
                {
                    _position = new Vector3(data.GetElementAsFloat(0), data.GetElementAsFloat(1), data.GetElementAsFloat(2));
                    Debug.Log("Received");
                }
            );

        }
        // Update is called once per frame
        void Update()
        {
             _particleEffect.SetVector3("ParticleOrigin", _position);

        }

        void OnDestroy()
        {
            _server?.Dispose();
            _server = null;
        }
    }
}