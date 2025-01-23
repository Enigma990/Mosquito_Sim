using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectMosquitoUI : MonoBehaviour
{
    [SerializeField] private Button queenButton;
    [SerializeField] private Button assassinButton;
    [SerializeField] private Button warriorButton;

    int selectedButtonIndex = -1;

    private void Start()
    {
        queenButton.onClick.AddListener(() =>
        {
            queenButton.Select();
            SelectMosquito(0);
        });

        assassinButton.onClick.AddListener(() =>
        {
            assassinButton.Select();
            SelectMosquito(1);
        });

        warriorButton.onClick.AddListener(() =>
        {
            warriorButton.Select();
            SelectMosquito(2);
        });

    }

    private void SelectMosquito(int index)
    {
        if (selectedButtonIndex == index)
        {
            MainMenu.Instance.OnClick_PlayBtn(selectedButtonIndex);
        }

        selectedButtonIndex = index;
    }

}
