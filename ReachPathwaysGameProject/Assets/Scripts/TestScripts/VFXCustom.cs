using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using DG.Tweening;

public class VFXCustom : MonoBehaviour
{
    
    public VisualEffect visualEffect;

    private float rounds;

    [SerializeField]
    private float timeDuration;

    [SerializeField]
    private string PropertyName;

    void Start()
    {
        rounds = 1;

        visualEffect.SetFloat(PropertyName, rounds);
    }

    
    public void UpdateValue()
    {
        if(rounds < 4)
        {
            float value = rounds;
            DOTween.To(() => value, x => value = x, rounds + 1, timeDuration)
                .OnUpdate(() => {
                    visualEffect.SetFloat(PropertyName, value);
                });

            rounds++;
        }
    }

}
