using UnityEngine;

namespace Excercise1
{

    public interface ICharacterRegistry
    {
        public bool TryAddCharacter(string id, ICharacter character);
    
        bool TryRemoveCharacter(string id);
    }

}
