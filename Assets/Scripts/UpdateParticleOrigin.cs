using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

using OscJack;

namespace MultiLayerScreen
{
    public class UpdateParticleOrigin : MonoBehaviour
    {
        OscServer _server;
        VisualEffect _particleEffect;
        Vector3 _position;

        // Start is called before the first frame update
        void Start()
        {
            _server = new OscServer(9000); // Port number
            _particleEffect = this.GetComponent<VisualEffect>();
            Debug.Log("Server listen on PORT 9000");
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