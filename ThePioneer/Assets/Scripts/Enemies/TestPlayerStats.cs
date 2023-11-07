using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MV {
    public class TestPlayerStats : MonoBehaviour {
        public int healthLevel = 10;
        public int maxHealth;
        public int currentHealth;

        void Start() {
            maxHealth = SetMaxHealthFromHealthLevel();
            currentHealth = maxHealth;

        }

        private int SetMaxHealthFromHealthLevel() {
            maxHealth = healthLevel * 10;
            return maxHealth;
        }

        public void Damage(int damage) {
            currentHealth = currentHealth - damage;

            if (currentHealth <= 0) {
                currentHealth = 0;
            }
        }
    }
}