using RestSharp;
using RestSharp.Contrib;
using System;

namespace BedSharp
{
    public class FakeRestPredicate
    {
        private static Predicate<IRestRequest> tautology = (x) => true;
        private Predicate<IRestRequest> predicate;

        public FakeRestResponse Respond { get; private set; }
        public FakeRestResponse Timeout { get; private set; }

        private FakeRestPredicate(FakeRestPredicate fake, Predicate<IRestRequest> pr = null) 
            : this(req => fake.predicate(req) && (pr ?? tautology)(req))
        {
        }

        internal FakeRestPredicate(Predicate<IRestRequest> pr = null)
        {
            predicate = pr ?? tautology;
            Respond = new FakeRestResponse(predicate);
            Timeout = new FakeRestResponse(predicate).Status(ResponseStatus.TimedOut);
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
