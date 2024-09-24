using Manager;
using RepeatUtils;
using ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sound
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundEmitter : RepeatMonoBehaviour
    {
        [SerializeField]
        private AudioSource audioSource;
        
        private Coroutine playingCoroutine;
        private SoundSO soundSO;

        public SoundSO SoundSO => soundSO;

        protected override void Awake()
        {
            base.Awake();
            LoadComponent(ref audioSource, gameObject);
        }

        public SoundEmitter Initialize(SoundSO soundSO)
        {
            this.soundSO = soundSO;
            audioSource.volume = soundSO.Volume;
            audioSource.clip = soundSO.AudioClip;
            audioSource.outputAudioMixerGroup = soundSO.AudioMixerGroup;
            audioSource.loop = soundSO.Loop;
            audioSource.playOnAwake = soundSO.PlayOnAwake;
            return this;
        }

        public void Play()
        {
            if (playingCoroutine != null)
            {
                StopCoroutine(playingCoroutine);
            }

            audioSource.Play();
            playingCoroutine = StartCoroutine(WaitForSoundToEnd());
        }

        private IEnumerator WaitForSoundToEnd()
        {
            yield return new WaitWhile(() => audioSource.isPlaying);
            Stop();
        }

        public void Stop()
        {
            if (playingCoroutine != null)
            {
                StopCoroutine(playingCoroutine);
                playingCoroutine = null;
            }

            audioSource.Stop();
            SoundPooling.Instance.Despawn(this);
        }

        public void SetRandomPitch(float min = -0.05f, float max = 0.05f)
        {
            audioSource.pitch += Random.Range(min, max);
        }
    }
}
