using Ultima5Redux.References.MapUnits.NonPlayerCharacters;

namespace Ultima5Redux.MapUnits.TurnResults.SpecificTurnResults
{
    public class NpcJoinedParty : TurnResult
    {
        public NpcJoinedParty(NonPlayerCharacterReference npcReference) : base(TurnResultType.NpcJoinedParty) =>
            NpcReference = npcReference;

        public NonPlayerCharacterReference NpcReference { get; }
    }
}