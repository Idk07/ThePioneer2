using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

namespace MV {
    public class EnemyLocomotionManager : MonoBehaviour {

        EnemyManager enemyManager;
        EnemyAnimatorManager enemyAnimatorManager;
        EnemyStats enemyStats;
        private NavMeshAgent navMeshAgent;
        public Rigidbody enemyRigidBody;

        public Transform targetPlayer;

        public float radius;
        [Range(0, 360)]
        public float angle;



        public GameObject playerRef;
        public LayerMask detectionLayer;
        public LayerMask obstructionMask;
        public bool canSeePlayer;
        public bool seeIt = false;

        public float stoppingDistance;
        public float distanceFromTargetAttack;
        public float distanceFromTargetAttack2;


        public float rotationSpeed;


        private void Start() {
            playerRef = GameObject.FindGameObjectWithTag("Player");
            StartCoroutine(FOVRoutine());
            navMeshAgent.enabled = false;
            enemyRigidBody.isKinematic = false;
            canSeePlayer = false;
            seeIt = false;


        }
        private void Awake() {
            enemyManager = GetComponent<EnemyManager>();
            navMeshAgent = GetComponent<NavMeshAgent>();
            enemyAnimatorManager = GetComponentInChildren<EnemyAnimatorManager>();
            enemyRigidBody = GetComponent<Rigidbody>();
            enemyStats = GetComponent<EnemyStats>();


        }

        private IEnumerator FOVRoutine() {
            WaitForSeconds wait = new WaitForSeconds(0f);

            while (true) {
                yield return wait;
                HandleDetection();
            }
        }

        public void HandleDetection() {
            Collider[] colliders = Physics.OverlapSphere(transform.position, radius, detectionLayer);

            if (colliders.Length != 0) {
                Transform target = colliders[0].transform;

                Vector3 directionToTarget = (target.position - transform.position).normalized;


                if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2) {
                    float distanceToTarget = Vector3.Distance(transform.position, target.position);

                    if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask)) {

                        canSeePlayer = true;
                        seeIt = true;
                        distanceFromTargetAttack = Vector3.Distance(target.position, transform.position);
                    } else
                        canSeePlayer = false;
                } else
                    canSeePlayer = false;
            } else if (canSeePlayer)
                canSeePlayer = false;

        }

        public void HandleMoveToTarget() {
            
            if(seeIt == false) 
               return;
            

            Vector3 targetDirection = targetPlayer.position - transform.position;
            distanceFromTargetAttack2 = Vector3.Distance(targetPlayer.position, transform.position);
            float viewableAngle = Vector3.Angle(targetDirection, transform.forward);

            if (enemyManager.isPerformingAction == true) {
                navMeshAgent.enabled = false;
                enemyAnimatorManager.anim.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
                
            } else {
               
                if(distanceFromTargetAttack > stoppingDistance) {
                    
                    enemyAnimatorManager.anim.SetFloat("Vertical", 1, 0.1f, Time.deltaTime);            

                } else if(distanceFromTargetAttack <= stoppingDistance) {
                    
                    enemyAnimatorManager.anim.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
                }
            }

            HandleRotateTowardsTarget();

         //navMeshAgent.transform.localPosition = Vector3.zero;
         //  navMeshAgent.transform.localRotation = Quaternion.identity;
               
          

        }
           

        public void HandleRotateTowardsTarget() {
            //Rotate manually
            if(enemyManager.isPerformingAction) {
                Vector3 direction = targetPlayer.position - transform.position;
                direction.y = 0;
                direction.Normalize();

                if(direction == Vector3.zero) {
                    direction = transform.forward;
                }

                Quaternion targerRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targerRotation, rotationSpeed * Time.deltaTime);
            }else {
                Vector3 relativeDirection = transform.InverseTransformDirection(navMeshAgent.desiredVelocity);
                Vector3 targetVelocity = enemyRigidBody.velocity;

                navMeshAgent.enabled = true;
                navMeshAgent.SetDestination(targetPlayer.position);
                   
                enemyRigidBody.velocity = targetVelocity;
                transform.rotation = Quaternion.Slerp(transform.rotation, navMeshAgent.transform.rotation, rotationSpeed *Time.deltaTime);
            }
        }

        public void IsDeathDontMove() {
            if( enemyStats.currentHealth == 0 ) {
                navMeshAgent.enabled = false;
                navMeshAgent.updateRotation = false;
            }
        }
    }
}
