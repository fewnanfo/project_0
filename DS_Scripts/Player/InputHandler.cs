using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class InputHandler : MonoBehaviour
    {
        public float horizontal;
        public float vertical;
        public float moveAmount;
        public float mouseX;
        public float mouseY;

        public bool a_Input;
        public bool b_Input;
        public bool y_Input;    
        public bool rb_Input;
        public bool rt_Input;
        public bool lb_Input;
        public bool lt_Input;
        public bool critical_Attack_Input;
        public bool jump_Input;
        public bool inventory_input;
        public bool lockOnInput;
        public bool right_Stick_Right_Input;
        public bool right_Stick_Left_Input;

        public bool d_Pad_Up;
        public bool d_Pad_Down;
        public bool d_Pad_Left;
        public bool d_Pad_Right;

        public bool rollFlag;
        public bool twoHandFlag;
        public bool sprintFlag;
        public bool comboFlag;
        public bool hvyAtkFlag;
        public bool lockOnFlag;
        public bool inventoryFlag;
        public float rollInputTimer;


        public Transform criticalAttackRayCastStartPoint;



        PlayerControl inputActions;
        PlayerCombatManager playerAttacker;
        PlayerInventory playerInventory;
        PlayerManager playerManager;
        PlayerStatsManager playerStats;
        BlockingCollider blockingCollider;
        WeaponSlotManager weaponSlotManager;
        CameraHandler cameraHandler;
        PlayerAnimatorManager playerAnimatorManager;
        UIManager uIManager;
        
        
        Vector2 movementInput;
        Vector2 cameraInput;

        private void Awake()
        {
            playerAttacker = GetComponent<PlayerCombatManager>();
            playerInventory = GetComponent<PlayerInventory>();
            playerManager = GetComponent<PlayerManager>();
            playerStats = GetComponent<PlayerStatsManager>();
            weaponSlotManager = GetComponentInChildren<WeaponSlotManager>();
            blockingCollider = GetComponentInChildren<BlockingCollider>();
            uIManager = FindObjectOfType<UIManager>();
            cameraHandler = FindObjectOfType<CameraHandler>();
            playerAnimatorManager = GetComponentInChildren<PlayerAnimatorManager>();
            
        } 


        public void TickInput(float delta)
        {
            HandleMoveInput(delta);
            HandleRollInput(delta);
            HandleCombatInput(delta);
            HandleQuickSlotsInput();
            HandleInventoryInput();
            HandleLockOnInput();
            HandleTwoHandInput();
            HandleCriticalAttackInput();
        }


        public void OnEnable()
        {
            if(inputActions == null)
            {
                inputActions = new PlayerControl();
                inputActions.PlayerMovement.Movement.performed += inputActions => movementInput = inputActions.ReadValue<Vector2>();
                inputActions.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();
                inputActions.PlayerActions.RB.performed += i => rb_Input = true;
                inputActions.PlayerActions.RT.performed += i => rt_Input = true;
                inputActions.PlayerActions.LT.performed += i => lt_Input = true;
                inputActions.PlayerActions.LB.performed += i => lb_Input = true;
                inputActions.PlayerActions.LB.canceled += i => lb_Input = false;
                inputActions.PlayerQuickSlots.dPadRight.performed += i => d_Pad_Right = true;
                inputActions.PlayerQuickSlots.dPadLeft.performed += i => d_Pad_Left = true;
                inputActions.PlayerActions.A.performed += i => a_Input = true;
                inputActions.PlayerActions.Roll.performed += i => b_Input = true;
                inputActions.PlayerActions.Roll.canceled += i => b_Input = false;
                inputActions.PlayerActions.Jump.performed += i => jump_Input = true;
                inputActions.PlayerActions.Inventory.performed += i => inventory_input = true;
                inputActions.PlayerActions.LockOn.performed += i => lockOnInput = true;
                inputActions.PlayerMovement.LockOnTargetRight.performed += i => right_Stick_Right_Input = true;
                inputActions.PlayerMovement.LockOnTargetLeft.performed += i => right_Stick_Left_Input = true;
                inputActions.PlayerActions.Y.performed += inputActions => y_Input = true;
                inputActions.PlayerActions.CriticalAttack.performed += i => critical_Attack_Input = true;
            }

            inputActions.Enable();
                    
        }

        private void OnDisable()
        {
            inputActions.Disable();
        }

        public void HandleMoveInput(float delta)
        {
            horizontal = movementInput.x;
            vertical = movementInput.y;
            moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
            mouseX = cameraInput.x;
            mouseY = cameraInput.y;
            
        }

        private void HandleRollInput(float delta)
        {
            b_Input = inputActions.PlayerActions.Roll.phase == UnityEngine.InputSystem.InputActionPhase.Performed;
            
            if (b_Input)
            {
                rollInputTimer += delta;

                if (playerStats.currentStamina <= 0)
                {
                    b_Input = false;
                    sprintFlag = false;
                }

                if(moveAmount > 0.5f && playerStats.currentStamina > 0)
                {
                    sprintFlag = true;
                }
                    
            }
            else
            {
                sprintFlag = false;

                if (rollInputTimer > 0 && rollInputTimer < 0.5f)
                {
                    sprintFlag = false;
                    rollFlag = true;
                }
                rollInputTimer = 0;
            }

        }

        private void HandleCombatInput(float delta)
        {

                if (rb_Input)
                {
                    if (playerManager.canDoCombo)
                    {
                        comboFlag = true;
                        playerAttacker.HandleWeaponCombo(playerInventory.rightWeapon);
                        comboFlag = false;
                    }
                    else
                    {
                        if (playerManager.isInteracting)
                            return;

                        if (playerManager.canDoCombo)
                            return;

                        playerAnimatorManager.anim.SetBool("isUsingRightHand", true);
                        playerAttacker.PerformRBMeleeAction();
                    }

                    playerAttacker.PerformRBMeleeAction();
                }
            
            
            if (rt_Input)
            {
                if (playerManager.isInteracting)
                    return;

                if (playerManager.canDoCombo)
                    return;

                playerAttacker.HandleHeavyAttack(playerInventory.rightWeapon);
            }

            if (lt_Input)
            {
                //else handle light attack if melee weapon
                if (twoHandFlag)
                {
                    //if two handling handle weapon art
                }
                else
                {
                    playerAttacker.HandleLTAction();
                }
            }

            if (lb_Input)
            {
                playerAttacker.HandleLBAction();
            }
            else
            {
                playerManager.isBlocking = false;

                if (blockingCollider.blockingCollider.enabled)
                {
                    blockingCollider.DisableBlockingCollider();
                }
            }

           

        }

        private void HandleQuickSlotsInput()
        {
            if (d_Pad_Right)
            {
                playerInventory.ChangeRightWeapon();
            }
            else if (d_Pad_Left)
            {
                playerInventory.ChangeLeftWeapon();
            }
        }

        private void HandleInventoryInput()
        {
            
            if (inventory_input)
            {
                inventoryFlag = !inventoryFlag;

                if (inventoryFlag)
                {
                    uIManager.OpenSelectWindow();
                    uIManager.UpdateUI();
                    uIManager.hudWindow.SetActive(false);
                }
                else
                {
                    uIManager.CloseSelectionWindow();
                    uIManager.CloseAllInventoryWindows();
                    uIManager.hudWindow.SetActive(true);
                }
            }
        }

        private void HandleLockOnInput()
        {
            if(lockOnInput && lockOnFlag == false)
            {
                
                lockOnInput = false;
                cameraHandler.HandleLockOn();

                if(cameraHandler.nearestLockOnTarget != null)
                {
                    cameraHandler.currentLockOnTarget = cameraHandler.nearestLockOnTarget;
                    lockOnFlag = true;
                }
            }
            else if(lockOnInput && lockOnFlag)
            {
                lockOnInput = false;
                lockOnFlag = false;
                cameraHandler.ClearLockOnTargets();
            }

            if (lockOnFlag && right_Stick_Left_Input)
            {
                right_Stick_Left_Input = false;
                cameraHandler.HandleLockOn();
                if(cameraHandler.leftLockTarget != null)
                {
                    cameraHandler.currentLockOnTarget = cameraHandler.leftLockTarget;
                }
            }

            if(lockOnFlag && right_Stick_Right_Input)
            {
                right_Stick_Right_Input = false;
                cameraHandler.HandleLockOn();
                if(cameraHandler.rightLockTarget != null)
                {
                    cameraHandler.currentLockOnTarget = cameraHandler.rightLockTarget;
                }
            }
            cameraHandler.SetCameraHeight();
        }

        private void HandleTwoHandInput()
        {
            if (y_Input)
            {
                y_Input = false;
                twoHandFlag = !twoHandFlag;

                if(twoHandFlag)
                {
                    weaponSlotManager.LoadWeaponOnSlot(playerInventory.rightWeapon, false);
                }
                else
                {
                    weaponSlotManager.LoadWeaponOnSlot(playerInventory.rightWeapon, false);
                    weaponSlotManager.LoadWeaponOnSlot(playerInventory.leftWeapon, true);
                }
            }
        }

        private void HandleCriticalAttackInput()
        {
            if (critical_Attack_Input)
            {
                critical_Attack_Input = false;
                playerAttacker.AttemptBackStabOrRiposte();
            }
        }
            
    }
}

