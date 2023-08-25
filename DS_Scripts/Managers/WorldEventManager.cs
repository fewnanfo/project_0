using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class WorldEventManager : MonoBehaviour
    {
        //fog wall

        UIBossHealthBar bossHealthBar;
        EnemyBossManager boss;

        public bool bossFightIsActive; // currently fighting boss
        public bool bossHasBeebAwakend;// woke the boss wached cutsecene but died during fight
        public bool bossHasBeenDefeated;//boss has been defeated

        private void Awake()
        {
            bossHealthBar = FindObjectOfType<UIBossHealthBar>();
        }

        public void ActivateBossFight()
        {
            bossFightIsActive = true;
            bossHasBeebAwakend = true;
            bossHealthBar.SetUIHealthBarToActive();
            //active fog wall
        }

        public void BossHasBeenDefeated()
        {
            bossHasBeenDefeated = true;
            bossFightIsActive = false;

            //deactivate fog wall
        }
    }

    

}

