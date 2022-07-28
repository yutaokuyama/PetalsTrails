using LayeredScreen;
using UnityEngine;
using RosettaUI;

using PrefsGUI;

using PrefsGUI.RosettaUI;

namespace LayeredScreen
{
    [RequireComponent(typeof(RosettaUIRoot))]
    public class MenuLauncher : MonoBehaviour
    {
        public Vector2 position;
        [SerializeField]
        private LayeredScreenManager manager;


        private void Start()
        {
            var root = GetComponent<RosettaUIRoot>();
            root.Build(CreateElement());
        }

        Element CreateElement()
        {
            return UI.Window(
                "Controls",
                manager.deviceId.CreateElement(),
                manager.spoutSenderName.CreateElement(),
                manager.delayMode.CreateElement(),
                manager.OSCPort.CreateElement(),
                UI.WindowLauncher<MasterControl>("Object Params"),
                UI.WindowLauncher<PostProcesses>("PostProcess"),
                UI.Label(() => $"file path: {PrefsGUI.Kvs.PrefsKvsPathSelector.path}"),
                UI.Button(nameof(Prefs.Save), Prefs.Save),
                UI.Button(nameof(Prefs.DeleteAll), Prefs.DeleteAll)
            ).SetPosition(position);
        }
    }
}