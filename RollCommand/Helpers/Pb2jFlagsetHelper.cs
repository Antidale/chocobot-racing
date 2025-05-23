using chocobot_racing.Enums;

namespace chocobot_racing.RollCommand.Helpers
{
    // ReSharper disable once InconsistentNaming
    public static class Pb2jFlagsetHelper
    {
        public static (string flagsetDetails, Pb2jFlagsetChoices flagsetChoice) GetFlagset(Pb2jFlagsetChoices flagsetChoice)
        {
            Pb2jFlagsetChoices[] flagsets = [Pb2jFlagsetChoices.Ladder, Pb2jFlagsetChoices.HopTillYouShop, Pb2jFlagsetChoices.ProBotics];

            flagsets = flagsets.Where(x => x != flagsetChoice).ToArray();

            var selectedFlagset = flagsets[Random.Shared.Next(flagsets.Length)];

            var flagsetDetails = selectedFlagset switch
            {
                Pb2jFlagsetChoices.Ladder =>
                    "O1:quest_forge/2:quest_tradepink/random:1,quest,char/req:all/win:crystal Kmain/summon/moon Pkey Cstandard/nofree/j:abilities Twildish Sstandard Bstandard/alt:gauntlet Etoggle Glife/sylph/backrow -kit:basic -kit2:trap -spoon -smith:super -pushbtojump",

                Pb2jFlagsetChoices.HopTillYouShop =>
                    "O1:quest_murasamealtar/2:quest_crystalaltar/3:quest_whitealtar/4:quest_ribbonaltar/5:quest_masamunealtar/req:4/win:game Kmain/moon/unsafe Pkey Cstandard/nofree/distinct:5/j:abilities/nekkie Twildish/money Swild/no:j Bstandard/alt:gauntlet/whichburn Etoggle/noexp/no:jdrops Gmp/warp/life/sylph/backrow -kit:better -kit2:money -kit3:money -spoon -vanilla:traps -pushbtojump",

                Pb2jFlagsetChoices.ProBotics =>
                    "O1:quest_forge/2:quest_tradepink/3:quest_unlocksewer/random:2,tough_quest/req:4/win:crystal Kmain/summon/moon/unsafe Pkey Cstandard/nofree/distinct:7/no:tellah,fusoya/restrict:rydia,edward,yang,palom,porom/j:abilities/nekkie/nodupes/bye/hero Tpro Sstandard/no:j Bstandard/unsafe/alt:gauntlet/whyburn Etoggle/no:jdrops Glife/backrow -kit:freedom -kit2:notdeme -kit3:cid -noadamants -nocursed -spoon -pushbtojump",

                _ => ""
            };

            return (flagsetDetails, selectedFlagset);
        }
    }
}
