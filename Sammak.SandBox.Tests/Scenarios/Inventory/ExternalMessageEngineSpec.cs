using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using Sammak.SandBox.Inventory;
using Sammak.SandBox.Inventory.ConsumerHandlers;
using Sammak.SandBox.Inventory.ExtenderWrapper;
using Sammak.SandBox.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Sammak.SandBox.Tests.Scenarios.Inventory
{
    public class ExternalMessageEngineSpec
    {
        private Mock<IExternalMessageEngine> _externalMessageEngineMock;
        private Mock<ILogger> _loggerMock;
        private ExternalMessageHandler _handlerToTest;
        private ExternalMessageHandlerWithComparables _handlerWithComparablesToTest;

        #region Tests

        [SetUp]
        public void TestInit()
        {
            _externalMessageEngineMock = new Mock<IExternalMessageEngine>();
            _loggerMock = new Mock<ILogger>();
            _handlerToTest = new ExternalMessageHandler(_loggerMock.Object, _externalMessageEngineMock.Object);
            _handlerWithComparablesToTest = new ExternalMessageHandlerWithComparables(_loggerMock.Object, _externalMessageEngineMock.Object);
        }

        [Test]
        public void ExternalMessageHandler_Calls_ExternalMessageEngine_Process()
        {
            //Arrange
            //_externalMessageEngineMock.Setup(m => m.Process(It.IsAny<ExternalMessagePayload>())).Verifiable();

            //Act
            _handlerToTest.Handle("");

            //Assert
            _externalMessageEngineMock.Verify(m => m.Process(It.IsAny<ExternalMessagePayload>()), Times.Once);
        }

        [Test]
        public void ExternalMessageHandler_Calls_ExternalMessageEngine_Process_With_Payload()
        {
            //Arrange
            var payload = new ExternalMessagePayload
            {
                MessageType = ExternalMessageType.CancelOrder,
                Id = Guid.NewGuid().ToString(),
                SourceSystemId = 1,
                ParameterDictionary = new Dictionary<string, string>
                {
                    {"key1", "value1" }
                },
                UserName = "Test User"
            };
            Expression<Func<ExternalMessagePayload, bool>> matchedParam =
                p =>
                    p.MessageType == payload.MessageType &&
                    p.Id == payload.Id &&
                    p.UserName == payload.UserName;

            //Act
            //mimic message event with json string as payload
            _handlerToTest.Handle(JsonConvert.SerializeObject(payload));

            //Assert
            _externalMessageEngineMock.Verify(m => m.Process(It.Is(matchedParam)), Times.Once);
        }

         [Test]
        public void ExternalMessageHandlerWithComparables_Calls_ExternalMessageEngine_ProcessWithComparables_With_Payload()
        {
            //Arrange
            var payload = new ComparableExternalMessagePayload
            {
                MessageType = ExternalMessageType.CancelOrder,
                OrderId = Guid.NewGuid().ToString(),
                SourceSystemId = 1,
                Metadata = new ComparableDictionary<string, string>
                {
                    {"key1", "value1" }
                },
                Username = "Test User"
            };
            Expression<Func<ComparableExternalMessagePayload, bool>> matchedParam =
                p =>
                    p.MessageType == payload.MessageType &&
                    p.OrderId == payload.OrderId &&
                    p.Username == payload.Username;

            //Act
            //mimic message event with json string as payload
            _handlerWithComparablesToTest.Handle(JsonConvert.SerializeObject(payload));

            //Assert
            _externalMessageEngineMock.Verify(m => m.ProcessWithComparables(It.Is(matchedParam)), Times.Once);
            _externalMessageEngineMock.Verify(m => m.ProcessWithComparables(payload), Times.Once);
        }

       #endregion
    }

    #region Temporary extension to the interface to test 

    //public static class OrderEngineExtensions
    //{
    //    public static void HandleExternalMessage(this IOrderEngine orderEngine, ExternalMessagePayload payload)
    //    {
    //        // TODO: some temporary implementation
    //    }
    //}

    #endregion
}
