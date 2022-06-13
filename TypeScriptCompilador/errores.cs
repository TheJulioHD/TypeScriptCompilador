using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeScriptCompilador
{
    public enum tipoError
    {
        Lexico,
        Sintactico,
        Semantico,
        CodigoIntermedio,
        Ejecucion
    }
    class errores
    {
        private int line;
        private int code;
        private tipoError tipoError;
        private string mensajeError;

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

        public int Code
        {
            get
            {
                return code;
            }
            set
            {
                code = value;
            }
        }

        public tipoError TipoError
        {
            get
            {
                return tipoError;
            }
            set
            {
                tipoError = value;
            }
        }

        public string MensajeError
        {
            get
            {
                return mensajeError;
            }
            set
            {
                mensajeError = value;
            }
        }
    }
}
