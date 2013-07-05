using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDN2.Exceptions
{
    class GObjectException
    {
    }

    class ListDataNotFindException : Exception
    {
        public ListDataNotFindException()
            : base() { }

        public ListDataNotFindException(string message)
            : base(message) { }

        public ListDataNotFindException(string message, Exception innerException)
            : base(message, innerException) { }
    }

    class ObjectDataNotFindException : Exception
    {
        public ObjectDataNotFindException()
            : base() { }

        public ObjectDataNotFindException(string message)
            : base(message) { }

        public ObjectDataNotFindException(string message, Exception innerException)
            : base(message, innerException) { }
    }

    class ObjectTypeUndefineException : Exception
    {
        public ObjectTypeUndefineException()
            : base() { }

        public ObjectTypeUndefineException(string message)
            : base(message) { }

        public ObjectTypeUndefineException(string message, Exception innerException)
            : base(message, innerException) { }
    }


    class ObjectTypeMismatchingException : Exception
    {
        public ObjectTypeMismatchingException()
            : base() { }

        public ObjectTypeMismatchingException(string message)
            : base(message) { }

        public ObjectTypeMismatchingException(string message, Exception innerException)
            : base(message, innerException) { }
    }

    class AttributeNotFindException : Exception
    {
        public AttributeNotFindException()
            : base() { }

        public AttributeNotFindException(string message)
            : base(message) { }

        public AttributeNotFindException(string message, Exception innerException)
            : base(message, innerException) { }
    }

}
