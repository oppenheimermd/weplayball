using System;

namespace WePlayBall.Helpers
{
    /// <summary>
    /// System Time of machine instance
    /// </summary>
    public static class SystemTime
    {
        public static readonly Func<DateTime> Now = () => DateTime.UtcNow;
    }
}
