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
            // H�mta s�kv�gen till aktuell katalog
            string currentDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location) ?? throw new Exception();
            _pathToExecutable = Path.Combine(currentDirectory, "..", "..", "..", "..", "checkout-system-cafe\\bin\\Debug\\net8.0-windows\\checkout-system-cafe.exe");
        }

        public Tuple<Window, ConditionFactory> StartWindowHelper()
        {
            // Start application
            var app = FlaUI.Core.Application.Launch(_pathToExecutable);

            // Use UIA3Automation automate integration with UI
            using var automation = new UIA3Automation();
            // Get main window for application
            var window = app.GetMainWindow(automation);

            // Create a fabric for condition to search for UI-element
            ConditionFactory cf = new(new UIA3PropertyLibrary());

            return new Tuple<Window, ConditionFactory>(window, cf);
        }

        [TestMethod]
        public void ZeroAtStartTest()
        {
            (Window window, ConditionFactory cf) = StartWindowHelper();

            // F�nd label showing total price
            Label totalpricelabel = window.FindFirstDescendant(cf.ByAutomationId("totalPrice")).AsLabel();

            // "0 kr" totalprice control
            Trace.Assert(totalpricelabel.Text == "0 kr", "Could not find 0 kr");
            window.Close();
        }

        [TestMethod]
        public void OneCoffeeTest()
        {
            (Window window, ConditionFactory cf) = StartWindowHelper();

            Button coffeebutton = window.FindFirstDescendant(cf.ByAutomationId("coffee")).AsButton();
            Label totalpricelabel = window.FindFirstDescendant(cf.ByAutomationId("totalPrice")).AsLabel();

            coffeebutton.Click();
            Trace.Assert(totalpricelabel.Text == "15 kr", "Could not find 15 kr");
            window.Close();
        }

        [TestMethod]
        public void ThreeCoffeeTest()
        {
            (Window window, ConditionFactory cf) = StartWindowHelper();

            Button coffeebutton = window.FindFirstDescendant(cf.ByAutomationId("coffee")).AsButton();
            Label totalpricelabel = window.FindFirstDescendant(cf.ByAutomationId("totalPrice")).AsLabel();

            coffeebutton.Click();
            coffeebutton.Click();
            coffeebutton.Click();
            Trace.Assert(totalpricelabel.Text == "45 kr", "Total price does not work");
            window.Close();
        }

        [TestMethod]
        public void ResetButtonTest()
        {
            (Window window, ConditionFactory cf) = StartWindowHelper();

            Button coffeebutton = window.FindFirstDescendant(cf.ByAutomationId("coffee")).AsButton();
            Button resetbutton = window.FindFirstDescendant(cf.ByAutomationId("reset")).AsButton();
            Label totalpricelabel = window.FindFirstDescendant(cf.ByAutomationId("totalPrice")).AsLabel();

            coffeebutton.Click();
            coffeebutton.Click();
            coffeebutton.Click();
            resetbutton.Click();
            Trace.Assert(totalpricelabel.Text == "0 kr", "Reset button does not work");
            window.Close();
        }

        [TestMethod]
        public void ResetAndCountinueTest()
        {
            (Window window, ConditionFactory cf) = StartWindowHelper();

            Button coffeebutton = window.FindFirstDescendant(cf.ByAutomationId("coffee")).AsButton();
            Button resetbutton = window.FindFirstDescendant(cf.ByAutomationId("reset")).AsButton();
            Label totalpricelabel = window.FindFirstDescendant(cf.ByAutomationId("totalPrice")).AsLabel();

            coffeebutton.Click();
            resetbutton.Click();
            coffeebutton.Click();
            Trace.Assert(totalpricelabel.Text == "15 kr", "Add coffee combined with reset price does not work");
            window.Close();
        }

        [TestMethod]
        public void ResetWhenZeroTest()
        {
            (Window window, ConditionFactory cf) = StartWindowHelper();
            Button resetbutton = window.FindFirstDescendant(cf.ByAutomationId("reset")).AsButton();
            Label totalpricelabel = window.FindFirstDescendant(cf.ByAutomationId("totalPrice")).AsLabel();

            resetbutton.Click();
            Trace.Assert(totalpricelabel.Text == "0 kr", "Reset when total is zero does not work");
            window.Close();
        }
    }
}