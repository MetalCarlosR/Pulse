using UnityEngine.AI;
using UnityEngine;

public class algo : MonoBehaviour
{
    NavMeshAgent navMesh;
    [SerializeField]
    private Transform player = null;

    bool algob = true;
    void Start()
    {
        navMesh = GetComponent<NavMeshAgent>();
        navMesh.updateRotation = false;
        navMesh.updateUpAxis = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(algob)   navMesh.SetDestination(player.position);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            algob = false;
            navMesh.ResetPath();
        }
        if (Input.GetKeyUp(KeyCode.Space)) algob = true; 
    }
}
