using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Empahsis
{
    class WordData
    {
        public readonly string text;
        public readonly Lexeme[] lexemes;
        public readonly int[] syllableEmphasis;
        public readonly int hyphenationCount;
        public readonly string[] hyphenationSegments;

        public WordData(string text, Lexeme[] lexemes, string[] hyphenationSegments)
        {
            this.text = text;
            this.lexemes = lexemes;
            this.hyphenationCount = hyphenationSegments.Length;
            this.hyphenationSegments = hyphenationSegments;
            List<int> ar = new List<int>();
            foreach(Lexeme lexeme in lexemes)
            {
                if(lexeme.emphasis > -1)
                {
                    ar.Add(lexeme.emphasis);
                }
            }
            syllableEmphasis = ar.ToArray();
        }

        public static Dictionary<string, Lexeme> terminals = new Dictionary<string, Lexeme>();
        static WordData()
        {
            terminals["AA"] = new Lexeme(Stress.AA, Sound.AA, Voice.Vowel, -1);
            terminals["AA0"] = new Lexeme(Stress.AA0, Sound.AA, Voice.Vowel, 0);
            terminals["AA1"] = new Lexeme(Stress.AA1, Sound.AA, Voice.Vowel, 1);
            terminals["AA2"] = new Lexeme(Stress.AA2, Sound.AA, Voice.Vowel, 2);
            terminals["AE"] = new Lexeme(Stress.AE, Sound.AE, Voice.Vowel, -1);
            terminals["AE0"] = new Lexeme(Stress.AE0, Sound.AE, Voice.Vowel, 0);
            terminals["AE1"] = new Lexeme(Stress.AE1, Sound.AE, Voice.Vowel, 1);
            terminals["AE2"] = new Lexeme(Stress.AE2, Sound.AE, Voice.Vowel, 2);
            terminals["AH"] = new Lexeme(Stress.AH, Sound.AH, Voice.Vowel, -1);
            terminals["AH0"] = new Lexeme(Stress.AH0, Sound.AH, Voice.Vowel, 0);
            terminals["AH1"] = new Lexeme(Stress.AH1, Sound.AH, Voice.Vowel, 1);
            terminals["AH2"] = new Lexeme(Stress.AH2, Sound.AH, Voice.Vowel, 2);
            terminals["AO"] = new Lexeme(Stress.AO, Sound.AO, Voice.Vowel, -1);
            terminals["AO0"] = new Lexeme(Stress.AO0, Sound.AO, Voice.Vowel, 0);
            terminals["AO1"] = new Lexeme(Stress.AO1, Sound.AO, Voice.Vowel, 1);
            terminals["AO2"] = new Lexeme(Stress.AO2, Sound.AO, Voice.Vowel, 2);
            terminals["AW"] = new Lexeme(Stress.AW, Sound.AW, Voice.Vowel, -1);
            terminals["AW0"] = new Lexeme(Stress.AW0, Sound.AW, Voice.Vowel, 0);
            terminals["AW1"] = new Lexeme(Stress.AW1, Sound.AW, Voice.Vowel, 1);
            terminals["AW2"] = new Lexeme(Stress.AW2, Sound.AW, Voice.Vowel, 2);
            terminals["AY"] = new Lexeme(Stress.AY, Sound.AY, Voice.Vowel, -1);
            terminals["AY0"] = new Lexeme(Stress.AY0, Sound.AY, Voice.Vowel, 0);
            terminals["AY1"] = new Lexeme(Stress.AY1, Sound.AY, Voice.Vowel, 1);
            terminals["AY2"] = new Lexeme(Stress.AY2, Sound.AY, Voice.Vowel, 2);
            terminals["B"] = new Lexeme(Stress.B, Sound.B, Voice.Stop, -1);
            terminals["CH"] = new Lexeme(Stress.CH, Sound.CH, Voice.Affricate, -1);
            terminals["D"] = new Lexeme(Stress.D, Sound.D, Voice.Stop, -1);
            terminals["DH"] = new Lexeme(Stress.DH, Sound.DH, Voice.Fricative, -1);
            terminals["EH"] = new Lexeme(Stress.EH, Sound.EH, Voice.Vowel, -1);
            terminals["EH0"] = new Lexeme(Stress.EH0, Sound.EH, Voice.Vowel, 0);
            terminals["EH1"] = new Lexeme(Stress.EH1, Sound.EH, Voice.Vowel, 1);
            terminals["EH2"] = new Lexeme(Stress.EH2, Sound.EH, Voice.Vowel, 2);
            terminals["ER"] = new Lexeme(Stress.ER, Sound.ER, Voice.Vowel, -1);
            terminals["ER0"] = new Lexeme(Stress.ER0, Sound.ER, Voice.Vowel, 0);
            terminals["ER1"] = new Lexeme(Stress.ER1, Sound.ER, Voice.Vowel, 1);
            terminals["ER2"] = new Lexeme(Stress.ER2, Sound.ER, Voice.Vowel, 2);
            terminals["EY"] = new Lexeme(Stress.EY, Sound.EY, Voice.Vowel, -1);
            terminals["EY0"] = new Lexeme(Stress.EY0, Sound.EY, Voice.Vowel, 0);
            terminals["EY1"] = new Lexeme(Stress.EY1, Sound.EY, Voice.Vowel, 1);
            terminals["EY2"] = new Lexeme(Stress.EY2, Sound.EY, Voice.Vowel, 2);
            terminals["F"] = new Lexeme(Stress.F, Sound.F, Voice.Fricative, -1);
            terminals["G"] = new Lexeme(Stress.G, Sound.G, Voice.Stop, -1);
            terminals["HH"] = new Lexeme(Stress.HH, Sound.HH, Voice.Aspirate, -1);
            terminals["IH"] = new Lexeme(Stress.IH, Sound.IH, Voice.Vowel, -1);
            terminals["IH0"] = new Lexeme(Stress.IH0, Sound.IH, Voice.Vowel, 0);
            terminals["IH1"] = new Lexeme(Stress.IH1, Sound.IH, Voice.Vowel, 1);
            terminals["IH2"] = new Lexeme(Stress.IH2, Sound.IH, Voice.Vowel, 2);
            terminals["IY"] = new Lexeme(Stress.IY, Sound.IY, Voice.Vowel, -1);
            terminals["IY0"] = new Lexeme(Stress.IY0, Sound.IY, Voice.Vowel, 0);
            terminals["IY1"] = new Lexeme(Stress.IY1, Sound.IY, Voice.Vowel, 1);
            terminals["IY2"] = new Lexeme(Stress.IY2, Sound.IY, Voice.Vowel, 2);
            terminals["JH"] = new Lexeme(Stress.JH, Sound.JH, Voice.Affricate, -1);
            terminals["K"] = new Lexeme(Stress.K, Sound.K, Voice.Stop, -1);
            terminals["L"] = new Lexeme(Stress.L, Sound.L, Voice.Liquid, -1);
            terminals["M"] = new Lexeme(Stress.M, Sound.M, Voice.Nasal, -1);
            terminals["N"] = new Lexeme(Stress.N, Sound.N, Voice.Nasal, -1);
            terminals["NG"] = new Lexeme(Stress.NG, Sound.NG, Voice.Nasal, -1);
            terminals["OW"] = new Lexeme(Stress.OW, Sound.OW, Voice.Vowel, -1);
            terminals["OW0"] = new Lexeme(Stress.OW0, Sound.OW, Voice.Vowel, 0);
            terminals["OW1"] = new Lexeme(Stress.OW1, Sound.OW, Voice.Vowel, 1);
            terminals["OW2"] = new Lexeme(Stress.OW2, Sound.OW, Voice.Vowel, 2);
            terminals["OY"] = new Lexeme(Stress.OY, Sound.OY, Voice.Vowel, -1);
            terminals["OY0"] = new Lexeme(Stress.OY0, Sound.OY, Voice.Vowel, 0);
            terminals["OY1"] = new Lexeme(Stress.OY1, Sound.OY, Voice.Vowel, 1);
            terminals["OY2"] = new Lexeme(Stress.OY2, Sound.OY, Voice.Vowel, 2);
            terminals["P"] = new Lexeme(Stress.P, Sound.P, Voice.Stop, -1);
            terminals["R"] = new Lexeme(Stress.R, Sound.R, Voice.Liquid, -1);
            terminals["S"] = new Lexeme(Stress.S, Sound.S, Voice.Fricative, -1);
            terminals["SH"] = new Lexeme(Stress.SH, Sound.SH, Voice.Fricative, -1);
            terminals["T"] = new Lexeme(Stress.T, Sound.T, Voice.Stop, -1);
            terminals["TH"] = new Lexeme(Stress.TH, Sound.TH, Voice.Fricative, -1);
            terminals["UH"] = new Lexeme(Stress.UH, Sound.UH, Voice.Vowel, -1);
            terminals["UH0"] = new Lexeme(Stress.UH0, Sound.UH, Voice.Vowel, 0);
            terminals["UH1"] = new Lexeme(Stress.UH1, Sound.UH, Voice.Vowel, 1);
            terminals["UH2"] = new Lexeme(Stress.UH2, Sound.UH, Voice.Vowel, 2);
            terminals["UW"] = new Lexeme(Stress.UW, Sound.UW, Voice.Vowel, -1);
            terminals["UW0"] = new Lexeme(Stress.UW0, Sound.UW, Voice.Vowel, 0);
            terminals["UW1"] = new Lexeme(Stress.UW1, Sound.UW, Voice.Vowel, 1);
            terminals["UW2"] = new Lexeme(Stress.UW2, Sound.UW, Voice.Vowel, 2);
            terminals["V"] = new Lexeme(Stress.V, Sound.V, Voice.Fricative, -1);
            terminals["W"] = new Lexeme(Stress.W, Sound.W, Voice.Semivowel, -1);
            terminals["Y"] = new Lexeme(Stress.Y, Sound.Y, Voice.Semivowel, -1);
            terminals["Z"] = new Lexeme(Stress.Z, Sound.Z, Voice.Fricative, -1);
            terminals["ZH"] = new Lexeme(Stress.ZH, Sound.ZH, Voice.Fricative, -1);
        }
    }

    struct Lexeme
    {
        public readonly Stress stress;
        public readonly Sound sound;
        public readonly Voice voice;
        public readonly int emphasis;
        public Lexeme(Stress stress, Sound sound, Voice voice, int emphasis)
        {
            this.stress = stress;
            this.sound = sound;
            this.voice = voice;
            this.emphasis = emphasis;
        }
    }

    enum Stress
    {
        AA,
        AA0,
        AA1,
        AA2,
        AE,
        AE0,
        AE1,
        AE2,
        AH,
        AH0,
        AH1,
        AH2,
        AO,
        AO0,
        AO1,
        AO2,
        AW,
        AW0,
        AW1,
        AW2,
        AY,
        AY0,
        AY1,
        AY2,
        B,
        CH,
        D,
        DH,
        EH,
        EH0,
        EH1,
        EH2,
        ER,
        ER0,
        ER1,
        ER2,
        EY,
        EY0,
        EY1,
        EY2,
        F,
        G,
        HH,
        IH,
        IH0,
        IH1,
        IH2,
        IY,
        IY0,
        IY1,
        IY2,
        JH,
        K,
        L,
        M,
        N,
        NG,
        OW,
        OW0,
        OW1,
        OW2,
        OY,
        OY0,
        OY1,
        OY2,
        P,
        R,
        S,
        SH,
        T,
        TH,
        UH,
        UH0,
        UH1,
        UH2,
        UW,
        UW0,
        UW1,
        UW2,
        V,
        W,
        Y,
        Z,
        ZH
    }
    enum Sound
    {
        AA,
        AE,
        AH,
        AO,
        AW,
        AY,
        B,
        CH,
        D,
        DH,
        EH,
        ER,
        EY,
        F,
        G,
        HH,
        IH,
        IY,
        JH,
        K,
        L,
        M,
        N,
        NG,
        OW,
        OY,
        P,
        R,
        S,
        SH,
        T,
        TH,
        UH,
        UW,
        V,
        W,
        Y,
        Z,
        ZH
    }
    enum Voice
    {
        Stop,
        Affricate,
        Fricative,
        Vowel,
        Aspirate,
        Liquid,
        Nasal,
        Semivowel
    }
}
