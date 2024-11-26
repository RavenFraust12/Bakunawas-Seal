using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldVFX : MonoBehaviour
{
    public float vfxDuration;
    private AudioSource sfx;
    void Start()
    {
        sfx = GetComponent<AudioSource>();
        Invoke("DestroyVFX", vfxDuration);
    }

    private void Update()
    {
        sfx.volume = AudioManager.instance.sfx.volume;
    }

    void DestroyVFX()
    {
        Destroy(this.gameObject);
    }
}
