namespace RogueLike.Scripts.Events.Weapon
{
    public class OnWeaponActivate
    {
        public int ButtonNumber { get; private set; }
        
        public OnWeaponActivate(int buttonNumber)
        {
            ButtonNumber = buttonNumber;
        }
    }
}