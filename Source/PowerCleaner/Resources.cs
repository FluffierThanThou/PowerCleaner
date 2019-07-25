// Resources.cs
// Copyright Karel Kroeze, -2019

using UnityEngine;
using Verse;

namespace PowerCleaner
{
    [StaticConstructorOnStartup]
    public static class Resources
    {
        public static readonly Texture2D PowerCleanerIcon;
        static Resources()
        {
            PowerCleanerIcon = ContentFinder<Texture2D>.Get( "UI/Icons/PowerCleaner" );
        }
    }
}