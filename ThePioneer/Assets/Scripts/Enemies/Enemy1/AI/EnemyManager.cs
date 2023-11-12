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
        public bool entroBoss;
       

        [Header("A.I Settings")]

        

        //Cambiar el TestPlayerStats por el nombre del script que contenga el PlayerStats
        TestPlayerStats playerStats;

        private void Start() {
            instanceEnemyManager = this;
            entroBoss = false;
        
            
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
            

            enemyLocomotionManager.HandleDetection();

            enemyLocomotionManager.IsDeathDontMove();
          
            if (enemyLocomotionManager.isDeath == false) { 
                if (enemyLocomotionManager.canSeePlayer == true ) {
                    if (isPerformingAction != true) {

                        if (enemyLocomotionManager.distanceFromTargetAttack > enemyLocomotionManager.stoppingDistance) {
                            enemyLocomotionManager.HandleMoveToTarget(true);
                        } else if (enemyLocomotionManager.distanceFromTargetAttack <= 10f) {
                            AttackTarget();                           

                            
                        }
                    }
                } else {
                   
                        enemyLocomotionManager.HandleMoveToTarget(false);
                    
                }
            }
        /*
            if(enemyLocomotionManager.canSeePlayer != true) {
                enemyLocomotionManager.HandleDetection();
                
            }else {
                
                if (isPerformingAction != true && enemyLocomotionManager.seeIt == true) {
                   
                     if (enemyLocomotionManager.distanceFromTargetAttack > enemyLocomotionManager.stoppingDistance) {
                         enemyLocomotionManager.HandleMoveToTarget();
                     } 
                    else if (enemyLocomotionManager.distanceFromTargetAttack <= 10f) {
                         AttackTarget();
                       


                    }
                }
                
            }

            */
            
          

        }

        private void HandleRecoveryTimer() { 


            if(currentRecoveryTime > 0) {
                currentRecoveryTime -= Time.deltaTime;                

            }
            if (isPerformingAction) {
                if (currentRecoveryTime <= 0) {
                    isPerformingAction = false;

                }
            }

            /*
            if (isPerformingAction) {
                if (currentRecoveryTime <= 0) {
                    isPerformingAction = false;
                
                }
            }
            */


            /*
            if(currentRecoveryTime > 0) {
                currentRecoveryTime -= Time.deltaTime;

            }
            if (isPerformingAction) {
                if (currentRecoveryTime <= 0) {
                    isPerformingAction = false;
                }
            }

            //   if(currentRecoveryTime <= 0) {
            //   isPerformingAction = false;
            //   }
            */


        }

       
       
        #region Attacks

        private void AttackTarget() {

         

            if (currentAttacks == null) {
                GetNewAttack();
                     

            } else {
                //GetNewAttack();
                isPerformingAction = true;               
                currentRecoveryTime = currentAttacks.recoveryTime;               
                enemyAnimatorManager.PlayTargetAnimation(currentAttacks.actionAnimation, true);
                currentAttacks = null;
                //GetNewAttack();
            }

            
        }

        private void GetNewAttack() {
       
            int maxScore = 3;
           
            int randomValue = Random.Range(0, maxScore);
            
                EnemyAttackAction enemyAttackAction = enemyAttacks[randomValue];
            currentAttacks = enemyAttackAction;

        }




        #endregion

        


    }

}