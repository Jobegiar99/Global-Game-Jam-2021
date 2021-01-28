using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
    public class BoneTransformHelper : MonoBehaviour
    {
        public HumanBodyBones bone;
        public Animator anim;

        private void Awake()
        {
            transform.SetParent(anim.GetBoneTransform(bone), true);
        }
    }
}