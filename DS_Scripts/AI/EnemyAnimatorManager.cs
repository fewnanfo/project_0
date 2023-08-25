using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class EnemyAnimatorManager : AnimatorManager
    {
        EnemyManager enemyManager;
        EnemyBossManager enemyBossManager;
        EnemyEffectsManager enemyEffectsManager;


        protected override void Awake()
        {
            base.Awake();
            enemyManager = GetComponentInParent<EnemyManager>();
            anim = GetComponent<Animator>();
            enemyBossManager = GetComponentInParent<EnemyBossManager>();
            enemyEffectsManager = GetComponentInParent<EnemyEffectsManager>();
        }
        

        public void InstantiateBossParticleFX()
        {
            BossFxTransform bossFxTransform = GetComponentInChildren<BossFxTransform>();
            GameObject phaseFX = Instantiate(enemyBossManager.particaleFX, bossFxTransform.transform);
        }

        public void PlayWeaponTrailFX()
        {
            enemyEffectsManager.PlayWeaponFX(false);
        }

        private void OnAnimatorMove()
        {
            float delta = Time.deltaTime;
            enemyManager.enemyRigidbody.drag = 0;
            Vector3 deltaPosition = anim.deltaPosition;
            deltaPosition.y = 0;
            Vector3 velocity = deltaPosition / delta;
            enemyManager.enemyRigidbody.velocity = velocity;

            if (enemyManager.isRotatingWithRootMotion)
            {
                enemyManager.transform.rotation *= anim.deltaRotation;
            }
        }
    }
}

