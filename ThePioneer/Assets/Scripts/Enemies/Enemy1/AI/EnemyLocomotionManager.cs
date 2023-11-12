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
        public bool isDeath;

       // public Transform targetPlayer;

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
            isDeath = false;
           


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
            if(canSeePlayer == true) {
                radius = 30f;
            } else {
                radius = 8.6f;
            }
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

        public void HandleMoveToTarget(bool camina) {
            
           
            

            Vector3 targetDirection = playerRef.transform.position - transform.position;
            distanceFromTargetAttack2 = Vector3.Distance(playerRef.transform.position, transform.position);
            float viewableAngle = Vector3.Angle(targetDirection, transform.forward);

            if (enemyManager.isPerformingAction == true) {
             
                navMeshAgent.enabled = false;
                enemyAnimatorManager.anim.SetBool("IsWalking", false);
                    


            } else {
              
                if(camina == true) {
                    if (distanceFromTargetAttack2 >= 1.5f) {
                        navMeshAgent.enabled = true;
                        HandleRotateTowardsTarget();
                        enemyAnimatorManager.anim.SetBool("IsWalking", true);
                        //  HandleRotateTowardsTarget();
                    }
                } else  {
                    
                    navMeshAgent.enabled = false;
                    enemyAnimatorManager.anim.SetBool("IsWalking", false);


                }
            }


            /*
            if (enemyManager.isPerformingAction == true) {
                navMeshAgent.enabled = false;
                enemyAnimatorManager.anim.SetBool("IsWalking",false);
                
                
            } else {
             
               
                if(distanceFromTargetAttack2 >= 1.5f && canSeePlayer == true) {
                  
                    enemyAnimatorManager.anim.SetBool("IsWalking", true);
                  //  HandleRotateTowardsTarget();

                } else{
                    print("no se mueve");
                    enemyAnimatorManager.anim.SetBool("IsWalking", false);
                   
                   
                }
            }
            */

            

         //navMeshAgent.transform.localPosition = Vector3.zero;
         //  navMeshAgent.transform.localRotation = Quaternion.identity;
               
          

        }
           

        public void HandleRotateTowardsTarget() {
            //Rotate manually
            if(enemyManager.isPerformingAction) {
                Vector3 direction = playerRef.transform.position - transform.position;
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
                navMeshAgent.SetDestination(playerRef.transform.position);
                   
                enemyRigidBody.velocity = targetVelocity;
                transform.rotation = Quaternion.Slerp(transform.rotation, navMeshAgent.transform.rotation, rotationSpeed *Time.deltaTime);
            }
        }

        public void IsDeathDontMove() {
            if( enemyStats.currentHealth == 0 ) {
                isDeath = true;
                navMeshAgent.enabled = false;
                navMeshAgent.updateRotation = false;
            }
        }

        public void DontMove() {
           
                
                navMeshAgent.enabled = false;
                navMeshAgent.updateRotation = false;
            
        }
    }
}
