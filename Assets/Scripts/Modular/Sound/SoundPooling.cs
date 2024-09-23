using RepeatUtils.DesignPattern.SingletonPattern;
using ScriptableObjects;
using System.Collections.Generic;
using UnityEngine;

namespace Sound
{
    public class SoundPooling : Singleton<SoundPooling>
    {
        private List<SoundEmitter> soundEmitterPool = new ();
        private List<SoundEmitter> frequentSoundEmitters = new();

        [SerializeField] 
        private SoundEmitter soundEmitterPrefab;
        
        [SerializeField] 
        private int maxPoolSize = 100;
        
        [SerializeField] 
        private int maxFrequentSoundEmitters = 30;

        public void CreateSound(SoundSO soundSO, Vector3 position, float minRandomPitch, float maxRandomPitch)
        {
            if (!CanPlaySound(soundSO)) return;
            SoundEmitter soundEmitter = GetFromPool(soundSO);
            soundEmitter.gameObject.SetActive(true);
            soundEmitter.transform.position = position;
            soundEmitter.SetRandomPitch(minRandomPitch, maxRandomPitch);

            if(soundSO.FrequentSound) frequentSoundEmitters.Add(soundEmitter);
            soundEmitter.Play();
        }

        public bool CanPlaySound(SoundSO soundSO)
        {
            if(soundEmitterPool.Count >= maxPoolSize) return false;
            if (!soundSO.FrequentSound) return true;
            return frequentSoundEmitters.Count < maxFrequentSoundEmitters;
        }

        private SoundEmitter GetFromPool(SoundSO soundSO)
        {
            foreach (var soundEmmit in soundEmitterPool)
            {
                if (!soundEmmit.gameObject.activeSelf && soundEmmit.SoundSO == soundSO)
                {
                    return soundEmmit;
                }
            }

            SoundEmitter newSoundEmitter = Instantiate(soundEmitterPrefab).Initialize(soundSO);
            newSoundEmitter.transform.parent = transform;
            newSoundEmitter.transform.name = soundSO.name;
            soundEmitterPool.Add(newSoundEmitter);
            return newSoundEmitter;
        }

        public void Despawn(SoundEmitter soundEmitter)
        {
            soundEmitter.gameObject.SetActive(false);
            if (soundEmitter.SoundSO.FrequentSound && frequentSoundEmitters.Contains(soundEmitter))
            {
                frequentSoundEmitters.Remove(soundEmitter);
            }

            if(soundEmitterPool.Count >= maxPoolSize && soundEmitterPool.Contains(soundEmitter))
            {
                soundEmitterPool.Remove(soundEmitter);
            }
        }

        public void StopAll()
        {
            soundEmitterPool.ForEach(soundEmitter => soundEmitter.Stop());
            frequentSoundEmitters.Clear();
        }
    }
}
