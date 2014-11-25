﻿using NUnit.Framework;
using PostmarkDotNet;
using System;
using System.Configuration;
using System.Threading.Tasks;

namespace Postmark.PCL.Tests
{
    [TestFixture]
    public abstract class ClientBaseFixture
    {
        private static void AssertSettingsAvailable()
        {
            Assert.NotNull(READ_INBOUND_TEST_SERVER_TOKEN, "READ_INBOUND_TEST_SERVER_TOKEN must be defined as an environment variable or app setting.");
            Assert.NotNull(READ_SELENIUM_TEST_SERVER_TOKEN, "READ_SELENIUM_TEST_SERVER_TOKEN must be defined as an environment variable or app setting.");
            Assert.NotNull(WRITE_ACCOUNT_TOKEN, "WRITE_ACCOUNT_TOKEN must be defined as an environment variable or app setting.");
            Assert.NotNull(WRITE_TEST_SERVER_TOKEN, "WRITE_TEST_SERVER_TOKEN must be defined as an environment variable or app setting.");
            Assert.NotNull(WRITE_TEST_SENDER_EMAIL_ADDRESS, "WRITE_TEST_SENDER_EMAIL_ADDRESS must be defined as an environment variable or app setting.");
            Assert.NotNull(WRITE_TEST_EMAIL_RECIPIENT_ADDRESS, "WRITE_TEST_EMAIL_RECIPIENT_ADDRESS must be defined as an environment variable or app setting.");
        }

        /// <summary>
        /// Retrieve the config variable from the environment, 
        /// or app.config if the environment doesn't specify it.
        /// </summary>
        /// <param name="variableName">The name of the environment variable to get.</param>
        /// <returns></returns>
        private static string ConfigVariable(string variableName)
        {
            return Environment.GetEnvironmentVariable(variableName) ?? ConfigurationManager.AppSettings[variableName];
        }

        public static readonly DateTime TESTING_DATE = DateTime.Now;

        public static readonly string READ_INBOUND_TEST_SERVER_TOKEN = ConfigVariable("READ_INBOUND_TEST_SERVER_TOKEN");
        public static readonly string READ_SELENIUM_TEST_SERVER_TOKEN = ConfigVariable("READ_SELENIUM_TEST_SERVER_TOKEN");

        public static readonly string WRITE_ACCOUNT_TOKEN = ConfigVariable("WRITE_ACCOUNT_TOKEN");
        public static readonly string WRITE_TEST_SERVER_TOKEN = ConfigVariable("WRITE_TEST_SERVER_TOKEN");
        public static readonly string WRITE_TEST_SENDER_EMAIL_ADDRESS = ConfigVariable("WRITE_TEST_SENDER_EMAIL_ADDRESS");
        public static readonly string WRITE_TEST_EMAIL_RECIPIENT_ADDRESS = ConfigVariable("WRITE_TEST_EMAIL_RECIPIENT_ADDRESS");

        protected PostmarkClient _client;

        [SetUp]
        public void RunSetupSynchronously()
        {
            AssertSettingsAvailable();
            Setup();
        }

        public abstract Task Setup();
    }
}
