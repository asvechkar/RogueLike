namespace RogueLike.Scripts.Weapon
{
    public struct ProjectileParams
    {
        public float Speed { get; private set; }
        public float Duration { get; private set; }
        public float Damage { get; private set; }
        public int WeaponLevel { get; private set; }

        public ProjectileParams(float speed, float duration, float damage, int level)
        {
            Speed = speed;
            Duration = duration;
            Damage = damage;
            WeaponLevel = level;
        }
    }
}