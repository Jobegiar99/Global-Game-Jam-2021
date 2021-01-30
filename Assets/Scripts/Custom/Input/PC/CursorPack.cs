using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.Cursor
{
    [CreateAssetMenu(fileName = "NewCursorPack", menuName = "Game/Cursor")]
    public class CursorPack : ScriptableObject
    {
        public CursorInfo defaultPointer;
        public CursorInfo openHand;
        public CursorInfo closedHand;
        public CursorInfo handPointer;
        public CursorInfo blocked;
    }
}