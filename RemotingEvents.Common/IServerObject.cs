using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RemotingEvents.Common
{
    public interface IServerObject
    {

        #region Events

        event MessageArrivedEvent MessageArrived;

        #endregion

        #region Methods

        void PublishMessage(string Message);
        string HelloWorld();

        #endregion

    }
}
