using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;
using DialogueEditor;

public class PlayerController : MonoBehaviour
{
    const string IDLE = "Idle";
    const string WALK = "Walk";

    CustomActions input;

    NavMeshAgent agent;
    Animator animator;

    [Header("Movement")]
    [SerializeField] ParticleSystem clickEffect;
    [SerializeField] LayerMask clickableLayers;

    float lookRotationSpeed = 8f;

    void Awake() 
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        input = new CustomActions();
        AssignInputs();
    }

    void AssignInputs()
    {
        input.Main.Move.performed += ctx => ClickToMove();
    }

    void ClickToMove()
    {
        // Check if conversation is active before processing movement
        if (ConversationManager.Instance.IsConversationActive)
            return; // Ignore input if UI is active

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100, clickableLayers)) 
        {
            agent.destination = hit.point;
            if (clickEffect != null)
            {
                Instantiate(clickEffect, hit.point + new Vector3(0, 0.1f, 0), clickEffect.transform.rotation);
            }
        }
    }

    void OnEnable() 
    { 
        input.Enable(); 
    }

    void OnDisable() 
    { 
        input.Disable(); 
    }

    void Update() 
    {
        FaceTarget();
        SetAnimations();
    }

    void FaceTarget()
    {
        if (agent.velocity != Vector3.zero)
        {
            Vector3 direction = (agent.destination - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * lookRotationSpeed);
        }
    }

    void SetAnimations()
    {
        if (agent.velocity == Vector3.zero)
        { 
            animator.Play(IDLE); 
        }
        else
        { 
            animator.Play(WALK); 
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collided object is not a trigger and has no Rigidbody
        if (!collision.collider.isTrigger && collision.rigidbody == null)
        {
            // Stop the player movement
            agent.isStopped = true;
            agent.ResetPath(); // Clears the path to stop movement
            Debug.Log("Player hit a static obstacle and stopped.");
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (!collision.collider.isTrigger && collision.rigidbody == null)
        {
            // Allow the player to move again after leaving the obstacle
            agent.isStopped = false;
            Debug.Log("Player cleared the obstacle and can move again.");
        }
    }
}
