using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class UISoundManager : MonoBehaviour
{
    public static UISoundManager Instance;

    [Header("Sons Padrão")]
    [SerializeField] private AudioClip somClique;
    [SerializeField] private AudioClip somHover;

    [Header("Volume")]
    [Range(0f, 1f)]
    [SerializeField] private float volumeClique = 1f;

    [Range(0f, 1f)]
    [SerializeField] private float volumeHover = 0.6f;

    private AudioSource audioSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            audioSource = GetComponent<AudioSource>();

            audioSource.playOnAwake = false;
            audioSource.loop = false;
            audioSource.spatialBlend = 0f;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void TocarClique()
    {
        if (somClique == null)
            return;

        audioSource.PlayOneShot(somClique, volumeClique);
    }

    public void TocarHover()
    {
        if (somHover == null)
            return;

        audioSource.PlayOneShot(somHover, volumeHover);
    }

    public void TocarSom(AudioClip clip, float volume = 1f)
    {
        if (clip == null)
            return;

        audioSource.PlayOneShot(clip, volume);
    }
}