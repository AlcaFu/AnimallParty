using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    public AudioSource musicSource; // Untuk memutar musik
    public AudioSource sfxSource;   // Untuk memutar SFX sementara
    public AudioSource loopedSfxSource; // Untuk memutar SFX secara loop

    [Header("Audio Clips")]
    public AudioClip[] musicClips;  // Array musik
    public AudioClip[] sfxClips;    // Array SFX

    private void Awake()
    {
        // Pastikan objek ini tidak dihancurkan saat pindah scene
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); // Hancurkan duplikat
        }
    }

    /// <summary>
    /// Memutar musik berdasarkan indeks dalam array
    /// </summary>
    /// <param name="musicIndex">Indeks musik yang akan diputar</param>
    public void PlayMusic(int musicIndex)
    {
        if (musicIndex < 0 || musicIndex >= musicClips.Length)
        {
            Debug.LogWarning("Music index out of range!");
            return;
        }

        if (musicSource.isPlaying)
        {
            musicSource.Stop(); // Hentikan musik yang sedang dimainkan
        }

        musicSource.clip = musicClips[musicIndex];
        musicSource.Play();
    }

    /// <summary>
    /// Memutar SFX satu kali berdasarkan indeks dalam array
    /// </summary>
    /// <param name="sfxIndex">Indeks SFX yang akan diputar</param>
    public void PlaySFX(int sfxIndex)
    {
        if (sfxIndex < 0 || sfxIndex >= sfxClips.Length)
        {
            Debug.LogWarning("SFX index out of range!");
            return;
        }

        sfxSource.PlayOneShot(sfxClips[sfxIndex]); // Memutar SFX sementara tanpa menghentikan yang lain
    }

    /// <summary>
    /// Memutar SFX secara loop berdasarkan indeks dalam array
    /// </summary>
    /// <param name="sfxIndex">Indeks SFX yang akan diputar secara loop</param>
    public void PlayLoopedSFX(int sfxIndex)
    {
        if (sfxIndex < 0 || sfxIndex >= sfxClips.Length)
        {
            Debug.LogWarning("SFX index out of range!");
            return;
        }

        if (loopedSfxSource.isPlaying)
        {
            loopedSfxSource.Stop(); // Hentikan loop SFX sebelumnya jika ada
        }

        loopedSfxSource.clip = sfxClips[sfxIndex];
        loopedSfxSource.loop = true; // Aktifkan mode loop
        loopedSfxSource.Play();
    }

    /// <summary>
    /// Menghentikan SFX yang sedang diputar secara loop
    /// </summary>
    public void StopLoopedSFX()
    {
        if (loopedSfxSource.isPlaying)
        {
            loopedSfxSource.Stop();
        }
    }
}
