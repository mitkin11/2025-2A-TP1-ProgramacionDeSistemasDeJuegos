using UnityEngine;

namespace Excercise1
{
     [RequireComponent(typeof(Character))]
    public class CharacterRegistrar : MonoBehaviour
    {
        private ICharacter character;
        [SerializeField] private MonoBehaviour registrySource;
        private ICharacterRegistry registry;

        private void Awake()
        {
            character = GetComponent<ICharacter>();

            if (registrySource == null)
            {
                registrySource = FindObjectOfType<CharacterService>();
                if (registrySource == null)
                {
                    Debug.LogError($"CharacterRegistrar on {name}: No CharacterService found in scene.");
                    return;
                }
            }


            registry = registrySource as ICharacterRegistry;

            if (registry == null)
                Debug.LogError("Invalid registry source assigned.");
        }

        private void OnEnable()
        {
            if (!string.IsNullOrEmpty(character.Id))
                registry?.TryAddCharacter(character.Id, character);
        }

        private void OnDisable()
        {
            if (!string.IsNullOrEmpty(character.Id))
                registry?.TryRemoveCharacter(character.Id);
        }
    }
}
