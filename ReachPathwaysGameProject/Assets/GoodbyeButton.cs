using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodbyeButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDisable()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.menuBack, transform.position);
    }
}
