using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    [HideInInspector]
    public NavMeshAgent agent;
    public Vector3 forward;
    public AudioSource audioSource;
    public List<AudioClip> footStepSounds;
    public Transform flashlight;
    public Animator animator;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(FootstepSoundsRoutine());
    }

    private void Update()
    {
        if (agent.velocity.magnitude > 0.01f)
        {
            forward = agent.velocity.normalized;
        }
        if (flashlight)
            flashlight.transform.LookAt(transform.position + forward + Vector3.up * 2.3f);
        if (agent.hasPath)
        {
            animator.SetBool("walking", true);
            animator.speed = agent.velocity.magnitude;
        }
        else
        {
            animator.speed = 1f;
            animator.SetBool("walking", false);
        }
        Shader.SetGlobalVector("_PlayerPosition", transform.position);
    }

    public IEnumerator FootstepSoundsRoutine()
    {
        while (true)
        {
            while (agent.hasPath)
            {
                if (audioSource)
                    audioSource.PlayOneShot(footStepSounds[Random.Range(0, footStepSounds.Count)]);
                yield return new WaitForSeconds(0.4f);
            }
            yield return new WaitForEndOfFrame();
        }
    }

    public void SetDestination(Vector3 target)
    {
        agent.SetDestination(target);
    }
}
