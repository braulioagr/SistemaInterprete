using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Antlr4.Runtime.Misc;

namespace Practica6
{
    internal class MyErrorListener : BaseErrorListener
    {
        int id;
        private bool existeError;
        public MyErrorListener(int id)
        {
            this.id = id;
            this.existeError = false;
        }

        public override void SyntaxError([NotNull] IRecognizer recognizer, [Nullable] IToken offendingSymbol, int line, int charPositionInLine, [NotNull] string msg, [Nullable] RecognitionException e)
        {
            existeError = true;
            base.SyntaxError(recognizer, offendingSymbol, line, charPositionInLine, msg, e);
            //INTERMEDIO
        }
        public bool ExisteError
        {
            get { return this.existeError; }
        }
    }
}