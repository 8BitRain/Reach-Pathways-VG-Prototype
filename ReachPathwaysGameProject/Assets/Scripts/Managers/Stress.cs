using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stress : MonoBehaviour
{
    public static Stress Instance;

    public int stressLevel { get; private set; }

    private int maxStressLevel;

    [SerializeField]
    private StressUI stressUI;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        maxStressLevel = maxStressLevel == 0 || maxStressLevel < 5 ? 5 : maxStressLevel; 
        stressLevel = 0;

        stressUI.SetStressLevel(stressLevel);
    }

    public void IncreaseStress()
    {
        if(stressLevel < maxStressLevel)
        {
            stressLevel++;
        }
        else
        {
            ResetStressValue();
        }

        stressUI.SetStressLevel(stressLevel);
    }

    public void DescreaseStress()
    {
        if(stressLevel > 0)
        {
            stressLevel--;
        }

        stressUI.SetStressLevel(stressLevel);
    }

    private void ResetStressValue()
    {
        stressLevel = 0;
    }
}
