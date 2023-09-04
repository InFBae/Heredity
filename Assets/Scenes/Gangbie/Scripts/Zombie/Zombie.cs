using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
    private GameObject player;
    private ZombieGazeController gazeController;

    [SerializeField] float moveSpeed;
    [SerializeField] float runSpeed;

    [SerializeField, Range(0.01f, 2f)] float turnSomoothTime;
    private float turnSmoothVelocity;

    public GameObject targetEntity;
    public LayerMask targetMask;

    public int curStateNum;
    private bool hasTarget => targetEntity != null;

    private enum State
    {
        Idle, Patrol, Trace
    }

    StateMachine<State, Zombie> stateMachine;

    private State state;

    private Rigidbody rb;
    private NavMeshAgent agent;
    private Animator anim;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        gazeController = GetComponent<ZombieGazeController>();

        player = GameObject.FindWithTag("Player");

        stateMachine = new StateMachine<State, Zombie>(this);
        stateMachine.AddState(State.Idle,   new IdleState(this, stateMachine));
        stateMachine.AddState(State.Patrol, new PatrolState(this, stateMachine));
        stateMachine.AddState(State.Trace,  new TraceState(this, stateMachine));
    }

    public void Setup(float moveSpeed, float runSpeed)
    {
        this.moveSpeed = moveSpeed;
        this.runSpeed = runSpeed;
    }

    private void Start()
    {
        targetEntity = null;
        stateMachine.SetUp(State.Idle);
    }

    private void Update()
    {
        stateMachine.Update();
        anim.SetFloat("Speed", agent.desiredVelocity.magnitude);

        // Debug.Log(hasTarget);
        // Debug.Log(targetEntity);
        // Debug.Log(stateMachine.CurState);
    }

    private abstract class ZombieState : StateBase<State, Zombie>
    {
        protected GameObject gameObject => owner.gameObject;
        protected Transform transform => owner.transform;
        protected NavMeshAgent agent => owner.agent;
        protected Animator anim => owner.anim;

        protected ZombieState(Zombie owner, StateMachine<State, Zombie> stateMachine) : base(owner, stateMachine)
        {
        }
    }

    private class IdleState : ZombieState
    {
        public IdleState(Zombie owner, StateMachine<State, Zombie> stateMachine) : base(owner, stateMachine)
        {
        }
        public override void Setup()
        {

        }
        public override void Enter()
        {
            owner.curStateNum = 1;
            owner.agent.speed = 0;
            idleRoutine = owner.StartCoroutine(IdleRoutine());
        }
        public override void Update()
        {
        }
        public override void Transition()
        {
        }
        public override void Exit()
        {
            owner.StopCoroutine(idleRoutine);
        }

        Coroutine idleRoutine;

        private IEnumerator IdleRoutine()
        {
            yield return new WaitForSeconds(3);
            stateMachine.ChangeState(State.Patrol);
        }
    }
    private class PatrolState : ZombieState
    {
        private int routineNum;
        public PatrolState(Zombie owner, StateMachine<State, Zombie> stateMachine) : base(owner, stateMachine)
        {
        }
        public override void Setup()
        {

        }
        public override void Enter()
        {
            owner.curStateNum = 2;
            routineNum = 0;
            owner.agent.speed = owner.moveSpeed;
            owner.agent.SetDestination(transform.position);
            patrolRoutine = owner.StartCoroutine(PatrolRoutine());
        }
        public override void Update()
        {
        }
        public override void Transition()
        {
            if(owner.hasTarget)
            {
                stateMachine.ChangeState(State.Trace);
            }
            else if (routineNum == 5)
            {
                stateMachine.ChangeState(State.Idle);
            }

        }
        public override void Exit()
        {
            owner.StopCoroutine(patrolRoutine);
        }

        Coroutine patrolRoutine;

        private IEnumerator PatrolRoutine()
        {
            while (!owner.hasTarget)
            {
                if (agent.remainingDistance <= 1f)
                {
                    routineNum++;
                    var patrolPosition = Utility.GetRandomPointOnNavMesh(transform.position, 7f, NavMesh.AllAreas);
                    agent.SetDestination(patrolPosition);
                }
                yield return new WaitForSeconds(0.1f);
            }
        }
    }

    private class TraceState : ZombieState
    {
        private int routineNum;
        private float time;
        private Vector3 prevPostion;
        public TraceState(Zombie owner, StateMachine<State, Zombie> stateMachine) : base(owner, stateMachine)
        {
        }
        public override void Setup()
        {

        }
        public override void Enter()
        {
            owner.curStateNum = 3;
            time = 0;
            owner.agent.speed = owner.runSpeed;
            traceRoutine = owner.StartCoroutine(TraceRoutine());
        }
        public override void Update()
        {
            time += Time.deltaTime;

            if (time > 12f)
            {
                owner.targetEntity = null;
                owner.gazeController.slider.gameObject.SetActive(true);
            }
        }
        public override void Transition()
        {
            if (!owner.hasTarget)
            {
                stateMachine.ChangeState(State.Patrol);
            }
        }
        public override void Exit()
        {
            owner.StopCoroutine(traceRoutine);
        }

        Coroutine traceRoutine;

        private IEnumerator TraceRoutine()
        {
            while (owner.hasTarget)
            {
                agent.SetDestination(owner.targetEntity.transform.position);

                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}
