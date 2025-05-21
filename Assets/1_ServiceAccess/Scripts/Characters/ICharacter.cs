using UnityEngine;

namespace Excercise1
{
    public interface ICharacter
    {
        string Id { get; }
        Transform transform { get; }
    }
}