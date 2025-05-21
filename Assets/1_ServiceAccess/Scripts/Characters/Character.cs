using System;
using UnityEngine;

namespace Excercise1
{
    public class Character : MonoBehaviour, ICharacter
    {
        [SerializeField] protected string id;


        public string Id => id;
        public Transform transform => base.transform;

    }
}