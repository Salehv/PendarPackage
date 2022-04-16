using UnityEngine;

public class PPanel : MonoBehaviour
{
    public string panelName;

    public void SetVisibility(bool show)
    {
        gameObject.SetActive(show);
    }
}
