using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class StandardAIPatroling : MonoBehaviour
{
    //this script looks like pure spaghetti 
    EnemySeek seeker;
    EnemySoundDetection soundDetect;

    NavMeshAgent agent;

    public Animator cultist;
    public AnimatorControllerParameter[] bools;

    public AudioSource source;
    public AudioClip[] sounds;

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

    bool isInvestigating = false;

    public float time;

    public bool startedFlashlight = false;

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
        /*
        if (lookAtPlayer)
        {
            if (!playerTransformStored)
            {
                playerLast = player.transform;
                playerTransformStored = true;
            }
            if (playerTransformStored)
            {
                agent.destination = playerLast.position;
                if (agent.remainingDistance < 1f)
                {
                    StartCoroutine(LookForTarget());
                    flashLightOn = false;
                }
            }
        }
        if (seeker.targetSpotted)
        {
            lookAtPlayer = false;
            flashLightOn = false;
            agent.destination = player.transform.position;
            agent.speed = 5;
            cultist.SetBool("run", true);
        }
        else if (!seeker.targetSpotted)
        {
            agent.speed = 3;
        }

        if (!seeker.targetSpotted && soundDetect.searchingSound)
        {
            agent.destination = soundDetect.soundPos;
            if (agent.remainingDistance < 0.5f)
            {
                agent.ResetPath();
                if (!coroutineStarted)
                {
                    coroutineStarted = true;
                    StartCoroutine(LookForTarget());
                }
            }
        }
        */

        // chasing
        if (seeker.targetSpotted)
        {
            agent.speed = 5;
            cultist.SetBool("run", true);
            agent.destination = player.transform.position;
        }
        else
        {
            agent.speed = 3;
        }

        isInvestigating = seeker.flashLightOn || soundDetect.searchingSound || isInvestigating;

        // searching
        if ((isInvestigating || seeker.flashLightOn || soundDetect.searchingSound) && !seeker.targetSpotted)
        {
            // start timer on how long flashlight is exposed on AI
            if (seeker.flashLightOn)
            {
                //Debug.Log("[AI] Now going toward flashlight source...");
                agent.destination = player.transform.position;

                if (!startedFlashlight)
                {
                    startedFlashlight = true;
                    StartCoroutine(FlashlightExposure());
                }
            }
            else if (soundDetect.searchingSound)
            {
                //Debug.Log("[AI] Now going toward sound source...");
                agent.destination = soundDetect.soundPos;
            }

            // start the alerted animation
            if (!seenFlashlight)
            {
                StartCoroutine(AlertAnimation());
            }


            //Debug.LogFormat("[AI] Dist remaining: {0}", agent.remainingDistance);
            if (agent.remainingDistance < 0.5f)
            {
              //  Debug.Log("[AI] Reached location of interest.");
                agent.ResetPath();
                if (!coroutineStarted)
                {
                //    Debug.Log("[AI] Starting to look around for the player.");
                    coroutineStarted = true;
                    StartCoroutine(LookForTarget());
                }

                
            }
        }
        else if (!seeker.flashLightOn)
        {
            agent.isStopped = false;
            time = 0;
        }

        // patrolling
        if (!soundDetect.heardSound && !seeker.targetSpotted && !isInvestigating)
        {
            if (!source.isPlaying)
            {
                PlaySound();
            }
            cultist.SetBool("walk", true);
            cultist.SetBool("run", false);
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
            {
                //Debug.Log("[AI] Going to next patrol point.");
                NextPosition();
            }
        }
    }


    void PlaySound()
    {
        source.clip = sounds[Random.Range(0, sounds.Length)];
        source.PlayOneShot(source.clip);
    }

    IEnumerator LookForTarget()
    {
        agent.isStopped = true;
        cultist.SetBool("investigate", true);
        yield return new WaitForSeconds(2.21f);
        cultist.SetBool("investigate", false);
        isInvestigating = false;
        agent.isStopped = false;
        seenFlashlight = false;
        soundDetect.searchingSound = false;
        coroutineStarted = false;
    }

    IEnumerator AlertAnimation()
    {
        agent.isStopped = true;
        cultist.SetBool("idle", false);
        cultist.SetBool("alert", true);
        seenFlashlight = true;
        yield return new WaitForSeconds(.25f);
        agent.isStopped = false;
        cultist.SetBool("alert", false);
        cultist.SetBool("walk", true);
        //lookAtPlayer = true;
    }

    IEnumerator FlashlightExposure()
    {
        while(seeker.flashLightOn)
        {
            time += Time.deltaTime;

            if (time >= 5.0f)
            {
                seeker.viewRad = seeker.FlashlightViewRad;
                time = 0;
                break;
            }
            yield return null;
        }

        startedFlashlight = false;
    }
}
