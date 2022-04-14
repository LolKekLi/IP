using System.Linq;
using UnityEngine;

public class SimpleAgentController : MonoBehaviour
{
    [SerializeField]
    private UiAgentController uiAgentController = null;

    [SerializeField]
    private GameObject _target = null;

    private AgentBehaviour[] _agentBehaviours = null;

    private void Start()
    {
        _agentBehaviours = GetComponents<AgentBehaviour>();

        uiAgentController.Prepare(_agentBehaviours, this);

        for (int i = 0; i < _agentBehaviours.Length; i++)
        {
            _agentBehaviours[i].Prepare(_target);
            DisableState(_agentBehaviours[i].Type);
        }
    }

    public void DisableState(StateType stateType)
    {
        var firstOrDefault = _agentBehaviours.FirstOrDefault(a => a.Type == stateType);
        firstOrDefault.enabled = false;
    }

    public void ToggleState(StateType stateType)
    {
        var firstOrDefault = _agentBehaviours.FirstOrDefault(a => a.Type == stateType);
        firstOrDefault.enabled = !firstOrDefault.enabled;
    }
}