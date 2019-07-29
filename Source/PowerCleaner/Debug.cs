// Debug.cs
// Copyright Karel Kroeze, -2019

using System.Diagnostics;

namespace PowerCleaner
{
    public static class Debug
    {
        [Conditional("DEBUG")]
        public static void Log( object msg )
        {
            Verse.Log.Message( $"PowerCleaner :: {msg}" );
        }
    }
}