namespace Tanks.Enums
{
    public enum AddableObjects
    {
        MainPlayer,
        StupidEvilPlayer,
        NormalBullet,
        NormalBlock,
        DestroyableBlock,
        Hole,
        NotDestroyingExplosion,
        DestroyingExplosion,
        Mine
    }

    public static class AddableObjectsFunctionality
    {
        public static bool PositionRequired(AddableObjects obj)
            => obj != AddableObjects.MainPlayer && obj != AddableObjects.NormalBullet;
    }
}