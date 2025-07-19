using UnityEngine;

public class PanelStateController : MonoBehaviour
{
    public static void OpenPanel(GameObject _panel) => _panel.SetActive(true);

    public static void ClosePanel(GameObject _panel) => _panel.SetActive(false);
}