using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SocketManager : MonoBehaviour
{
    [System.Serializable]
    public class SwitchSocketPair
    {
        public XRSocketInteractor socket;
        public XRBaseInteractable switchInteractable;
    }

    public SwitchSocketPair[] switchSocketPairs;

    private void Start()
    {
        foreach (var pair in switchSocketPairs)
        {
            pair.socket.selectEntered.AddListener((args) => OnSwitchPlaced(args, pair));
        }
    }

    private void OnSwitchPlaced(SelectEnterEventArgs args, SwitchSocketPair pair)
    {
        // Ensure the correct switch is placed in the correct socket
        if (args.interactable == pair.switchInteractable)
        {
            // Activate the switch's SimpleInteractable component
            var simpleInteractable = pair.switchInteractable.GetComponent<XRSimpleInteractable>();
            if (simpleInteractable != null)
            {
                simpleInteractable.enabled = true;
            }
        }
    }

    private void OnDestroy()
    {
        foreach (var pair in switchSocketPairs)
        {
            pair.socket.selectEntered.RemoveAllListeners();
        }
    }
}
