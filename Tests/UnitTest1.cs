using System.Diagnostics;
using FlaUI.UIA3;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Conditions;
using System.Reflection;
namespace Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void zeroAtStartTest()
        {
            // H�mta s�kv�gen till aktuell katalog
            string currentDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

            // Starta applikationen
            var app = FlaUI.Core.Application.Launch(Path.Combine(currentDirectory, "..", "..", "..", "..", "checkout-system-cafe\\bin\\Debug\\net8.0-windows\\checkout-system-cafe.exe"));

            // Anv�nd UIA3Automation f�r att automatisera interaktion med UI
            using (var automation = new UIA3Automation())
            {
                // H�mta huvudf�nstret f�r applikationen
                var window = app.GetMainWindow(automation);

                // Skapa en fabrik f�r villkor f�r att s�ka efter UI-element
                ConditionFactory cf = new ConditionFactory(new UIA3PropertyLibrary());

                // Hitta etiketten som visar totalpriset
                Label totalpricelabel = window.FindFirstDescendant(cf.ByAutomationId("Totalpris")).AsLabel();

                // Kontrollera att totalpriset �r "0 kr"
                Trace.Assert(totalpricelabel.Text == "0 kr", "Could not find 0 kr");
            }
        }

        [TestMethod]
        public void oneCoffeeTest()
        {
            string currentDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            var app = FlaUI.Core.Application.Launch(Path.Combine(currentDirectory, "..", "..", "..", "..", "checkout-system-cafe\\bin\\Debug\\net8.0-windows\\checkout-system-cafe.exe"));
            using (var automation = new UIA3Automation())
            {
                var window = app.GetMainWindow(automation);
                ConditionFactory cf = new ConditionFactory(new UIA3PropertyLibrary());

                Button coffeebutton = window.FindFirstDescendant(cf.ByAutomationId("Kaffe")).AsButton();
                Label totalpricelabel = window.FindFirstDescendant(cf.ByAutomationId("Totalpris")).AsLabel();

                coffeebutton.Click();
                Trace.Assert(totalpricelabel.Text == "15 kr", "Could not find 15 kr");
            }

        }

        [TestMethod]
        public void threeCoffeeTest()
        {
            string currentDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            var app = FlaUI.Core.Application.Launch(Path.Combine(currentDirectory, "..", "..", "..", "..", "checkout-system-cafe\\bin\\Debug\\net8.0-windows\\checkout-system-cafe.exe"));
            using (var automation = new UIA3Automation())
            {
                var window = app.GetMainWindow(automation);
                ConditionFactory cf = new ConditionFactory(new UIA3PropertyLibrary());

                Button coffeebutton = window.FindFirstDescendant(cf.ByAutomationId("Kaffe")).AsButton();
                Label totalpricelabel = window.FindFirstDescendant(cf.ByAutomationId("Totalpris")).AsLabel();

                coffeebutton.Click();
                coffeebutton.Click();
                coffeebutton.Click();
                Trace.Assert(totalpricelabel.Text == "45 kr", "Total price does not work");
            }

        }

        [TestMethod]
        public void resetButtonTest()
        {
            string currentDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            var app = FlaUI.Core.Application.Launch(Path.Combine(currentDirectory, "..", "..", "..", "..", "checkout-system-cafe\\bin\\Debug\\net8.0-windows\\checkout-system-cafe.exe"));
            using (var automation = new UIA3Automation())
            {
                var window = app.GetMainWindow(automation);
                ConditionFactory cf = new ConditionFactory(new UIA3PropertyLibrary());

                Button coffeebutton = window.FindFirstDescendant(cf.ByAutomationId("Kaffe")).AsButton();
                Button resetbutton = window.FindFirstDescendant(cf.ByAutomationId("Nollst�ll")).AsButton();
                Label totalpricelabel = window.FindFirstDescendant(cf.ByAutomationId("Totalpris")).AsLabel();

                coffeebutton.Click();
                coffeebutton.Click();
                coffeebutton.Click();
                resetbutton.Click();
                Trace.Assert(totalpricelabel.Text == "0 kr", "Reset button does not work");
            }

        }

        [TestMethod]
        public void resetAndCountinueTest()
        {
            string currentDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            var app = FlaUI.Core.Application.Launch(Path.Combine(currentDirectory, "..", "..", "..", "..", "checkout-system-cafe\\bin\\Debug\\net8.0-windows\\checkout-system-cafe.exe"));
            using (var automation = new UIA3Automation())
            {
                var window = app.GetMainWindow(automation);
                ConditionFactory cf = new ConditionFactory(new UIA3PropertyLibrary());

                Button coffeebutton = window.FindFirstDescendant(cf.ByAutomationId("Kaffe")).AsButton();
                Button resetbutton = window.FindFirstDescendant(cf.ByAutomationId("Nollst�ll")).AsButton();
                Label totalpricelabel = window.FindFirstDescendant(cf.ByAutomationId("Totalpris")).AsLabel();

                coffeebutton.Click();
                resetbutton.Click();
                coffeebutton.Click();
                Trace.Assert(totalpricelabel.Text == "15 kr", "Add coffee combined with reset price does not work");
            }

        }

        [TestMethod]
        public void resetWhenZeroTest()
        {
            string currentDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            var app = FlaUI.Core.Application.Launch(Path.Combine(currentDirectory, "..", "..", "..", "..", "checkout-system-cafe\\bin\\Debug\\net8.0-windows\\checkout-system-cafe.exe"));
            using (var automation = new UIA3Automation())
            {
                var window = app.GetMainWindow(automation);
                ConditionFactory cf = new ConditionFactory(new UIA3PropertyLibrary());

                Button resetbutton = window.FindFirstDescendant(cf.ByAutomationId("Nollst�ll")).AsButton();
                Label totalpricelabel = window.FindFirstDescendant(cf.ByAutomationId("Totalpris")).AsLabel();

                resetbutton.Click();
                Trace.Assert(totalpricelabel.Text == "0 kr", "Reset when total is zero does not work");
            }

        }
    }
}

