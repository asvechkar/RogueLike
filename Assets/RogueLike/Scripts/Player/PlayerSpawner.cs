using UnityEngine;

namespace RogueLike.Scripts.Player
{
    public class PlayerSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject player;
        private void Start()
        {
            player = Instantiate(player, transform.position, Quaternion.identity);
        }
    }
}