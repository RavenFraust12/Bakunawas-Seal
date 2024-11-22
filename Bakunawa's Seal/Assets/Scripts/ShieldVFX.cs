using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldVFX : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyVFX", 6);
    }

    void DestroyVFX()
    {
        Destroy(this.gameObject);
    }
}
