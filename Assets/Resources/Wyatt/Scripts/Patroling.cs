using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patroling : MonoBehaviour
{
    EnemySeek seeker;
    EnemySoundDetection soundDetect;
    NavMeshAgent agent;
    public GameObject player;
    public Transform[] nodes;
    public float searchTimer;
    int destinationPoint = 0;
    bool coroutineStarted;
    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        seeker = gameObject.GetComponent<EnemySeek>();
        soundDetect = gameObject.GetComponent<EnemySoundDetection>();
    }

    void NextPosition()
    {
        if(nodes.Length == 0)
        {
            return;
        }
        agent.destination = nodes[destinationPoint].position;

        destinationPoint = (destinationPoint + 1) % nodes.Length;
    }
    // Update is called once per frame
    void Update()
    {
        if(!soundDetect.heardSound&&!seeker.targetSpotted)
        {
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
            {
                NextPosition();
            }
        }

        if(seeker.targetSpotted)
        {
            agent.destination = player.transform.position;
        }
        if(soundDetect.searchingSound)
        {
            agent.destination = soundDetect.soundPos.position;
            if(agent.remainingDistance < 0.5f)
            {
                agent.ResetPath();
                if(!coroutineStarted)
                {
                    coroutineStarted = true;
                    StartCoroutine(lookfortarget());
                }
            }
        }
    }
    IEnumerator lookfortarget()
    {

        yield return new WaitForSeconds(searchTimer);
        soundDetect.searchingSound = false;
        coroutineStarted = false;
    }
}
