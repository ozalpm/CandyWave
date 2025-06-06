﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using Hertzole.GoldPlayer;
using UnityEngine.Events;

namespace Hertzole.GoldPlayer
{
    /// <summary>
    /// Used for easily playing audio clip(s) with parameters.
    /// </summary>
    [System.Serializable]
    public struct AudioItem
    {
        [SerializeField]
        [EditorTooltip("Determines if this audio should be enabled.")]
        [FormerlySerializedAs("m_Enabled")]
        private bool enabled;
        [SerializeField]
        [EditorTooltip("Determines if the pitch should be randomized.")]
        [FormerlySerializedAs("m_RandomPitch")]
        private bool randomPitch;
        [SerializeField]
        [EditorTooltip("The pitch that the audio should play at.")]
        [FormerlySerializedAs("m_Pitch")]
        private float pitch;
        [SerializeField]
        [EditorTooltip("The minimum pitch the audio can play at.")]
        [FormerlySerializedAs("m_MinPitch")]
        private float minPitch;
        [SerializeField]
        [EditorTooltip("The maximum pitch the audio can play at.")]
        [FormerlySerializedAs("m_MaxPitch")]
        private float maxPitch;
        [SerializeField]
        [EditorTooltip("Determines if the volume should be changed when playing.")]
        [FormerlySerializedAs("m_ChangeVolume")]
        private bool changeVolume;
        [SerializeField]
        [Range(0f, 1f)]
        [EditorTooltip("The volume that should be set when playing.")]
        [FormerlySerializedAs("m_Volume")]
        private float volume;
        [SerializeField]
        [EditorTooltip("All the audio clips.")]
        [FormerlySerializedAs("m_AudioClips")]
        private AudioClip[] audioClips;

        /// <summary> Determines if this audio should be enabled. </summary>
        public bool Enabled { get { return enabled; } set { enabled = value; } }
        /// <summary> Determines if the pitch should be randomized. </summary>
        public bool RandomPitch { get { return randomPitch; } set { randomPitch = value; } }
        /// <summary> The pitch that the audio should play at. </summary>
        public float Pitch { get { return pitch; } set { pitch = value; } }
        /// <summary> The minimum pitch the audio can play at. </summary>
        public float MinPitch { get { return minPitch; } set { minPitch = value; } }
        /// <summary> The maximum pitch the audio can play at. </summary>
        public float MaxPitch { get { return maxPitch; } set { maxPitch = value; } }
        /// <summary> Determines if the volume should be changed when playing. </summary>
        public bool ChangeVolume { get { return changeVolume; } set { changeVolume = value; } }
        /// <summary> The volume that should be set when playing. </summary>
        public float Volume { get { return volume; } set { volume = value; } }
        /// <summary> All the audio clips. </summary>
        public AudioClip[] AudioClips { get { return audioClips; } set { audioClips = value; } }

        public AudioItem(bool enabled)
        {
            // Set enabled to the provided enabled parameter.
            this.enabled = enabled;
            // Fill in the rest with standard values.
            randomPitch = false;
            pitch = 1;
            minPitch = 1;
            maxPitch = 1;
            changeVolume = false;
            volume = 1;
            audioClips = new AudioClip[0];
        }

        public AudioItem(bool enabled, bool randomPitch, float pitch, float minPitch, float maxPitch) : this(enabled)
        {
            // Set random pitch to the provided random pitch parameter.
            this.randomPitch = randomPitch;
            // Set pitch to the provided pitch parameter.
            this.pitch = pitch;
            // Set the minimum pitch to the provided minimum pitch parameter.
            this.minPitch = minPitch;
            // Set the maximum pitch to the provided maximum pitch parameter.
            this.maxPitch = maxPitch;
            // Fill in the rest with standard values.
            changeVolume = false;
            volume = 1;
            audioClips = new AudioClip[0];
        }

        public AudioItem(bool enabled, bool randomPitch, float pitch, float minPitch, float maxPitch, bool changeVolume, float volume) : 
            this(enabled, randomPitch, pitch, minPitch, maxPitch)
        {
            // Set change volume to the provided change volume parameter.
            this.changeVolume = changeVolume;
            // Set the volume to the provided volume parameter.
            this.volume = volume;
            // Fill in the rest with standard values.
            audioClips = new AudioClip[0];
        }

        public AudioItem(bool enabled, bool randomPitch, float pitch, float minPitch, float maxPitch, bool changeVolume, float volume, AudioClip[] audioClips) : 
            this(enabled, randomPitch, pitch, minPitch, maxPitch, changeVolume, volume)
        {
            // Set audio clips to the array of clips provided.
            this.audioClips = audioClips;
        }
        
        public AudioItem(bool enabled, bool randomPitch, float pitch, float minPitch, float maxPitch, bool changeVolume, float volume, AudioClip audioClip) : 
            this(enabled, randomPitch, pitch, minPitch, maxPitch, changeVolume, volume, new AudioClip[1] { audioClip }) { }

        /// <summary>
        /// Plays a random audio clip at on a audio source and uses the settings set on the item.
        /// </summary>
        /// <param name="audioSource">The source to play the sounds on.</param>
        public void Play(UnityEvent audioSource)
        {
            // Only play if the audio item is enabled.
            if (enabled && audioSource != null)
            {
                audioSource.Invoke();
            }
        }

        public override bool Equals(object obj)
        {
#if NET_4_6 || (UNITY_2018_3_OR_NEWER && !NET_LEGACY)
            return obj is AudioItem item && Equals(item);
#else
            return obj is AudioItem && Equals((AudioItem)obj);
#endif
        }

        public bool Equals(AudioItem other)
        {
            return enabled == other.enabled && randomPitch == other.randomPitch && changeVolume == other.changeVolume && pitch == other.pitch &&
                   minPitch == other.minPitch && maxPitch == other.maxPitch && volume == other.volume && AreAudioClipsEqual(other.audioClips);
        }

        internal bool AreAudioClipsEqual(AudioClip[] other)
        {
            if (audioClips == null && other == null)
            {
                return true;
            }

            if (audioClips == null && other != null)
            {
                return false;
            }

            if (other == null)
            {
                return false;
            }

            if (audioClips.Length != other.Length)
            {
                return false;
            }

            for (int i = 0; i < audioClips.Length; i++)
            {
                if (audioClips[i] != other[i])
                {
                    return false;
                }
            }

            return true;
        }

        public override int GetHashCode()
        {
            int hashCode = -1799565391;
            hashCode = hashCode * -1521134295 + enabled.GetHashCode();
            hashCode = hashCode * -1521134295 + randomPitch.GetHashCode();
            hashCode = hashCode * -1521134295 + changeVolume.GetHashCode();
            hashCode = hashCode * -1521134295 + pitch.GetHashCode();
            hashCode = hashCode * -1521134295 + minPitch.GetHashCode();
            hashCode = hashCode * -1521134295 + maxPitch.GetHashCode();
            hashCode = hashCode * -1521134295 + volume.GetHashCode();
            hashCode = hashCode * -1521134295 + GetAudioClipsHashCode();
            return hashCode;
        }

        internal int GetAudioClipsHashCode()
        {
            if (audioClips == null)
            {
                return 0;
            }
            
            unchecked
            {
                int hash = audioClips.Length.GetHashCode();
                for (int i = 0; i < audioClips.Length; i++)
                {
                    hash = hash * 27 * (audioClips[i] == null ? 1 : audioClips[i].GetHashCode());
                }

                return hash;
            }
        }

        public static bool operator ==(AudioItem left, AudioItem right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(AudioItem left, AudioItem right)
        {
            return !(left == right);
        }
    }
}
