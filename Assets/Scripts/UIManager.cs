using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject MainMenu;
    [SerializeField] private Button StartButton;
    [SerializeField] private Button Settings;
    [SerializeField] private Button ExitButtonMain;

    [SerializeField] private GameObject PauseMenu;
    [SerializeField] private Button ResumeButton;
    [SerializeField] private Button MainMenuButton;
    [SerializeField] private Button ExitButtonPause;

    [SerializeField] private GameObject HudMenu;
    [SerializeField] private TMP_Text LevelsText;
    [SerializeField] private TMP_Text HealthText;
    [SerializeField] private TMP_Text StaminaText;
    [SerializeField] private TMP_Text ExperienceText;
    [SerializeField] private Slider HealthSlider;
    [SerializeField] private Slider StaminaSlider;
    [SerializeField] private Slider ExperienceSlider;

    private Player m_Player;

    private void Awake()
    {
        StartButton.onClick.AddListener(Play);
        ResumeButton.onClick.AddListener(Resume);
        MainMenuButton.onClick.AddListener(Quit);
        ExitButtonMain.onClick.AddListener(Exit);
        ExitButtonPause.onClick.AddListener(Exit);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(m_Player != null)
        {
            Refresh();
        }
        
        else
        {
            Show(MainMenu);
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    private void Refresh()
    {
        LevelsText.text = m_Player.Level.ToString();

        HealthSlider.maxValue = m_Player.Vitality;
        StaminaSlider.maxValue = m_Player.Endurance;
        ExperienceSlider.maxValue = m_Player.Required;

        HealthSlider.value = m_Player.Health;
        StaminaSlider.value = m_Player.Stamina;
        ExperienceSlider.value = m_Player.Experience;

        HealthText.text = m_Player.Health.ToString("0") + "/" + m_Player.Vitality;
        StaminaText.text = m_Player.Stamina + "/" + m_Player.Endurance;
        ExperienceText.text = m_Player.Experience + "/" + m_Player.Required;
    }

    public void Show(GameObject Menu)
    {
        Menu.SetActive(true);
    }

    public void Hide(GameObject Menu)
    {
        Menu.SetActive(false);
    }

    private void Play()
    {
        GameManager.Instance.Play();
        m_Player = GameObject.Find("Player").GetComponent<Player>();

        Refresh();
        Hide(MainMenu);
        Show(HudMenu);
    }

    private void Pause()
    {
        Show(PauseMenu);
        Time.timeScale = 0.0f;
        
        GameManager.Instance.Paused = true;
    }

    private void Resume()
    {
        Hide(PauseMenu);
        Time.timeScale = 1.0f;
    }

    private void Quit()
    {
        Time.timeScale = 1.0f;
        GameManager.Instance.Paused = true;
        SceneManager.LoadScene(0);
    }

    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
