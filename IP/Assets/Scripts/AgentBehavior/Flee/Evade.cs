using UnityEngine;

public class Evade : Flee
{
    public  float maxPrediction;
    
    private GameObject targetAux;
    private Agent targetAgent;

    public override StateType Type => StateType.Evade;
    
    public override void Prepare(GameObject agent)
    {
        base.Prepare(agent);
        
        targetAgent = target.GetComponent<Agent>();
        targetAux = target;
        target = new GameObject();
    }

    private void OnDestroy()
    {
        Destroy(targetAux);
    }

    public override Steering GetSteering()
    {
        var direction = targetAux.transform.position - transform.position;
        var distance = direction.magnitude;
        var speed = _agent.velocity.magnitude;
        var prediction = 0f;

        if (speed <= distance / maxPrediction)
        {
            prediction = maxPrediction;
        }
        else
        {
            prediction = distance / speed;
        }

        var position = target.transform.position;
        position = targetAux.transform.position;
        position += targetAgent.velocity * prediction;
        target.transform.position = position;

        return base.GetSteering();
    }
}