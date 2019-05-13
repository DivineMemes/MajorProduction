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
    Transform playerLast;

    public float searchTimer;
    public float suspectTimer;

    int destinationPoint = 0;

    public bool seenFlashlight;
    bool coroutineStarted;
    bool targetSpotted;
    bool lookAtPlayer;
    bool playerTransformStored;
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
            if (!seenFlashlight)
            {
                StartCoroutine(AlertAnimation());
            }
            if(lookAtPlayer)
            {
                transform.LookAt(player.transform);
                agent.isStopped = false;
                if(!playerTransformStored)
                {
                    playerLast = player.transform;
                    playerTransformStored = true;
                }
                if(playerTransformStored)
                {
                    agent.destination = playerLast.position;
                }
            }
            
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

        if(!seeker.targetSpotted)
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
    IEnumerator AlertAnimation()
    {
        foreach (AnimatorControllerParameter boolean in cultist.parameters)
        {
            cultist.SetBool(boolean.name, false);
        }
        cultist.SetBool("idle", false);
        cultist.SetBool("alert", true);
        seenFlashlight = true;
        yield return new WaitForSeconds(1.958f);
        cultist.SetBool("alert", false);
        cultist.SetBool("walk", true);
        lookAtPlayer = true;
        
    }
}
