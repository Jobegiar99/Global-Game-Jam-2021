using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;

namespace Utilities
{
    public static class Helper
    {
        public static void Destroy(GameObject g)
        {
            if (Application.isPlaying)
            {
                Object.Destroy(g);
            }
            else
            {
                Object.DestroyImmediate(g);
            }
        }

        public static void DestroyAllChildren(Transform transform)
        {
            if (Application.isPlaying)
            {
                foreach (Transform child in transform)
                {
                    if (child != transform)
                        Object.Destroy(child.gameObject);
                }
            }
            else
            {
                var tempList = transform.Cast<Transform>().ToList();
                foreach (var child in tempList)
                {
                    if (child != transform)
                        Object.DestroyImmediate(child.gameObject);
                }
            }
        }

        public static int FullCircular(int currentId, int length)
        {
            if (length == 0) return -1;
            return (currentId + length) % length;
        }

        public static float FullCircular(float currentId, float length)
        {
            if (length == 0) return -1f;
            return (currentId + length) % length;
        }

        public static int GetRandomNoReapiting(ref int old, int min, int max)
        {
            if ((max - min) <= 1)
                return Mathf.Abs((max - min));

            int current = old;
            while (current == old)
            {
                current = Random.Range(min, max);
            }
            old = current;
            return old;
        }

        public static float GetRandomNoReapiting(ref float old, float min, float max)
        {
            if ((max - min) <= 1)
                return Mathf.Abs((max - min));

            float current = old;
            while (current == old)
            {
                current = Random.Range(min, max);
            }
            old = current;

            return 0f;
        }

        public static GameObject CreateChild(Transform parent, string name = "_child", GameObject prefab = null)
        {
            GameObject child;
            if (prefab == null)
                child = new GameObject(name);
            else
                child = Object.Instantiate(prefab, parent);

            child.transform.SetParent(parent);
            child.transform.localScale = Vector3.one;
            child.transform.localPosition = Vector3.zero;
            child.transform.localRotation = Quaternion.identity;
            child.name = name;
            return child;
        }

        public static T GetAddComponent<T>(GameObject gameObject) where T : Component
        {
            if (gameObject.GetComponent<T>() == null)
                gameObject.AddComponent<T>();
            return gameObject.GetComponent<T>();
        }

        public static Vector3[] GetVerticesFromCollider2D(BoxCollider2D col, bool useHeight = false, float height = -1)
        {
            var vertices = new Vector3[4];
            var thisMatrix = col.transform.localToWorldMatrix;
            var storedRotation = col.transform.rotation;
            Vector3 storedPosition = col.transform.position;
            var worldPos = col.transform.position * -1;
            col.transform.rotation = Quaternion.identity;

            var extents = col.bounds.extents;
            var center = col.bounds.center;
            var scale = col.transform.lossyScale;
            vertices[0] = thisMatrix.MultiplyPoint3x4(extents + center) + worldPos;
            vertices[1] = thisMatrix.MultiplyPoint3x4(new Vector3(-extents.x, extents.y, extents.z) + center) + worldPos;
            vertices[2] = thisMatrix.MultiplyPoint3x4(new Vector3(-extents.x, -extents.y, extents.z) + center) + worldPos;
            vertices[3] = thisMatrix.MultiplyPoint3x4(new Vector3(extents.x, -extents.y, extents.z) + center) + worldPos;
            col.transform.rotation = storedRotation;
            return vertices;
        }

        public static bool LayerIsInMask(int layer, LayerMask mask)
        {
            return mask == (mask | (1 << layer));
        }

        public static void SetLayerRecursively(this GameObject go, int layerNumber)
        {
            foreach (Transform trans in go.GetComponentsInChildren<Transform>(true))
            {
                trans.gameObject.layer = layerNumber;
            }
        }
    }
}