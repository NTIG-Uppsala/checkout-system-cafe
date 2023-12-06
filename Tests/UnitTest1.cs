using System.Diagnostics;
using System.Reflection;

using FlaUI.Core.AutomationElements;
using FlaUI.Core.Conditions;
using FlaUI.UIA3;
namespace Tests
{
    [TestClass]
    public class UnitTest1
    {
        private readonly string _pathToExecutable;

        public UnitTest1()
        {
            // Hämta sökvägen till aktuell katalog
            string currentDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location) ?? throw new Exception();
            _pathToExecutable = Path.Combine(currentDirectory, "..", "..", "..", "..", "checkout-system-cafe\\bin\\Debug\\net8.0-windows\\checkout-system-cafe.exe");
        }

        public Tuple<Window, ConditionFactory> StartWindowHelper()
        {
            // Starta applikationen
            var app = FlaUI.Core.Application.Launch(_pathToExecutable);

            // Använd UIA3Automation för att automatisera interaktion med UI
            using var automation = new UIA3Automation();
            // Hämta huvudfönstret för applikationen
            var window = app.GetMainWindow(automation);

            // Skapa en fabrik för villkor för att söka efter UI-element
            ConditionFactory cf = new ConditionFactory(new UIA3PropertyLibrary());

            return new Tuple<Window, ConditionFactory>(window, cf);
        }

        [TestMethod]
        public void zeroAtStartTest()
        {
            (Window window, ConditionFactory cf) = StartWindowHelper();

            // Hitta etiketten som visar totalpriset
            Label totalpricelabel = window.FindFirstDescendant(cf.ByAutomationId("Totalpris")).AsLabel();

            // Kontrollera att totalpriset är "0 kr"
            Trace.Assert(totalpricelabel.Text == "0 kr", "Could not find 0 kr");
        }

        [TestMethod]
        public void oneCoffeeTest()
        {
            (Window window, ConditionFactory cf) = StartWindowHelper();

            Button coffeebutton = window.FindFirstDescendant(cf.ByAutomationId("Kaffe")).AsButton();
            Label totalpricelabel = window.FindFirstDescendant(cf.ByAutomationId("Totalpris")).AsLabel();

            coffeebutton.Click();
            Trace.Assert(totalpricelabel.Text == "15 kr", "Could not find 15 kr");

        }

        [TestMethod]
        public void threeCoffeeTest()
        {
            (Window window, ConditionFactory cf) = StartWindowHelper();

            Button coffeebutton = window.FindFirstDescendant(cf.ByAutomationId("Kaffe")).AsButton();
            Label totalpricelabel = window.FindFirstDescendant(cf.ByAutomationId("Totalpris")).AsLabel();

            coffeebutton.Click();
            coffeebutton.Click();
            coffeebutton.Click();
            Trace.Assert(totalpricelabel.Text == "45 kr", "Total price does not work");

        }

        [TestMethod]
        public void resetButtonTest()
        {
            (Window window, ConditionFactory cf) = StartWindowHelper();

            Button coffeebutton = window.FindFirstDescendant(cf.ByAutomationId("Kaffe")).AsButton();
            Button resetbutton = window.FindFirstDescendant(cf.ByAutomationId("Nollställ")).AsButton();
            Label totalpricelabel = window.FindFirstDescendant(cf.ByAutomationId("Totalpris")).AsLabel();

            coffeebutton.Click();
            coffeebutton.Click();
            coffeebutton.Click();
            resetbutton.Click();
            Trace.Assert(totalpricelabel.Text == "0 kr", "Reset button does not work");

        }

        [TestMethod]
        public void resetAndCountinueTest()
        {
            (Window window, ConditionFactory cf) = StartWindowHelper();

            Button coffeebutton = window.FindFirstDescendant(cf.ByAutomationId("Kaffe")).AsButton();
            Button resetbutton = window.FindFirstDescendant(cf.ByAutomationId("Nollställ")).AsButton();
            Label totalpricelabel = window.FindFirstDescendant(cf.ByAutomationId("Totalpris")).AsLabel();

            coffeebutton.Click();
            resetbutton.Click();
            coffeebutton.Click();
            Trace.Assert(totalpricelabel.Text == "15 kr", "Add coffee combined with reset price does not work");

        }

        [TestMethod]
        public void resetWhenZeroTest()
        {

            (Window window, ConditionFactory cf) = StartWindowHelper();
            Button resetbutton = window.FindFirstDescendant(cf.ByAutomationId("Nollställ")).AsButton();
            Label totalpricelabel = window.FindFirstDescendant(cf.ByAutomationId("Totalpris")).AsLabel();

            resetbutton.Click();
            Trace.Assert(totalpricelabel.Text == "0 kr", "Reset when total is zero does not work");

        }
    }
}