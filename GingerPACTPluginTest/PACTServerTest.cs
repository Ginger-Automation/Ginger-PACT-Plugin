using Amdocs.Ginger.Plugin.Core;
using Ginger_PACT_Plugin;
using GingerTestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GingerPACTPluginTest
{
    [TestClass]
    public class PACTServerTest
    {
        [TestMethod]
        public void StartServer()
        {
            //Arrange
            PACTService service = new PACTService();
            GingerAction GA = new GingerAction();


            //Act
            service.StartPACTServer(GA, 1234);

            //Assert
            //Assert.AreEqual("PACT Mock Server Started on port: 1234 http://localhost:1234", GA.ExInfo, "Exinfo message");
            Assert.AreEqual(null, GA.Errors, "No Errors");

        }

        [TestMethod]
        public void LoadInteractionsFile()      {
            //Arrange
            PACTService service = new PACTService();
            GingerAction GA = new GingerAction();
            service.StartPACTServer(GA, 5555);
            GingerAction GA2 = new GingerAction();
            string fileName = TestResources.GetTestResourcesFile("Sample.PACT.json");

            //Act
            service.LoadInteractionsFile(GA2, fileName);

            //Assert
            //Assert.AreEqual("PACT Mock Server Started on port: 5555 http://localhost:5555", GA.ExInfo, "Exinfo message");
            Assert.AreEqual(null, GA2.Errors, "No Errors");

        }


        [TestMethod]
        public void ClearInteractions()
        {
            //Arrange
            PACTService service = new PACTService();
            GingerAction GA = new GingerAction();
            service.StartPACTServer(GA, 6677);
            GingerAction GA2 = new GingerAction();
            string fileName = TestResources.GetTestResourcesFile("Sample.PACT.json");
            service.LoadInteractionsFile(GA2, fileName);

            //Act
            GingerAction GA3 = new GingerAction();
            service.ClearInteractions(GA3);

            //Assert            
            Assert.AreEqual(null, GA2.Errors, "No Errors");

        }
    }
}
