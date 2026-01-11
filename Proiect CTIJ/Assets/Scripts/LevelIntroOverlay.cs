using UnityEngine;

public class LevelStartOverlay : MonoBehaviour
{
    [SerializeField] private float displayDuration = 3f;

    private void OnEnable()
    {
        CancelInvoke();
        Invoke(nameof(Hide), displayDuration);
    }

    private void Start()
    {
        CancelInvoke();
        Invoke(nameof(Hide), displayDuration);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
