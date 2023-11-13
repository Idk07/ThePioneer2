using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MV
{
    public class PlayerAttacker : MonoBehaviour
    {
        AnimatorHandler animatorHandler;
        PlayerStats playerStats;

        private void Awake()
        {
            animatorHandler = GetComponentInChildren<AnimatorHandler>();
            playerStats = GetComponentInParent<PlayerStats>();
        }
        public void HandleLightAttack()
        {
            if(playerStats.died==true)
            {
                return;
            }
            animatorHandler.PlayTargetAnimation("LightAttack", true);
            Debug.Log("LightAttack");
            playerStats.TakeStaminaDamage(playerStats.baseStamina * playerStats.lightAttackMultiplier);
        }

        public void HandleHeavyAttack() 
        {
            if (playerStats.died == true)
            {
                return;
            }
            animatorHandler.PlayTargetAnimation("HeavyAttack", true);
            Debug.Log("HeavyAttack");
            playerStats.TakeStaminaDamage(playerStats.baseStamina * playerStats.heavyAttackMultiplier);
        }

        public void HandleDirectAttack()
        {
            if (playerStats.died == true)
            {
                return;
            }
            animatorHandler.PlayTargetAnimation("DirectAttack", true);
            Debug.Log("DirectAttack");
            playerStats.TakeStaminaDamage(playerStats.baseStamina * playerStats.directAttackMultiplier);
        }

        public void DrainStamina()
        {
            
        }
    }
}

