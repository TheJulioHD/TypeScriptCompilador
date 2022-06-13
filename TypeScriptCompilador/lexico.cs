using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeScriptCompilador
{
    class lexico
    {
        public List<errores> listaErroresLexicio;
        public List<token> listaToken;
        private string code;
        private int line;
        private int state;
        private int column;

        private token NewToken;
        private char characterActuality;
        private string lexicon;
        private int prompter;

        public lexico(string codef)
        {
            code = codef + " ";
            listaToken = new List<token>();// initialize
            listaErroresLexicio = new List<errores>();// initialize
        }

        public List<token> runlexicon() 
        {
            line = 1;
            do
            {
                characterActuality = nextCharacter(prompter);
                if (characterActuality.Equals('\n')) line++;
                lexicon += characterActuality;
                column = returncolumn(characterActuality);
                state = matrizTransicion[state, column];
                if (state < 0 && state >-499)
                {
                    if (lexicon.Length > 1)
                    {
                        lexicon = lexicon.Remove(lexicon.Length - 1);
                        prompter--;
                    }
                    NewToken = new token() { ValorToken = state, LeXICON = lexicon, Line = line };
                    if (state == -1) NewToken.ValorToken = isReservedWord(NewToken.LeXICON);


                    NewToken.TipoTokenS = IsTypeToken(NewToken.ValorToken);
                    listaToken.Add(NewToken);
                    cleanEcl();


                }
                else if (state <=-500)
                {
                    listaErroresLexicio.Add(Errores(state));
                    cleanEcl();
                }
                else if (state == 0)    cleanEcl();

                prompter++;


            } while (prompter < code.ToCharArray().Length);
            return listaToken;
        }
                    
        private void cleanEcl()
        {
            state = 0;
            column = 0;
            lexicon = string.Empty;
        }
        private int[,] matrizTransicion =
        {

               //  0       1        2        3         4        5        6        7        8        9       10       11       12       13       14       15       16       17       18       19       20       21       22       23       24       25       26        27        28         29         30          31
               // dig ||  pal  ||   "   ||   '   ||    +   ||   -   ||   *   ||   /   ||   %   ||   <   ||   >   ||   =   ||   !   ||   &   ||   |   ||   {   ||   }   ||   (   ||   )   ||   [   ||   ]   ||   .   ||   ,   ||   ;   ||   :   ||   ?   ||   _   ||  espa  ||  enter  ||   eof   ||   tab  ||   Desco   ||
        /*  0 */{  2  ,    1   ,    5   ,     6  ,    8    ,    9   ,   10   ,   11   ,   -10  ,   13   ,   12   ,   14   ,   15   ,   17   ,   16   ,  -22   ,  -23   ,  -20   ,  -21   ,  -24   ,  -25   ,  -30   ,  -26   ,  -27   ,  -28   ,  -29   ,    21  ,    0    ,     0    ,     0    ,     0   ,      0   , },
        /*  1 */{  1  ,    1   ,  -500  ,   -500 ,   -1    ,   -1   ,   -1   ,   -1   ,    -1  ,   -1   ,   -1   ,   -1   ,  -500  ,   -1   ,   -1   ,   -1   ,   -1   ,   -1   ,   -1   ,   -1   ,   -1   ,   -1   ,   -1   ,   -1   ,   -1   ,  -500  ,    1   ,   -1    ,    -1    ,    -1    ,    -1   ,     -1   , },
        /*  2 */{  2  ,  -501  ,  -501  ,   -501 ,   -2    ,   -2   ,   -2   ,   -2   ,    -2  ,   -2   ,   -2   ,   -2   ,  -501  ,   -2   ,   -2   ,  -501  ,   -2   ,  -501  ,   -2   ,  -501  ,   -2   ,    3   ,   -2   ,   -2   ,  -501  ,  -501  ,  -501  ,   -2    ,    -2    ,    -2    ,    -2   ,     -2   , },
        /*  3 */{  4  ,  -502  ,  -502  ,   -502 ,  -502   ,  -502  ,  -502  ,  -502  ,   -502 ,  -502  ,  -502  ,  -502  ,  -502  ,  -502  ,  -502  ,  -502  ,  -502  ,  -502  ,  -502  ,  -502  ,  -502  ,  -502  ,  -502  ,  -502  ,  -502  ,  -502  ,  -502  ,   -502  ,   -502   ,   -502   ,   -502  ,    -502  , },
        /*  4 */{  4  ,  -502  ,  -502  ,   -502 ,   -3    ,   -3   ,   -3   ,   -3   ,    -3  ,   -3   ,   -3   ,   -3   ,  -502  ,   -3   ,   -3   ,  -502  ,   -3   ,  -502  ,   -3   ,  -502  ,   -3   ,  -502  ,   -3   ,   -3   ,  -502  ,  -502  ,  -502  ,    -3   ,    -3    ,    -3    ,    -3   ,     -3   , },
        /*  5 */{  5  ,    5   ,   35   ,     5  ,    5    ,    5   ,    5   ,    5   ,     5  ,    5   ,    5   ,    5   ,    5   ,    5   ,    5   ,    5   ,    5   ,    5   ,    5   ,    5   ,    5   ,    5   ,    5   ,    5   ,    5   ,    5   ,    5   ,     5   ,   -503   ,   -503   ,     5   ,    -503  , },
        /*  6 */{  7  ,    7   ,    7   ,   -504 ,    7    ,    7   ,    7   ,    7   ,     7  ,    7   ,    7   ,    7   ,    7   ,    7   ,    7   ,    7   ,    7   ,    7   ,    7   ,    7   ,    7   ,    7   ,    7   ,    7   ,    7   ,    7   ,    7   ,     7   ,   -504   ,   -504   ,   -504  ,    -504  , },
        /*  7 */{-504 ,  -504  ,  -504  ,    36  ,  -504   ,  -504  ,  -504  ,  -504  ,   -504 ,  -504  ,  -504  ,  -504  ,  -504  ,  -504  ,  -504  ,  -504  ,  -504  ,  -504  ,  -504  ,  -504  ,  -504  ,  -504  ,  -504  ,  -504  ,  -504  ,  -504  ,  -504  ,   -504  ,   -504   ,   -504   ,   -504  ,    -504  , },
        /*  8 */{ -6  ,   -6   ,   -6   ,    -6  ,   22    ,   -6   ,   -6   ,   -6   ,    -6  ,   -6   ,   -6   ,   25   ,   -6   ,   -6   ,   -6   ,   -6   ,   -6   ,   -6   ,   -6   ,   -6   ,   -6   ,   -6   ,   -6   ,   -6   ,   -6   ,   -6   ,   -6   ,    -6   ,    -6    ,    -6    ,    -6   ,     -6   , },
        /*  9 */{ -7  ,   -7   ,   -7   ,    -7  ,   -7    ,   24   ,   -7   ,   -7   ,    -7  ,   -7   ,   -7   ,   26   ,   -7   ,   -7   ,   -7   ,   -7   ,   -7   ,   -7   ,   -7   ,   -7   ,   -7   ,   -7   ,   -7   ,   -7   ,   -7   ,   -7   ,   -7   ,    -7   ,    -7    ,    -7    ,    -7   ,     -7   , },
        /* 10 */{ -8  ,   -8   ,   -8   ,    -8  ,   -8    ,   -8   ,   -8   ,   -8   ,    -8  ,   -8   ,   -8   ,   27   ,   -8   ,   -8   ,   -8   ,   -8   ,   -8   ,   -8   ,   -8   ,   -8   ,   -8   ,   -8   ,   -8   ,   -8   ,   -8   ,   -8   ,   -8   ,    -8   ,    -8    ,    -8    ,    -8   ,     -8   , },
        /* 11 */{ -9  ,   -9   ,   -9   ,    -9  ,   -9    ,   -9   ,   20   ,   18   ,    -9  ,   -9   ,   -9   ,   28   ,   -9   ,   -9   ,   -9   ,   -9   ,   -9   ,   -9   ,   -9   ,   -9   ,   -9   ,   -9   ,   -9   ,   -9   ,   -9   ,   -9   ,   -9   ,    -9   ,    -9    ,    -9    ,    -9   ,     -9   , },
        /* 12 */{ -11 ,   -11  ,  -11   ,   -11  ,  -11    ,  -11   ,  -11   ,  -11   ,    -11 ,   -11  ,  -11   ,   30   ,   -11  ,  -11   ,   -11  ,  -11   ,   -11  ,  -11   ,   -11  ,   -11  ,  -11   ,   -11  ,  -11   ,   -11  ,   -11  ,  -11   ,  -11   ,    -11  ,   -11    ,   -11    ,    -11  ,    -11   , },
        /* 13 */{ -12 ,   -12  ,  -12   ,   -12  ,  -12    ,  -12   ,  -12   ,  -12   ,    -12 ,   -12  ,  -12   ,   29   ,   -12  ,  -12   ,   -12  ,  -12   ,   -12  ,  -12   ,   -12  ,   -12  ,  -12   ,   -12  ,  -12   ,   -12  ,   -12  ,  -12   ,  -12   ,    -12  ,   -12    ,   -12    ,    -12  ,    -12   , },
        /* 14 */{-107 ,  -107  , -107   ,  -107  ,  -107   ,  -107  ,  -107  ,  -107  ,   -107 ,  -107  ,  -107  ,   31   ,  -107  ,  -107  ,  -107  ,  -107  ,  -107  ,  -107  ,  -107  ,  -107  ,  -107  ,  -107  ,  -107  ,   -107 ,   -107 ,  -107  ,  -107  ,   -107  ,   -107   ,   -107   ,   -107  ,    -107  , },
        /* 15 */{ -17 ,   17   ,  -17   ,   -17  ,  -17    ,  -17   ,  -17   ,  -17   ,   -17  ,   -17  ,  -17   ,   32   ,   -17  ,  -17   ,   -17  ,  -17   ,   -17  ,  -17   ,  -17   ,   -17  ,  -17   ,   -17  ,  -17   ,   -17  ,   -17  ,  -17   ,  -17   ,    -17  ,   -17    ,   -17    ,   -107  ,    -17   , },
        /* 16 */{-507 ,  -507  , -507   ,  -507  ,  -507   ,  -507  ,  -507  ,  -507  ,  -507  ,  -507  ,  -507  ,  -507  ,  -507  ,  -507  ,   33   ,  -507  ,  -507  ,  -507  ,  -507  ,  -507  ,  -507  ,  -507  ,  -507  ,   -507 ,   -507 ,  -507  ,  -507  ,   -507  ,   -507   ,   -507   ,   -507  ,    -507  , },
        /* 17 */{-508 ,  -508  , -508   ,  -508  ,  -508   ,  -508  ,  -508  ,  -508  ,  -508  ,  -508  ,  -508  ,  -508  ,  -508  ,   34   ,  -508  ,  -508  ,  -508  ,  -508  ,  -508  ,  -508  ,  -508  ,  -508  ,  -508  ,   -508 ,   -508 ,  -508  ,  -508  ,   -508  ,   -508   ,   -508   ,   -509  ,    -509  , },
        /* 18 */{ 18  ,   18   ,   18   ,   18   ,   18    ,   18   ,   18   ,   18   ,   18   ,   18   ,   18   ,   18   ,   18   ,   18   ,   18   ,   18   ,   18   ,   18   ,   18   ,   18   ,   18   ,   18   ,   18   ,    18  ,    18  ,   18   ,   18   ,    18   ,   -108   ,   -108   ,    18   ,    -108  , },
        /* 19 */{ 19  ,   19   ,   19   ,   19   ,   19    ,   19   ,   20   ,  -505  ,   19   ,   19   ,   19   ,   19   ,   19   ,   19   ,   19   ,   19   ,   19   ,   19   ,   19   ,   19   ,   19   ,   19   ,   19   ,    19  ,    19  ,   19   ,   19   ,    19   ,    19    ,    19    ,    19   ,     19   , },
        /* 20 */{ 19  ,   19   ,   19   ,   19   ,   19    ,   19   ,   19   ,   23   ,   19   ,   19   ,   19   ,   19   ,   19   ,   19   ,   19   ,   19   ,   19   ,   19   ,   19   ,   19   ,   19   ,   19   ,   19   ,    19  ,    19  ,   19   ,   19   ,    19   ,    19    ,    19    ,    19   ,     19   , },
        /* 21 */{-506 ,   1    , -506   ,  -506  ,  -506   ,  -506  ,  -506  ,  -506  ,  -506  ,  -506  ,  -506  ,  -506  ,  -506  ,  -506  ,  -506  ,  -506  ,  -506  ,  -506  ,  -506  ,  -506  ,  -506  ,  -506  ,  -506  ,   -506 ,   -506 ,  -506  ,  -506  ,   -506  ,   -506   ,   -506   ,   -506  ,    -506  , },
        /* 22 */{-33  ,  -33   , -33    ,  -33   ,  -33    ,  -33   ,  -33   ,  -33   ,  -33   ,  -33   ,  -33   ,  -33   ,  -33   ,  -33   ,  -33   ,  -33   ,   -33  ,  -33   ,  -33   ,  -33   ,  -33   ,  -33   ,  -33   ,   -33  ,   -33  ,  -33   ,  -33   ,   -33   ,   -33    ,   -33    ,   -33   ,    -33   , },
        /* 23 */{-109 ,  -109  , -109   ,  -109  ,  -109   ,  -109  ,  -109  ,  -109  ,  -109  ,  -109  ,  -109  ,  -109  ,  -109  ,  -109  ,  -109  ,  -109  ,   -109 ,  -109  ,  -109  ,  -109  ,  -109  ,  -109  ,  -109  ,   -109 ,   -109 ,  -109  ,  -109  ,   -109  ,   -109   ,   -109   ,   -109  ,    -109  , },
        /* 24 */{-34  ,  -34   ,  -34   ,  -34   ,  -34    ,  -34   ,  -34   ,  -34   ,  -34   ,  -34   ,  -34   ,  -34   ,  -34   ,  -34   ,  -34   ,  -34   ,   -34  ,  -34   ,  -34   ,  -34   ,  -34   ,  -34   ,  -34   ,   -34  ,   -34  ,  -34   ,  -34   ,   -34   ,   -34    ,   -34    ,   -34   ,    -34   , },
        /* 25 */{-35  ,  -35   ,  -35   ,  -35   ,  -35    ,  -35   ,  -35   ,  -35   ,  -35   ,  -35   ,  -35   ,  -35   ,  -35   ,  -35   ,  -35   ,  -35   ,   -35  ,  -35   ,  -35   ,  -35   ,  -35   ,  -35   ,  -35   ,   -35  ,   -35  ,  -35   ,  -35   ,   -35   ,   -35    ,   -35    ,   -35   ,    -35   , },
        /* 26 */{-36  ,  -36   ,  -36   ,  -36   ,  -36    ,  -36   ,  -36   ,  -36   ,  -36   ,  -36   ,  -36   ,  -36   ,  -36   ,  -36   ,  -36   ,  -36   ,   -36  ,  -36   ,  -36   ,  -36   ,  -36   ,  -36   ,  -36   ,   -36  ,   -36  ,  -36   ,  -36   ,   -36   ,   -36    ,   -36    ,   -36   ,    -36   , },
        /* 27 */{-37  ,  -37   ,  -37   ,  -37   ,  -37    ,  -37   ,  -37   ,  -37   ,  -37   ,  -37   ,  -37   ,  -37   ,  -37   ,  -37   ,  -37   ,  -37   ,   -37  ,  -37   ,  -37   ,  -37   ,  -37   ,  -37   ,  -37   ,   -37  ,   -37  ,  -37   ,  -37   ,   -37   ,   -37    ,   -37    ,   -37   ,    -37   , },
        /* 28 */{-38  ,  -38   ,  -38   ,  -38   ,  -38    ,  -38   ,  -38   ,  -38   ,  -38   ,  -38   ,  -38   ,  -38   ,  -38   ,  -38   ,  -38   ,  -38   ,   -38  ,  -38   ,  -38   ,  -38   ,  -38   ,  -38   ,  -38   ,   -38  ,   -38  ,  -38   ,  -38   ,   -38   ,   -38    ,   -38    ,   -38   ,    -38   , },
        /* 29 */{-13  ,  -13   ,  -13   ,  -13   ,  -13    ,  -13   ,  -13   ,  -13   ,  -13   ,  -13   ,  -13   ,  -13   ,  -13   ,  -13   ,  -13   ,  -13   ,   -13  ,  -13   ,  -13   ,  -13   ,  -13   ,  -13   ,  -13   ,   -13  ,   -13  ,  -13   ,  -13   ,   -13   ,   -13    ,   -13    ,   -13   ,    -13   , },
        /* 30 */{-14  ,  -14   ,  -14   ,  -14   ,  -14    ,  -14   ,  -14   ,  -14   ,  -14   ,  -14   ,  -14   ,  -14   ,  -14   ,  -14   ,  -14   ,  -14   ,   -14  ,  -14   ,  -14   ,  -14   ,  -14   ,  -14   ,  -14   ,   -14  ,   -14  ,  -14   ,  -14   ,   -14   ,   -14    ,   -14    ,   -14   ,    -14   , },
        /* 31 */{-15  ,  -15   ,  -15   ,  -15   ,  -15    ,  -15   ,  -15   ,  -15   ,  -15   ,  -15   ,  -15   ,  -15   ,  -15   ,  -15   ,  -15   ,  -15   ,   -15  ,  -15   ,  -15   ,  -15   ,  -15   ,  -15   ,  -15   ,   -15  ,   -15  ,  -15   ,  -15   ,   -15   ,   -15    ,   -15    ,   -15   ,    -15   , },
        /* 32 */{-16  ,  -16   ,  -16   ,  -16   ,  -16    ,  -16   ,  -16   ,  -16   ,  -16   ,  -16   ,  -16   ,  -16   ,  -16   ,  -16   ,  -16   ,  -16   ,   -16  ,  -16   ,  -16   ,  -16   ,  -16   ,  -16   ,  -16   ,   -16  ,   -16  ,  -16   ,  -16   ,   -16   ,   -16    ,   -16    ,   -16   ,    -16   , },
        /* 33 */{-18  ,  -18   ,  -18   ,  -18   ,  -18    ,  -18   ,  -18   ,  -18   ,  -18   ,  -18   ,  -18   ,  -18   ,  -18   ,  -18   ,  -18   ,  -18   ,   -18  ,  -18   ,  -18   ,  -18   ,  -18   ,  -18   ,  -18   ,   -18  ,   -18  ,  -18   ,  -18   ,   -18   ,   -18    ,   -18    ,   -18   ,    -18   , },
        /* 34 */{-19  ,  -19   ,  -19   ,  -19   ,  -19    ,  -19   ,  -19   ,  -19   ,  -19   ,  -19   ,  -19   ,  -19   ,  -19   ,  -19   ,  -19   ,  -19   ,   -19  ,  -19   ,  -19   ,  -19   ,  -19   ,  -19   ,  -19   ,   -19  ,   -19  ,  -19   ,  -19   ,   -19   ,   -19    ,   -19    ,   -19   ,    -19   , },
        /* 35 */{-4   ,  -4    ,  -4    ,  -4    ,  -4     ,  -4    ,  -4    ,  -4    ,  -4    ,  -4    ,  -4    ,  -4    ,  -4    ,  -4    ,  -4    ,  -4    ,   -4   ,  -4    ,  -4    ,  -4    ,  -4    ,  -4    ,  -4    ,   -4   ,   -4   ,  -4    ,  -4    ,   -4    ,   -4     ,   -4     ,   -4    ,    -4    , },
        /* 36 */{-5   ,  -5    ,  -5    ,  -5    ,  -5     ,  -5    ,  -5    ,  -5    ,  -5    ,  -5    ,  -5    ,  -5    ,  -5    ,  -5    ,  -5    ,  -5    ,   -5   ,  -5    ,  -5    ,  -5    ,  -5    ,  -5    ,  -5    ,   -5   ,   -5   ,  -5    ,  -5    ,   -5    ,   -5     ,   -5     ,   -5    ,    -5    , },
        /* 37 */{-120 ,  -120  , -120   ,  -120  ,  -120   , -120   , -120   , -120   , -120   , -120   , -120   , -120   , -120   , -120   , -120   , -120   ,  -120  , -120   , -120   , -120   , -120   , -120   , -120   ,  -120  ,  -120  , -120   , -120   ,  -120   ,  -120    ,  -120    ,  -120   ,   -120   , },

        ///*  0 */{  2  ,    1   ,    5   ,     6  ,    8    ,    9   ,   10   ,   11   ,   -10  ,   13   ,   12   ,   14   ,   15   ,   17   ,   16   ,  -22   ,  -23   ,  -20   ,  -21   ,  -24   ,  -25   ,  -30   ,  -26   ,  -27   ,  -28   ,  -29   ,    21  ,    0    ,     0    ,     0    ,     0   ,      0   , },

        };

        private int isReservedWord(string lexicon)
        {
            switch (lexicon)
            {
                case "class":
                    return -39;
                case "Extends":
                    return -40;
                case "Interface":
                    return -41;
                case "base":
                    return -42;
                case "public":
                    return -43;
                case "private":
                    return -44;
                case "static":
                    return -45;
                case "protected":
                    return -46;
                case "number":
                    return -47;
                case "int":
                    return -48;
                case "decimal":
                    return -49;
                case "string":
                    return -50;
                case "char":
                    return -51;
                case "any":
                    return -52;
                case "bool":
                    return -53;
                case "boolean":
                    return -54;
                case "var":
                    return -55;
                case "const":
                    return -56;
                case "let":
                    return -57;
                case "type":
                    return -58;
                case "new":
                    return -59;
                case "return":
                    return -60;
                case "super":
                    return -61;
                case "switch":
                    return -62;
                case "this":
                    return -63;
                case "throw":
                    return -64;
                case "try":
                    return -65;
                case "typeof":
                    return -66;
                case "do":
                    return -67;
                case "import":
                    return -68;
                case "void":
                    return -69;
                case "while":
                    return -70;
                case "with":
                    return -71;
                case "yeidl":
                    return -72;
                case "ennum":
                    return -73;
                case "implements":
                    return -74;

                case "interface":
                    return -75;
                case "package":
                    return -76;
                case "break":
                    return -77;
                case "repeat":
                    return -78;

                case "byte":
                    return -79;
                case "final":
                    return -80;

                case "double":
                    return -81;

                case "goto":
                    return -82;
                case "long":
                    return -83;

                case "native":
                    return -84;
                case "short":
                    return -85;

                case "throws":
                    return -86;
                case "synchronized":
                    return -87;
                case "transient":
                    return -88;
                case "volatile":
                    return -89;
                case "float":
                    return -90;
                case "null":
                    return -91;
                case "true":
                    return -92;
                case "false":
                    return -93;
                default:
                    return -1;
            }
        }
        private char nextCharacter(int posicion)
        {
            return Convert.ToChar(code.Substring(posicion, 1));
        }

        private int returncolumn(char character)
        {
              //  0       1        2        3         4        5        6        7        8        9       10       11       12       13       14       15       16       17       18       19       20       21       22       23       24       25       26        27        28         29         30          31
            // dig ||  char  ||   "   ||   '   ||    +   ||   -   ||   *   ||   /   ||   %   ||   <   ||   >   ||   =   ||   !   ||   &   ||   |   ||   {   ||   }   ||   (   ||   )   ||   [   ||   ]   ||   .   ||   ,   ||   ;   ||   :   ||   ?   ||   _   ||  espa  ||  enter  ||   eof   ||   tab  ||   Desco   ||
            if (char.IsDigit(character))
            {
                return 0;
            }
            else if (char.IsLetter(character))
            {
                return 1;
            }
            else if (character.Equals('"'))
            {
                return 2;
            }
            else if (character.Equals('\''))
            {
                return 3;
            }
            else if (character.Equals('+'))
            {
                return 4;
            }
            else if (character.Equals('-'))
            {
                return 5;
            }
            else if (character.Equals('*'))
            {
                return 6;
            }
            else if (character.Equals('/'))
            {
                return 7;
            }
            else if (character.Equals('%'))
            {
                return 8;
            }
            else if (character.Equals('<'))
            {
                return 9;
            }
            else if (character.Equals('>'))
            {
                return 10;
            }
            else if (character.Equals('='))
            {
                return 11;
            }
            else if (character.Equals('!'))
            {
                return 12;
            }
            else if (character.Equals('&'))
            {
                return 13;
            }
            else if (character.Equals('|'))
            {
                return 14;
            }
            else if (character.Equals('{'))
            {
                return 15;
            }
            else if (character.Equals('}'))
            {
                return 16;
            }
            else if (character.Equals('('))
            {
                return 17;
            }
            else if (character.Equals(')'))
            {
                return 18;
            }
            else if (character.Equals('['))
            {
                return 19;
            }
            else if (character.Equals(']'))
            {
                return 20;
            }
            else if (character.Equals('.'))
            {
                return 21;
            }
            else if (character.Equals(','))
            {
                return 22;
            }
            else if (character.Equals(';'))
            {
                return 23;
            }
            else if (character.Equals(':'))
            {
                return 24;
            }
            else if (character.Equals('?'))
            {
                return 25;
            }
            else if (character.Equals('_'))
            {
                return 26;
            }
            else if (character.Equals(' '))
            {
                return 27;
            }
            else if (character.Equals('\n'))
            {
                return 28;
            }
            /*
            else if (character.Equals('EOF'))
            {
                return 29;
            }
            */
            else if (character.Equals('\t'))
            {
                return 30;
            }

            else
            {
                return 31;
            }
        }
        private tipoToken IsTypeToken(int estado)
        {
            switch (estado)
            {
                case -1:
                    return tipoToken.identifier;
                case -2:
                    return tipoToken.IntegerNumber;
                case -3:
                    return tipoToken.Decimalnumber;
                case -4:
                    return tipoToken.String;
                case -5:
                    return tipoToken.Character;
                case -6:
                    return tipoToken.ArithmeticOperator;
                case -7:
                    return tipoToken.ArithmeticOperator;
                case -8:
                    return tipoToken.ArithmeticOperator;
                case -9:
                    return tipoToken.ArithmeticOperator;
                case -10:
                    return tipoToken.ArithmeticOperator;
                case -11:
                    return tipoToken.RelationalOperator;
                case -12:
                    return tipoToken.RelationalOperator;
                case -13:
                    return tipoToken.RelationalOperator;
                case -14:
                    return tipoToken.RelationalOperator;
                case -15:
                    return tipoToken.RelationalOperator;
                case -16:
                    return tipoToken.RelationalOperator;
                case -17:
                    return tipoToken.LogicOperator;
                case -18:
                    return tipoToken.LogicOperator;
                case -19:
                    return tipoToken.LogicOperator;
                case -20:
                    return tipoToken.SimpleSymbol;
                case -21:
                    return tipoToken.SimpleSymbol;
                case -22:
                    return tipoToken.SimpleSymbol;
                case -23:
                    return tipoToken.SimpleSymbol;
                case -24:
                    return tipoToken.SimpleSymbol;
                case -25:
                    return tipoToken.SimpleSymbol;
                case -26:
                    return tipoToken.SimpleSymbol;
                case -27:
                    return tipoToken.SimpleSymbol;
                case -28:
                    return tipoToken.SimpleSymbol;
                case -29:
                    return tipoToken.SimpleSymbol;
                case -30:
                    return tipoToken.SimpleSymbol;
                case -31:
                    return tipoToken.SimpleSymbol;
                case -32:
                    return tipoToken.SimpleSymbol;
                case -33:
                    return tipoToken.SymbolDouble;
                case -34:
                    return tipoToken.SymbolDouble;
                case -35:
                    return tipoToken.Increase;
                case -36:
                    return tipoToken.Increase;
                case -37:
                    return tipoToken.Increase;
                case -38:
                    return tipoToken.Increase;
                case -39:
                    return tipoToken.ReservedWord;
                case -40:
                    return tipoToken.ReservedWord;
                case -41:
                    return tipoToken.ReservedWord;
                case -42:
                    return tipoToken.ReservedWord;
                case -43:
                    return tipoToken.ReservedWord;
                case -44:
                    return tipoToken.ReservedWord;
                case -45:
                    return tipoToken.ReservedWord;
                case -46:
                    return tipoToken.ReservedWord;
                case -47:
                    return tipoToken.ReservedWord;
                case -48:
                    return tipoToken.ReservedWord;
                case -49:
                    return tipoToken.ReservedWord;
                case -50:
                    return tipoToken.ReservedWord;
                case -51:
                    return tipoToken.ReservedWord;
                case -52:
                    return tipoToken.ReservedWord;
                case -53:
                    return tipoToken.ReservedWord;
                case -54:
                    return tipoToken.ReservedWord;
                case -55:
                    return tipoToken.ReservedWord;
                case -56:
                    return tipoToken.ReservedWord;
                case -57:
                    return tipoToken.ReservedWord;
                case -58:
                    return tipoToken.ReservedWord;
                case -59:
                    return tipoToken.ReservedWord;
                case -60:
                    return tipoToken.ReservedWord;
                case -61:
                    return tipoToken.ReservedWord;
                case -62:
                    return tipoToken.ReservedWord;
                case -63:
                    return tipoToken.ReservedWord;
                case -64:
                    return tipoToken.ReservedWord;
                case -65:
                    return tipoToken.ReservedWord;
                case -66:
                    return tipoToken.ReservedWord;
                case -67:
                    return tipoToken.ReservedWord;
                case -68:
                    return tipoToken.ReservedWord;
                case -69:
                    return tipoToken.ReservedWord;
                case -70:
                    return tipoToken.ReservedWord;
                case -71:
                    return tipoToken.ReservedWord;
                case -72:
                    return tipoToken.ReservedWord;
                case -73:
                    return tipoToken.ReservedWord;
                case -74:
                    return tipoToken.ReservedWord;
                case -75:
                    return tipoToken.ReservedWord;
                case -76:
                    return tipoToken.ReservedWord;
                case -77:
                    return tipoToken.ReservedWord;
                case -78:
                    return tipoToken.ReservedWord;
                case -79:
                    return tipoToken.ReservedWord;
                case -80:
                    return tipoToken.ReservedWord;
                case -81:
                    return tipoToken.ReservedWord;
                case -82:
                    return tipoToken.ReservedWord;
                case -83:
                    return tipoToken.ReservedWord;
                case -84:
                    return tipoToken.ReservedWord;
                case -85:
                    return tipoToken.ReservedWord;
                case -86:
                    return tipoToken.ReservedWord;
                case -87:
                    return tipoToken.ReservedWord;
                case -88:
                    return tipoToken.ReservedWord;
                case -89:
                    return tipoToken.ReservedWord;
                case -90:
                    return tipoToken.ReservedWord;
                case -91:
                    return tipoToken.ReservedWord;
                case -92:
                    return tipoToken.ReservedWord;
                case -93:
                    return tipoToken.ReservedWord;
                case -94:
                    return tipoToken.ReservedWord;
                case -95:
                    return tipoToken.ReservedWord;
                case -96:
                    return tipoToken.ReservedWord;
                case -97:
                    return tipoToken.ReservedWord;
                case -98:
                    return tipoToken.ReservedWord;
                case -99:
                    return tipoToken.ReservedWord;
                case -100:
                    return tipoToken.ReservedWord;
                case -101:
                    return tipoToken.ReservedWord;
                case -102:
                    return tipoToken.ReservedWord;
                case -103:
                    return tipoToken.ReservedWord;
                case -104:
                    return tipoToken.ReservedWord;
                case -105:
                    return tipoToken.ReservedWord;
                case -106:
                    return tipoToken.ReservedWord;
                case -107:
                    return tipoToken.Assignment;
                case -108:
                    return tipoToken.Commentary;
                case -109:
                    return tipoToken.CommentMultiline;
                case -110:
                    return tipoToken.ReservedWord;
                case -111:
                    return tipoToken.ReservedWord;
                case -112:
                    return tipoToken.ReservedWord;
                case -113:
                    return tipoToken.ReservedWord;
                case -114:
                    return tipoToken.ReservedWord;
                case -120:
                    return tipoToken.Lambda;
                default:
                    return tipoToken.Astranger;
            }
        }



        private errores Errores(int estado)
        {
            string mensajeError = "";

            switch (estado)
            {
                case 31:
                    mensajeError = "Simobolo Desconocido";
                    break;
                case -500:
                    mensajeError = "Identificador no valido";
                    break;
                case -501:
                    mensajeError = "Número entero no valido";
                    break;
                case -502:
                    mensajeError = "Número decimal no valido";
                    break;
                case -503:
                    mensajeError = "Formato de cadena invalida: Se esperaba una comilla";
                    break;
                case -504:
                    mensajeError = "Se esperaba un carácter";
                    break;
                case -505:
                    mensajeError = "Se esperaba un fin de comentario";
                    break;
                case -506:
                    mensajeError = "Palabra desconocida";
                    break;
                case -507:
                    mensajeError = "Formato de operador invalido, falta una |";
                    break;
                case -508:
                    mensajeError = "Formato de operador invalido, falta una &";
                    break;
                default:
                    mensajeError = "Error Desconocido";
                    break;
            }
            return new errores() { Code = estado, MensajeError = mensajeError, TipoError = tipoError.Lexico, Line = line };
        }


       

       

       
    }
}
