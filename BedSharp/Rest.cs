using System;

namespace BedSharp
{
    public static class Rest
    {
        public static FakeRestPredicate On()
        {
            return new FakeRestPredicate();
        }

        public static FakeRestPredicate On(string verb)
        {
            return new FakeRestPredicate(x=>x.Method.ToString().Equals(verb, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
