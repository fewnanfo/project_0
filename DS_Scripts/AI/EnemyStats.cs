using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class EnemyStats : CharacterStatsManager
    {

        Animator animator;
        EnemyAnimatorManager enemyAnimatorManager;
        EnemyBossManager enemyBossManager;
        public UIEnemyHealthBar enemyHealthBar;
        CharacterSoundFXManager characterSoundFXManager;
        public GameObject winUI;

        public bool isBoss;

        private void Awake()
        {
            //animator = GetComponentInChildren<Animator>();
            characterSoundFXManager = GetComponent<CharacterSoundFXManager>();
            enemyAnimatorManager = GetComponentInChildren<EnemyAnimatorManager>();
            enemyBossManager = GetComponent<EnemyBossManager>();
            maxHealth = SetmaxHealthFromHealthLevel();
            currentHealth = maxHealth;
        }

       

        void Start()
        {
            if (!isBoss)
            {
                enemyHealthBar.SetMaxHealth(maxHealth);
            }            
        }
        private int SetmaxHealthFromHealthLevel()
        {
            maxHealth = healthLevel * 10;
            return maxHealth;
        }

        public override void TakeDamageNoAnimation(int damage)
        {
            base.TakeDamageNoAnimation(damage);

            if (!isBoss)
            {
                enemyHealthBar.SetHealth(currentHealth);
            }
            else if(isBoss && enemyBossManager != null)
            {
                enemyBossManager.UpdateBossHealthBar(currentHealth, maxHealth);
                characterSoundFXManager.PlayRandomDamageSoundFX();

                if(currentHealth <= 0)
                {
                    HandleDeath();
                }

            }

        }

        public void BreakGuard()
        {
            enemyAnimatorManager.PlayTargetAnimation("BreakGuard", true);
        }

        public void TakeDamage(int damage, string damageAnimation = "Damaged")
        {
            //need to characterManager fixed and overriding takedamage function, characterstats to enemy, player stat script

            //if (isDead)
            //    return;

            if (!isBoss)
            {
                enemyHealthBar.SetHealth(currentHealth);
            }
            else if(isBoss && enemyBossManager != null)
            {
                enemyBossManager.UpdateBossHealthBar(currentHealth, maxHealth);
            }

            if (currentHealth <= 0)
            {
                HandleDeath();
            }
            else
            {
                currentHealth = currentHealth - damage;
                enemyAnimatorManager.PlayTargetAnimation(damageAnimation, true);
                characterSoundFXManager.PlayRandomDamageSoundFX();
            }

        }

        private void HandleDeath()
        {
            if(currentHealth <= 0)
            {
                currentHealth = 0;
                enemyAnimatorManager.PlayTargetAnimation("Death", true);
                isDead = true;
                winUI.gameObject.SetActive(true);
                

    }
            
        }

    }
   

   
   
}

