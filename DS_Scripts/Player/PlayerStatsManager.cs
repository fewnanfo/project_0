using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class PlayerStatsManager : CharacterStatsManager
    {
        PlayerManager playerManager;

        HealthBar healthBar;
        StaminaBar staminaBar;
        PlayerAnimatorManager animatorHandler;
        CharacterSoundFXManager characterSoundFXManager;

        public float staminaRegenerationAmount = 10;
        public float staminaRegenerationTimer = 0;

        public GameObject defeatUI;

        private void Awake()
        {
            playerManager = GetComponent<PlayerManager>();
            healthBar = FindObjectOfType<HealthBar>();
            staminaBar = FindObjectOfType<StaminaBar>();
            animatorHandler = GetComponentInChildren<PlayerAnimatorManager>();
            characterSoundFXManager = GetComponent<CharacterSoundFXManager>();
        }

        void Start()
        {
            maxHealth = SetMaxHealthFromHealthLevel();
            currentHealth = maxHealth;
            healthBar.SetMaxHealth(maxHealth);
            healthBar.SetCurrenthealth(currentHealth);

            maxStamina = SetMaxStaminaFromStaminaLevel();
            currentStamina = maxStamina;
            staminaBar.SetMaxStamina(maxStamina);
            staminaBar.SetCurrentStamina(currentStamina);
        }
        public override void HandlePoiseResetTimer()
        {
            if (poiseResetTimer > 0)
            {
                poiseResetTimer = poiseResetTimer - Time.deltaTime;
            }
            else if(poiseResetTimer <= 0 && playerManager.isInteracting)
            {
                totalPoiseDefence = armorPoiseBonus;
            }
        }

        private int SetMaxHealthFromHealthLevel()
        {
            maxHealth = healthLevel * 10;
            return maxHealth;
        }

        private float SetMaxStaminaFromStaminaLevel()
        {
            maxStamina = staminaLevel * 10;
            return maxStamina;
        }

        
        public void TakeDamage(int damage, string damageAnimation = "Damaged")
        {
            if (playerManager.isInvulerable)
                return;

            if (isDead)
            {
                return;
            }
            currentHealth = currentHealth - damage;
            healthBar.SetCurrenthealth(currentHealth);
            animatorHandler.PlayTargetAnimation(damageAnimation, true);
            characterSoundFXManager.PlayRandomDamageSoundFX();
            

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                isDead = true;
                animatorHandler.PlayTargetAnimation("Death", true);
                defeatUI.gameObject.SetActive(true);
            }
        }

        public override void TakeDamageNoAnimation(int damage)
        {
            base.TakeDamageNoAnimation(damage);
            healthBar.SetCurrenthealth(currentHealth);
        }

        public void TakeStaminaDamage(int damage)
        {
            currentStamina = currentStamina - damage;

            staminaBar.SetCurrentStamina(currentStamina);
        }

        public void RegenerateStamaina()
        {
            if (playerManager.isInteracting)
            {
                staminaRegenerationTimer = 0;
            }
            else
            {
                staminaRegenerationTimer += Time.deltaTime;
            
                if (currentStamina < maxStamina && staminaRegenerationTimer > 1f)
                {
                    currentStamina += staminaRegenerationAmount * Time.deltaTime;
                    staminaBar.SetCurrentStamina(Mathf.RoundToInt(currentStamina));
                }
            }            
        }
    }
}

