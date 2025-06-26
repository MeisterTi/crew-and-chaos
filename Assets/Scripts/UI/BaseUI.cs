using UnityEngine;

public class BaseUI : MonoBehaviour
{
    /// <summary>
    /// BaseUI method for showing the UI
    /// </summary>
    protected void Show()
    {
        gameObject.SetActive(true);
    }

    /// <summary>
    /// BaseUI method for hiding the UI
    /// </summary>
    protected void Hide()
    {
        gameObject.SetActive(false);
    }
}
