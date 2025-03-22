using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class SnakeEnemyController : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    public int wanderRadius = 10;
    public int wanderOffset = 10;


    public Vector3 target;
    public float maxVelocity;
    public float turnSpeed;
    public float maxDistance = 10;
    public float maxXDistance;
    public float maxZDistance;
    public float destinationTime;
    private float maxDestinationTime = 3;


    private Quaternion rotation;


    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        if (target == new Vector3(0, 0, 0) || Vector3.Distance(target, gameObject.transform.position) < maxDistance)
        {
            Wander();
        }

        Seek();
    }

    // Update is called once per frame
    void Update()
    {

        // condición de parada
        if (Vector3.Distance(target, gameObject.transform.position) < maxDistance || destinationTime > maxDestinationTime)
        {
            Wander();
        }


        destinationTime += Time.deltaTime;

        Seek();
        // actualización efectivo del movimiento
        gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, rotation, Time.deltaTime * turnSpeed);
        gameObject.transform.position += gameObject.transform.forward.normalized * maxVelocity * Time.deltaTime;
    }

    void Wander()
    {
        Vector3 localTarget = UnityEngine.Random.insideUnitSphere;
        localTarget.y = 0f;
        localTarget.Normalize();
        localTarget *= wanderRadius;
        localTarget += new Vector3(0, 0, wanderOffset);

        target = transform.TransformPoint(localTarget);
        destinationTime = 0;
    }

    void Seek()
    {
        Vector3 direction = target - transform.position;
        direction.y = 0f;    // (x, z): position in the floor

        Vector3 movement = direction.normalized * 10;

        float angle = Mathf.Rad2Deg * Mathf.Atan2(movement.x, movement.z);
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.up);  // up = y
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction),
                                              Time.deltaTime * 5);
        transform.position += transform.forward.normalized * 10 * Time.deltaTime;

        navMeshAgent.destination = target;


    }


}
