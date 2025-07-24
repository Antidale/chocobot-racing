
using chocobot_racing.RollCommand.Enums;

namespace chocobot_racing.RollCommand.Helpers;

public static class PresetHelper
{
    public static (FeHostedApi Api, string Flagset) GetPresetDetails(FePresetChoices choice)
    {
        return choice switch
        {
            FePresetChoices.AdamantCupGroup => (FeHostedApi.Main, "Orandom:7,tough_quest/req:7/win:crystal Kmain/summon/moon Pkey Cstandard/nofree/restrict:cecil,fusoya/j:abilities/nekkie/party:4 Twildish/maxtier:7 Sstandard Bstandard/alt:gauntlet Etoggle Glife/sylph/backrow -kit:better -spoon"),

            FePresetChoices.AdamantCupBracket => (FeHostedApi.Main, "Orandom:7,tough_quest/req:7/win:crystal Kmain/summon/moon Pkey Crelaxed/nofree/no:cecil,fusoya/j:abilities/nekkie/party:4 Tpro/maxtier:6 Sstandard/sell:quarter Bstandard/alt:gauntlet Etoggle Glife/sylph -kit:basic -spoon -smith:alt"),

            FePresetChoices.SupermarketSweep => (FeHostedApi.Main, "Orandom:4,tough_quest,boss/req:3/win:crystal Kmain Pshop Crelaxed/j:abilities Twild Swild/free/no:apples Bstandard/whyburn Etoggle Glife/sylph -spoon"),

            FePresetChoices.OmnidexterousMemersGuild => (FeHostedApi.Main, "O1:boss_golbez/2:boss_fabulgauntlet/random:6,boss/req:7/win:crystal Kmain/summon/moon/force:hook Pkey Crelaxed/noearned/distinct:7/start:any/no:fusoya/j:abilities/nekkie/nodupes Twildish Sstandard Bstandard/alt:gauntlet Etoggle Gwarp/life/sylph/backrow -kit:basic -kit2:freedom -noadamants -spoon -smith:super"),

            FePresetChoices.ZZ4 => (FeHostedApi.Main, "O1:boss_golbez/2:boss_fabulgauntlet/random:6,boss/req:7/win:crystal Kmain/summon/moon/force:hook Pkey Crelaxed/noearned/distinct:7/start:any/no:fusoya/j:abilities/nekkie/nodupes Twildish Sstandard Bstandard/alt:gauntlet Etoggle Gwarp/life/sylph/backrow -kit:basic -kit2:freedom -noadamants -spoon -smith:super"),

            FePresetChoices.ZZ5BlueMoon => (FeHostedApi.Main, "O1:boss_golbez/2:boss_fabulgauntlet/random:6,boss/req:7/win:crystal Kmain/summon/moon/force:hook Pkey Crelaxed/noearned/distinct:7/start:any/no:fusoya/j:abilities/nekkie/nodupes Twildish Sstandard Bstandard/alt:gauntlet Etoggle Gwarp/life/sylph/backrow -kit:basic -kit2:freedom -noadamants -spoon -smith:super"),

            FePresetChoices.ZZ6 => (FeHostedApi.Main, "O1:quest_magnes/2:quest_forge/3:boss_fabulgauntlet/4:boss_golbez/random:3,tough_quest/req:6/win:crystal Kmain/summon/moon/nofree Pkey Cstandard/nofree/restrict:cecil,fusoya/j:abilities/nekkie/hero Twildish/maxtier:5 Sstandard Bstandard/alt:gauntlet/whichburn Etoggle/cantrun Gwarp/life/sylph/backrow -kit:better -kit2:dwarf -spoon"),

            FePresetChoices.EELMoonveilMixer => (FeHostedApi.Main, "O1:quest_magnes/2:quest_forge/3:boss_fabulgauntlet/4:boss_golbez/random:3,tough_quest/req:6/win:crystal Kmain/summon/moon/nofree Pkey Cstandard/nofree/restrict:cecil,fusoya/j:abilities/nekkie/hero Twildish/maxtier:5 Sstandard Bstandard/alt:gauntlet/whichburn Etoggle/cantrun Gwarp/life/sylph/backrow -kit:better -kit2:dwarf -spoon"),

            FePresetChoices.EELPotionParty => (FeHostedApi.Main, "O1:quest_monsterqueen/2:quest_monsterking/3:quest_cavebahamut/4:quest_masamunealtar/5:quest_forge/random:3,tough_quest/req:6/win:game Kmain/miab/nofree/force:magma Pnone Cstandard/nofree/distinct:7/start:not_tellah,not_fusoya/no:fusoya/restrict:cecil/j:abilities/nekkie/nodupes/hero Twildish/money Swild/no:j Bstandard/nofree/alt:gauntlet/whichburn Etoggle/noexp Gwarp/life/sylph/backrow -kit:freedom -kit2:miab -kit3:basic -noadamants -nocursed -spoon"),

            FePresetChoices.ProBotics => (FeHostedApi.Main, "O1:quest_forge/2:quest_tradepink/3:quest_unlocksewer/random:2,tough_quest/req:4/win:crystal Kmain/summon/moon/unsafe Pkey Cstandard/nofree/distinct:7/no:tellah,fusoya/restrict:rydia,edward,yang,palom,porom/j:abilities/nekkie/nodupes/bye/hero Tpro Sstandard/no:j Bstandard/unsafe/alt:gauntlet/whyburn Etoggle/no:jdrops Glife/backrow -kit:freedom -kit2:notdeme -kit3:cid -noadamants -nocursed -spoon -pushbtojump"),

            FePresetChoices.FuWario => (FeHostedApi.Main, "O1:quest_forge/2:quest_tradepink/3:quest_pass/win:crystal Kmain/miab/nofree Pkey Crelaxed/nofree/start:fusoya/j:abilities Twildish Sstandard/no:j Bstandard/nofree/alt:gauntlet Etoggle/noexp Gwarp/life/sylph/backrow -kit:freedom -vanilla:fusoya"),

            FePresetChoices.LadderPB2J => (FeHostedApi.Main, "O1:quest_forge/2:quest_tradepink/random:1,quest,char/req:all/win:crystal Kmain/summon/moon Pkey Cstandard/nofree/j:abilities Twildish Sstandard Bstandard/alt:gauntlet Etoggle Glife/sylph/backrow -kit:basic -kit2:miab -spoon -smith:super -pushbtojump"),

            FePresetChoices.D2T => (FeHostedApi.Galeswift, "Omode:ki11/req:all/win:crystal Kmain/summon/moon/miab:above,below Pshop Cstandard/nofree/start:not_tellah/thrift:4/j:abilities/hero Tstandard Swildish/no:sirens Bstandard/alt:gauntlet/whichburn/whichbez Etoggle Glife/sylph/backrow -kit:basic -kit2:freedom -kit3:exit -noadamants -spoon -exp:nokeybonus,kicheckbonus5,maxlevelbonus -doorsrando:all"),

            FePresetChoices.ACE => (FeHostedApi.Galeswift, "Omode:ki12/random:2,quest/random2:1,tough_quest/req:all/win:crystal Kmain/summon/moon/nofree:dwarf/unweighted Pkey Cstandard/nofree/restrict:cecil,fusoya/j:abilities/paladin/nekkie/party:4/treasure:free,unsafe Twildish Sprice:200/pricey:items/standard Bstandard/alt:gauntlet/whichbez Etoggle Glife/sylph/backrow -kit:better -smith:alt -fusoya:sequential_r -exp:objectivebonus25 -tweak:edwardheal"),

            FePresetChoices.FBF => (FeHostedApi.Galeswift, "O1:quest_forge/2:quest_cavebahamut/3:quest_monsterking/4:quest_monsterqueen/5:quest_masamunealtar/random:3,tough_quest/req:6/win:game Kmain/miab:above/pink/nofree:package/force:magma Pnone Cstandard/nofree/distinct:7/start:not_tellah,not_fusoya/no:fusoya/restrict:cecil/j:abilities/nekkie/nodupes/hero Twildish/mintier:2/money Swild/always:damage_items/no:j Bstandard/nofree/unsafe/alt:gauntlet/whichburn/whichbez/woahdin Etoggle/noexp/nogp Gwarp/life/sylph/backrow -kit:dwarf -kit2:miab -kit3:basic -noadamants -nocursed -spoon -exp:kicheckbonus2 -tweak:edwardheal"),

            FePresetChoices.ZZA => (FeHostedApi.Galeswift, "O1:quest_forge/req:all/win:crystal Kmain/moon/pink/nofree/unweighted/start:spoon Pkey Crelaxed/nofree/distinct:8/thrift:3/j:abilities/nodupes/hero Tsemipro/playable/junk Swildish/no:vampires,damage_items Bstandard/restrict:giant,package/whichburn/whichbez Etoggle Glife/sylph/backrow/64 -kit:freedom -noadamants -fusoya:maybe -exp:maxlevelbonus -tweak:edwardheal"),

            FePresetChoices.KcharIntro => (FeHostedApi.Alpha, "OA1:collect_ki10/2:quest_forge/do_all:crystal Kmain/miab:above/char Pkey Cstandard/nofree/start:edge/j:abilities Twildish Sstandard Bstandard/alt:gauntlet/whyburn Etoggle Glife/sylph/backrow Qmsgspeedfix -kit:better -spoon -smith:super"),

            FePresetChoices.PlinkPonyClub => (FeHostedApi.Alpha, "OArandom:4,tough_quest/do_2:spoon/do_4:superweapon OBrandom:4,char/do_2:ribbon/do_4:adamantarmor OC1:collect_boss13/random:4,boss/do_3:siren/do_5:cursedring ODgroup_a:all/group_b:all/group_c:all/do_2:crystal Kmain/summon/moon/miab:lst/char/nofree Pkey Cstandard/nofree/nogiant/distinct:8/start:any/j:abilities/nekkie/nodupes/hero Tpro/playable Sstandard/playable/no:sirens Bchaos/alt:gauntlet/chaosburn Etoggle Xobjbonus:5 Gwarp/life/sylph/backrow Qfastrom/msgspeedfix -kit:better -noadamants -nocursed -smith:alt,playable"),

            FePresetChoices.AngryBird => (FeHostedApi.Alpha, "OA1:boss_ogopogo/2:boss_paledim/3:boss_wyvern/4:boss_plague/5:boss_dlunar/do_2:siren/do_4:cursedring OB1:collect_boss5/2:collect_boss15/3:collect_boss23/group_a:all/do_1:moonveil/do_2:adamantarmor/do_3:crystal/do_4:game Kmain/summon/moon Pkey Cstandard/nofree/maybe/j:abilities/nodupes/hero Tstandard/playable/miabs:wildish Sstandard/playable/no:sirens Bchaos/alt:gauntlet/chaosburn Etoggle Xobjbonus:16 Gwarp/life/sylph/backrow Qfastrom/msgspeedfix -kit:better -noadamants -nocursed"),

            FePresetChoices.HeroHold => (FeHostedApi.Alpha, "OA1:collect_ki8/do_all:cecil OB1:collect_ki9/do_all:crystalsword OC1:collect_ki10/do_all:crystal Kmain/summon/moon/miab:above/char/forge/nofree Pkey Cstandard/no:cecil,fusoya/j:abilities/nekkie/party:4/permajoin/wishes Tstandard Sstandard Bchaos/alt:gauntlet/chaosburn Etoggle Xobjbonus:10 Gwarp/life/backrow Qfastrom/msgspeedfix -kit:freedom -kit2:miab -noadamants -nocursed -spoon -vanilla:agility"),

            FePresetChoices.DarkMatter5 => (FeHostedApi.Alpha, "Omode_dkmatter:quests/win:crystal OA1:collect_dkmatter20/do_all:superweapon Kmain/summon/miab:above/char/nofree Pkey Cstandard/nofree/distinct:8/j:abilities/nodupes/hero Tpro/playable Sstandard Bchaos/alt:gauntlet/chaosburn Etoggle Xnokeybonus/objbonus:10 Gwarp/life/sylph/backrow Qfastrom/msgspeedfix -kit:better -kit2:freedom -noadamants -spoon -smith:alt,playable"),

            FePresetChoices.SpoonyNinja => (FeHostedApi.Alpha, "OA1:quest_monsterqueen/2:quest_monsterking/3:quest_murasamealtar/4:quest_crystalaltar/5:quest_whitealtar/6:quest_ribbonaltar/7:quest_masamunealtar/8:quest_baronbasement/random:3,quest/do_1:spoon/do_2:spoon/do_3:spoon/do_4:spoon/do_5:spoon/do_6:spoon/do_7:spoon/do_8:spoon/do_9:spoon/do_10:spoon/do_all:spoon OB1:quest_forge/2:quest_tradepan/3:quest_sealedcave/4:quest_tradepink/do_1:spoon/do_2:spoon/do_3:crystal Kmain/summon/moon Pkey Crelaxed/nofree/distinct:7/start:edge/j:abilities/nekkie Tpro/playable/sparse:70/miabs:standard Sstandard Bchaos/chaosburn Etoggle Xnoboost/nokeybonus/objbonus:10 Glife/sylph/backrow Qfastrom/msgspeedfix -kit:better -kit2:meme -kit3:money -noadamants -smith:alt"),

            FePresetChoices.Sumomo => (FeHostedApi.Main, "O1:quest_traderat/2:quest_falcon/3:quest_ribbonaltar/random:3,tough_quest,boss/req:5/win:crystal Kmain/summon/moon/nofree Pkey Crelaxed/nofree/distinct:7/start:not_cecil,not_tellah,not_fusoya/j:abilities/nekkie/nodupes/bye/permajoin/hero Twildish/mintier:3 Sstandard Bstandard/alt:gauntlet/whichburn Etoggle Gwarp/life/sylph/backrow -kit:freedom -spoon"),

            _ => (FeHostedApi.Main, "")
        };
    }
}
