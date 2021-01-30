using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedMeter : MonoBehaviour
{
    public RectTransform meterRT;
    public RectTransform indicatorRT;
    public RectTransform areaRT;

    public float speed = 10f;

    public float TravelDistance => meterRT.sizeDelta.x * 0.5f;
    public float AreaDistance => areaRT.sizeDelta.x * 0.5f;
    private bool isMoving = false;

    public bool IsMoving => isMoving;

    private void Awake()
    {
    }

    private bool IsInValidArea
    {
        get
        {
            return indicatorRT.anchoredPosition.x >= -AreaDistance &&
                indicatorRT.anchoredPosition.x <= AreaDistance;
        }
    }

    private float timer = 0f;

    private void Update()
    {
        timer += Time.deltaTime;
        if (isMoving)
        {
            Vector3 pos = indicatorRT.anchoredPosition;
            float perecentage = Mathf.PingPong(timer, 1f);
            pos.x = -TravelDistance + perecentage * TravelDistance * 2f;
            indicatorRT.anchoredPosition = pos;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (isMoving)
            {
                StopWithAction(() => Debug.Log("Valid"), () => Debug.Log("Invalid"));
            }
            else { Move(); }
        }
    }

    public void Move(float speed = -1)
    {
        if (this.speed != -1) this.speed = speed;
        isMoving = true;
        timer = 0f;
    }

    public void Stop()
    {
        isMoving = false;
    }

    public void StopWithAction(System.Action onValid, System.Action onInvalid)
    {
        if (IsInValidArea) onValid?.Invoke();
        else onInvalid?.Invoke();
        Stop();
    }
}