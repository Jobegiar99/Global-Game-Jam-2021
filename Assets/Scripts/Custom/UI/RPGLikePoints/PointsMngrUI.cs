using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities.Math;

namespace Utilities.UI
{
    public class PointsMngrUI : MonoBehaviour
    {
        public List<PointsUI> points = new List<PointsUI>();
        public Queue<PointsUI> availablePoints = new Queue<PointsUI>();

        public Gote.HUD.PointsUIHelper helper;

        [HideInInspector] public static PointsMngrUI Instance { get { if (!_ins) _ins = FindObjectOfType<PointsMngrUI>(); return _ins; } }
        private static PointsMngrUI _ins = null;

        private void Awake()
        {
            points.ForEach(el => availablePoints.Enqueue(el));
        }

        public void Show(PointsUIInfo info, Vector3 worldPos)
        {
            if (points.Count == 0) return;
            if (availablePoints.Count == 0) return;
            PointsUI p = availablePoints.Dequeue();
            p.Show(info, CustomMath.WolrdToCanvas(worldPos, p.CanvasRT));
            this.ExecuteLater(() => availablePoints.Enqueue(p), p.showTime / info.speed);
        }

        public void Show(PointsUIInfo info, Transform target)
        {
            Show(info, target.position);
        }
    }
}