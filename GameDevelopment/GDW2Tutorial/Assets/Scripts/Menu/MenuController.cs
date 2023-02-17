using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject activeMenu;
    public AudioSource backgroundAudio;

    public List<KeyCode> increaseVert;
    public List<KeyCode> decreaseVert;
    public List<KeyCode> increaseHoriz;
    public List<KeyCode> decreaseHoriz;
    public List<KeyCode> confirmButtons;

    private MenuDefinitions activeMenuDefinitions;
    private int activeButton = 0;

    public void Start()
    {
        //Update active menu definitions at start
        UpdateActiveMenuDefinition();
    }

    public void Update()
    {
        switch (activeMenuDefinitions.GetMenuType())
        {
            case MenuType.HORIZONTAL:
                MenuInput(increaseHoriz, decreaseHoriz);
                break;
            case MenuType.VERTICAL:
                MenuInput(increaseVert, decreaseVert);
                break;
        }
    }

    private void MenuInput(List<KeyCode> increase, List<KeyCode> decrease)
    {
        int newActive = activeButton;

        for(int i = 0; i < increase.Count; i++)
        {
            if (Input.GetKeyDown(increase[i]))
            {
                newActive = SwitchCurrentButton(1);
            }
        }

        for (int i = 0; i < decrease.Count; i++)
        {
            if (Input.GetKeyDown(decrease[i]))
            {
                newActive = SwitchCurrentButton(-1);
            }
        }

        for (int i = 0; i < confirmButtons.Count; i++)
        {
            if (Input.GetKeyDown(confirmButtons[i]))
            {
                ClickCurrentButton();
            }
        }

        activeButton = newActive;
    }

    private void ClickCurrentButton()
    {
        if (!activeMenuDefinitions.GetButtonDefinitions()[activeButton].GetDisableControls())
        {
            StartCoroutine(activeMenuDefinitions.GetButtonDefinitions()[activeButton].ClickButton());
        }
    }
    private int SwitchCurrentButton(int increment)
    {
        if (!activeMenuDefinitions.GetButtonDefinitions()[activeButton].GetDisableControls())
        {
            int newActive = Utility.WrapAround(activeMenuDefinitions.GetButtonCount(), activeButton, increment);

            activeMenuDefinitions.GetButtonDefinitions()[activeButton].SwappedOff();
            activeMenuDefinitions.GetButtonDefinitions()[newActive].SwappedTo();
            return newActive;
        }
        return activeButton;
    }



    public void UpdateActiveMenuDefinition()
    {
        activeMenuDefinitions = activeMenu.GetComponent<MenuDefinitions>();

        if (activeMenuDefinitions.menuMusic != null)
        {
            backgroundAudio.clip = activeMenuDefinitions.menuMusic;
            backgroundAudio.Play();
        }
        else if (!activeMenuDefinitions.continuePrevMusic)
        {
            backgroundAudio.Stop();
        }
    }

    public void SetActiveMenu(GameObject theActiveMenu)
    {
        //Set active menu
        activeMenu = theActiveMenu;

        //Make sure to update definition
        UpdateActiveMenuDefinition();
    }
}
