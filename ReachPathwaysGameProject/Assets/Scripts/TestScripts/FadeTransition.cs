using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class FadeTransition : MonoBehaviour
{
    public static FadeTransition Instance;

    [SerializeField] 
    private Transform fadeGameObject;

    [SerializeField]
    private float fadeOutDuration;

    private Image image;

    private void Awake()
    {
        //Singleton
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

    void Start()
    {
        if(fadeGameObject.GetComponent<Image>() != null)
        {
            image = fadeGameObject.GetComponent<Image>();
        }

        fadeOutDuration = fadeOutDuration == 0 ? 2 : fadeOutDuration;
    }

    public void FadeIn(float secondsDuration)
    {
        image.DOFade(1, secondsDuration).OnComplete(() => FadeOut(fadeOutDuration) );
    }

    public void FadeOut(float secondsDuration)
    {
        image.DOFade(0, secondsDuration).OnComplete(() => Debug.Log("fade out completed"));
    }

}