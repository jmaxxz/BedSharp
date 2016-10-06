using RestSharp;
using RestSharp.Extensions.MonoHttp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BedSharp
{
    public class FakeRestPredicate
    {
        private static Predicate<IRestRequest> tautology = (x) => true;
        private Predicate<IRestRequest> predicate;

        public IEnumerable<FakeRestResponse> ExistingResponses { get; private set; }
        public FakeRestResponse Respond { get; private set; }
        public FakeRestResponse Timeout { get; private set; }

        private FakeRestPredicate(FakeRestPredicate fake, Predicate<IRestRequest> pr = null)
            : this(fake.ExistingResponses, req => fake.predicate(req) && (pr ?? tautology)(req))
        {
        }

        internal FakeRestPredicate(Predicate<IRestRequest> pr = null)
            : this(Enumerable.Empty<FakeRestResponse>(), pr)
        {
        }

        internal FakeRestPredicate(IEnumerable<FakeRestResponse> existing, Predicate<IRestRequest> pr = null)
        {
            predicate = pr ?? tautology;
            ExistingResponses = existing;
            Respond = new FakeRestResponse(existing, predicate);
            Timeout = Respond.Status(ResponseStatus.TimedOut);
        }

        internal FakeRestPredicate Verb(string verb)
        {
            return new FakeRestPredicate(this, x => x.Method.ToString().Equals(verb, StringComparison.InvariantCultureIgnoreCase));
        }

        public FakeRestPredicate Url(string url)
        {
            return new FakeRestPredicate(this, req =>
            {
                string finalUrl = req.Resource;

                foreach (var p in req.Parameters)
                {
                    finalUrl = finalUrl.Replace("{" + p.Name + "}", HttpUtility.UrlEncodeUnicode(p.Value.ToString()));
                }
                return finalUrl == url;
            });
        }
    }
}
