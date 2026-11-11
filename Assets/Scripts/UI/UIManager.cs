using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public PlayerStatBar playerStatBar;
    [Header("Events Listening")]
    public CharacterEventSO healthEvent;
    public SceneLoadEventSO loadEvent;

    private void OnEnable()
    {
        healthEvent.OnEventRaised += OnHealthEvent;
        loadEvent.LoadRequestEvent += OnLoadEvent;
    }

    private void OnDisable()
    {
        healthEvent.OnEventRaised -= OnHealthEvent;
        loadEvent.LoadRequestEvent -= OnLoadEvent;
    }

    private void OnHealthEvent(Character character)
    {
        var percentage = character.currentHealth/character.maxHealth;
        playerStatBar.OnHealthChange(percentage);

        playerStatBar.OnPowerChange(character);
    }
    
    private void OnLoadEvent(GameSceneSO sceneToLoad, Vector3 posToGo, bool fadeScreen)
    {
        var isMenu = sceneToLoad.sceneType == SceneType.Menu;
        playerStatBar.gameObject.SetActive(!isMenu);
    }
}
