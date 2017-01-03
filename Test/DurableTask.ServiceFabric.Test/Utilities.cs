﻿//  ----------------------------------------------------------------------------------
//  Copyright Microsoft Corporation
//  Licensed under the Apache License, Version 2.0 (the "License");
//  you may not use this file except in compliance with the License.
//  You may obtain a copy of the License at
//  http://www.apache.org/licenses/LICENSE-2.0
//  Unless required by applicable law or agreed to in writing, software
//  distributed under the License is distributed on an "AS IS" BASIS,
//  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//  See the License for the specific language governing permissions and
//  limitations under the License.
//  ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DurableTask.ServiceFabric.Test
{
    public static class Utilities
    {
        public static async Task ThrowsException<TException>(Func<Task> action, string expectedMessage) where TException : Exception
        {
            try
            {
                await action();
                Assert.Fail($"Method {action.Method} did not throw the expected exception of type {typeof(TException).Name}");
            }
            catch (Exception ex)
            {
                AggregateException aggregate = ex as AggregateException;
                if (aggregate != null)
                {
                    ex = aggregate.InnerException;
                }

                TException expected = ex as TException;
                if (expected == null)
                {
                    Assert.Fail($"Method {action.Method} is expected to throw exception of type {typeof(TException).Name} but has thrown {ex.GetType().Name} instead.");
                }
                else if (!string.Equals(expected.Message, expectedMessage, StringComparison.Ordinal))
                {
                    Assert.Fail($"Method {action.Method} is expected to throw exception with message '{expectedMessage}' but has thrown the message '{expected.Message}' instead.");
                }
            }
        }
    }
}
