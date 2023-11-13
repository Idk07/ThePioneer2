using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MV
{
    public class PlayerStats : MonoBehaviour
    {
        //Health
        public int healthLevel = 10;
        public int maxHealth;
        public int currentHealth;
        public bool died = false;

        //Stamina
        public int staminaLevel = 10;
        public int maxStamina;
        public int currentStamina;
        public int baseStamina;
        public int lightAttackMultiplier;
        public int directAttackMultiplier;
        public int heavyAttackMultiplier;


        AnimatorHandler animatorHandler;

        
        public HealthBar healthBar;
        public StaminaBar staminaBar;

        private void Awake()
        {
            animatorHandler = GetComponentInChildren<AnimatorHandler>();
        }
        void Start()
        {
            maxHealth = SetMaxHealthFromHealthLevel();
            currentHealth = maxHealth;
            healthBar.SetMaxHealth(maxHealth);

            maxStamina = SetMaxStaminaFromStaminaLevel();
            currentStamina = maxStamina;
            staminaBar.SetMaxStamina(maxStamina);
        }

        private void OnConnectedToServer()
        {
            maxHealth = healthLevel*10; 
            maxStamina = staminaLevel*10;
        }

        private int SetMaxHealthFromHealthLevel()
        {
            maxHealth = healthLevel * 10;
            return maxHealth;
        }
        
        public void TakeDamage(int damage)
        {
            currentHealth = currentHealth - damage;
            healthBar.SetCurrentHealth(currentHealth);

            if(currentHealth <= 0)
            {
                died = true;
                currentHealth = 0;
                animatorHandler.PlayTargetAnimation("Death", true);
                //Handle Player Death

            }
        }
        private int SetMaxStaminaFromStaminaLevel()
        {
            maxStamina = staminaLevel * 10;
            return maxStamina;
        }

        public void TakeStaminaDamage(int damage)
        {
            currentStamina = currentStamina - damage;
            staminaBar.SetCurrentStamina(currentStamina);
        }

    }

}