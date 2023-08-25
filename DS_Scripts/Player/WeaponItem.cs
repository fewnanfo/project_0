using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    [CreateAssetMenu(menuName = "Items/Weapon Item")]

    public class WeaponItem : Item
    {
        public GameObject modelPrefab;
        public bool isUnarmed;

        [Header("Damage")]
        public int baseDamage = 25;
        public int criticalDamageMultipler = 4;

        [Header("Poise")]
        public float poiseBreak;
        public float offensivePoiseBonus;

        [Header("Absorption")]
        public float physicalDamageAbsortion;

        [Header("Idle Animations")]
        public string right_Hand_idle;
        public string left_Hand_idle;
        public string th_idle;

        [Header("Weapon Art")]
        public string weapon_art;

        [Header("One Handed Attack Animations")]
        public string OH_Light_Attack_1;
        public string OH_Light_Attack_2;
        public string OH_Light_Attack_3;
        public string OH_Light_Attack_4;

        public string TH_Light_Attack_1;
        public string TH_Light_Attack_2;
        public string TH_Light_Attack_3;
        public string TH_Light_Attack_4;

        public string TH_Heavy_Attack_1;
        public string OH_Heavy_Attack_1;

        [Header("Stamina Costs")]
        public int baseStamina;
        public float lightAttackMultipllier;
        public float heavyAttackMultipllier;

        [Header("Weapon Type")]
        public bool isMeleeWeapon;
        public bool isShieldWeapon;

        [Header("Sound FX")]
        public AudioClip[] weaponSlashes;
    }

    

}
