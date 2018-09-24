using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using XCYN.Print.rabbitmq;

namespace XCYN.Test
{
    [TestClass]
    public class MQTestCase
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

        [TestMethod]
        public void PublishPersistent()
        {
            Publish.PublishPersistent();
        }

        [TestMethod]
        public void MyTestMethod()
        {
            for (int i = 0; i < 100; i++)
            {
                var s = i;
            }
        }

    }
}
