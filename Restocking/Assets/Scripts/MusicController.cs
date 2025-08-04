using UnityEngine;

public class MusicController : MonoBehaviour
{
    public AudioSource audioS;

    void Awake(){
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioS.volume = ((float) SettingsManager.musicVolume)/10.0f;
        GameObject musicPlayer = GameObject.FindWithTag("Music");
        if (musicPlayer != this.gameObject){
            Destroy(musicPlayer);
        }
    }

    // Update is called once per frame
    void Update()
    {
        audioS.volume = ((float) SettingsManager.musicVolume)/10.0f;
    }
}
