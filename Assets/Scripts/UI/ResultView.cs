using TMPro;
using UnityEngine;

public class ResultView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _bestScore, _currentScore;

    private void Start()
    {
        if (_bestScore != null)
            _bestScore.text = PlayerPrefs.GetInt("BestDistance").ToString() + "m";

        if (_currentScore != null)
            _currentScore.text = ((int)GameManager.Instance.Distance).ToString() + "m";
    }
}