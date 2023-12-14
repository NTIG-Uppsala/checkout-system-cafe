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
        private Window? _window;
        private ConditionFactory? _cf;

        private readonly string _pathToExecutable;

        public UnitTest1()
        {
            string currentDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location) ?? throw new Exception();
            _pathToExecutable = Path.Combine(currentDirectory, "..", "..", "..", "..", "checkout-system-cafe\\bin\\Debug\\net8.0-windows\\checkout-system-cafe.exe");
        }

        [TestInitialize]
        public void TestInitialize()
        {
            var app = FlaUI.Core.Application.Launch(_pathToExecutable);
            using var automation = new UIA3Automation();
            _window = app.GetMainWindow(automation);
            _cf = new ConditionFactory(new UIA3PropertyLibrary());
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _window.Close();
        }

        [TestMethod]
        public void ZeroAtStartTest()
        {
            Label totalpricelabel = _window.FindFirstDescendant(_cf.ByAutomationId("totalPrice")).AsLabel();
            Trace.Assert(totalpricelabel.Text == "0 kr", "Could not find 0 kr");
        }

        [TestMethod]
        public void OneCoffeeTest()
        {
            Button coffeebutton = _window.FindFirstDescendant(_cf.ByAutomationId("kaffe")).AsButton();
            Label totalpricelabel = _window.FindFirstDescendant(_cf.ByAutomationId("totalPrice")).AsLabel();

            coffeebutton.Click();
            Trace.Assert(totalpricelabel.Text == "15 kr", "Could not find 15 kr");
        }

        [TestMethod]
        public void ThreeCoffeeTest()
        {
            Button coffeebutton = _window.FindFirstDescendant(_cf.ByAutomationId("kaffe")).AsButton();
            Label totalpricelabel = _window.FindFirstDescendant(_cf.ByAutomationId("totalPrice")).AsLabel();

            coffeebutton.Click();
            coffeebutton.Click();
            coffeebutton.Click();
            Trace.Assert(totalpricelabel.Text == "45 kr", "Total price does not work");
        }

        [TestMethod]
        public void ThreeCappuccinoTest()
        {
            Button cappuccinobutton = _window.FindFirstDescendant(_cf.ByAutomationId("cappuccino")).AsButton();
            Label totalpricelabel = _window.FindFirstDescendant(_cf.ByAutomationId("totalPrice")).AsLabel();

            cappuccinobutton.Click();
            cappuccinobutton.Click();
            cappuccinobutton.Click();
            Trace.Assert(totalpricelabel.Text == "90 kr", "Could not find 90 kr");
        }

        [TestMethod]
        public void ResetButtonTest()
        {
            Button coffeebutton = _window.FindFirstDescendant(_cf.ByAutomationId("kaffe")).AsButton();
            Button resetbutton = _window.FindFirstDescendant(_cf.ByAutomationId("reset")).AsButton();
            Label totalpricelabel = _window.FindFirstDescendant(_cf.ByAutomationId("totalPrice")).AsLabel();

            coffeebutton.Click();
            coffeebutton.Click();
            coffeebutton.Click();
            resetbutton.Click();
            Trace.Assert(totalpricelabel.Text == "0 kr", "Reset button does not work");
        }

        [TestMethod]
        public void ResetAndContinueTest()
        {
            Button coffeebutton = _window.FindFirstDescendant(_cf.ByAutomationId("kaffe")).AsButton();
            Button resetbutton = _window.FindFirstDescendant(_cf.ByAutomationId("reset")).AsButton();
            Label totalpricelabel = _window.FindFirstDescendant(_cf.ByAutomationId("totalPrice")).AsLabel();

            coffeebutton.Click();
            resetbutton.Click();
            coffeebutton.Click();
            Trace.Assert(totalpricelabel.Text == "15 kr", "Add coffee combined with reset price does not work");
        }

        [TestMethod]
        public void ResetWhenZeroTest()
        {
            Button resetbutton = _window.FindFirstDescendant(_cf.ByAutomationId("reset")).AsButton();
            Label totalpricelabel = _window.FindFirstDescendant(_cf.ByAutomationId("totalPrice")).AsLabel();

            resetbutton.Click();
            Trace.Assert(totalpricelabel.Text == "0 kr", "Reset when total is zero does not work");
        }
    }
}