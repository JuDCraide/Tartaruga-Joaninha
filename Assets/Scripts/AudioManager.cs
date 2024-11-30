using UnityEngine;
using UnityEngine.Audio;
using System;
using System.Collections;
using UnityEngine.Pool;
using System.Collections.Generic;
using System.Threading;

class AudioSourcePool {
    public List<AudioSource> pooledObjects;
    public int amountToPool;
    static Mutex _mutex = new Mutex();

    public AudioSourcePool(int amountToPool) {
        this.amountToPool = amountToPool;
        pooledObjects = new List<AudioSource>(amountToPool);
    }

    public AudioSource GetPooledObject() {
        _mutex.WaitOne();
        AudioSource gameObject = null;
        var pollCount = pooledObjects.Count;

        for (int i = 0; i < pollCount; i++) {
            if (!pooledObjects[i].isPlaying) {
                gameObject = pooledObjects[i];
            }
        }
        if (!gameObject) {
            if (pollCount < amountToPool) {
                gameObject = AudioManager.instance.gameObject.AddComponent<AudioSource>();
            }
            else {
                gameObject = pooledObjects[0];
                pooledObjects.RemoveAt(0);
            }
        }
        pooledObjects.Add(gameObject);
        _mutex.ReleaseMutex();
        return gameObject;
    }

}

public class AudioManager : MonoBehaviour {

    public static AudioManager instance;
    public Sound bgMusic;
    private AudioSource musicSource;
    private AudioSourcePool audioSourcePool;

    void Awake() {
        audioSourcePool = new AudioSourcePool(5);
        if (instance == null) {
            instance = this;
        }
        else {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

    }

    void Start() {
        musicSource = AudioManager.instance.gameObject.AddComponent<AudioSource>();
        bgMusic.InitializeSound(musicSource);
        musicSource.Play();
    }

    public void Play(Sound s) {
        AudioSource source = audioSourcePool.GetPooledObject();
        s.InitializeSound(source);
        source.Play();
    }
}
