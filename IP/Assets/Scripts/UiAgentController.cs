using UnityEngine;

public class UiAgentController : MonoBehaviour
{
    [SerializeField]
    private ControlButton _controlButtonPrefab = null;

    [SerializeField]
    private Transform _buttonGroup = null;

    public void Prepare(AgentBehaviour[] agentBehaviours, SimpleAgentController simpleAgentController)
    {
        for (int i = 0; i < agentBehaviours.Length; i++)
        {
            var controlButton = Instantiate(_controlButtonPrefab, _buttonGroup);

            var stateType = agentBehaviours[i].Type;
            
            controlButton.Prepare(agentBehaviours[i], () =>
            {
                simpleAgentController.ToggleState(stateType);
            });
        }
    }
}