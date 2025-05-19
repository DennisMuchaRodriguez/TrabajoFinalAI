using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class VillagerAI : MonoBehaviour
{
    private BehaviorTree behaviorTree;
    private NavMeshAgent agent;
    private IAEyeBase eye;
    private Health health;
    public Transform safeZone;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        eye = GetComponent<IAEyeBase>();
        health = GetComponent<Health>();

        var root = new Selector(new List<BTNode>
        {
            new Sequence(new List<BTNode>
            {
                new Condition(() => eye.ViewEnemy != null),
                new Action(Flee)
            }),
            new Action(Wander)
        });

        behaviorTree = new BehaviorTree(root);
    }

    void Update() => behaviorTree?.Update();

    void Flee()
    {
        if (eye.ViewEnemy != null)
        {
            Vector3 fleeDirection = transform.position - eye.ViewEnemy.transform.position;
            agent.SetDestination(transform.position + fleeDirection.normalized * 10f);
        }
    }

    void Wander()
    {
        if (agent.remainingDistance < 1f)
            agent.SetDestination(RandomNavmeshPoint(5f));
    }

    Vector3 RandomNavmeshPoint(float radius)
    {
        Vector3 randomPoint = Random.insideUnitSphere * radius + transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomPoint, out hit, radius, NavMesh.AllAreas);
        return hit.position;
    }
}