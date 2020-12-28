using System;
using System.Collections.Generic;
using System.Text;

namespace ProjektBiblioteka
{
    public class OwnException : Exception
    {
        public OwnException() { }

        public OwnException(string message) : base(message)
        { }

        public OwnException(string message, Exception inner) : base(message, inner)
        { }
    }
}