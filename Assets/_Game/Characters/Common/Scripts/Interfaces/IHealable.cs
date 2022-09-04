public interface IHealable
{
    /// <summary>
    /// Returns true - if health was succesfully restored, false - if not (lives are full)
    /// </summary>
    public bool RestoreHealth(int healthAmountToRestore);

}
