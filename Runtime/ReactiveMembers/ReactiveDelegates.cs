namespace UnityMVVM
{
    public delegate bool NonGenericPredicate();

    /// <summary>
    /// Delegate type for raise on value changed event when property was changed by model or business logic.
    /// </summary>
    public delegate void ReactiveMemberChangedHandler<T>(T value);
}
