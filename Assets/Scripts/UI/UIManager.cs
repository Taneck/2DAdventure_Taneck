using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public PlayerStatBar playerStatBar;
    [Header("Events Listening")]
    public CharacterEventSO healthEvent;
    public SceneLoadEventSO unloadedSceneEvent;
    public VoidEventSO loadDataEvent;
    public VoidEventSO gameOverEvent;
    public VoidEventSO backToMenuEvent;
    public FloatEventSO syncVolumeEvent;

    [Header("Events Broadcasting")]
    public VoidEventSO pauseEvent;

    [Header("Components")]
    public GameObject gameOverPanel;
    public GameObject restartBtn;
    public GameObject mobileTouch;
    public Button settingBtn;
    public GameObject pausePanel;
    public Slider volumeSlider;

    private void Awake()
    {
        #if UNITY_STANDALONE
        mobileTouch.SetActive(false);
        #endif

        settingBtn.onClick.AddListener(TogglePausePanel);
    }

    private void OnEnable()
    {
        healthEvent.OnEventRaised += OnHealthEvent;
        unloadedSceneEvent.LoadRequestEvent += OnUnloadedSceneEvent;
        loadDataEvent.OnEventRaised += OnLoadDataEvent;
        gameOverEvent.OnEventRaised += OnGameOverEvent;
        backToMenuEvent.OnEventRaised += OnLoadDataEvent;
        syncVolumeEvent.OnEventRaised += OnSyncVolumeEvent;
    }

    private void OnDisable()
    {
        healthEvent.OnEventRaised -= OnHealthEvent;
        unloadedSceneEvent.LoadRequestEvent -= OnUnloadedSceneEvent;
        loadDataEvent.OnEventRaised -= OnLoadDataEvent;
        gameOverEvent.OnEventRaised -= OnGameOverEvent;
        backToMenuEvent.OnEventRaised -= OnLoadDataEvent;
        syncVolumeEvent.OnEventRaised -= OnSyncVolumeEvent;
    }

    private void OnHealthEvent(Character character)
    {
        var percentage = character.currentHealth/character.maxHealth;
        playerStatBar.OnHealthChange(percentage);

        playerStatBar.OnPowerChange(character);
    }
    
    private void OnUnloadedSceneEvent(GameSceneSO sceneToLoad, Vector3 posToGo, bool fadeScreen)
    {
        var isMenu = sceneToLoad.sceneType == SceneType.Menu;
        playerStatBar.gameObject.SetActive(!isMenu);
    }
    
    private void OnLoadDataEvent()
    {
        gameOverPanel.SetActive(false);
    }
    
    private void OnSyncVolumeEvent(float value)
    {
        volumeSlider.value = (value + 80) / 100f;
    }

    private void OnGameOverEvent()
    {
        gameOverPanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(restartBtn);
    }

    private void TogglePausePanel()
    {
        if (pausePanel.activeInHierarchy)
        {
            pausePanel.SetActive(false);
            Time.timeScale = 1f;
        }
        else
        {
            pauseEvent.RaiseEvent();
            pausePanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
