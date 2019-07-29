using UnityEngine;
using Verse;

namespace PowerCleaner
{
    public class MapComponent_PowerCleaner : MapComponent
    {

        public MapComponent_PowerCleaner( Map map ) : base( map )
        {
        }

        public PowerCleaner ActivePowerCleaner { get; set; }

        public bool ValidPowerCleaner =>
            ActivePowerCleaner?.Wearer != null                    &&
            Find.Selector.IsSelected( ActivePowerCleaner.Wearer ) &&
            ActivePowerCleaner.Wearer.Drafted;

        public override void MapComponentOnGUI()
        {
            if ( ValidPowerCleaner )
            {
                GenUI.DrawMouseAttachment( Resources.SprayIcon );

                // handle button down state
                if ( ( Event.current.type == EventType.MouseDown || Event.current.type == EventType.MouseDrag ) &&
                     Event.current.button == 0 || Input.GetMouseButton( 0 ) )
                {
                    if ( Find.TickManager.CurTimeSpeed                                  > TimeSpeed.Paused &&
                        ( Find.TickManager.TicksGame + GetHashCode() ) % Water._ticksPerSpawn == 0 )
                    {

                        var water = new Water( ActivePowerCleaner.Wearer.DrawPos, UI.MouseMapPosition() );
                        water.def = ThingDefOf.PowerCleaner_Water;
                        water.PostMake();
                        GenSpawn.Spawn( water, water.Position, map );
                    }
                    if ( Event.current.type != EventType.Repaint && Event.current.type != EventType.Layout )
                        Event.current.Use();
                }

                if ( Event.current.type == EventType.MouseDown && Event.current.button == 1 )
                {
                    ActivePowerCleaner = null;
                    Event.current.Use();
                }
            }
            else
                ActivePowerCleaner = null;
        }

        public override void MapComponentUpdate()
        {

        }
    }
}