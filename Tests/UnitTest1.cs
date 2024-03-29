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
        private Window _window;
        private ConditionFactory _cf;

        private readonly string _pathToExecutable;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public UnitTest1()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
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
            Trace.Assert(totalpricelabel.Text == "0,00 kr", "Could not find 0,00 kr");
        }

        [TestMethod]
        public void OneCoffeeTest()
        {
            Button coffeebutton = _window.FindFirstDescendant(_cf.ByAutomationId("kaffe")).AsButton();
            Label totalpricelabel = _window.FindFirstDescendant(_cf.ByAutomationId("totalPrice")).AsLabel();

            coffeebutton.Click();
            Trace.Assert(totalpricelabel.Text == "15,00 kr", "Could not find 15,00 kr");
        }

        [TestMethod]
        public void OneBunTest()
        {
            Button bunbutton = _window.FindFirstDescendant(_cf.ByAutomationId("bulle")).AsButton();
            Label totalpricelabel = _window.FindFirstDescendant(_cf.ByAutomationId("totalPrice")).AsLabel();

            bunbutton.Click();
            Trace.Assert(totalpricelabel.Text == "12,50 kr", "Could not find 12,50 kr");
        }

        [TestMethod]
        public void ThreeCoffeeTest()
        {
            Button coffeebutton = _window.FindFirstDescendant(_cf.ByAutomationId("kaffe")).AsButton();
            Label totalpricelabel = _window.FindFirstDescendant(_cf.ByAutomationId("totalPrice")).AsLabel();

            coffeebutton.Click();
            coffeebutton.Click();
            coffeebutton.Click();
            Trace.Assert(totalpricelabel.Text == "45,00 kr", "Total price does not work");
        }

        [TestMethod]
        public void ThreeCappuccinoTest()
        {
            Button cappuccinobutton = _window.FindFirstDescendant(_cf.ByAutomationId("cappuccino")).AsButton();
            Label totalpricelabel = _window.FindFirstDescendant(_cf.ByAutomationId("totalPrice")).AsLabel();

            cappuccinobutton.Click();
            cappuccinobutton.Click();
            cappuccinobutton.Click();
            Trace.Assert(totalpricelabel.Text == "90,00 kr", "Could not find 90,00 kr");
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
            Trace.Assert(totalpricelabel.Text == "0,00 kr", "Reset button does not work");
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
            Trace.Assert(totalpricelabel.Text == "15,00 kr", "Add coffee combined with reset price does not work");
        }

        [TestMethod]
        public void ResetWhenZeroTest()
        {
            Button resetbutton = _window.FindFirstDescendant(_cf.ByAutomationId("reset")).AsButton();
            Label totalpricelabel = _window.FindFirstDescendant(_cf.ByAutomationId("totalPrice")).AsLabel();

            resetbutton.Click();
            Trace.Assert(totalpricelabel.Text == "0,00 kr", "Reset when total is zero does not work");
        }

        [TestMethod]
        public void PaymentTest()
        {
            Button coffeebutton = _window.FindFirstDescendant(_cf.ByAutomationId("kaffe")).AsButton();
            Button paymentbutton = _window.FindFirstDescendant(_cf.ByAutomationId("payment")).AsButton();
            Label totalpricelabel = _window.FindFirstDescendant(_cf.ByAutomationId("totalPrice")).AsLabel();

            coffeebutton.Click();
            coffeebutton.Click();
            paymentbutton.Click();
            Trace.Assert(totalpricelabel.Text == "0,00 kr", "Payment button does not work");
        }

        [TestMethod]
        public void ChosenProductsTest()
        {
            Button coffeebutton = _window.FindFirstDescendant(_cf.ByAutomationId("kaffe")).AsButton();
            Button bunbutton = _window.FindFirstDescendant(_cf.ByAutomationId("bulle")).AsButton();

            coffeebutton.Click();
            bunbutton.Click();

            DataGridView productwindow = _window.FindFirstDescendant(_cf.ByAutomationId("dataGrid")).AsDataGridView();

            // List of item names which should be seen in the product grid
            string[] productstocheck = ["Kaffe", "Bulle"];

            // Checks that item names is displayed in any row or cell in the product grid
            foreach (var product in productstocheck)
            {
                var productexists = productwindow.Rows.Any(row =>
                    row.Cells.Any(cell => cell.Value != null && cell.Value.ToString() == product));

                // Checks if productexists is true, otherwise it will show an error message 
                Trace.Assert(productexists, $"Product '{product}' not found in the product grid.");
            }
        }

        [TestMethod]
        public void MultipleChosenProductsTest()
        {
            Button cappuccinobutton = _window.FindFirstDescendant(_cf.ByAutomationId("cappuccino")).AsButton();

            cappuccinobutton.Click();
            cappuccinobutton.Click();
            cappuccinobutton.Click();

            DataGridView productwindow = _window.FindFirstDescendant(_cf.ByAutomationId("dataGrid")).AsDataGridView();

            // Checks that correct amount is displayed in any row or cell in the product grid
            var correctamount = productwindow.Rows.Any(row =>
                row.Cells.Any(cell => cell.Value != null && Convert.ToInt32(cell.Value) == 3));

            // Checks if correctamount is true, otherwise it will show an error message 
            Trace.Assert(correctamount, $"Amount '3' not found in the product grid.");
        }

        [TestMethod]
        public void ProductHistoryTest()
        {
            Button cappuccinobutton = _window.FindFirstDescendant(_cf.ByAutomationId("cappuccino")).AsButton();
            Button coffeebutton = _window.FindFirstDescendant(_cf.ByAutomationId("kaffe")).AsButton();
            Button iceteabutton = _window.FindFirstDescendant(_cf.ByAutomationId("iste")).AsButton();
            Button paymentbutton = _window.FindFirstDescendant(_cf.ByAutomationId("payment")).AsButton();
            Button showhistorybutton = _window.FindFirstDescendant(_cf.ByAutomationId("showHistory")).AsButton();

            cappuccinobutton.Click();
            coffeebutton.Click();
            paymentbutton.Click();
            iceteabutton.Click();
            paymentbutton.Click();
            showhistorybutton.Click();

            DataGridView historywindow = _window.FindFirstDescendant(_cf.ByAutomationId("historyDataGrid")).AsDataGridView();

            string[] productstocheck = ["Cappuccino", "Kaffe", "Iste"];

            // Loop that verifies so every payed product exists in the history product grid
            foreach (var product in productstocheck)
            {
                var productexists = historywindow.Rows.Any(row =>
                    row.Cells.Any(cell => cell.Value != null && cell.Value.ToString() == product));

                Trace.Assert(productexists, $"Product '{product}' not found in the product grid.");
            }
        }
        [TestMethod]
        public void RegretButtonTest()
        {
            Button coffeebutton = _window.FindFirstDescendant(_cf.ByAutomationId("kaffe")).AsButton();
            Button bunbutton = _window.FindFirstDescendant(_cf.ByAutomationId("bulle")).AsButton();
            Button regretbutton = _window.FindFirstDescendant(_cf.ByAutomationId("regret")).AsButton();
            Label totalpricelabel = _window.FindFirstDescendant(_cf.ByAutomationId("totalPrice")).AsLabel();

            coffeebutton.Click();
            bunbutton.Click();
            regretbutton.Click();

            DataGridView productwindow = _window.FindFirstDescendant(_cf.ByAutomationId("dataGrid")).AsDataGridView();

            Trace.Assert(totalpricelabel.Text == "15,00 kr", "Total price does not update correctly when regret button is pressed");
            var correctproduct = productwindow.Rows.Any(row =>
                row.Cells.Any(cell => cell.Value != null && cell.Value.ToString() == "Kaffe")); // Verifies that coffee is the only product left in the dataGrid
            Trace.Assert(correctproduct, "Correct product 'Kaffe' not found when '�ngra' button is pressed");
        }
    }
}