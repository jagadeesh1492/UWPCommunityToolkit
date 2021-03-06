﻿// ******************************************************************
// Copyright (c) Microsoft. All rights reserved.
// This code is licensed under the MIT License (MIT).
// THE CODE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
// INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
// IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
// DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
// TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH
// THE CODE OR THE USE OR OTHER DEALINGS IN THE CODE.
// ******************************************************************

using Microsoft.Toolkit.Uwp;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Windows.ApplicationModel.Background;

namespace UnitTests.Helpers
{
    [TestClass]
    public class Test_BackgroundTaskHelper
    {
        [TestCleanup]
        public void TestCleanUp()
        {
            if (BackgroundTaskHelper.IsBackgroundTaskRegistered("TaskName") == true)
            {
                BackgroundTaskHelper.Unregister("TaskName");
            }
        }

        [TestCategory("Helpers")]
        [TestMethod]
        public void Test_BackgroundTaskHelper_Register_NoConditions()
        {
            Assert.IsNotNull(BackgroundTaskHelper.Register("TaskName", new TimeTrigger(15, true)));
        }

        [TestCategory("Helpers")]
        [TestMethod]
        public void Test_BackgroundTaskHelper_Register_WithConditions()
        {
            Assert.IsNotNull(BackgroundTaskHelper.Register("TaskName", new TimeTrigger(15, true), false, true, new SystemCondition(SystemConditionType.InternetAvailable), new SystemCondition(SystemConditionType.UserPresent)));
        }

        [TestCategory("Helpers")]
        [TestMethod]
        public void Test_BackgroundTaskHelper_Unregister()
        {
            BackgroundTaskHelper.Register("TaskName", new TimeTrigger(15, true));

            if (!BackgroundTaskHelper.IsBackgroundTaskRegistered("TaskName"))
            {
                Assert.Inconclusive("Task failed to register!");
            }

            BackgroundTaskHelper.Unregister("TaskName");

            Assert.IsFalse(BackgroundTaskHelper.IsBackgroundTaskRegistered("TaskName"));
        }

        [TestCategory("Helpers")]
        [TestMethod]
        public void Test_BackgroundTaskHelper_IsBackgroundTaskRegistered_NoTask()
        {
            Assert.IsFalse(BackgroundTaskHelper.IsBackgroundTaskRegistered("TaskName"));
        }

        [TestCategory("Helpers")]
        [TestMethod]
        public void Test_BackgroundTaskHelper_IsBackgroundTaskRegistered_WithValidTask()
        {
            BackgroundTaskRegistration registeredTask = BackgroundTaskHelper.Register("TaskName", new TimeTrigger(15, true));

            if (registeredTask == null)
            {
                Assert.Inconclusive("Task failed to register");
            }

            Assert.IsTrue(BackgroundTaskHelper.IsBackgroundTaskRegistered("TaskName"));
        }

        [TestCategory("Helpers")]
        [TestMethod]
        public void Test_BackgroundTaskHelper_GetBackgroundTask_NoTask()
        {
            Assert.IsNull(BackgroundTaskHelper.GetBackgroundTask("TaskName"));
        }

        [TestCategory("Helpers")]
        [TestMethod]
        public void Test_BackgroundTaskHelper_GetBackgroundTask_WithValidTask()
        {
            BackgroundTaskRegistration registeredTask = BackgroundTaskHelper.Register("TaskName", new TimeTrigger(15, true));

            if (registeredTask == null)
            {
                Assert.Inconclusive("Task failed to register");
            }

            Assert.IsNotNull(BackgroundTaskHelper.GetBackgroundTask("TaskName"));
        }
    }
}
