using LayeredScreen;
using UnityEngine;
using RosettaUI;

namespace LayeredScreen
{
    [RequireComponent(typeof(RosettaUIRoot))]
    public class MenuLauncher : MonoBehaviour
    {
        public Vector2 position;
        
        private void Start()
        {
            var root = GetComponent<RosettaUIRoot>();
            root.Build(CreateElement());
        }

        Element CreateElement()
        {
            Debug.Log("Create Element");
            return UI.Window(
                "Controls",
                UI.WindowLauncher<MasterControl>("MasterControl")
            ).SetPosition(position);
        }
    }
}