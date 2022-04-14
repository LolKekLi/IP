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
        _text.text = $"{GetNameByType(type)}";
        
        var buttonImage = button.image;
        (buttonImage.color, _disableColor) = (_disableColor, buttonImage.color);
        

        button.onClick.AddListener(() =>
        {
            (buttonImage.color, _disableColor) = (_disableColor, buttonImage.color);

            onClickAction?.Invoke();
        });
    }

    private string GetNameByType(StateType type)
    {
        switch (type)
        {
            case StateType.Pursue:
                return "Преследовать";
            
            case StateType.Evade:
                return "Избегать";
            
            case StateType.Seek:
                return "Стремиться";
            
            case StateType.Flee:
                return "Бежать";
            
            case StateType.Arrive:
                return "Достигнуть цели";
            
            case StateType.Leave:
                return "Уход от погони";
            
            case StateType.Face:
                return "Смотреть на цель";
            
            case StateType.Wander:
                return "Блуждание вокруг";
            
            default:
                return $"{type} Неизветсная функция";
        }
    }
}