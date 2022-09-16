﻿using UnityEngine.InputSystem;
using Game.Services;
using UnityEngine;
using System;

namespace Game.Input
{
    public sealed class InputService : MonoBehaviour, IInputService
    {
        public event Action<PlayerInputsData> OnReadPlayerInputs;

        [Header("Input System")] 
        private PlayerInputs _playerInputs;
        private PlayerInputs.LandMapActions _landActions;
        private PlayerInputsData _playerInputsData;

        [Header("Inputs")] 
        private Vector2 _playerMovement;
        private bool _pressInteract;

        private void Awake()
        {
            ServiceLocator.RegisterService<IInputService>(this);

            DontDestroyOnLoad(gameObject);

            InitializePlayerInputs();

            EnablePlayerInputs();

            SubscribeEvents();
        }

        private void OnDestroy()
        {
            ServiceLocator.DeregisterService<IInputService>();

            DisablePlayerInputs();

            UnsubscribeEvents();
        }

        private void Update()
        {
            UpdatePlayerInputsData();

            OnReadPlayerInputs?.Invoke(_playerInputsData);

            ResetInputs();
        }

        private void SubscribeEvents()
        {
            // _landActions.Movement.performed += SetPlayerMovement;
            //
            // _landActions.Interact.performed += HandleInteract;
            // _landActions.Interact.canceled += HandleInteract;
        }

        private void UnsubscribeEvents()
        {
            // _landActions.Movement.performed -= SetPlayerMovement;
            //
            // _landActions.Interact.performed -= HandleInteract;
            // _landActions.Interact.canceled -= HandleInteract;
        }

        private void InitializePlayerInputs()
        {
            _playerInputs = new PlayerInputs();

            _landActions = _playerInputs.LandMap;
        }

        private void EnablePlayerInputs()
        {
            _playerInputs.Enable();
        }

        private void DisablePlayerInputs()
        {
            _playerInputs.Disable();
        }

        private void ResetInputs()
        {
            _pressInteract = false;
        }

        private void UpdatePlayerInputsData()
        {
            _playerInputsData.PlayerMovement = _playerMovement;
            _playerInputsData.PressInteract = _pressInteract;
        }

        private void HandleInteract(InputAction.CallbackContext context)
        {
            switch (context.phase)
            {
                case InputActionPhase.Performed:
                {
                    _pressInteract = true;
                    break;
                }
                case InputActionPhase.Canceled:
                {
                    _pressInteract = false;
                    break;
                }
            }
        }

        private void SetPlayerMovement(InputAction.CallbackContext action)
        {
            _playerMovement = action.ReadValue<Vector2>();
        }
    }
}
