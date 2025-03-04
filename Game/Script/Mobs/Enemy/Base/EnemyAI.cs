using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

[SelectionBase]
public abstract class EnemyAI : MonoBehaviour {
    [SerializeField] private State defaultState;

    protected NavMeshAgent agent;

    protected const float ATTACK_RATE_TIME = 2f;
    protected float attackNextTime;

    private float nextCheckDirectionTime;
    private const float CHECK_DIRECTION_DURATION = 0.1f;

    protected State currentState { get; set; }
    protected Vector3 currentPosition;

    protected WalkingDto walkingDto;
    protected ChasingDto chasingDto;
    protected KnockBack knockBack;
    protected CommonEntity entity;

    public EventHandler onDie;
    public EventHandler onEnemyAttack;

    public enum State {
        Idle,
        Walking,
        Chasing,
        Attacking,
        Death,
        Patroling
    }

    private void Awake() {
        agent = GetComponent<NavMeshAgent>();
        walkingDto = GetComponent<WalkingDto>();
        chasingDto = GetComponent<ChasingDto>();
        knockBack = GetComponent<KnockBack>();
        entity = GetComponent<CommonEntity>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.speed = entity.speed;
        currentState = defaultState;
    }

    protected virtual void Start() {
        entity = GetComponent<CommonEntity>();
        entity.onTakeHurt += OnTakeHurt;
    }

    protected virtual void FixedUpdate() {
        if (knockBack && knockBack.isKnocking)
            return;
        StateHandler();
        FacingDirectionHandler();
    }

    protected virtual void OnTakeHurt(object sender, HurtEventArgs e) {
        knockBack.GetKnockedBack(e.transformSource);
        if (entity.GetHealth() > 0) {
            StartCoroutine(OnTakeHurt());
        }
        else {
            DetectDeath();
        }
    }

    private IEnumerator OnTakeHurt() {
        currentState = State.Idle;
        yield return new WaitForSeconds(0.5f);
        currentState = State.Chasing;
    }

    private void DetectDeath() {
        currentState = State.Death;
        onDie.Invoke(this, EventArgs.Empty);
        knockBack.StopKnockBackMovement();
        StartCoroutine(Destroy());
    }

    private IEnumerator Destroy() {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }

    protected virtual void StateHandler() {
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
            case State.Walking:
                CheckState();
                Walking();
                break;
        }
    }

    protected virtual void CheckState() {
        if (!PlayerController.instance) {
            currentState = State.Walking;
            return;
        }

        var distance = Vector3.Distance(transform.position, PlayerController.instance.transform.position);
        switch (distance) {
            case <= 0.5f:
                agent.ResetPath();
                currentState = State.Attacking;
                walkingDto.walkingTime = 0f;
                break;
            case var value when value <= chasingDto.chasingDistance :
                currentState = State.Chasing;
                walkingDto.walkingTime = 0f;
                attackNextTime = 0f;
                break;
            default:
                currentState = State.Walking;
                attackNextTime = 0f;
                break;
        }
    }

    protected virtual void Walking() {
        walkingDto.walkingTime -= Time.deltaTime;
        if (walkingDto.walkingTime <= 0f) {
            var targetPosition = GetWalkingPosition();
            agent.SetDestination(targetPosition);
            agent.speed = entity.speed;
            walkingDto.walkingTime = walkingDto.walkingTimeMax;
        }

        chasingDto.chasingBoostTime = chasingDto.chasingBoostTimeMax;
    }

    protected virtual void Attacking() {
        if (Time.time < attackNextTime || !entity.canAttack)
            return;
        StartCoroutine(RotateAttack());
        onEnemyAttack?.Invoke(this, EventArgs.Empty);
        attackNextTime = Time.time + ATTACK_RATE_TIME;
    }

    private IEnumerator RotateAttack() {
        yield return new WaitForSeconds(Random.Range(0.5f, 1.5f));
        currentState = State.Chasing;
    }

    protected virtual void Chasing() {
        chasingDto.chasingBoostTime -= Time.deltaTime;
        if (chasingDto.chasingBoostTime >= 0f) {
            agent.speed = entity.speed * chasingDto.boostSpeed;
        }
        agent.SetDestination(GetOffsetPosition(PlayerController.instance.transform.position));
    }

    private Vector3 GetOffsetPosition(Vector3 targetPosition) {
        const float offset = 0.4f; // Расстояние разброса
        var randomOffset = new Vector3(Random.Range(-offset, offset), Random.Range(-offset, offset), 0);
        return targetPosition + randomOffset;
    }

    private void FacingDirectionHandler() {
        if (Time.time < nextCheckDirectionTime)
            return;

        switch (currentState) {
            case State.Walking or State.Patroling:
                ChangeFacingDirection(currentPosition, transform.position);
                break;
            case State.Attacking or State.Chasing:
                ChangeFacingDirection(currentPosition, PlayerController.instance.transform.position);
                break;
        }

        currentPosition = transform.position;
        nextCheckDirectionTime = Time.time + CHECK_DIRECTION_DURATION;
    }

    private Vector3 GetWalkingPosition() {
        return currentPosition + Utils.GetRandomDir() * Random.Range(walkingDto.walkingDistanceMin,
            walkingDto.walkingDistanceMax);
    }

    protected virtual void ChangeFacingDirection(Vector3 startedPosition, Vector3 targetPosition) {
        transform.rotation = Quaternion.Euler(0, startedPosition.x > targetPosition.x ? 180 : 0, 0);
    }

    public bool IsWalking() {
        return agent.velocity != Vector3.zero && currentState == State.Walking;
    }

    public bool IsChasing() {
        return agent.velocity != Vector3.zero && currentState == State.Chasing;
    }
}