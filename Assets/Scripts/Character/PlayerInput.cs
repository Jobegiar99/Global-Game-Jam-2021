using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private Camera m_camera;
    [SerializeField] private LayerMask m_layer;
    [SerializeField] private PlayerController m_playerController;

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
        if (Input.GetMouseButtonDown(0)) Click();
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
            }
        }
    }
}