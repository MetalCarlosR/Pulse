using UnityEngine;
using UnityEngine.AI;

public class MovEnemigo : MonoBehaviour
{
    [SerializeField]
    private Vector2[] posiciones;
    private NavMeshAgent navMesh = null;

    void Start()
    {
        navMesh = GetComponent<NavMeshAgent>();

        navMesh.updateRotation = false;
        navMesh.updateUpAxis = false;
    }
    public void SetPath(Vector3 pos)
    {
        navMesh.SetDestination(pos);
    }
    public void ClearPath()
    {
        navMesh.ResetPath();
    }
}
