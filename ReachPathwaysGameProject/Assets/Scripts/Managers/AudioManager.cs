using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    // Place all events you want to have stored as comma separated EventReference variables, then assign them to the corresponding FMOD events in the inspector
    [SerializeField]
    public EventReference ambience;
    private EventInstance ambienceInstance;

    private List<EventInstance> instances = new();

    private void Awake()
    {
        // Singleton - If there is an instance, and it's not me, delete myself
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    // UNCOMMENT FOR AMBIENCE SFX ON SCRIPT LOAD
    // private void Start()
    // {
    //     ambienceInstance.getPlaybackState(out PLAYBACK_STATE state);
    //     if (state == PLAYBACK_STATE.STOPPED)
    //     {
    //         ambienceInstance = (EventInstance)PlaySFX(ambience, null, gameObject, true);
    //     }
    // }

    // Primary function for audio playback - overloaded method that allows for playback of both one-shots and instances. Returns the instance if an instance is made for future reference, and returns null if it plays a one-shot.
    // Example usage
    // One-shot: AudioManager.Instance.PlaySFX(fmodEvent, transform.position)
    // Instance: AudioManager.Instance.PlaySFX(bgmEvent, AudioManager.Instance.transform.position, AudioManager.Instance.gameObject, true)
    public EventInstance? PlaySFX(EventReference eventRef, Vector3? position = null, GameObject parent = null, bool isInstance = false)
    {
        // For spatialization - Sets position equal to the non-nullable Vector3 type if it isn't null, or zero if it is null
        Vector3 pos = position != null ? (Vector3)position : Vector3.zero;

        // Plays the sound as a one-shot if isInstance is false - this is the default mode
        if (isInstance == false)
        {
            RuntimeManager.PlayOneShot(eventRef, pos);
            return null;
        }
        // If isInstance is true, create an FMOD event, attach it to the desired parent for spatialization, start it, then store and return the reference
        else
        {
            EventInstance instance = RuntimeManager.CreateInstance(eventRef);
            RuntimeManager.AttachInstanceToGameObject(instance, parent.transform);
            instance.start();
            AddInstance(instance);
            return instance;
        }

    }

    // Saves the event instance's reference in the AudioManager's instance list
    public void AddInstance(EventInstance inst)
    {
        instances.Add(inst);
    }

    // Stops an active event and removes it from the AudioManager's instance list
    public void RemoveInstance(EventInstance instance)
    {
        instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        instances.Remove(instance);
    }

    // Stops and removes all instances from the AudioManager's instance list
    private void ClearInstances()
    {
        foreach (EventInstance instance in instances)
        {
            instance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            instances.Remove(instance);
        }
    }

    void OnDestroy()
    {
        ClearInstances();
    }
}
