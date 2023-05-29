using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace Bank_Orange.MSTest
{
    [TestClass]
    public class BankAccountTest
    {
        [TestMethod]
        public void GetCurrencyExchangesTest_Dollar_Check() 
        {
            // Arrange 
            CurrencyExchanges currencyExchanges = new CurrencyExchanges(18, 25);
            BankAccount account = new BankAccount();

            // Act
            account.GetCurrencyExchanges(currencyExchanges);
            var actual = account.currencyExchanges.DollarCurrencyRate;
            var expected = 25;

            // Assert
            Assert.AreEqual(expected, actual);

        }

        [TestMethod]
        public void GetCurrencyExchangesTest_Euro_Check()
        {
            // Arrange 
            CurrencyExchanges currencyExchanges = new CurrencyExchanges(18, 25);
            BankAccount account = new BankAccount();

            // Act
            account.GetCurrencyExchanges(currencyExchanges);
            var actual = account.currencyExchanges.EuroCurrencyRate;
            var expected = 18;

            // Assert
            Assert.AreEqual(expected, actual);

        }



        // CurrencyConverter Tests vvv
        [TestMethod]
        public void CurrencyConverterTest_From_Kr_To_Euro()
        {
            // Arrange
            CurrencyExchanges currencyExchanges = new CurrencyExchanges(18, 25);
            BankAccount account = new BankAccount();
            account.GetCurrencyExchanges(currencyExchanges);

            // Act
            var actual = account.CurrencyConverter("Kr", "€", 100);
            var expected = (decimal)100/18;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CurrencyConverterTest_From_Euro_To_Kr()
        {
            // Arrange
            CurrencyExchanges currencyExchanges = new CurrencyExchanges(18, 25);
            BankAccount account = new BankAccount();
            account.GetCurrencyExchanges(currencyExchanges);

            // Act
            var actual = account.CurrencyConverter("€", "Kr", 100);
            var expected = (decimal)100 * 18;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CurrencyConverterTest_From_Kr_To_Dollar()
        {
            // Arrange
            CurrencyExchanges currencyExchanges = new CurrencyExchanges(18, 25);
            BankAccount account = new BankAccount();
            account.GetCurrencyExchanges(currencyExchanges);

            // Act
            var actual = account.CurrencyConverter("Kr", "$", 100);
            var expected = (decimal)100 / 25;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CurrencyConverterTest_From_Dollar_To_Kr()
        {
            // Arrange
            CurrencyExchanges currencyExchanges = new CurrencyExchanges(18, 25);
            BankAccount account = new BankAccount();
            account.GetCurrencyExchanges(currencyExchanges);

            // Act
            var actual = account.CurrencyConverter("$", "Kr", 100);
            var expected = (decimal)100 * 25;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CurrencyConverterTest_From_Dollar_To_Euro()
        {
            // Arrange
            CurrencyExchanges currencyExchanges = new CurrencyExchanges(18, 25);
            BankAccount account = new BankAccount();
            account.GetCurrencyExchanges(currencyExchanges);

            // Act
            var actual = account.CurrencyConverter("$", "€", 100);
            var expected = (decimal)100 * 25 / 18;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CurrencyConverterTest_From_Euro_To_Dollar()
        {
            // Arrange
            CurrencyExchanges currencyExchanges = new CurrencyExchanges(18, 25);
            BankAccount account = new BankAccount();
            account.GetCurrencyExchanges(currencyExchanges);

            // Act
            var actual = account.CurrencyConverter("€", "$", 100);
            var expected = (decimal)100 * 18 / 25;

            // Assert
            Assert.AreEqual(expected, actual);
        }
        // CurrencyConverter Tests ^^^


        // had to remove console.clear on line 102 in BankAccount class for test to be successfull
        [TestMethod]
        public void AddNewAccountTest()
        {
            // Arrange
            BankAccount account = new BankAccount();

            // Act
            var input = new StringReader("1\nTest\n1\n1000");
            Console.SetIn(input);
            account.AddNewBankAccount();

            var actualName = account.BankAccountList[0].AccountName;
            var actualType = account.BankAccountList[0].IsSavingsAccount;
            var actualMoney = account.BankAccountList[0].Money;
            var actualCurrency = account.BankAccountList[0].CurrencyType;

            var expectedName = "Test";
            var expectedType = false;
            var expectedMoney = 1000;
            var expectedCurrency = "Kr";


            // Assert
            Assert.AreEqual(expectedName, actualName);
            Assert.AreEqual(expectedType, actualType);
            Assert.AreEqual(expectedMoney, actualMoney);
            Assert.AreEqual(expectedCurrency, actualCurrency);
        }

        [TestMethod]
        public void ReciveMoneyTest()
        {
            // Arrange
            BankAccount bankAccount = new BankAccount();
            var input = new StringReader("1\nTest\n1\n1000");
            Console.SetIn(input);
            bankAccount.AddNewBankAccount();
            decimal moneyToRecive = 1337;

            // Act
            bankAccount.RecievMoney(moneyToRecive, 1);
            decimal actual = bankAccount.TotalMoney();
            decimal expected = 2337;

            Assert.AreEqual(expected, actual);
        }

        // Had to remove console.clear here aswell on 392 in BankAccount class
        [TestMethod]
        public void DisplayLogHistory()
        {
            // Arrange
            var account = new BankAccount();
            string testLog = "This is a test :)";
            account.LogList.Add(testLog);

            // Act
            var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            account.DisplayLogHistory();
            var actualOutput = consoleOutput.ToString();
            var expectedOutput = "\n\tThis is a test :).";

            // Assert
            Assert.AreEqual(expectedOutput, actualOutput);
        }

    }
}
