using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using XCYN.Print.rabbitmq;

namespace XCYN.Test
{
    [TestClass]
    public class MQTest
    {
        [TestMethod]
        public void PublishBasic()
        {
            Publish.PublishBasic();
        }

        [TestMethod]
        public void ConsumerAck()
        {
            Consumer.ConsumerAck();
        }

        [TestMethod]
        public void ConsumerReject()
        {
            Consumer.ConsumerReject();
        }

        [TestMethod]
        public void PublishTX()
        {
            Publish.PublishTX();
        }

    }
}
