using UnityEngine;

namespace RogueLike.Scripts.GameCore
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private GameObject player;
        private void Start()
        {
            player = Instantiate(player, transform.position, Quaternion.identity);
        }
    }
}