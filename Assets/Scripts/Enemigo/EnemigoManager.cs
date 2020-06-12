using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemigoManager : MonoBehaviour
{
    [SerializeField]
    private List<Transform> nodes = new List<Transform>();
    private Enemigo enemigo = null;
    private int index = 0;
    private NavMeshAgent navMesh = null;
    private bool chase = false, patrol = false;

    //void Start()
    //{
    //    if (!GameManager.gmInstance_.save)
    //    {
    //        navMesh = GetComponent<NavMeshAgent>();
    //        navMesh.updateRotation = false;
    //        navMesh.updateUpAxis = false;
    //        if (nodes.Count >= 2)
    //        {
    //            enemigo = GetComponent<Enemigo>();
    //            enemigo.StartDelay();
    //            index = Random.Range(0, nodes.Count);
    //            Patroll();
    //        }
    //        else
    //        {
    //            Debug.LogWarning("Error no nodes found on" + this + ". Destroying" + this);
    //            Destroy(gameObject);
    //        }
    //    }
    //}

    public Enemigo getEnemy()
    {
        return enemigo;
    }

    public void Death()
    {
        enemigo.Death();
    }
    private void Update()
    {
        if (chase)
        {
            transform.up = navMesh.destination - transform.position;
            if (Vector2.Distance(transform.position, navMesh.destination) < 0.5) ClearPath();
        }
        else if (patrol)
        {
            transform.up = navMesh.destination - transform.position;
            if (Vector2.Distance(transform.position, navMesh.destination) < 0.5) NextNode();
        }
        if (transform.rotation.x != 0 || transform.rotation.y != 0) transform.rotation = Quaternion.Euler(0, 0, transform.eulerAngles.z);
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

    public List<Vector3> GetNodes()
    {
        List<Vector3> nodesOut = new List<Vector3>();

        foreach (Transform n in nodes)
        {
            nodesOut.Add(n.position);
        }

        return nodesOut;
    }

    public void LoadEnemy(List<Vector3> nodes_, int count, Enemigo.State state, Enemigo.State prevState)
    {
        List<Transform> newNodes = new List<Transform>(count);


        for (int i = 0; i < count; i++)
        {
            GameObject node = new GameObject();
            node.transform.position = nodes_[i];
            newNodes.Add(node.transform);
        }
        nodes = newNodes;
        GameManager.gmInstance_.AddNodes(nodes, gameObject.name);

        navMesh = GetComponent<NavMeshAgent>();
        navMesh.updateRotation = false;
        navMesh.updateUpAxis = false;
        if (nodes.Count >= 2)
        {
            enemigo = GetComponent<Enemigo>();
            enemigo.StartDelay();
            index = Random.Range(0, nodes.Count);
            Patroll();

            enemigo.SetState(state);
            enemigo.SetPrevState(prevState);
        }
        else
        {
            Debug.LogWarning("Error no nodes found on" + this + ". Destroying" + this);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Puerta door = collision.GetComponent<Puerta>();
        if (door)
        {
            door.MovPuerta(transform);
        }
    }
}
