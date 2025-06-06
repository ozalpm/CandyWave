﻿using UnityEngine;
using UnityEngine.Events;

namespace Hertzole.GoldPlayer
{
    /// <summary>
    /// Used to make custom player audio behaviours.
    /// </summary>
    [DisallowMultipleComponent]
    [AddComponentMenu("")]
    public abstract class PlayerAudioBehaviour : MonoBehaviour
    {
        [SerializeField]
        [EditorTooltip("If true, this will handle all the audio handling. Else the Player Audio module will handle it.")]
        private bool independentAudioHandling = false;

#if UNITY_EDITOR || GOLD_PLAYER_DISABLE_OPTIMIZATIONS
        /// <summary> The player controller. </summary>
        protected GoldPlayerController PlayerController { get; private set; }
        /// <summary> The player input. </summary>
        protected IGoldInput PlayerInput { get; private set; }

        /// <summary> The audio source where all the footsteps sounds will be played. </summary>
        protected UnityEvent FootstepsSource { get; private set; }
        /// <summary> The audio source where all the jump sounds will be played. </summary>
        protected UnityEvent JumpSource { get; private set; }
        /// <summary> The audio source where all the land sounds will be played. </summary>
        protected UnityEvent LandSource { get; private set; }
#else
        protected GoldPlayerController PlayerController;
        protected IGoldInput PlayerInput;
        protected UnityEvent FootstepsSource;
        protected UnityEvent JumpSource;
        protected UnityEvent LandSource;
#endif

        /// <summary> If true, this will handle all the audio handling. Else the Player Audio module will handle it. </summary>
        public virtual bool IndependentAudioHandling { get { return independentAudioHandling; } set { independentAudioHandling = value; } }

        /// <summary>
        /// Initializes the audio behaviour.
        /// </summary>
        /// <param name="footstepsSource"></param>
        /// <param name="jumpSource"></param>
        /// <param name="landSource"></param>
        public void Initialize(GoldPlayerController playerController, IGoldInput input, UnityEvent footstepsSource, UnityEvent jumpSource, UnityEvent landSource)
        {
            PlayerController = playerController;
            PlayerInput = input;

            FootstepsSource = footstepsSource;
            JumpSource = jumpSource;
            LandSource = landSource;
            
            OnInitialized();
        }

        /// <summary>
        /// Called when the behaviour has been initialized.
        /// </summary>
        protected virtual void OnInitialized() { }

        /// <summary>
        /// Called every frame.
        /// </summary>
        public virtual void OnUpdate(float deltaTime) { }

        /// <summary>
        /// Called at every fixed framerate frame.
        /// </summary>
        public virtual void OnFixedUpdate(float fixedDeltaTime) { }

        /// <summary>
        /// Called every frame, after OnUpdate.
        /// </summary>
        public virtual void OnLateUpdate(float deltaTime) { }

        /// <summary>
        /// Called when a footstep should be played.
        /// </summary>
        public abstract void PlayFootstepSound();

        /// <summary>
        /// Called when a jump sound should be played.
        /// </summary>
        public abstract void PlayJumpSound();

        /// <summary>
        /// Called when a land sound should be played.
        /// </summary>
        public abstract void PlayLandSound();
    }
}
