using JetBrains.Annotations;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;



[System.Serializable] // Permet à Unity de sérialiser cette classe et d'afficher ses champs dans l'inspecteur
public class Data
{
    public int starsPerLevel; // Tableau d'entiers
    public bool[] bools; // Tableau de booléens

    // Constructeur pour initialiser les tableaux avec la taille spécifiée
    public Data(int size)
    {
        
        bools = new bool[3]; // Crée un tableau de 3 booléens pour chaque entier
    }
}
public class SettingSystem : MonoBehaviour
{
    
    [SerializeField]private  Animator animator;


    [SerializeField] private AudioMixer audioMixer;

    [Scene]
    public string sceneToKeepObjects;

    //[Scene]
    //public string hideInMenu;

    [Scene]
    public string showStars;

    //[Scene]
    //public string levelToLoad;


    private bool animationForward = false;

    bool isVibration = true;


    private string exposedParameterName = "SFX";

    private float sfxBaseVolume;

    public int nbStars;
    public TextMeshProUGUI nbStarsText;
    [SerializeField] private GameObject starsVisuel;
    [SerializeField] private GameObject homeButton;


    public Data[] donnees;


    public int levelNumber;

    public static SettingSystem instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

    }




    private void Start()
    {
        if (audioMixer == null)
        {
            Debug.LogError("AudioMixer non défini dans le script MixerVolumeGetter !");
            return;
        }
        DontDestroyOnLoad(gameObject);

        sfxBaseVolume = GetMixerVolume();
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name != sceneToKeepObjects)
        {
            DisableObjects();
        }
        else 
        {
            EnableObjects();
        }
        //if (SceneManager.GetActiveScene().name == hideInMenu)
        //{
        //    UnHideObjects();
        //}
        if (SceneManager.GetActiveScene().name == showStars)
        {
            ShowTotalStars();
        }
        else
        {
            UnShowTotalStars();
        }




        nbStarsText.text = nbStars.ToString();

    }
    float GetMixerVolume()
    {
        float volume = 0f;
        bool result = audioMixer.GetFloat(exposedParameterName, out volume);
        if (!result)
        {
            Debug.LogError("Impossible d'obtenir le volume de l'AudioMixer pour le paramètre exposé: " + exposedParameterName);
        }
        return volume;
    }


    public void SystemMenu()
    {
        if (animationForward)
        {
            animator.SetBool("IsOn", false);
            animationForward = false;
        }
        else
        {
            animator.SetBool("IsOn", true);
            animationForward = true;
        }
        
    }
 
    public void QuitGame()
    {
        Application.Quit();
        AudioManager.instance.PlayRandom(SoundState.BACKBUTTON);
    }
    public void BackHomeMenu()
    {
        SceneManager.LoadScene("LevelSelection");
        AudioManager.instance.PlayRandom(SoundState.BACKBUTTON);
    }
    public void Settings()
    {
        AudioManager.instance.PlayRandom(SoundState.SETTINGS);
    }

    public void EnableVibration()
    {
        // Activer les vibrations du téléphone
        Handheld.Vibrate();
        isVibration = true;
        AudioManager.instance.PlayRandom(SoundState.BUTTON);

        Debug.Log("Vibration activée");
    }

    public void Vibrate()
    {
        if(isVibration) Handheld.Vibrate();
    }

    // Fonction pour désactiver les vibrations
    public void DisableVibration()
    {
        // Désactiver les vibrations du téléphone
        isVibration = false;
        AudioManager.instance.PlayRandom(SoundState.BACKBUTTON);
        Debug.Log("Vibration désactivée");
    }

    public void EnableMusic()
    {
        AudioManager.instance.Unpaused(SoundState.MENU);
        AudioManager.instance.PlayRandom(SoundState.BUTTON);
    }

    public void DisableMusic()
    {
        AudioManager.instance.Paused(SoundState.MENU);
        AudioManager.instance.PlayRandom(SoundState.BACKBUTTON);


    }
    public void EnableSFX()
    {
        audioMixer.SetFloat(exposedParameterName, sfxBaseVolume);
        AudioManager.instance.PlayRandom(SoundState.BUTTON);
    }

    public void DisableSFX()
    {
        audioMixer.SetFloat(exposedParameterName, -80f);
        AudioManager.instance.PlayRandom(SoundState.BACKBUTTON);

    }



    void DisableObjects()
    {
        GameObject[] objectsToDisable = GameObject.FindGameObjectsWithTag("ObjectsToDisable");
        foreach (GameObject obj in objectsToDisable)
        {
            obj.SetActive(false);
        }
    }
    void EnableObjects()
    {
        GameObject[] objectsToDisable = GameObject.FindGameObjectsWithTag("ObjectsToDisable");
        foreach (GameObject obj in objectsToDisable)
        {
            obj.SetActive(true);
        }
    }

    //void UnHideObjects()
    //{
    //    homeButton.SetActive(true);
    //}

    void ShowTotalStars()
    {
        starsVisuel.SetActive(true);

    }
    void UnShowTotalStars()
    {
        starsVisuel.SetActive(false);

    }


}
