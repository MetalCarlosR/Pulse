using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovEnemigo : MonoBehaviour
{
    [SerializeField]
    private List<Transform> nodes = new List<Transform>();
    private int index = 0;
    private NavMeshAgent navMesh = null;
    private bool chase = false, patrol = false;

    void Start()
    {
        navMesh = GetComponent<NavMeshAgent>();
        navMesh.updateRotation = false;
        navMesh.updateUpAxis = false;
        if (nodes.Count >= 2)
        {
            index = Random.Range(0, nodes.Count);
            Patroll();
        }
        else
        {
            Debug.LogWarning("Error no nodes found on" + this);
            gameObject.SetActive(false);
        }

    }

    private void Update()
    {
        if (chase)
        {
            transform.up = navMesh.destination - transform.position;
            if (Vector2.Distance(transform.position, navMesh.destination) < 2) ClearPath();
        }
        else if (patrol)
        {
            transform.up = navMesh.destination - transform.position;
            if (Vector2.Distance(transform.position, navMesh.destination) < 2) NextNode();
        }
    }
    public void ChasePlayer(Vector3 pos)
    {
        chase = true;
        patrol = false;
        navMesh.SetDestination(pos);
    }
    public void Patroll()
    {
        patrol = true;
        chase = false;
        navMesh.SetDestination(nodes[index].position);
    }
    private void NextNode()
    {
        index++;
        if (index == nodes.Count) index = 0;
        navMesh.SetDestination(nodes[index].position);
    }
    public void ClearPath()
    {
        chase = false;
        patrol = false;
        navMesh.ResetPath();
    }
}
