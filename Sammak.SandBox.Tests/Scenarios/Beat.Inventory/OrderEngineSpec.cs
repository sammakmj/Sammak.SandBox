using Moq;
using NUnit.Framework;
using Sammak.SandBox.Inventory;
using Sammak.SandBox.Inventory.ExtenderWrapper;
using Sammak.SandBox.Models;

namespace Sammak.SandBox.Tests.Scenarios.Inventory
{
    [TestFixture]
    public class OrderEngineSpec
    {
        #region Tests

        [Test]
        public void ExternalMessageHandler_Calls_OrderEngine_HandleExternalMessage()
        {
            // Arrange
            var payload = new ExternalMessagePayload
            {
                MessageType = ExternalMessageType.CreateOrder,
                Username = "username"
            };

            var orderEngine = new OrderEngine();
            var orderEngineMock = new Mock<IOrderEngine>();
            var orderEngineStaticWrapperMock = new Mock<IOrderEngineStaticWrapper>();

            orderEngineStaticWrapperMock.Setup(o => o.HandleExternalMessage(orderEngine, payload)).Verifiable();

            //var handler = new ExternalMessageHandler(_loggerMock.Object, orderEngineMock.Object);

            //// Act
            //handler.Handle(JsonConvert.SerializeObject(payload));

            //// Assert
            //orderEngineMock.Verify(o => o.HandleExternalMessage(payload), Times.Once);
        }

        #endregion
    }

    #region Temporary extension to the interface to test 

    public static class OrderEngineExtensions
    {
        public static void HandleExternalMessage(this IOrderEngine orderEngine, ExternalMessagePayload payload)
        {
            // TODO: some temporary implementation
        }
    }

    #endregion
}
