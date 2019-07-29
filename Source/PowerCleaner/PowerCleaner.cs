// Comp_PowerCleaner.cs
// Copyright Karel Kroeze, -2019

using System.Collections.Generic;
using RimWorld;
using Verse;

namespace PowerCleaner
{
    public class PowerCleaner : Apparel
    {
        public PowerCleaner()
        {
            command = new Command_PowerCleaner( this );
        }

        public Command_PowerCleaner command;

        public override IEnumerable<Gizmo> GetWornGizmos()
        {
            foreach ( var gizmo in base.GetWornGizmos() )
                yield return gizmo;

            if ( !Wearer.Drafted )
            {
                command.disabled = true;
                command.disabledReason = "IsNotDrafted".Translate( Wearer.LabelShort, Wearer );
            }
            else
            {
                command.disabled = false;
            }
            yield return command;
        }
    }
}