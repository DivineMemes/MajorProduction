using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StandardAIPatroling : MonoBehaviour
{
    EnemySeek seeker;
    EnemySoundDetection soundDetect;
    NavMeshAgent agent;
    public Animator cultist;
    public AnimatorControllerParameter[] bools;
    public GameObject player;
    public Transform[] nodes;
    public float searchTimer;
    public float suspectTimer;
    int destinationPoint = 0;
    bool coroutineStarted;
    bool targetSpotted;
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
            cultist.SetBool("walk", true);
            cultist.SetBool("run", false);
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
            {
                NextPosition();
            }
        }

        if (seeker.flashlightSeen)
        {
            foreach(AnimatorControllerParameter boolean in cultist.parameters)
            {
                cultist.SetBool(boolean.name, false);
            }
            cultist.SetBool("idle", true);
            agent.isStopped = true;
        }
        else if(!seeker.flashlightSeen)
        {
            agent.isStopped = false;
        }
        if(seeker.targetSpotted)
        {
            agent.destination = player.transform.position;
            agent.speed = 5;
            cultist.SetBool("run", true);
        }

        if (!seeker.targetSpotted)
        {
            agent.speed = 3;
        }


        if (!seeker.targetSpotted && soundDetect.searchingSound)
        {
            //StartCoroutine(HeardSomething());
            agent.destination = soundDetect.soundPos;
            if(agent.remainingDistance < 0.5f)
            {
                //cultist.SetBool("run", true);
                agent.ResetPath();
                if(!coroutineStarted)
                {
                    coroutineStarted = true;
                    StartCoroutine(LookForTarget());
                }
            }
        }
    }
    IEnumerator LookForTarget()
    {
        yield return new WaitForSeconds(searchTimer);
        soundDetect.searchingSound = false;
        coroutineStarted = false;
    }

    IEnumerator HeardSomething()
    {
        //cultist.SetBool("surprised", true);
        yield return new WaitForSeconds(suspectTimer);
    }
}
