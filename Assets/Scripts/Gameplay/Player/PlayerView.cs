using TMPro;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _distanceText;

    public void UpdateDistanceText(float distance)
    {
        _distanceText.text = ((int)distance).ToString() + "m";
    }
}