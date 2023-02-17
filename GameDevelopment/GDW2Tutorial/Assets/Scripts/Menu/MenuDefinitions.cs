using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum MenuType
{
    HORIZONTAL,
    VERTICAL
}

public class MenuDefinitions : MonoBehaviour
{
    public MenuType menuType = MenuType.HORIZONTAL;
    public AudioClip menuMusic;
    public bool continuePrevMusic = false;
    public List<GameObject> menuButtonObjects = new List<GameObject>();

    private List<ButtonDefinition> menuButtonDefinitions = new List<ButtonDefinition>();
    private List<Button> menuButtons = new List<Button>();
    private List<Animator> menuAnimators = new List<Animator>();

    public void Start()
    {
        for (int i = 0; i < menuButtonObjects.Count; i++)
        {
            menuButtonDefinitions.Add(menuButtonObjects[i].GetComponent<ButtonDefinition>());
            menuButtons.Add(menuButtonObjects[i].GetComponent<Button>());

            //Grab out animator comp if it exists
            Animator temp = null;
            menuButtonObjects[i].TryGetComponent(out temp);

            //If theres no animator itll be null
            //well check for null when we check the animator
            menuAnimators.Add(temp);
        }
    }

    public MenuType GetMenuType()
    {
        return menuType;
    }

    public int GetButtonCount()
    {
        return menuButtonObjects.Count;
    }

    public List<ButtonDefinition> GetButtonDefinitions()
    {
        return menuButtonDefinitions;
    }

    public List<Button> GetButtons()
    {
        return menuButtons;
    }

    public List<Animator> GetAnimators()
    {
        return menuAnimators;
    }
}
