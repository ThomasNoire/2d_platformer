﻿using UnityEngine;

namespace PC2D
{
   

    public class PlatformerAnimation2D : MonoBehaviour
    {
        public float jumpRotationSpeed;
        public GameObject visualChild;

        private PlatformerMotor2D _motor;
        private Animator _animator;
        private bool _isJumping;
        private bool _currentFacingLeft;

      
        void Start()
        {
            _motor = GetComponent<PlatformerMotor2D>();
            _animator = visualChild.GetComponent<Animator>();
            _animator.Play("Idle");

            _motor.onJump += SetCurrentFacingLeft;
        }

      
        void Update()
        {
            if (_motor.motorState == PlatformerMotor2D.MotorState.Jumping ||
                _isJumping &&
                    (_motor.motorState == PlatformerMotor2D.MotorState.Falling ||
                                 _motor.motorState == PlatformerMotor2D.MotorState.FallingFast))
            {
                _isJumping = true;
                _animator.Play("Jump");

                if (_motor.velocity.x <= -0.1f)
                {
                    _currentFacingLeft = true;
                }
                else if (_motor.velocity.x >= 0.1f)
                {
                    _currentFacingLeft = false;
                }

                Vector3 rotateDir = _currentFacingLeft ? Vector3.forward : Vector3.back;
                visualChild.transform.Rotate(rotateDir, jumpRotationSpeed * Time.deltaTime);
            }
            else
            {
                _isJumping = false;
                visualChild.transform.rotation = Quaternion.identity;

                if (_motor.motorState == PlatformerMotor2D.MotorState.Falling ||
                                 _motor.motorState == PlatformerMotor2D.MotorState.FallingFast)
                {
                    _animator.Play("Fall");
                }
                else if (_motor.motorState == PlatformerMotor2D.MotorState.WallSliding ||
                         _motor.motorState == PlatformerMotor2D.MotorState.WallSticking)
                {
                    _animator.Play("Cling");
                }
                else if (_motor.motorState == PlatformerMotor2D.MotorState.OnCorner)
                {
                    _animator.Play("On Corner");
                }
                else if (_motor.motorState == PlatformerMotor2D.MotorState.Slipping)
                {
                    _animator.Play("Slip");
                }
                else if (_motor.motorState == PlatformerMotor2D.MotorState.Dashing)
                {
                    _animator.Play("Dash");
                }
                else
                {
                    if (_motor.velocity.sqrMagnitude >= 0.1f * 0.1f)
                    {
                        _animator.Play("Walk");
                    }
                    else
                    {
                        _animator.Play("Idle");
                    }
                }
            }

         
            float valueCheck = _motor.normalizedXMovement;

            if (_motor.motorState == PlatformerMotor2D.MotorState.Slipping ||
                _motor.motorState == PlatformerMotor2D.MotorState.Dashing ||
                _motor.motorState == PlatformerMotor2D.MotorState.Jumping)
            {
                valueCheck = _motor.velocity.x;
            }
            
            if (Mathf.Abs(valueCheck) >= 0.1f)
            {
                Vector3 newScale = visualChild.transform.localScale;
                newScale.x = Mathf.Abs(newScale.x) * ((valueCheck > 0) ? 1.0f : -1.0f);
                visualChild.transform.localScale = newScale;
            }
        }

        private void SetCurrentFacingLeft()
        {
            _currentFacingLeft = _motor.facingLeft;
        }
    }
}
