using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private Camera m_camera;
    [SerializeField] private LayerMask m_layer;
    [SerializeField] private PlayerController m_playerController;
    [SerializeField] private HUDManager m_hud;

    public PlayerController PlayerController => m_playerController;

    private void Reset()
    {
        m_camera = Camera.main;
    }

    private void Awake()
    {
        if (!m_camera) m_camera = Camera.main;
    }

    private void Update()
    {
        if (m_hud.HasOpenMenus) return;

        Hover();
        if (Input.GetMouseButtonDown(0)) Click();
    }

    private EnvironmentObject currentEnvironment;

    public void Hover()
    {
        bool unhover = true;
        Ray ray = m_camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hit, 200f, m_layer.value))
        {
            var environment = hit.collider.GetComponent<EnvironmentObject>();
            if (environment)
            {
                if (environment != currentEnvironment)
                {
                    if (currentEnvironment) currentEnvironment.Hover(false);
                    environment.Hover(true);
                    currentEnvironment = environment;
                }
                unhover = false;
            }
        }

        if (unhover && currentEnvironment) { currentEnvironment.Hover(false); currentEnvironment = null; }
    }

    public void Click()
    {
        Ray ray = m_camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hit, 200f, m_layer.value))
        {
            var environment = hit.collider.GetComponent<EnvironmentObject>();
            if (environment)
            {
                var data = environment.OnClick(hit, PlayerController);
                if (currentEnvironment) currentEnvironment.Hover(false);
                currentEnvironment = null;
            }
        }
    }
}