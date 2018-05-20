using Models.Towers;

namespace Models
{
    public static class WeaponsLibrary
    {
        public static readonly PistolModel Pistol = new PistolModel();
        public static readonly RifleModel Rifle = new RifleModel();
        public static readonly MachinegunModel Machinegun = new MachinegunModel();
        public static readonly CanonModel Canon = new CanonModel();
        public static readonly RocketModel Rocket = new RocketModel();
    }
}