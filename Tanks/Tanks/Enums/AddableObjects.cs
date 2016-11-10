namespace Tanks.Enums
{
    public enum AddableObjects
    {
        MainPlayer,
        EvilPlayer,
        NormalBullet,
        NormalBlock,
        DestroyableBlock,
        Hole,
        NotDestroyingExplosion,
        DestroyingExplosion,
        Mine,
        Nothing
    }

    public static class AddableObjectsFunctionality
    {
        public static bool PositionRequired(AddableObjects obj)
            => obj != AddableObjects.MainPlayer && obj != AddableObjects.NormalBullet;
    }
}