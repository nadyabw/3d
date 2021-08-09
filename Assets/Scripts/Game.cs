using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private Transform canvasTransform;
    [SerializeField] private PauseView pauseView;

    private bool isPaused;

    private static Game instance;

    public bool IsPaused { get => isPaused;}
    public static Game Instance { get => instance;}

    private void Awake()
    {
        instance = this;
    }

    private void OnEnable()
    {
        AddHandlers();
    }

    private void OnDisable()
    {
        RemoveHandlers();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    private void AddHandlers()
    {
        PauseView.OnClose += HandlePauseViewClosed;
    }

    private void RemoveHandlers()
    {
        PauseView.OnClose -= HandlePauseViewClosed;
    }

    private void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f;
            pauseView.ShowPanel();
        }
        else
        {
            Time.timeScale = 1f;
            pauseView.HidePanel();
        }
    }

    private void HandlePauseViewClosed()
    {
        TogglePause();
    }
}
