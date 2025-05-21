using System;
using UnityEngine;

namespace Excercise1
{
    public class Enemy : Character
    {
        [SerializeField] private float speed = 5;
        [SerializeField] private string playerId = "Player";
        private ICharacter _player;
        private string _logTag;

        private void Reset()
            => id = nameof(Enemy);

        private void Awake()
            => _logTag = $"{name}({nameof(Enemy).Colored("#555555")}):";

        private void Start()
        {
            var registry = FindObjectOfType<CharacterService>() as ICharacterRegistry;
             _player = (registry as CharacterService)?.GetById(playerId);

            if (_player == null)
                Debug.LogError($"{_logTag} Player not found!");
        }


        private void Update()
        {
            if (_player == null)
                return;
            var direction = _player.transform.position - transform.position;
            transform.position += direction.normalized * (speed * Time.deltaTime);
        }
    }
}