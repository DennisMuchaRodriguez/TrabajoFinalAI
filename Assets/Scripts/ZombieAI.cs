using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class ZombieAI : MonoBehaviour
{
    private BehaviorTree behaviorTree;
    private NavMeshAgent agent;
    private IAEyeBase eye;
    private Health health;

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
                new Action(PursueTarget),
                new Action(Attack)
            }),
            new Action(Wander)
        });

        behaviorTree = new BehaviorTree(root);
    }

    void Update() => behaviorTree?.Update();

    void PursueTarget()
    {
        if (eye.ViewEnemy != null)
            agent.SetDestination(eye.ViewEnemy.transform.position);
    }

    void Attack()
    {
        if (Vector3.Distance(transform.position, eye.ViewEnemy.transform.position) < 2f)
            eye.ViewEnemy.Damage(10, health);
    }

    void Wander()
    {
        if (agent.remainingDistance < 1f)
            agent.SetDestination(RandomNavmeshPoint(10f));
    }

    Vector3 RandomNavmeshPoint(float radius)
    {
        Vector3 randomPoint = Random.insideUnitSphere * radius + transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomPoint, out hit, radius, NavMesh.AllAreas);
        return hit.position;
    }
}