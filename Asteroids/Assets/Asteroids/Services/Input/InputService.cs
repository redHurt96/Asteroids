using System;
using Asteroids.Domain.Services;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Asteroids.Services.Input
{
    public class InputService : IInputService, IDisposable
    {
        private ShipInputActions _shipInputActions;
        private InputAction _acceleration;
        private InputAction _rotation;

        public bool IsShipAccelerated => Mathf.Approximately(_acceleration.ReadValue<float>(), 1f);
        public float RotateDirection => _rotation.ReadValue<float>();

        public InputService()
        {
            _shipInputActions = new ShipInputActions();

            _acceleration = _shipInputActions.SpaceShip.Acceleration;
            _rotation = _shipInputActions.SpaceShip.Rotation;

            _acceleration.Enable();
            _rotation.Enable();
        }


        public void Dispose()
        {
            _shipInputActions.Disable();
            _acceleration.Disable();
            _rotation.Disable();
        }
    }
}
