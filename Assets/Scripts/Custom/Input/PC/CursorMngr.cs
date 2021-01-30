using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Utilities.Cursor
{
    public class CursorMngr : MonoBehaviour
    {
        public CursorPack cursorPack;
        public CursorInfo Cursor { get; private set; }
        public CursorMode Mode { get; private set; }
        public CursorStatus Status { get; private set; }
        public static CursorMngr Instance;

        public void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
                return;
            }
        }

        private void Start()
        {
            SetCursor(CursorStatus.Default);
            Cursor = cursorPack.defaultPointer;
        }

        public void SetCursor(CursorStatus status)
        {
            Cursor = GetCursor(status);
            Status = status;
            UnityEngine.Cursor.SetCursor(Cursor.texture, Cursor.hotSpot, CursorMode.Auto);
        }

        public CursorInfo GetCursor(CursorStatus status)
        {
            switch (status)
            {
                case CursorStatus.Default:
                    return cursorPack.defaultPointer;

                case CursorStatus.OpenHand:
                    return cursorPack.openHand;

                case CursorStatus.ClosedHand:
                    return cursorPack.closedHand;

                case CursorStatus.HandPointer:
                    return cursorPack.handPointer;

                case CursorStatus.Blocked:
                    return cursorPack.blocked;

                default:
                    return cursorPack.defaultPointer;
            }
        }
    }

    public enum CursorStatus { Default, OpenHand, ClosedHand, HandPointer, Blocked }

    [System.Serializable]
    public class CursorInfo
    {
        public Texture2D texture;
        public Vector2 hotSpot = Vector2.zero;
    }
}