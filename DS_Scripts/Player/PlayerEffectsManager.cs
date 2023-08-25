using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{

    public class PlayerEffectsManager : CharacterEffectsManager
    {
        PlayerStatsManager playerStatsManager;
        WeaponSlotManager playerweaponSlotManager;

        public GameObject currentParticleFX;
        public GameObject instantiatedFXModel;
        public int amountToBeHealed;

        private void Awake()
        {
            playerStatsManager = GetComponent<PlayerStatsManager>();
            playerweaponSlotManager = GetComponentInChildren<WeaponSlotManager>();
        }
    }

}
