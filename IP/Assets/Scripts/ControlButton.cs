using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ControlButton : MonoBehaviour
{
    [SerializeField]
    private Color _disableColor = default;

    [SerializeField]
    private TextMeshProUGUI _text = null;

    public void Prepare(StateType type, Action onClickAction)
    {
        var button = GetComponent<Button>();
        _text.text = $"{type}";
        
        var buttonImage = button.image;
        (buttonImage.color, _disableColor) = (_disableColor, buttonImage.color);
        

        button.onClick.AddListener(() =>
        {
            (buttonImage.color, _disableColor) = (_disableColor, buttonImage.color);

            onClickAction?.Invoke();
        });
    }
}