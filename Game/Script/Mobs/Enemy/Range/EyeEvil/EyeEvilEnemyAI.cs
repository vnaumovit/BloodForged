using UnityEngine;

public class EyeEvilEnemyAI : EnemyAI {
    private bool isFinishedSecondDestination;
    private Vector3 firstDestination;
    private Vector3 secondDestination;

    protected new void Start() {
        base.Start();
        firstDestination = transform.position;
        secondDestination = transform.position + new Vector3(2f, 4f, 0f);
    }

    protected override void StateHandler() {
        switch (currentState) {
            default:
            case State.Death:
                break;
            case State.Idle:
                CheckState();
                break;
            case State.Chasing:
                CheckState();
                Chasing();
                break;
            case State.Attacking:
                CheckState();
                Attacking();
                break;
            case State.Patroling:
                CheckState();
                Patrolling();
                break;
        }
    }

    protected override void CheckState() {
        if (!PlayerController.instance) {
            currentState = State.Patroling;
            return;
        }

        var distance = Vector3.Distance(transform.position, PlayerController.instance.transform.position);
        switch (distance) {
            case <= 4f:
                agent.ResetPath();
                currentState = State.Attacking;
                break;
            case var value when value <= chasingDto.chasingDistance:
                currentState = State.Chasing;
                attackNextTime = 0f;
                break;
            default:
                PatrollingAfterState();
                currentState = State.Patroling;
                attackNextTime = 0f;
                break;
        }
    }

    private void PatrollingAfterState() {
        if (currentState == State.Patroling) return;
        agent.SetDestination(secondDestination);
        isFinishedSecondDestination = false;
    }

    private void Patrolling() {
        isFinishedSecondDestination = Vector3.Distance(transform.position, secondDestination) <= 0.1f;
        if (isFinishedSecondDestination) {
            agent.SetDestination(firstDestination);
        }
        else if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance) {
            agent.SetDestination(secondDestination);
        }
    }

    protected override void ChangeFacingDirection(Vector3 startedPosition, Vector3 targetPosition) {
        transform.rotation = Quaternion.Euler(0, startedPosition.x < targetPosition.x ? 180 : 0, 0);
    }

}