using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Fungsi untuk memuat ulang scene saat ini
    public void ReloadCurrentScene()
    {
        Time.timeScale = 1.0f;
        AudioManager.Instance.PlaySFX(1);
        // Mendapatkan nama scene yang sedang aktif
        string currentSceneName = SceneManager.GetActiveScene().name;
        
        // Memuat ulang scene tersebut
        SceneManager.LoadScene(currentSceneName);
    }

    // Fungsi untuk memuat scene dengan nama "Main Menu"
    public void LoadMainMenu()
    {
        Time.timeScale = 1.0f;
        AudioManager.Instance.PlaySFX(1);
        SceneManager.LoadScene("Main Menu");
    }

    public void ReloadSelectedScene(string name)
    {
        Time.timeScale = 1.0f;
        AudioManager.Instance.PlaySFX(1);
        // Memuat ulang scene tersebut
        SceneManager.LoadScene(name);
    }
}