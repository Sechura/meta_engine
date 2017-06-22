using System;
using System.Reflection;

namespace Engine
{
    public class TestClassAttribute : Attribute
    {
        public TestClassAttribute()
        {
            // empty
        }
    }

    public class TestMethodAttribute : Attribute
    {
        protected object[] _TestParameter;

        public object[] TestParameters
        {
            get
            {
                return _TestParameter;
            }
        }

        public TestMethodAttribute()
        {
            _TestParameter = new object[0];
        }

        public TestMethodAttribute(object pParameter)
        {
            _TestParameter = new object[1] { pParameter };
        }

        public TestMethodAttribute(object pParameter1, object pParameter2)
        {
            _TestParameter = new object[2] { pParameter1, pParameter2 };
        }

        public TestMethodAttribute(object pParameter1, object pParameter2, object pParameter3)
        {
            _TestParameter = new object[3] { pParameter1, pParameter2, pParameter3 };
        }

        public TestMethodAttribute(object pParameter1, object pParameter2, object pParameter3, object pParameter4)
        {
            _TestParameter = new object[4] { pParameter1, pParameter2, pParameter3, pParameter4 };
        }

        public TestMethodAttribute(object pParameter1, object pParameter2, object pParameter3, object pParameter4, object pParameter5)
        {
            _TestParameter = new object[5] { pParameter1, pParameter2, pParameter3, pParameter4, pParameter5 };
        }

        public TestMethodAttribute(object pParameter1, object pParameter2, object pParameter3, object pParameter4, object pParameter5, object pParameter6)
        {
            _TestParameter = new object[6] { pParameter1, pParameter2, pParameter3, pParameter4, pParameter5, pParameter6 };
        }

        public TestMethodAttribute(object pParameter1, object pParameter2, object pParameter3, object pParameter4, object pParameter5, object pParameter6, object pParameter7)
        {
            _TestParameter = new object[7] { pParameter1, pParameter2, pParameter3, pParameter4, pParameter5, pParameter6, pParameter7 };
        }

        public TestMethodAttribute(object pParameter1, object pParameter2, object pParameter3, object pParameter4, object pParameter5, object pParameter6, object pParameter7, object pParameter8)
        {
            _TestParameter = new object[8] { pParameter1, pParameter2, pParameter3, pParameter4, pParameter5, pParameter6, pParameter7, pParameter8 };
        }
    }

    public static class Test
    {
        public static void IsTrue(bool pExpression)
        {
            if(false == pExpression)
            {
                // report that the assert failed
            }
        }

        public static void IsFalse(bool pExpression)
        {
            if(true == pExpression)
            {
                // report that the assert failed
            }
        }

        public static bool PerformAllTests()
        {
            float fMinutes, fSeconds = (float)Environment.TickCount;
            bool fResult = true;

            // TODO: this is really just a prototype method to test everything
            // in the future this should match up TestMethodAttribute and
            // TestClassAttribute and also run methods in TestClassAttribute
            // even if they aren't marked with TestMethodAttribute.

            try
            {
                Assembly fEntryAssembly = Assembly.GetEntryAssembly();
                Type[] fAssemblyTypes = fEntryAssembly.GetTypes();

                for (int fIndex = 0; fAssemblyTypes.Length > fIndex; fIndex += 1)
                {
                    TypeInfo fTypeInfo = fAssemblyTypes[fIndex].GetTypeInfo();
                    TestMethodAttribute fAttribute = fTypeInfo.GetCustomAttribute<TestMethodAttribute>();

                    if (null != fAttribute)
                    {
                        object fInstance = Activator.CreateInstance(fAssemblyTypes[fIndex]);

                        MethodInfo fMethod = fAssemblyTypes[fIndex].GetMethod(fTypeInfo.Name);
                        MethodInfo fGenericMethod = fMethod.MakeGenericMethod(fAssemblyTypes[fIndex]);

                        if (false == (bool)fGenericMethod.Invoke(fInstance, fAttribute.TestParameters))
                        {
                            fResult = false;

                            // report that the test failed
                        }
                    }
                }
            }
            catch(Exception pException)
            {
                fResult = false;

                // report that the test failed and output the exception info

                Console.WriteLine(pException.Message); // temp, remove later
            }

            fSeconds = ((float)Environment.TickCount - fSeconds) / 1000.0f;
            fMinutes = 60.0f > fSeconds ? 0.0f : fSeconds / 60.0f;

            // write test report

            return fResult;
        }
    }
}
