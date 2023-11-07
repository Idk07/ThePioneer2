using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MV {
    public class EnemyStats : MonoBehaviour {

        EnemyAnimatorManager enemyAnimatorManager;

        public int healthLevel = 10;
        public int maxHealth;
        public int currentHealth;

        Animator animator;

        public HealthBar healthBar;

        private void Awake() {
            animator = GetComponent<Animator>();
            enemyAnimatorManager = GetComponentInChildren<EnemyAnimatorManager>();
        }

        void Start() {
            maxHealth = SetMaxHealthFromHealthLevel();
            currentHealth = maxHealth;
            healthBar.SetMaxHealth(maxHealth);
        }

        private void Update() {
            if(Input.GetKeyUp(KeyCode.L)) {
                Damage(50);
            }
        }

        private int SetMaxHealthFromHealthLevel() {
            maxHealth = healthLevel * 10;
            return maxHealth;
        }

        public void Damage(int damage) {
            currentHealth = currentHealth - damage;
            healthBar.SetCurrentHealth(currentHealth);

            if(currentHealth <= 0) {
                currentHealth = 0;
                enemyAnimatorManager.PlayTargetAnimation("Death", true);
            }
        }

            
       
        
      
       
    }
}