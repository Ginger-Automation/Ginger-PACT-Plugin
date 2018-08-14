using Ginger_PACT_Plugin;
using GingerPlugInsNET.ActionsLib;
using GingerTestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GingerPACTPluginTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void StartServer()
        {
            //Arrange
            PACTService service = new PACTService();
            GingerAction GA = new GingerAction("StartService");


            //Act
            service.StartPACTServer(ref GA, 1234);

            //Assert
            Assert.AreEqual("PACT Mock Server Started on port: 1234 http://localhost:1234", GA.ExInfo, "Exinfo message");
            Assert.AreEqual(null, GA.Errors, "No Errors");

        }

        [TestMethod]
        public void LoadInteractionsFile()
        {
            //Arrange
            PACTService service = new PACTService();
            GingerAction GA = new GingerAction("StartService");

            //Act
            service.StartPACTServer(ref GA, 5555);
            GingerAction GA2 = new GingerAction("LoadInteractionsFile");
            service.LoadInteractionsFile(ref GA2, TestResources.GetTestResourcesFile("Sample.PACT.json"));

            //Assert
            Assert.AreEqual("PACT Mock Server Started on port: 55555 http://localhost:5555", GA.ExInfo, "Exinfo message");
            Assert.AreEqual(null, GA.Errors, "No Errors");

        }
    }
}