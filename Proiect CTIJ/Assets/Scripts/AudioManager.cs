using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Audio Sources")]
    public AudioSource coinCollectSource;
    public AudioSource damageSource;
    public AudioSource levelCompleteSource;

    private AudioClip coinClip;
    private AudioClip impactClip;
    private AudioClip levelClip;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        coinClip = Resources.Load<AudioClip>("audio/coin");
        impactClip = Resources.Load<AudioClip>("audio/impact");
        levelClip = Resources.Load<AudioClip>("audio/level");
    }

    public void PlayCoinSound()
    {
        if (coinCollectSource != null && coinClip != null)
        {
            coinCollectSource.clip = coinClip;
            coinCollectSource.PlayOneShot(coinClip);
        }
    }

    public void PlayDamageSound()
    {
        if (damageSource != null && impactClip != null)
        {
            damageSource.clip = impactClip;
            damageSource.PlayOneShot(impactClip);
        }
    }

    public void PlayLevelCompleteSound()
    {
        if (levelCompleteSource != null && levelClip != null)
        {
            levelCompleteSource.clip = levelClip;
            levelCompleteSource.PlayOneShot(levelClip);
        }
    }
}
