using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MV {
    public class EnemyManager : MonoBehaviour {
        EnemyLocomotionManager enemyLocomotionManager;
        EnemyAnimatorManager enemyAnimatorManager;


        public static EnemyManager instanceEnemyManager;

        public bool isPerformingAction;

        public EnemyAttackAction[] enemyAttacks;
        public EnemyAttackAction currentAttacks;

        public float currentRecoveryTime = 0;


        public int damagePlayer = 25;
        private bool isRecover;
        private bool attackNoStop;

        [Header("A.I Settings")]

        

        //Cambiar el TestPlayerStats por el nombre del script que contenga el PlayerStats
        TestPlayerStats playerStats;

        private void Start() {
            instanceEnemyManager = this;
        }


        public void Awake() {
            enemyLocomotionManager = GetComponent<EnemyLocomotionManager>();
            enemyAnimatorManager = GetComponentInChildren<EnemyAnimatorManager>();

            //Cambiar el TestPlayerStats por el nombre del script que contenga el PlayerStats
            playerStats = GameObject.Find("Player").GetComponent<TestPlayerStats>();



        }
        private void Update() {
            HandleCurrentAction();
            HandleRecoveryTimer();
           

        }

        private void HandleCurrentAction() {
            /*
            if (enemyLocomotionManager.seeIt == true) {
                if (enemyLocomotionManager.distanceFromTargetAttack > enemyLocomotionManager.stoppingDistance) {

                    enemyLocomotionManager.HandleMoveToTarget();
                } //else if (enemyLocomotionManager.distanceFromTargetAttack <= enemyLocomotionManager.stoppingDistance) {
                //    AttackTarget();
               // }
            } else {
                enemyLocomotionManager.HandleDetection();
            }
            */
            enemyLocomotionManager.IsDeathDontMove();

            if(enemyLocomotionManager.seeIt != true) {
                enemyLocomotionManager.HandleDetection();
                
            }else {
                
                if (isPerformingAction != true && enemyLocomotionManager.seeIt == true) {
                   
                     if (enemyLocomotionManager.distanceFromTargetAttack > enemyLocomotionManager.stoppingDistance) {
                         enemyLocomotionManager.HandleMoveToTarget();
                     } else if (enemyLocomotionManager.distanceFromTargetAttack <= 20f) {
                         AttackTarget();
                     }
                }
                
            }
            
          

        }

        private void HandleRecoveryTimer() { 

            if(currentRecoveryTime > 0) {
                currentRecoveryTime -= Time.deltaTime;

            }
            if(isPerformingAction) {
                if(currentRecoveryTime <= 0) {
                    isPerformingAction = false;
                }
            }
          

        }


        #region Attacks

        private void AttackTarget() {

            if (isPerformingAction) 
                return;

            if (currentAttacks == null) {
                GetNewAttack();
                
            } else {
                //GetNewAttack();
                isPerformingAction = true;
                currentRecoveryTime = currentAttacks.recoveryTime;
                enemyAnimatorManager.PlayTargetAnimation(currentAttacks.actionAnimation, true);
                currentAttacks = null;
                GetNewAttack();
            }

            /*
            if (currentAttacks == null) {
                GetNewAttack();
            } else {
                isPerformingAction = true;
                currentRecoveryTime = currentAttacks.recoveryTime;
                enemyAnimatorManager.PlayTargetAnimation(currentAttacks.actionAnimation, true);
                currentAttacks = null;
            }
            */
        }

        private void GetNewAttack() {
            Vector3 targetsDirection = enemyLocomotionManager.targetPlayer.position - transform.position;
            float viewableAngle = Vector3.Angle(targetsDirection, transform.position);
            enemyLocomotionManager.distanceFromTargetAttack = Vector3.Distance(enemyLocomotionManager.targetPlayer.position, transform.position);
            int maxScore = 0;
            

            for(int i = 0; i < enemyAttacks.Length; i++) {
                EnemyAttackAction enemyAttackAction = enemyAttacks[i];

                if(enemyLocomotionManager.distanceFromTargetAttack <= enemyAttackAction.maximunDistanceNeededToAttack 
                    && enemyLocomotionManager.distanceFromTargetAttack >= enemyAttackAction.minimunDistanceNeededToAttack) {  

                    if(viewableAngle <= enemyAttackAction.maximunAttackAngle
                        && viewableAngle >= enemyAttackAction.minimunAttackAngle ) {

                        maxScore += enemyAttackAction.attackScore;
                    }
                }
            }

            int randomValue = Random.Range(0, maxScore);
            int temporaryScore = 0;

            for(int i = 0;i < enemyAttacks.Length;i++) {
                EnemyAttackAction enemyAttackAction = enemyAttacks[i];

                if (enemyLocomotionManager.distanceFromTargetAttack <= enemyAttackAction.maximunDistanceNeededToAttack
                    && enemyLocomotionManager.distanceFromTargetAttack >= enemyAttackAction.minimunDistanceNeededToAttack) {

                    if (viewableAngle <= enemyAttackAction.maximunAttackAngle
                        && viewableAngle >= enemyAttackAction.minimunAttackAngle) {

                        if(currentAttacks != null) 
                            return;

                            temporaryScore += enemyAttackAction.attackScore;

                            if(temporaryScore > randomValue) {
                                currentAttacks = enemyAttackAction;
                            
                            }
                    }
                }
            }
        }

      
        #endregion

       

    }

}