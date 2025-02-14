using UnityEngine;

public class EyeEvilEnemyAI : EnemyAI {

    protected override void StateHandler() {
        switch (currentState) {
            default:
            case State.Death:
            case State.Idle:
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
                Walking();
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
                currentState = State.Attacking;
                agent.ResetPath();
                walkingDto.walkingTime = 0f;
                break;
            case var value when chasingDto.chasingDistance <= value :
                currentState = State.Chasing;
                walkingDto.walkingTime = 0f;
                attackNextTime = 0f;
                break;
            default:
                currentState = State.Patroling;
                attackNextTime = 0f;
                break;
        }
    }

    protected void Patrolling() {

    }
}