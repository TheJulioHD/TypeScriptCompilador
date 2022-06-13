using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeScriptCompilador
{
    public enum tipoToken
    {
        identifier,
        ArithmeticOperator,
        RelationalOperator,
        LogicOperator,
        SimpleSymbol,
        AssignmentOperator,
        ReservedWord,
        SymbolDouble,
        IntegerNumber,
        Decimalnumber,
        String,
        Character,
        Increase,
        Assignment,
        Commentary,
        CommentMultiline,
        Astranger,
        Lambda
    }
    public class token
    {
       

        private tipoToken TipoToken;
        private int valorToken;
        private string leXICON;
        private int line;

        public int ValorToken
        {
            get
            {
                return valorToken;
            }
            set
            {
                valorToken = value;
            }
        }

        public string LeXICON
        {
            get
            {
                return leXICON;
            }
            set
            {
                leXICON = value;
            }
        }

        public int Line
        {
            get
            {
                return line;
            }
            set
            {
                line = value;
            }
        }

        public tipoToken TipoTokenS
        {
            get
            {
                return TipoToken;
            }
            set
            {
                TipoToken = value;
            }
        }


    }
}
