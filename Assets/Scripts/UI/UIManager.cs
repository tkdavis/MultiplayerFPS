using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField]
    private View[] views;

    void Awake()
    {
        Instance = this;
    }

    public void Initialize()
    {
        foreach (View view in views) {
            {
                view.Initialize();
            }
        }
    }

    public void Show<T>() where T : View
    {
        foreach (View view in views)
        {
            view.gameObject.SetActive(view is T);
        }
    }
}
