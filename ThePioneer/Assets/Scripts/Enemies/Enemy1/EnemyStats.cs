using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MV {
    public class EnemyStats : MonoBehaviour {

        EnemyAnimatorManager enemyAnimatorManager;
        public GameObject vidaBarraEnemigo;
        public new Collider collider;


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
                DontShowBarra();
                
                
                PuntosdeExperiencia.instancePuntosDeExperiencia.SumaDeExperiencia(50);
                
            }
        }
        private void DontShowBarra() {
            vidaBarraEnemigo.SetActive(false);
        }

        /*
        private void OnCollisionEnter(Collision collision) {
            if(currentHealth > 0) {
                return;
            }

            if(collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Player")){
                collider.isTrigger = true;
            } else {
                collider.isTrigger = false;
            }
        }
        */





    }
}