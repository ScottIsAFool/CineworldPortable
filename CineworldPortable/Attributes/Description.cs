using System;

namespace CineworldPortable.Attributes
{
    internal class Description : Attribute
    {
        public string Text;

        public Description(string text)
        {
            Text = text;
        }
    }
}