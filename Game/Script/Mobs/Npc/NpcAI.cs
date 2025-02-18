using UnityEngine;
using UnityEngine.AI;

public class NpcAI : MonoBehaviour {
    [SerializeField] private State startingState = State.Idle;
    [SerializeField] private float roamingDistanceMax = 6f;
    [SerializeField] private float roamingDistanceMin = 2f;
    [SerializeField] private float roamingTimerMax = 2f;

    private NavMeshAgent agent;
    private State currentState;
    private float roamingTimer;
    private Vector3 currentPosition;
    private Vector3 roamingPosition;

    private enum State {
        Idle,
        Roaming,
    }

    public void Awake() {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        currentState = startingState;
    }

    public void FixedUpdate() {
        switch (currentState) {
            default:
            case State.Idle:
                break;
            case State.Roaming:
                roamingTimer -= Time.deltaTime;
                if (roamingTimer <= 0f) {
                    Roaming();
                    roamingTimer = roamingTimerMax;
                }

                break;
        }
    }

    private void Roaming() {
        var targetPosition = GetRoamingPosition();
        currentPosition = transform.position;
        ChangeFacingDirection(currentPosition, targetPosition);
        agent.SetDestination(targetPosition);
    }

    private Vector3 GetRoamingPosition() {
        return currentPosition + Utils.GetRandomDir() * Random.Range(roamingDistanceMin, roamingDistanceMax);
    }

    private void ChangeFacingDirection(Vector3 startedPosition, Vector3 targetPosition) {
        transform.rotation = Quaternion.Euler(0, startedPosition.x > targetPosition.x ? -180 : 0, 0);
    }
}