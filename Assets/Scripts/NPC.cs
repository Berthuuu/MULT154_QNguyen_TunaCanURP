using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{
    private NavMeshAgent agent;
    public GameObject target;
    public GameObject[] huntingSpots;
    private Rigidbody rbBody;
    public BMode mode;

    public enum BMode
    {
        SEEK,
        FLEE,
        PURSUE,
        EVADE,
    }
    // Start is called before the first frame update
    float currentSpeed
    {
        get { return rbBody.velocity.magnitude; }
    }
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rbBody = target.GetComponent<Rigidbody>();
    }

    void Seek(Vector3 location)
    {
        agent.SetDestination(location);
    }

    void Flee (Vector3 location)
    {
        Vector3 fleeVector = location - this.transform.position;
        agent.SetDestination(this.transform.position - fleeVector);
    }

    void Pursue()
    {
        Vector3 targetDir = target.transform.position - this.transform.position;

        float relativeHeading = Vector3.Angle(this.transform.forward, this.transform.TransformVector(target.transform.forward));
        float toTarget = Vector3.Angle(this.transform.forward, this.transform.TransformVector(targetDir));
        if ((toTarget > 90 && relativeHeading < 20) || currentSpeed < 0.01f)
        {
            Seek(target.transform.position);
            return;
        }

        float lookAhead = targetDir.magnitude / (agent.speed + currentSpeed);
        Seek(target.transform.position + target.transform.forward * lookAhead);
    }

    void Evade()
    {
        Vector3 targetDir = target.transform.position - this.transform.position;
        float lookAhead = targetDir.magnitude / (agent.speed + currentSpeed);
        Flee(target.transform.position + target.transform.forward * lookAhead);
    }

    bool CanSeeTarget()
    {
        RaycastHit raycastInfo;
        Vector3 rayToTarget = target.transform.position - this.transform.position;
        if (Physics.Raycast(this.transform.position, rayToTarget, out raycastInfo))
        {
            if (raycastInfo.transform.gameObject.tag == "Player")
                return true;
        }
        return false;
    }

    bool CanTargetSeeMe()
    {
        RaycastHit raycastInfo;
        Vector3 targetFwdWS = target.transform.TransformDirection(target.transform.forward);
        Debug.DrawRay(target.transform.position, targetFwdWS * 10);
        Debug.DrawRay(target.transform.position, target.transform.forward * 10, Color.green);
        if (Physics.Raycast(target.transform.position, target.transform.forward, out raycastInfo))
        {
            if (raycastInfo.transform.gameObject == gameObject)
                return true;
        }
        return false;
    }
    // Update is called once per frame
    void Update()
    {
        switch(mode)
        {
            case BMode.SEEK:
                Seek(target.transform.position);
                break;
            case BMode.PURSUE:
                Pursue();
                break;
            case BMode.FLEE:
                Flee(target.transform.position);
                break;
            case BMode.EVADE:
                Evade();
                break;
        }


    }
}
